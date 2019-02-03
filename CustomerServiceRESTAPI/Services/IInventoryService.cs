using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerServiceRESTAPI.Models;

namespace CustomerServiceRESTAPI.Services
{
    public interface IInventoryService
    {
        Task<IEnumerable<ProductDto>> GetProductsAsync();
        Task<ProductDto> GetProductAsync(string serialNumber);
    }
}
