using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.EF_core;
using DataAccessLayer.Interfaces;
using Domain;

namespace DataAccessLayer
{
    public class UserRepo : IUserRepo
    {
        private readonly AppDbContext _context;
        UserRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Insert(UserEntity user)
        {
            try
            {
                _context.user.AddAsync(user);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
