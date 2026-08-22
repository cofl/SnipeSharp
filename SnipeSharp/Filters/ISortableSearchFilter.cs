namespace SnipeSharp.Filters
{
    /// <summary>
    /// The typed search filter interface, for filtering collections in the API and sorting the based on a column.
    /// </summary>
    /// <typeparam name="T">The sort column type; for example, an enumeration, or a string.</typeparam>
    public interface ISortableSearchFilter<T> : ISearchFilter
    {
        /// <summary>
        /// The column to sort on.
        /// </summary>
        /// <remarks>This should be converted to a string during serialization. If implementing your own, be sure to set the JsonConverter to StringEnumConverter and add EnumMember values to all members.</remarks>
        T SortColumn { get; set; }

        /// <summary>
        /// Whether to sort the request in ascending or descending order based on the sort column (by default the creaetion date).
        /// </summary>
        SearchOrder? Order { get; set; }
    }
}
