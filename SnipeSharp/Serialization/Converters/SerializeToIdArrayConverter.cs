using Newtonsoft.Json;
using SnipeSharp.Models;
using System;

namespace SnipeSharp.Serialization.Converters
{
    internal sealed class SerializeToIdArrayConverter : JsonConverter<AbstractBaseModel[]>
    {
        internal static readonly SerializeToIdArrayConverter Instance = new SerializeToIdArrayConverter();
        public override AbstractBaseModel[] ReadJson(JsonReader reader, Type objectType, AbstractBaseModel[] existingValue, bool hasExistingValue, JsonSerializer serializer)
            => (AbstractBaseModel[])serializer.Deserialize<GenericEndPointModel[]>(reader);

        public override void WriteJson(JsonWriter writer, AbstractBaseModel[] value, JsonSerializer serializer)
        {
            writer.WriteStartArray();
            if (null != value)
                foreach (var item in value)
                    writer.WriteValue(item.Id);
            writer.WriteEndArray();
        }
    }
}
