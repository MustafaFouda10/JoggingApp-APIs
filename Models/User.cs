using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JoggingApp.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Column("nvarchar(255)")]
        public string UserName { get; set; }
        public int Age { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool IsLogout { get; set; }

        [ForeignKey("Role")]
        public int RoleId { get; set; }

        public virtual Role? Role { get; set;}
        public virtual ICollection<Jogging>? Joggings { get; set;}
    }
}
