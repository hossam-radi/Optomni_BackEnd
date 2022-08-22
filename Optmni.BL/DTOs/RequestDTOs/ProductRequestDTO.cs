using Optmni.Utilities.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Optmni.BL.DTOs.RequestDTOs
{
    public class ProductRequestDTO
    {

        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("name")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        [Required(ErrorMessage = "Description is required")]
        public string description { get; set; }

        [JsonPropertyName("region_id")]
        [Required(ErrorMessage = "Region Id is required")]
        public int RegionId { get; set; }

        [JsonPropertyName("price")]
        [Required(ErrorMessage = "Price is required")]
        public decimal Price { get; set; }

        [JsonPropertyName("product_type")]
        public ProductType ProductType { get; set; }
    }
}
