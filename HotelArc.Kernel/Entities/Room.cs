using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelArc.Kernel.Entities
{
    public class Room
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid RoomId { get; set; }

        [DataType(DataType.ImageUrl)]
        [Required(ErrorMessage = "Room Image is required")]
        [MaxLength(100, ErrorMessage = "Maximum Room Image Length must be 100 charachters")]
        [Column(TypeName = "nvarchar(100)")]
        public string RoomImage { get; set; } = "HotelArchSuitDefault.png";

        [Required(ErrorMessage = "Room Number is required")]
        [MinLength(3, ErrorMessage = "Minimum Room Number Length must be 3 charachters")]
        [MaxLength(6, ErrorMessage = "Maximum Room Number Length must be 6 charachters")]
        public string RoomNumber { get; set; }

        [Required(ErrorMessage = "Room Type is required")]
        [MinLength(3, ErrorMessage = "Minimum Room Type Length must be 3 charachters")]
        [MaxLength(15, ErrorMessage = "Maximum Room Type Length must be 15 charachters")]
        public string RoomType { get; set; }

        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Price is required")]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0, 100000, ErrorMessage = "Price must be between 0 and 100000")]

        public decimal Price { get; set; } // Price per night


        public bool IsAvaliable { get; set; } = true;

        public bool IsDeleted { get; set; }

        public virtual ICollection<Reservation>? Reservations { get; set; }

    }


}
