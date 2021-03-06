using System.Management.Automation;
using SnipeSharp.Models;
using SnipeSharp.PowerShell.BindingTypes;

namespace SnipeSharp.PowerShell.Cmdlets
{
    /// <summary>Removes a Snipe IT Department.</summary>
    /// <remarks>
    ///   <para>The Remove-Department cmdlet removes one or more Department objects by name or by Snipe IT internal id number from the Snipe IT database.</para>
    ///   <para>Whatever identifier is used, both accept pipeline input.</para>
    /// </remarks>
    /// <example>
    ///   <code>Remove-Department 12</code>
    ///   <para>Removes a Department by its Internal Id.</para>
    /// </example>
    /// <example>
    ///   <code>Remove-Department Department4368</code>
    ///   <para>Removes a Department explicitly by its Name.</para>
    /// </example>
    /// <example>
    ///   <code>1..100 | Remove-Department</code>
    ///   <para>Removes the first 100 Department objects by their Snipe IT internal Id numbers.</para>
    /// </example>
    /// <seealso cref="FindDepartment" />
    [Cmdlet(VerbsCommon.Remove, nameof(Department),
        DefaultParameterSetName = nameof(RemoveObject<Department, ObjectBinding<Department>>.ParameterSets.ByName),
        ConfirmImpact = ConfirmImpact.High,
        SupportsShouldProcess = true
    )]
    [OutputType(typeof(RequestResponse<Department>))]
    public sealed class RemoveDepartment: RemoveObject<Department, ObjectBinding<Department>>
    {
    }
}
