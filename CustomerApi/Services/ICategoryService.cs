using CustomerApi.Models;

namespace CustomerApi.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAsync();

        Task<Category?> GetAsync(string id);

        Task CreateAsync(Category newCategory);

        Task UpdateAsync(string id, Category updatedCategory);

        Task RemoveAsync(string id);
    }
}