using Newtonsoft.Json;
using SnipeSharp.Collections;
using SnipeSharp.Models;
using System;
using System.Collections.Generic;

namespace SnipeSharp.Serialization.Converters
{
    internal sealed class CustomFieldDictionaryConverter : JsonConverter<CustomFieldDictionary>
    {
        internal static readonly CustomFieldDictionaryConverter Instance = new CustomFieldDictionaryConverter();

        public override CustomFieldDictionary ReadJson(JsonReader reader, Type objectType, CustomFieldDictionary existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.StartArray)
            {
                serializer.Deserialize(reader); // discard, we don't want an array.
                return existingValue;
            }
            else
            {
                var values = serializer.Deserialize<Dictionary<string, AssetCustomField>>(reader);
                if (null == values)
                    throw new NullReferenceException("Failed to deserialize AssetCustomField dictionary");
                var dictionary = new CustomFieldDictionary();
                foreach (var pair in values)
                {
                    pair.Value.FriendlyName = pair.Key;
                    dictionary.Add(pair.Value);
                }
                return dictionary;
            }
        }

        public override void WriteJson(JsonWriter writer, CustomFieldDictionary value, JsonSerializer serializer)
            => throw new NotImplementedException();
    }
}
