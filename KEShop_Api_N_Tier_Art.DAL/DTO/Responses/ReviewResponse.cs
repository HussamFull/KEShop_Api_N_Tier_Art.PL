﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEShop_Api_N_Tier_Art.DAL.DTO.Responses
{
    public class ReviewResponse
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Comment { get; set; }
        public int Rate { get; set; }
    }
}
