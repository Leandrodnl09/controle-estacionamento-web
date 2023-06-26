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

            var pathCarrosModelos = "api/modelos";

            var requestModelos = new HttpRequestMessage(HttpMethod.Get, pathCarrosModelos);

            var responseModelos = await _httpClient.SendAsync(requestModelos);

            responseModelos.EnsureSuccessStatusCode();

            var contentModelos = await responseModelos.Content.ReadAsStringAsync();

            var modelos = JsonSerializer.Deserialize<List<CarrosModeloModel>>(contentModelos);

            ViewData["Modelo"] = modelos;

            return View(controlePermanencia);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(ControlePermanenciaCarrosModel model)
        {
            try
            {
                var path = "api/controlepermanencia";
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var messageRequest = new HttpRequestMessage(HttpMethod.Post, path)
                {
                    Content = JsonContent.Create(model)
                };

                var response = await _httpClient.SendAsync(messageRequest);

                response.EnsureSuccessStatusCode();

                return RedirectToAction("Index", new { Message = "Ok" });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", new { Message = "NOk" });
            }

        }

        public async Task<IActionResult> Edit(int id)
        {
            var path = $"api/controlepermanencia/{id}";

            var request = new HttpRequestMessage(HttpMethod.Get, path);

            var response = await _httpClient.SendAsync(request);

            var content = await response.Content.ReadAsStringAsync();

            var controlePermanencia = JsonSerializer.Deserialize<ControlePermanenciaCarrosModel>(content);

            return View(controlePermanencia);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ControlePermanenciaCarrosModel controlePermanencia)
        {
            var path = $"api/controlepermanencia/{controlePermanencia.Id}";
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var messageRequest = new HttpRequestMessage(HttpMethod.Put, path)
            {
                Content = JsonContent.Create(controlePermanencia)
            };

            var response = await _httpClient.SendAsync(messageRequest);

            response.EnsureSuccessStatusCode();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var path = $"api/controlepermanencia/{id}";
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var messageRequest = new HttpRequestMessage(HttpMethod.Delete, path);

            var response = await _httpClient.SendAsync(messageRequest);

            response.EnsureSuccessStatusCode();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Close(int id)
        {
            try
            {
                var path = $"/finalizarpermanencia/{id}";
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var messageRequest = new HttpRequestMessage(HttpMethod.Put, path);

                var response = await _httpClient.SendAsync(messageRequest);

                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();

                return RedirectToAction("Index", new { Message = "Ok" });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", new { Message = "NOk" });
            }
        }
    }
}
