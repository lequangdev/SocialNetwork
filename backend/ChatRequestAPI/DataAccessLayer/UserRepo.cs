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
    public class UserRepo :BaseRepo<UserEntity>, IUserRepo
    {
        private readonly AppDbContext _context;
        public UserRepo(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<bool> InsertUser(UserEntity user)
        {
            await _context.user.AddAsync(user);
            int rowsAffected = await _context.SaveChangesAsync();
            return rowsAffected > 0;
        }
    }
}
