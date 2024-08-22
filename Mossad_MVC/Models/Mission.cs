using System.ComponentModel.DataAnnotations;

namespace Mossad_MVC.Models
{

    public class Mission
    {
       
        public int Id { get; set; }
        public int AgentId { get; set; }
        public int TargetId { get; set; }
        public float Timelaft { get; set; }
        public float TotalTime { get; set; }

        //[AllowedValues("assigned", "finish", "pussible")]
        public string Status { get; set; }
    }
}
