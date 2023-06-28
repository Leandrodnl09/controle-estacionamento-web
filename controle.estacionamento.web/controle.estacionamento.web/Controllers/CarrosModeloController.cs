using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Reflection;
using controle.estacionamento.web.Models;
using controle.estacionamento.web.Controllers;

namespace BaseControleEstacionamentoWeb.Controllers
{
    public class CarrosModeloController : Controller
    {
        HttpClient _httpClient;

        public CarrosModeloController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7062");
        }

        public async Task<IActionResult> Index()
        {
            var path = "api/modelos";

            var request = new HttpRequestMessage(HttpMethod.Get, path);

            var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var modelos = JsonSerializer.Deserialize<List<CarrosModeloModel>>(content);

            return View(modelos);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CarrosModeloModel model)
        {
            try
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

                return RedirectToAction("Index", new { Message = "Ok" });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", new { Message = "NOk" });
            }

        }

        public async Task<IActionResult> Edit(int id)
        {
            var path = $"api/modelos/{id}";

            var request = new HttpRequestMessage(HttpMethod.Get, path);

            var response = await _httpClient.SendAsync(request);

            var content = await response.Content.ReadAsStringAsync();

            var modelo = JsonSerializer.Deserialize<CarrosModeloModel>(content);

            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CarrosModeloModel model)
        {
            try
            {
                var path = $"api/modelos/{model.Id}";
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var messageRequest = new HttpRequestMessage(HttpMethod.Put, path)
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

        public async Task<IActionResult> Delete(int id)
        {   
            try
            {
                var path = $"api/modelos/{id}";
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var messageRequest = new HttpRequestMessage(HttpMethod.Delete, path);

                var response = await _httpClient.SendAsync(messageRequest);

                response.EnsureSuccessStatusCode();

                return RedirectToAction("Index", new { Message = "Ok" });
            }
            catch (Exception)
            {
                return RedirectToAction("Index", new { Message = "NOk", TextMessage = $"Erro ao deletar o modelo" });
            }
        }
    }
}
