using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JoggingApp.Models
{
    public class Permission
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Column("nvarchar(255)")]
        public string Name { get; set; }

        public virtual ICollection<RolePermission>? RolePermissions { get; set; }

    }
}
