using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CustomerServiceRESTAPI.Models;
using Newtonsoft.Json;

namespace CustomerServiceRESTAPI.Services
{
    public class InventoryService : IInventoryService
    {
        static HttpClient _client = new HttpClient();

        public async Task<ProductDto> GetProductAsync(string serialNumber)
        {
            var result = await _client.GetAsync($"https://inventory343.azurewebsites.net/api/products/{serialNumber}");
            if (result.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ProductDto>(await result.Content.ReadAsStringAsync());
            } else
            return null;
        }

        public async Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
            var result = await _client.GetStringAsync("https://inventory343.azurewebsites.net/api/products");
            return JsonConvert.DeserializeObject<IEnumerable<ProductDto>>(result);
        }
    }
}
