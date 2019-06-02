using CarGallery.Models;
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

        public ActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Car car)
        {
            if (this.ModelState.IsValid)
            {
                car = this._carService.Create(car);

                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(car);
        }

        public ActionResult Edit(string id)
        {
            var car = this._carService.Get(id);

            if (car != null)
            {
                return this.View(car);
            }

            return this.NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, Car car)
        {
            try
            {
                if (id != car.Id)
                {
                    return this.NotFound();
                }

                if (this.ModelState.IsValid)
                {
                    this._carService.Update(id, car);

                    return this.RedirectToAction(nameof(this.Index));
                }

                return this.View(car);
            }
            catch
            {
                return this.View();
            }
        }

        public ActionResult Details(string id)
        {
            var car = this._carService.Get(id);

            if (car != null)
            {
                return this.View(car);
            }

            return this.NotFound();
        }
    }
}