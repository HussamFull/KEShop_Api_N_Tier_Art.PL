using KEShop_Api_N_Tier_Art.BLL.Services.Interfaces;
using KEShop_Api_N_Tier_Art.DAL.Models;
using KEShop_Api_N_Tier_Art.DAL.Repositories.Classes;
using KEShop_Api_N_Tier_Art.DAL.Repositories.Interfaces;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEShop_Api_N_Tier_Art.BLL.Services.Classes
{
    public class GenericService<TRequest, TResponse, TEntity> : IGenaricService<TRequest, TResponse, TEntity>
    where TEntity : BaseModel
    {
        // Assuming you have a repository injected here
        private readonly IGenericRepository<TEntity> _repository;
        public GenericService(IGenericRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public int Create(TRequest request)
        {
            var entity = request.Adapt< TEntity > ();

            return _repository.Add(entity);

        }

        public int Delete(int id)
        {
            var entity = _repository.GetById(id);
            if (entity == null) return 0;
            return _repository.Remove(entity);


        }

        public IEnumerable<TResponse> GetAll()
        {
            var entities = _repository.GetAll();
           // if (entities is null || !entities.Any()) return Enumerable.Empty<TResponse>();
            return entities.Adapt<IEnumerable<TResponse>>();

        }

        public TResponse? GetById(int id)
        {
            var entity = _repository.GetById(id);
            if (entity is null) return default;
            return entity.Adapt<TResponse>();

        }

        public bool ToggleStatus(int id)
        {
            var entity = _repository.GetById(id);
            if (entity is null) return false;
            entity.Status = entity.Status == Status.Active ? Status.Inactive : Status.Active; // Assuming Status is a property in BaseModel
            _repository.Update(entity);
            return true;

        }

        public int Update(int id, TRequest request)
        {
            var entity = _repository.GetById(id);
            if (entity is null) return 0;
            // Map the request to the existing entity
            var updateEntity =  request.Adapt(entity);
            // Update the entity in the repository
            return _repository.Update(updateEntity);

        }
    }
}
