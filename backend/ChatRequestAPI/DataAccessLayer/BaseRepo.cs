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
        public async Task<List<TEntity>> GetAll()
        {
            try
            {
                var result = await _context.Set<TEntity>().ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                return new List<TEntity>();
            }
        }
    }
}
