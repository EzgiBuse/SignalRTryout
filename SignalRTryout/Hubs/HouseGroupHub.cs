using Microsoft.AspNetCore.SignalR;
using System.ComponentModel;

namespace SignalRTryout.Hubs
{
    public class HouseGroupHub :Hub
    {
        public static List<string> GroupsJoined { get; set; } = new List<string>();

        public async Task JoinHouse(string hauseName)
        {
            if(!GroupsJoined.Contains(Context.ConnectionId+":"+hauseName))
            {
              
                GroupsJoined.Add(Context.ConnectionId + ":" + hauseName);
                string houselist = ""; 
                    foreach(var i in GroupsJoined)
                {
                    if(i.Contains(Context.ConnectionId
                        ))
                    {
                        houselist += i.Split(":")[1] + " ";
                    }
                }
               await Clients.Caller.SendAsync("subscriptionStatus", houselist,true);
                await Groups.AddToGroupAsync(Context.ConnectionId, hauseName);
            }
        }

        public async Task LeaveHouse(string hauseName)
        {
            if (GroupsJoined.Contains(Context.ConnectionId + ":" + hauseName))
            {

                GroupsJoined.Remove(Context.ConnectionId + ":" + hauseName);
                string houselist = "";
                foreach (var i in GroupsJoined)
                {
                    if (i.Contains(Context.ConnectionId
                        ))
                    {
                        houselist += i.Split(":")[1] + " ";
                    }
                }
                await Clients.Caller.SendAsync("subscriptionStatus", houselist, false);
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, hauseName);
            }
        }
    }
}
