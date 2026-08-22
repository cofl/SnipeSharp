using SnipeSharp.Models;
using System;
using System.Runtime.ExceptionServices;

namespace SnipeSharp
{
    /// <summary>
    /// Represents a response value or error.
    /// </summary>
    public struct ApiOptionalResponse<T>
        where T : ApiObject
    {
        /// <summary>
        /// Does this response have a value?
        /// </summary>
        public bool HasValue
            => null != Value;

        /// <summary>
        /// The response value.
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// Any error that was thrown while gathering the response.
        /// </summary>
        public Exception Exception { get; set; }

        /*public static implicit operator T(ApiOptionalResponse<T> apiOptionalResponse)
        {
            if (apiOptionalResponse.HasValue)
                return apiOptionalResponse.Value;

            // if we don't have a value, rethrow our inner exception.
            ExceptionDispatchInfo.Capture(apiOptionalResponse.Exception).Throw();
            return default;
        }*/

        /// <summary>
        /// Rethrow any exception in this response.
        /// </summary>
        /// <returns>This same response.</returns>
        public ApiOptionalResponse<T> RethrowExceptionIfAny()
        {
            if (null != Exception)
                ExceptionDispatchInfo.Capture(Exception).Throw();
            return this;
        }
    }
}
