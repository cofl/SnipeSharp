using System.Management.Automation;
using SnipeSharp.Models;
using SnipeSharp.PowerShell.BindingTypes;

namespace SnipeSharp.PowerShell.Cmdlets
{
    /// <summary>Removes a Snipe IT custom field.</summary>
    /// <remarks>
    ///   <para>The Remove-FieldSet cmdlet removes one or more custom field objects by name or by Snipe IT internal id number from the Snipe IT database.</para>
    ///   <para>Whatever identifier is used, both accept pipeline input.</para>
    /// </remarks>
    /// <example>
    ///   <code>Remove-CustomField 12</code>
    ///   <para>Removes a CustomField by its Internal Id.</para>
    /// </example>
    /// <example>
    ///   <code>Remove-CustomField FieldSet4368</code>
    ///   <para>Removes a CustomField explicitly by its Name.</para>
    /// </example>
    /// <example>
    ///   <code>1..100 | Remove-CustomField</code>
    ///   <para>Removes the first 100 CustomField objects by their Snipe IT internal Id numbers.</para>
    /// </example>
    /// <seealso cref="FindCustomField" />
    [Cmdlet(VerbsCommon.Remove, nameof(CustomField),
        DefaultParameterSetName = nameof(RemoveObject<CustomField, ObjectBinding<CustomField>>.ParameterSets.ByName),
        ConfirmImpact = ConfirmImpact.High,
        SupportsShouldProcess = true
    )]
    [OutputType(typeof(RequestResponse<CustomField>))]
    public sealed class RemoveCustomField: RemoveObject<CustomField, ObjectBinding<CustomField>>
    {
    }
}
