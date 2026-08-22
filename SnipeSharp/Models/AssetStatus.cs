using SnipeSharp.Models.Enumerations;
using SnipeSharp.Serialization;

namespace SnipeSharp.Models
{
    /// <summary>
    /// The status of an <see cref="Asset">Asset</see>.
    /// </summary>
    /// <remarks>Yes, it's not a <see cref="StatusLabel">StatusLabel</see>, but the Id's are the same.</remarks>
    /// <seealso cref="SnipeSharp.EndPoint.StatusLabelEndPoint.FromAssetStatus(AssetStatus)" />
    /// <seealso cref="SnipeSharp.Models.StatusLabel.ToAssetStatus" />
    public sealed class AssetStatus : IObjectWithId
    {
        /// <summary>
        /// The Id of the <see cref="StatusLabel">StatusLabel</see> in the SnipeIT database.
        /// </summary>
        [DeserializeAs("id")]
        public int StatusId { get; internal set; }

        /// <summary>
        /// The friendly name of the status.
        /// </summary>
        /// <value></value>
        [DeserializeAs("name")]
        public string Name { get; internal set; }

        /// <summary>
        /// The category of statuses that this StatusLabel fits in.
        /// </summary>
        [DeserializeAs("status_type")]
        public StatusType? StatusType { get; internal set; }

        /// <summary>
        /// The actual category of statuses an asset with this status is in.
        /// </summary>
        [DeserializeAs("status_meta")]
        public StatusMeta? StatusMeta { get; internal set; }

        int IObjectWithId.Id => StatusId;

        /// <inheritdoc />
        public override string ToString()
        {
            if (null == StatusMeta)
                return Name ?? StatusId.ToString();
            return $"{Name ?? StatusId.ToString()}: {StatusMeta}";
        }
    }
}
