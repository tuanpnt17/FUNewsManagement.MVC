namespace RepositoryLayer.Data
{
    public interface IGenericRepository<T>
    {
        Task<IEnumerable<T>> ListAllAsync();

        Task<T> CreateAccountAsync(T t);

        Task<int?> UpdateAccountAsync(T t);

        Task<int?> DeleteAccountAsync(T? t);
    }
}
