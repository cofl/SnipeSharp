using System;
using System.Collections.Generic;
using System.Management.Automation;
using SnipeSharp;
using SnipeSharp.Models;
using SnipeSharp.PowerShell.BindingTypes;

namespace SnipeSharp.PowerShell.Cmdlets.Set
{
    /// <summary>
    /// <para type="synopsis">Changes the order of fields in an existing Snipe-IT field set.</para>
    /// <para type="description">The Set-FieldSetOrder cmdlet changes the order of custom fields in an existing Snipe-IT field set object.</para>
    /// </summary>
    /// <example>
    ///   <code>Set-FieldSetOrder -Name "Potato Peeler" -Order "Length", "Width", "Handle Size"</code>
    ///   <para>Changes the order of fields in the fieldset "Potato Peeler" to  "Length, Width, Handle Size".</para>
    /// </example>
    [Cmdlet(VerbsCommon.Set, "FieldSetOrder")]
    [OutputType(typeof(FieldSet))]
    public class SetFieldSetOrder: SetObject<FieldSet>
    {
        /// <summary>
        /// The fields of the field set, in the order desired.
        /// </summary>
        [Parameter(Mandatory = true)]
        [Alias("Fields")]
        public ObjectBinding<CustomField>[] Order { get; set; }
        
        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            switch(ParameterSetName)
            {
                case nameof(ParameterSets.ByInternalId):
                    Identity = ObjectBinding<FieldSet>.FromId(Id);
                    break;
                case nameof(ParameterSets.ByName):
                    Identity = ObjectBinding<FieldSet>.FromName(Name);
                    break;
                case nameof(ParameterSets.ByIdentity):
                    break;
            }
            if(Identity.IsNull)
            {
                WriteError(new ErrorRecord(Identity.Error, $"{nameof(FieldSet)} not found: {Identity.Query}", ErrorCategory.InvalidArgument, null));
                return;
            }

            var orderedFields = new List<CustomField>();
            foreach(var field in Order)
            {
                if(field.IsNull)
                {
                    WriteError(new ErrorRecord(field.Error, $"{nameof(CustomField)} was not found: {field.Query}", ErrorCategory.InvalidArgument, null));
                    return;
                }
                orderedFields.Add(field.Object);
            }

            WriteObject(ApiHelper.Instance.GetEndPoint<CustomField>().Reorder(Identity.Object, orderedFields));
        }
        
        /// <inheritdoc />
        protected override void PopulateItem(FieldSet item)
        {
            // nop, not called
        }
    }
}