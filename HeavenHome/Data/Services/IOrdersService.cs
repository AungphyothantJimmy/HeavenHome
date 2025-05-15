using HeavenHome.Models;

namespace HeavenHome.Data.Services
{
    public interface IOrdersService
    {
        Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress);
        Task<List<Order>> GetOrdersByUserIdandRoleAsync(string userId, string userRole);
    }
}
