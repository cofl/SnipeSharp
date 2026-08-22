using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SnipeSharp.Models.Enumerations;
using SnipeSharp.Serialization;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SnipeSharp.Models
{
    /// <summary>
    /// An assignment. An asset may be assigned to a <see cref="User" />, a <see cref="Location" />,
    /// or a <see cref="Asset" />. This class wraps all three for the API.
    /// </summary>
    public sealed class AssetAssignedTo : ApiObject
    {
        [JsonConstructor]
        private AssetAssignedTo() { }

        internal AssetAssignedTo(User user)
        {
            Id = user.Id;
            Name = user.Name;
            Type = AssignedToType.User;
        }

        internal AssetAssignedTo(Location location)
        {
            Id = location.Id;
            Name = location.Name;
            Type = AssignedToType.Location;
        }

        internal AssetAssignedTo(Asset asset)
        {
            Id = asset.Id;
            Name = asset.Name;
            Type = AssignedToType.Asset;
        }

        /// <value>The internal Id of the object.</value>
        [DeserializeAs("id")]
        public int Id { get; private set; }

        /// <value>The name of the object.</value>
        [DeserializeAs("name")]
        public string Name { get; set; }

        /// <value>The type of object this represents.</value>
        [DeserializeAs("type")]
        public AssignedToType Type { get; set; }

        /// <inheritdoc />
        public override string ToString()
            => Name ?? Id.ToString();

        [JsonExtensionData]
        private Dictionary<string, JToken> _extensionData { get; set; }

        /// <summary>Extra fields that may have been included with the API response not represented by this model.</summary>
        public IReadOnlyDictionary<string, string> ExtensionData { get; private set; } = new Dictionary<string, string>();

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            if (null == _extensionData)
                return;
            var dict = new Dictionary<string, string>();
            foreach (var pair in _extensionData)
                dict[pair.Key] = pair.Value.ToObject<string>();
            ExtensionData = dict;
            _extensionData = null;
        }

        /// <summary>
        /// Converts this object into a stub user object that can be used for checking in.
        /// </summary>
        /// <returns>A stub user object, with only the ID field populated.</returns>
        public StubUser AsUser()
        {
            if (Type != AssignedToType.User)
                throw new InvalidOperationException($"Object {Id} is a \"{Type}\", not a User.");
            return new StubUser(
                id: Id,
                name: Name,
                firstName: ExtensionData.TryGetValue("first_name", out var firstName) ? firstName : null,
                lastName: ExtensionData.TryGetValue("last_name", out var lastName) ? lastName : null,
                userName: ExtensionData.TryGetValue("username", out var username) ? username : null,
                employeeNumber: ExtensionData.TryGetValue("employee_number", out var employeeNumber) ? employeeNumber : null
            );
        }

        /// <summary>
        /// Converts this object into a stub location object that can be used for checking in.
        /// </summary>
        /// <returns>A stub location object.</returns>
        public Stub<Location> AsLocation()
        {
            if (Type != AssignedToType.Location)
                throw new InvalidOperationException($"Object {Id} is a \"{Type}\", not a Location.");
            // location assignments have no extension data to copy
            return new Stub<Location>(Id, Name);
        }

        /// <summary>
        /// Converts this object into a stub asset object that can be used for checking in.
        /// </summary>
        /// <returns>A stub asset object.</returns>
        public Stub<Asset> AsAsset()
        {
            if (Type != AssignedToType.Asset)
                throw new InvalidOperationException($"Object {Id} is a \"{Type}\", not a Asset.");
            // asset assignments have no extension data to copy
            return new Stub<Asset>(Id, Name);
        }
    }
}
