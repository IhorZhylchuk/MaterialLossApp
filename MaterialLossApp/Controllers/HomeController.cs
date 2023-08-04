using MaterialLossApp.Models;
using MaterialLossApp.Repo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;

namespace MaterialLossApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly SqlRepo _repo;
        private readonly ApplicationDbContext _dbContext;

        public HomeController(SqlRepo repo, ApplicationDbContext dbContext)
        {
            _repo = repo;
            _dbContext = dbContext;
        }

        public IActionResult DefaultMenu()
        {
            var recipies = new SelectList(_dbContext.Recipes.Select(n => n.Name).ToList());
            var opakowanie = new SelectList(_dbContext.Ingredients.Where(i => i.Use == "Container").Select(n => n.Name).ToList());
            var dekel = new SelectList(_dbContext.Ingredients.Where(i => i.Use == "Cap").Select(n => n.Name).ToList());
            var naklejka = new SelectList(_dbContext.Ingredients.Where(i => i.Use == "Label").Select(n => n.Name).ToList());
            var orders = new SelectList(_dbContext.Items.ToList(), "Id", "NrZlecenia");
            ViewBag.Recipies = recipies;
            ViewBag.Opakowania = opakowanie;
            ViewBag.Dekel = dekel;
            ViewBag.Naklejka = naklejka;
            ViewBag.Orders = orders;

            return View();
        }

        public JsonResult GetZlecenie(int? numerZlecenia)
        {
            if (numerZlecenia != null)
            {
                try
                {
                    var search = _dbContext.Items.Select(i => i.NrZlecenia).ToList().Any(i => i == numerZlecenia);
                    return Json(new { result = search });
                }
                catch (Exception e)
                {
                    e.Message.ToString();
                }

            }
            return Json(null);
        }
        //[Authorize]
        public async Task<JsonResult> GetZleceniaAsync()
        {
            try
            {
                var zlecenia = await _repo.GetAllOrdersAsync();
                return Json(zlecenia);
            }
            catch (Exception e)
            {
                e.Message.ToString();
            }
            return Json(null);
        }

        //[Authorize]
        public async Task<JsonResult> DetailsAsync(int id)
        {
            if (id != 0)
            {
                try
                {
                    var zlecenie = await _repo.GetOrderByIdAsync(id);
                    var surowce = _dbContext.Relations.Where(i => i.RecipeId == zlecenie.RecipeId).Select(i => i.IngredientsId).ToList();


                    List<Ingredient> opakowaniaList = new List<Ingredient>()
                    {
                        _dbContext.Ingredients.Where(n => n.Name == zlecenie.Opakowanie).Select(o => o).First(),
                        _dbContext.Ingredients.Where(n => n.Name == zlecenie.PokrywaNekrętka).Select(o => o).First(),
                        _dbContext.Ingredients.Where(n => n.Name == zlecenie.Naklejka).Select(o => o).First()
                    };

                    List<Tuple<int, string, double>> ingredients = new List<Tuple<int, string, double>>();
                    List<Tuple<int, string, double>> surowiec = new List<Tuple<int, string, double>>();
                    foreach (var elem in surowce)
                    {
                        var counts = _dbContext.ItemsCount.Where(i => i.IngredientId == elem).Select(i => i.IngredientCount).First();
                        var ingredient = _dbContext.Ingredients.Where(i => i.Id == elem).Select(i => i).First();
                        if (elem == 13)
                        {
                            surowiec.Add(new Tuple<int, string, double>(ingredient.MaterialNumber, ingredient.Name, counts));

                        }
                        else
                        {
                            ingredients.Add(new Tuple<int, string, double>(ingredient.MaterialNumber, ingredient.Name, counts));
                        }

                    }
                    ingredients.Add(surowiec[0]);
                    return Json(new { items = ingredients, details = zlecenie, opakowania = opakowaniaList });
                }
                catch (Exception e)
                {
                    e.Message.ToString();
                }
            }
            return Json(null);
        }

        public async Task<JsonResult> DetailsTest(int id)
        {
            if (id != 0)
            {
                try
                {
                    var zlecenie = await _dbContext.Items.Where(i => i.Id == id).FirstAsync();
                    var surowce = _dbContext.Relations.Where(i => i.RecipeId == zlecenie.RecipeId).Select(i => i.IngredientsId).ToList();


                    List<Ingredient> opakowaniaList = new List<Ingredient>()
                    {
                        _dbContext.Ingredients.Where(n => n.Name == zlecenie.Opakowanie).Select(o => o).First(),
                        _dbContext.Ingredients.Where(n => n.Name == zlecenie.PokrywaNekrętka).Select(o => o).First(),
                        _dbContext.Ingredients.Where(n => n.Name == zlecenie.Naklejka).Select(o => o).First()
                    };

                    List<Tuple<int, string, double>> ingredients = new List<Tuple<int, string, double>>();
                    List<Tuple<int, string, double>> surowiec = new List<Tuple<int, string, double>>();
                    foreach (var elem in surowce)
                    {
                        var counts = _dbContext.ItemsCount.Where(i => i.IngredientId == elem).Select(i => i.IngredientCount).First();
                        var ingredient = _dbContext.Ingredients.Where(i => i.Id == elem).Select(i => i).First();
                        if (elem == 13)
                        {
                            surowiec.Add(new Tuple<int, string, double>(ingredient.MaterialNumber, ingredient.Name, counts));

                        }
                        else
                        {
                            ingredients.Add(new Tuple<int, string, double>(ingredient.MaterialNumber, ingredient.Name, counts));
                        }

                    }
                    ingredients.Add(surowiec[0]);
                    return Json(new { items = ingredients, details = zlecenie, opakowania = opakowaniaList });
                }
                catch (Exception e)
                {
                    e.Message.ToString();
                }
            }
            return Json(null);
        }

        public async Task<JsonResult> GetItems()
        {
            return Json(await _repo.GetAllOrdersAsync());
        }
      // [Authorize]
        public async Task<IActionResult> NoweZlecenieAsync(Item model)
        {
            if (model != null)
            {
                try
                {
                    Item item = model;
                    item.RecipeId = _dbContext.Recipes.Where(n => n.Name == model.RecipesName).Select(i => i.Id).FirstOrDefault();

                    var capasity = _dbContext.Ingredients.Where(n => n.Name == model.Opakowanie).Select(i => i.Capacity).FirstOrDefault();
                    item.IlośćOpakowań = Convert.ToInt32(model.Count / capasity);
                    item.IlośćNaklejek = item.IlośćOpakowań;
                    item.IlośćPokrywNekrętek = item.IlośćOpakowań;

                    await _dbContext.Items.AddAsync(item);
                    _dbContext.SaveChanges();


                    var surowce = _dbContext.Relations.Where(i => i.RecipeId == item.RecipeId).Select(i => i).ToList();
                    List<ItemsCount> ingredientsCount = new List<ItemsCount>();
                    foreach (var id in surowce)
                    {
                        var surowiec = new ItemsCount();
                        surowiec.IngredientId = id.IngredientsId;
                        surowiec.ItemId = item.Id;
                        surowiec.IngredientCount = DefaultRecipies.Count(model.Count, id.Amount);
                        ingredientsCount.Add(surowiec);
                    }

                    await _dbContext.ItemsCount.AddRangeAsync(ingredientsCount);
                    _dbContext.SaveChanges();

                    return RedirectToAction("DefaultMenu");
                }
                catch (Exception e)
                {
                    e.Message.ToString();
                }
            }
            return NotFound();
        }
        public async Task<JsonResult> GetAcctualCount(int orderId, int count)
        {
            var order = await _dbContext.Items.Where(n => n.NrZlecenia == orderId).FirstAsync();
           // var receipeId = _dbContext.Recipes.Where(n => n.Name == order.RecipesName).Select(i => i.Id).FirstOrDefault();
            var capasity = _dbContext.Ingredients.Where(n => n.Name == order.Opakowanie).Select(i => i.Capacity).FirstOrDefault();
            order.IlośćOpakowań = Convert.ToInt32(count / capasity);
            order.IlośćNaklejek = order.IlośćOpakowań;
            order.IlośćPokrywNekrętek = order.IlośćOpakowań;

            var surowce = _dbContext.Relations.Where(i => i.RecipeId == order.RecipeId).Select(i => i).ToList();
            List<ItemsCount> ingredientsCount = new List<ItemsCount>();
            foreach (var id in surowce)
            {
                var surowiec = new ItemsCount();
                surowiec.IngredientId = id.IngredientsId;
                surowiec.ItemId = order.Id;
                surowiec.IngredientCount = DefaultRecipies.Count(count, id.Amount);
                ingredientsCount.Add(surowiec);
            }
            return Json(new {updatedOrder = order, iCount =  ingredientsCount});
        }
        /*
        public JsonResult GetIngredients()
        {
            var zlecenie = _dbContext.Items.Select(i => i).FirstOrDefault();
            try
            {
                //var orders = _dbContext.NewOrders.Where(i => i.ItemId == zlecenie.NrZlecenia).Select(n => n.IngredientNumber);
                var surowce = _dbContext.Relations.Where(i => i.RecipeId == zlecenie.RecipeId).Where(i => i.IngredientsId != 13).Select(i => i.IngredientsId).ToList();

                List<Ingredient> opakowaniaList = new List<Ingredient>()
                {
                     _dbContext.Ingredients.Where(n => n.Name == zlecenie.Opakowanie && zlecenie.Contains(n.MaterialNumber)== false).Select(o => o).FirstOrDefault(),
                     _dbContext.Ingredients.Where(n => n.Name == zlecenie.PokrywaNekrętka && orders.Contains(n.MaterialNumber)== false).Select(o => o).FirstOrDefault(),
                     _dbContext.Ingredients.Where(n => n.Name == zlecenie.Naklejka && orders.Contains(n.MaterialNumber)== false).Select(o => o).FirstOrDefault()

                };

                List<Tuple<int, string, double>> ingredients = new List<Tuple<int, string, double>>();

                foreach (var elem in surowce)
                {
                    var ingredient = _dbContext.Ingredients.Where(i => i.Id == elem && orders.Contains(i.MaterialNumber) == false).Select(i => i).FirstOrDefault();
                    try
                    {
                        var counts = _dbContext.ItemsCount.Where(i => i.IngredientId == ingredient.Id).Select(i => i.IngredientCount).FirstOrDefault();
                        ingredients.Add(new Tuple<int, string, double>(ingredient.MaterialNumber, ingredient.Name, counts));

                    }
                    catch (Exception e)
                    {
                        e.Message.ToString();
                    }

                };
                if (ingredients.Count() != 0)
                {
                    return Json(new { details = zlecenie, opakowania = opakowaniaList, items = ingredients, num = 1, onOrders = orders });
                }
                else if (opakowaniaList.Where(i => i != null).ToList().Count() != 0)
                {
                    return Json(new { details = zlecenie, opakowania = opakowaniaList, items = ingredients, num = 1, onOrders = orders });
                }
                else
                {
                    return Json(new { details = zlecenie, num = 0 });
                }

            }
            catch (Exception e)
            {
                e.Message.ToString();
            }
            return Json(new { details = zlecenie, num = 0 });
        }
        */
        //[Authorize]
        public async Task<IActionResult> DeleteAsync(int? id)
        {
            if (id != null)
            {
                try
                {
                    var result = _dbContext.Items.Where(b => b.Id == id).First();
                    _dbContext.Items.Remove(result);
                    await _dbContext.SaveChangesAsync();
                    return Json(new { success = true, message = "Removed successfully" });
                }
                catch (Exception e)
                {
                    e.Message.ToString();
                }
            }
            return NotFound();

        }

       // [Authorize]
        [HttpGet]
        public async Task<JsonResult> EditOrderAsync(int id)
        {
                try
                {
                    var zlecenie = await _repo.GetOrderByIdAsync(id);
                    return Json(new
                    {
                        nrZlecenia = zlecenie?.NrZlecenia,
                        recipesName = zlecenie?.RecipesName,
                        count = zlecenie?.Count,
                        opakowanie = zlecenie?.Opakowanie,
                        pokrywaNekrętka = zlecenie?.PokrywaNekrętka,
                        naklejka = zlecenie?.Naklejka,
                        id = zlecenie?.Id
                    });
                }
                catch (Exception e)
                {
                    e.Message.ToString();
                    return Json(null);
                }
        }

      //  [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditedOrder(Item model)
        {
            if (model != null)
            {
                try
                {
                    var item = await _repo.GetOrderByIdAsync(model.Id);
                    item.RecipesName = model.RecipesName;
                    item.Id = model.Id;
                    item.PokrywaNekrętka = model.PokrywaNekrętka;
                    item.Opakowanie = model.Opakowanie;
                    item.NrZlecenia = model.NrZlecenia;
                    item.Naklejka = model.Naklejka;
                    item.Count = model.Count;

                    item.RecipeId = _dbContext.Recipes.Where(n => n.Name == model.RecipesName).Select(i => i.Id).First();

                    var capasity = _dbContext.Ingredients.Where(n => n.Name == item.Opakowanie).Select(i => i.Capacity).First();
                    item.IlośćOpakowań = Convert.ToInt32(model.Count / capasity);
                    item.IlośćNaklejek = item.IlośćOpakowań;
                    item.IlośćPokrywNekrętek = item.IlośćOpakowań;
                    _dbContext.Items.Update(item);

                    var surowce = _dbContext.Relations.Where(i => i.RecipeId == item.RecipeId).Select(i => i).ToList();
                    var ingredients = _dbContext.ItemsCount.Where(i => i.ItemId == item.Id).Select(i => i).ToList();
                    _dbContext.ItemsCount.RemoveRange(ingredients);
                    List<ItemsCount> ingredientsCount = new List<ItemsCount>();
                    foreach (var id in surowce)
                    {
                        var surowiec = new ItemsCount();
                        surowiec.IngredientId = id.IngredientsId;
                        surowiec.ItemId = item.Id;
                        surowiec.IngredientCount = DefaultRecipies.Count(model.Count, id.Amount);
                        ingredientsCount.Add(surowiec);
                    }
                    _dbContext.ItemsCount.UpdateRange(ingredientsCount);
                    await _dbContext.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    e.Message.ToString();
                }
            }
            return RedirectToAction("DefaultMenu");
        }


    }
}