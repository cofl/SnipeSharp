using SnipeSharp.Models.Enumerations;

namespace SnipeSharp.Models
{
    /// <summary>
    /// Indicates that this object supports listing actions that are available.
    /// </summary>
    internal interface IAvailableActions
    {
        /// <summary>
        /// The set of actions that are available on this object.
        /// </summary>
        /// <remarks>
        /// This is returned from the Api as a Dictionary&lt;string,bool&gt;, so only the true values are put in here.
        /// </remarks>
        AvailableAction AvailableActions { get; }
    }
}
