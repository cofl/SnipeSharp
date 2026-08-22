using SnipeSharp.Serialization;
using IsRequiredType = SnipeSharp.Models.Enumerations.IsRequired;

namespace SnipeSharp.Models
{
    /// <summary>
    /// ApiObject for (dis)associating custom fields to/from fieldsets.
    /// </summary>
    /// <remarks>This class is for internal use only.</remarks>
    internal sealed class CustomFieldAssociation : ApiObject
    {
        /// <summary>
        /// FieldSet to (dis)associate to/from.
        /// </summary>
        [SerializeAs("fieldset_id", SerializeAs.IdValue, IsRequired = true)]
        public FieldSet FieldSet { get; set; }

        /// <summary>
        /// Backing field for <see cref="IsRequired"/>
        /// </summary>
        private IsRequiredType _isRequired;

        /// <summary>
        /// Whether or not the field is required in the fieldset.
        /// </summary>
        [DeserializeAs("required")]
        [SerializeAs("required")]
        public bool IsRequired
        {
            get => _isRequired == IsRequiredType.On;
            set => _isRequired = value ? IsRequiredType.On : IsRequiredType.Off;
        }

        /// <summary>
        /// The order of the field in the fieldset.
        /// </summary>
        [SerializeAs("order")]
        public int Order { get; set; }
    }
}
