using HotelArc.Kernel.Entities;
using HotelArc.MVCUI.Utils;
using HotelArc.Process.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelArc.MVCUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class RoomsController : Controller
    {
        private readonly IRoomService _roomService;

        public RoomsController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        // GET: RoomsController
        public async Task<ActionResult> Index()
        {
            IEnumerable<Room> rooms = await _roomService.GetRoomsByIncludeAsync();

            return View(rooms);
        }

        // GET: RoomsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: RoomsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Room collection, IFormFile? RoomImage)
        {
            try
            {
                if (RoomImage != null)
                {
                    collection.RoomImage = await FileHelper.FileLoaderAsync(RoomImage);
                }

                await _roomService.AddAsync(collection);


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RoomsController/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {


            return View(await _roomService.FindAsync(id));
        }

        // POST: RoomsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, Room collection, IFormFile? RoomImage)
        {
            try
            {
                if (RoomImage != null)
                {
                    collection.RoomImage = await FileHelper.FileLoaderAsync(RoomImage);
                }

                await _roomService.UpdateAsync(collection,id);
                
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RoomsController/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            Room room = await _roomService.FindAsync(id);

            return View(room);

        }

        // POST: RoomsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id, Room collection)
        {
            try
            {
                await _roomService.SoftDeleteAsync(collection.RoomId);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
