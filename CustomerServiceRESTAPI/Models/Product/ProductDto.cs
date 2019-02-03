using System;
namespace CustomerServiceRESTAPI.Models
{
    public class ProductDto
    {
        public string SerialNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Warehouse { get; set; }
    }
}
