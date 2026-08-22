using SnipeSharp.EndPoint;
using SnipeSharp.Models.Enumerations;
using SnipeSharp.Serialization;
using System;
using System.Runtime.Serialization;

namespace SnipeSharp.Models
{
    /// <summary>
    /// A Company.
    /// Companies own assets, licenses, components, etc., and has users that work for it.
    /// </summary>
    [PathSegment("companies")]
    public sealed class Company : AbstractBaseModel, IAvailableActions, IPatchable
    {
        /// <summary>Create a new Company object.</summary>
        public Company() { }

        /// <summary>Create a new Company object with the supplied ID, for use with updating.</summary>
        internal Company(int id)
        {
            Id = id;
        }

        private string name;
        private bool isNameModified;
        /// <inheritdoc />
        /// <remarks>This field is required, and must be unique.</remarks>
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

        /// <summary>
        /// The url for the image of this company.
        /// </summary>
        [DeserializeAs("image")]
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
        private bool isImageUriModified;
        private Uri imageUri;

        /// <value>The number of assets this company owns.</value>
        [DeserializeAs("assets_count")]
        public int AssetsCount { get; set; }

        /// <value>The number of licenses this company owns.</value>
        [DeserializeAs("licenses_count")]
        public int LicensesCount { get; set; }

        /// <value>The number of accessories this company owns.</value>
        [DeserializeAs("accessories_count")]
        public int AccessoriesCount { get; set; }

        /// <value>The number of consumables this company owns.</value>
        [DeserializeAs("consumables_count")]
        public int ConsumablesCount { get; set; }

        /// <value>The number of components this company owns.</value>
        [DeserializeAs("components_count")]
        public int ComponentsCount { get; set; }

        /// <value>The number of users in this company.</value>
        [DeserializeAs("users_count")]
        public int UsersCount { get; set; }

        /// <inheritdoc />
        [DeserializeAs("available_actions", DeserializeAs.AvailableActions)]
        public AvailableAction AvailableActions { get; private set; }

        void IPatchable.SetAllModifiedState(bool isModified)
        {
            isNameModified = isModified;
            isImageUriModified = isModified;
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            ((IPatchable)this).SetAllModifiedState(false);
        }
    }
}
