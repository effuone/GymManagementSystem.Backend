namespace GMS.Core.Interfaces
{
    public interface IAsyncRepository<T> where T:class
    {
        Task<T> GetAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<int> CreateAsync(T model);
        Task UpdateAsync(int id, T model);
        Task DeleteAsync(int id);
    }
}