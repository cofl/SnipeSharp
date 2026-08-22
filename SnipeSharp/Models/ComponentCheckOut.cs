using SnipeSharp.Models.Enumerations;
using SnipeSharp.Serialization;

namespace SnipeSharp.Models
{
    /// <summary>
    /// A Componenet/Asset relation.
    /// </summary>
    public sealed class ComponentCheckOut : ApiObject, IAvailableActions
    {
        /// <summary>
        /// The Id of the Component in Snipe-IT.
        /// </summary>
        [DeserializeAs("assigned_pivot_id")]
        public int ComponentId { get; private set; }

        /// <summary>
        /// The Id of the User in Snipe-IT.
        /// </summary>
        [DeserializeAs("id")]
        public int AssetId { get; private set; }


        /// <summary>
        /// The assignee type of this particular check out.
        /// </summary>
        /// <remarks>Currently, this field will always be Asset.</remarks>
        [DeserializeAs("type")]
        public AssignedToType Type { get; private set; }

        /// <inheritdoc />
        /// <remarks>Currently, this will always be <c>{CheckIn}</c>.</remarks>
        [DeserializeAs("available_actions", DeserializeAs.AvailableActions)]
        public AvailableAction AvailableActions { get; private set; }
    }
}
