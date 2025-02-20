using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Interfaces;
using Domain;
using ServiceLayer.Interfaces;

namespace ServiceLayer
{
    public class UserService : BaseService<UserEntity>, IUserService
    {
        private readonly IUserRepo _UserRepo;
        public UserService(IUserRepo UserRepo) : base(UserRepo)
        {
            _UserRepo = UserRepo;
        }
        public async Task<bool> InsertUser(UserEntity user)
        {
            try
            {
                user.user_id = Guid.NewGuid();
                var result = await _UserRepo.InsertUser(user);
                return result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
