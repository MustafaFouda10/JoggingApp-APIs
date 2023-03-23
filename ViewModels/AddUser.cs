using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JoggingApp.ViewModels
{
    public class AddUser
    {
        [Required]
        [Column("nvarchar(255)")]
        public string UserName { get; set; }
        public int Age { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
