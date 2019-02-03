using System;
namespace CustomerServiceRESTAPI.Models
{
    public class ReviewWithClientDto
    {
        public int Id { get; set; }
        public int AgentId { get; set; }
        public string Content { get; set; }
        public string DateCreated { get; set; }
        public ClientDto Client { get; set; }
    }
}
