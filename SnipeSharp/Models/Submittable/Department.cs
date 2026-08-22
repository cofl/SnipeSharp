using SnipeSharp.EndPoint;
using SnipeSharp.Models.Enumerations;
using SnipeSharp.Serialization;
using System;
using System.Runtime.Serialization;

namespace SnipeSharp.Models
{
    /// <summary>
    /// A Department.
    /// Departments structure Users within a Company.
    /// </summary>
    [PathSegment("departments")]
    public sealed class Department : AbstractBaseModel, IAvailableActions, IPatchable
    {
        /// <summary>Create a new Department object.</summary>
        public Department() { }

        /// <summary>Create a new Department object with the supplied ID, for use with updating.</summary>
        internal Department(int id)
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

        /// <value>The url for the image of the department.</value>
        [DeserializeAs("image")]
        [SerializeAs("image")]
        [Patch(nameof(isImageUriModified))]
        public Uri ImageUri
        {
            get => imageUri;
            set
            {
                isImageUriModified = true;
                imageUri = value;
            }
        }
        private bool isImageUriModified = false;
        private Uri imageUri;

        /// <value>The company this department is a part of.</value>
        [DeserializeAs("company")]
        [SerializeAs("company_id", SerializeAs.IdValue)]
        [Patch(nameof(isCompanyModified))]
        public Stub<Company> Company
        {
            get => company;
            set
            {
                isCompanyModified = true;
                company = value;
            }
        }
        private bool isCompanyModified = false;
        private Stub<Company> company;

        /// <value>The manager of the department.</value>
        [DeserializeAs("manager")]
        [SerializeAs("manager_id", SerializeAs.IdValue)]
        [Patch(nameof(isManagerModified))]
        public StubUser Manager
        {
            get => manager;
            set
            {
                isManagerModified = true;
                manager = value;
            }
        }
        private bool isManagerModified = false;
        private StubUser manager;

        /// <value>Where this department is located.</value>
        [DeserializeAs("location")]
        [SerializeAs("location_id", SerializeAs.IdValue)]
        [Patch(nameof(isLocationModified))]
        public Stub<Location> Location
        {
            get => location;
            set
            {
                isLocationModified = true;
                location = value;
            }
        }
        private bool isLocationModified = false;
        private Stub<Location> location;

        /* Disabled here until we can also read it from the API.
         * /// <value>The description for this department.</value>
         * [Field("notes", true)]
         * public string Notes { get; set; }
         */

        /// <value>The number of users in this department.</value>
        [DeserializeAs("users_count")]
        public int UsersCount { get; private set; }

        /// <inheritdoc />
        [DeserializeAs("available_actions", DeserializeAs.AvailableActions)]
        public AvailableAction AvailableActions { get; private set; }

        void IPatchable.SetAllModifiedState(bool isModified)
        {
            isNameModified = isModified;
            isImageUriModified = isModified;
            isCompanyModified = isModified;
            isManagerModified = isModified;
            isLocationModified = isModified;
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            ((IPatchable)this).SetAllModifiedState(false);
        }
    }
}
