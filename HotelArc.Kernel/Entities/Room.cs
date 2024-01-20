using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelArc.Kernel.Entities
{
    public class Room
    {
        public int RoomId { get; set; }

        public string RoomNumber { get; set; }

        public string RoomType { get; set; }

        public decimal Price { get; set; }

        public bool IsAvaliable { get; set; }

        public bool IsDeleted { get; set; }


    }

    public class Reservation
    {
        public int ReservationId { get; set; }


    }

    public class AppUser
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Username is required", ErrorMessageResourceName = "Required")]
        public string UserName { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email is required", ErrorMessageResourceName = "Required")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required", ErrorMessageResourceName = "Required")]
        [MinLength(8, ErrorMessage = "Minimum Password Length must be 8 charachters", ErrorMessageResourceName = "Char Length")]
        public string Password { get; set; }


        public int RoleId { get; set; }

        [ForeignKey(nameof(RoleId))]
        public Role Role { get; set; }

        public bool IsDeleted { get; set; }


        public virtual ICollection<Reservation>? Reservations { get; set; }
    }

    public class Role
    {
        public int RoleId { get; set; }

        public string RoleName { get; set; }

        public bool IsDeleted { get; set; }
    }


}
