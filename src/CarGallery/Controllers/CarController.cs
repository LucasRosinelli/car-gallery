using CarGallery.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarGallery.Controllers
{
    public class CarController : Controller
    {
        private readonly CarService _carService;

        public CarController(CarService carService)
        {
            this._carService = carService;
        }

        public ActionResult Index()
        {
            return this.View(this._carService.Get());
        }
    }
}