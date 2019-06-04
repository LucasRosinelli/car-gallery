using CarGallery.Models;
using CarGallery.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CarGallery.Controllers
{
    public class CarController : Controller
    {
        private readonly IHostingEnvironment _environment;
        private readonly IConfiguration _configuration;
        private readonly string _carImageUploadLocationSettingValue;
        private readonly string _carImageUploadLocation;
        private readonly CarService _carService;

        public CarController(IHostingEnvironment environment, IConfiguration configuration, CarService carService)
        {
            this._environment = environment;
            this._configuration = configuration;
            this._carImageUploadLocationSettingValue = this._configuration.GetValue<string>("CarImageUploadLocation");
            this._carImageUploadLocation = Path.Combine(this._environment.WebRootPath, this._carImageUploadLocationSettingValue);
            Directory.CreateDirectory(this._carImageUploadLocation);
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
                if (car.ImageFile != null && !string.IsNullOrWhiteSpace(car.ImageFile.FileName))
                {
                    var uniqueFileName = this.GetUniqueFileName(car.ImageFile.FileName);
                    var contentType = car.ImageFile.ContentType;
                    var filePath = Path.Combine(this._carImageUploadLocation, uniqueFileName);
                    car.ImageFile.CopyTo(new FileStream(filePath, FileMode.Create));
                    car.ImageUrl = Path.Combine("/", this._carImageUploadLocationSettingValue, uniqueFileName);
                }
                car = this._carService.Create(car);

                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(car);
        }

        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            var smallId = Guid.NewGuid().ToString();
            smallId = smallId.Substring(smallId.LastIndexOf('-') + 1);
            return $"{Path.GetFileNameWithoutExtension(fileName)}_{smallId}{Path.GetExtension(fileName)}";
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

        public ActionResult Details(string id)
        {
            var car = this._carService.Get(id);

            if (car != null)
            {
                return this.View(car);
            }

            return this.NotFound();
        }

        public ActionResult Delete(string id)
        {
            var car = this._carService.Get(id);

            if (car != null)
            {
                return this.View(car);
            }

            return this.NotFound();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var car = this._carService.Get(id);

            if (car != null)
            {
                this._carService.Remove(car);

                return this.RedirectToAction(nameof(this.Index));
            }

            return this.NotFound();
        }
    }
}