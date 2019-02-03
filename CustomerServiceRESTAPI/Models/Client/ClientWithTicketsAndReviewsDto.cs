using System;
using System.Collections.Generic;

namespace CustomerServiceRESTAPI.Models
{
    public class ClientWithTicketsAndReviewsDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; }

        public ICollection<TicketDto> Tickets { get; set; } = new List<TicketDto>();
        public ICollection<ReviewDto> Reviews { get; set; } = new List<ReviewDto>();
    }
}
