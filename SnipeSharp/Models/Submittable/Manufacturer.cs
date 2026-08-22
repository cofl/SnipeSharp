using SnipeSharp.EndPoint;
using SnipeSharp.Models.Enumerations;
using SnipeSharp.Serialization;
using System;
using System.Runtime.Serialization;

namespace SnipeSharp.Models
{
    /// <summary>
    /// A Manufacturer.
    /// Manufacturers create accessories, consumables, licenses, and models (models are associated with assets).
    /// </summary>
    [PathSegment("manufacturers")]
    public sealed class Manufacturer : AbstractBaseModel, IAvailableActions, IPatchable
    {
        /// <summary>Create a new Manufacturer object.</summary>
        public Manufacturer() { }

        /// <summary>Create a new Manufacturer object with the supplied ID, for use with updating.</summary>
        internal Manufacturer(int id)
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

        /// <value>Gets/sets the URL for the Manufacturer's website.</value>
        [DeserializeAs("url")]
        [SerializeAs("url")]
        [Patch(nameof(isUrlModified))]
        public Uri Url
        {
            get => url;
            set
            {
                isUrlModified = true;
                url = value;
            }
        }
        private bool isUrlModified = false;
        private Uri url;

        /// <value>Gets/sets the URL of the image for the Manufacturer.</value>
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

        /// <value>Gets/sets the url of the manufacturer's support site.</value>
        [DeserializeAs("support_url")]
        [SerializeAs("support_url")]
        [Patch(nameof(isSupportUrlModified))]
        public Uri SupportUrl
        {
            get => supportUrl;
            set
            {
                isSupportUrlModified = true;
                supportUrl = value;
            }
        }
        private bool isSupportUrlModified = false;
        private Uri supportUrl;


        /// <value>Gets/sets the manufacturer's support phone number.</value>
        [DeserializeAs("support_phone")]
        [SerializeAs("support_phone")]
        [Patch(nameof(isSupportPhoneNumberModified))]
        public string SupportPhoneNumber
        {
            get => supportPhoneNumber;
            set
            {
                isSupportPhoneNumberModified = true;
                supportPhoneNumber = value;
            }
        }
        private bool isSupportPhoneNumberModified = false;
        private string supportPhoneNumber;

        /// <value>Gets/sets the manufacturer's support email address.</value>
        [DeserializeAs("support_email")]
        [SerializeAs("support_email")]
        [Patch(nameof(isSupportEmailAddressModified))]
        public string SupportEmailAddress
        {
            get => supportEmailAddress;
            set
            {
                isSupportEmailAddressModified = true;
                supportEmailAddress = value;
            }
        }
        private bool isSupportEmailAddressModified = false;
        private string supportEmailAddress;

        /// <value>The number of assets produced by this manufacturer.</value>
        [DeserializeAs("assets_count")]
        public int? AssetsCount { get; private set; }

        /// <value>The number of licenses produced by this manufacturer.</value>
        [DeserializeAs("licenses_count")]
        public int? LicensesCount { get; private set; }

        /// <value>The number of consumables produced by this manufacturer.</value>
        [DeserializeAs("consumables_count")]
        public int? ConsumablesCount { get; private set; }

        /// <value>The number of accessories produced by this manufacturer.</value>
        [DeserializeAs("accessories_count")]
        public int? AccessoriesCount { get; private set; }

        /// <value>Gets the date this manufacturer was deleted.</value>
        [DeserializeAs("deleted_at", DeserializeAs.Timestamp)]
        public DateTime? DeletedAt { get; private set; }

        /// <inheritdoc />
        [DeserializeAs("available_actions", DeserializeAs.AvailableActions)]
        public AvailableAction AvailableActions { get; private set; }

        void IPatchable.SetAllModifiedState(bool isModified)
        {
            isNameModified = isModified;
            isUrlModified = isModified;
            isImageUriModified = isModified;
            isSupportUrlModified = isModified;
            isSupportPhoneNumberModified = isModified;
            isSupportEmailAddressModified = isModified;
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            ((IPatchable)this).SetAllModifiedState(false);
        }
    }
}
