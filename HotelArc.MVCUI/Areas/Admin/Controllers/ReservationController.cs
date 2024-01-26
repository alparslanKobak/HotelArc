using HotelArc.Kernel.Entities;
using HotelArc.Process.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace HotelArc.MVCUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ReservationController : Controller
    {
        private readonly IReservationService _reservationService;
        private readonly IRoomService _roomService;
        private readonly IAppUserService _appUserService;

        public ReservationController(IReservationService reservationService, IRoomService roomService, IAppUserService appUserService)
        {
            _reservationService = reservationService;
            _roomService = roomService;
            _appUserService = appUserService;
        }

        // GET: ReservationController
        public async Task<ActionResult> Index()
        {
            IEnumerable<Reservation> reservations = await  _reservationService.GetReservationsByIncludeAsync();
            return View(reservations);
        }

        // GET: ReservationController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ReservationController/Create
        public async Task<ActionResult> Create()
        {
            ViewBag.Rooms = new SelectList(await _roomService.GetRoomsByIncludeAsync(), "RoomId", "RoomNumber");
            ViewBag.Users = new SelectList(await _appUserService.GetAppUsersByIncludeAsync(), "AppUserId", "UserName");
            return View();
        }

        // POST: ReservationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Reservation collection)
        {
            try
            {
                bool IsRoomReserved = await _reservationService.IsRoomReserved(collection.RoomId, collection.CheckIn, collection.CheckOut);
                if (IsRoomReserved)
                {
                    TempData["Message"] = "<div class='alert alert-danger'>This room is already reserved for this date range.</div>";
                    return RedirectToAction(nameof(Create));
                }

                await _reservationService.AddAsync(collection);


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ReservationController/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            ViewBag.Rooms = new SelectList(await _roomService.GetRoomsByIncludeAsync(), "RoomId", "RoomNumber");

            ViewBag.Users = new SelectList(await _appUserService.GetAppUsersByIncludeAsync(), "AppUserId", "UserName");

            Reservation reservation = await _reservationService.GetReservationByIncludeAsync(id);

            return View(reservation);
          
        }

        // POST: ReservationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, Reservation collection)
        {
            try
            {
                bool IsRoomReserved = await _reservationService.IsRoomReserved(collection.RoomId, collection.CheckIn, collection.CheckOut, id);

                if (IsRoomReserved)
                {
                    TempData["Message"] = "<div class='alert alert-danger'>This room is already reserved for this date range.</div>";
                    return RedirectToAction(nameof(Create));
                    
                }
                await _reservationService.UpdateAsync(collection,id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.Rooms = new SelectList(await _roomService.GetRoomsByIncludeAsync(), "RoomId", "RoomNumber");
                ViewBag.Users = new SelectList(await _appUserService.GetAppUsersByIncludeAsync(), "AppUserId", "UserName");
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: ReservationController/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
          Reservation model =   await _reservationService.GetReservationByIncludeAsync(id);

            return View(model);
        }

        // POST: ReservationController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id, Reservation collection)
        {
            try
            {
                await _reservationService.SoftDeleteAsync(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
