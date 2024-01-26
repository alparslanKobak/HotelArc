using HotelArc.Kernel.Entities;

namespace HotelArc.MVCUI.Models
{
    public class RoomDetailViewModel
    {
        public Room? room { get; set; }

        public Guid? roomId { get; set; }

        public ReservationViewModel? reservation { get; set; }
    }
}
