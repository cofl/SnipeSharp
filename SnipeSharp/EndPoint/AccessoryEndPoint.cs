using SnipeSharp.Models;

namespace SnipeSharp.EndPoint
{
    /// <summary>
    /// Provides additional methods for the Accessory endpoint.
    /// </summary>
    public sealed class AccessoryEndPoint : EndPoint<Accessory>
    {
        /// <param name="api">The Api to grab the RequestManager from.</param>
        /// <exception cref="SnipeSharp.Exceptions.MissingRequiredAttributeException">When the type parameter does not have the <see cref="PathSegmentAttribute">PathSegmentAttribute</see> attribute.</exception>
        internal AccessoryEndPoint(SnipeItApi api) : base(api) { }

        /// <summary>
        /// Get the list of accessory check outs for an accessory.
        /// </summary>
        /// <param name="accessory">The accessory to get the check-out list of.</param>
        /// <returns>A ResponseCollection list of AccessoryCheckOuts.</returns>
        public ResponseCollection<AccessoryCheckOut> GetCheckedOut(Accessory accessory)
            => Api.RequestManager.GetAll<AccessoryCheckOut>($"{EndPointInfo.BaseUri}/{accessory.Id}/checkedout").RethrowExceptionIfAny().Value;

        /// <summary>
        /// Check out an accessory.
        /// </summary>
        /// <param name="accessory">The accessory to check out.</param>
        /// <param name="assignedUser">The user to check out to.</param>
        /// <param name="note">An optional note for the checkout log.</param>
        /// <returns></returns>
        public RequestResponse<ApiObject> CheckOut(Accessory accessory, User assignedUser, string note = null)
            => CheckOut(new AccessoryCheckOutRequest(accessory, assignedUser) { Note = note });

        /// <summary>
        /// Check out an accessory.
        /// </summary>
        /// <param name="request">An accessory check-out request.</param>
        /// <returns></returns>
        public RequestResponse<ApiObject> CheckOut(AccessoryCheckOutRequest request)
            => Api.RequestManager.Post<AccessoryCheckOutRequest, ApiObject>($"{EndPointInfo.BaseUri}/{request.Accessory.Id}/checkout", request).RethrowExceptionIfAny().Value;

        /// <summary>
        /// Check in an accessory.
        /// </summary>
        /// <param name="accessory">An accessory checked out to a user. Checked-out accessories have unique ID numbers.</param>
        /// <param name="note">An optional note for the checkin log.</param>
        /// <returns></returns>
        public RequestResponse<ApiObject> CheckIn(Accessory accessory, string note = null)
            => CheckIn(new AccessoryCheckInRequest(accessory) { Note = note });

        /// <summary>
        /// Check in an accessory.
        /// </summary>
        /// <param name="request">An accessory check-in request.</param>
        /// <returns></returns>
        public RequestResponse<ApiObject> CheckIn(AccessoryCheckInRequest request)
            => Api.RequestManager.Post<AccessoryCheckInRequest, ApiObject>($"{EndPointInfo.BaseUri}/{request.Accessory.Id}/checkin", request).RethrowExceptionIfAny().Value;
    }
}
