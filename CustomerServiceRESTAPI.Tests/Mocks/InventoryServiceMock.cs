using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CustomerServiceRESTAPI.Models;
using CustomerServiceRESTAPI.Services;

namespace CustomerServiceRESTAPI.Tests.Mocks
{
    class InventoryServiceMock : IInventoryService
    {
        public static ProductDto TestProduct = new ProductDto()
        {
            SerialNumber = "007",
            Name = "Spy Watch",
            Description = "Can release paralyzing nerve gas to subdue enemy agents",
            Status = "sold",
            Warehouse = "Top Secret Lab"
        };

        public IEnumerable<ProductDto> _products = new List<ProductDto>()
        {
            TestProduct
        };
        public Task<ProductDto> GetProductAsync(string serialNumber)
        {
            return Task.Run(async () => TestProduct);
        }

        public Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
            return Task.Run(async () => _products);
        }
    }
}
