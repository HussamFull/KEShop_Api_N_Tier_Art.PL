using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace KEShop_Api_N_Tier_Art.DAL.DTO.Responses
{
    public class ProductResponse
    {
       
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int Quantity { get; set; }

        [JsonIgnore]
        public string MainImage { get; set; }

        //public string MainImageUrl => $"https://localhost:7227/images/{MainImage}";
        public string MainImageUrl { get; set; }

        public List<string> SubImagesUrl { get; set; } = new List<string>();

        public List<ReviewResponse> Reviews { get; set; } = new List<ReviewResponse>();


    }
}
