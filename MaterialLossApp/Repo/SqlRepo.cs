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
        public async Task<IActionResult> CreateOrderAsync(Item model)
        {
            Item item = model;
            item.RecipeId = await _context.Recipes.Where(n => n.Name == model.RecipesName).Select(i => i.Id).FirstAsync();

            var capasity = await _context.Ingredients.Where(n => n.Name == model.Opakowanie).Select(i => i.Capacity).FirstAsync();
            if(capasity != 0)
            {
                item.IlośćOpakowań = Convert.ToInt32(model.Count / capasity);
                item.IlośćNaklejek = item.IlośćOpakowań;
                item.IlośćPokrywNekrętek = item.IlośćOpakowań;

                await _context.AddAsync(item);
                await _context.SaveChangesAsync();

                var jsonResponse = new { message = "Success!" };
                return new JsonResult(jsonResponse);
            }
            else
            {

                var jsonResponse = new { message = "Error occurred!" };
                return new JsonResult(jsonResponse);
            }

        }

        public async Task<IEnumerable<Item>> GetAllOrdersAsync()
        {
            var orders = await _context.Items.ToListAsync();
            return orders;
        }

        public async Task<Item> GetOrderByIdAsync(int id)
        {
            return await _context.Items.Where(n => n.Id == id).FirstAsync();
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task DeleteItemAsync(int id)
        {
            var result = await _context.Items.Where(b => b.Id == id).FirstAsync();
            _context.Items.Remove(result);
            await _context.SaveChangesAsync();
        }
    }
}
