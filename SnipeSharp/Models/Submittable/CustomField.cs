using SnipeSharp.EndPoint;
using SnipeSharp.Models.Enumerations;
using SnipeSharp.Serialization;
using System;
using System.Runtime.Serialization;

namespace SnipeSharp.Models
{
    /// <summary>
    /// A custom field.
    /// Custom fields are in a many-to-many relationship with <see cref="FieldSet">Fieldsets</see>.
    /// </summary>
    [PathSegment("fields")]
    public sealed class CustomField : AbstractBaseModel, IPatchable
    {
        /// <summary>Create a new CustomField object.</summary>
        public CustomField() { }

        /// <summary>Create a new CustomField object with the supplied ID, for use with updating.</summary>
        internal CustomField(int id)
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

        /// <value>Gets the internal column name.</value>
        [DeserializeAs("db_column_name")]
        public string ColumnName { get; private set; }

        /// <summary>
        /// <para>Gets/sets the format of the field, for example:</para>
        /// <list type="table">
        ///   <listheader>
        ///     <term>Format</term>
        ///     <description>Description</description>
        ///   </listheader>
        ///   <item>
        ///     <term>alpha</term>
        ///     <description>Only alphabetical characters.</description>
        ///   </item>
        ///   <item>
        ///     <term>alpha_dash</term>
        ///     <description>Only alphabetical characters and '-'.</description>
        ///   </item>
        ///   <item>
        ///     <term>numeric</term>
        ///     <description>Only numbers.</description>
        ///   </item>
        ///   <item>
        ///     <term>alpha_num</term>
        ///     <description>Only alphanumeric characters.</description>
        ///   </item>
        ///   <item>
        ///     <term>email</term>
        ///     <description>A string in an email format.</description>
        ///   </item>
        ///   <item>
        ///     <term>date</term>
        ///     <description>A date.</description>
        ///   </item>
        ///   <item>
        ///     <term>url</term>
        ///     <description>A url.</description>
        ///   </item>
        ///   <item>
        ///     <term>ip</term>
        ///     <description>Any IP address.</description>
        ///   </item>
        ///   <item>
        ///     <term>ipv4</term>
        ///     <description>Explicitly an IPv4 address.</description>
        ///   </item>
        ///   <item>
        ///     <term>ipv6</term>
        ///     <description>Explicitly an IPv6 address.</description>
        ///   </item>
        ///   <item>
        ///     <term>regex:/^[a-fA-F0-9]{2}:[a-fA-F0-9]{2}:[a-fA-F0-9]{2}:[a-fA-F0-9]{2}:[a-fA-F0-9]{2}:[a-fA-F0-9]{2}$/</term>
        ///     <description>A MAC Address. Other regex format follow the same "regex:/^.$/" format.</description>
        ///   </item>
        ///   <item>
        ///     <term>boolean</term>
        ///     <description>A boolean value.</description>
        ///   </item>
        /// </list>
        /// </summary>
        /// <value>Gets/sets the format of the field.</value>
        [DeserializeAs("format")]
        [SerializeAs("format")]
        [Patch(nameof(isFormatModified))]
        public string Format
        {
            get => format;
            set
            {
                isFormatModified = true;
                format = value;
            }
        }
        private bool isFormatModified = false;
        private string format;

        private readonly static string[] EndOfLines = new string[] { "\n", "\r\n" };

        /// <value>Gets/sets the raw value of any list type, separated by newlines.</value>
        [DeserializeAs("field_values")]
        [SerializeAs("field_values")]
        [Patch(nameof(isFieldValuesRawModified))]
        public string FieldValuesRaw
        {
            get => fieldValuesRaw;
            set
            {
                isFieldValuesRawModified = true;
                fieldValuesRaw = value;
                if (null == value)
                    FieldValues = new string[0];
                else
                    FieldValues = value.Split(EndOfLines, StringSplitOptions.None);
            }
        }
        private bool isFieldValuesRawModified = false;
        private string fieldValuesRaw;

        /// <value>Gets/sets if the field is encrypted or not.</value>
        [DeserializeAs("field_encrypted")]
        [SerializeAs("field_encrypted")]
        [Patch(nameof(isIsFieldEncryptedModified))]
        public bool? IsFieldEncrypted
        {
            get => isFieldEncrypted;
            set
            {
                isIsFieldEncryptedModified = true;
                isFieldEncrypted = value;
            }
        }
        private bool isIsFieldEncryptedModified = false;
        private bool? isFieldEncrypted;

        /// <value>Gets/sets if this field will be listed in emails sent to users.</value>
        [DeserializeAs("show_in_email")]
        [SerializeAs("show_in_email")]
        [Patch(nameof(isShowInCheckOutEmailModified))]
        public bool? ShowInCheckOutEmail
        {
            get => showInCheckOutEmail;
            set
            {
                isShowInCheckOutEmailModified = true;
                showInCheckOutEmail = value;
            }
        }
        private bool isShowInCheckOutEmailModified = false;
        private bool? showInCheckOutEmail;

        /// <value>Gets/sets the help text for the field.</value>
        [DeserializeAs("help_text")]
        [SerializeAs("help_text")]
        [Patch(nameof(isHelpTextModified))]
        public string HelpText
        {
            get => helpText;
            set
            {
                isHelpTextModified = true;
                helpText = value;
            }
        }
        private bool isHelpTextModified = false;
        private string helpText;

        /// <value>Gets the values of a list type as an array.</value>
        [DeserializeAs("field_values_array")]
        public string[] FieldValues { get; private set; }

        /// <value>Get/sets what the type of the element is.</value>
        [DeserializeAs("type")]
        [SerializeAs("element")]
        [Patch(nameof(isTypeModified))]
        public CustomFieldElement Type
        {
            get => type;
            set
            {
                isTypeModified = true;
                type = value;
            }
        }
        private bool isTypeModified = false;
        private CustomFieldElement type;

        /// <value>Gets if this is a required field.</value>
        [DeserializeAs("required")]
        [SerializeAs("required")]
        [Patch(nameof(isIsRequiredModified))]
        public bool? IsRequired
        {
            get => isRequired;
            set
            {
                isIsRequiredModified = true;
                isRequired = value;
            }
        }
        private bool isIsRequiredModified = false;
        private bool? isRequired;

        void IPatchable.SetAllModifiedState(bool isModified)
        {
            isNameModified = isModified;
            isFormatModified = isModified;
            isFieldValuesRawModified = isModified;
            isIsFieldEncryptedModified = isModified;
            isShowInCheckOutEmailModified = isModified;
            isHelpTextModified = isModified;
            isTypeModified = isModified;
            isIsRequiredModified = isModified;
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            ((IPatchable)this).SetAllModifiedState(false);
        }
    }
}
