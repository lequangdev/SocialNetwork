using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.EF_core;
using DataAccessLayer.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;


namespace DataAccessLayer
{
    public abstract class BaseRepo<TEntity> : IBaseRepo<TEntity> where TEntity : class
    {
        private readonly AppDbContext _context;
        string _tableName = "";
        public BaseRepo(AppDbContext context)
        {
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
            if (model == null)
            {
                return false;
            }
            else
            {
                await _context.Set<TEntity>().AddRangeAsync(model);
                int rowsAffected = await _context.SaveChangesAsync();
                return rowsAffected > 0;
            }
        }
        public virtual async Task<bool> UpdateByID(TEntity model, Guid ID)
        {
            var existingEntity = await _context.Set<TEntity>().FindAsync(ID);
            if (existingEntity == null)
            { return false; }

            foreach (var property in typeof(TEntity).GetProperties())
            {
                var newValue = property.GetValue(model);
                if (newValue != null)
                {
                    property.SetValue(existingEntity, newValue);
                }
            }

            await _context.SaveChangesAsync();
            return true;

        }

        public virtual async Task<bool> DeleteByID(Guid ID)
        {
            var Model = await _context.Set<TEntity>().FindAsync(ID);
            if (Model == null)
            {
                return false; 
            }
            else
            {
                _context.Set<TEntity>().Remove(Model);
                await _context.SaveChangesAsync();
                return true;
            }    
        }

        public virtual async Task<List<TEntity>> GetAll()
        {
            var result = await _context.Set<TEntity>().ToListAsync();
            return result;
        }
    }
}
