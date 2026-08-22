using SnipeSharp.Serialization;
using System;

namespace SnipeSharp.Models
{
    /// <summary>
    /// AbstractBaseModel is an ApiObject associated with an Api EndPoint.
    /// </summary>
    public abstract class AbstractBaseModel : ApiObject, IObjectWithId
    {
        /// <value>The internal Id of the object.</value>
        [DeserializeAs("id")]
        public int Id { get; set; }

        /// <value>The name of the object.</value>
        [DeserializeAs("name")]
        public virtual string Name { get; set; }

        /// <value>The creation date of this object in Snipe-IT.</value>
        [DeserializeAs("created_at", DeserializeAs.Timestamp)]
        public DateTime? CreatedAt { get; protected set; }

        /// <value>The most recent date this object was modified in Snipe-IT.</value>
        [DeserializeAs("updated_at", DeserializeAs.Timestamp)]
        public DateTime? UpdatedAt { get; protected set; }

        /// <inheritdoc />
        public override string ToString()
            => Name ?? Id.ToString();
    }
}
