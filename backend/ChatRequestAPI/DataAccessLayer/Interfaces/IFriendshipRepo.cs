using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using DTO;

namespace DataAccessLayer.Interfaces
{
    public interface IFriendshipRepo : IBaseRepo<FriendshipEntity>
    {
        Task<List<UserEntity>> GetFriendInvitationByUser_id(Guid id);
        Task<bool> AcceptFriendship(FriendDTO friendship);
        Task<List<UserEntity>> GetFriendByUser_id(Guid id);
        Task<bool> NotAcceptFriendship(FriendDTO friendship);
    }
}
