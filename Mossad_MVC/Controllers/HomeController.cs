using Microsoft.AspNetCore.Mvc;
using Mossad_MVC.Models;
using System.Collections.Generic;
using System.Diagnostics;
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
            IEnumerable<agent> agents = await _httpClient.GetFromJsonAsync<IEnumerable<agent>>("http://localhost:5166/agents");
            return View(agents);
        }

        //רשימת המשימות
        public async Task<IActionResult> getallmissions()
        {
            IEnumerable<Mission> missions = await _httpClient.GetFromJsonAsync<IEnumerable<Mission>>("http://localhost:5166/missions");

            return View(missions);
        }

      //רשימת מטרות
        public async Task<IActionResult> getalltargets()
        {
            IEnumerable<Target> targets = await _httpClient.GetFromJsonAsync<IEnumerable<Target>>("http://localhost:5166/targets");

            return View(targets);
        }

        //עדכון מטרה לפעילה
        public async Task<IActionResult> UpdateMission(int id)
        {
            var response = await _httpClient.PutAsJsonAsync($"http://localhost:5166/Missions/{id}", new { id = id });
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(getallmissions));
            }
            return View(Index);
        }


        //דף ניהול
        public async Task<IActionResult> meneger()
        {
            Details details = await _httpClient.GetFromJsonAsync<Details>("http://localhost:5166/missions/meneger");
            return View(details);
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
 