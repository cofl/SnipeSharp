using System;

namespace SnipeSharp.Exceptions
{
    /// <summary>
    /// The exception that is thrown when retrieving additional batches of data
    /// for requests, and no values are returned, even though neither the limit
    /// nor the total have been reached.
    /// </summary>
    /// <seealso cref="RestClientManager.GetAll{R}(string, Filters.ISearchFilter)"/>
    public sealed class ApiReturnedInsufficientValuesForRequestException : Exception
    {
    }
}
