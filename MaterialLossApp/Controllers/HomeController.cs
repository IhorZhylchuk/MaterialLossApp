using MaterialLossApp.Models;
using MaterialLossApp.Repo;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MaterialLossApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly SqlRepo _repo;

        public HomeController(SqlRepo repo)
        {
            _repo = repo;
        }

        public IActionResult DefaultMenu()
        {
            
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}