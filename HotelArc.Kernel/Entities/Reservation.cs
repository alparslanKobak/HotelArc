using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelArc.Kernel.Entities
{
    public class Reservation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ReservationId { get; set; }


        [Required(ErrorMessage = "Room is required")]
        public Guid RoomId { get; set; }

        [ForeignKey(nameof(RoomId))]
        public virtual Room? Room { get; set; }

        [Required(ErrorMessage = "User is required")]
        public Guid AppUserId { get; set; }

        [ForeignKey(nameof(AppUserId))]
        public virtual AppUser? AppUser { get; set; }


        public DateTime CheckIn { get; set; }

        public DateTime CheckOut { get; set; }

        public bool IsDeleted { get; set; }
    }


}
