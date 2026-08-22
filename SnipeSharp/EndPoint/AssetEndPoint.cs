using SnipeSharp.Models;
using System;

namespace SnipeSharp.EndPoint
{
    /// <summary>
    /// Provides additional methods for the Asset endpoint.
    /// </summary>
    public sealed class AssetEndPoint : EndPoint<Asset>
    {
        /// <param name="api">The Api to grab the RequestManager from.</param>
        /// <exception cref="SnipeSharp.Exceptions.MissingRequiredAttributeException">When the type parameter does not have the <see cref="PathSegmentAttribute">PathSegmentAttribute</see> attribute.</exception>
        internal AssetEndPoint(SnipeItApi api) : base(api) { }

        /// <summary>
        /// Check out an asset.
        /// </summary>
        /// <param name="request">An asset check-out request.</param>
        /// <returns></returns>
        public RequestResponse<ApiObject> CheckOut(AssetCheckOutRequest request)
            => Api.RequestManager.Post<AssetCheckOutRequest, ApiObject>($"{EndPointInfo.BaseUri}/{request.Asset.Id}/checkout", request).RethrowExceptionIfAny().Value;

        /// <summary>
        /// Check in an asset with an optional message.
        /// </summary>
        /// <param name="asset">An asset to check in.</param>
        /// <param name="note">An optional message for the check-in log.</param>
        /// <returns></returns>
        public RequestResponse<ApiObject> CheckIn(Asset asset, string note = null)
            => CheckIn(new AssetCheckInRequest(asset)
            {
                Note = note
            });

        /// <summary>
        /// Check in an asset.
        /// </summary>
        /// <param name="request">An asset check-in request.</param>
        /// <returns></returns>
        public RequestResponse<ApiObject> CheckIn(AssetCheckInRequest request)
            => Api.RequestManager.Post<AssetCheckInRequest, ApiObject>($"{EndPointInfo.BaseUri}/{request.Asset.Id}/checkin", request).RethrowExceptionIfAny().Value;

        /// <summary>
        /// Marks an asset as audited.
        /// </summary>
        /// <param name="asset">An asset to audit.</param>
        /// <param name="location">The location of the audit.</param>
        /// <param name="nextAuditDate">The date of the next audit.</param>
        /// <param name="notes">A note for the audit log.</param>
        /// <returns>An <see cref="AssetAudit"/> with some fields missing, and the request status.</returns>
        public RequestResponse<AssetAudit> Audit(Asset asset, Location location = null, DateTime? nextAuditDate = null, string notes = null)
        {
            var audit = new AssetAudit
            {
                Asset = asset,
                Location = location,
                NextAuditDate = nextAuditDate,
                Note = notes
            };
            return Api.RequestManager.Post<AssetAudit>($"{EndPointInfo.BaseUri}/audit", audit).RethrowExceptionIfAny().Value;
        }

        /// <summary>
        /// Retrieve an asset by its tag.
        /// </summary>
        /// <param name="tag">An asset tag.</param>
        /// <returns>The asset with the corresponding asset tag.</returns>
        /// <exception cref="SnipeSharp.Exceptions.ApiErrorException">If no asset with that tag could be found, or there was another error with the API.</exception>
        public Asset GetByTag(string tag)
            => GetByTagOptional(tag).RethrowExceptionIfAny().Value;

        /// <summary>
        /// Retrieve an asset by its tag, but do not throw any errors.
        /// </summary>
        /// <param name="tag">An asset tag.</param>
        /// <returns>A tuple containing the asset (if it was found), and any error (if there was one).</returns>
        public ApiOptionalResponse<Asset> GetByTagOptional(string tag)
            => Api.RequestManager.Get<Asset>($"{EndPointInfo.BaseUri}/bytag/{tag}");

        /// <summary>
        /// Retrieve assets by serial number.
        /// </summary>
        /// <param name="serial">A serial number.</param>
        /// <returns>The assets with the corresponding serial number.</returns>
        /// <exception cref="SnipeSharp.Exceptions.ApiErrorException">If no asset with that serial could be found, or there was another error with the API.</exception>
        public ResponseCollection<Asset> FindBySerial(string serial)
            => FindBySerialOptional(serial).RethrowExceptionIfAny().Value;

        /// <summary>
        /// Retrieve an asset by its serial, alongside any error
        /// </summary>
        /// <param name="serial">A serial number.</param>
        /// <returns>An optional response containing the asset (if it was found), and any error (if there was one).</returns>
        public ApiOptionalMultiResponse<Asset> FindBySerialOptional(string serial)
            => Api.RequestManager.Get<ResponseCollection<Asset>>($"{EndPointInfo.BaseUri}/byserial/{serial}");
    }
}
