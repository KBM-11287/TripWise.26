using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace TripWise.Mvc.Services
{
    public class ApiClientService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApiClientService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        private HttpClient CreateClient()
        {
            var client = _httpClientFactory.CreateClient("TripWiseApi");
            var httpContext = _httpContextAccessor.HttpContext;
            string? token = null;
            if (httpContext != null && httpContext.Request?.Cookies != null)
            {
                httpContext.Request.Cookies.TryGetValue("TripWiseToken", out token);
            }
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            return client;
        }

        public async Task<T> GetAsync<T>(string url)
        {
            var client = CreateClient();
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<T>(json);
            if (result is null)
            {
                throw new InvalidOperationException("Deserialization returned null.");
            }
            return result;
        }

        public async Task<T> PostAsync<T>(string url, object data)
        {
            var client = CreateClient();
            
            var response = await client.PostAsJsonAsync(url, data);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<T>(json);
            if (result is null)
            {
                throw new InvalidOperationException("Deserialization returned null.");
            }
            return result;
        }

        public async Task<T> PutAsync<T>(string url, object data)
        {
            var client = CreateClient();
            
            var response = await client.PutAsJsonAsync(url, data);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<T>(json);
            if (result is null)
            {
                throw new InvalidOperationException("Deserialization returned null.");
            }
            return result;
        }

        public async Task DeleteAsync(string url)
        {
            var client = CreateClient();
            var response = await client.DeleteAsync(url);
            response.EnsureSuccessStatusCode();
        }
    }
}
