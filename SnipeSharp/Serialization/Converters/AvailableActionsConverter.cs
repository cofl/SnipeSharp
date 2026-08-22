using Newtonsoft.Json;
using SnipeSharp.Models.Enumerations;
using System;
using System.Collections.Generic;

namespace SnipeSharp.Serialization.Converters
{
    internal sealed class AvailableActionsConverter : JsonConverter<AvailableAction>
    {
        public static readonly AvailableActionsConverter Instance = new AvailableActionsConverter();
        public override AvailableAction ReadJson(JsonReader reader, Type objectType, AvailableAction existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var dictionary = serializer.Deserialize<Dictionary<AvailableAction, bool>>(reader);
            var set = AvailableAction.None;
            if (null != dictionary)
                foreach (var pair in dictionary)
                    if (pair.Value)
                        set |= pair.Key;
            return set;
        }

        public override void WriteJson(JsonWriter writer, AvailableAction value, JsonSerializer serializer)
            => throw new NotImplementedException();
    }
}
