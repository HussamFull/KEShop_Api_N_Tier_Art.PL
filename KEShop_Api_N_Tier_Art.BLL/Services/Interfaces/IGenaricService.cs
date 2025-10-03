using KEShop_Api_N_Tier_Art.DAL.DTO.Requests;
using KEShop_Api_N_Tier_Art.DAL.DTO.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEShop_Api_N_Tier_Art.BLL.Services.Interfaces
{
    public interface IGenaricService<TRequest, TResponse, TEntity>
       
    {
      
        int Create(TRequest request);
        IEnumerable<TResponse> GetAll(bool onlyActive = false);
        TResponse? GetById(int id);
        int Update( int id, TRequest request);
        int Delete(int id);

        bool ToggleStatus(int id);

        public IQueryable<TEntity> Entities { get; }


    }
}
