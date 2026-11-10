using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;

namespace SnipeSharp.Models.Enumerations
{
    /// <summary>
    /// Maps <see cref="MaintenanceType"/> values to and from the names Snipe-IT uses for them.
    /// </summary>
    /// <remarks>Matching is case-insensitive: v8.8 seeds "PAT Test" where earlier versions used "PAT test".</remarks>
    internal static class MaintenanceTypeNames
    {
        private static readonly Dictionary<string, MaintenanceType> ByName = BuildLookup();

        private static Dictionary<string, MaintenanceType> BuildLookup()
        {
            var lookup = new Dictionary<string, MaintenanceType>(StringComparer.OrdinalIgnoreCase);
            foreach (var field in typeof(MaintenanceType).GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                var value = (MaintenanceType)field.GetValue(null);
                lookup[field.GetCustomAttribute<EnumMemberAttribute>()?.Value ?? field.Name] = value;
                lookup[field.Name] = value;
            }
            return lookup;
        }

        internal static bool TryParse(string name, out MaintenanceType type)
        {
            if (!string.IsNullOrWhiteSpace(name) && ByName.TryGetValue(name.Trim(), out type))
                return true;
            type = default;
            return false;
        }

        internal static string ToApiName(MaintenanceType type)
            => typeof(MaintenanceType).GetField(type.ToString())?.GetCustomAttribute<EnumMemberAttribute>()?.Value
               ?? type.ToString();
    }
}
