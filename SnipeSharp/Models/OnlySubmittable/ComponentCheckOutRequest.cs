using SnipeSharp.Serialization;

namespace SnipeSharp.Models
{
    /// <summary>
    /// A request to associate an Accessory with a User.
    /// </summary>
    public sealed class ComponentCheckOutRequest : ApiObject
    {
        /// <value>The Accessory that will be checked out.</value>
        /// <remarks>This property is not serialized, but instead used for its Id value.</remarks>
        public Component Component { get; private set; }

        /// <value>The assigned user.</value>
        [SerializeAs("assigned_to", SerializeAs.IdValue)]
        public Asset AssignedAsset { get; private set; }

        /// <value>The note to put in the log for this check-out event.</value>
        [SerializeAs("note")]
        public string Note { get; set; }

        ///<value>How many to assign</value>
        [SerializeAs("assigned_qty")]
        public int Quantity { get; set; }

        /// <summary>
        /// Begins a new ComponentCheckOutRequest assigning the supplied component to the supplied asset.
        /// </summary>
        /// <param name="component">The accessory to assign.</param>
        /// <param name="asset">The User to assign the accessory to.</param>
        /// <param name="quantity">How many components to assign to the asset</param>
        public ComponentCheckOutRequest(Component component, Asset asset, int quantity)
        {
            Component = component;
            AssignedAsset = asset;
            Quantity = quantity;
        }
    }
}
