using SnipeSharp.EndPoint;
using SnipeSharp.Models.Enumerations;
using SnipeSharp.Serialization;
using System;
using System.Runtime.Serialization;

namespace SnipeSharp.Models
{
    /// <summary>
    /// A Category.
    /// Categories may be checked out to Assets, Accessories, Consumables and Components.
    /// </summary>
    [PathSegment("categories")]
    public sealed class Category : AbstractBaseModel, IAvailableActions, IPatchable
    {
        /// <summary>Create a new Category object.</summary>
        public Category() { }

        /// <summary>Create a new Category object with the supplied ID, for use with updating.</summary>
        internal Category(int id)
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

        /// <summary>The image of this category.</summary>
        [DeserializeAs("image")]
        public Uri Image { get; private set; } //TODO: is this settable in the API?

        /// <summary>
        /// Indicates the type of objects this category may contain.
        /// </summary>
        /// <remarks>Thsi field is required.</remarks>
        [DeserializeAs("category_type")]
        [SerializeAs("category_type", IsRequired = true)]
        [Patch(nameof(isCategoryTypeModified))]
        public CategoryType? CategoryType
        {
            get => categoryType;
            set
            {
                isCategoryTypeModified = true;
                categoryType = value;
            }
        }
        private bool isCategoryTypeModified = false;
        private CategoryType? categoryType;

        /// <summary>
        /// If true, this Category has a Eula or there is a default Eula.
        /// </summary>
        [DeserializeAs("eula")]
        [SerializeAs("eula")]
        [Patch(nameof(isHasEulaModified))]
        public bool? HasEula
        {
            get => hasEula;
            set
            {
                isHasEulaModified = true;
                hasEula = value;
            }
        }
        private bool isHasEulaModified = false;
        private bool? hasEula;

        /// <value>Sets the Eula text for the Category.</value>
        /// <remarks>Can only be used to set the Eula text; this field will never have content after deserialization.</remarks>
        [SerializeAs("eula_text")]
        [Patch(nameof(isEulaTextModified))]
        public string EulaText
        {
            get => eulaText;
            set
            {
                isEulaTextModified = true;
                eulaText = value;
            }
        }
        private bool isEulaTextModified = false;
        private string eulaText;

        /// <value>Sets whether the Category uses the default Eula or its own.</value>
        /// <remarks>
        /// <para>Can only be used to set the Eula text; this field will never have content after deserialization.</para>
        /// <para>(TODO: check)Setting this to true while there is no default Eula set will not throw an error.</para>
        /// </remarks>
        [SerializeAs("use_default_eula")]
        [Patch(nameof(isUsesDefaultEulaModified))]
        public bool? UsesDefaultEula
        {
            get => usesDefaultEula;
            set
            {
                isUsesDefaultEulaModified = true;
                usesDefaultEula = value;
            }
        }
        private bool isUsesDefaultEulaModified = false;
        private bool? usesDefaultEula;

        /// <value>If true, then the user will be emailed with details when the asset is checked in or out.</value>
        [DeserializeAs("checkin_email")]
        [SerializeAs("checkin_email")]
        [Patch(nameof(isEmailUserOnCheckInOrOutModified))]
        public bool? EmailUserOnCheckInOrOut
        {
            get => emailUserOnCheckInOrOut;
            set
            {
                isEmailUserOnCheckInOrOutModified = true;
                emailUserOnCheckInOrOut = value;
            }
        }
        private bool isEmailUserOnCheckInOrOutModified = false;
        private bool? emailUserOnCheckInOrOut;

        /// <value>If true, then the user must confirm acceptance of assets in this category.</value>
        [DeserializeAs("require_acceptance")]
        [SerializeAs("require_acceptance")]
        [Patch(nameof(isIsAcceptanceRequiredModified))]
        public bool? IsAcceptanceRequired
        {
            get => isAcceptanceRequired;
            set
            {
                isIsAcceptanceRequiredModified = true;
                isAcceptanceRequired = value;
            }
        }
        private bool isIsAcceptanceRequiredModified = false;
        private bool? isAcceptanceRequired;

        /// <value>The number of assets in this category.</value>
        [DeserializeAs("assets_count")]
        public int? AssetsCount { get; private set; }

        /// <value>The number of accessories in this category.</value>
        [DeserializeAs("accessories_count")]
        public int? AccessoriesCount { get; private set; }

        /// <value>The number of consumables in this category.</value>
        [DeserializeAs("consumables_count")]
        public int? ConsumablesCount { get; private set; }

        /// <value>The number of components in this category.</value>
        [DeserializeAs("components_count")]
        public int? ComponentsCount { get; private set; }

        /// <value>The number of licenses in this category.</value>
        [DeserializeAs("licenses_count")]
        public int? LicensesCount { get; private set; }

        /// <value>The number of items in this category</value>
        public int? ItemCount
        {
            get
            {
                switch (CategoryType)
                {
                    case Enumerations.CategoryType.Accessory:
                        return AccessoriesCount;
                    case Enumerations.CategoryType.Asset:
                        return AssetsCount;
                    case Enumerations.CategoryType.Component:
                        return ComponentsCount;
                    case Enumerations.CategoryType.Consumable:
                        return ConsumablesCount;
                    case Enumerations.CategoryType.License:
                        return LicensesCount;
                    default:
                        return null;
                }
            }
        }

        /// <inheritdoc />
        [DeserializeAs("available_actions", DeserializeAs.AvailableActions)]
        public AvailableAction AvailableActions { get; private set; }

        void IPatchable.SetAllModifiedState(bool isModified)
        {
            isNameModified = isModified;
            isCategoryTypeModified = isModified;
            isHasEulaModified = isModified;
            isEulaTextModified = isModified;
            isUsesDefaultEulaModified = isModified;
            isEmailUserOnCheckInOrOutModified = isModified;
            isIsAcceptanceRequiredModified = isModified;
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            ((IPatchable)this).SetAllModifiedState(false);
        }
    }
}
