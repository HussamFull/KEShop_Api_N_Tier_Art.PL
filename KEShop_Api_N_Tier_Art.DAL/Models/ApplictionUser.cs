using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEShop_Api_N_Tier_Art.DAL.Models
{
<<<<<<< HEAD
    public class ApplicationUser : IdentityUser
=======
    public class ApplictionUser : IdentityUser
>>>>>>> 76abe613bff7cfd810f1158b19341d7351c6d730
    {
        public string FullName { get; set; }
 
        public string? City { get; set; }

        public string? Street { get; set; }

        public string? PostalCode { get; set; }
        public string? Country { get; set; }


    }
}
