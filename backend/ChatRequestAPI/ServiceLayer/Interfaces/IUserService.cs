using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace ServiceLayer.Interfaces
{
    public interface IUserService : IBaseService<UserEntity>
    {
        public Task<bool> InsertUser(UserEntity user);
    }
}
