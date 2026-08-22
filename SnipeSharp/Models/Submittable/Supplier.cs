using SnipeSharp.EndPoint;
using SnipeSharp.Models.Enumerations;
using SnipeSharp.Serialization;
using System;
using System.Runtime.Serialization;

namespace SnipeSharp.Models
{
    /// <summary>
    /// A supplier.
    /// Suppliers sell assets, licenses, and accessories.
    /// </summary>
    [PathSegment("suppliers")]
    public sealed class Supplier : AbstractBaseModel, IAvailableActions, IPatchable
    {
        /// <summary>Create a new Supplier object.</summary>
        public Supplier() { }

        /// <summary>Create a new Supplier object with the supplied ID, for use with updating.</summary>
        internal Supplier(int id)
        {
            Id = id;
        }

        /// <inheritdoc />
        /// <remarks>This field is required and must be unique among undeleted suppliers.</remarks>
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

        /// <value>The URL of the image for this supplier.</value>
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

        /// <value>Gets/sets the first address line for this supplier.</value>
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

        /// <value>Gets/sets the second address line for this supplier.</value>
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

        /// <value>Gets/sets the city this supplier is in.</value>
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

        /// <value>Gets/sets the state this supplier is in.</value>
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

        /// <value>Gets/sets the country this supplier is in.</value>
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

        /// <value>Gets/sets the Zip Code area this supplier is in.</value>
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

        /// <value>Gets/sets the fax number for this supplier.</value>
        [DeserializeAs("fax")]
        [SerializeAs("fax")]
        [Patch(nameof(isFaxNumberModified))]
        public string FaxNumber
        {
            get => faxNumber;
            set
            {
                isFaxNumberModified = true;
                faxNumber = value;
            }
        }
        private bool isFaxNumberModified = false;
        private string faxNumber;

        /// <value>Gets/sets the phone number for this supplier.</value>
        [DeserializeAs("phone")]
        [SerializeAs("phone")]
        [Patch(nameof(isPhoneNumberModified))]
        public string PhoneNumber
        {
            get => phoneNumber;
            set
            {
                isPhoneNumberModified = true;
                phoneNumber = value;
            }
        }
        private bool isPhoneNumberModified = false;
        private string phoneNumber;

        /// <value>Gets/sets the contact email address for this supplier.</value>
        [DeserializeAs("email")]
        [SerializeAs("email")]
        [Patch(nameof(isEmailAddressModified))]
        public string EmailAddress
        {
            get => emailAddress;
            set
            {
                isEmailAddressModified = true;
                emailAddress = value;
            }
        }
        private bool isEmailAddressModified = false;
        private string emailAddress;

        /// <value>Gets the contact for this supplier.</value>
        [DeserializeAs("contact")]
        [SerializeAs("contact")]
        [Patch(nameof(isContactModified))]
        public string Contact
        {
            get => contact;
            set
            {
                isContactModified = true;
                contact = value;
            }
        }
        private bool isContactModified = false;
        private string contact;

        /// <value>Gets the number of assets purchased from this supplier.</value>
        [DeserializeAs("assets_count")]
        public int? AssetsCount { get; private set; }

        /// <value>Gets the number of accessories purchased from this supplier.</value>
        [DeserializeAs("accessories_count")]
        public int? AccessoriesCount { get; private set; }

        /// <value>Gets the number of licenses purchased from this supplier</value>
        [DeserializeAs("licenses_count")]
        public int? LicensesCount { get; private set; }

        /// <value>Gets/sets the notes or description for this supplier.</value>
        [DeserializeAs("notes")]
        [SerializeAs("notes")]
        [Patch(nameof(isNotesModified))]
        public string Notes
        {
            get => notes;
            set
            {
                isNotesModified = true;
                notes = value;
            }
        }
        private bool isNotesModified = false;
        private string notes;

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
            isFaxNumberModified = isModified;
            isPhoneNumberModified = isModified;
            isEmailAddressModified = isModified;
            isContactModified = isModified;
            isNotesModified = isModified;
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            ((IPatchable)this).SetAllModifiedState(false);
        }
    }
}
