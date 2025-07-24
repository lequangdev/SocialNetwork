using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.EF_core;
using DataAccessLayer.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Interfaces;

namespace ServiceLayer
{
    public class UserService : BaseService<UserEntity>, IUserService
    {
        private readonly IUserRepo _UserRepo;


        public UserService(IUserRepo UserRepo, AppDbContext dbContext) : base(UserRepo, dbContext)
        {
            _UserRepo = UserRepo;
        }
        public async Task<bool> InsertUser(UserEntity user)
        {
            user.user_id = Guid.NewGuid();
            return await _UserRepo.InsertUser(user);
        }
        public async Task<UserEntity> LoginUser(string user_account, string user_password)
        {
            UserEntity result = await _UserRepo.LoginUser(user_account);
            if (result.user_account == user_account && result.user_password == user_password)
            {
                result.user_password = null;
                return result;
            }
            else {
                return null;
            }
            
        }

        public override async Task<List<UserEntity>> GetAll()
        {
            var result = await _UserRepo.GetAll();
            result.ForEach(user =>
            {
                user.user_password = null;
            });
            return result;
        }

        public async Task<List<UserEntity>> GetUserByFullname(string payload)
        {
            return await _UserRepo.GetUserByFullname(payload);
        }
    }
}
