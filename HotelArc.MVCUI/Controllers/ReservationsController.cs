using HotelArc.Kernel.Entities;
using HotelArc.MVCUI.Models;
using HotelArc.Process.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HotelArc.MVCUI.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly IReservationService _reservationService;
        private readonly IRoomService _roomService;
        private readonly IAppUserService _appUserService;

        public ReservationsController(IReservationService reservationService, IRoomService roomService, IAppUserService appUserService)
        {
            _reservationService = reservationService;
            _roomService = roomService;
            _appUserService = appUserService;
        }

        // GET: ReservationsController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ReservationsController/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                TempData["Message"] = "<div class='alert alert-danger'>You must be logged in to view this page.</div>";
                return RedirectToAction("Login", "Auth");
            }

            Room room = await _roomService.GetRoomByIncludeAsync(id);

            RoomDetailViewModel model = new RoomDetailViewModel
            {
                room = room,
                roomId = id,
              
            };
            return View(model);
            
        }

        

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Details(Guid roomId, ReservationViewModel reservationViewModel)
        {
            try
            {
                // Email adresini önce bir değişkende sakla
                var emailClaim = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
                if (string.IsNullOrEmpty(emailClaim))
                {
                    // Email claim'i yoksa hata döndür veya uygun bir şekilde ele al
                    TempData["Message"] = "<div class='alert alert-danger'>User email not found.</div>";
                    return RedirectToAction("Details", "Rooms", new { id = roomId });
                }

                // Artık direkt string olarak email'i kullanabiliriz
                AppUser user = await _appUserService.GetAppUserByIncludeAsync(x => x.Email == emailClaim);
                if (user == null)
                {
                    // Kullanıcı bulunamadıysa hata döndür veya uygun bir şekilde ele al
                    TempData["Message"] = "<div class='alert alert-danger'>User not found.</div>";
                    return RedirectToAction("Details", "Rooms", new { id = roomId });
                }

                // User nesnesini kullanarak reservation oluştur
                Reservation reservation = new Reservation
                {
                    RoomId = roomId,
                    AppUserId = user.AppUserId,
                    CheckIn = reservationViewModel.CheckIn,
                    CheckOut = reservationViewModel.CheckOut
                };

                bool IsAvailable = await _reservationService.IsRoomReserved(reservation.RoomId, reservation.CheckIn, reservation.CheckOut, reservation.AppUserId);

                if (IsAvailable)
                {
                    TempData["Message"] = "<div class='alert alert-danger'>This room is already reserved for the selected dates.</div>";
                    return RedirectToAction("Details", "Rooms", new { id = roomId });
                }
                await _reservationService.AddAsync(reservation);
                TempData["Message"] = "<div class='alert alert-success'>Reservation has been successfully created.</div>";
                return RedirectToAction("Index","Home");
            }
            catch(Exception e)
            {
                TempData["Message"] = $"<div class='alert alert-danger'>An error occurred while creating the reservation. {e.Message} + {e.InnerException}</div>";
                return RedirectToAction("Details", "Rooms", new { id = roomId });
            }
        }


    }
}
