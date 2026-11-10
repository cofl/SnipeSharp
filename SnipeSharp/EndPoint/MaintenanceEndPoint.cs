using SnipeSharp.Models;
using SnipeSharp.Models.Enumerations;
using System;
using System.Linq;

namespace SnipeSharp.EndPoint
{
    /// <summary>
    /// Provides additional behaviour for the Maintenances endpoint.
    /// </summary>
    public sealed class MaintenanceEndPoint : EndPoint<Maintenance>
    {
        /// <param name="api">The Api to grab the RequestManager from.</param>
        internal MaintenanceEndPoint(SnipeItApi api) : base(api) { }

        /// <summary>
        /// Create a maintenance.
        /// </summary>
        /// <remarks>
        /// Snipe-IT v8.8+ requires a <see cref="Maintenance.MaintenanceTypeId"/>. When only
        /// <see cref="Maintenance.MaintenanceType"/> is set, the id is looked up by name.
        /// </remarks>
        /// <exception cref="ArgumentException">If neither a type nor a type id is set, or no Snipe-IT maintenance type has that name.</exception>
        public override Maintenance Create(Maintenance toCreate)
        {
            if (null == toCreate)
                throw new ArgumentNullException(nameof(toCreate));
            if (null == toCreate.MaintenanceTypeId)
            {
                if (null == toCreate.MaintenanceType)
                    throw new ArgumentException($"Set {nameof(Maintenance.MaintenanceTypeId)} or {nameof(Maintenance.MaintenanceType)}.", nameof(toCreate));
                toCreate.MaintenanceTypeId = GetMaintenanceTypeId(toCreate.MaintenanceType.Value);
            }
            return base.Create(toCreate);
        }

        /// <summary>
        /// Find the id of the Snipe-IT maintenance type matching a built-in <see cref="MaintenanceType"/>.
        /// </summary>
        /// <exception cref="ArgumentException">If Snipe-IT has no maintenance type with that name.</exception>
        public int GetMaintenanceTypeId(MaintenanceType type)
        {
            var name = MaintenanceTypeNames.ToApiName(type);
            var match = Api.MaintenanceTypes.FindAll()
                .FirstOrDefault(t => string.Equals(t.Name?.Trim(), name, StringComparison.OrdinalIgnoreCase));
            if (null == match)
                throw new ArgumentException($"Snipe-IT has no maintenance type named \"{name}\".", nameof(type));
            return match.Id;
        }
    }
}
