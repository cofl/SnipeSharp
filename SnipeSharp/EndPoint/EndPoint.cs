using SnipeSharp.Exceptions;
using SnipeSharp.Filters;
using SnipeSharp.Models;
using SnipeSharp.Serialization;
using System;
using System.Linq;
using System.Reflection;

namespace SnipeSharp.EndPoint
{
    /// <summary>
    /// Implements <see cref="IEndPoint{T}" />.
    /// </summary>
    /// <typeparam name="T">A <see cref="Models.AbstractBaseModel">AbstractBaseModel</see> with the <see cref="PathSegmentAttribute">PathSegmentAttribute</see> attribute.</typeparam>
    public class EndPoint<T> : IEndPoint<T>
        where T : AbstractBaseModel
    {
        /// <summary>The API instance this endpoint is a part of.</summary>
        protected readonly SnipeItApi Api;

        /// <summary>The API info for the end point this class wraps.</summary>
        protected readonly PathSegmentAttribute EndPointInfo;

        /// <param name="api">The Api to grab the RequestManager from.</param>
        /// <exception cref="SnipeSharp.Exceptions.MissingRequiredAttributeException">When the type parameter does not have the <see cref="PathSegmentAttribute">PathSegmentAttribute</see> attribute.</exception>
        internal EndPoint(SnipeItApi api)
        {
            Api = api;
            EndPointInfo = typeof(T).GetCustomAttribute<PathSegmentAttribute>();
            if (null == EndPointInfo)
                throw new MissingRequiredAttributeException(nameof(PathSegmentAttribute), typeof(T).Name);
        }

        /// <inheritdoc />
        public ApiOptionalMultiResponse<T> FindAllOptional(ISearchFilter filter = null)
            => Api.RequestManager.GetAll<T>(EndPointInfo.BaseUri, filter);

        /// <inheritdoc />
        public ResponseCollection<T> FindAll(ISearchFilter filter = null)
            => FindAllOptional(filter).RethrowExceptionIfAny().Value;

        /// <inheritdoc />
        public ApiOptionalMultiResponse<T> FindAllOptional(string search)
            => FindAllOptional(new SearchFilter(search));

        /// <inheritdoc />
        public ResponseCollection<T> FindAll(string search)
            => FindAll(new SearchFilter(search));

        /// <inheritdoc />
        public ApiOptionalResponse<T> FindOneOptional(ISearchFilter filter = null)
        {
            if (null == filter)
                filter = new SearchFilter();
            filter.Limit = 1;
            var response = Api.RequestManager.Get<ResponseCollection<T>>(EndPointInfo.BaseUri, filter);
            if (!response.HasValue)
                return new ApiOptionalResponse<T> { Exception = response.Exception };
            return new ApiOptionalResponse<T> { Value = response.Value.FirstOrDefault() };
        }

        /// <inheritdoc />
        public T FindOne(ISearchFilter filter)
            => FindOneOptional(filter).RethrowExceptionIfAny().Value;

        /// <inheritdoc />
        public ApiOptionalResponse<T> FindOneOptional(string search)
            => FindOneOptional(new SearchFilter(search));

        /// <inheritdoc />
        public T FindOne(string search)
            => FindOne(new SearchFilter(search));

        /// <inheritdoc />
        public T Get(int id)
            => GetOptional(id).RethrowExceptionIfAny().Value;

        /// <inheritdoc />
        public ApiOptionalResponse<T> GetOptional(int id)
            => Api.RequestManager.Get<T>($"{EndPointInfo.BaseUri}/{id}");

        /// <inheritdoc />
        public T Get(string name, bool caseSensitive = false, ISearchFilter filter = null)
            => GetOptional(name, caseSensitive, filter).RethrowExceptionIfAny().Value;

        /// <inheritdoc />
        public ApiOptionalResponse<T> GetOptional(string name, bool caseSensitive = false, ISearchFilter filter = null)
        {
            filter = filter ?? new SearchFilter();
            filter.Search = name;
            var comparer = caseSensitive ? StringComparer.Ordinal : StringComparer.OrdinalIgnoreCase;
            var result = FindAllOptional(filter);
            if (!result.HasValue)
                return new ApiOptionalResponse<T> { Exception = result.Exception };
            return new ApiOptionalResponse<T> { Value = result.Value.Where(i => comparer.Equals(i.Name, name)).FirstOrDefault() };
        }

        /// <inheritdoc />
        public ResponseCollection<T> GetAll()
            => FindAll();

        /// <inheritdoc />
        public ApiOptionalMultiResponse<T> GetAllOptional()
            => FindAllOptional();

        /// <inheritdoc />
        public ApiOptionalMultiResponse<T> GetAllOptional(ISearchFilter filter = null)
        => FindAllOptional(filter);

        /// <inheritdoc />
        public T Create(T toCreate)
            => Api.RequestManager.Post(EndPointInfo.BaseUri, CheckRequiredFields(toCreate)).RethrowExceptionIfAny().Value.Payload;

        /// <inheritdoc />
        public RequestResponse<T> Delete(int id)
            => Api.RequestManager.Delete<T>($"{EndPointInfo.BaseUri}/{id}").RethrowExceptionIfAny().Value;

        /// <inheritdoc />
        public T Update(T toUpdate)
            => Api.RequestManager.Patch($"{EndPointInfo.BaseUri}/{toUpdate.Id}", toUpdate).RethrowExceptionIfAny().Value.Payload;

        /// <inheritdoc />
        public T Set(T toSet)
        {
            var patchable = toSet as IPatchable;
            if (null != patchable)
                patchable.SetAllModifiedState(true);
            return Api.RequestManager.Put($"{EndPointInfo.BaseUri}/{toSet.Id}", CheckRequiredFields(toSet)).RethrowExceptionIfAny().Value.Payload;
        }

        /// <inheritdoc />
        public T this[int id]
            => Get(id);

        /// <inheritdoc />
        public T this[string name, bool caseSensitive = false, ISearchFilter filter = null]
            => Get(name, caseSensitive, filter);

        private T CheckRequiredFields(T @object)
        {
            foreach (var property in typeof(T).GetProperties())
                if (property.GetCustomAttribute<SerializeAsAttribute>() is SerializeAsAttribute attribute && attribute.IsRequired && null == property.GetValue(@object))
                    throw new MissingRequiredFieldException<T>(property.Name);
            return @object;
        }
    }
}
