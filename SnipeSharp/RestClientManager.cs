using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using SnipeSharp.Exceptions;
using SnipeSharp.Filters;
using SnipeSharp.Models;
using SnipeSharp.Serialization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

namespace SnipeSharp
{
    internal sealed class RestClientManager
    {
        private readonly SnipeItApi Api;
        private readonly RestClient Client;

        private readonly NewtonsoftJsonSerializer serializerDeserializer = new NewtonsoftJsonSerializer();

        internal RestClientManager(SnipeItApi api, RestClientOptions clientOptions)
        {
            this.Api = api;

            //clientOptions.BaseHost = api.Uri.ToString();
            //clientOptions.BaseUrl = api.Uri;

            this.Client = new RestClient(clientOptions);

            Client.AddDefaultHeader("Accept", "application/json");
            Client.AddDefaultHeader("Cache-Control", "no-cache");
            Client.AddDefaultHeader("Content-type", "application/json");


        }

        internal void SetTokenAndUri()
        {
            if (null == Api.Uri)
                throw new NullApiUriException();
            if (null == Api.Token)
                throw new NullApiTokenException();

        }

        //internal void ResetToken()
        //    => Client.Authenticator = null;
        //internal void ResetUri()
        //    => Client.BaseUrl = null;

        internal string GetRaw(string path)
        {
            var response = Client.Execute(new RestRequest(path));
            if (!response.IsSuccessful)
                throw new ApiErrorException(response.StatusCode, response.Content);
            return response.Content;
        }

        internal ApiOptionalResponse<R> Get<R>(string path, ISearchFilter filter = null)
            where R : ApiObject
        {
            var request = CreateRequest(path, Method.Get, filter);
            SetTokenAndUri();

#if DEBUG
            try
            {
                var uri = Client.BuildUri(request);
                Api.DebugList.Add(uri.ToString());
            }
            catch (System.Exception ex)
            {
                string e = ex.Message.ToString();
            }
#endif

            var response = Client.Execute(request);

            if (!response.IsSuccessful)
                throw new ApiErrorException(response.StatusCode, response.Content);

#if DEBUG
            Api.DebugResponseList.Add(response);
#endif

            var responseJson = JObject.Parse(response.Content);

            var statusToken = responseJson["status"];

            if (statusToken != null &&
                statusToken.Type == JTokenType.String &&
                statusToken.ToString() == "error")
            {
                var asRequestResponse = serializerDeserializer.Deserialize<RequestResponse<R>>(response);

                return new ApiOptionalResponse<R>
                {
                    Exception = asRequestResponse.Messages.ContainsKey("general")
                        ? new ApiErrorException(asRequestResponse.Messages["general"])
                        : new ApiErrorException(asRequestResponse.Messages)
                };
            }

            return new ApiOptionalResponse<R>
            {
                Value = serializerDeserializer.Deserialize<R>(response)
            };
        }

        internal ApiOptionalMultiResponse<R> GetAll<R>(string path, ISearchFilter filter = null) where R : ApiObject
        {
            var result = Get<ResponseCollection<R>>(path, filter);
            // if we couldn't find a result, don't try to get more.
            if (!result.HasValue)
                return result;
            var offset = filter?.Offset ?? 0;

            if ((null == filter?.Limit) && offset + result.Value.Count < result.Value.Total)
            {
                if (null == filter)
                    filter = new SearchFilter();
                filter.Limit = 1000;
                filter.Offset = offset + result.Value.Count;
                while (offset + result.Value.Count < result.Value.Total)
                {
                    var batch = Get<ResponseCollection<R>>(path, filter);
                    if (!batch.HasValue)
                        return batch;
                    if (0 == batch.Value.Count)
                        throw new ApiReturnedInsufficientValuesForRequestException();
                    result.Value.AddRange(batch.Value.Rows);
                    filter.Offset += batch.Value.Count;
                }
            }
            return result;
        }

        internal ApiOptionalResponse<RequestResponse<R>> Post<R>(string path, R obj)
            where R : ApiObject
            => ExecuteRequest<R>(Method.Post, path, obj);

        internal ApiOptionalResponse<RequestResponse<R>> Post<T, R>(string path, T obj)
            where T : ApiObject
            where R : ApiObject
            => ExecuteRequest<R>(Method.Post, path, obj);

        internal ApiOptionalResponse<RequestResponse<R>> Put<R>(string path, R obj)
            where R : ApiObject
            => ExecuteRequest<R>(Method.Put, path, obj);

        internal ApiOptionalResponse<RequestResponse<R>> Patch<R>(string path, R obj)
            where R : ApiObject
            => ExecuteRequest<R>(Method.Patch, path, obj);

        internal ApiOptionalResponse<RequestResponse<R>> Delete<R>(string path)
            where R : ApiObject
            => ExecuteRequest<R>(Method.Delete, path);

        private ApiOptionalResponse<RequestResponse<R>> ExecuteRequest<R>(Method method, string path, ApiObject obj = null)
            where R : ApiObject
        {
            var request = CreateRequest(path, method, obj);
            SetTokenAndUri();
#if DEBUG
            var uri = Client.BuildUri(request);
            Api.DebugList.Add(uri.ToString());
#endif
            var response = Client.Execute(request);
            if (!response.IsSuccessful)
                throw new ApiErrorException(response.StatusCode, response.Content);
#if DEBUG
            Api.DebugResponseList.Add(response);
#endif
            var asRequestResponse = serializerDeserializer.Deserialize<RequestResponse<R>>(response);

            if ("error" == asRequestResponse.Status)
            {
                return new ApiOptionalResponse<RequestResponse<R>>
                {
                    Exception = null != asRequestResponse.Messages && asRequestResponse.Messages.ContainsKey("general")
                        ? new ApiErrorException(asRequestResponse.Messages["general"])
                        : new ApiErrorException(asRequestResponse.Messages)
                };
            }
            return new ApiOptionalResponse<RequestResponse<R>> { Value = asRequestResponse };
        }

        internal RestRequest CreateRequest(string path, Method method, object obj)
        {
            var request = new RestRequest(path, method)
            {
                RequestFormat = DataFormat.Json

                //JsonSerializer = serializerDeserializer
            };

            if (null == obj)
                return request;
            if (Method.Get != method)
            {
                //request.AddJsonBody(obj);

                request.AddBody(serializerDeserializer.Serialize(obj));
#if DEBUG
                Api.DebugRequestList.Add(serializerDeserializer.Serialize(obj));
#endif
                return request;
            }
            var type = obj.GetType();
            foreach (var property in type.GetProperties())
            {
                var attribute = property.GetCustomAttribute<SerializeAsAttribute>(inherit: false);
                if (null == attribute)
                    continue;
                // don't need to bother with PatchAttribute because it only applies to Post and Put (this is Get).
                object value = property.GetValue(obj);
                if (attribute.IsRequired && null == value)
                    throw new MissingRequiredFieldException<object>(type.Name, property.Name);
                if (null == value)
                    // early-out, don't try to use converter, don't add parameter
                    continue;
                if (SerializationContractResolver.TryGetConverter(property, attribute, out var converter))
                {
                    var stringBuilder = new StringBuilder();
                    using (var stringWriter = new StringWriter(stringBuilder))
                    using (var jsonWriter = new JsonTextWriter(stringWriter))
                        converter.WriteJson(jsonWriter, value, NewtonsoftJsonSerializer.Serializer);
                    value = stringBuilder.ToString();
                }
                else if (value.GetType().IsEnum && null != value.GetType().GetCustomAttribute<EnumNameConverterAttribute>())
                {
                    value = value.GetType().GetMember(value.ToString(), BindingFlags.Static | BindingFlags.Public).FirstOrDefault()
                                ?.GetCustomAttribute<EnumMemberAttribute>()?.Value
                                ?? value.ToString();
                }
                request.AddParameter(attribute.Key, value.ToString());
            }
            return request;
        }
    }
}
