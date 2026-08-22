using SnipeSharp.Models;
using System;
using System.Collections;
using System.Collections.Generic;

namespace SnipeSharp.Collections
{
    /// <summary>A custom dictionary wrapper for asset custom fields, allowing string->string mapping.</summary>
    public sealed class CustomFieldDictionary : IDictionary<string, AssetCustomField>
    {
        /// <summary>
        /// Create a new Custom Field dictionary.
        /// </summary>
        internal CustomFieldDictionary()
        {
            Friendly = new FriendlyNameDictionary(this);
        }

        private Dictionary<string, AssetCustomField> BackingDictionary = new Dictionary<string, AssetCustomField>();

        /// <value>The underlying model dictionary.</value>
        public IDictionary<string, AssetCustomField> Models => (IDictionary<string, AssetCustomField>)this;

        /// <summary>Maps the friendly names to the internal database column names.</summary>
        private Dictionary<string, string> FriendlyNames = new Dictionary<string, string>();

        /// <summary>This same dictionary, but mapping from friendly names instead of internal column names.</summary>
        public FriendlyNameDictionary Friendly { get; }

        /// <summary>The string values of the fields in this dictionary.</summary>
        public IEnumerable<string> StringValues => GetValues();
        private IEnumerable<string> GetValues()
        {
            foreach (var value in Values)
                yield return value.Value;
        }

        /// <summary>Has the dictionary been modified?</summary>
        internal bool IsModified
        {
            get
            {
                foreach (var val in Values)
                    if (val.IsModified)
                        return true;
                return false;
            }
            set
            {
                foreach (var val in Values)
                    val.IsModified = value;
            }
        }

        /// <inheritdoc />
        public ICollection<string> Keys => BackingDictionary.Keys;

        /// <inheritdoc />
        public ICollection<AssetCustomField> Values => BackingDictionary.Values;

        /// <inheritdoc />
        public int Count => BackingDictionary.Count;

        /// <inheritdoc />
        public bool IsReadOnly => ((IDictionary<string, AssetCustomField>)BackingDictionary).IsReadOnly;

        /// <summary>Get and set custom field values by SnipeIT database column name.</summary>
        /// <param name="key">The database column name</param>
        public string this[string key]
        {
            get => BackingDictionary[key].Value;
            set
            {
                if (null == key)
                    throw new ArgumentNullException(paramName: nameof(key));
                if (null == value)
                    throw new ArgumentNullException(paramName: nameof(value));
                if (BackingDictionary.TryGetValue(key, out var model))
                    model.Value = value;
                else
                {
                    model = new AssetCustomField { FriendlyName = key, Field = key, Value = value };
                }
                ((IDictionary<string, AssetCustomField>)this)[key] = model;
            }
        }

        /// <exception cref="ArgumentNullException">If the value is null.</exception>
        /// <exception cref="ArgumentException">If the value.Field property is not null and it does not match the key.</exception>
        /// <exception cref="ArgumentException">If the dictionary already contains the value's friendly name and the value is not being updated.</exception>
        AssetCustomField IDictionary<string, AssetCustomField>.this[string key]
        {
            get => BackingDictionary[key];
            set
            {
                if (null == key)
                    throw new ArgumentNullException(paramName: nameof(key));
                if (null == value)
                    throw new ArgumentNullException(paramName: nameof(value));
                if (null != value.Field && key != value.Field)
                    throw new ArgumentException($"Field key \"{value.Field}\" is not null and does not match key \"{key}\"", paramName: nameof(value));
                if (null == value.Field || null == value.FriendlyName)
                {
                    value = new AssetCustomField
                    {
                        Field = value.Field ?? key,
                        FriendlyName = value.FriendlyName ?? value.Field ?? key,
                        Format = value.Format,
                        Value = value.Value
                    };
                }
                var hasFriendlyName = FriendlyNames.TryGetValue(value.FriendlyName, out var existingKey);
                if (hasFriendlyName && existingKey != value.Field)
                    throw new ArgumentException($"The dictionary already contains a friendly name \"{value.FriendlyName}\"");
                value.IsModified = true;
                if (!BackingDictionary.ContainsKey(key))
                {
                    Add(value);
                }
                else
                {
                    // remove the non-matching friendly name for the existing field.
                    if (!hasFriendlyName)
                        FriendlyNames.Remove(BackingDictionary[value.Field].FriendlyName);
                    BackingDictionary[value.Field] = value;
                    FriendlyNames[value.FriendlyName] = value.Field;
                }
            }
        }

        /// <summary>Attempt to get custom field values by SnipeIT database column name.</summary>
        /// <param name="key">The database column name</param>
        /// <param name="value">The value of the field, or null.</param>
        /// <returns>True, if the field is present.</returns>
        public bool TryGetValue(string key, out string value)
        {
            if (BackingDictionary.TryGetValue(key, out var model))
            {
                value = model.Value;
                return true;
            }
            value = null;
            return false;
        }

        /// <inheritdoc />
        bool IDictionary<string, AssetCustomField>.TryGetValue(string key, out AssetCustomField value)
            => BackingDictionary.TryGetValue(key, out value);

        /// <summary>Add custom field values by SnipeIT database column name.</summary>
        public void Add(string key, string value)
            => Add(new AssetCustomField { FriendlyName = key, Field = key, Value = value });

        /// <summary>Adds an <see cref="AssetCustomField"/> to the dictionary.</summary>
        /// <remarks>
        ///     <para>The key is in the <see cref="AssetCustomField.Field" /> property of <paramref name="value" />.</para>
        ///     <para>If the <see cref="AssetCustomField.FriendlyName" /> property is null, the <see cref="AssetCustomField.Field" /> property will be used.</para>
        /// </remarks>
        /// <param name="value">The field to add to the dictionary.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="value"/> or its <see cref="AssetCustomField.Field"/> property are null.</exception>
        /// <exception cref="ArgumentException">If the dictionary already contains the value's friendly name and the value is not being updated.</exception>
        public void Add(AssetCustomField value)
            => ((IDictionary<string, AssetCustomField>)this).Add(value?.Field, value);

        /// <inheritdoc />
        void ICollection<KeyValuePair<string, AssetCustomField>>.Add(KeyValuePair<string, AssetCustomField> item)
            => ((IDictionary<string, AssetCustomField>)this).Add(item.Key, item.Value);

        /// <inheritdoc />
        /// <exception cref="ArgumentNullException">If <paramref name="value"/> or its <see cref="AssetCustomField.Field"/> property are null.</exception>
        /// <exception cref="ArgumentNullException">If <paramref name="key"/> is null.</exception>
        /// <exception cref="ArgumentException">If the dictionary already contains the value's friendly name and the value is not being updated.</exception>
        void IDictionary<string, AssetCustomField>.Add(string key, AssetCustomField value)
        {
            if (null == value)
                throw new ArgumentNullException(paramName: nameof(value));
            if (null == key)
                throw new ArgumentNullException(paramName: nameof(key));
            if (null != value.Field && key != value.Field)
                throw new ArgumentException($"Field key \"{value.Field}\" is not null and does not match key \"{key}\"", paramName: nameof(value));
            if (null == value.Field || null == value.FriendlyName)
            {
                value = new AssetCustomField
                {
                    Field = value.Field ?? key,
                    FriendlyName = value.FriendlyName ?? value.Field ?? key,
                    Format = value.Format,
                    Value = value.Value
                };
            }
            value.IsModified = true;
            if (ContainsKey(value.Field))
                throw new ArgumentException($"The dictionary already contains a key \"{value.Field}\"");
            if (FriendlyNames.ContainsKey(value.FriendlyName))
                throw new ArgumentException($"The dictionary already contains a friendly name \"{value.FriendlyName}\"");
            BackingDictionary.Add(value.Field, value);
            FriendlyNames.Add(value.FriendlyName, value.Field);
        }

        /// <inheritdoc />
        public bool ContainsKey(string key)
            => BackingDictionary.ContainsKey(key);

        /// <inheritdoc />
        public bool Contains(KeyValuePair<string, AssetCustomField> item)
            => ((IDictionary<string, AssetCustomField>)BackingDictionary).Contains(item);

        /// <inheritdoc />
        public void Clear()
        {
            BackingDictionary.Clear();
            FriendlyNames.Clear();
        }

        void ICollection<KeyValuePair<string, AssetCustomField>>.CopyTo(KeyValuePair<string, AssetCustomField>[] array, int arrayIndex)
            => ((IDictionary<string, AssetCustomField>)BackingDictionary).CopyTo(array, arrayIndex);

        /// <summary>It is not possible to remove fields from a CustomFieldDictionary.</summary>
        bool IDictionary<string, AssetCustomField>.Remove(string key)
            => throw new NotImplementedException();

        /// <summary>It is not possible to remove fields from a CustomFieldDictionary.</summary>
        bool ICollection<KeyValuePair<string, AssetCustomField>>.Remove(KeyValuePair<string, AssetCustomField> item)
            => throw new NotImplementedException();

        /// <inheritdoc />
        public IEnumerator<KeyValuePair<string, AssetCustomField>> GetEnumerator()
            => BackingDictionary.GetEnumerator();

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        IEnumerator<KeyValuePair<string, AssetCustomField>> IEnumerable<KeyValuePair<string, AssetCustomField>>.GetEnumerator()
            => ((IDictionary<string, AssetCustomField>)BackingDictionary).GetEnumerator();

        /// <summary>
        /// A dictionary mapping from friendly names to values, wrapping a <see cref="CustomFieldDictionary"/>.
        /// </summary>
        public sealed class FriendlyNameDictionary : IReadOnlyDictionary<string, string>
        {
            private readonly CustomFieldDictionary CustomFields;
            internal FriendlyNameDictionary(CustomFieldDictionary dictionary)
            {
                CustomFields = dictionary;
            }

            /// <inheritdoc />
            public string this[string key]
            {
                get => CustomFields[CustomFields.FriendlyNames[key]];
                set => CustomFields[CustomFields.FriendlyNames[key]] = value;
            }

            /// <inheritdoc />
            public IEnumerable<string> Keys => CustomFields.FriendlyNames.Keys;

            /// <inheritdoc />
            public IEnumerable<string> Values => CustomFields.StringValues;

            /// <inheritdoc />
            public int Count => CustomFields.Count;

            /// <inheritdoc />
            public bool ContainsKey(string key)
                => CustomFields.FriendlyNames.ContainsKey(key);

            /// <inheritdoc />
            public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
            {
                foreach (var key in Keys)
                    yield return new KeyValuePair<string, string>(key, CustomFields[CustomFields.FriendlyNames[key]]);
            }

            /// <inheritdoc />
            public bool TryGetValue(string key, out string value)
                => CustomFields.TryGetValue(CustomFields.FriendlyNames[key], out value);

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
