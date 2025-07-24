using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.EF_core;
using DataAccessLayer.Interfaces;
using Domain;
using DTO;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Interfaces;

namespace ServiceLayer
{
    public class FriendshipService : BaseService<FriendshipEntity>, IFriendshipService 
    {
        private readonly IFriendshipRepo _friendshipRepo;
        
        public FriendshipService(IFriendshipRepo friendshipRepo, AppDbContext dbContext) : base(friendshipRepo, dbContext) 
        {
            _friendshipRepo = friendshipRepo;
        }


        public async Task<List<UserEntity>> GetFriendInvitationByUser_id(Guid id)
        {
            return await _friendshipRepo.GetFriendInvitationByUser_id(id);
        }

        public override async Task<bool> Insert(List<FriendshipEntity> model)
        {
            var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                foreach (var entity in model)
                {
                    entity.friendship_id = Guid.NewGuid();
                    entity.status = "0";
                }
                await _friendshipRepo.Insert(model);

                foreach (var entity in model)
                {
                    var temp = entity.friend_id;
                    entity.friendship_id = Guid.NewGuid();
                    entity.friend_id = entity.user_id;
                    entity.user_id = temp;
                    entity.status = "1";
                }
                await _friendshipRepo.Insert(model);
                await transaction.CommitAsync();
                return true;
            }
            catch (Exception ex) {
                await transaction.RollbackAsync();
                return false;
            }
        }
        public async Task<bool> AcceptFriendship(FriendDTO friendship)
        {
            return await _friendshipRepo.AcceptFriendship(friendship);
        }
        public async Task<bool> NotAcceptFriendship(FriendDTO friendship)
        {
            return await _friendshipRepo.NotAcceptFriendship(friendship);
        }
        public async Task<List<UserEntity>> GetFriendByUser_id(Guid id)
        {
            return await _friendshipRepo.GetFriendByUser_id(id);
        }

    }
}
