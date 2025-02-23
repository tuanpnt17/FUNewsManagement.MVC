namespace RepositoryLayer.Data
{
    public interface IGenericRepository<T>
    {
        Task<IEnumerable<T>> ListAllAsync();

        Task<T> CreateAsync(T t);

        Task<int?> UpdateAsync(T t);

        Task<int?> DeleteAsync(T? t);
    }
}
