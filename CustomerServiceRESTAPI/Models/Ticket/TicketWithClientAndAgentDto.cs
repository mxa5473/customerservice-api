using System;
using System.Collections.Generic;
namespace CustomerServiceRESTAPI.Models
{
    public class TicketWithClientAndAgentDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string ProductSerialNumber { get; set; }

        public ClientDto Client { get; set; }

        public int AgentId { get; set; }

        public ICollection<CommentDto> Comments { get; set; } = new List<CommentDto>();
    }
}
