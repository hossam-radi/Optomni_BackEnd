using Optmni.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Optmni.BL.DTOs.ResponseDTOs
{
    public class OrderListResponseDTO
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("grower_id")]
        public string growerId { get; set; }

        [JsonPropertyName("grower_name")]
        public string GrowerName { get; set; }

        [JsonPropertyName("customer_id")]
        public string CustomerId { get; set; }

        [JsonPropertyName("customer_name")]
        public string CustomerName { get; set; }

        [JsonPropertyName("order_status")]
        public OrderStatus OrderStatus { get; set; }

        [JsonPropertyName("order_details")]
        public List<OrderDetailsResponseDTO> OrderDetails { get; set; }

    }

    public class OrderDetailsResponseDTO
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("product_id")]
        public string ProductId { get; set; }

        [JsonPropertyName("product_name")]
        public string ProductName { get; set; }

        [JsonPropertyName("quantity")]
        public decimal Quantity { get; set; }

        [JsonPropertyName("unit")]
        public ProductUnit ProductUnit { get; set; }

        [JsonPropertyName("total_price")]
        public decimal TotalPrice { get; set; }
    }
}
