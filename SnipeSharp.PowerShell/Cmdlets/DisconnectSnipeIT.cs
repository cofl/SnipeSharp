using System.Management.Automation;

namespace SnipeSharp.PowerShell.Cmdlets
{
    /// <summary>Ends the current session with Snipe IT.</summary>
    /// <remarks>The Disconnect-SnipeIT cmdlet ends the current session with Snipe IT. This cmdlet does not throw any errors if there is no connected session.</remarks>
    /// <example>
    ///   <code>Disconnect-SnipeIT</code>
    ///   <para>Disconnect from the current Snipe IT session, or verify that there isn't a current session.</para>
    /// </example>
    /// <seealso cref="ConnectSnipeIT" />
    [Cmdlet(VerbsCommunications.Disconnect, "IT")]
    public sealed class DisconnectSnipeIT: PSCmdlet
    {
        /// <inheritdoc />
        protected override void EndProcessing(){
            ApiHelper.Instance = null;
        }
    }
}
