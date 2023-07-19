using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace MaterialLossApp.Models
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Relation> Relations { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContext) : base(dbContext)
        {

        }
      
        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);
            DefaultRecipies.SeedData(builder);
        }
    }

}
