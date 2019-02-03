using System;
using System.Collections.Generic;

namespace CustomerServiceRESTAPI.Models
{
    public class CommentWithTicketsDto
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public string Content { get; set; }
        public int ClientId { get; set; }
        public int AgentId { get; set; }
        //public string DateCreated { get; set; }
    }
}
