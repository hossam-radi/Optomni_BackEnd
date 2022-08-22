using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Optmni.BL.DTOs.RequestDTOs
{
    public class OrderRequestDTO
    {
        [JsonPropertyName("date_birth")]
        [Required(ErrorMessage = "Date of birth is required")]
        public DateTime DateOfBirth { get; set; }

        [JsonPropertyName("pic_profile")]
        public string? PicProfile { get; set; }

        [JsonPropertyName("nationality_id")]
        [Required(ErrorMessage = "nationality is required")]
        public int? NationalityId { get; set; }

        [JsonPropertyName("Region_ids")]
        [Required(ErrorMessage = "Region is Required")]
        public List<int> RegionIds { get; set; }

        [JsonPropertyName("iban_number")]
        [Required(ErrorMessage = "iban number is required")]
        public string IbanNumber { get; set; }

        [JsonPropertyName("experience")]
        public string? Experience { get; set; }

     

        [JsonPropertyName("bank_acoount_name")]
        public string? BankAccountName { get; set; }

      

        [JsonPropertyName("fee_per_hour")]
        [Required(ErrorMessage = "fee per hour is required")]
        public decimal FeePerHour { get; set; }
    }
}
