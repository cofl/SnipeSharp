using SnipeSharp.Models;
using SnipeSharp.Serialization;

namespace SnipeSharp.Filters
{
    /// <summary>
    /// A filter for accessories, featuring accessory-only search fields.
    /// </summary>
    public sealed class AccessorySearchFilter : ISortableSearchFilter<AccessorySearchColumn?>
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
        public AccessorySearchColumn? SortColumn { get; set; }

        /// <inheritdoc />
        [SerializeAs("order")]
        public SearchOrder? Order { get; set; }

        /// <summary>
        /// Only search for accessories owned by this Company if set.
        /// </summary>
        [SerializeAs("company_id", SerializeAs.IdValue)]
        public Company Company { get; set; }

        /// <summary>
        /// Only search for accessories in this category if set.
        /// </summary>
        [SerializeAs("category_id", SerializeAs.IdValue)]
        public Category Category { get; set; }

        /// <summary>
        /// Only search for accessories by this manufacturer if set.
        /// </summary>
        [SerializeAs("manufacturer_id", SerializeAs.IdValue)]
        public Manufacturer Manufacturer { get; set; }

        /// <summary>
        /// Only search for accessories by this supplier if set.
        /// </summary>
        [SerializeAs("supplier_id", SerializeAs.IdValue)]
        public Supplier Supplier { get; set; }

        /// <summary>
        /// Create a new search filter with no search parameters.
        /// </summary>
        public AccessorySearchFilter()
        {
        }

        /// <summary>
        /// Create a new search filter with a search parameter.
        /// </summary>
        /// <param name="searchString">The value to search for.</param>
        public AccessorySearchFilter(string searchString)
        {
            Search = searchString;
        }
    }
}
