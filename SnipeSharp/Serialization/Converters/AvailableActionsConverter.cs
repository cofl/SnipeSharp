using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SnipeSharp.Models.Enumerations;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;

namespace SnipeSharp.Serialization.Converters
{
    /// <summary>
    /// Reads Snipe-IT's <c>available_actions</c> object into an <see cref="AvailableAction"/> flag set.
    /// </summary>
    /// <remarks>
    /// Snipe-IT adds new actions between releases (v8 added <c>view</c>, <c>bulk_selectable</c>,
    /// <c>adjust_quantity</c> and <c>complete</c>). Keys this library does not know are skipped
    /// rather than failing the whole response.
    /// </remarks>
    internal sealed class AvailableActionsConverter : JsonConverter<AvailableAction>
    {
        public static readonly AvailableActionsConverter Instance = new AvailableActionsConverter();

        private static readonly Dictionary<string, AvailableAction> ByApiName = BuildLookup();

        private static Dictionary<string, AvailableAction> BuildLookup()
        {
            var lookup = new Dictionary<string, AvailableAction>(StringComparer.OrdinalIgnoreCase);
            foreach (var field in typeof(AvailableAction).GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                var value = (AvailableAction)field.GetValue(null);
                if (value == AvailableAction.None)
                    continue;
                var apiName = field.GetCustomAttribute<EnumMemberAttribute>()?.Value ?? field.Name;
                lookup[apiName] = value;
            }
            return lookup;
        }

        public override AvailableAction ReadJson(JsonReader reader, Type objectType, AvailableAction existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var token = JToken.Load(reader);
            var set = AvailableAction.None;
            if (!(token is JObject actions))
                return set;

            foreach (var action in actions.Properties())
                if (ByApiName.TryGetValue(action.Name, out var flag) && IsTruthy(action.Value))
                    set |= flag;
            return set;
        }

        private static bool IsTruthy(JToken value)
        {
            switch (value.Type)
            {
                case JTokenType.Boolean:
                    return value.Value<bool>();
                case JTokenType.Integer:
                    return value.Value<long>() != 0;
                case JTokenType.String:
                    var s = value.Value<string>();
                    return bool.TryParse(s, out var b) ? b : s == "1";
                default:
                    return false;
            }
        }

        public override void WriteJson(JsonWriter writer, AvailableAction value, JsonSerializer serializer)
            => throw new NotImplementedException();
    }
}
