using CarGallery.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace CarGallery.Services
{
    public class CarService
    {
        private readonly string ConfigConnectionString = "CarGalleryDb";
        private readonly string ConfigDatabaseName = "CarGalleryDb";
        private readonly string ConfigCarCollectionName = "Cars";

        private readonly IMongoCollection<Car> cars;

        public CarService(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetConnectionString(this.ConfigConnectionString));
            var database = client.GetDatabase(this.ConfigDatabaseName);
            this.cars = database.GetCollection<Car>(this.ConfigCarCollectionName);
        }

        public List<Car> Get()
        {
            return this.cars.Find(c => true).ToList();
        }

        public Car Get(string id)
        {
            return this.cars.Find(c => c.Id == id).FirstOrDefault();
        }

        public Car Create(Car car)
        {
            this.cars.InsertOne(car);
            return car;
        }

        public void Update(string id, Car car)
        {
            this.cars.ReplaceOne(c => c.Id == id, car);
        }

        public void Remove(string id)
        {
            this.cars.DeleteOne(c => c.Id == id);
        }

        public void Remove(Car car)
        {
            this.cars.DeleteOne(c => c.Id == car.Id);
        }
    }
}