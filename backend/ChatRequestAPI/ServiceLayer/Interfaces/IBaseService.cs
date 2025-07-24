using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces
{
    public interface IBaseService<TEntity>
    {
        Task<bool> Insert(List<TEntity> model);
        Task<bool> UpdateByID(TEntity model, Guid ID);
        Task<bool> DeleteByID(Guid ID);
        Task<List<TEntity>> GetAll();
        Task<TEntity> GetByID(Guid ID);
    }
}
