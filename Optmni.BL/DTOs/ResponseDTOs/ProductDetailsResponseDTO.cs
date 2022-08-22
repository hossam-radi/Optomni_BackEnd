using Optmni.Utilities.Enums;
using System.Text.Json.Serialization;

namespace Optmni.BL.DTOs.ResponseDTOs
{
    public class ProductDetailsResponseDTO
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("pic_url")]
        public string PictureUrl { get; set; }

        [JsonPropertyName("region_id")]
        public int? RegionId { get; set; }

        [JsonPropertyName("region")]
        public string? RegionName { get; set; }

        [JsonPropertyName("product_type")]
        public ProductType ProductType { get; set; }

        [JsonPropertyName("Price")]
        public decimal Price { get; set; }

    }
}
