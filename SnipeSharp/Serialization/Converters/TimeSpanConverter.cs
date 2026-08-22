using Newtonsoft.Json;
using System;

namespace SnipeSharp.Serialization.Converters
{
    internal sealed class TimeSpanConverter : JsonConverter<TimeSpan?>
    {
        public static readonly TimeSpanConverter Instance = new TimeSpanConverter();

        public override TimeSpan? ReadJson(JsonReader reader, Type objectType, TimeSpan? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var days = serializer.Deserialize<int?>(reader);
            if (null == days)
                return null;
            return new TimeSpan(days.Value, 0, 0, 0);
        }

        public override void WriteJson(JsonWriter writer, TimeSpan? value, JsonSerializer serializer)
        {
            if (null == value)
                writer.WriteNull();
            else
                writer.WriteValue(value.Value.Days);
        }
    }
}
