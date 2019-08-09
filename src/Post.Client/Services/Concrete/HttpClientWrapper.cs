using Microsoft.Extensions.Configuration;
using Post.Client.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Post.Client.Services.Concrete
{
    public class HttpClientWrapper : IHttpClientWrapper
    {
        private readonly HttpClient _httpClient;

        public HttpClientWrapper(IConfiguration configuration)
        {
            string url = configuration.GetValue<string>("ServerAddress");
            _httpClient = new HttpClient()
            {
                BaseAddress =  new Uri(url)
            };
        }

        public async Task<bool> SendMessage(string message)
        {
            var content = new StringContent(message);

            var response = await _httpClient.PostAsync("api/messages", content);

            return response.IsSuccessStatusCode;
        }
    }
}
