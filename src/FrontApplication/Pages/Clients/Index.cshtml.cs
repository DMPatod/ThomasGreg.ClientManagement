using FrontApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace FrontApplication.Pages.Clients
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient httpClient;
        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            httpClient = httpClientFactory.CreateClient("ClientAPI");
        }
        [BindProperty]
        public List<Client> Clients { get; set; }
        public async Task OnGet()
        {
            try
            {
                var response = await httpClient.GetAsync("api/v1/Client");
                if (!response.IsSuccessStatusCode)
                {
                    return;
                }

                var data = await response.Content.ReadAsStringAsync();
                Clients = JsonConvert.DeserializeObject<List<Client>>(data);
            }
            catch (Exception)
            {

            }
        }
        public async Task<IActionResult> OnPostDelete(string id)
        {
            var response = await httpClient.DeleteAsync($"api/v1/Client/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return BadRequest();
            }

            var data = await response.Content.ReadAsStringAsync();
            return RedirectToPage("Index");
        }
    }
}
