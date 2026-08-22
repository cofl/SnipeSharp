using Newtonsoft.Json;
using System;

namespace SnipeSharp.Serialization.Converters
{
    internal sealed class MonthStringToIntConverter : JsonConverter<int?>
    {
        public static readonly MonthStringToIntConverter Instance = new MonthStringToIntConverter();

        public override int? ReadJson(JsonReader reader, Type objectType, int? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            // from DepreciationsTransformer.php, month strings are created in the form $"{monthInteger} {translate("general.months")}"
            var str = serializer.Deserialize<string>(reader);
            if (null == str || str == "None")
                // in AssetModelsTransformer.php, if there is no eol, the string "None" is returned rather than null.
                return null;
            if (int.TryParse(str.Split(' ')[0], out var asInt))
                return asInt;
            return null;
        }

        public override void WriteJson(JsonWriter writer, int? value, JsonSerializer serializer)
            => throw new NotImplementedException();
    }
}
