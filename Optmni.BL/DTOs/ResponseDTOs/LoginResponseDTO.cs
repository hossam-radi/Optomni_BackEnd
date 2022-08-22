using Optmni.Utilities.Enums;
using System.Text.Json.Serialization;

namespace Optmni.BL.DTOs.ResponseDTOs
{
    public class LoginResponseDTO
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }

        [JsonPropertyName("user_name")]
        public string UserName { get; set; }

        [JsonPropertyName("user_type")]
        public UserTypes UserType { get; set; }
    }
}
