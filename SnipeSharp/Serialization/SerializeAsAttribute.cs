using System;

namespace SnipeSharp.Serialization
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    internal sealed class SerializeAsAttribute : Attribute
    {
        public string Key { get; set; }
        public SerializeAs SerializeAs { get; private set; }

        public bool IsRequired { get; set; } = false;

        internal SerializeAsAttribute(string key, SerializeAs serializationType = SerializeAs.Default)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentException("Key cannot be null or empty.", paramName: nameof(key));
            Key = key;
            SerializeAs = serializationType;
        }
    }

    /// <summary>
    /// Any special converters for Serialization
    /// </summary>
    internal enum SerializeAs
    {
        Default = 0,

        SimpleDate,

        SimpleDateOnly,

        Timespan,

        IdValue,

        IdValueArray,
    }
}
