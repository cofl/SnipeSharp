using System.Management.Automation;
using SnipeSharp.Models;
using SnipeSharp.PowerShell.BindingTypes;

namespace SnipeSharp.PowerShell.Cmdlets
{
    /// <summary>Gets a Snipe IT depreciation.</summary>
    /// <remarks>
    ///   <para>The Get-Depreciation cmdlet gets one or more depreciation objects by name or by Snipe IT internal id number.</para>
    ///   <para>Whatever identifier is used, both accept pipeline input.</para>
    /// </remarks>
    /// <example>
    ///   <code>Get-Depreciation 14</code>
    ///   <para>Retrieve an depreciation by its Internal Id.</para>
    /// </example>
    /// <example>
    ///   <code>Get-Depreciation Depreciation4368</code>
    ///   <para>Retrieve an depreciation explicitly by its Name.</para>
    /// </example>
    /// <example>
    ///   <code>1..100 | Get-Depreciation</code>
    ///   <para>Retrieve the first 100 depreciations by their Snipe IT internal Id numbers.</para>
    /// </example>
    /// <seealso cref="FindDepreciation" />
    [Cmdlet(VerbsCommon.Get, nameof(Depreciation), DefaultParameterSetName = nameof(GetDepreciation.ParameterSets.All))]
    [OutputType(typeof(Depreciation))]
    public sealed class GetDepreciation: GetObject<Depreciation, ObjectBinding<Depreciation>>
    {
    }
}
