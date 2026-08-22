using SnipeSharp.Serialization;

namespace SnipeSharp.Models
{
    /// <summary>
    /// <para>A custom field on an <see cref="Asset">Asset</see> object.</para>
    /// </summary>
    /// <remarks>
    /// <para>These are store in <see cref="Asset.CustomFields">Asset.CustomFields</see>.</para>
    /// </remarks>
    public sealed class AssetCustomField : ApiObject
    {
        /// <summary>
        /// The Friendly Name of the field.
        /// </summary>
        /// <seealso cref="Asset.OnDeserialized(System.Runtime.Serialization.StreamingContext)" />
        public string FriendlyName { get; set; }

        /// <summary>
        /// The Database column name in SnipeIT.
        /// </summary>
        [DeserializeAs("field")]
        public string Field { get; set; }

        /// <summary>
        /// The value of the field.
        /// </summary>
        [DeserializeAs("value")]
        public string Value
        {
            get => value;
            set
            {
                this.value = value;
                IsModified = true;
            }
        }
        private string value;

        /// <summary>
        /// The format the field must fit, specified by SnipeIT.
        /// </summary>
        // TODO: Parse this into a special object that will verify the value matches before updating?
        [DeserializeAs("field_format")]
        public string Format { get; set; }

        /// <summary>
        /// Has this field been modified?
        /// </summary>
        internal bool IsModified { get; set; } = false;

        /// <inheritdoc />
        public override string ToString()
        {
            if (null != FriendlyName)
                return $"{FriendlyName}: {Value ?? "<empty>"}";
            else
                return Value ?? base.ToString();
        }
    }
}
