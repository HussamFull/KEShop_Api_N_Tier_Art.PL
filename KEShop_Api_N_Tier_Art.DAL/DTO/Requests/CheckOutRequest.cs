using KEShop_Api_N_Tier_Art.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace KEShop_Api_N_Tier_Art.DAL.DTO.Requests
{
    public class CheckOutRequest
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PaymentMethodEnum PaymentMethod { get; set; }


    }
}
