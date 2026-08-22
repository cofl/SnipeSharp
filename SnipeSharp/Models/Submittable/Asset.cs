using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SnipeSharp.Collections;
using SnipeSharp.EndPoint;
using SnipeSharp.Models.Enumerations;
using SnipeSharp.Serialization;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SnipeSharp.Models
{
    /// <summary>
    /// An Asset.
    /// Asset may be checked out to Users, Locations, or other Assets.
    /// </summary>
    [PathSegment("hardware")]
    public sealed class Asset : AbstractBaseModel, IAvailableActions, IPatchable
    {
        /// <summary>Create a new Asset object.</summary>
        public Asset() { }

        /// <summary>Create a new Asset object with the supplied ID, for use with updating.</summary>
        internal Asset(int id)
        {
            Id = id;
        }

        /// <summary>If this asset has been deleted.</summary>
        public bool IsDeleted => DeletedAt.HasValue;

        /// <inheritdoc />
        [DeserializeAs("name")]
        [SerializeAs("name")]
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
        /// The asset tag of the Asset.
        /// </summary>
        /// <remarks>
        /// <para>This field is required, and must be unique amongst non-deleted assets.</para>
        /// </remarks>
        [DeserializeAs("asset_tag")]
        [SerializeAs("asset_tag", IsRequired = true)]
        [Patch(nameof(isAssetTagModified))]
        public string AssetTag
        {
            get => assetTag;
            set
            {
                isAssetTagModified = true;
                assetTag = value;
            }
        }
        private bool isAssetTagModified = false;
        private string assetTag;

        /// <summary>
        /// The serial (number) of the Asset.
        /// </summary>
        /// <remarks>This value must be unique amongst all assets.</remarks>
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

        /// <summary>
        /// The model of the Asset.
        /// </summary>
        /// <remarks>
        /// <para>This field is required.</para>
        /// </remarks>
        [DeserializeAs("model")]
        [SerializeAs("model_id", SerializeAs.IdValue, IsRequired = true)]
        [Patch(nameof(isModelModified))]
        public Stub<Model> Model
        {
            get => model;
            set
            {
                isModelModified = true;
                model = value;
            }
        }
        private bool isModelModified = false;
        private Stub<Model> model;

        /// <summary>
        /// The model number of the model of the Asset.
        /// </summary>
        /// <remarks>This field cannot be used to set the model.</remarks>
        [DeserializeAs("model_number")]
        public string ModelNumber { get; private set; }

        /// <summary>
        /// The End-of-life date for the Asset, based on its purchase date and the end-of-life time of its model.
        /// </summary>
        [DeserializeAs("eol", DeserializeAs.DateObject)]
        public DateTime? EndOfLife { get; private set; }

        /// <summary>
        /// The status of the asset, derived from its associated StatusLabel and other metadata.
        /// </summary>
        /// <seealso cref="StatusLabelEndPoint.FromAssetStatus(AssetStatus)" />
        /// <seealso cref="StatusLabel.ToAssetStatus" />
        [DeserializeAs("status_label")]
        [SerializeAs("status_id", SerializeAs.IdValue, IsRequired = true)]
        [Patch(nameof(isStatusModified))]
        public AssetStatus Status
        {
            get => status;
            set
            {
                isStatusModified = true;
                status = value;
            }
        }
        private bool isStatusModified = false;
        private AssetStatus status;

        /// <summary>
        /// The category of the model of the Asset.
        /// </summary>
        /// <remarks>
        /// <para>To update this field, see <see cref="Model.Category"/></para>
        /// </remarks>
        [DeserializeAs("category")]
        [SerializeAs("category_id", SerializeAs.IdValue)]
        public Stub<Category> Category { get; private set; }

        /// <summary>
        /// The manufacturer of the model of the Asset.
        /// </summary>
        /// <remarks>
        /// <para>This field will be converted to the value of its Id when serialized.</para>
        /// <para>When deserialized, this value does not have all properties filled. Fetch the value using the relevant endpoint to gather the rest of the information.</para>
        /// <para>To update this field, see <see cref="Model.Manufacturer"/></para>
        /// </remarks>
        [DeserializeAs("manufacturer")]
        [SerializeAs("manufacturer_id", SerializeAs.IdValue)]
        public Stub<Manufacturer> Manufacturer { get; private set; }

        /// <summary>
        /// The supplier of the Asset.
        /// </summary>
        /// <remarks>
        /// <para>This field will be converted to the value of its Id when serialized.</para>
        /// <para>When deserialized, this value does not have all properties filled. Fetch the value using the relevant endpoint to gather the rest of the information.</para>
        /// </remarks>
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
        /// Notes for the Asset.
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
        /// The order number associated with this Asset's purchase.
        /// </summary>
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
        /// The company that owns this Asset.
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
        /// <para>The location this asset is currently at.</para>
        /// <para>It is preferable to check out the asset than to set its location directly.</para>
        /// <seealso cref="AssetEndPoint.CheckOut(AssetCheckOutRequest)" />
        /// <para>When an asset is checked in, its location will be set to its <see cref="DefaultLocation">DefaultLocation</see>.</para>
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
        /// The default location for the asset.
        /// </summary>
        [DeserializeAs("rtd_location")]
        [SerializeAs("rtd_location_id", SerializeAs.IdValue)]
        [Patch(nameof(isDefaultLocationModified))]
        public Stub<Location> DefaultLocation
        {
            get => defaultLocation;
            set
            {
                isDefaultLocationModified = true;
                defaultLocation = value;
            }
        }
        private bool isDefaultLocationModified = false;
        private Stub<Location> defaultLocation;

        /// <summary>
        /// The url for the image of the asset.
        /// </summary>
        /// <remarks>If the asset does not have an image set explicitly, it uses its model's image by default.</remarks>
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

        /// <summary>
        /// <para>The object this asset is assigned to; it may be either a <see cref="User">User</see>, a <see cref="Location">Location</see>, or another <see cref="Asset">Asset</see>.</para>
        /// </summary>
        [DeserializeAs("assigned_to")]
        // This property does not have the Patch attribute because we use the OnSerializing
        // function to set a custom field with its value, depending on its type.
        public AssetAssignedTo AssignedTo
        {
            get => assignedTo;
            private set
            {
                isAssignedToModified = true;
                assignedTo = value;
            }
        }
        /// <value>If the assigned object has been modified.</value>
        private bool isAssignedToModified = false;
        private AssetAssignedTo assignedTo;

        /// <summary>
        /// Assign this asset to a user.
        /// </summary>
        /// <param ref="user">The user to assign to.</param>
        /// <returns>The stub object representing the user to the API.</returns>
        public AssetAssignedTo AssignTo(User user)
            => AssignedTo = new AssetAssignedTo(user);

        /// <summary>
        /// Assign this asset to a location.
        /// </summary>
        /// <param ref="location">The location to assign to.</param>
        /// <returns>The stub object representing the location to the API.</returns>
        public AssetAssignedTo AssignTo(Location location)
            => AssignedTo = new AssetAssignedTo(location);

        /// <summary>
        /// Assign this asset to a asset.
        /// </summary>
        /// <param ref="asset">The asset to assign to.</param>
        /// <returns>The stub object representing the asset to the API.</returns>
        public AssetAssignedTo AssignTo(Asset asset)
            => AssignedTo = new AssetAssignedTo(asset);

        /// <summary>
        /// The number of months the warranty covers for this asset.
        /// </summary>
        [DeserializeAs("warranty_months", DeserializeAs.MonthStringAsInt)]
        [SerializeAs("warranty_months")]
        [Patch(nameof(isWarrantyMonthsModified))]
        public int? WarrantyMonths
        {
            get => warrantyMonths;
            set
            {
                isWarrantyMonthsModified = true;
                warrantyMonths = value;
            }
        }
        private bool isWarrantyMonthsModified = false;
        private int? warrantyMonths;

        /// <summary>
        /// The date the warranty for this asset expires. This value is calculated from <see cref="PurchaseDate">PurchaseDate</see> and <see cref="WarrantyMonths">WarrantyMonths</see>.
        /// </summary>
        [DeserializeAs("warranty_expires", DeserializeAs.DateObject)]
        public DateTime? WarrantyExpires { get; private set; }

        /// <summary>
        /// The date this asset was last audited.
        /// </summary>
        /// <seealso cref="AssetEndPoint.Audit(Asset, Location, DateTime?, string)" />
        [DeserializeAs("last_audit_date", DeserializeAs.Timestamp)]
        public DateTime? LastAuditDate { get; private set; }

        /// <summary>
        /// The date this asset will next be audited.
        /// </summary>
        /// <seealso cref="AssetEndPoint.Audit(Asset, Location, DateTime?, string)" />
        [DeserializeAs("next_audit_date", DeserializeAs.DateObject)]
        public DateTime? NextAuditDate { get; private set; }

        /// <summary>
        /// <para>The date this asset was marked deleted.</para>
        /// <para>As assets are never truly deleted, yes, this is a field that can have a value.</para>
        /// </summary>
        [DeserializeAs("deleted_at", DeserializeAs.Timestamp)]
        public DateTime? DeletedAt { get; private set; }

        /// <summary>
        /// The date this Asset was purchased.
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
        /// The date this Asset was last checked out.
        /// </summary>
        /// <seealso cref="AssetEndPoint.CheckOut(AssetCheckOutRequest)" />
        [DeserializeAs("last_checkout", DeserializeAs.Timestamp)]
        [Patch(nameof(isLastCheckOutModified))]
        public DateTime? LastCheckOut
        {
            get => lastCheckOut;
            set
            {
                isLastCheckOutModified = true;
                lastCheckOut = value;
            }
        }
        private bool isLastCheckOutModified = false;
        private DateTime? lastCheckOut;

        /// <summary>
        /// The date this Asset is expected to be checked back in.
        /// </summary>
        /// <seealso cref="AssetEndPoint.CheckOut(AssetCheckOutRequest)" />
        [DeserializeAs("expected_checkin", DeserializeAs.DateObject)]
        public DateTime? ExpectedCheckIn { get; private set; }

        /// <summary>
        /// The cost of this Asset when purchased.
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
        /// The number of times this asset has been checked in.
        /// </summary>
        [DeserializeAs("checkin_counter")]
        public int CheckInCounter { get; private set; }

        /// <summary>
        /// The number of times this asset has been checked out.
        /// </summary>
        [DeserializeAs("checkout_counter")]
        public int CheckOutCounter { get; private set; }

        /// <summary>
        /// The number of times this asset has been requested.
        /// </summary>
        [DeserializeAs("requests_counter")]
        public int RequestsCounter { get; private set; }

        /// <summary>
        /// Whether or not a user can check this asset out.
        /// </summary>
        [DeserializeAs("user_can_checkout")]
        public bool? UserCanCheckOut { get; private set; }

        /// <inheritdoc />
        [DeserializeAs("available_actions", DeserializeAs.AvailableActions)]
        public AvailableAction AvailableActions { get; private set; }

        /// <summary>
        /// <para>Custom fields for this Asset, selected by the Model's FieldSet.</para>
        /// <para>Values in this collection will be serialized with the key <c><see cref="AssetCustomField.Field">value.Field</see> ?? key</c> and the value <see cref="AssetCustomField.Value">value.Value</see>.</para>
        /// </summary>
        [DeserializeAs("custom_fields", DeserializeAs.CustomFieldDictionary)]
        [Patch(nameof(isCustomFieldsModified))]
        public CustomFieldDictionary CustomFields { get; set; } = new CustomFieldDictionary();

        private bool isCustomFieldsModified
        {
            get => CustomFields?.IsModified ?? false;
            set
            {
                if (null != CustomFields)
                    CustomFields.IsModified = value;
            }
        }

        [JsonExtensionData]
        private Dictionary<string, JToken> _customFields { get; set; }

        [OnSerializing]
        private void OnSerializing(StreamingContext context)
        {
            _customFields = new Dictionary<string, JToken>();
            if (null != CustomFields)
                foreach (var pair in CustomFields)
                    // only serialize modified custom fields
                    // Value cannot be null, because we check that in CustomFieldDictionary
                    if (pair.Value!.IsModified)
                        _customFields[pair.Value!.Field ?? pair.Key] = pair.Value!.Value;
            if (null != AssignedTo && isAssignedToModified) // TODO: is it possible to un-assign with this method?
            {
                switch (AssignedTo.Type)
                {
                    case AssignedToType.Asset:
                        _customFields["assigned_asset"] = AssignedTo.Id;
                        break;
                    case AssignedToType.Location:
                        _customFields["assigned_location"] = AssignedTo.Id;
                        break;
                    case AssignedToType.User:
                        _customFields["assigned_user"] = AssignedTo.Id;
                        break;
                }
            }
        }

        [OnSerialized]
        private void OnSerialized(StreamingContext context)
        {
            // remove unnecessary object now.
            _customFields = null;
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            if (null != _customFields)
            {
                foreach (var pair in _customFields)
                {
                    if (!pair.Key.StartsWith("_snipeit_")) // custom fields start with _snipeit_
                        continue;
                    CustomFields.Add(new AssetCustomField
                    {
                        FriendlyName = pair.Key,
                        Field = pair.Key,
                        Value = pair.Value.ToObject<string>()
                    });
                }
                _customFields = null;
            }
            ((IPatchable)this).SetAllModifiedState(false);
        }

        void IPatchable.SetAllModifiedState(bool isModified)
        {
            isNameModified = isModified;
            isAssetTagModified = isModified;
            isSerialModified = isModified;
            isModelModified = isModified;
            isStatusModified = isModified;
            isSupplierModified = isModified;
            isNotesModified = isModified;
            isOrderNumberModified = isModified;
            isCompanyModified = isModified;
            isLocationModified = isModified;
            isDefaultLocationModified = isModified;
            isImageUriModified = isModified;
            isAssignedToModified = isModified;
            isWarrantyMonthsModified = isModified;
            isPurchaseDateModified = isModified;
            isLastCheckOutModified = isModified;
            isPurchaseCostModified = isModified;
            isCustomFieldsModified = isModified;
        }
    }
}
