using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEShop_Api_N_Tier_Art.DAL.Models
{
    public class Brand : BaseModel
    {
      
        public string Name { get; set; }

        public string MainImage { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public List<Product> Products { get; set; } = new List<Product>();


    }
}
