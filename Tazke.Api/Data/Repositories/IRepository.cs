namespace Tazke.Api.Data.Repositories;

public interface IRepository<T>
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task<T> CreateAsync(T product);
    Task<T> UpdateAsync(T product);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}
