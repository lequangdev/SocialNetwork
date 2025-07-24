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
    public class UserRepo : BaseRepo<UserEntity>, IUserRepo
    {
        public UserRepo(AppDbContext dbContext) : base(dbContext)
        {

        }
        public async Task<bool> InsertUser(UserEntity user)
        {
            await _dbContext.user.AddAsync(user);
            int rowsAffected = await _dbContext.SaveChangesAsync();
            return rowsAffected > 0;
        }
        public async Task<UserEntity> LoginUser(string user_account)
        {
            var result = await _dbContext.user.FirstOrDefaultAsync(e => e.user_account == user_account);
            return result!;
        }
        public async Task<List<UserEntity>> GetUserByFullname(string payload)
        {
            return await _dbContext.user.Where(u => EF.Functions.Like(u.user_fullName, $"%{payload}%")).ToListAsync();
        }

    }
}
