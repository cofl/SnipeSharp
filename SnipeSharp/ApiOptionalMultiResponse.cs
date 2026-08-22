using SnipeSharp.Models;
using System;
using System.Runtime.ExceptionServices;

namespace SnipeSharp
{
    /// <summary>
    /// Represents a response value or error, where the value is a collection rather than a single item.
    /// </summary>
    public struct ApiOptionalMultiResponse<T>
        where T : ApiObject
    {
        /// <summary>
        /// Does this response have a value?
        /// </summary>
        public bool HasValue
            => null != Value && Value.Count > 0;

        /// <summary>
        /// The response collection.
        /// </summary>
        public ResponseCollection<T> Value { get; set; }

        /// <summary>
        /// Any error that was thrown while gathering the response.
        /// </summary>
        public Exception Exception { get; set; }

        /// <summary>
        /// Rethrow any exception in this response.
        /// </summary>
        /// <returns>This same response.</returns>
        public ApiOptionalMultiResponse<T> RethrowExceptionIfAny()
        {
            if (null != Exception)
                ExceptionDispatchInfo.Capture(Exception).Throw();
            return this;
        }

        /// <summary>Implicitly cast from an ApiResponse to a multiresponse.</summary>
        public static implicit operator ApiOptionalMultiResponse<T>(ApiOptionalResponse<ResponseCollection<T>> response) => new ApiOptionalMultiResponse<T>
        {
            Value = response.Value,
            Exception = response.Exception
        };
    }
}
