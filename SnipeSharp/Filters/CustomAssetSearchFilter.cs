using SnipeSharp.Serialization;
using System.Runtime.Serialization;

namespace SnipeSharp.Filters
{
    /// <summary>
    /// A filter for assets, with no limit on sort column.
    /// </summary>
    public sealed class CustomAssetSearchFilter : AbstractAssetSearchFilter, ISortableSearchFilter<string>
    {
        /// <inheritdoc />
        [SerializeAs("limit")]
        public int? Limit { get; set; }

        /// <inheritdoc />
        [SerializeAs("offset")]
        public int? Offset { get; set; }

        /// <inheritdoc />
        [SerializeAs("search")]
        public string Search { get; set; }

        /// <inheritdoc />
        [SerializeAs("sort")]
        public string SortColumn { get; set; }

        /// <inheritdoc />
        [SerializeAs("order")]
        public SearchOrder? Order { get; set; }

        /// <summary>
        /// Initialize a new instance of the CustomAssetSearchFilter class.
        /// </summary>
        public CustomAssetSearchFilter()
        {
        }

        /// <summary>
        /// Initialize a new instance of the CustomAssetSearchFilter class with the specified search string.
        /// </summary>
        public CustomAssetSearchFilter(string searchString)
        {
            Search = searchString;
        }

        [OnSerializing]
        private void OnSerializing(StreamingContext context)
        {
            if (null != CustomFields && CustomFields.Count > 0)
                _customFields = CustomFields;
        }
    }
}
