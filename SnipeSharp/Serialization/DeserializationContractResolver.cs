using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SnipeSharp.Serialization.Converters;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace SnipeSharp.Serialization
{
    internal sealed class DeserializationContractResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);
            var attribute = member.GetCustomAttribute<DeserializeAsAttribute>(inherit: false);
            if (null != attribute)
            {
                property.PropertyName = attribute.Key;
                property.Writable = true;
                switch (attribute.DeserializeAs)
                {
                    case DeserializeAs.Default:
                        break;
                    case DeserializeAs.Timestamp:
                        property.Converter = TimestampConverter.Instance;
                        break;
                    case DeserializeAs.DateObject:
                        property.Converter = DateObjectConverter.Instance;
                        break;
                    case DeserializeAs.AvailableActions:
                        property.Converter = AvailableActionsConverter.Instance;
                        break;
                    case DeserializeAs.SimpleDate:
                        property.Converter = SimpleDateConverter.Instance;
                        break;
                    case DeserializeAs.Timespan:
                        property.Converter = TimeSpanConverter.Instance;
                        break;
                    case DeserializeAs.MonthStringAsInt:
                        property.Converter = MonthStringToIntConverter.Instance;
                        break;
                    case DeserializeAs.ReadOnlyCollection:
                        property.Converter = ReadOnlyResponseCollectionConverter.Instance;
                        break;
                    case DeserializeAs.MaybeFalseUri:
                        property.Converter = MaybeFalseUriConverter.Instance;
                        break;
                    case DeserializeAs.CustomFieldDictionary:
                        property.Converter = CustomFieldDictionaryConverter.Instance;
                        break;
                    case DeserializeAs.PermissionDictionary:
                        property.Converter = PermissionDictionaryConverter.Instance;
                        break;
                }
            }
            else
            {
                property.Ignored = true;
            }
            return property;
        }

        protected override List<MemberInfo> GetSerializableMembers(Type objectType)
        {
            // start with the defaults
            var list = base.GetSerializableMembers(objectType);
            // add non-public, readable, serializable properties
            foreach (var member in objectType.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic))
                if (member.CanWrite)
                    list.Add(member);
            return list;
        }
    }
}
