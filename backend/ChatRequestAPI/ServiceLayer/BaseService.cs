using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.EF_core;
using DataAccessLayer.Interfaces;
using ServiceLayer.Interfaces;

namespace ServiceLayer
{
    public abstract class BaseService<TEntity> : IBaseService<TEntity>
    {
        IBaseRepo<TEntity> _repo;
        private readonly AppDbContext _context;
        string _tableName = "";
        public BaseService(IBaseRepo<TEntity> repo, AppDbContext context)
        {
            _repo = repo;
            _context = context;
            _tableName = GetTableName(typeof(TEntity).Name);
        }
        public static string GetTableName(string tableName)
        {
            string suffix = "Entity";
            if (tableName.EndsWith(suffix))
            {
                return tableName.Substring(0, tableName.Length - suffix.Length);
            }
            return tableName;
        }

        public virtual async Task<bool> Insert(List<TEntity> model)
        {
            var result = await _repo.Insert(model);
            return result;
        }

        public virtual async Task<bool> UpdateByID(TEntity model, Guid ID)
        {
            var result = await _repo.UpdateByID(model, ID);
            return result;
        }


        public virtual async Task<bool> DeleteByID(Guid ID)
        {
            var result = await _repo.DeleteByID(ID);
            return result;
        }

        public virtual async Task<List<TEntity>> GetAll()
        {
            var result = await _repo.GetAll();
            return result;
        }

    }
}
