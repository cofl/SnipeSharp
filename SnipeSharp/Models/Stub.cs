using Newtonsoft.Json;
using SnipeSharp.Serialization;

namespace SnipeSharp.Models
{
    /// <summary>
    /// Represents a stub object of a certain type, with only the Id and Name filled by the API.
    /// </summary>
    public class Stub<T> : ApiObject, IObjectWithId
        where T : AbstractBaseModel, new()
    {
        /// <summary>The Id of the object.</summary>
        [DeserializeAs("id")]
        public int Id { get; protected set; }

        /// <summary>The name of the object.</summary>
        [DeserializeAs("name")]
        public string Name { get; protected set; }

        /// <summary>The default constructor used by subclasses and the deserializer.</summary>
        [JsonConstructor]
        protected Stub() { }

        internal Stub(int id, string name)
        {
            Id = id;
            Name = name;
        }

        /// <summary>
        /// Transform a stub to a full object, if for some reason you need to do that.
        /// </summary>
        public static implicit operator T(Stub<T> stub) => new T { Id = stub.Id, Name = stub.Name };

        /// <summary>
        /// Transform a full object to a stub, for setting values.
        /// </summary>
        public static implicit operator Stub<T>(T full) => new Stub<T> { Id = full.Id, Name = full.Name };
    }
}
