using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.EF_core;
using DataAccessLayer.Interfaces;
using Domain;
using DTO;
using ServiceLayer.Interfaces;

namespace ServiceLayer
{
    public class MessageService : BaseService<MessageEntity>, IMessageService
    {
        private readonly IMessageRepo _messageRepo;
        private readonly IRoom_chatRepo _room_chatRepo;
        private readonly IRoom_userRepo _room_userRepo;

        public MessageService(IMessageRepo MessageRepo, AppDbContext dbContext, IRoom_chatRepo room_ChatRepo, IRoom_userRepo room_UserRepo) : base(MessageRepo, dbContext)
        {

            _messageRepo = MessageRepo;
            _room_chatRepo = room_ChatRepo;
            _room_userRepo = room_UserRepo;
        }
        public async Task<bool> SendMessageAsync(MessageDTO messager)

        {
            MessageEntity message = new MessageEntity();
            message.message_id = Guid.NewGuid();
            message.user_id = messager.user_id;
            message.message_type = messager.message_type;
            message.message_content = messager.message_content;
            message.message_status = 1;
            message.room_id = messager.room_id;
            return await _messageRepo.SendMessageAsync(message);
        }

        public async Task<Guid?> CrreateRoomTwoAsync(ConnectRoomTwoDTO user)
        {
            try
            {

                Room_chatEntity room_Chat = new Room_chatEntity();
                Room_userEntity room_User_send = new Room_userEntity();
                Room_userEntity room_User_receiver = new Room_userEntity();
                room_Chat.room_id = Guid.NewGuid();
                room_Chat.room_key = user.user_id;
                var result = await _room_chatRepo.InsertRoom_chat(room_Chat);
                if (result)
                {
                    room_User_send.user_id = user.user_id;
                    room_User_send.room_id = room_Chat.room_id;
                    room_User_send.room_user_id = Guid.NewGuid();
                    var checkes = await _room_userRepo.Insert([room_User_send]);

                    room_User_receiver.user_id = user.receiver_id;
                    room_User_receiver.room_id = room_Chat.room_id;
                    room_User_receiver.room_user_id = Guid.NewGuid();
                    await _room_userRepo.Insert([room_User_receiver]);

                    return room_Chat.room_id;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex) {
                return null;
            }
        }

        public async Task<Guid?> CheckedRoomUserAsync(ConnectRoomTwoDTO user)
        {
            return await _messageRepo.CheckedRoomUserAsync(user);
        }

        public async Task<List<MessageEntity>> getMessageByRoomId(Guid room_id)
        {
            return await _messageRepo.getMessageByRoomId(room_id);
        }
    }
}
