using controle.estacionamento.web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;

namespace controle.estacionamento.web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        HttpClient _httpClient;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7062");
        }

        public async Task<IActionResult> Index()
        {
            if (!HttpContext.User.Identity.IsAuthenticated) // login
            {
                return RedirectToAction("Login");               
            }
            string path = "api/modelos";

            var httpRequest = new HttpRequestMessage(HttpMethod.Get, path);

            var response = await _httpClient.SendAsync(httpRequest);

            var content = await response.Content.ReadAsStringAsync();

            var carrosModelos = JsonSerializer.Deserialize<List<CarrosModeloModel>>(content);

            return View(carrosModelos);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        
        public IActionResult Login() // login
        {
            return View();
        }

        public async Task<IActionResult> LoginAsync(LoginModel model)
        {

        }
        public async Task<IActionResult> AboutAsync()
        {
            var path = "api/modelos";

            var messageRequest = new HttpRequestMessage(HttpMethod.Get, path);

            var response = await _httpClient.SendAsync(messageRequest);

            var jsonContent = await response.Content.ReadAsStringAsync();

            var carrosModel = JsonSerializer.Deserialize<List<CarrosModeloModel>>(jsonContent);

            return View(carrosModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}