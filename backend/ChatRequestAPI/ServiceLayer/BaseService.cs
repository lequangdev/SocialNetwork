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
        string _tableName = "";
        public BaseService(IBaseRepo<TEntity> repo)
        {
            _repo = repo;
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

        public async Task<bool> Insert(List<TEntity> model)
        {
            try
            {
                var result = await _repo.Insert(model);
                return result;
            }
            catch (Exception ex) 
            {
                return false;
            }
        }

        public async Task<bool> UpdateByID(TEntity model, Guid ID)
        {
            try
            {
                var result = await _repo.UpdateByID(model, ID);
                return result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public async Task<bool> DeleteByID(Guid ID)
        {
            try
            {
                var result = await _repo.DeleteByID(ID);
                return result;
            }
            catch (Exception ex)
            {
                return false;
            }
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
