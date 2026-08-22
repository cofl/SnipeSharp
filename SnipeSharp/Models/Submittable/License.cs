using SnipeSharp.EndPoint;
using SnipeSharp.Models.Enumerations;
using SnipeSharp.Serialization;
using System;
using System.Runtime.Serialization;

namespace SnipeSharp.Models
{
    /// <summary>
    /// A License.
    /// Licenses may be checked out to Assets or Users.
    /// </summary>
    [PathSegment("licenses")]
    public sealed class License : AbstractBaseModel, IAvailableActions, IPatchable
    {
        /// <summary>Create a new License object.</summary>
        public License() { }

        /// <summary>Create a new License object with the supplied ID, for use with updating.</summary>
        internal License(int id)
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

        /// <value>The company that owns this license.</value>
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

        /// <value>The manufacturer that produced this license.</value>
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

        /// <value>The Product Key for this license.</value>
        [DeserializeAs("product_key")]
        [SerializeAs("serial")]
        [Patch(nameof(isProductKeyModified))]
        public string ProductKey
        {
            get => productKey;
            set
            {
                isProductKeyModified = true;
                productKey = value;
            }
        }
        private bool isProductKeyModified = false;
        private string productKey;

        /// <value>The supplier order number associated with this License's purchase.</value>
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

        /// <value>The purchase order associated with this License's purchase.</value>
        [DeserializeAs("purchase_order")]
        [SerializeAs("purchase_order")]
        [Patch(nameof(isPurchaseOrderModified))]
        public string PurchaseOrder
        {
            get => purchaseOrder;
            set
            {
                isPurchaseOrderModified = true;
                purchaseOrder = value;
            }
        }
        private bool isPurchaseOrderModified = false;
        private string purchaseOrder;

        /// <value>The date this License was purchased.</value>
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

        /// <value>The cost of this License when purchased.</value>
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

        /// <value>The description for this License.</value>
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

        /// <value>The date this license expires. This is not the TerminationDate!</value>
        [DeserializeAs("expiration_date", DeserializeAs.DateObject)]
        [SerializeAs("expiration_date", SerializeAs.SimpleDate)]
        public DateTime? ExpirationDate { get; private set; }

        /// <value>The number of seats this license is good for.</value>
        /// <remarks>This field is required.</remarks>
        [DeserializeAs("seats")]
        [SerializeAs("seats", IsRequired = true)]
        [Patch(nameof(isTotalSeatsModified))]
        public int? TotalSeats
        {
            get => totalSeats;
            set
            {
                isTotalSeatsModified = true;
                totalSeats = value;
            }
        }
        private bool isTotalSeatsModified = false;
        private int? totalSeats;

        /// <value>The number of remaining seats on this license.</value>
        [DeserializeAs("free_seats_count")]
        public int? FreeSeats { get; private set; }

        /// <value>The name of the entity this license is licensed to.</value>
        [DeserializeAs("license_name")]
        [SerializeAs("license_name")]
        [Patch(nameof(isLicensedToNameModified))]
        public string LicensedToName
        {
            get => licensedToName;
            set
            {
                isLicensedToNameModified = true;
                licensedToName = value;
            }
        }
        private bool isLicensedToNameModified = false;
        private string licensedToName;

        /// <value>The email address of the entity this license is licensed to.</value>
        [DeserializeAs("license_email")]
        [SerializeAs("license_email")]
        [Patch(nameof(isLicensedToEmailAddressModified))]
        public string LicensedToEmailAddress
        {
            get => licensedToEmailAddress;
            set
            {
                isLicensedToEmailAddressModified = true;
                licensedToEmailAddress = value;
            }
        }
        private bool isLicensedToEmailAddressModified = false;
        private string licensedToEmailAddress;

        /// <value>Whether or not this license is maintained.</value>
        [DeserializeAs("maintained")]
        [SerializeAs("maintained")]
        [Patch(nameof(isIsMaintainedModified))]
        public bool? IsMaintained
        {
            get => isMaintained;
            set
            {
                isIsMaintainedModified = true;
                isMaintained = value;
            }
        }
        private bool isIsMaintainedModified = false;
        private bool? isMaintained;

        /// <value>The category this license is in.</value>
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

        /// <inheritdoc />
        [DeserializeAs("available_actions", DeserializeAs.AvailableActions)]
        public AvailableAction AvailableActions { get; private set; }

        /// <value>Gets/sets if sears on this license are reassignable.</value>
        [DeserializeAs("reassignable")]
        [SerializeAs("reassignable")]
        [Patch(nameof(isIsReassignableModified))]
        public bool IsReassignable
        {
            get => isReassignable;
            set
            {
                isIsReassignableModified = true;
                isReassignable = value;
            }
        }
        private bool isIsReassignableModified = false;
        private bool isReassignable;

        /// <value>Gets/sets the supplier who sold this license.</value>
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

        /// <value>Whether or not there are seats left to check out.</value>
        [DeserializeAs("user_can_checkout")]
        public bool? UserCanCheckOut { get; private set; }

        void IPatchable.SetAllModifiedState(bool isModified)
        {
            isNameModified = isModified;
            isCompanyModified = isModified;
            isManufacturerModified = isModified;
            isProductKeyModified = isModified;
            isOrderNumberModified = isModified;
            isPurchaseOrderModified = isModified;
            isPurchaseDateModified = isModified;
            isPurchaseCostModified = isModified;
            isNotesModified = isModified;
            isTotalSeatsModified = isModified;
            isLicensedToNameModified = isModified;
            isLicensedToEmailAddressModified = isModified;
            isIsMaintainedModified = isModified;
            isCategoryModified = isModified;
            isIsReassignableModified = isModified;
            isSupplierModified = isModified;
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            ((IPatchable)this).SetAllModifiedState(false);
        }
    }
}
