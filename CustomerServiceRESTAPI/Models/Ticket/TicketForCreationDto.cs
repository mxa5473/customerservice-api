using System;
namespace CustomerServiceRESTAPI.Models
{
    public class TicketForCreationDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ProductSerialNumber { get; set; }
    }
}
