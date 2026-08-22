using SnipeSharp.Exceptions;
using SnipeSharp.Filters;
using SnipeSharp.Models;

namespace SnipeSharp.EndPoint
{
    /// <summary>
    /// Provides additional methods for the User endpoint.
    /// </summary>
    public sealed class UserEndPoint : EndPoint<User>
    {
        /// <param name="api">The Api to grab the RequestManager from.</param>
        /// <exception cref="SnipeSharp.Exceptions.MissingRequiredAttributeException">When the type parameter does not have the <see cref="PathSegmentAttribute">PathSegmentAttribute</see> attribute.</exception>
        internal UserEndPoint(SnipeItApi api) : base(api) { }

        /// <summary>
        /// Get the assets assigned to a user.
        /// </summary>
        /// <remarks>The Asset objects returned by this function will not have their assignee data.</remarks>
        /// <param name="user">The user to get the assigned assets of.</param>
        /// <returns>A ResponseCollection list of Assets without their assignee data.</returns>
        public ResponseCollection<Asset> GetAssignedAssets(User user)
            => Api.RequestManager.GetAll<Asset>($"{EndPointInfo.BaseUri}/{user.Id}/assets").RethrowExceptionIfAny().Value;

        /// <summary>
        /// Get the current user of the API.
        /// </summary>
        /// <returns>An optional response representing the user information for the current user of the API, or any error thrown.</returns>
        public ApiOptionalResponse<SelfUser> MeOptional()
            => Api.RequestManager.Get<SelfUser>($"{EndPointInfo.BaseUri}/me");

        /// <summary>
        /// Get the current user of the API.
        /// </summary>
        /// <returns>The user information for the current user of the API.</returns>
        /// <exception cref="SnipeSharp.Exceptions.ApiErrorException">If there was an error accessing the API.</exception>
        public SelfUser Me()
            => MeOptional().RethrowExceptionIfAny().Value;

        /// <summary>
        /// Gets a user by their username.
        /// </summary>
        /// <param name="username">The username of the user to search for.</param>
        /// <param name="filter">The search filter to use (the username will override the search string)</param>
        /// <returns>The user information for the user with the provided username.</returns>
        /// <exception cref="SnipeSharp.Exceptions.ApiErrorException">If there was an error accessing the API or the user was not found.</exception>
        public User GetByUserName(string username, UserSearchFilter filter = null)
            => GetByUserNameOptional(username, filter).RethrowExceptionIfAny().Value;

        /// <summary>
        /// Gets a user by their email address.
        /// </summary>
        /// <param name="email">The email address of the user to search for.</param>
        /// <param name="filter">The search filter to use (the email will override the search string)</param>
        /// <returns>The user information for the user with the provided email address.</returns>
        /// <exception cref="SnipeSharp.Exceptions.ApiErrorException">If there was an error accessing the API or the user was not found.</exception>
        public User GetByEmailAddress(string email, UserSearchFilter filter = null)
            => GetByEmailAddressOptional(email, filter).RethrowExceptionIfAny().Value;

        /// <summary>
        /// Gets a user by their username, but do not throw any errors.
        /// </summary>
        /// <param name="username">The username of the user to search for.</param>
        /// <param name="filter">The search filter to use (the username will override the search string)</param>
        /// <returns>An optional response containing the user (if it was found), and any error (if there was one).</returns>
        public ApiOptionalResponse<User> GetByUserNameOptional(string username, UserSearchFilter filter = null)
        {
            filter = filter ?? new UserSearchFilter();
            filter.Search = username;
            var results = FindAllOptional(filter);
            if (!results.HasValue)
                return new ApiOptionalResponse<User> { Exception = results.Exception };
            foreach (var user in results.Value)
                if (user.UserName == username)
                    return new ApiOptionalResponse<User> { Value = user };
            return new ApiOptionalResponse<User> { Exception = new ApiErrorException($"No user was found by the username \"{username}\".") };
        }

        /// <summary>
        /// Gets a user by their email address, but do not throw any errors.
        /// </summary>
        /// <param name="email">The email address of the user to search for.</param>
        /// <param name="filter">The search filter to use (the email will override the search string)</param>
        /// <returns>An optional response containing the user (if it was found), and any error (if there was one).</returns>
        public ApiOptionalResponse<User> GetByEmailAddressOptional(string email, UserSearchFilter filter = null)
        {
            filter = filter ?? new UserSearchFilter();
            filter.Email = email;
            var results = FindAllOptional(filter);
            if (!results.HasValue)
                return new ApiOptionalResponse<User> { Exception = results.Exception };
            foreach (var user in results.Value)
                if (user.EmailAddress == email)
                    return new ApiOptionalResponse<User> { Value = user };
            return new ApiOptionalResponse<User> { Exception = new ApiErrorException($"No user was found by the email address \"{email}\".") };
        }

        /// <summary>
        /// Get the accessories checked out by a user.
        /// </summary>
        /// <param name="user">The user to get the checked-out accessroies of.</param>
        /// <returns>A ResponseCollection list of Accessories.</returns>
        public ResponseCollection<Accessory> GetAssignedAccessories(User user)
            => Api.RequestManager.GetAll<Accessory>($"{EndPointInfo.BaseUri}/{user.Id}/accessories").RethrowExceptionIfAny().Value;
    }
}
