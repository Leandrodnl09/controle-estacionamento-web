using controle.estacionamento.web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace controle.estacionamento.web.Controllers
{
    public class ControleEstacionamentoController : Controller
    {
        HttpClient _httpClient;

        public ControleEstacionamentoController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7062");
        }

        public async Task<IActionResult> Index()
        {
            var path = "api/controlepermanencia";

            var request = new HttpRequestMessage(HttpMethod.Get, path);

            var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var controlePermanencia = JsonSerializer.Deserialize<List<ControlePermanenciaCarrosModel>>(content);

            return View(controlePermanencia);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(ControlePermanenciaCarrosModel model)
        {
            var path = "api/modelos";
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var messageRequest = new HttpRequestMessage(HttpMethod.Post, path)
            {
                Content = JsonContent.Create(model)
            };

            var response = await _httpClient.SendAsync(messageRequest);

            response.EnsureSuccessStatusCode();

            return RedirectToAction("Index");
        }
    }
}
