namespace Mossad_MVC.Models.viewmode
{
    public class mission_meneger
    {
        public int MissionId { get; set; }
        public float TimeLeft { get; set; }
        public float TotalTime { get; set; }
        public string Status { get; set; }
        public string AgentNickname { get; set; }
        public string location {  get; set; } 
        public string TargetName { get; set; }
        public string TargetPosition { get; set; }
        public string TargetLocation {  get; set; }
    }
}
