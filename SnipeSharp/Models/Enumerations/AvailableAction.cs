using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Runtime.Serialization;

namespace SnipeSharp.Models.Enumerations
{
    /// <summary>
    /// Members of this enumeration are operations that may be performed through the API.
    /// </summary>
    /// <seealso cref="IAvailableActions"/>
    [JsonConverter(typeof(StringEnumConverter))]
    [Flags]
    public enum AvailableAction
    {
        /// <summary>There are no available actions.</summary>
        None = 0,
        /// <summary>The associated object may be checked out.</summary>
        [EnumMember(Value = "checkout")]
        CheckOut = 1,
        /// <summary>The associated object may be checked in.</summary>
        [EnumMember(Value = "checkin")]
        CheckIn = 2,
        /// <summary>The associated object may be cloned.</summary>
        [EnumMember(Value = "clone")]
        Clone = 4,
        /// <summary>The associated object may be deleted from the database.</summary>
        /// <remarks>Deleting may not be a full deletion, but merely an assigning of the "Deleted" status.</remarks>
        [EnumMember(Value = "delete")]
        Delete = 8,
        /// <summary>The associated object may be restored.</summary>
        [EnumMember(Value = "restore")]
        Restore = 16,
        /// <summary>The associated object may have its properties set and updated.</summary>
        [EnumMember(Value = "update")]
        Update = 32,
        /// <summary>The associated object may be requested.</summary>
        [EnumMember(Value = "request")]
        Request = 64,
        /// <summary>The associated request may be canceled.</summary>
        [EnumMember(Value = "cancel")]
        CancelRequest = 128,
        /// <summary>The associated object may be audited.</summary>
        [EnumMember(Value = "audit")]
        Audit = 256,
        /// <summary>The associated object may be created.</summary>
        [EnumMember(Value = "create_asset")]
        CreateAsset = 512,
    }
}
