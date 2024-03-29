using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRTryout.Data;
using SignalRTryout.Hubs;
using SignalRTryout.Models;
using System.Diagnostics;

namespace SignalRTryout.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHubContext<HallowsHub> _hallowHub;
        private readonly IHubContext<OrderHub> _orderHub;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger ,
            IHubContext<HallowsHub> hallowhub, ApplicationDbContext context, IHubContext<OrderHub> orderHub)
        {
            _logger = logger;
            _hallowHub = hallowhub;
            _context = context;
            _orderHub = orderHub;
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

        [ActionName("Order")]
        public async Task<IActionResult> Order()
        {
            string[] name = { "Bhrugen", "Ben", "Jess", "Laura", "Ron" };
            string[] itemName = { "Food1", "Food2", "Food3", "Food4", "Food5" };

            Random rand = new Random();
            // Generate a random index less than the size of the array.  
            int index = rand.Next(name.Length);

            Order order = new Order()
            {
                Name = name[index],
                ItemName = itemName[index],
                Count = index
            };

            return View(order);
        }

        [ActionName("Order")]
        [HttpPost]
        public async Task<IActionResult> OrderPost(Order order)
        {

            _context.Orders.Add(order);
            _context.SaveChanges();
            await _orderHub.Clients.All.SendAsync("newOrder");
            return RedirectToAction(nameof(Order));
        }
        [ActionName("OrderList")]
        public async Task<IActionResult> OrderList()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetAllOrder()
        {
            var productList = _context.Orders.ToList();
            return Json(new { data = productList });
        }
    }
}
