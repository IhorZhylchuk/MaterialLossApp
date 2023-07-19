
using MaterialLossApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace MaterialLossApp.Interfaces
{
    public interface IDefaultInterface
    {
        Task<IActionResult> CreateOrderAsync(Item item);
        Task<IEnumerable<Item>> GetAllOrdersAsync();
        Task<Item> GetOrderByNumAsync(int ordersNumber);
        Task SaveChangesAsync();
    }
}