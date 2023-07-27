using CustomerApi.Models;

namespace CustomerApi.Services
{
    public interface IOrderService
    {
        //Task<List<Order>> GetAsync();
        Task<IEnumerable<Order>> GetAsync();

        Task<Order?> GetAsync(string id);

        Task CreateAsync(Order newOrder);

        Task UpdateAsync(string id, Order updatedOrder);

        Task RemoveAsync(string id);
    }
}