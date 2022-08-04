using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace AthenaService.Common.CustomHttpClient
{
    public class OtherCustomHttpClient : IOtherCustomHttpClient
    {
        private readonly HttpClient _httpClient;

        public OtherCustomHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<T?> GetDataAsync<T>(string url, string? authorization = null)
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            if(authorization != null)
            {
                httpRequestMessage.Headers.Authorization = AuthenticationHeaderValue.Parse(authorization);
            }

            var response = await _httpClient.SendAsync(httpRequestMessage);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(result);
        }

        public async Task<string> GetStringAsync(string url)
        {
            return await _httpClient.GetStringAsync(url);
        }
    }
}
