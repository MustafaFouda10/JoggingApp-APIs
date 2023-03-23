using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JoggingApp.Models
{
    public class Jogging
    {
        [Key]
        public int Id { get; set; }

        [Range(0.1, double.MaxValue)]
        public double Distance { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
      
        [ForeignKey("User")]
        public int UserId { get; set; }

        public virtual User? User { get; set; }
    }
}
