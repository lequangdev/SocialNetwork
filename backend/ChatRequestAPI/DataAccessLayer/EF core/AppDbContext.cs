using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.EF_core
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<UserEntity> user { get; set; }
        public DbSet<MessageEntity> Message { get; set; }
        public DbSet<Room_chatEntity> room_chat { get; set; }
        public DbSet<Room_userEntity> room_user { get; set; }
        public DbSet<FriendshipEntity> friendship { get; set; }
    }
}
