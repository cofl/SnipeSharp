using SnipeSharp.Models;
using System.Collections.Generic;

namespace SnipeSharp.EndPoint
{
    /// <summary>
    /// Provides additional methods for the Custom Field endpoint.
    /// </summary>
    public sealed class CustomFieldEndPoint : EndPoint<CustomField>
    {
        /// <param name="api">The Api to grab the RequestManager from.</param>
        /// <exception cref="SnipeSharp.Exceptions.MissingRequiredAttributeException">When the type parameter does not have the <see cref="PathSegmentAttribute">PathSegmentAttribute</see> attribute.</exception>
        internal CustomFieldEndPoint(SnipeItApi api) : base(api) { }

        /// <summary>
        /// Associates a custom field with a custom field set.
        /// </summary>
        /// <param name="field">The field to associate.</param>
        /// <param name="fieldSet">The field set to associate to.</param>
        /// <param name="required">Is the field required?</param>
        /// <param name="order">The field's order in the set.</param>
        /// <returns>The request status.</returns>
        public RequestResponse<FieldSet> Associate(CustomField field, FieldSet fieldSet, bool required = false, int? order = null)
        {
            var obj = new CustomFieldAssociation
            {
                FieldSet = fieldSet,
                IsRequired = required
            };
            if (null != order)
                obj.Order = order.Value;
            return Api.RequestManager.Post<CustomFieldAssociation, FieldSet>($"{EndPointInfo.BaseUri}/{field.Id}/associate", obj).RethrowExceptionIfAny().Value;
        }

        /// <summary>
        /// Disassociates a custom field from a custom field set.
        /// </summary>
        /// <param name="field">The field to disassociate.</param>
        /// <param name="fieldSet">The field set to disassociate from.</param>
        /// <returns>The request status.</returns>
        public RequestResponse<FieldSet> Disassociate(CustomField field, FieldSet fieldSet)
            => Api.RequestManager.Post<CustomFieldAssociation, FieldSet>($"{EndPointInfo.BaseUri}/{field.Id}/disassociate", new CustomFieldAssociation { FieldSet = fieldSet }).RethrowExceptionIfAny().Value;

        /// <summary>
        /// Reorders the fields in a field set.
        /// </summary>
        /// <param name="fieldSet">The fieldset to reorder.</param>
        /// <param name="customFields">The fields of the field set in their new order.</param>
        /// <returns>The request status.</returns>
        public RequestResponse<ApiObject> Reorder(FieldSet fieldSet, ICollection<CustomField> customFields)
        {
            var arr = new CustomField[customFields.Count];
            customFields.CopyTo(arr, 0);
            return Reorder(fieldSet, arr);
        }

        /// <summary>
        /// Reorders the fields in a field set.
        /// </summary>
        /// <param name="fieldSet">The fieldset to reorder.</param>
        /// <param name="customFields">The fields of the field set in their new order.</param>
        /// <returns>The request status.</returns>
        public RequestResponse<ApiObject> Reorder(FieldSet fieldSet, params CustomField[] customFields)
            => Api.RequestManager.Post<CustomFieldReordering, ApiObject>($"{EndPointInfo.BaseUri}/fieldsets/{fieldSet.Id}/order", new CustomFieldReordering { Fields = customFields }).RethrowExceptionIfAny().Value;
    }
}
