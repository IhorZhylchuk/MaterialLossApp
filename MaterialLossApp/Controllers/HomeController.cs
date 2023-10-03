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
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Drawing;
using System;
using Syncfusion.Pdf.Grid;
using System.Text;
using Microsoft.SqlServer.Server;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MaterialLossApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly SqlRepo _repo;
        private readonly ApplicationDbContext _dbContext;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public HomeController(SqlRepo repo, ApplicationDbContext dbContext, IWebHostEnvironment hostingEnvironment)
        {
            _repo = repo;
            _dbContext = dbContext;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult DefaultMenu()
        {
            try
            {
                var recipies = new SelectList(_dbContext.Recipes.Select(n => n.Name).ToList());
                var opakowanie = new SelectList(_dbContext.Ingredients.Where(i => i.Use == "Container").Select(n => n.Name).ToList());
                var dekel = new SelectList(_dbContext.Ingredients.Where(i => i.Use == "Cap").Select(n => n.Name).ToList());
                var naklejka = new SelectList(_dbContext.Ingredients.Where(i => i.Use == "Label").Select(n => n.Name).ToList());
                var orders = new SelectList(_dbContext.Items.ToList(), "Id", "NrZlecenia");
                var relisedOrders = new SelectList(_dbContext.RealizedOrders.ToList(), "RealizedOrderId", "RealizedOrderNumber");
                ViewBag.Recipies = recipies;
                ViewBag.Opakowania = opakowanie;
                ViewBag.Dekel = dekel;
                ViewBag.Naklejka = naklejka;
                ViewBag.Orders = orders;
                ViewBag.RelisedOrders = relisedOrders;

                return View();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return View();
        }
        public JsonResult GetZlecenie(int? numerZlecenia)
        {
            if (numerZlecenia != 0)
            {
                try
                {
                    var searchInOrder = _dbContext.Items.Select(i => i.NrZlecenia).ToList().Any(i => i == numerZlecenia);
                    var searchInRelised = _dbContext.RealizedOrders.Select(i => i.RealizedOrderNumber).ToList().Any(i => i == numerZlecenia);
                    return Json(new { resultForOrders = searchInOrder, resultForRelised = searchInRelised });
                }
                catch (Exception e)
                {
                    e.Message.ToString();
                }

            }
            return Json(null);
        }
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
        public async Task<JsonResult> GetItems()
        {
            return Json(await _repo.GetAllOrdersAsync());
        }
        public async Task<IActionResult> NoweZlecenieAsync(Item model)
        {
            if (model != null)
            {
                try
                {
                    await _repo.CreateOrderAsync(model);

                    var item = await _dbContext.Items.Where(n => n.NrZlecenia == model.NrZlecenia).FirstAsync();
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
                    await _dbContext.SaveChangesAsync();

                    return RedirectToAction("DefaultMenu");
                }
                catch (Exception e)
                {
                    e.Message.ToString();
                }
            }
            return NotFound();
        }
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (id != 0)
            {
                try
                {
                    await _repo.DeleteItemAsync(id);
                    return Json(new { success = true, message = "Removed successfully" });
                }
                catch (Exception e)
                {
                    e.Message.ToString();
                }
            }
            return NotFound();

        }

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
                        var actualCount = float.Parse(jsonResultData[i].GetProperty("actualCount").GetString());
                        var waste = jsonResultData[i].GetProperty("waste").GetString();
                        var comment = jsonResultData[i].GetProperty("comment").GetString();

                        Comment commentWaste = new Comment
                        {
                            Description = comment,
                            WasteIngredientId = wasteIngredient.WasteIngredientId,
                            RealizedOrderId = realizedOrder.RealizedOrderId

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
                    return Json(new { message = "Success" });
                }
                catch (Exception e)
                {
                    return Json(e.Message.ToString());
                }
            }
            return Json(null);
        }

        [HttpGet]
        public JsonResult CheckIfRelised(string ordersNumber)
        {
            if(ordersNumber != null)
            {
                var ordersCheck = _dbContext.RealizedOrders.Select(n => n.RealizedOrderNumber).ToList().Any(n => n == Int32.Parse(ordersNumber));

                if (ordersCheck == true)
                {
                    try
                    {
                        var relisedOrder = _dbContext.RealizedOrders.Where(n => n.RealizedOrderNumber == Int32.Parse(ordersNumber)).First();
                        return Json(new { check = ordersCheck, relisedCount = relisedOrder.Count, recipe = relisedOrder.RecipeName });
                    }
                    catch (Exception e)
                    {
                        return Json(e.Message);
                    }
                }
                else
                {
                    return Json(new { check = ordersCheck });
                }
            }
            return Json(null);
        }
        [HttpGet]
        public async Task<JsonResult> ReturnRelisedOrder(string ordersNumber)
        {
            if (ordersNumber != null)
            {
                try
                {
                    var relisedOrder = await _dbContext.RealizedOrders.Where(n => n.RealizedOrderNumber == Int32.Parse(ordersNumber)).FirstAsync();
                    var wasteIngredients = await _dbContext.WasteIngredients.Where(n => n.RealizedOrderId == relisedOrder.RealizedOrderId).ToListAsync();
                    var comments = await _dbContext.Comments.Where(i => i.RealizedOrderId == relisedOrder.RealizedOrderId).ToListAsync();

                    List<Tuple<int, string, float, string, string>> listOfWasteIngredients = new List<Tuple<int, string, float, string, string>>();
                    List<Tuple<int, string, float, string, string>> listOfWastePack = new List<Tuple<int, string, float, string, string>>();

                    for (var i = 0; i < wasteIngredients.Count; i++)
                    {
                        var ingredient = wasteIngredients[i];
                        var comment = comments.Where(i => i.WasteIngredientId == ingredient.WasteIngredientId).Select(d => d.Description).First();


                        var section = _dbContext.Ingredients.Where(i => i.MaterialNumber == ingredient.IngredientNumber).Select(s => s.SectionName).First();
                        if (section == "Opakowania")
                        {
                            listOfWastePack.Add(new Tuple<int, string, float, string, string>(ingredient.IngredientNumber, ingredient.IngredientName,
                                ingredient.Count, ingredient.Waste, comment));
                        }
                        else
                        {
                            listOfWasteIngredients.Add(new Tuple<int, string, float, string, string>(ingredient.IngredientNumber, ingredient.IngredientName,
                                ingredient.Count, ingredient.Waste, comment));
                        }

                    }

                    return Json(new { relisedOrder, listOfWasteIngredients, listOfWastePack });
                }
                catch (Exception e)
                {
                    return Json(e.Message.ToString());
                }
            }
            return Json(null);
        }
        public IActionResult GeneretaPdfFile(string check, string relisedOrder)
        {
            try
            {
                var relisedOrderFromDB = _dbContext.RealizedOrders.Where(n => n.RealizedOrderNumber == Int32.Parse(relisedOrder)).First();
                var wasteIngredients = _dbContext.WasteIngredients.Where(n => n.RealizedOrderId == relisedOrderFromDB.RealizedOrderId).ToList();
                var comments = _dbContext.Comments.Where(i => i.RealizedOrderId == relisedOrderFromDB.RealizedOrderId).ToList();

                PdfDocument pdfDocument = new PdfDocument();
                PdfPage currentPage = pdfDocument.Pages.Add();
                SizeF clientSize = currentPage.GetClientSize();
                FileStream imageStream = new FileStream("C:\\Users\\Igor\\source\\repos\\MaterialLossApp\\MaterialLossApp\\wwwroot\\logo.png", FileMode.Open, FileAccess.Read);
                PdfImage icon = new PdfBitmap(imageStream);
                SizeF iconSize = new SizeF(60, 60);
                PointF iconLocation = new PointF(14, 13);
                PdfGraphics graphics = currentPage.Graphics;
                graphics.DrawImage(icon, iconLocation, iconSize);

                string path = _hostingEnvironment.WebRootPath + "/Font/Helvetica.ttf";
                Stream fontStream = new FileStream(path, FileMode.Open, FileAccess.Read);

                PdfTrueTypeFont font = new PdfTrueTypeFont(fontStream, 12, PdfFontStyle.Regular);
                var text = new PdfTextElement("Data realizacji: " + DateTime.Now.ToString("dd/MM/yyyy"), font, new PdfSolidBrush(Color.Black));
                text.StringFormat = new PdfStringFormat(PdfTextAlignment.Right);
                PdfLayoutResult result = text.Draw(currentPage, new PointF(clientSize.Width - 25, iconLocation.Y + 100));
                PdfTrueTypeFont fontBold = new PdfTrueTypeFont(fontStream, 11, PdfFontStyle.Regular);
                text = new PdfTextElement("RAPORT WYKONANEGO ZLECENIA", fontBold);
                text.StringFormat = new PdfStringFormat(PdfTextAlignment.Center);
                result = text.Draw(currentPage, new PointF(clientSize.Width - 250, result.Bounds.Y + 60));

                List<string> zlecenieInfo = new List<string>() { "Numer zlecenia", "Nazwa produktu", "Wyprodukowana " + DefaultRecipies.ReturnValue("iłość kg.") };
                List<string> values = new List<string>() { relisedOrderFromDB.RealizedOrderNumber.ToString(), relisedOrderFromDB.RecipeName, relisedOrderFromDB.Count.ToString() };

                PdfGrid gridForOrder = new PdfGrid();
                gridForOrder.Style.Font = font;
                gridForOrder.Columns.Add(3);
                gridForOrder.Columns[0].Width = 160;
                gridForOrder.Columns[1].Width = 160;
                gridForOrder.Columns[2].Width = 160;

                gridForOrder.Headers.Add(1);
                PdfStringFormat stringFormat = new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle);
                PdfGridRowStyle style = new PdfGridRowStyle();
                PdfGridRow ordersHeader = gridForOrder.Headers[0];

                ordersHeader.Cells[0].Value = zlecenieInfo[0];
                ordersHeader.Cells[0].StringFormat = stringFormat;
                ordersHeader.Cells[1].Value = zlecenieInfo[1];
                ordersHeader.Cells[1].StringFormat = stringFormat;
                ordersHeader.Cells[2].Value = zlecenieInfo[2];
                ordersHeader.Cells[2].StringFormat = stringFormat;

                ordersHeader.Style = style;

                PdfGridRow ordersRow = gridForOrder.Rows.Add();
                ordersRow.Cells[0].Value = values[0];
                ordersRow.Cells[0].StringFormat = stringFormat;

                ordersRow.Cells[1].Value = values[1];
                ordersRow.Cells[1].StringFormat = stringFormat;

                ordersRow.Cells[2].Value = values[2];
                ordersRow.Cells[2].StringFormat = stringFormat;

                gridForOrder.ApplyBuiltinStyle(PdfGridBuiltinStyle.GridTable4Accent5);
                PdfGridStyle gridStyle = new PdfGridStyle();
                gridStyle.CellPadding = new PdfPaddings(5, 5, 10, 10);
                gridForOrder.Style = gridStyle;

                PdfGridLayoutFormat layoutFormat = new PdfGridLayoutFormat();
                layoutFormat.Layout = PdfLayoutType.Paginate;
                result = gridForOrder.Draw(currentPage, 14, result.Bounds.Bottom + 20, clientSize.Width - 35, layoutFormat);
                text = new PdfTextElement("Lista zyżytych surowców", fontBold);
                text.StringFormat = new PdfStringFormat(PdfTextAlignment.Center);
                result = text.Draw(currentPage, new PointF(clientSize.Width - 250, result.Bounds.Y + 80));

                PdfGrid grid = new PdfGrid();
                grid.Style.Font = font;
                grid.Columns.Add(5);
                grid.Columns[0].Width = 70;
                grid.Columns[1].Width = 100;
                grid.Columns[2].Width = 60;
                grid.Columns[3].Width = 70;

                grid.Headers.Add(1);
                PdfGridRow header = grid.Headers[0];

                header.Cells[0].Value = "Nr.materialu";
                header.Cells[0].StringFormat = stringFormat;
                header.Cells[1].Value = "Nazwa surowca";
                header.Cells[1].StringFormat = stringFormat;
                header.Cells[2].Value = DefaultRecipies.ReturnValue("Faktyczna ilość (szt./kg)");
                header.Cells[2].StringFormat = stringFormat;
                header.Cells[3].Value = "Straty";
                header.Cells[3].StringFormat = stringFormat;
                header.Cells[4].Value = "Komentarze";
                header.Cells[4].StringFormat = stringFormat;


                header.Style = style;

                for (var i = 0; i < wasteIngredients.Count; i++)
                {
                    var ingredient = wasteIngredients[i];
                    var comment = comments.Where(i => i.WasteIngredientId == ingredient.WasteIngredientId).Select(d => d.Description).First();


                    PdfGridRow row = grid.Rows.Add();
                    row.Cells[0].Value = ingredient.IngredientNumber.ToString();
                    row.Cells[0].StringFormat = stringFormat;

                    row.Cells[1].Value = DefaultRecipies.ReturnValue(ingredient.IngredientName.ToString());
                    row.Cells[1].StringFormat = stringFormat;

                    row.Cells[2].Value = ingredient.Count.ToString();
                    row.Cells[2].StringFormat = stringFormat;

                    row.Cells[3].Value = ingredient.Waste.ToString();
                    row.Cells[3].StringFormat = stringFormat;

                    row.Cells[4].Value = comment;
                    row.Cells[4].StringFormat = stringFormat;

                    grid.ApplyBuiltinStyle(PdfGridBuiltinStyle.GridTable4Accent5);

                    grid.Style = gridStyle;

                }

                layoutFormat.Layout = PdfLayoutType.Paginate;
                result = grid.Draw(currentPage, 14, result.Bounds.Bottom + 20, clientSize.Width - 35, layoutFormat);
                PdfPage nextPage = pdfDocument.Pages.Add();

                var jsonResultData = JsonSerializer.Deserialize<dynamic>(check)!;
                var pasteryzacja = DefaultRecipies.ReturnValue("Pasteryzacja przeprowadzona według receptury?");
                var ciałaObce = DefaultRecipies.ReturnValue("Czy znaleziono w surowcach lub w gotowym produkcie ciała obce? Czy był poinformowany przełozony/dział jakości? ");
                var dataOpakowania = "Data umieszczona na opakowaniu odpowiada karcie produktu?";
                var zgodnośćZRecepturą = DefaultRecipies.ReturnValue("Gotowy produkt jest zgony z recepturą?");
                var metaldetektor = DefaultRecipies.ReturnValue(" Czy był sprawdzony metaldetektor? Czy w ciągu zmiany wystepowały jakieś problemy z metaldetektorem?");
                var opakowanie = "Opakowanie zgodne ze standardem?";
                var lepkość = "Lepkość produktu";
                var ekstrakt = "Ekstrakt";
                var ph = "Ph";
                var temperatura = "Temperatura przed rozlewem";

                List<string> qa = new List<string> {pasteryzacja, ciałaObce, dataOpakowania,
                zgodnośćZRecepturą, metaldetektor, opakowanie};

                text = new PdfTextElement("QA Checks", fontBold);
                text.StringFormat = new PdfStringFormat(PdfTextAlignment.Center);
                result = text.Draw(nextPage, new PointF(clientSize.Width - 250, result.Bounds.Top - 270));

                PdfGrid gridForCecks = new PdfGrid();
                gridForCecks.Style.Font = font;
                gridForCecks.Columns.Add(4);
                gridForCecks.Columns[0].Width = 120;
                gridForCecks.Columns[1].Width = 120;
                gridForCecks.Columns[2].Width = 120;
                gridForCecks.Columns[3].Width = 120;

                gridForCecks.Headers.Add(1);
                PdfGridRow ckecksHeader = gridForCecks.Headers[0];

                ckecksHeader.Cells[0].Value = DefaultRecipies.ReturnValue("Lepkość produktu");
                ckecksHeader.Cells[0].StringFormat = stringFormat;
                ckecksHeader.Cells[1].Value = "Ekstrakt";
                ckecksHeader.Cells[1].StringFormat = stringFormat;
                ckecksHeader.Cells[2].Value = "Ph";
                ckecksHeader.Cells[2].StringFormat = stringFormat;
                ckecksHeader.Cells[3].Value = "Temperatura przed rozlewem";
                ckecksHeader.Cells[3].StringFormat = stringFormat;

                ckecksHeader.Style = style;

                PdfGridRow checksRow = gridForCecks.Rows.Add();
                checksRow.Cells[0].Value = DefaultRecipies.ReturnComment(jsonResultData, "Lepkość") + " Pa·s";
                checksRow.Cells[0].StringFormat = stringFormat;

                checksRow.Cells[1].Value = DefaultRecipies.ReturnComment(jsonResultData, "Ekstrakt") + " %";
                checksRow.Cells[1].StringFormat = stringFormat;

                checksRow.Cells[2].Value = DefaultRecipies.ReturnComment(jsonResultData, "Ph");
                checksRow.Cells[2].StringFormat = stringFormat;

                checksRow.Cells[3].Value = DefaultRecipies.ReturnComment(jsonResultData, "Temperatura") + " °C";
                checksRow.Cells[3].StringFormat = stringFormat;

                gridForCecks.ApplyBuiltinStyle(PdfGridBuiltinStyle.GridTable4Accent5);
                gridForCecks.Style = gridStyle;

                layoutFormat.Layout = PdfLayoutType.Paginate;
                result = gridForCecks.Draw(nextPage, 14, result.Bounds.Y + 20, clientSize.Width - 35, layoutFormat);


                text = new PdfTextElement("Przebieg procesu produkcyjnego", fontBold);
                text.StringFormat = new PdfStringFormat(PdfTextAlignment.Center);
                result = text.Draw(nextPage, new PointF(clientSize.Width - 250, result.Bounds.Y + 80));

                List<string> qaBools = new List<string> { DefaultRecipies.ReturnBoolResult(jsonResultData, "Pasteryzacja"),
                DefaultRecipies.ReturnBoolResult(jsonResultData, "CiałaObce"), DefaultRecipies.ReturnBoolResult(jsonResultData, "DataOpakowania"),
                DefaultRecipies.ReturnBoolResult(jsonResultData, "Receptura"), DefaultRecipies.ReturnBoolResult(jsonResultData, "MetalDetektor"),
                DefaultRecipies.ReturnBoolResult(jsonResultData, "Opakowanie")};

                List<string> commentsForProcess = new List<string>() { DefaultRecipies.ReturnComment(jsonResultData, "PasteryzacjaKomentarz"),
                DefaultRecipies.ReturnComment(jsonResultData, "CiałaObceKomentarz"), DefaultRecipies.ReturnComment(jsonResultData, "DataOpakowaniaKomentarz"),
                DefaultRecipies.ReturnComment(jsonResultData, "RecepturaKomentarz"), DefaultRecipies.ReturnComment(jsonResultData, "MetalDetektorKomentarz"),
                DefaultRecipies.ReturnComment(jsonResultData, "OpakowanieKomentarz")};

                List<string> qaChecks = new List<string>() { lepkość, ekstrakt, ph, temperatura };

                PdfGrid qAgrid = new PdfGrid();
                qAgrid.Style.Font = font;
                qAgrid.Columns.Add(3);
                qAgrid.Columns[0].Width = 240;
                qAgrid.Columns[1].Width = 40;
                qAgrid.Columns[2].Width = 200;

                qAgrid.Headers.Add(1);
                stringFormat = new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle);
                PdfGridRow QAheader = grid.Headers[0];

                QAheader.Cells[0].Value = "";
                QAheader.Cells[0].StringFormat = stringFormat;
                QAheader.Cells[1].Value = "";
                QAheader.Cells[1].StringFormat = stringFormat;
                QAheader.Cells[2].Value = "";
                QAheader.Cells[2].StringFormat = stringFormat;

                QAheader.Style = style;

                for (var i = 0; i < qa.Count; i++)
                {
                    PdfGridRow qaRow = qAgrid.Rows.Add();
                    qaRow.Cells[0].Value = qa[i];
                    qaRow.Cells[0].StringFormat = stringFormat;

                    qaRow.Cells[1].Value = qaBools[i];
                    qaRow.Cells[1].StringFormat = stringFormat;

                    qaRow.Cells[2].Value = commentsForProcess[i];
                    qaRow.Cells[2].StringFormat = stringFormat;

                    qAgrid.ApplyBuiltinStyle(PdfGridBuiltinStyle.GridTable4Accent5);
                    qAgrid.Style = gridStyle;

                }
                layoutFormat.Layout = PdfLayoutType.Paginate;
                result = qAgrid.Draw(nextPage, 14, result.Bounds.Bottom + 20, clientSize.Width - 35, layoutFormat);
                MemoryStream stream = new MemoryStream();
                pdfDocument.Save(stream);
                pdfDocument.Close(true);
                stream.Position = 0;

                FileStreamResult fileStreamResult = new FileStreamResult(stream, "application/pdf");
                fileStreamResult.FileDownloadName = "Report.pdf";

                return fileStreamResult;
            }
            catch (Exception e)
            {
                return Json(new { message = e.Message.ToString() });
            }

        }

    }
}