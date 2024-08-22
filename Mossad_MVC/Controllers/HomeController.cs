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

        public async Task<IActionResult> Index()
        {
            IEnumerable<agent> agents = await listagents();
            return View(agents);
        }

        public async Task<IEnumerable<agent>> listagents()
        {
            var list1 = await _httpClient.GetFromJsonAsync<IEnumerable<agent>>("http://localhost:5166/agents");
            return list1;
        }

        public async Task<IActionResult> getallmissions()
        {
            IEnumerable<Mission> missions = await listmissions();
          
            return View(missions);
        }

        public async Task<IEnumerable<Mission>> listmissions()
        {
            var list1 = await _httpClient.GetFromJsonAsync<IEnumerable<Mission>>("http://localhost:5166/api/mission");
            return list1;
        }

      
        public async Task<IActionResult> UpdateMission(int id)
        {
            var response = await _httpClient.PostAsJsonAsync($"http://localhost:5166/api/Mission/{id}", id);
            return RedirectToAction(nameof(getallmissions));
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
