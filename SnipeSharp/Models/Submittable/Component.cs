using SnipeSharp.EndPoint;
using SnipeSharp.Models.Enumerations;
using SnipeSharp.Serialization;
using System;
using System.Runtime.Serialization;

namespace SnipeSharp.Models
{
    /// <summary>
    /// A Component.
    /// Components may be checked out to Assets.
    /// </summary>
    [PathSegment("components")]
    public sealed class Component : AbstractBaseModel, IAvailableActions, IPatchable
    {
        /// <summary>Create a new Component object.</summary>
        public Component() { }

        /// <summary>Create a new Component object with the supplied ID, for use with updating.</summary>
        internal Component(int id)
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

        /// <value>The URL of the image for the component.</value>
        [DeserializeAs("image")]
        [Patch(nameof(isImageUriModified))]
        public Uri ImageUri
        {
            get => imageUri;
            private set
            {
                isImageUriModified = true;
                imageUri = value;
            }
        }
        private bool isImageUriModified = false;
        private Uri imageUri;

        /// <value>Gets/sets the serial number for the component.</value>
        [DeserializeAs("serial")]
        [SerializeAs("serial")]
        [Patch(nameof(isSerialModified))]
        public string Serial
        {
            get => serial;
            set
            {
                isSerialModified = true;
                serial = value;
            }
        }
        private bool isSerialModified = false;
        private string serial;

        /// <value>Gets/sets the component's location.</value>
        /// <remarks>
        /// <para>This field will be converted to the value of its Id when serialized.</para>
        /// <para>When deserialized, this value does not have all properties filled. Fetch the value using the relevant endpoint to gather the rest of the information.</para>
        /// </remarks>
        [DeserializeAs("location")]
        [SerializeAs("location_id", SerializeAs.IdValue)]
        [Patch(nameof(isLocationModified))]
        public Stub<Location> Location
        {
            get => location;
            set
            {
                isLocationModified = true;
                location = value;
            }
        }
        private bool isLocationModified = false;
        private Stub<Location> location;

        /// <value>Gets/sets the total quantity of this component.</value>
        /// <remarks>
        /// <para>This value must be greater than or equal to one.</para>
        /// <para>This field is required.</para>
        /// </remarks>
        [DeserializeAs("qty")]
        [SerializeAs("qty", IsRequired = true)]
        [Patch(nameof(isQuantityModified))]
        public int? Quantity
        {
            get => quantity;
            set
            {
                isQuantityModified = true;
                quantity = value;
            }
        }
        private bool isQuantityModified = false;
        private int? quantity;

        /// <value>Gets/sets the minimum quantity before an alert should pop up</value>
        [DeserializeAs("min_amt")]
        [SerializeAs("min_amt")]
        [Patch(nameof(isMinimumQuantityModified))]
        public int? MinimumQuantity
        {
            get => minimumQuantity;
            set
            {
                isMinimumQuantityModified = true;
                minimumQuantity = value;
            }
        }
        private bool isMinimumQuantityModified = false;
        private int? minimumQuantity;

        /// <value>Gets/sets the Category this Component is in.</value>
        /// <remarks>
        /// <para>The Category must have the CategoryType "Component" for the change to be realized in the API; the API won't stop you from giving anything a Category of the wrong type, though.</para>
        /// <para>This field is required.</para>
        /// </remarks>
        [DeserializeAs("category")]
        [SerializeAs("category_id", SerializeAs.IdValue, IsRequired = true)]
        [Patch(nameof(isCategoryModified))]
        public Stub<Category> Category
        {
            get => category;
            set
            {
                isCategoryModified = true;
                category = value;
            }
        }
        private bool isCategoryModified = false;
        private Stub<Category> category;

        /// <value>The order number associated with this Components's purchase.</value>
        [DeserializeAs("order_number")]
        [SerializeAs("order_number")]
        [Patch(nameof(isOrderNumberModified))]
        public string OrderNumber
        {
            get => orderNumber;
            set
            {
                isOrderNumberModified = true;
                orderNumber = value;
            }
        }
        private bool isOrderNumberModified = false;
        private string orderNumber;

        /// <value>The date this Component was purchased.</value>
        [DeserializeAs("purchase_date", DeserializeAs.DateObject)]
        [SerializeAs("purchase_date", SerializeAs.SimpleDate)]
        [Patch(nameof(isPurchaseDateModified))]
        public DateTime? PurchaseDate
        {
            get => purchaseDate;
            set
            {
                isPurchaseDateModified = true;
                purchaseDate = value;
            }
        }
        private bool isPurchaseDateModified = false;
        private DateTime? purchaseDate;

        /// <value>Gets/sets the cost of this Component when purchased.</value>
        [DeserializeAs("purchase_cost")]
        [SerializeAs("purchase_cost")]
        [Patch(nameof(isPurchaseCostModified))]
        public decimal? PurchaseCost
        {
            get => purchaseCost;
            set
            {
                isPurchaseCostModified = true;
                purchaseCost = value;
            }
        }
        private bool isPurchaseCostModified = false;
        private decimal? purchaseCost;

        /// <value>The quantity of this Component that has not yet been checked out.</value>
        [DeserializeAs("remaining")]
        public int? RemainingQuantity { get; private set; }

        /// <value>The Company this Accessory belongs to.</value>
        [DeserializeAs("company")]
        [SerializeAs("company_id", SerializeAs.IdValue)]
        [Patch(nameof(isCompanyModified))]
        public Stub<Company> Company
        {
            get => company;
            set
            {
                isCompanyModified = true;
                company = value;
            }
        }
        private bool isCompanyModified = false;
        private Stub<Company> company;

        /// <value>Indicates that this accessory is available to be checked out.</value>
        [DeserializeAs("user_can_checkout")]
        public bool? UserCanCheckOut { get; private set; }

        /// <inheritdoc />
        [DeserializeAs("available_actions", DeserializeAs.AvailableActions)]
        public AvailableAction AvailableActions { get; private set; }

        void IPatchable.SetAllModifiedState(bool isModified)
        {
            isNameModified = isModified;
            isImageUriModified = isModified;
            isSerialModified = isModified;
            isLocationModified = isModified;
            isQuantityModified = isModified;
            isMinimumQuantityModified = isModified;
            isCategoryModified = isModified;
            isOrderNumberModified = isModified;
            isPurchaseDateModified = isModified;
            isPurchaseCostModified = isModified;
            isCompanyModified = isModified;
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            ((IPatchable)this).SetAllModifiedState(false);
        }
    }
}
