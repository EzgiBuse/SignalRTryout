using Microsoft.AspNetCore.SignalR;

namespace SignalRTryout.Hubs
{
    public class ChatHub :Hub
    {
        public async Task SendMessageToAll(string user, string message)
        {
            await Clients.All.SendAsync("MessageReceived", user, message);
        }


    }
}
