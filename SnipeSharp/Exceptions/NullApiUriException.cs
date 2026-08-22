using System;

namespace SnipeSharp.Exceptions
{
    /// <summary>
    /// The exception that is thrown when an API request is attempted but no API URI has been specified.
    /// </summary>
    /// <seealso cref="SnipeSharp.SnipeItApi.Uri" />
    public sealed class NullApiUriException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the NullApiUriException class.
        /// </summary>
        public NullApiUriException() : base("No API Uri set.")
        {
        }
    }
}
