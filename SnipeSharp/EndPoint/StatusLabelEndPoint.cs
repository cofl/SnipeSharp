using SnipeSharp.Models;
using System;

namespace SnipeSharp.EndPoint
{
    /// <summary>
    /// Provides additional methods for the Status Label endpoint.
    /// </summary>
    public sealed class StatusLabelEndPoint : EndPoint<StatusLabel>
    {
        /// <param name="api">The Api to grab the RequestManager from.</param>
        /// <exception cref="SnipeSharp.Exceptions.MissingRequiredAttributeException">When the type parameter does not have the <see cref="PathSegmentAttribute">PathSegmentAttribute</see> attribute.</exception>
        internal StatusLabelEndPoint(SnipeItApi api) : base(api) { }

        /// <summary>
        /// Get the list of assets with a certain status label.
        /// </summary>
        /// <param name="label">A status label to look up.</param>
        /// <returns>A ResponseCollection of Assets.</returns>
        /// <exception cref="SnipeSharp.Exceptions.ApiErrorException">If there was an error accessing the API, or the status label does not exist.</exception>
        /// <exception cref="System.ArgumentNullException">If <paramref name="label"/> is null.</exception>
        public ResponseCollection<Asset> GetAssets(StatusLabel label)
        {
            if (null == label)
                throw new ArgumentNullException(paramName: nameof(label));
            return Api.RequestManager.GetAll<Asset>($"{EndPointInfo.BaseUri}/{label.Id}/assetlist").RethrowExceptionIfAny().Value;
        }

        /// <summary>
        /// Checks if a specific status label is a deployable type.
        /// </summary>
        /// <param name="label">A status label to check.</param>
        /// <returns>True if the label is a deployable type, otherwise false.</returns>
        /// <exception cref="SnipeSharp.Exceptions.ApiErrorException">If there was an error accessing the API, or the status label does not exist.</exception>
        /// <exception cref="System.ArgumentNullException">If <paramref name="label"/> is null.</exception>
        public bool IsDeployable(StatusLabel label)
        {
            if (null == label)
                throw new ArgumentNullException(paramName: nameof(label));
            return Api.RequestManager.GetRaw($"{EndPointInfo.BaseUri}/{label.Id}/deployable").Trim() == "1";
        }

        /// <summary>
        /// Convert an AssetStatus to a StatusLabel by its Id.
        /// </summary>
        /// <param name="status">The AssetStatus to convert.</param>
        /// <returns>The StatusLabel corresponding to the provided AssetStatus.</returns>
        /// <exception cref="SnipeSharp.Exceptions.ApiErrorException">If there was an error accessing the API, or the status label does not exist.</exception>
        /// <exception cref="System.ArgumentNullException">If <paramref name="status"/> is null.</exception>
        public StatusLabel FromAssetStatus(AssetStatus status)
        {
            if (null == status)
                throw new ArgumentNullException(paramName: nameof(status));
            return Get(status.StatusId);
        }
    }
}
