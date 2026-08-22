using SnipeSharp.EndPoint;
using SnipeSharp.Models.Enumerations;
using SnipeSharp.Serialization;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SnipeSharp.Models
{
    /// <summary>
    /// A user.
    /// </summary>
    [PathSegment("users")]
    public sealed class User : AbstractBaseModel, IAvailableActions, IPatchable
    {
        /// <summary>Create a new User object.</summary>
        public User() { }

        /// <summary>Create a new User object with the supplied ID, for use with updating.</summary>
        internal User(int id)
        {
            Id = id;
        }

        /// <value>The URL of the user's gravatar.</value>
        [DeserializeAs("avatar")]
        [Patch(nameof(isAvatarUrlModified))]
        public Uri AvatarUrl
        {
            get => avatarUrl;
            set
            {
                isAvatarUrlModified = true;
                avatarUrl = value;
            }
        }
        private bool isAvatarUrlModified = false;
        private Uri avatarUrl;

        /// <value>Gets the user's name.</value>
        /// <remarks>This field cannot be used to set a user's name; it is constructed from the <see cref="FirstName"/> and <see cref="LastName"/> by the API.</remarks>
        [DeserializeAs("name")]
        public override string Name
        {
            get => base.Name;
            set => base.Name = value;
        }

        /// <value>Gets/sets the user's first name.</value>
        /// <remarks>This field is required.</remarks>
        [DeserializeAs("first_name")]
        [SerializeAs("first_name", IsRequired = true)]
        [Patch(nameof(isFirstNameModified))]
        public string FirstName
        {
            get => firstName;
            set
            {
                isFirstNameModified = true;
                firstName = value;
            }
        }
        private bool isFirstNameModified = false;
        private string firstName;

        /// <value>Gets/sets the user's last name.</value>
        [DeserializeAs("last_name")]
        [SerializeAs("last_name")]
        [Patch(nameof(isLastNameModified))]
        public string LastName
        {
            get => lastName;
            set
            {
                isLastNameModified = true;
                lastName = value;
            }
        }
        private bool isLastNameModified = false;
        private string lastName;

        /// <value>Gets/sets the user's username.</value>
        /// <remarks>This field is required.</remarks>
        [DeserializeAs("username")]
        [SerializeAs("username", IsRequired = true)]
        [Patch(nameof(isUserNameModified))]
        public string UserName
        {
            get => userName;
            set
            {
                isUserNameModified = true;
                userName = value;
            }
        }
        private bool isUserNameModified = false;
        private string userName;

        /// <value>Sets the user's password.</value>
        /// <remarks>This field is required.</remarks>
        [SerializeAs("password", IsRequired = true)]
        [Patch(nameof(isPasswordModified))]
        public string Password
        {
            private get => password;
            set
            {
                isPasswordModified = true;
                password = value;
            }
        }
        private bool isPasswordModified = false;
        private string password;

        /// <summary>Confirms the user's password.</summary>
        /// <remarks>Must match <see cref="Password"/>.</remarks>
        [SerializeAs("password_confirmation", IsRequired = true)]
        [Patch(nameof(isPasswordConfirmationModified))]
        public string PasswordConfirmation
        {
            private get => passwordConfirmation;
            set
            {
                isPasswordConfirmationModified = true;
                passwordConfirmation = value;
            }
        }
        private bool isPasswordConfirmationModified = false;
        private string passwordConfirmation;

        /// <value>Gets/sets the user's employee number.</value>
        [DeserializeAs("employee_num")]
        [SerializeAs("employee_num")]
        [Patch(nameof(isEmployeeNumberModified))]
        public string EmployeeNumber
        {
            get => employeeNumber;
            set
            {
                isEmployeeNumberModified = true;
                employeeNumber = value;
            }
        }
        private bool isEmployeeNumberModified = false;
        private string employeeNumber;

        /// <value>Gets/sets the user's manager.</value>
        [DeserializeAs("manager")]
        [SerializeAs("manager_id", SerializeAs.IdValue)]
        [Patch(nameof(isManagerModified))]
        public Stub<User> Manager
        {
            get => manager;
            set
            {
                isManagerModified = true;
                manager = value;
            }
        }
        private bool isManagerModified = false;
        private Stub<User> manager;

        /// <value>Gets/sets the title of the user's job.</value>
        [DeserializeAs("jobtitle")]
        [SerializeAs("jobtitle")]
        [Patch(nameof(isJobTitleModified))]
        public string JobTitle
        {
            get => jobTitle;
            set
            {
                isJobTitleModified = true;
                jobTitle = value;
            }
        }
        private bool isJobTitleModified = false;
        private string jobTitle;

        /// <value>Gets/sets the user's phone number.</value>
        [DeserializeAs("phone")]
        [SerializeAs("phone")]
        [Patch(nameof(isPhoneNumberModified))]
        public string PhoneNumber
        {
            get => phoneNumber;
            set
            {
                isPhoneNumberModified = true;
                phoneNumber = value;
            }
        }
        private bool isPhoneNumberModified = false;
        private string phoneNumber;

        /// <value>Gets/sets the user's address.</value>
        [DeserializeAs("address")]
        [SerializeAs("address")]
        [Patch(nameof(isAddressModified))]
        public string Address
        {
            get => address;
            set
            {
                isAddressModified = true;
                address = value;
            }
        }
        private bool isAddressModified = false;
        private string address;

        /// <value>Gets/sets the city of the user's address.</value>
        [DeserializeAs("city")]
        [SerializeAs("city")]
        [Patch(nameof(isCityModified))]
        public string City
        {
            get => city;
            set
            {
                isCityModified = true;
                city = value;
            }
        }
        private bool isCityModified = false;
        private string city;

        /// <value>Gets/sets the state of the user's address.</value>
        [DeserializeAs("state")]
        [SerializeAs("state")]
        [Patch(nameof(isStateModified))]
        public string State
        {
            get => state;
            set
            {
                isStateModified = true;
                state = value;
            }
        }
        private bool isStateModified = false;
        private string state;

        /// <value>Gets/sets the country of the user's address.</value>
        [DeserializeAs("country")]
        [SerializeAs("country")]
        [Patch(nameof(isCountryModified))]
        public string Country
        {
            get => country;
            set
            {
                isCountryModified = true;
                country = value;
            }
        }
        private bool isCountryModified = false;
        private string country;

        /// <value>Gets/sets the zip code of the user's address.</value>
        [DeserializeAs("zip")]
        [SerializeAs("zip")]
        [Patch(nameof(isZipCodeModified))]
        public string ZipCode
        {
            get => zipCode;
            set
            {
                isZipCodeModified = true;
                zipCode = value;
            }
        }
        private bool isZipCodeModified = false;
        private string zipCode;

        /// <value>Gets/sets the user's email address.</value>
        [DeserializeAs("email")]
        [SerializeAs("email")]
        [Patch(nameof(isEmailAddressModified))]
        public string EmailAddress
        {
            get => emailAddress;
            set
            {
                isEmailAddressModified = true;
                emailAddress = value;
            }
        }
        private bool isEmailAddressModified = false;
        private string emailAddress;

        /// <value>Gets/sets the user's department.</value>
        [DeserializeAs("department")]
        [SerializeAs("department_id", SerializeAs.IdValue)]
        [Patch(nameof(isDepartmentModified))]
        public Stub<Department> Department
        {
            get => department;
            set
            {
                isDepartmentModified = true;
                department = value;
            }
        }
        private bool isDepartmentModified = false;
        private Stub<Department> department;

        /// <value>Gets/sets the user's location.</value>
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

        /// <value>Gets the user's notes or description.</value>
        /// <remarks>Currently, this field cannot be set.</remarks>
        [DeserializeAs("notes")]
        [Patch(nameof(isNotesModified))]
        public string Notes
        {
            get => notes;
            private set
            {
                isNotesModified = true;
                notes = value;
            }
        }
        private bool isNotesModified = false;
        private string notes;

        /// <value>Gets the user's permissions.</value>
        [DeserializeAs("permissions", DeserializeAs.PermissionDictionary)]
        public Dictionary<string, bool> Permissions { get; private set; }

        /// <value>Gets/sets if this user has been activated.</value>
        [DeserializeAs("activated")]
        [SerializeAs("activated")]
        [Patch(nameof(isIsActivatedModified))]
        public bool? IsActivated
        {
            get => isActivated;
            set
            {
                isIsActivatedModified = true;
                isActivated = value;
            }
        }
        private bool isIsActivatedModified = false;
        private bool? isActivated;

        /// <value>Gets if the user has activated two-factor authentication.</value>
        [DeserializeAs("two_factor_activated")]
        public bool? IsTwoFactorActivated { get; private set; }

        /// <value>Gets the number of assets assigned to this user.</value>
        [DeserializeAs("assets_count")]
        public int? AssetsCount { get; private set; }

        /// <value>Gets the number of licenses assigned to this user.</value>
        [DeserializeAs("licenses_count")]
        public int? LicensesCount { get; private set; }

        /// <value>Gets the number of accessories assigned to this user.</value>
        [DeserializeAs("accessories_count")]
        public int? AccessoriesCount { get; private set; }

        /// <value>Gets the number of consumables assigned to this user.</value>
        [DeserializeAs("consumables_count")]
        public int? ConsumablesCount { get; private set; }

        /// <value>Gets/sets the company this user works for.</value>
        /// <remarks>
        /// <para>This field will be converted to the value of its Id when serialized.</para>
        /// <para>When deserialized, this value does not have all properties filled. Fetch the value using the relevant endpoint to gather the rest of the information.</para>
        /// </remarks>
        [DeserializeAs("company")]
        [SerializeAs("company_id", SerializeAs.IdValue)]
        [Patch(nameof(isCompanyModified))]
        public Company Company
        {
            get => company;
            set
            {
                isCompanyModified = true;
                company = value;
            }
        }
        private bool isCompanyModified = false;
        private Company company;

        /// <value>Gets the date this user last logged on.</value>
        [DeserializeAs("last_login", DeserializeAs.Timestamp)]
        public DateTime? LastLogin { get; private set; }

        /// <inheritdoc />
        [DeserializeAs("available_actions", DeserializeAs.AvailableActions)]
        public AvailableAction AvailableActions { get; private set; }

        /// <value>Gets the groups this user is a member of.</value>
        [DeserializeAs("groups")]
        [SerializeAs("groups", SerializeAs.IdValueArray)]
        [Patch(nameof(isGroupsModified))]
        public ResponseCollection<Group> Groups
        {
            get => groups;
            set
            {
                isGroupsModified = true;
                groups = value;
            }
        }
        private bool isGroupsModified = false;
        private ResponseCollection<Group> groups;

        void IPatchable.SetAllModifiedState(bool isModified)
        {
            isAvatarUrlModified = isModified;
            isFirstNameModified = isModified;
            isLastNameModified = isModified;
            isUserNameModified = isModified;
            isPasswordModified = isModified;
            isPasswordConfirmationModified = isModified;
            isEmployeeNumberModified = isModified;
            isManagerModified = isModified;
            isJobTitleModified = isModified;
            isPhoneNumberModified = isModified;
            isAddressModified = isModified;
            isCityModified = isModified;
            isStateModified = isModified;
            isCountryModified = isModified;
            isZipCodeModified = isModified;
            isEmailAddressModified = isModified;
            isDepartmentModified = isModified;
            isLocationModified = isModified;
            isNotesModified = isModified;
            isIsActivatedModified = isModified;
            isCompanyModified = isModified;
            isGroupsModified = isModified;
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            ((IPatchable)this).SetAllModifiedState(false);
        }
    }
}
