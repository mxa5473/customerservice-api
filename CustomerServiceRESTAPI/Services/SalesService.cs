using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CustomerServiceRESTAPI.Entities;
using Newtonsoft.Json;

namespace CustomerServiceRESTAPI.Services
{
    public class SalesService : ISalesService
    {
        static HttpClient _client = new HttpClient();

        // Holds the data needed for a request to Sales' "refund" endpoint.
        private class RequestResponse
        {
            public string customerEmail { get; set; }
            public string customerShippingState { get; set; }
            public string customerShippingStreetAddress { get; set; }
            public string customerShippingTown { get; set; }
            public string customerShippingZip { get; set; }
            public IEnumerable<ProductForReplacementRequest> products { get; set; } = new List<ProductForReplacementRequest>();
            private const int totalCost = 0;

            public RequestResponse(Ticket ticket, string modelToReplace)
            {
                customerEmail = ticket.Client.Email;
                customerShippingState = ticket.Client.AddressState;
                customerShippingStreetAddress = ticket.Client.AddressLine1 + "\n" + ticket.Client.AddressLine2;
                customerShippingTown = ticket.Client.AddressCity;
                customerShippingZip = ticket.Client.AddressZipcode;
                products.Append(new ProductForReplacementRequest(ticket.ProductSerialNumber));
            }

            // Holds the data about a product to be replaced.
            public class ProductForReplacementRequest
            {
                private string model { get; set; }
                private const int priceSoldAt = 0;
                private const bool refurbished = false;

                public ProductForReplacementRequest(string productModel)
                {
                    model = productModel;
                }
            }
        }

        // Places a request to Sales' "refund" API endpoint to order a device replacement.
        public async Task<bool> RequestReplacementDevice(Ticket ticketForRefund)
        {
            return (await _client.PutAsync("http://54.242.81.38:8080/orders/new/refund",
                new StringContent(
                    JsonConvert.SerializeObject(
                        new RequestResponse(ticketForRefund,
                            (await new InventoryService().GetProductAsync(ticketForRefund.ProductSerialNumber)).Name)).ToString(),
                Encoding.UTF8, "application/json"))).IsSuccessStatusCode;
        }
    }
}
