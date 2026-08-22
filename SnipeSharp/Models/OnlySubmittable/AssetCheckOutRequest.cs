using SnipeSharp.Models.Enumerations;
using SnipeSharp.Serialization;
using System;

namespace SnipeSharp.Models
{
    /// <summary>
    /// A request to associate an Asset with a User, Location, or other Asset.
    /// </summary>
    public sealed class AssetCheckOutRequest : ApiObject
    {
        /// <value>The Asset that will be checked out.</value>
        /// <remarks>This property is not serialized, but instead used for its Id value.</remarks>
        public Asset Asset { get; private set; }

        /// <value>The assigned object, if it is a Location.</value>
        [SerializeAs("assigned_location", SerializeAs.IdValue)]
        public Location AssignedLocation { get; private set; }

        /// <value>The assigned object, if it is an Asset.</value>
        [SerializeAs("assigned_asset", SerializeAs.IdValue)]
        public Asset AssignedAsset { get; private set; }

        /// <value>The assigned object, if it is a User.</value>
        [SerializeAs("assigned_user", SerializeAs.IdValue)]
        public User AssignedUser { get; private set; }

        /// <value>What the type of the assigned object is.</value>
        [SerializeAs("checkout_to_type")]
        public AssignedToType AssignedToType { get; private set; }

        /// <value>The date the asset was checked out; if null, then the current timestamp.</value>
        [SerializeAs("checkout_at", SerializeAs.SimpleDate)]
        public DateTime? CheckOutAt { get; set; }

        /// <value>The date the asset is expected to be checked back in.</value>
        [SerializeAs("expected_checkin", SerializeAs.SimpleDate)]
        public DateTime? ExpectedCheckIn { get; set; }

        /// <value>The note to put in the log for this check-out event.</value>
        [SerializeAs("note")]
        public string Note { get; set; }

        /// <value>The new name of the Asset once it is checked out.</value>
        [SerializeAs("name")]
        public string AssetName { get; set; }

        /// <summary>
        /// Begins a new AssetCheckOutRequest assigning the supplied asset to the supplied location.
        /// </summary>
        /// <param name="asset">The asset to assign.</param>
        /// <param name="location">The Location to assign the asset to.</param>
        public AssetCheckOutRequest(Asset asset, Location location)
        {
            Asset = asset;
            AssignedLocation = location;
            AssignedToType = AssignedToType.Location;
        }

        /// <summary>
        /// Begins a new AssetCheckOutRequest assigning the supplied asset to the supplied user.
        /// </summary>
        /// <param name="asset">The asset to assign.</param>
        /// <param name="user">The User to assign the asset to.</param>
        public AssetCheckOutRequest(Asset asset, User user)
        {
            Asset = asset;
            AssignedUser = user;
            AssignedToType = AssignedToType.User;
        }

        /// <summary>
        /// Begins a new AssetCheckOutRequest assigning the supplied asset to another supplied asset.
        /// </summary>
        /// <param name="asset">The asset to assign.</param>
        /// <param name="assignedAsset">The asset to assign the asset to.</param>
        /// <exception cref="ArgumentException">If <paramref name="assignedAsset"/> refers to a deleted asset.</exception>
        public AssetCheckOutRequest(Asset asset, Asset assignedAsset)
        {
            Asset = asset;
            AssignedAsset = assignedAsset;
            if (assignedAsset.IsDeleted)
                throw new ArgumentException("Cannot check out to a deleted asset.", paramName: nameof(assignedAsset));
            AssignedToType = AssignedToType.Asset;
        }
    }
}
