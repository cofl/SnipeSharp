using SnipeSharp.Models.Enumerations;
using SnipeSharp.Serialization;
using System;

namespace SnipeSharp.Models
{
    /// <summary>
    /// An asset that can be requested.
    /// </summary>
    public sealed class RequestableAsset : ApiObject, IAvailableActions
    {
        /// <summary>The ID of the asset.</summary>
        [DeserializeAs("id")]
        public int Id { get; private set; }

        /// <summary>The name of the asset.</summary>
        [DeserializeAs("name")]
        public string Name { get; private set; }

        /// <summary>The asset tag.</summary>
        [DeserializeAs("asset_tag")]
        public string AssetTag { get; private set; }

        /// <summary>The serial number.</summary>
        [DeserializeAs("serial")]
        public string Serial { get; private set; }

        /// <summary>The url for the image of the asset.</summary>
        [DeserializeAs("image")]
        public Uri ImageUri { get; private set; }

        /// <summary>The name of the model of the Asset.</summary>
        [DeserializeAs("model")]
        public string ModelName { get; private set; }

        /// <summary>The model number of the model of the Asset.</summary>
        [DeserializeAs("model_number")]
        public string ModelNumber { get; private set; }

        /// <summary>The name of the location the asset is at.</summary>
        [DeserializeAs("location")]
        public string LocationName { get; private set; }

        /// <summary>The deployment status of the asset.</summary>
        [DeserializeAs("status")]
        public StatusMeta StatusMeta { get; private set; }

        /// <summary>The date to the Asset is expected to be checked back in.</summary>
        [DeserializeAs("expected_checkin", DeserializeAs.DateObject)]
        public DateTime? ExpectedCheckIn { get; private set; }

        /// <inheritdoc />
        [DeserializeAs("available_actions", DeserializeAs.AvailableActions)]
        public AvailableAction AvailableActions { get; private set; }
    }
}
