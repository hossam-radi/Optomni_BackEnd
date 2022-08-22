using System.Text.Json.Serialization;

namespace Optmni.BL.DTOs.ResponseDTOs
{
    public class LookUpResponseDTO
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
