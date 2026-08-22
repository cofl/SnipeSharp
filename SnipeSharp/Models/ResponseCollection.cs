using Newtonsoft.Json;
using SnipeSharp.Serialization;
using System.Collections;
using System.Collections.Generic;

namespace SnipeSharp.Models
{
    /// <summary>
    /// A collection of items from the API, with a total count indicating the total number of rows for the query, and a rows list containing objects up to the limit of the query, which may not be all items.
    /// </summary>
    /// <typeparam name="T">The type of elements in this collection.</typeparam>
    [JsonObject]
    public sealed class ResponseCollection<T> : ApiObject, IList<T>, IReadOnlyList<T>, ICollection<T>, IReadOnlyCollection<T>
        where T : ApiObject
    {
        /// <value>The total number of objects available from the endpoint for whatever filters were applied.</value>
        /// <remarks>This value may not be the same as <see cref="Count"/>.</remarks>
        [DeserializeAs("total")]
        public long Total { get; set; }

        /// <value>The elements in this collection, as returned by the API.</value>
        [DeserializeAs("rows")]
        public List<T> Rows { get; set; } = new List<T>();

        /// <inheritdoc />
        public T this[int index]
        {
            get => Rows[index];
            set => Rows[index] = value;
        }

        /// <inheritdoc />
        public int Count => Rows?.Count ?? 0;

        /// <inheritdoc />
        public bool IsReadOnly => ((IList<T>)Rows).IsReadOnly;

        /// <inheritdoc />
        public void Add(T item)
            => Rows.Add(item);

        /// <inheritdoc />
        public void AddRange(IEnumerable<T> collection)
            => Rows.AddRange(collection);

        /// <inheritdoc />
        public void Clear()
            => Rows.Clear();

        /// <inheritdoc />
        public bool Contains(T item)
            => Rows.Contains(item);

        /// <inheritdoc />
        public void CopyTo(T[] array, int arrayIndex)
            => Rows.CopyTo(array, arrayIndex);

        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator()
            => Rows.GetEnumerator();

        /// <inheritdoc />
        public int IndexOf(T item)
            => Rows.IndexOf(item);

        /// <inheritdoc />
        public void Insert(int index, T item)
            => Rows.Insert(index, item);

        /// <inheritdoc />
        public bool Remove(T item)
            => Rows.Remove(item);

        /// <inheritdoc />
        public void RemoveAt(int index)
            => Rows.RemoveAt(index);

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
            => Rows.GetEnumerator();
    }
}
