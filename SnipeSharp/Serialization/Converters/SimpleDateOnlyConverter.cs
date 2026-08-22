using Newtonsoft.Json;
using System;

namespace SnipeSharp.Serialization.Converters
{
    internal sealed class SimpleDateOnlyConverter : JsonConverter<DateTime?>
    {
        public static readonly SimpleDateOnlyConverter Instance = new SimpleDateOnlyConverter();

        public override DateTime? ReadJson(JsonReader reader, Type objectType, DateTime? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var str = serializer.Deserialize<string>(reader);
            if (!string.IsNullOrWhiteSpace(str) && DateTime.TryParse(str, out var datetime))
                return datetime;
            return null;
        }

        public override void WriteJson(JsonWriter writer, DateTime? value, JsonSerializer serializer)
        {
            if (null == value)
                writer.WriteNull();
            else
                // value used for checkout_at in app/Http/Controllers/Api/AssetsController.php#checkout(AssetCheckoutRequest,int)
                // is of the form 'Y-m-d H:i:s'; I assume expected_checkin is the same, so we'll use this for it -- @cofl
                writer.WriteValue(value.Value.ToString("yyyy-MM-dd"));
        }
    }
}
