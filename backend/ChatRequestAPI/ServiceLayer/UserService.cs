using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.EF_core;
using DataAccessLayer.Interfaces;
using Domain;
using ServiceLayer.Interfaces;

namespace ServiceLayer
{
    public class UserService : BaseService<UserEntity>, IUserService
    {
        private readonly IUserRepo _UserRepo;
        private readonly AppDbContext _context;

        public UserService(IUserRepo UserRepo, AppDbContext context) : base(UserRepo, context)
        {
            _UserRepo = UserRepo;
            _context = context;
        }
        public async Task<bool> InsertUser(UserEntity user)
        {
            user.user_id = Guid.NewGuid();
            var result = await _UserRepo.InsertUser(user);
            return result;
        }
    }
}
