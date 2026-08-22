using SnipeSharp.Exceptions;
using SnipeSharp.Filters;
using SnipeSharp.Models;
using SnipeSharp.Serialization;

namespace SnipeSharp.EndPoint
{
    /// <summary>
    /// A contract all EndPoints must follow.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEndPoint<T> where T : ApiObject
    {
        /// <summary>
        /// Create a new object of the generic type.
        /// </summary>
        /// <param name="toCreate">An instance of the generic type with serializable fields.</param>
        /// <returns>A stub of the object, indicating that the creation was successful.</returns>
        /// <exception cref="ApiErrorException">When the request was not successful.</exception>
        T Create(T toCreate);

        /// <summary>
        /// Update an existing object of the generic type.
        /// </summary>
        /// <param name="toUpdate">An instance of the generic type with serializable fields.</param>
        /// <returns>A stub of the object, indicating that the update was successful.</returns>
        /// <exception cref="ApiErrorException">When the request was not successful.</exception>
        T Update(T toUpdate);

        /// <summary>
        /// Replace an existing object of the generic type.
        /// </summary>
        /// <remarks>After this operation, if the generic type implements <see cref="IPatchable" />, all patchable fields will be marked modified.</remarks>
        /// <param name="toSet">An instance of the generic type with serializable fields.</param>
        /// <returns>A stub of the object, indicating that the update was successful.</returns>
        /// <exception cref="ApiErrorException">When the request was not successful.</exception>
        T Set(T toSet);

        /// <summary>
        /// Delete an object of the generic type by Id.
        /// </summary>
        /// <param name="id">The Id of an object of the generic type.</param>
        /// <returns>A response indicating that the deletion was successful.</returns>
        /// <exception cref="ApiErrorException">When the request was not successful.</exception>
        RequestResponse<T> Delete(int id);

        /// <summary>
        /// Get an object of the generic type by Id.
        /// </summary>
        /// <param name="id">The Id of the object to fetch from the EndPoint.</param>
        /// <returns>The object with the supplied Id.</returns>
        /// <exception cref="ApiErrorException">When the request was not successful or the object does not exist.</exception>
        T Get(int id);

        /// <summary>
        /// Get an object of the generic type by Id.
        /// </summary>
        /// <param name="id">The Id of the object to fetch from the EndPoint.</param>
        /// <returns>An optional response representing either the object with the supplied Id, or any error thrown.</returns>
        ApiOptionalResponse<T> GetOptional(int id);

        /// <summary>
        /// Get an object of the generic type by Name.
        /// </summary>
        /// <param name="name">The name of the object to fetch from the EndPoint.</param>
        /// <param name="caseSensitive">If true, perform a case-sensitive match. Default false.</param>
        /// <returns>The object with the supplied name, or null if it does not exist.</returns>
        /// <param name="filter">A filter for specifying further options; the search string will be overridden.</param>
        /// <exception cref="ApiErrorException">When the request was not successful.</exception>
        T Get(string name, bool caseSensitive = false, ISearchFilter filter = null);

        /// <summary>
        /// Get an object of the generic type by Name.
        /// </summary>
        /// <param name="name">The name of the object to fetch from the EndPoint.</param>
        /// <param name="caseSensitive">If true, perform a case-sensitive match. Default false.</param>
        /// <param name="filter">A filter for specifying further options; the search string will be overridden.</param>
        /// <returns>An optional response representing either the object with the supplied name, or any error thrown.</returns>
        ApiOptionalResponse<T> GetOptional(string name, bool caseSensitive = false, ISearchFilter filter = null);

        /// <summary>
        /// Finds all objects of the generic type matching the filter.
        /// </summary>
        /// <param name="filter">The filter to search by.</param>
        /// <returns>A collection of relevant objects, which may be empty.</returns>
        ResponseCollection<T> FindAll(ISearchFilter filter = null);

        /// <summary>
        /// Finds all objects of the generic type matching the filter.
        /// </summary>
        /// <param name="filter">The filter to search by.</param>
        /// <returns>An optional response representing either a collection of relevant objects, which may be empty, or any error thrown.</returns>
        ApiOptionalMultiResponse<T> FindAllOptional(ISearchFilter filter = null);

        /// <summary>
        /// Finds all objects of the generic type matching the search string.
        /// </summary>
        /// <param name="search">The search string to filter by.</param>
        /// <returns>A collection of relevant objects, which may be empty.</returns>
        ResponseCollection<T> FindAll(string search);

        /// <summary>
        /// Finds all objects of the generic type matching the filter.
        /// </summary>
        /// <param name="search">The search string to filter by.</param>
        /// <returns>An optional response representing either a collection of relevant objects, which may be empty, or any error thrown.</returns>
        ApiOptionalMultiResponse<T> FindAllOptional(string search);

        /// <summary>
        /// Gets all objects in the EndPoint.
        /// </summary>
        /// <returns>A collection of relevant objects, which may be empty.</returns>
        /// <remarks>It is recommended that implementations simply wrap <see cref="FindAll(ISearchFilter)"/>.</remarks>
        ResponseCollection<T> GetAll();

        /// <summary>
        /// Gets all objects in the EndPoint.
        /// </summary>
        /// <returns>An optional response representing a collection of relevant objects, which may be empty, or any error thrown.</returns>
        ApiOptionalMultiResponse<T> GetAllOptional();

        /// <summary>
        /// Finds the first object of the generic type matching the filter.
        /// </summary>
        /// <param name="filter">The filter to search by. The limit will be forced to 1.</param>
        /// <returns>The relevant object, or null if no object was found matching the filter.</returns>
        T FindOne(ISearchFilter filter);

        /// <summary>
        /// Finds the first object of the generic type matching the filter.
        /// </summary>
        /// <param name="filter">The filter to search by. The limit will be forced to 1.</param>
        /// <returns>An optional response representing the relevant object (null if no object was found matching the filter), or any error thrown.</returns>
        ApiOptionalResponse<T> FindOneOptional(ISearchFilter filter);

        /// <summary>
        /// Finds the first object of the generic type matching the search string.
        /// </summary>
        /// <param name="search">The search string to filter by.</param>
        /// <returns>The relevant object, or null if no object was found matching the filter.</returns>
        T FindOne(string search);

        /// <summary>
        /// Finds the first object of the generic type matching the filter.
        /// </summary>
        /// <param name="search">The search string to filter by.</param>
        /// <returns>An optional response representing the relevant object (null if no object was found matching the filter), or any error thrown.</returns>
        ApiOptionalResponse<T> FindOneOptional(string search);

        /// <value>Gets an object by Id.</value>
        /// <seealso cref="Get(int)" />
        T this[int id] { get; }

        /// <value>Gets an object by name.</value>
        /// <seealso cref="Get(string,bool,ISearchFilter)" />
        T this[string name, bool caseSensitive = false, ISearchFilter filter = null] { get; }
    }
}
