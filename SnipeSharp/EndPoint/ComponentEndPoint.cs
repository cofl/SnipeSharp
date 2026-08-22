using SnipeSharp.Models;

namespace SnipeSharp.EndPoint
{
    /// <summary>
    /// Provides additional methods for the Component endpoint.
    /// </summary>
    public sealed class ComponentEndPoint : EndPoint<Component>
    {
        /// <param name="api">The Api to grab the RequestManager from.</param>
        /// <exception cref="SnipeSharp.Exceptions.MissingRequiredAttributeException">When the type parameter does not have the <see cref="PathSegmentAttribute">PathSegmentAttribute</see> attribute.</exception>
        internal ComponentEndPoint(SnipeItApi api) : base(api) { }

        /// <summary>
        /// Get the list of component assignees for a component.
        /// </summary>
        /// <param name="component">The component to get the assignee list of.</param>
        /// <returns>A ResponseCollection list of ComponentAssignees.</returns>
        public ResponseCollection<ComponentAsset> GetAssignedAssets(Component component)
            => Api.RequestManager.GetAll<ComponentAsset>($"{EndPointInfo.BaseUri}/{component.Id}/assets").RethrowExceptionIfAny().Value;

        /// <summary>
        /// Check out an accessory.
        /// </summary>
        /// <param name="request">An accessory check-out request.</param>
        /// <returns></returns>
        public RequestResponse<ApiObject> CheckOut(ComponentCheckOutRequest request)
            => Api.RequestManager.Post<ComponentCheckOutRequest, ApiObject>($"{EndPointInfo.BaseUri}/{request.Component.Id}/checkout", request).RethrowExceptionIfAny().Value;


        /// <summary>
        /// Check out a component.
        /// </summary>
        /// <param name="component">The accessory to check out.</param>
        /// <param name="assignedAsset">The user to check out to.</param>
        ///  /// <param name="quantity">How many components should be assigned to the asset.</param>
        /// <param name="note">An optional note for the checkout log.</param>
        /// <returns></returns>
        public RequestResponse<ApiObject> CheckOut(Component component, Asset assignedAsset, int quantity, string note = null)
            => CheckOut(new ComponentCheckOutRequest(component, assignedAsset, quantity) { Note = note });
    }
}
