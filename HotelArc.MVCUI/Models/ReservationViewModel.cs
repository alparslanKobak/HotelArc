using System.ComponentModel.DataAnnotations;

namespace HotelArc.MVCUI.Models
{
    public class ReservationViewModel
    {
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CheckIn { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CheckOut { get; set; }
    }
}
