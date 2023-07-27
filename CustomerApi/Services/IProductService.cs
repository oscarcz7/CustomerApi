using CustomerApi.Models;

namespace CustomerApi.Services
{
    public interface IProductService
    {
        //Task<List<Product>> GetAsync();
        Task<IEnumerable<Product>> GetAsync();

        Task<Product?> GetById(string id);

        Task CreateAsync(Product newProduct);

        Task UpdateAsync(string id, Product updatedProduct);

        Task RemoveAsync(string id);

    }
}