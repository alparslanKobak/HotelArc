using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelArc.Kernel.Entities
{
    public class Role
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid RoleId { get; set; }

        [Required(ErrorMessage = "Role Name is required")]
        [MinLength(3, ErrorMessage = "Minimum Role Name Length must be 3 charachters")]
        [MaxLength(15, ErrorMessage = "Maximum Role Name Length must be 15 charachters")]
        [Column(TypeName = "nvarchar(15)")]
        public string RoleName { get; set; }

        public bool IsDeleted { get; set; }
    }


}
