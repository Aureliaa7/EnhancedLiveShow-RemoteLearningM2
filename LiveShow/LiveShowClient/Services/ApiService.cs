using LiveShowClient.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace LiveShowClient.Services
{
    public class ApiService : IApiService
    {
        private readonly HttpClient httpClient;

        public ApiService(HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;
            httpClient.Timeout = TimeSpan.FromMinutes(30);
            this.httpClient.BaseAddress = new Uri(configuration.GetValue<string>("AppSettings:BaseUrl"));
        }

        public async Task<T> GetContentFromHttpAsync<T>(string url)
        {
            string address = httpClient.BaseAddress + url;
            var content = await httpClient.GetAsync(address).Result.Content.ReadAsStringAsync();
            var result =  JsonConvert.DeserializeObject<T>(content);
            return result;
        }

        public async Task<string> PostDataAsync<T>(string uri, T data)
        {
            string address = httpClient.BaseAddress + uri;
            var serializedData = JsonConvert.SerializeObject(data);
            var buffer = System.Text.Encoding.UTF8.GetBytes(serializedData);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var result = await httpClient.PostAsync(address, byteContent);
            return await result.Content.ReadAsStringAsync();
        }

        public async Task<string> PatchDataAsync<T>(string uri, T data)
        {
            string address = httpClient.BaseAddress + uri;
            var serializedData = JsonConvert.SerializeObject(data);
            var buffer = System.Text.Encoding.UTF8.GetBytes(serializedData);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = await httpClient.PatchAsync(address, byteContent);
            return await result.Content.ReadAsStringAsync();
        }

        public async Task<string> DeleteDataAsync(string uri)
        {
            string address = httpClient.BaseAddress + uri;
            var result = await httpClient.DeleteAsync(address);
            return result.StatusCode.ToString();
        }

        public async Task<string> PutAsync<T>(string uri, T data)
        {
            string address = httpClient.BaseAddress + uri;
            var serializedData = JsonConvert.SerializeObject(data);
            var buffer = System.Text.Encoding.UTF8.GetBytes(serializedData);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = await httpClient.PutAsync(address, byteContent);
            return await result.Content.ReadAsStringAsync();
        }
    }
}
