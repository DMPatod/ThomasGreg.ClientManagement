using FrontApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;

namespace FrontApplication.Pages.Clients
{
    public class FormModel : PageModel
    {
        private readonly HttpClient httpClient;
        public FormModel(IHttpClientFactory httpClientFactory)
        {
            httpClient = httpClientFactory.CreateClient("ClientAPI");
        }
        [BindProperty]
        public string Id { get; set; }
        [BindProperty]
        public Client Client { get; set; } = new Client();
        public async Task OnGet(string id)
        {
            Id = id;
            if (Id is not null)
            {
                var response = await httpClient.GetAsync($"api/v1/Client/{Id}");
                if (!response.IsSuccessStatusCode)
                {
                    return;
                }

                var data = await response.Content.ReadAsStringAsync();
                Client = JsonConvert.DeserializeObject<Client>(data);
            }
        }
        public async Task<IActionResult> OnPost()
        {
            Client apiResponse;
            var content = new StringContent(JsonConvert.SerializeObject(Client), Encoding.UTF8, "application/json");
            if (Id is null)
            {
                var response = await httpClient.PostAsync("api/v1/Client", content);
                if (!response.IsSuccessStatusCode)
                {
                    return BadRequest();
                }

                var data = await response.Content.ReadAsStringAsync();
                apiResponse = JsonConvert.DeserializeObject<Client>(data)!;
            }
            else
            {
                var response = await httpClient.PutAsync($"api/v1/Client/{Id}", content);
                if (!response.IsSuccessStatusCode)
                {
                    return BadRequest();
                }

                var data = await response.Content.ReadAsStringAsync();
                apiResponse = JsonConvert.DeserializeObject<Client>(data)!;
            }
            return RedirectToPage("Form", new { apiResponse.Id });
        }
    }
}
