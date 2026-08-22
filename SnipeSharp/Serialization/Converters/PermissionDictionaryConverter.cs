using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SnipeSharp.Serialization
{
    internal sealed class PermissionDictionaryConverter : JsonConverter<Dictionary<string, bool>>
    {
        public static readonly PermissionDictionaryConverter Instance = new PermissionDictionaryConverter();
        public override Dictionary<string, bool> ReadJson(JsonReader reader, Type objectType, Dictionary<string, bool> existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var rawDictionary = serializer.Deserialize<Dictionary<string, int>>(reader);
            var newDictionary = new Dictionary<string, bool>();
            if (null != rawDictionary)
            {
                foreach (var pair in rawDictionary)
                {
                    newDictionary[pair.Key] = pair.Value != 0;
                }
            }
            return newDictionary;
        }

        public override void WriteJson(JsonWriter writer, Dictionary<string, bool> value, JsonSerializer serializer)
            => throw new NotImplementedException();
    }
}
