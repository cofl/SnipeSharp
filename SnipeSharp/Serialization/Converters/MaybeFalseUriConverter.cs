using Newtonsoft.Json;
using System;

namespace SnipeSharp.Serialization.Converters
{
    internal sealed class MaybeFalseUriConverter : JsonConverter<Uri>
    {
        public static readonly MaybeFalseUriConverter Instance = new MaybeFalseUriConverter();
        public override Uri ReadJson(JsonReader reader, Type objectType, Uri existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var obj = serializer.Deserialize(reader) as string;
            if (null == obj)
                return null;
            return new Uri(obj);
        }

        public override void WriteJson(JsonWriter writer, Uri value, JsonSerializer serializer)
            => throw new NotImplementedException();
    }
}
