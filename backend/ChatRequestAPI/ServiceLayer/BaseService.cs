using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Interfaces;
using ServiceLayer.Interfaces;

namespace ServiceLayer
{
    public abstract class BaseService<TEntity> : IBaseService<TEntity>
    {
        IBaseRepo<TEntity> _repo;

        public BaseService(IBaseRepo<TEntity> repo)
        {
            _repo = repo;
        }

        public async Task<List<TEntity>> GetAll()
        {
            try
            {
                var result = await _repo.GetAll();
                return result;
            }
            catch (Exception ex)
            {
                return new List<TEntity>();
            }
        }
    }
}
