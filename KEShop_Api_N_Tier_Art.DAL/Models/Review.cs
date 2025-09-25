using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEShop_Api_N_Tier_Art.DAL.Models
{
    public class Review
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Rate { get; set; }
        public string? Comment { get; set; }
        
        public DateTime ReviewDate { get; set; }

        public string? Ordering { get; set; }


    }
}
