using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEShop_Api_N_Tier_Art.DAL.DTO.Responses
{
    public class CartSummaryResponse
    {
        public List<CartResponse> Items { get; set; } = new List<CartResponse>();
        public decimal CartToltal => Items.Sum(i => i.TotalPrice);
    }
}
