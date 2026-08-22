using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SnipeSharp.Serialization;
using System.Runtime.Serialization;

namespace SnipeSharp.Filters
{
    /// <summary>
    /// Columns a request search can be sorted on.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    [EnumNameConverter]
    public enum RequestableAssetSearchColumn
    {
        /// <summary>The creation time.</summary>
        [EnumMember(Value = "created_at")] // value is not valid, but defaults to valid in API controller
        CreationTime = 0,
        /// <summary>The asset model.</summary>
        [EnumMember(Value = "model")]
        Model,
        /// <summary>The asset model number.</summary>
        [EnumMember(Value = "model_number")]
        ModelNumber,
        /// <summary>The asset category.</summary>
        [EnumMember(Value = "category")]
        Category,
        /// <summary>The asset manufacturer.</summary>
        [EnumMember(Value = "manufacturer")]
        Manufacturer
    }
}
