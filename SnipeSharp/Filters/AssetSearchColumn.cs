using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SnipeSharp.Serialization;
using System.Runtime.Serialization;

namespace SnipeSharp.Filters
{
    /// <summary>
    /// Columns an asset search can be sorted on.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    [EnumNameConverter]
    public enum AssetSearchColumn
    {
        /// <summary>The internal Id number.</summary>
        [EnumMember(Value = "id")]
        Id,
        /// <summary>The name of the asset.</summary>
        [EnumMember(Value = "name")]
        Name,
        /// <summary>The asset's asset tag.</summary>
        [EnumMember(Value = "asset_tag")]
        AssetTag,
        /// <summary>The model number of the asset.</summary>
        [EnumMember(Value = "model_number")]
        ModelNumber,
        /// <summary>The date the asset was last checked out.</summary>
        [EnumMember(Value = "last_checkout")]
        LastCheckOut,
        /// <summary>The notes field describing the asset.</summary>
        [EnumMember(Value = "notes")]
        Notes,
        /// <summary>The expected checkin date for the asset.</summary>
        [EnumMember(Value = "expected_checkin")]
        ExpectedCheckIn,
        /// <summary>The order number associated with the asset's purchase.</summary>
        [EnumMember(Value = "order_number")]
        OrderNumber,
        /// <summary>The uri associated with the image for the asset.</summary>
        [EnumMember(Value = "image")]
        ImageUri,
        /// <summary>Who the asset is assigned to.</summary>
        [EnumMember(Value = "assigned_to")]
        AssignedTo,
        /// <summary>The creation date of the asset in Snipe-IT.</summary>
        [EnumMember(Value = "created_at")]
        CreatedAt,
        /// <summary>The date the asset was last modified in Snipe-IT.</summary>
        [EnumMember(Value = "updated_at")]
        UpdatedAt,
        /// <summary>The date the asset was purchased.</summary>
        [EnumMember(Value = "purchase_date")]
        PurchaseDate,
        /// <summary>The cost of te asset.</summary>
        [EnumMember(Value = "purchase_cost")]
        PurchaseCost,
        /// <summary>The date the asset was last audited.</summary>
        [EnumMember(Value = "last_audit_date")]
        LastAuditDate,
        /// <summary>The date the asset is next scheduled to be audited.</summary>
        [EnumMember(Value = "next_audit_date")]
        NextAuditDate,
        /// <summary>The length of the asset's warranty.</summary>
        [EnumMember(Value = "warranty_months")]
        WarrantyMonths,
        /// <summary>The number of times the asset has been checked out.</summary>
        [EnumMember(Value = "checkout_counter")]
        CheckOutCounter,
        /// <summary>The number of times the asset has been checked in.</summary>
        [EnumMember(Value = "checkin_counter")]
        CheckInCounter,
        /// <summary>The number of times the asset has been requested.</summary>
        [EnumMember(Value = "requests_counter")]
        RequestsCounter,
        /// <summary>The status of the asset in inventory (Deployed, Ready to deploy, Recycle.</summary>
        [EnumMember(Value = "status")]
        Status
    }
}
