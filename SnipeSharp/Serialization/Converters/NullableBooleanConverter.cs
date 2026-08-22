using Newtonsoft.Json;
using System;

namespace SnipeSharp.Serialization.Converters
{
    internal sealed class NullableBooleanConverter : JsonConverter<bool?>
    {
        internal static readonly NullableBooleanConverter Instance = new NullableBooleanConverter();
        public override bool? ReadJson(JsonReader reader, Type objectType, bool? existingValue, bool hasExistingValue, JsonSerializer serializer)
            => serializer.Deserialize<bool?>(reader);

        public override void WriteJson(JsonWriter writer, bool? value, JsonSerializer serializer)
        {
            if (null == value)
                writer.WriteNull();
            else
                writer.WriteRawValue((bool)value ? "true" : "false");
        }
    }
}
