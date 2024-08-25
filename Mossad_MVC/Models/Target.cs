using System.ComponentModel.DataAnnotations;

namespace Mossad_MVC.Models
{
    public class Target
    {
      
        public int Id { get; set; }

        public string Name { get; set; }

        public string Position { get; set; }

        public string PhotoUrl { get; set; }
     
        public int X_axis { get; set; }

        public int Y_axis { get; set; }

        public bool Alive = true;

        public bool Active { get; set; } = false;
    }
}
