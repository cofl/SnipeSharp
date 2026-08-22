using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serializers;
//using RestSharp.Deserializers;
//using RestSharp.Serialization;

namespace SnipeSharp.Tests
{
    internal sealed class FakeResponse : RestResponse
    {
        internal static FakeResponse FromFile(string path, bool isSuccessful = true, HttpStatusCode? statusCode = null)
            => new FakeResponse(null == path ? string.Empty : File.ReadAllText(path), isSuccessful, statusCode);

        public FakeResponse(string content, bool isSuccessful = true, HttpStatusCode? statusCode = null)
        {
            Content = content;
            IsSuccessful = isSuccessful;
            if(statusCode.HasValue)
                StatusCode = statusCode.Value;
        }
        public RestRequest Request { get; set; }
        public string ContentType { get; set; }
        public long ContentLength { get; set; }
        public string ContentEncoding { get; set; }
        public string Content { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccessful { get; set; }
        public string StatusDescription { get; set; }
        public byte[] RawBytes { get; set; }
        public Uri ResponseUri { get; set; }
        public string Server { get; set; }
        //public IList<RestResponseCookie> Cookies => throw new NotImplementedException();
        public IList<Parameter> Headers => throw new NotImplementedException();
        public ResponseStatus ResponseStatus { get; set; }
        public string ErrorMessage { get; set; }
        public Exception ErrorException { get; set; }
        public Version ProtocolVersion { get; set; }
    }

    internal sealed class FakeRequest
    {
        internal RestRequest Request;
        internal string Body;
        internal Method Method;
        internal Dictionary<string, string> Headers = new Dictionary<string, string>();
    }

    internal sealed class FakeRestClient : RestClient
    {
        internal readonly Queue<RestResponse> Responses = new Queue<RestResponse>();
        internal readonly LinkedList<FakeRequest> Requests = new LinkedList<FakeRequest>();
        private IAuthenticator authenticator;

        public RestResponse Response { get => Responses.Dequeue(); set {} }
        public string UserAgent { get; set; } = "StubRestClient";
        public RestResponse Execute(RestRequest request) =>  Execute(request, request.Method);
        public RestResponse Execute(RestRequest request, Method httpMethod)
        {
            var fakeRequest = new FakeRequest {
                Request = request,
                Method = httpMethod
            };
            foreach(var parameter in request.Parameters)
            {
                if(parameter.Type == ParameterType.RequestBody)
                {
                    fakeRequest.Body = parameter.Value.ToString();
                } else if(parameter.Type == ParameterType.HttpHeader)
                {
                    fakeRequest.Headers[parameter.Name] = parameter.Value.ToString();
                }
            }
            Requests.AddFirst(fakeRequest);
            return Response;
        }
        public Uri BuildUri(RestRequest request) => Utility.TEST_URI;

        public CookieContainer CookieContainer { get; set; } = new CookieContainer();
        public bool AutomaticDecompression { get; set; } = false;
        public int? MaxRedirects { get; set; }
        public int Timeout { get; set; } = int.MaxValue;
        public int ReadWriteTimeout { get; set; } = int.MaxValue;
        public bool UseSynchronizationContext { get; set; } = false;
        public new IAuthenticator Authenticator { get => authenticator; set => authenticator = value; }
        public Uri BaseUrl { get; set; }
        public Encoding Encoding { get; set; } = Encoding.UTF8;
        public bool FailOnDeserializationError { get; set; } = true;
        public string ConnectionGroupName { get; set; }
        public bool PreAuthenticate { get; set; } = false;
        public bool UnsafeAuthenticatedConnectionSharing { get; set; } = false;
        public new IList<Parameter> DefaultParameters { get; set; } = new List<Parameter>();

        public string BaseHost { get; set; }
        public bool AllowMultipleDefaultParametersWithSameName { get; set; } = false;
        public X509CertificateCollection ClientCertificates { get; set; }
        public IWebProxy Proxy { get; set; }
        public RequestCachePolicy CachePolicy { get; set; }
        public bool Pipelined { get; set; } = false;
        public bool FollowRedirects { get; set; } = false;
        public RemoteCertificateValidationCallback RemoteCertificateValidationCallback { get; set; }

        //public void AddHandler(string contentType, R deserializer)
        //    => throw new NotImplementedException();

        //public void AddHandler(string contentType, Func<IDeserializer> deserializerFactory)
        //    => throw new NotImplementedException();

        public string BuildUriWithoutQueryParameters(RestRequest request)
            => throw new NotImplementedException();

        public void ClearHandlers(){}

        public void ConfigureWebRequest(Action<HttpWebRequest> configurator){}

        public RestResponse<T> Deserialize<T>(RestResponse response)
            => throw new NotImplementedException();

        public byte[] DownloadData(RestRequest request)
            => throw new NotImplementedException();

        public byte[] DownloadData(RestRequest request, bool throwOnError)
            => throw new NotImplementedException();

        public RestResponse<T> Execute<T>(RestRequest request) where T : new()
            => throw new NotImplementedException();

        public RestResponse<T> Execute<T>(RestRequest request, Method httpMethod) where T : new()
            => throw new NotImplementedException();

        public RestResponse ExecuteAsGet(RestRequest request, string httpMethod)
            => throw new NotImplementedException();

        public RestResponse<T> ExecuteAsGet<T>(RestRequest request, string httpMethod) where T : new()
            => throw new NotImplementedException();

        public RestResponse ExecuteAsPost(RestRequest request, string httpMethod)
            => throw new NotImplementedException();

        public RestResponse<T> ExecuteAsPost<T>(RestRequest request, string httpMethod) where T : new()
            => throw new NotImplementedException();

        public RestRequestAsyncHandle ExecuteAsync(RestRequest request, Action<RestResponse, RestRequestAsyncHandle> callback)
            => throw new NotImplementedException();

        public RestRequestAsyncHandle ExecuteAsync<T>(RestRequest request, Action<RestResponse<T>, RestRequestAsyncHandle> callback)
            => throw new NotImplementedException();

        public RestRequestAsyncHandle ExecuteAsync(RestRequest request, Action<RestResponse, RestRequestAsyncHandle> callback, Method httpMethod)
            => throw new NotImplementedException();

        public RestRequestAsyncHandle ExecuteAsync<T>(RestRequest request, Action<RestResponse<T>, RestRequestAsyncHandle> callback, Method httpMethod)
            => throw new NotImplementedException();

        public RestRequestAsyncHandle ExecuteAsyncGet(RestRequest request, Action<RestResponse, RestRequestAsyncHandle> callback, string httpMethod)
            => throw new NotImplementedException();

        public RestRequestAsyncHandle ExecuteAsyncGet<T>(RestRequest request, Action<RestResponse<T>, RestRequestAsyncHandle> callback, string httpMethod)
            => throw new NotImplementedException();

        public RestRequestAsyncHandle ExecuteAsyncPost(RestRequest request, Action<RestResponse, RestRequestAsyncHandle> callback, string httpMethod)
            => throw new NotImplementedException();

        public RestRequestAsyncHandle ExecuteAsyncPost<T>(RestRequest request, Action<RestResponse<T>, RestRequestAsyncHandle> callback, string httpMethod)
            => throw new NotImplementedException();

        public Task<RestResponse<T>> ExecuteGetTaskAsync<T>(RestRequest request)
            => throw new NotImplementedException();

        public Task<RestResponse<T>> ExecuteGetTaskAsync<T>(RestRequest request, CancellationToken token)
            => throw new NotImplementedException();

        public Task<RestResponse> ExecuteGetTaskAsync(RestRequest request)
            => throw new NotImplementedException();

        public Task<RestResponse> ExecuteGetTaskAsync(RestRequest request, CancellationToken token)
            => throw new NotImplementedException();

        public Task<RestResponse<T>> ExecutePostTaskAsync<T>(RestRequest request)
            => throw new NotImplementedException();

        public Task<RestResponse<T>> ExecutePostTaskAsync<T>(RestRequest request, CancellationToken token)
            => throw new NotImplementedException();

        public Task<RestResponse> ExecutePostTaskAsync(RestRequest request)
            => throw new NotImplementedException();

        public Task<RestResponse> ExecutePostTaskAsync(RestRequest request, CancellationToken token)
            => throw new NotImplementedException();

        public Task<RestResponse<T>> ExecuteTaskAsync<T>(RestRequest request, CancellationToken token)
            => throw new NotImplementedException();

        public Task<RestResponse<T>> ExecuteTaskAsync<T>(RestRequest request, Method httpMethod)
            => throw new NotImplementedException();

        public Task<RestResponse<T>> ExecuteTaskAsync<T>(RestRequest request)
            => throw new NotImplementedException();

        public Task<RestResponse> ExecuteTaskAsync(RestRequest request, CancellationToken token)
            => throw new NotImplementedException();

        public Task<RestResponse> ExecuteTaskAsync(RestRequest request, CancellationToken token, Method httpMethod)
            => throw new NotImplementedException();

        public Task<RestResponse> ExecuteTaskAsync(RestRequest request)
            => throw new NotImplementedException();

        public void RemoveHandler(string contentType){}

        public IRestClient UseQueryEncoder(Func<string, Encoding, string> queryEncoder)
            => throw new NotImplementedException();

        public IRestClient UseSerializer(IRestSerializer serializer)
            => throw new NotImplementedException();

        public IRestClient UseUrlEncoder(Func<string, string> encoder)
            => throw new NotImplementedException();
    }
}
