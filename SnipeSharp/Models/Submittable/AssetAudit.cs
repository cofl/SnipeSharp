using SnipeSharp.EndPoint;
using SnipeSharp.Serialization;
using System;

namespace SnipeSharp.Models
{
    /// <summary>
    /// ApiObject for submitting Asset audits. Asset audits are only retrieved
    /// when submitting the audit.
    /// </summary>
    /// <seealso cref="AssetEndPoint.Audit(Asset, Location, DateTime?, string)" />
    public sealed class AssetAudit : ApiObject
    {
        /// <summary>
        /// The asset tag of the Asset being audited.
        /// </summary>
        /// <remarks>This field is required.</remarks>
        [DeserializeAs("asset_tag")]
        [SerializeAs("asset_tag", SerializeAs.IdValue, IsRequired = true)]
        public Asset Asset { get; set; }

        /// <summary>
        /// The audit location.
        /// </summary>
        /// <remarks>This field will not be filled by the API.</remarks>
        [SerializeAs("location_id", SerializeAs.IdValue)]
        public Location Location { get; set; }

        /// <summary>
        /// The next scheduled audit date.
        /// </summary>
        [DeserializeAs("next_audit_date", DeserializeAs.Timestamp)]
        [SerializeAs("next_audit_date", SerializeAs.SimpleDate)]
        public DateTime? NextAuditDate { get; set; }

        /// <summary>
        /// Notes for the audit log.
        /// </summary>
        [DeserializeAs("note")]
        [SerializeAs("note")]
        public string Note { get; set; }
    }
}
