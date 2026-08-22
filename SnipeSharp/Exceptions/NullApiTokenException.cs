using System;

namespace SnipeSharp.Exceptions
{
    /// <summary>
    /// The exception that is thrown when an API request is attempted but no API Token has been specified.
    /// </summary>
    /// <seealso cref="SnipeSharp.SnipeItApi.Token" />
    public sealed class NullApiTokenException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the MissingRequiredAttributeException class.
        /// </summary>
        public NullApiTokenException() : base("No API Token set.")
        {
        }
    }
}
