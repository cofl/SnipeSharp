using SnipeSharp.EndPoint;

namespace SnipeSharp.Models
{
    /// <summary>
    /// A maintenance type as stored by Snipe-IT (v8.8+), e.g. "Repair" or "Calibration".
    /// </summary>
    /// <remarks>
    /// Snipe-IT moved maintenance types from a fixed list into their own table, so creating a
    /// <see cref="Maintenance"/> now needs the type's id. The fixed list is still available as
    /// <see cref="Enumerations.MaintenanceType"/>; <see cref="SnipeItApi.Maintenances"/> resolves
    /// those names to ids automatically.
    /// </remarks>
    [PathSegment("maintenance-types")]
    public sealed class MaintenanceTypeRecord : AbstractBaseModel
    {
        /// <summary>Create a new MaintenanceTypeRecord object.</summary>
        public MaintenanceTypeRecord() { }
    }
}
