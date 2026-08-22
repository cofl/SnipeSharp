using SnipeSharp.EndPoint;
using SnipeSharp.Models.Enumerations;
using SnipeSharp.Serialization;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SnipeSharp.Models
{
    /// <summary>
    /// A group.
    /// Groups are used to grant permissions to users in Snipe-IT.
    /// </summary>
    [PathSegment("groups")]
    public sealed class Group : AbstractBaseModel, IAvailableActions, IPatchable
    {
        /// <summary>Create a new Group object.</summary>
        public Group() { }

        /// <summary>Create a new Group object with the supplied ID, for use with updating.</summary>
        internal Group(int id)
        {
            Id = id;
        }

        /// <inheritdoc />
        /// <remarks>This field is required.</remarks>
        [DeserializeAs("name")]
        [SerializeAs("name", IsRequired = true)]
        [Patch(nameof(isNameModified))]
        public override string Name
        {
            get => name;
            set
            {
                isNameModified = true;
                name = value;
            }
        }
        private bool isNameModified = false;
        private string name;

        /// <inheritdoc />
        // TODO: change this to a more explicit "GroupPermissions" type and make it required (I think it may need to be required)
        [DeserializeAs("permissions", DeserializeAs.PermissionDictionary)]
        public Dictionary<string, bool> Permissions { get; private set; }

        /// <inheritdoc />
        [DeserializeAs("users_count")]
        public int? UsersCount { get; private set; }

        /// <inheritdoc />
        [DeserializeAs("available_actions", DeserializeAs.AvailableActions)]
        public AvailableAction AvailableActions { get; private set; }

        void IPatchable.SetAllModifiedState(bool isModified)
        {
            isNameModified = isModified;
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            ((IPatchable)this).SetAllModifiedState(false);
        }
    }
}
