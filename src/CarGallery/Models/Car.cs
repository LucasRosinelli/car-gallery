using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace CarGallery.Models
{
    public class Car
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("Brand")]
        [BsonRequired]
        [Required]
        public string Brand { get; set; }
        [BsonElement("Model")]
        [BsonRequired]
        [Required]
        public string Model { get; set; }
        [BsonElement("Year")]
        [BsonRequired]
        [Required]
        public int Year { get; set; }
        [BsonElement("Currency")]
        [BsonRequired]
        [Required]
        public string Currency { get; set; }
        [BsonElement("Price")]
        [BsonRepresentation(BsonType.Decimal128)]
        [BsonRequired]
        [Required]
        public decimal Price { get; set; }
        [BsonElement("ImageUrl")]
        [Display(Name = "Photo")]
        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }
        [BsonIgnore]
        [Display(Name = "Photo")]
        [DataType(DataType.ImageUrl)]
        public IFormFile ImageFile { get; set; }
    }
}