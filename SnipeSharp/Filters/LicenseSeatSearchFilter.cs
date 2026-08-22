using SnipeSharp.Serialization;
using System;

namespace SnipeSharp.Filters
{
    /// <summary>
    /// A filter for license seats, featuring license-seat-only search fields.
    /// </summary>
    public sealed class LicenseSeatSearchFilter : ISortableSearchFilter<LicenseSeatSearchColumn?>
    {
        /// <summary>
        /// The number of items to return in a single request. By default 50.
        /// </summary>
        [SerializeAs("limit")]
        public int? Limit { get; set; }

        /// <summary>
        /// The number of items to skip before taking the limit; for use in pagination and getting more than 1000 objects at once.
        /// </summary>
        [SerializeAs("offset")]
        public int? Offset { get; set; }

        /// <summary>
        /// The column to sort on.
        /// </summary>
        [SerializeAs("sort")]
        public LicenseSeatSearchColumn? SortColumn { get; set; }

        /// <summary>
        /// Whether to sort the request in ascending or descending order based on the sort column.
        /// </summary>
        [SerializeAs("order")]
        public SearchOrder? Order { get; set; }

        /// <summary>
        /// This field is ignored by the LicenseSeat search, but is present to fulfill the interface requirements.
        /// </summary>
        public string Search
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }
    }
}
