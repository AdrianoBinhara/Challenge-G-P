using System;
using System.Text.Json.Serialization;

namespace GPInventory.Models
{
    public class ItemsModel
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("category")]
        public string Category { get; set; }

        [JsonPropertyName("isupdate")]
        public int IsUpdated { get; set; }
    }

}
