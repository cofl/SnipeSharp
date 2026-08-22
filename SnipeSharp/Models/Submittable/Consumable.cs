using SnipeSharp.EndPoint;
using SnipeSharp.Models.Enumerations;
using SnipeSharp.Serialization;
using System;
using System.Runtime.Serialization;

namespace SnipeSharp.Models
{
    /// <summary>
    /// A Consumable.
    /// Consumables may be checked out to Users, but unlike Accessories cannot be checked back in.
    /// </summary>
    [PathSegment("consumables")]
    public sealed class Consumable : AbstractBaseModel, IAvailableActions, IPatchable
    {
        /// <summary>Create a new Consumable object.</summary>
        public Consumable() { }

        /// <summary>Create a new Consumable object with the supplied ID, for use with updating.</summary>
        internal Consumable(int id)
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

        /// <value>Gets the URL of the image for this consumable.</value>
        /// <remarks>Currently, this field is not fillable.</remarks>
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

        /// <value>Gets/sets the category of the consumable.</value>
        /// <remarks>
        /// <para>The Category must have the CategoryType "Consumable" for the change to be realized in the API; the API won't stop you from giving anything a Category of the wrong type, though.</para>
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

        /// <value>Gets/sets the company that owns this consumable.</value>
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

        /// <value>Gets/sets the item number of this consumable.</value>
        [DeserializeAs("item_no")]
        [SerializeAs("item_no")]
        [Patch(nameof(isItemNumberModified))]
        public string ItemNumber
        {
            get => itemNumber;
            set
            {
                isItemNumberModified = true;
                itemNumber = value;
            }
        }
        private bool isItemNumberModified = false;
        private string itemNumber;

        /// <value>Gets/sets location for this consumable.</value>
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

        /// <value>Gets/sets the manufacturer who produced this consumable.</value>
        [DeserializeAs("manufacturer")]
        [SerializeAs("manufacturer_id", SerializeAs.IdValue)]
        [Patch(nameof(isManufacturerModified))]
        public Stub<Manufacturer> Manufacturer
        {
            get => manufacturer;
            set
            {
                isManufacturerModified = true;
                manufacturer = value;
            }
        }
        private bool isManufacturerModified = false;
        private Stub<Manufacturer> manufacturer;

        /// <value>Gets/sets the total quantity of this consumable.</value>
        /// <remarks>This field is required.</remarks>
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

        /// <value>Gets/sets the minimum quantity of this consumable before an alert is raised.</value>
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

        /// <value>Gets/sets the model number of this consumable.</value>
        [DeserializeAs("model_number")]
        [SerializeAs("model_number")]
        [Patch(nameof(isModelNumberModified))]
        public string ModelNumber
        {
            get => modelNumber;
            set
            {
                isModelNumberModified = true;
                modelNumber = value;
            }
        }
        private bool isModelNumberModified = false;
        private string modelNumber;

        /// <value>Gets the remaining quantity of this consumable.</value>
        [DeserializeAs("remaining")]
        public int? RemainingQuantity { get; private set; }

        /// <value>Gets/sets the order number associated with this consumable's purchase.</value>
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

        /// <value>The cost of this Consumable when purchased.</value>
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

        /// <value>The date this Consumable was purchased.</value>
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

        /// <value>Gets whether this consumable can be checked out or not.</value>
        [DeserializeAs("user_can_checkout")]
        public bool? UserCanCheckOut { get; private set; }

        /// <inheritdoc />
        [DeserializeAs("available_actions", DeserializeAs.AvailableActions)]
        public AvailableAction AvailableActions { get; private set; }

        /// <value>Gets/sets if this consumable is requestable or not.</value>
        [DeserializeAs("requestable")]
        [SerializeAs("requestable")]
        [Patch(nameof(isIsRequestableModified))]
        public bool? IsRequestable
        {
            get => isRequestable;
            set
            {
                isIsRequestableModified = true;
                isRequestable = value;
            }
        }
        private bool isIsRequestableModified = false;
        private bool? isRequestable;

        void IPatchable.SetAllModifiedState(bool isModified)
        {
            isNameModified = isModified;
            isImageUriModified = isModified;
            isCategoryModified = isModified;
            isCompanyModified = isModified;
            isItemNumberModified = isModified;
            isLocationModified = isModified;
            isManufacturerModified = isModified;
            isQuantityModified = isModified;
            isMinimumQuantityModified = isModified;
            isModelNumberModified = isModified;
            isOrderNumberModified = isModified;
            isPurchaseCostModified = isModified;
            isPurchaseDateModified = isModified;
            isIsRequestableModified = isModified;
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            ((IPatchable)this).SetAllModifiedState(false);
        }
    }
}
