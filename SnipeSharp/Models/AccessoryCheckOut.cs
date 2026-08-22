using SnipeSharp.Models.Enumerations;
using SnipeSharp.Serialization;

namespace SnipeSharp.Models
{
    /// <summary>
    /// An Accessory/User relation.
    /// </summary>
    public sealed class AccessoryCheckOut : ApiObject, IAvailableActions
    {
        /// <summary>
        /// The Id of the Accessory in Snipe-IT.
        /// </summary>
        [DeserializeAs("assigned_pivot_id")]
        public int AccessoryId { get; private set; }

        /// <summary>
        /// The Id of the User in Snipe-IT.
        /// </summary>
        [DeserializeAs("id")]
        public int UserId { get; private set; }

        /// <summary>
        /// The user's username.
        /// </summary>
        [DeserializeAs("username")]
        public string UserName { get; private set; }

        /// <summary>
        /// The user's full name.
        /// </summary>
        [DeserializeAs("name")]
        public string Name { get; private set; }

        /// <summary>
        /// The user's first name.
        /// </summary>
        [DeserializeAs("first_name")]
        public string FirstName { get; private set; }

        /// <summary>
        /// The user's last name.
        /// </summary>
        [DeserializeAs("last_name")]
        public string LastName { get; private set; }

        /// <summary>
        /// The user's employee number.
        /// </summary>
        [DeserializeAs("employee_number")]
        public string EmployeeNumber { get; private set; }

        /// <summary>
        /// The assignee type of this particular check out.
        /// </summary>
        /// <remarks>Currently, this field will always be User.</remarks>
        [DeserializeAs("type")]
        public AssignedToType Type { get; private set; }

        /// <inheritdoc />
        /// <remarks>Currently, this will always be <c>{CheckIn}</c>.</remarks>
        [DeserializeAs("available_actions", DeserializeAs.AvailableActions)]
        public AvailableAction AvailableActions { get; private set; }
    }
}
