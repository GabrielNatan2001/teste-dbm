using Communication.Requests;
using Communication.Response;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace MVC.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly HttpClient _httpClient;

        public ProdutoController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("produto");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var produtos = JsonConvert.DeserializeObject<List<ResponseProdutoJson>>(json);
                return View(produtos);
            }
            return View(new List<ResponseProdutoJson>());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RequestProdutoJson model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("produto", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Erro ao criar o produto. Tente novamente.");
            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"produto/{id}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var produto = JsonConvert.DeserializeObject<ResponseProdutoJson>(json);
                return View(produto);
            }
            return NotFound();
        }

        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"produto/{id}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var produto = JsonConvert.DeserializeObject<RequestProdutoJson>(json);
                var model = new ResponseProdutoJson
                {
                    Nome = produto.Nome,
                    Descricao = produto.Descricao,
                    Preco = produto.Preco
                };
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, RequestProdutoJson model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"produto/{id}", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Erro ao atualizar o produto.");
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.GetAsync($"produto/{id}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var produto = JsonConvert.DeserializeObject<ResponseProdutoJson>(json);
                return View(produto);
            }
            return NotFound();
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"produto/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Delete), new { id });
        }
    }
}
