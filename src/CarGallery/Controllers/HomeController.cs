using CarGallery.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CarGallery.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}