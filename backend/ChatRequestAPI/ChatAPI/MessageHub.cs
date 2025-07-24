using System.Collections.Concurrent;
using Domain;
using DTO;
using Microsoft.AspNetCore.SignalR;
using ServiceLayer.Interfaces;
using Infrastructure.ConnectUser;
using System.Text.RegularExpressions;
using ServiceLayer;

namespace ChatAPI
{

    public class MessageHub : Hub
    {
        private readonly IMessageService _messageService;
        private readonly IFriendshipService _friendshipService;
        public MessageHub( IMessageService messageService, IFriendshipService friendshipService)
        {
            _messageService = messageService;
            _friendshipService = friendshipService;
        }

        public override Task OnConnectedAsync()
        {
            string user_id = Context.UserIdentifier;

            if (!string.IsNullOrEmpty(user_id))
            {
                var connectionId = Context.ConnectionId;
                ConnectUserService.AddConnection(user_id, connectionId);
            }
            int quantity = ConnectUserService.GetTotalConnections();
            int total = ConnectUserService.GetTotalOnlineUsers();
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            string user_id = Context.UserIdentifier;
            var connectionId = Context.ConnectionId;

            if (!string.IsNullOrEmpty(user_id))
            {
                ConnectUserService.RemoveConnection(user_id, connectionId);
            }

            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessageAsync(MessageDTO message)
        {
            try
            {
                var result = await _messageService.SendMessageAsync(message);
                if (result)
                {
                    await Clients.Group($"{message.room_id}").SendAsync("ReceiveMessage", message);
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }

        public async Task LeaveRoomAsyn(Guid? room_id)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"{room_id}");
        }

        public async Task CreateRoomChat(ConnectRoomTwoDTO roomTwoDTO)
        {
            var room_id = await _messageService.CrreateRoomTwoAsync(roomTwoDTO);
            var connectionsOfB = ConnectUserService.GetConnections(roomTwoDTO.receiver_id.ToString());

            foreach (var connectionId in connectionsOfB)
            {
                await Clients.Client(connectionId).SendAsync("ReceiveGroupInvite", room_id);
            }
            await JoinRoomChat([room_id]);
        }

        

        public async Task JoinRoomChat(List<Guid?> room_id)
        {
            foreach (var item in room_id)
            {
                string groupName = $"{item}";
                await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            }
        }
        public async Task SendFriend(FriendshipEntity friendship)
        {
            var connectionsOfB = ConnectUserService.GetConnections(friendship.friend_id.ToString()!);
            var resultDB = await _friendshipService.Insert([friendship]);
            if (resultDB)
            {
                foreach (var connectionId in connectionsOfB)
                {
                    await Clients.Client(connectionId).SendAsync("ReceiveFromSendFriend", resultDB);
                }
            }
            else
            {
                return;
            }
        }

        public async Task AcceptFriendship(FriendDTO friendship)
        {
            var connectionsOfB = ConnectUserService.GetConnections(friendship.friend_id.ToString()!);
            var resultDB = await _friendshipService.AcceptFriendship(friendship);
            if (resultDB)
            {
                foreach (var connectionId in connectionsOfB)
                {
                    await Clients.Client(connectionId).SendAsync("ReceiveFromAcceptFriend", resultDB);
                }
            }
            else
            {
                return;
            }
        }

        public async Task NotAcceptFriendship(FriendDTO friendship)
        {
            var connectionsOfB = ConnectUserService.GetConnections(friendship.friend_id.ToString()!);
            var resultDB = await _friendshipService.NotAcceptFriendship(friendship);
            if (resultDB)
            {
                foreach (var connectionId in connectionsOfB)
                {
                    await Clients.Client(connectionId).SendAsync("ReceiveFromNotAcceptFriend", resultDB);
                }
            }
            else
            {
                return;
            }
        }
    }
}
