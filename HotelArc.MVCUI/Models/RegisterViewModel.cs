using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HotelArc.MVCUI.Models
{
    public class RegisterViewModel
    {
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [MaxLength(100, ErrorMessage = "Maximum Email Length must be 100 charachters")]
        [Column(TypeName = "nvarchar(100)")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Column(TypeName = "nvarchar(50)")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@.$]).+$", ErrorMessage = "Password must contain at least one lowercase letter, one uppercase letter, one digit, and one special character (@, ., $).")]
        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Minimum Password Length must be 8 charachters")]
        [MaxLength(50, ErrorMessage = "Maximum Password Length must be 50 charachters")]
        public string Password { get; set; }


        [Required(ErrorMessage = "Username is required")]
        [MinLength(8, ErrorMessage = "Minimum Username Length must be 8 charachters")]
        [MaxLength(20, ErrorMessage = "Maximum Username Length must be 20 charachters")]
        [Column(TypeName = "nvarchar(20)")]
        public string UserName { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Phone Number is required")]
        [Phone(ErrorMessage = "Invalid Phone Number")]
        [MaxLength(15, ErrorMessage = "Maximum Phone Number Length must be 15 charachters")]
        [MinLength(6, ErrorMessage = "Minimum Phone Number Length must be 6 charachters")]
        [Column(TypeName = "nvarchar(15)")] //nvarchar(15) ifadesi arapça vb. şekilde numara girilebilmesi için kullanıldı.
        public string PhoneNumber { get; set; }
    }
}
