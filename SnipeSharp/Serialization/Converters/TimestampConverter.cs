using Newtonsoft.Json;
using System;

namespace SnipeSharp.Serialization.Converters
{
    internal sealed class TimestampConverter : JsonConverter<DateTime?>
    {
        public static readonly TimestampConverter Instance = new TimestampConverter();

        public override DateTime? ReadJson(JsonReader reader, Type objectType, DateTime? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.String)
            {
                // This is probably one of the broken API endpoints that returns the model directly
                // instead of properly transforming it.
                if (serializer.Deserialize<string>(reader) is string response && DateTime.TryParse(response, out var datetime))
                    return datetime;
            }
            else if (reader.TokenType == JsonToken.Date)
            {
                if (serializer.Deserialize<DateTime>(reader) is DateTime response && DateTime.TryParse(response.ToString(), out var datetime))
                    return datetime;
            }
            else if (serializer.Deserialize<TimestampResponse>(reader) is TimestampResponse response && DateTime.TryParse(response.DateTime, out var datetime))
                return datetime;


            return null;
        }

        public override void WriteJson(JsonWriter writer, DateTime? value, JsonSerializer serializer)
            => throw new NotImplementedException();

        private sealed class TimestampResponse
        {
            [DeserializeAs("datetime")]
            public string DateTime { get; set; }
        }
    }
}
