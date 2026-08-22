using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;

namespace SnipeSharp.Exceptions
{
    /// <summary>
    /// The exception that is thrown when the SnipeIT Api returns a non-success <see cref="System.Net.HttpStatusCode">HttpStatusCode</see> or a successful HTTP status, but an API error status.
    /// </summary>
    public sealed class ApiErrorException : Exception
    {
        /// <summary>
        /// If the HTTP request was not successful, then true, else false.
        /// </summary>
        public readonly bool IsHttpError;

        /// <summary>
        /// The dictionary of messages from the API. May be null.
        /// </summary>
        public readonly IReadOnlyDictionary<string, string> Messages;

        /// <summary>
        /// The HTTP status code associated with the request. May be null.
        /// </summary>
        public readonly HttpStatusCode? HttpStatusCode;

        /// <summary>
        /// Initializes a new instance of the ApiErrorException class with a specified HttpStatusCode and message.
        /// </summary>
        /// <param name="errorCode">The HttpStatusCode of the failed request</param>
        /// <param name="message">The response content or other message, if any.</param>
        public ApiErrorException(HttpStatusCode errorCode, string message) : base(message ?? errorCode.ToString())
        {
            IsHttpError = true;
            HttpStatusCode = errorCode;
        }

        /// <summary>
        /// Initializes a new instance of the ApiErrorException class with a specified message.
        /// </summary>
        /// <param name="message">The message, if any.</param>
        public ApiErrorException(string message) : base(message)
        {
            IsHttpError = false;
        }

        /// <summary>
        /// Initializes a new instance of the ApiErrorException class with a specified dictionary of messages.
        /// </summary>
        /// <param name="messages">The messages map, if any.</param>
        public ApiErrorException(IReadOnlyDictionary<string, string> messages) : this(JsonConvert.SerializeObject(messages, Formatting.None))
        {
            Messages = messages;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            if (IsHttpError)
            {
                var message = Message;
                if (message.Length > 80)
                    message = $"{message.Substring(0, 77)}...";
                return $"{HttpStatusCode}: {message}";
            }
            else
            {
                return base.ToString();
            }
        }
    }
}
