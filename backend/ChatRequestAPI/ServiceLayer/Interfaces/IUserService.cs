using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace ServiceLayer.Interfaces
{
    public interface IUserService : IBaseService<UserEntity>
    {
        Task<bool> InsertUser(UserEntity user);
        Task<UserEntity> LoginUser(string user_account, string user_password);
        Task<List<UserEntity>> GetUserByFullname(string payload);
    }
}
