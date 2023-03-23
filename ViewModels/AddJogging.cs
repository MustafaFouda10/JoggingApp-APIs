using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace JoggingApp.ViewModels
{
    public class AddJogging
    {
        [Range(0.1,double.MaxValue)]
        public double Distance { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
    }
}
