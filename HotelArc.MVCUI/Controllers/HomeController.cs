using HotelArc.Kernel.Entities;
using HotelArc.MVCUI.Models;
using HotelArc.Process.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HotelArc.MVCUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRoomService _roomService;
        public HomeController(ILogger<HomeController> logger, IRoomService roomService)
        {
            _logger = logger;
            _roomService = roomService;
        }

        public async Task<IActionResult> Index()
        {
           IEnumerable<Room> rooms = await _roomService.GetRoomsByIncludeAsync();

            return View(rooms);
        }

     
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
