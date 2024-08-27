using Microsoft.AspNetCore.Mvc;
using Mossad_MVC.Models;
using Mossad_MVC.Models.viewmode;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text.Json;
namespace Mossad_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private HttpClient _httpClient;
        public HomeController(ILogger<HomeController> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        //רשימת הסוכנים
        public async Task<IActionResult> Index()
        {
            IEnumerable<agent> agents = await agentlist();
            return View(agents);
        }

        //רשימת מטרות
        public async Task<IActionResult> getalltargets()
        {
            IEnumerable<Target> targets = await targetlist();

            return View(targets);
        }

        //רשימת המשימות
        public async Task<IActionResult> getallmissions()
        {
            IEnumerable<Mission> missions = await _httpClient.GetFromJsonAsync<IEnumerable<Mission>>("http://localhost:5166/missions");

            return View(missions);
        }

      

        //עדכון מטרה לפעילה
        public async Task<IActionResult> UpdateMission(int id)
        {
            var response = await _httpClient.PutAsJsonAsync($"http://localhost:5166/Missions/{id}", new { id = id });
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(all));
            }
            return View(Index);
        }

        //מסך משימות עם כלל הנתונים
        public async Task<IActionResult> all()
        {
            List<mission_meneger> meneger = new List<mission_meneger>();
            IEnumerable<Mission> missions = await missionlist();
            IEnumerable<agent> agents = await agentlist();
            IEnumerable<Target> targets = await targetlist();
            foreach(Mission mission in missions) 
            {
               agent agent = agents.FirstOrDefault(agent => agent.Id == mission.AgentId);
               Target target = targets.FirstOrDefault(target => target.Id == mission.TargetId);
                mission_meneger mission_Meneger = new mission_meneger()
                {
                    MissionId = mission.Id,
                    TimeLeft = mission.Timelaft,
                    TotalTime = mission.TotalTime,
                    Status = mission.Status,
                    AgentNickname = agent.Nickname,
                    location = $"X: {agent.X_axis} Y: {agent.Y_axis}",
                    TargetName = target.Name,
                    TargetPosition = target.Position,
                    TargetLocation = $"X: {target.X_axis} Y: {target.Y_axis}"
                };
                meneger.Add(mission_Meneger);
            }
            return View(meneger);
        }



        //דף ניהול
        public async Task<IActionResult> meneger()
        {
            Details details = await _httpClient.GetFromJsonAsync<Details>("http://localhost:5166/missions/meneger");
            return View(details);
        }


        //יוצר רשימת מטרות
        public async Task<IEnumerable<Target>> targetlist() 
        {
            IEnumerable<Target> targets = await _httpClient.GetFromJsonAsync<IEnumerable<Target>>("http://localhost:5166/targets");
            return (targets);
        }
        //יוצר רשימת סוכנים
        public async Task<IEnumerable<agent>> agentlist() 
        {
            IEnumerable<agent> agents = await _httpClient.GetFromJsonAsync<IEnumerable<agent>>("http://localhost:5166/agents");
            return (agents);
        }
        //יוצר רשימת משימות
        public async Task<IEnumerable<Mission>> missionlist()
        {
            IEnumerable<Mission> missions = await _httpClient.GetFromJsonAsync<IEnumerable<Mission>>("http://localhost:5166/missions");
            return (missions);
        }


        //בונה טבלה
        public async Task<ActionResult> table()
        {
          
                var model = new Table();

            var targets = await targetlist();
            var agents = await agentlist();

            //foreach (var target in targets)
            //{
            //    string status;
            //    if (target.Eliminated = false)
            //    {
            //         status = "T";    
            //    }
            //    else { status = "X"; }
            //    model.points.Add(new point { Row = target.X_axis, Column = target.Y_axis, Symbol = status });
            //}
            //foreach (var agent in agents)
            //{
            //    model.points.Add(new point { Row = agent.X_axis, Column = agent.Y_axis, Symbol = "A" });
            //}
            model.points.AddRange(targets.Select(t => new point { Row = t.X_axis, Column = t.Y_axis, Symbol = "T" }));
            model.points.AddRange(agents.Select(a => new point { Row = a.X_axis, Column = a.Y_axis, Symbol = "A" }));

            return View(model);
        }


        public IActionResult Privacy()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
 