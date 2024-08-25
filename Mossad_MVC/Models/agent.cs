using System.ComponentModel.DataAnnotations;

namespace Mossad_MVC.Models
{
    public class agent
    {
      
        public int Id { get; set; }


        public string Nickname { get; set; }


        public string PhotoUrl { get; set; }

        public int X_axis { get; set; }
     
        public int Y_axis { get; set; }

        public bool Active { get; set; } = false;

        public bool assigned { get; set; } = false;
    }
}
