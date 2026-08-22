using SnipeSharp.Serialization;
using System;

namespace SnipeSharp.Filters
{
    /// <summary>
    /// A filter for finding requests.
    /// </summary>
    /// <seealso cref="EndPoint.AccountEndPoint.GetRequestableAssets(RequestableAssetSearchFilter)"/>
    public sealed class RequestableAssetSearchFilter : ISortableSearchFilter<RequestableAssetSearchColumn?>
    {
        /// <inheritdoc />
        [SerializeAs("limit")]
        public int? Limit { get; set; }

        /// <inheritdoc />
        [SerializeAs("offset")]
        public int? Offset { get; set; }

        /// <summary>Backing field for <see cref="Search"/>.</summary>
        private string _SearchString;
        /// <inheritdoc />
        /// <exception cref="System.ArgumentNullException">If attempting to set a null value.</exception>
        [SerializeAs("search")]
        public string Search
        {
            get => _SearchString;
            set
            {
                if (null == value)
                    throw new ArgumentNullException(paramName: nameof(value));
                _SearchString = value;
            }
        }

        /// <inheritdoc />
        [SerializeAs("sort")]
        public RequestableAssetSearchColumn? SortColumn { get; set; }

        /// <inheritdoc />
        [SerializeAs("order")]
        public SearchOrder? Order { get; set; }

        /// <summary>
        /// Instantiates a new RequestableAssetSearchFilter.
        /// </summary>
        public RequestableAssetSearchFilter()
        {
        }

        /// <summary>
        /// Instantiates a new RequestableAssetSearchFilter with the supplied search string.
        /// </summary>
        /// <param name="searchString">A string to search for.</param>
        /// <exception cref="System.ArgumentNullException">If <paramref name="searchString"/> is null.</exception>
        public RequestableAssetSearchFilter(string searchString)
        {
            if (null == searchString)
                throw new ArgumentNullException(paramName: nameof(searchString));
            Search = searchString;
        }
    }
}
