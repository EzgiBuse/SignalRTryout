using Microsoft.AspNetCore.SignalR;

namespace SignalRTryout.Hubs
{
    public class HallowsHub :Hub
    {
        public Dictionary<string,int> GetRaceStatus()
        {
            return SD.HallowRace;
        }
    }
}
