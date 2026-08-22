using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SnipeSharp.Serialization.Converters;
using System.Reflection;

namespace SnipeSharp.Serialization
{
    internal sealed class SerializationContractResolver : DefaultContractResolver
    {
        public static bool TryGetConverter(PropertyInfo property, SerializeAsAttribute fieldConverter, out JsonConverter converter)
        {
            switch (fieldConverter.SerializeAs)
            {
                case SerializeAs.Default:
                    if (null == property)
                    {
                        converter = null;
                        return false;
                    }
                    if (property.PropertyType.IsAssignableFrom(typeof(bool?)))
                    {
                        converter = NullableBooleanConverter.Instance;
                        return true;
                    }

                    converter = null;
                    return false;
                case SerializeAs.SimpleDate:
                    converter = SimpleDateConverter.Instance;
                    return true;
                case SerializeAs.SimpleDateOnly:
                    converter = SimpleDateOnlyConverter.Instance;
                    return true;
                case SerializeAs.Timespan:
                    converter = TimeSpanConverter.Instance;
                    return true;
                case SerializeAs.IdValue:
                    converter = SerializeToIdConverter.Instance;
                    return true;
                case SerializeAs.IdValueArray:
                    converter = SerializeToIdArrayConverter.Instance;
                    return true;
            }
            converter = null;
            return false;
        }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);
            var attribute = member.GetCustomAttribute<SerializeAsAttribute>(inherit: false);
            if ((null != attribute && !string.IsNullOrEmpty(attribute.Key)))
            {
                property.PropertyName = attribute.Key;
                property.Readable = true;
                var patch = member.GetCustomAttribute<PatchAttribute>(true);
                if (null != patch)
                {
                    var targetField = member.DeclaringType.GetField(patch.IndicatorFieldName, BindingFlags.Instance | BindingFlags.NonPublic);
                    if (null != targetField)
                        property.ShouldSerialize = (instance) => (bool)targetField.GetValue(instance);
                    else
                    {
                        var targetProperty = member.DeclaringType.GetProperty(patch.IndicatorFieldName, BindingFlags.Instance | BindingFlags.NonPublic);
                        property.ShouldSerialize = (instance) => (bool)targetProperty.GetValue(instance);
                    }
                }
                if (TryGetConverter(member as PropertyInfo, attribute, out var converter))
                    property.Converter = converter;
            }
            else
            {
                property.Ignored = true;
            }
            return property;
        }
    }
}
