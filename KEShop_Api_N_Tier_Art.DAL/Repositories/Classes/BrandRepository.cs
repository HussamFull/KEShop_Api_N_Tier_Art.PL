using KEShop_Api_N_Tier_Art.DAL.Data;
using KEShop_Api_N_Tier_Art.DAL.Models;
using KEShop_Api_N_Tier_Art.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace KEShop_Api_N_Tier_Art.DAL.Repositories.Classes
{

    // This class is a placeholder for the Category repository. هون بنعلمل كرد للكاتيجوري 
    public class BrandRepository : GenericRepository<Brand>, IBrandRepository
    {

        public BrandRepository(ApplictionDbContext context) : base(context)
        {
            
        }

    }
}
