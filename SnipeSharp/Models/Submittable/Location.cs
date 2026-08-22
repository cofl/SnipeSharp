using SnipeSharp.EndPoint;
using SnipeSharp.Models.Enumerations;
using SnipeSharp.Serialization;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SnipeSharp.Models
{
    /// <summary>
    /// A Location.
    /// </summary>
    [PathSegment("locations")]
    public sealed class Location : AbstractBaseModel, IAvailableActions, IPatchable
    {
        /// <summary>Create a new Location object.</summary>
        public Location() { }

        /// <summary>Create a new Location object with the supplied ID, for use with updating.</summary>
        internal Location(int id)
        {
            Id = id;
        }

        /// <inheritdoc />
        /// <remarks>This field is required.</remarks>
        [DeserializeAs("name")]
        [SerializeAs("name", IsRequired = true)]
        [Patch(nameof(isNameModified))]
        public override string Name
        {
            get => name;
            set
            {
                isNameModified = true;
                name = value;
            }
        }
        private bool isNameModified = false;
        private string name;

        /// <value>The URL of the image for this location.</value>
        [DeserializeAs("image")]
        [SerializeAs("image")]
        [Patch(nameof(isImageUriModified))]
        public Uri ImageUri
        {
            get => imageUri;
            set
            {
                isImageUriModified = true;
                imageUri = value;
            }
        }
        private bool isImageUriModified = false;
        private Uri imageUri;

        /// <value>Gets/sets the first address line for this location.</value>
        [DeserializeAs("address")]
        [SerializeAs("address")]
        [Patch(nameof(isAddressModified))]
        public string Address
        {
            get => address;
            set
            {
                isAddressModified = true;
                address = value;
            }
        }
        private bool isAddressModified = false;
        private string address;

        /// <value>Gets/sets the second address line for this location.</value>
        [DeserializeAs("address2")]
        [SerializeAs("address2")]
        [Patch(nameof(isAddress2Modified))]
        public string Address2
        {
            get => address2;
            set
            {
                isAddress2Modified = true;
                address2 = value;
            }
        }
        private bool isAddress2Modified = false;
        private string address2;

        /// <value>Gets/sets the city this location is in.</value>
        [DeserializeAs("city")]
        [SerializeAs("city")]
        [Patch(nameof(isCityModified))]
        public string City
        {
            get => city;
            set
            {
                isCityModified = true;
                city = value;
            }
        }
        private bool isCityModified = false;
        private string city;

        /// <value>Gets/sets the city this location is in.</value>
        [DeserializeAs("state")]
        [SerializeAs("state")]
        [Patch(nameof(isStateModified))]
        public string State
        {
            get => state;
            set
            {
                isStateModified = true;
                state = value;
            }
        }
        private bool isStateModified = false;
        private string state;

        /// <value>Gets/sets the city this location is in.</value>
        [DeserializeAs("country")]
        [SerializeAs("country")]
        [Patch(nameof(isCountryModified))]
        public string Country
        {
            get => country;
            set
            {
                isCountryModified = true;
                country = value;
            }
        }
        private bool isCountryModified = false;
        private string country;

        /// <value>Gets/sets the Zip Code area this location is in.</value>
        [DeserializeAs("zip")]
        [SerializeAs("zip")]
        [Patch(nameof(isZipCodeModified))]
        public string ZipCode
        {
            get => zipCode;
            set
            {
                isZipCodeModified = true;
                zipCode = value;
            }
        }
        private bool isZipCodeModified = false;
        private string zipCode;

        /// <value>The number of assets assigned to this location.</value>
        [DeserializeAs("assigned_assets_count")]
        public int? AssignedAssetsCount { get; private set; }

        /// <value>The number of assets at this location.</value>
        [DeserializeAs("assets_count")]
        public int? AssetsCount { get; private set; }

        /// <value>The number of users at this location.</value>
        [DeserializeAs("users_count")]
        public int? UsersCount { get; private set; }

        /// <value>Gets/sets the type of currency used at this location.</value>
        [DeserializeAs("currency")]
        [SerializeAs("currency", IsRequired = true)]
        [Patch(nameof(isCurrencyModified))]
        public string Currency
        {
            get => currency;
            set
            {
                isCurrencyModified = true;
                currency = value;
            }
        }
        private bool isCurrencyModified = false;
        private string currency;

        /// <value>Gets/sets the parent location for this location.</value>
        [DeserializeAs("parent")]
        [SerializeAs("parent_id", SerializeAs.IdValue)]
        [Patch(nameof(isParentLocationModified))]
        public Stub<Location> ParentLocation
        {
            get => parentLocation;
            set
            {
                isParentLocationModified = true;
                parentLocation = value;
            }
        }
        private bool isParentLocationModified = false;
        private Stub<Location> parentLocation;

        /// <value>The manager for this location.</value>
        /// <remarks>This is actually a full user, not a stub user, (see <code>LocationsTransformer.php</code>).</remarks>
        [DeserializeAs("manager")]
        [SerializeAs("manager_id", SerializeAs.IdValue)]
        [Patch(nameof(isManagerModified))]
        public User Manager
        {
            get => manager;
            set
            {
                isManagerModified = true;
                manager = value;
            }
        }
        private bool isManagerModified = false;
        private User manager;

        /// <value>The list of child locations for this location.</value>
        // this is not a ResponseCollection, so no special deserializer is needed
        [DeserializeAs("children")]
        public IReadOnlyList<Stub<Location>> ChildLocations { get; private set; }

        /// <inheritdoc />
        [DeserializeAs("available_actions", DeserializeAs.AvailableActions)]
        public AvailableAction AvailableActions { get; private set; }

        void IPatchable.SetAllModifiedState(bool isModified)
        {
            isNameModified = isModified;
            isImageUriModified = isModified;
            isAddressModified = isModified;
            isAddress2Modified = isModified;
            isCityModified = isModified;
            isStateModified = isModified;
            isCountryModified = isModified;
            isZipCodeModified = isModified;
            isCurrencyModified = isModified;
            isParentLocationModified = isModified;
            isManagerModified = isModified;
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            ((IPatchable)this).SetAllModifiedState(false);
        }
    }
}
