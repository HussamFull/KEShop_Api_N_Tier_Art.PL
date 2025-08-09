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
       
        public string Name { get; set; }
        public string Description { get; set; }

        [JsonIgnore]
        public string MainImage { get; set; }
        public string MainImageUrl => $"https://localhost:7227/images/{MainImage}";


    }
}
