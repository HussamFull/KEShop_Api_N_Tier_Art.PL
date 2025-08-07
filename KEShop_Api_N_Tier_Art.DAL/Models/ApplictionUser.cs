using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEShop_Api_N_Tier_Art.DAL.Models
{

    public class ApplicationUser : IdentityUser

    {
        public string FullName { get; set; }
 
        public string? City { get; set; }

        public string? Street { get; set; }

        public string? PostalCode { get; set; }
        public string? Country { get; set; }

        public string? CodeResetPassword { get; set; }

        public DateTime? PasswordResetCodeExpiry { get; set; }



    }
}
