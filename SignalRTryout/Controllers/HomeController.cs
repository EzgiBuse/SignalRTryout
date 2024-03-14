using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRTryout.Hubs;
using SignalRTryout.Models;
using System.Diagnostics;

namespace SignalRTryout.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHubContext<HallowsHub> _hallowHub;

        public HomeController(ILogger<HomeController> logger , IHubContext<HallowsHub> hallowhub)
        {
            _logger = logger;
            _hallowHub = hallowhub;
        }

        public async Task<IActionResult> Hallows(string type)
        {
            if (SD.HallowRace.ContainsKey(type))
            {
                SD.HallowRace[type]++;
            }

            await _hallowHub.Clients.All.
                SendAsync("UpdateHallowCount", SD.HallowRace[SD.Cloak],
                SD.HallowRace[SD.Stone],
                SD.HallowRace[SD.Wand]);

            return Accepted();
        }
        public IActionResult Index()
        {

            return View("MessagingView");
            
        }
        //public IActionResult Notification()
        //{

        //    return View("Notification");
        //}


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
