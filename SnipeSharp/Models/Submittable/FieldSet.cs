using SnipeSharp.EndPoint;
using SnipeSharp.Serialization;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SnipeSharp.Models
{
    /// <summary>
    /// A field set.
    /// Field sets determine the custom fields that are available on asset Models.
    /// </summary>
    /// <seealso cref="Model" />
    /// <seealso cref="Asset.CustomFields" />
    [PathSegment("fieldsets")]
    public sealed class FieldSet : AbstractBaseModel, IPatchable
    {
        /// <summary>Create a new FieldSet object.</summary>
        public FieldSet() { }

        /// <summary>Create a new FieldSet object with the supplied ID, for use with updating.</summary>
        internal FieldSet(int id)
        {
            Id = id;
        }

        /// <inheritdoc />
        /// <remarks>This field is required.</remarks>
        [DeserializeAs("name")]
        [SerializeAs("name", IsRequired = true)]
        [Patch(nameof(isNameModified))]
        public override string Name
        {
            get => name;
            set
            {
                isNameModified = true;
                name = value;
            }
        }
        private bool isNameModified = false;
        private string name;

        /// <value>Gets the CustomFields in this FieldSet.</value>
        /// <remarks>To set the fields in a set, see <see cref="CustomFieldEndPoint.Associate(CustomField, FieldSet, bool, int?)"/> and <see cref="CustomFieldEndPoint.Disassociate(CustomField, FieldSet)" />.</remarks>
        [DeserializeAs("fields")]
        public IReadOnlyCollection<CustomField> Fields { get; private set; }

        /// <value>Gets the Models this FieldSet applies to.</value>
        [DeserializeAs("models", DeserializeAs.ReadOnlyCollection)]
        public IReadOnlyCollection<Stub<Model>> Models { get; private set; }

        void IPatchable.SetAllModifiedState(bool isModified)
        {
            isNameModified = isModified;
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            ((IPatchable)this).SetAllModifiedState(false);
        }
    }
}
