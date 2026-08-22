using SnipeSharp.Models.Enumerations;
using SnipeSharp.Serialization;

namespace SnipeSharp.Models
{
    /// <summary>
    /// A license seat.
    /// </summary>
    public sealed class LicenseSeat : ApiObject, IAvailableActions
    {
        /// <value>Gets the Id of the seat</value>
        [DeserializeAs("id")]
        public long Id { get; private set; }

        /// <value>Gets the name of the seat</value>
        [DeserializeAs("name")]
        public string Name { get; private set; }

        /// <value>Gets the id of the corresponding license.</value>
        [DeserializeAs("license_id")]
        public long LicenseId { get; private set; }

        /// <value>Gets the corresponding assigned user, or null if the license is not assigned to a user.</value>
        /// <remarks>When deserialized, this value does not have all properties filled. Fetch the value using the relevant endpoint to gather the rest of the information.</remarks>
        [DeserializeAs("assigned_user")]
        public User AssignedUser { get; private set; }

        /// <value>Gets the corresponding assigned asset, or null if the license is not assigned to an asset.</value>
        /// <remarks>When deserialized, this value does not have all properties filled. Fetch the value using the relevant endpoint to gather the rest of the information.</remarks>
        [DeserializeAs("assigned_asset")]
        public Asset AssignedAsset { get; private set; }

        /// <value>Gets the location of the assignee, or null if the license is not assigned.</value>
        /// <remarks>When deserialized, this value does not have all properties filled. Fetch the value using the relevant endpoint to gather the rest of the information.</remarks>
        [DeserializeAs("location")]
        public Location Location { get; private set; }

        /// <value>Gets if the license is reassignable.</value>
        /// <seealso cref="License.IsReassignable" />
        [DeserializeAs("reassignable")]
        public bool IsReassignable { get; private set; }

        /// <value>Gets if this seat can be checked out.</value>
        [DeserializeAs("user_can_checkout")]
        public bool? UserCanCheckOut { get; set; }

        /// <inheritdoc />
        /// <seealso cref="License" />
        [DeserializeAs("available_actions", DeserializeAs.AvailableActions)]
        public AvailableAction AvailableActions { get; private set; }

        /// <value>Gets if this seat is checked out to an asset or user or not.</value>
        public bool IsCheckedOut => null != AssignedUser || null != AssignedAsset;
    }
}
