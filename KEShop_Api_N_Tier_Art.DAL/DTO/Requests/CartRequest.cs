using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEShop_Api_N_Tier_Art.DAL.DTO.Requests
{
    public class CartRequest
    {
        public int ProductId { get; set; }
        public IFormFile MainImage { get; set; }

    }
}
