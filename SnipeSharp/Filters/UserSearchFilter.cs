using SnipeSharp.Models;
using SnipeSharp.Serialization;
using System;

namespace SnipeSharp.Filters
{
    /// <summary>
    /// A filter for users, featuring user-only search fields.
    /// </summary>
    public sealed class UserSearchFilter : ISortableSearchFilter<UserSearchColumn?>
    {
        /// <inheritdoc />
        [SerializeAs("limit")]
        public int? Limit { get; set; }

        /// <inheritdoc />
        [SerializeAs("offset")]
        public int? Offset { get; set; }

        /// <inheritdoc />
        [SerializeAs("search")]
        public string Search { get; set; }

        /// <inheritdoc />
        [SerializeAs("username")]
        public string Username { get; set; }

        /// <inheritdoc />
        [SerializeAs("email")]
        public string Email { get; set; }

        /// <inheritdoc />
        [SerializeAs("sort")]
        public UserSearchColumn? SortColumn { get; set; }

        /// <inheritdoc />
        [SerializeAs("order")]
        public SearchOrder? Order { get; set; } = null;

        /// <summary>
        /// Search only deleted users.
        /// </summary>
        [SerializeAs("deleted")]
        public bool? Deleted { get; set; }

        /// <summary>
        /// Only search for users that work for this company.
        /// </summary>
        [SerializeAs("company_id", SerializeAs.IdValue)]
        public Company Company { get; set; }

        /// <summary>
        /// Only search for users at this location.
        /// </summary>
        [SerializeAs("location_id", SerializeAs.IdValue)]
        public Location Location { get; set; }

        /// <summary>
        /// Only search for users in this group.
        /// </summary>
        [SerializeAs("group_id", SerializeAs.IdValue)]
        public Group Group { get; set; }

        /// <summary>
        /// Only search for users in this department.
        /// </summary>
        [SerializeAs("department_id", SerializeAs.IdValue)]
        public Department Department { get; set; }

        /// <summary>
        /// Only search for users with the ldap_import
        /// </summary>
        public bool? LdapImport { private get; set; } = null;

        /// <summary>
        /// Only search for users with the ldap_import
        /// </summary>
        [SerializeAs("ldap_import")]
        public int? _ldapImport
        {
            get
            {
                int? returnVal = null;
                if (LdapImport != null)
                    returnVal = Convert.ToInt32(LdapImport);

                return returnVal;
            }
        }


        /// <summary>
        /// Initialize a new instance of the UserSearchFilter class.
        /// </summary>
        public UserSearchFilter()
        {
        }

        /// <summary>
        /// Initialize a new instance of the UserSearchFilter class with the specified search string.
        /// </summary>
        public UserSearchFilter(string searchString)
        {
            Search = searchString;
        }
    }
}
