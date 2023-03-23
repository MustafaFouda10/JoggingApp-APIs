using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JoggingApp.ViewModels
{
    public class RegisterViewModel
    {
        public string UserName { get; set; }
        public int Age { get; set; }
        public string Password { get; set; }
    }
}
