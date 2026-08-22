using SnipeSharp.EndPoint;
using SnipeSharp.Models.Enumerations;
using SnipeSharp.Serialization;
using System;
using System.Runtime.Serialization;

namespace SnipeSharp.Models
{
    /// <summary>
    /// A Status label.
    /// Status labels are used out to organize Assets and manage their state.
    /// </summary>
    [PathSegment("statuslabels")]
    public sealed class StatusLabel : AbstractBaseModel, IAvailableActions, IPatchable
    {
        /// <summary>Create a new StatusLabel object.</summary>
        public StatusLabel() { }

        /// <summary>Create a new StatusLabel object with the supplied ID, for use with updating.</summary>
        internal StatusLabel(int id)
        {
            Id = id;
        }

        /// <inheritdoc />
        /// <remarks>This field is required and must have a unique value.</remarks>
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

        /// <value>Indicates the type of label; valid values are <see cref="StatusType.Pending"/>, <see cref="StatusType.Deployable"/>, <see cref="StatusType.Undeployable"/> and <see cref="StatusType.Archived"/></value>
        [DeserializeAs("type")]
        [SerializeAs("type", IsRequired = true)]
        [Patch(nameof(isTypeModified))]
        public StatusType? Type
        {
            get => type;
            set
            {
                isTypeModified = true;
                type = value ?? StatusType.Undeployable;
            }
        }
        private bool isTypeModified = false;
        private StatusType type;

        /// <value>Gets if this label is of the Deployable type or not.</value>
        public bool IsDeployable => type == StatusType.Deployable;

        /// <value>Gets if this label is of the Archived type or not.</value>
        public bool IsArchived => type == StatusType.Archived;

        /// <value>Gets if this label is of the Pending type or not.</value>
        public bool IsPending => type == StatusType.Pending;

        /// <value>The color of the lable in the web navigation.</value>
        [DeserializeAs("color")]
        public string Color { get; private set; }

        /// <value>Whether or not this label shows in the web navigation.</value>
        [DeserializeAs("show_in_nav")]
        public bool? ShouldShowInNav { get; private set; }

        /// <value>Whether or not this label is a default label.</value>
        [DeserializeAs("default_lable")]
        public bool? IsDefaultLabel { get; private set; }

        /// <value>The number of assets with this label.</value>
        [DeserializeAs("assets_count")]
        public int? AssetsCount { get; private set; }

        /// <value>Description of the label.</value>
        [DeserializeAs("notes")]
        [SerializeAs("notes")]
        [Patch(nameof(isNotesModified))]
        public string Notes
        {
            get => notes;
            set
            {
                isNotesModified = true;
                notes = value;
            }
        }
        private bool isNotesModified = false;
        private string notes;

        /// <inheritdoc />
        [DeserializeAs("available_actions", DeserializeAs.AvailableActions)]
        public AvailableAction AvailableActions { get; private set; }

        /// <summary>Converts this label into an AssetStatus, for use with Assets.</summary>
        public AssetStatus ToAssetStatus()
            => new AssetStatus { StatusId = Id, Name = Name, StatusType = Type };

        void IPatchable.SetAllModifiedState(bool isModified)
        {
            isNameModified = isModified;
            isTypeModified = isModified;
            isNotesModified = isModified;
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            ((IPatchable)this).SetAllModifiedState(false);
        }

        public static implicit operator StatusLabel(string v)
        {
            throw new NotImplementedException();
        }
    }
}
