using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.EF_core;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Interfaces;

namespace ServiceLayer
{
    public abstract class BaseService<TEntity> : IBaseService<TEntity>
    {
        IBaseRepo<TEntity> _repo;
        protected readonly AppDbContext _dbContext;
        string _tableName = "";
        public BaseService(IBaseRepo<TEntity> repo, AppDbContext dbContext)
        {
            _repo = repo;
            _dbContext = dbContext;
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

        private void AssignNewGuidToPrimaryKey(TEntity entity)
        {
            var entityType = _dbContext.Model.FindEntityType(typeof(TEntity));
            var primaryKey = entityType?.FindPrimaryKey();

            if (primaryKey == null) return;

            foreach (var property in primaryKey.Properties)
            {
                var propertyInfo = typeof(TEntity).GetProperty(property.Name);
                propertyInfo?.SetValue(entity, Guid.NewGuid());
            }
        }

        public virtual async Task<bool> Insert(List<TEntity> model)
        {
            
            foreach (var entity in model)
            {
                AssignNewGuidToPrimaryKey(entity);
            }
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

        public async Task<TEntity> GetByID(Guid ID)
        {
           return await _repo.GetByID(ID);
        }


    }
}
