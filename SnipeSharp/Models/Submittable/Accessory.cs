using SnipeSharp.EndPoint;
using SnipeSharp.Models.Enumerations;
using SnipeSharp.Serialization;
using System;
using System.Runtime.Serialization;

namespace SnipeSharp.Models
{
    /// <summary>
    /// An Accessory.
    /// Accessories may be checked out to Users, but unlike Consumables can be checked back in.
    /// </summary>
    [PathSegment("accessories")]
    public sealed class Accessory : AbstractBaseModel, IAvailableActions, IPatchable
    {
        /// <summary>Create a new Accessory object.</summary>
        public Accessory() { }

        /// <summary>Create a new Accessory object with the supplied ID, for use with updating.</summary>
        internal Accessory(int id)
        {
            Id = id;
        }

        /// <summary>
        /// The Name of the Accessory in Snipe-IT.
        /// </summary>
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

        /// <summary>
        /// The Company this Accessory belongs to.
        /// </summary>
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

        /// <summary>
        /// The Manufacturer that made this Accessory.
        /// </summary>
        [DeserializeAs("manufacturer")]
        [SerializeAs("manufacturer_id", SerializeAs.IdValue, IsRequired = true)]
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

        /// <summary>
        /// The Supplier that sold this Accessory.
        /// </summary>
        [DeserializeAs("supplier")]
        [SerializeAs("supplier_id", SerializeAs.IdValue)]
        [Patch(nameof(isSupplierModified))]
        public Stub<Supplier> Supplier
        {
            get => supplier;
            set
            {
                isSupplierModified = true;
                supplier = value;
            }
        }
        private bool isSupplierModified = false;
        private Stub<Supplier> supplier;

        /// <summary>
        /// The ModelNumber of this Accessory.
        /// </summary>
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

        /// <summary>
        /// The Category this Accessory is in.
        /// </summary>
        /// <remarks>
        /// <para>The Category must have the CategoryType "Accessory" for the change to be realized in the API; the API won't stop you from giving anything a Category of the wrong type, though.</para>
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

        /// <summary>
        /// The Location this Accessory is in.
        /// </summary>
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

        /// <summary>
        /// Any notes on the Accessory.
        /// </summary>
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

        /// <summary>
        /// The total quantity of this Accessory.
        /// </summary>
        /// <remarks>
        /// <para>This value must be greater than or equal to one.</para>
        /// <para>This field is required.</para>
        /// </remarks>
        [DeserializeAs("qty")]
        [SerializeAs("qty", IsRequired = true)]
        [Patch(nameof(isQuantityModified))]
        public uint? Quantity
        {
            get => quantity;
            set
            {
                isQuantityModified = true;
                quantity = value;
            }
        }
        private bool isQuantityModified = false;
        private uint? quantity;

        /// <summary>
        /// The date this Accessory was purchased.
        /// </summary>
        [DeserializeAs("purchase_date", DeserializeAs.DateObject)]
        [SerializeAs("purchase_date", SerializeAs.SimpleDateOnly)]
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

        /// <summary>
        /// The cost of this Accessory when purchased.
        /// </summary>
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

        /// <summary>
        /// The order number associated with this Accessory's purchase.
        /// </summary>
        /// <remarks>A single Accessory only has one OrderNumber field. Multiple orders should use multiple Accessories of the same ModelNumber, IIRC.</remarks>
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

        /// <summary>
        /// The Minimum quantity of this Accessory before an alert should pop up.
        /// </summary>
        /// <remarks>Supposedly this is setable, but the field is not fillable in Snipe-IT.</remarks>
        [DeserializeAs("min_qty")]
        [SerializeAs("min_amt")]
        [Patch(nameof(isMinimumQuantityModified))]
        public uint? MinimumQuantity
        {
            get => minimumQuantity;
            set
            {
                isMinimumQuantityModified = true;
                minimumQuantity = value;
            }
        }
        private bool isMinimumQuantityModified = false;
        private uint? minimumQuantity;

        /// <summary>
        /// The quantity of this Accessory that has not yet been checked out.
        /// </summary>
        [DeserializeAs("remaining_qty")]
        public int? RemainingQuantity { get; private set; }

        /// <summary>
        /// The Url of the image for this Accessory in the web interface.
        /// </summary>
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

        /// <inheritdoc />
        [DeserializeAs("available_actions", DeserializeAs.AvailableActions)]
        public AvailableAction AvailableActions { get; private set; }

        /// <summary>
        /// Indicates that this accessory is available to be checked out.
        /// </summary>
        /// <remarks>This is the same as <code><see cref="RemainingQuantity"/> &gt; 0</code>.</remarks>
        [DeserializeAs("user_can_checkout")]
        public bool? UserCanCheckOut { get; private set; }

        void IPatchable.SetAllModifiedState(bool isModified)
        {
            isNameModified = isModified;
            isCompanyModified = isModified;
            isManufacturerModified = isModified;
            isSupplierModified = isModified;
            isModelNumberModified = isModified;
            isCategoryModified = isModified;
            isLocationModified = isModified;
            isQuantityModified = isModified;
            isNotesModified = isModified;
            isPurchaseCostModified = isModified;
            isPurchaseDateModified = isModified;
            isOrderNumberModified = isModified;
            isMinimumQuantityModified = isModified;
            isImageUriModified = isModified;
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            ((IPatchable)this).SetAllModifiedState(false);
        }
    }
}
