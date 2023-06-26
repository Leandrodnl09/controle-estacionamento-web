using controle.estacionamento.web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using System.Text.Json;

namespace controle.estacionamento.web.Controllers
{
    public class HomeController : Controller
    {
        IConfiguration _config;
        private readonly ILogger<HomeController> _logger;
        HttpClient _httpClient;

        public HomeController(ILogger<HomeController> logger, IConfiguration config)
        {
            _logger = logger;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7062");
            _config = config;
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

        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginModel model) // Login
        {
            string user = _config.GetSection("Login").GetSection("User").Value;
            string pass = _config.GetSection("Login").GetSection("Pass").Value;

            if (string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password))
            {
                ViewBag.ErrorMessage = "Por favor, preencha todos os campos.";
                return View(model);
            }

            if (model.Username == user && model.Password == pass)
            {
                // Defina pelo menos um conjunto de claims
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, model.Username),
            new Claim(ClaimTypes.Role, "Administrador"),
        };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                };

                // Logando
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties
                );
                return RedirectToAction("Index");
            }

            ViewBag.ErrorMessage = "Usuário ou senha incorretos.";
            return View(model);
        }


        public async Task<IActionResult> LogOutAsync()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
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