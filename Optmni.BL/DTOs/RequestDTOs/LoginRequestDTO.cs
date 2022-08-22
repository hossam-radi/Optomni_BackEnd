using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Optmni.BL.DTOs.RequestDTOs
{
    public class LoginRequestDTO
    {
        [JsonPropertyName("email")]
        [Required(ErrorMessage = "{0}_IS_REQUIRED")]
        [EmailAddress(ErrorMessage = "{0}_IS_NOT_VALID_FORMAT")]
        [Display(Name = "EMAIL")]
        public string Email { get; set; }

        [JsonPropertyName("password")]
        [Required(ErrorMessage = "{0}_IS_REQUIRED")]
        [Display(Name = "PASSWORD")]
        public string Password { get; set; }
    }
}
