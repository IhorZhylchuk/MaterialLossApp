using MaterialLossApp.Controllers;
using MaterialLossApp.Models;
using MaterialLossApp.Repo;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Moq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Tests_For_Material_Loss
{
    public class MainTest
    {
        public DbContextOptions<ApplicationDbContext> Options()
        {
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestMaterials")
                .Options;
            return dbContextOptions;
        }
        public List<Item> ReturnItems()
        {
            List<Item> items = new List<Item>()
        {
            new Item { Id = 13, Count = 1, NrZlecenia = 2733344 },
            new Item { Id = 24, Count = 1, NrZlecenia = 2255344 },
            new Item { Id = 32, Count = 133, NrZlecenia = 2213344 },
            };

            return items;
        }
        [Fact]
        public async Task GetZleceniaAsync_ReturnOrders()
        {
            using (var db = new ApplicationDbContext(Options()))
            {

                db.Items.AddRange(ReturnItems());
                db.SaveChanges();

                SqlRepo _sqlRepo = new SqlRepo(db);
                var result = await _sqlRepo.GetAllOrdersAsync();

                Assert.NotNull(result);
                Assert.IsAssignableFrom<List<Item>>(result);

                await db.Database.EnsureDeletedAsync();
            }
        }
        [Fact]
        public async Task DetailsAsync_ReturnOrderById()
        {
            using (var db = new ApplicationDbContext(Options()))
            {
                await db.Items.AddRangeAsync(ReturnItems());
                await db.SaveChangesAsync();

                SqlRepo _sqlRepo = new SqlRepo(db);
                IWebHostEnvironment hostingEnvironment = new Mock<IWebHostEnvironment>().Object;        

                var result = _sqlRepo.GetOrderByIdAsync(13);
                Assert.NotNull(result);
                await Assert.IsType<Task<Item>>(result);
                Assert.Equal(ReturnItems()[0].NrZlecenia, result.Result.NrZlecenia);

                HomeController controller = new HomeController(_sqlRepo, db, hostingEnvironment);
                var resultFromHome = await controller.DetailsAsync(13) as JsonResult;
                var jsonResult = Assert.IsType<JsonResult>(resultFromHome);

                Assert.NotNull(jsonResult);
                Assert.NotSame("[]", jsonResult);

                await db.Database.EnsureDeletedAsync();
            }
        }
        [Fact]
        public void GetZlecenie()
        {
            using (var db = new ApplicationDbContext(Options()))
            {
                db.Items.AddRange(ReturnItems());
                db.SaveChanges();

                SqlRepo _sqlRepo = new SqlRepo(db);
                IWebHostEnvironment hostingEnvironment = new Mock<IWebHostEnvironment>().Object;
                int nrZlecenia = 2733344;

                HomeController controller = new HomeController(_sqlRepo, db, hostingEnvironment);
                var result = controller.GetZlecenie(nrZlecenia) as JsonResult;

                Assert.NotNull(result);
                Assert.IsType<JsonResult>(result);
                var searchResult = result.Value?.GetType().GetProperty("result")?.GetValue(result.Value, null);
                Assert.True((bool)searchResult!);

                db.Database.EnsureDeleted();
            }
        }
        [Fact]
        public void GetZlecenie_ReturnNull()
        {
            using (var db = new ApplicationDbContext(Options()))
            {
                SqlRepo _sqlRepo = new SqlRepo(db);
                IWebHostEnvironment hostingEnvironment = new Mock<IWebHostEnvironment>().Object;
                int nrZlecenia = 0;

                HomeController controller = new HomeController(_sqlRepo, db, hostingEnvironment);
                var result = controller.GetZlecenie(nrZlecenia) as JsonResult;

                Assert.IsType<JsonResult>(result);
                Assert.Null(result.Value);

                db.Database.EnsureDeleted();
            }
        }
        [Fact]
        public async Task GetZlecenia_ReturnAll()
        {
            using (var db = new ApplicationDbContext(Options()))
            {

                SqlRepo _sqlRepo = new SqlRepo(db);
                IWebHostEnvironment hostingEnvironment = new Mock<IWebHostEnvironment>().Object;

                HomeController controller = new HomeController(_sqlRepo, db, hostingEnvironment);
                var result = await controller.GetZleceniaAsync() as JsonResult;

                var returnedResult = Assert.IsType<JsonResult>(result);
                Assert.NotNull(returnedResult);

                await db.Database.EnsureDeletedAsync();
            }
        }
        [Fact]
        public async Task NoweZlecenieAsync()
        {
            using (var db = new ApplicationDbContext(Options()))
            {
                var countBefore = await db.Items.CountAsync();

                SqlRepo _sqlRepo = new SqlRepo(db);
                IWebHostEnvironment hostingEnvironment = new Mock<IWebHostEnvironment>().Object;

                Item order = new Item() { NrZlecenia = 229233, Count = 1600, RecipesName = "Sos krówka" };
                Recipe recipe = new Recipe() { Id = 1, Name = "Sos krówka" };
                Ingredient ingredient = new Ingredient() { Id = 1, Capacity = 1, MaterialNumber = 44444444, Name = "Butelka czarna", SectionName = "Opakowania", Use = "Container" };
                await db.Recipes.AddAsync(recipe);
                await db.Ingredients.AddAsync(ingredient);
                await db.SaveChangesAsync();
                
                var opakowanie = await db.Ingredients.Where(o => o.Use == "Container").FirstAsync();
                order.Opakowanie = opakowanie.Name;
                order.RecipesName = recipe.Name;

                HomeController controller = new HomeController(_sqlRepo, db, hostingEnvironment);
                var result = await controller.NoweZlecenieAsync(order);
                int countAfter = await db.Items.CountAsync();

                Assert.NotNull(result);
                Assert.IsType<RedirectToActionResult>(result);
                Assert.Equal("DefaultMenu", (result as RedirectToActionResult)!.ActionName);
                Assert.NotEqual(countBefore, countAfter);

                await db.Database.EnsureDeletedAsync();
            }
        }
        [Fact]
        public async Task NoweZlecenieAsync_ReturnNull()
        {
            using (var db = new ApplicationDbContext(Options()))
            {

                var countBefore = await db.Items.CountAsync();

                SqlRepo _sqlRepo = new SqlRepo(db);
                IWebHostEnvironment hostingEnvironment = new Mock<IWebHostEnvironment>().Object;

                Item order = null!;

                HomeController controller = new HomeController(_sqlRepo, db, hostingEnvironment);
                await controller.NoweZlecenieAsync(order!);
                var countAfter = await db.Items.CountAsync();

                Assert.Equal(countBefore, countAfter);

                await db.Database.EnsureDeletedAsync();
            }
        }
        [Fact]
        public async Task DeleteAsync()
        {
            using (var db = new ApplicationDbContext(Options()))
            {
                await db.Items.AddRangeAsync(ReturnItems());
                await db.SaveChangesAsync();

                int countBefore = await db.Items.CountAsync();

                SqlRepo _sqlRepo = new SqlRepo(db);
                IWebHostEnvironment hostingEnvironment = new Mock<IWebHostEnvironment>().Object;

                HomeController controller = new HomeController(_sqlRepo, db, hostingEnvironment);
                var result = await controller.DeleteAsync(13) as JsonResult;
                var countAfter = await db.Items.CountAsync();

                Assert.NotNull(result);
                Assert.IsType<JsonResult>(result);  
                Assert.NotEqual(countBefore, countAfter);

                await db.Database.EnsureDeletedAsync();
            }
        }
        [Fact]
        public async Task DeleteAsync_ReturnNotFound()
        {
            using (var db = new ApplicationDbContext(Options()))
            {
                await db.Items.AddRangeAsync(ReturnItems());
                await db.SaveChangesAsync();

                SqlRepo _sqlRepo = new SqlRepo(db);
                IWebHostEnvironment hostingEnvironment = new Mock<IWebHostEnvironment>().Object;

                HomeController controller = new HomeController(_sqlRepo, db, hostingEnvironment);
                var result = await controller.DeleteAsync(0);

                Assert.IsType<NotFoundResult>(result);

                await db.Database.EnsureDeletedAsync();
            }
        }
        [Fact]
        public async Task CheckIfRelised_Return_Not_Null()
        {
            using (var db = new ApplicationDbContext(Options()))
            {
                RealizedOrders relisedOrder = new RealizedOrders() { Count = 1000, RealizedOrderId = 1, RealizedOrderNumber = 22233445, RecipeName = "Sos krówka" };
                await db.RealizedOrders.AddAsync(relisedOrder); 
                await db.SaveChangesAsync();

                SqlRepo _sqlRepo = new SqlRepo(db);
                IWebHostEnvironment hostingEnvironment = new Mock<IWebHostEnvironment>().Object;

                HomeController controller = new HomeController(_sqlRepo, db, hostingEnvironment);
                var result_True = controller.CheckIfRelised(relisedOrder.RealizedOrderNumber.ToString()) as  JsonResult;

                var searchResult = result_True.Value?.GetType().GetProperty("check")?.GetValue(result_True.Value, null);
                Assert.True((bool)searchResult!);

                var result_False = controller.CheckIfRelised(relisedOrder.RealizedOrderNumber.ToString() + "1") as JsonResult;

                var searchResult_False = result_False.Value?.GetType().GetProperty("check")?.GetValue(result_False.Value, null);
                Assert.False((bool)searchResult_False!);

                await db.Database.EnsureDeletedAsync();
            }
        }

        [Fact]
        public async Task CheckIfRelised_Return_Null()
        {
            using (var db = new ApplicationDbContext(Options()))
            {
                RealizedOrders relisedOrder = new RealizedOrders() { Count = 1000, RealizedOrderId = 1, RealizedOrderNumber = 22233445, RecipeName = "Sos krówka" };
                await db.RealizedOrders.AddAsync(relisedOrder);
                await db.SaveChangesAsync();

                SqlRepo _sqlRepo = new SqlRepo(db);
                IWebHostEnvironment hostingEnvironment = new Mock<IWebHostEnvironment>().Object;

                HomeController controller = new HomeController(_sqlRepo, db, hostingEnvironment);
                var result = controller.CheckIfRelised(null!) as JsonResult;

                Assert.Null(result.Value);

                await db.Database.EnsureDeletedAsync();
            }
        }
    }
}