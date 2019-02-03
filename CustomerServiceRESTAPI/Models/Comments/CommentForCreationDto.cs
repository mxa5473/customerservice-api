using System;
namespace CustomerServiceRESTAPI.Models
{
    public class CommentForCreationDto
    {
        public int TicketId { get; set; }
        public string Content { get; set; }
    }
}
