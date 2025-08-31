using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace KEShop_Api_N_Tier_Art.DAL.DTO.Responses
{
    public class CartResponse
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
        public decimal TotalPrice => Price * Count;

        [JsonIgnore]
        public string MainImage { get; set; }
        public string MainImageUrl => $"https://localhost:7227/images/{MainImage}";
       
    }
}
