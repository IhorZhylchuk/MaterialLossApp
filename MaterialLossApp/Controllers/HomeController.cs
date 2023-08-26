using MaterialLossApp.Models;
using MaterialLossApp.Repo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
//using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Helpers;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        [HttpPost]
        public async Task<JsonResult> GetValues(string jsonData)
        {

             if (jsonData != null)
                {
                    try
                    {
                        var jsonResultData = JsonSerializer.Deserialize<dynamic>(jsonData)!;
                        var order = Int32.Parse(jsonResultData[0].GetProperty("order").GetString());
                        var realisedCount = Int32.Parse(jsonResultData[0].GetProperty("realisedCount").GetString());
                        var recepture = jsonResultData[0].GetProperty("recepture").GetString();

                    /*
                    var ingredientNumber = Int32.Parse(jsonResultData[0].GetProperty("ingredientNumber").GetString());
                    var ingredientName = jsonResultData[0].GetProperty("ingredientNumber").GetString();
                    var count = Int32.Parse(jsonResultData[0].GetProperty("count").GetString());
                    var actualCount = Int32.Parse(jsonResultData[0].GetProperty("actualCount").GetString());
                    var waste = jsonResultData[0].GetProperty("waste").GetString();
                    var comment = jsonResultData[0].GetProperty("comment").GetString();
                    */
                        RealizedOrders realizedOrder = new RealizedOrders
                        {
                            RealizedOrderNumber = order,
                            Count = realisedCount,
                            RecipeName = recepture,
                        };
                        await _dbContext.RealizedOrders.AddAsync(realizedOrder);
                        await _dbContext.SaveChangesAsync();

                        for (var i = 0; i < jsonResultData.GetArrayLength(); i++)
                        {
                            WasteIngredients wasteIngredient = new WasteIngredients();
                            _dbContext.WasteIngredients.Add(wasteIngredient);
                            _dbContext.SaveChanges();
                            var ingredientNumber = Int32.Parse(jsonResultData[i].GetProperty("ingredientNumber").GetString());
                            var ingredientName = jsonResultData[i].GetProperty("ingredientName").GetString();
                           // var count = Int32.Parse(jsonResultData[i].GetProperty("count").GetString());
                            var actualCount = Double.Parse(jsonResultData[i].GetProperty("actualCount").GetString());
                            var waste = jsonResultData[i].GetProperty("waste").GetString();
                            var comment = jsonResultData[i].GetProperty("comment").GetString();

                            Comment commentWaste = new Comment
                            {
                                Description = comment,
                                //IngredientId = wasteIngredient,
                                WasteIngredientId = wasteIngredient.WasteIngredientId
                            };
                            await _dbContext.Comments.AddAsync(commentWaste);
                            await _dbContext.SaveChangesAsync();
                            wasteIngredient.RealizedOrderId = realizedOrder.RealizedOrderId;
                            wasteIngredient.CommentId = commentWaste.CommentId;
                            wasteIngredient.Count = actualCount;
                            wasteIngredient.IngredientName = ingredientName;
                            wasteIngredient.IngredientNumber = ingredientNumber;
                            wasteIngredient.Waste = waste;


                            _dbContext.WasteIngredients.Update(wasteIngredient);
                            _dbContext.Comments.Update(commentWaste);
                            await _dbContext.SaveChangesAsync();

                        }
                        return Json(new { message = "Success" }); 

                     //return Json(jsonData);
                }
                    catch(Exception e)
                    {
                        return Json(new {message = "An Error occured" + e.Message.ToString()});
                    }

                }
                return Json(null);
               
            /*
   try
   {
       dynamic jsonResultData = JsonConvert.DeserializeObject(jsonData)!;
       var order =Int32.Parse(jsonResultData.order);
       var realisedCount = Int32.Parse(jsonResultData.realisedCount);
       var recepture = jsonResultData.recepture;

       RealizedOrders realizedOrder = new RealizedOrders
       {
           RealizedOrderNumber = order,
           Count = realisedCount,
           RecipeName = recepture,
       };
       _dbContext.RealizedOrders.Add(realizedOrder);
       _dbContext.SaveChanges();
       for (var i = 0; i < jsonResultData.Count; i++)
       {
           WasteIngredients wasteIngredient = new WasteIngredients();

           var ingredientNumber = Int32.Parse(jsonResultData[i].ingredientNumber);
           var ingredientName = jsonResultData[i].ingredientName;
           var count = Int32.Parse(jsonResultData[i].count);
           var actualCount = Int32.Parse(jsonResultData[i].actualCount);
           var waste = jsonResultData[i].waste;
           var comment = jsonResultData[i].comment;

           Comment commentWaste = new Comment
           {
               Description = comment,
               Ingredient = wasteIngredient,
               WasteIngredientId = wasteIngredient.WasteIngredientId
           };

           wasteIngredient.RealizedOrder = realizedOrder;
           wasteIngredient.Comment = commentWaste;
           wasteIngredient.CommentId = commentWaste.CommentId;
           wasteIngredient.Count = count;
           wasteIngredient.IngredientName = ingredientName;
           wasteIngredient.IngredientNumber = ingredientNumber;
           wasteIngredient.Waste = waste;


           _dbContext.WasteIngredients.Add(wasteIngredient);
           _dbContext.Comments.Add(commentWaste);
           _dbContext.SaveChanges();

       }
       return Json(new { message = "Success" });
   }
   catch (Exception e) {
       return Json(new { message = e.Message.ToString()});

   }
   */

        }

        [HttpPost]
        public async Task<JsonResult> TestGet(string jsonData)
        {
            if(jsonData != null)
            {
                try
                {
                    var jsonResultData = JsonSerializer.Deserialize<dynamic>(jsonData)!;
                    var order = Int32.Parse(jsonResultData[0].GetProperty("order").GetString());
                    var realisedCount = Int32.Parse(jsonResultData[0].GetProperty("realisedCount").GetString());
                    var recepture = jsonResultData[0].GetProperty("recepture").GetString();

                    
                   // var ingredientNumber = Int32.Parse(jsonResultData[0].GetProperty("ingredientNumber").GetString());
                   // var ingredientName = jsonResultData[0].GetProperty("ingredientNumber").GetString();
                  //  var count = Int32.Parse(jsonResultData[0].GetProperty("count").GetString());
                   // var actualCount = Int32.Parse(jsonResultData[0].GetProperty("actualCount").GetString());
                    //var waste = jsonResultData[0].GetProperty("waste").GetString();
                    //var comment = jsonResultData[0].GetProperty("comment").GetString();
                    

                      RealizedOrders realizedOrder = new RealizedOrders
                      {
                         RealizedOrderNumber = order,
                        Count = realisedCount,
                          RecipeName = recepture,
                      };

                   // WasteIngredients wasteIngredient = new WasteIngredients();
                   // wasteIngredient.RealizedOrderId = realizedOrder.RealizedOrderId;
                    //wasteIngredient.CommentId = commentWaste.CommentId;
                   // wasteIngredient.Count = count;
                  //  wasteIngredient.IngredientName = ingredientName;
                    //wasteIngredient.IngredientNumber = ingredientNumber;
                  //  wasteIngredient.Waste = waste;

                     await _dbContext.RealizedOrders.AddAsync(realizedOrder);
                    // await _dbContext.WasteIngredients.AddAsync(wasteIngredient);
                     await _dbContext.SaveChangesAsync();

                      for (var i = 0; i < jsonResultData.GetArrayLength(); i++)
                      {
                          WasteIngredients wasteIngredient = new WasteIngredients();
                          await _dbContext.WasteIngredients.AddAsync(wasteIngredient);
                          await _dbContext.SaveChangesAsync();
                          var ingredientName = jsonResultData[i].GetProperty("ingredientName").GetString();
                          var ingredientNumber = new object();
                        if (ingredientName == "Woda")
                        {
                            ingredientNumber = 0;
                        }
                        else
                        {
                            ingredientNumber = Int32.Parse(jsonResultData[i].GetProperty("ingredientNumber").GetString());
                        }
                        //var count = Int32.Parse(jsonResultData[i].GetProperty("count").GetString());
                          var actualCount = float.Parse(jsonResultData[i].GetProperty("actualCount").GetString());
                          var waste = jsonResultData[i].GetProperty("waste").GetString();
                          var comment = jsonResultData[i].GetProperty("comment").GetString();



                          Comment commentWaste = new Comment
                          {
                              Description = comment,
                              WasteIngredientId = wasteIngredient.WasteIngredientId
                          };
                          await _dbContext.Comments.AddAsync(commentWaste);
                          await _dbContext.SaveChangesAsync();
                          wasteIngredient.RealizedOrderId = realizedOrder.RealizedOrderId;
                          wasteIngredient.CommentId = commentWaste.CommentId;
                          wasteIngredient.Count = actualCount;
                          wasteIngredient.IngredientName = ingredientName;
                          wasteIngredient.IngredientNumber = 0;
                          wasteIngredient.IngredientNumber = (int)ingredientNumber;
                          wasteIngredient.Waste = waste;


                        _dbContext.WasteIngredients.Update(wasteIngredient);
                        _dbContext.Comments.Update(commentWaste);
                        await _dbContext.SaveChangesAsync();

                      } 
                    //return Json(new { jsonResultData });
                    return Json(new {message = "Success"});
                }
                catch (Exception e)
                {
                    return Json(e.Message.ToString());
                }
            }
            return Json(null);
        }

        //  public async Task<IActionResult> WasteCountingAsync([FromBody] )
    }
}