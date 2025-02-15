using Microsoft.AspNetCore.SignalR;

namespace ChatRequestAPI
{
    public class ChatHub : Hub
    {
        // Method to send messages to all connected clients
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
