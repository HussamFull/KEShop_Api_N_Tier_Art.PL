using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEShop_Api_N_Tier_Art.DAL.DTO.Requests
{
    public class RegisterRequest
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        //public string ConfirmPassword { get; set; }
     
        //public string Address { get; set; }
        //public string City { get; set; }
        //public string Country { get; set; }
        //public string PostalCode { get; set; }
    }
}
