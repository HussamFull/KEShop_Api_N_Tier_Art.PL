using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEShop_Api_N_Tier_Art.DAL.Utils
{
    public interface ISeedData
    {
        Task DataSeedingAsync();

        Task IdentityDataSeedingAsync();

    }
}
