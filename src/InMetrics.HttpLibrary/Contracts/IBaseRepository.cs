namespace InMetrics.HttpLibrary.Contracts
{
    /// <summary>
    /// Design pattern: Façade, repository
    /// It could be used as a template
    /// </summary>
    /// <typeparam name="T"><see cref="T"/></typeparam>
    public interface IBaseRepository<T> where T : class
    {
        /// <summary>
        /// Request just one register
        /// </summary>
        /// <param name="url">The full EndPoint</param>
        /// <param name="id">The id</param>
        /// <returns>Fulfilled object</returns>
        Task<T> Get(string url, string id);
        Task<IList<T>> Get(string url);
        Task<IList<T>> Get(string url, T obj);
        Task<bool> Create(string url, T obj);
        Task<T> CreateReturnObj(string url, T obj);
        Task<bool> Update(string url, T obj);
        Task<bool> Delete(string url, string id);
    }
}
