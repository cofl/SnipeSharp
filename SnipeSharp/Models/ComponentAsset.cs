using SnipeSharp.Models.Enumerations;
using SnipeSharp.Serialization;
using System;

namespace SnipeSharp.Models
{
    /// <summary>
    /// A relation between a Component and an Asset.
    /// </summary>
    public sealed class ComponentAsset : ApiObject, IAvailableActions
    {
        /// <value>The ID number of the association row.</value>
        [DeserializeAs("assigned_pivot_id")]
        public int AssignedPivotId { get; private set; }

        /// <value>The ID number of the asset.</value>
        [DeserializeAs("id")]
        public int AssetId { get; private set; }

        /// <value>A name describing the asset.</value>
        [DeserializeAs("name")]
        public string Name { get; private set; }

        /// <value>How many of the component has been checked out to the asset.</value>
        [DeserializeAs("qty")]
        public int Quantity { get; private set; }

        /// <value>The type of the assignee.</value>
        [DeserializeAs("type")]
        public AssignedToType Type { get; private set; }

        /// <value>The creation date of this object in Snipe-IT.</value>
        [DeserializeAs("created_at", DeserializeAs.SimpleDate)]
        public DateTime? CreatedAt { get; private set; }

        /// <inheritdoc />
        [DeserializeAs("available_actions", DeserializeAs.AvailableActions)]
        public AvailableAction AvailableActions { get; private set; }
    }
}
