using MaterialLossApp.Interfaces;
using MaterialLossApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MaterialLossApp.Repo
{
    public class SqlRepo : IDefaultInterface
    {
        private readonly ApplicationDbContext _context;
        public SqlRepo(ApplicationDbContext dbContext) 
        { 
            _context = dbContext;
        }
        public async Task<IActionResult> CreateOrderAsync(Item item)
        {
            await _context.AddAsync(item);

            var jsonResponse = new { message = "Success!" };
            return new JsonResult(jsonResponse);
        }

        public async Task<IEnumerable<Item>> GetAllOrdersAsync()
        {
            var orders = await _context.Items.ToListAsync();
            return orders;
        }

        public async Task<Item> GetOrderByNumAsync(int ordersNumber)
        {
            return await _context.Items.Where(n => n.NrZlecenia == ordersNumber).FirstAsync();
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
