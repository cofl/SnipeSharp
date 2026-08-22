using System;

namespace SnipeSharp.Exceptions
{
    /// <summary>
    /// The exception that is thrown when an attribute is missing from a class.
    /// </summary>
    public sealed class MissingRequiredAttributeException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the MissingRequiredAttributeException class with specified attribute and class names.
        /// </summary>
        /// <param name="attributeName">The name of the required attribute class.</param>
        /// <param name="className">The name of the class that is missing the required attribute.</param>
        public MissingRequiredAttributeException(string attributeName, string className) : base($"Missing required attribute \"{attributeName}\" on class \"{className}\".")
        {
        }
    }
}
