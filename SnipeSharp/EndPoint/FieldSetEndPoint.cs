using SnipeSharp.Models;

namespace SnipeSharp.EndPoint
{
    /// <summary>
    /// Provides additional methods for the Field Set endpoint.
    /// </summary>
    public sealed class FieldSetEndPoint : EndPoint<FieldSet>
    {
        /// <param name="api">The Api to grab the RequestManager from.</param>
        /// <exception cref="SnipeSharp.Exceptions.MissingRequiredAttributeException">When the type parameter does not have the <see cref="PathSegmentAttribute">PathSegmentAttribute</see> attribute.</exception>
        internal FieldSetEndPoint(SnipeItApi api) : base(api) { }

        /// <summary>
        /// Retrieve the fields of a fieldset.
        /// </summary>
        /// <param name="fieldSet">The fieldset to retrieve fields from.</param>
        /// <returns>A response collection with the request status and fields.</returns>
        public ResponseCollection<CustomField> GetFields(FieldSet fieldSet)
            => Api.RequestManager.GetAll<CustomField>($"{EndPointInfo.BaseUri}/{fieldSet.Id}/fields").RethrowExceptionIfAny().Value;

        /// <summary>
        /// Retrieve the fields of a fieldset with the default values of the fields for a given model.
        /// </summary>
        /// <param name="model">The model to retrieve fields with default values for.</param>
        /// <returns>A response collection with the request status and fields with default values.</returns>
        public ResponseCollection<CustomField> GetFieldsWithDefaults(Model model)
            => Api.RequestManager.GetAll<CustomField>($"{EndPointInfo.BaseUri}/{model.FieldSet.Id}/fields/{model.Id}").RethrowExceptionIfAny().Value;
    }
}
