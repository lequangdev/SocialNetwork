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

namespace DataAccessLayer
{
    public class FriendshipRepo: BaseRepo<FriendshipEntity>, IFriendshipRepo
    {
        public FriendshipRepo(AppDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<List<UserEntity>> GetFriendInvitationByUser_id(Guid id)
        {
            var friendships = await _dbContext.friendship
                .Where(f => (f.user_id == id) && f.status == "1")
                .Include(f => f.user)
                .Include(f => f.friend)
                .ToListAsync();

            var friendList = friendships
                .Select(f => f.user_id == id ? f.friend : f.user)
                .Where(u => u != null)
                .Distinct()
                .ToList();
            return friendList!;
        }

        public async Task<bool> AcceptFriendship(FriendDTO friendship)
        {
            var friendships = await _dbContext.friendship
            .Where(f =>
                (f.user_id == friendship.user_id && f.friend_id == friendship.friend_id) ||
                (f.user_id == friendship.friend_id && f.friend_id == friendship.user_id))
            .ToListAsync();

                if (!friendships.Any())
                    return false;

                foreach (var f in friendships)
                {
                    f.status = "2";
                }

                await _dbContext.SaveChangesAsync();
                return true;
        }

        public async Task<List<UserEntity>> GetFriendByUser_id(Guid id)
        {
            var friendships = await _dbContext.friendship
                .Where(f => (f.user_id == id || f.friend_id == id) && f.status == "2")
                .Include(f => f.user)
                .Include(f => f.friend)
                .ToListAsync();

            var friendList = friendships
                .Select(f => f.user_id == id ? f.friend : f.user)
                .Where(u => u != null)
                .Distinct()
                .ToList();
            return friendList!;
        }

        public async Task<bool> NotAcceptFriendship(FriendDTO friendship)
        {
            var friendships = await _dbContext.friendship
            .Where(f =>
                (f.user_id == friendship.user_id && f.friend_id == friendship.friend_id) ||
                (f.user_id == friendship.friend_id && f.friend_id == friendship.user_id))
            .ToListAsync();

            if (!friendships.Any())
                return false;

            foreach (var f in friendships)
            {
                f.status = "3";
            }

            await _dbContext.SaveChangesAsync();
            return true;
        }
    }

}
