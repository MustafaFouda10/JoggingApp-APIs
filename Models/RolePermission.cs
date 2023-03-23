using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JoggingApp.Models
{
    public class RolePermission
    {
        [ForeignKey("Role")]
        public int RoleId { get; set; }
        [ForeignKey("Permission")]
        public int PermissionId { get; set; }

        public virtual Role? Role { get; set; }
        public virtual Permission? Permission { get; set; }

    }
}
