using Microsoft.AspNetCore.SignalR;

namespace SignalRTryout.Hubs
{
    public class NotificationHub : Hub
    {
        public static int notificationCounter = 0;
        public static List<string> messages = new();

        public async Task SendMessage(string message) { if(!string.IsNullOrEmpty(message))
            { messages.Add(message); 
                notificationCounter++;
                await LoadMessages();
            } }

        public async Task LoadMessages()
        {
            await Clients.All.SendAsync("LoadNotification", messages,notificationCounter);
        }

    }
}
