using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelArc.Kernel.Entities
{
    public class AppUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "Username is required", ErrorMessageResourceName = "Required")]
        [MinLength(8, ErrorMessage = "Minimum Username Length must be 8 charachters", ErrorMessageResourceName = "Char Length")]
        [MaxLength(20, ErrorMessage = "Maximum Username Length must be 20 charachters", ErrorMessageResourceName = "Char Length")]
        [Column(TypeName = "nvarchar(20)")]
        public string UserName { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email is required", ErrorMessageResourceName = "Required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address", ErrorMessageResourceName = "Invalid Email")]
        [MaxLength(100, ErrorMessage = "Maximum Email Length must be 100 charachters", ErrorMessageResourceName = "Char Length")]
        [Column(TypeName = "nvarchar(100)")]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Phone Number is required", ErrorMessageResourceName = "Required")]
        [Phone(ErrorMessage = "Invalid Phone Number", ErrorMessageResourceName = "Invalid Phone")]
        [MaxLength(15, ErrorMessage = "Maximum Phone Number Length must be 15 charachters", ErrorMessageResourceName = "Char Length")]
        [MinLength(6, ErrorMessage = "Minimum Phone Number Length must be 6 charachters", ErrorMessageResourceName = "Char Length")]
        [Column(TypeName = "nvarchar(15)")] //nvarchar(15) ifadesi arapça vb. şekilde numara girilebilmesi için kullanıldı.
        public string PhoneNumber { get; set; }

        [DataType(DataType.Password)]
        [Column(TypeName = "nvarchar(50)")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@.$]).+$", ErrorMessage = "Password must contain at least one lowercase letter, one uppercase letter, one digit, and one special character (@, ., $).")]
        [Required(ErrorMessage = "Password is required", ErrorMessageResourceName = "Required")]
        [MinLength(8, ErrorMessage = "Minimum Password Length must be 8 charachters", ErrorMessageResourceName = "Char Length")]
        [MaxLength(50, ErrorMessage = "Maximum Password Length must be 50 charachters", ErrorMessageResourceName = "Char Length")]
        public string Password { get; set; }


        public Guid RoleId { get; set; }

        [ForeignKey(nameof(RoleId))]
        public virtual Role Role { get; set; }

        public bool IsDeleted { get; set; }


        public virtual ICollection<Reservation>? Reservations { get; set; }
    }


}
