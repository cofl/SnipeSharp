using SnipeSharp.EndPoint;
using SnipeSharp.Serialization;

namespace SnipeSharp.Models
{
    /// <summary>
    /// A user, specifically one retrieved from /users/me, because for some reason
    /// that endpoint doesn't transform its user object. We don't deserialize
    /// all fields it returns on purpose, because the correct way to get the current
    /// user's information is <code>Api.Users.Get(Api.Users.Me().Id)</code>.
    /// </summary>
    [PathSegment("users")]
    public sealed class SelfUser : ApiObject
    {
        /// <summary>Create a new SelfUser object.</summary>
        public SelfUser() { }

        /// <value>The internal Id of the object.</value>
        [DeserializeAs("id")]
        public int Id { get; private set; }

        /// <summary>Gets the user's name.</summary>
        public string Name => $"{FirstName} {LastName}";

        /// <value>Gets/sets the user's first name.</value>
        /// <remarks>This field is required.</remarks>
        [DeserializeAs("first_name")]
        public string FirstName { get; private set; }

        /// <value>Gets/sets the user's last name.</value>
        [DeserializeAs("last_name")]
        public string LastName { get; private set; }

        /// <value>Gets/sets the user's username.</value>
        /// <remarks>This field is required.</remarks>
        [DeserializeAs("username")]
        public string UserName { get; private set; }

        /// <value>Gets/sets the user's email address.</value>
        [DeserializeAs("email")]
        public string EmailAddress { get; private set; }
    }
}
