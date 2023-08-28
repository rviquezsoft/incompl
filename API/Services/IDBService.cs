namespace API.Services
{
    public interface IDBService<T> where T : class
    {
        Task<List<T>> Get();
        Task<string> Patch(T entity);
        Task<string> Post(T entity);
    }
}