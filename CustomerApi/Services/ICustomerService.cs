using CustomerApi.Models;

namespace CustomerApi.Services
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetAsync();

        Task<Customer?> GetAsync(string id);

        Task CreateAsync(Customer newCustomer);

        Task UpdateAsync(string id, Customer updatedCustomer);

        Task RemoveAsync(string id);
    }
}