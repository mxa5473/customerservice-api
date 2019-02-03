using System;
using System.Collections.Generic;
using CustomerServiceRESTAPI.Entities;

namespace CustomerServiceRESTAPI.Services
{
    public interface ITicketRepository : IDBRepository<Ticket>
    {
        IEnumerable<Ticket> GetAllByAgent(int agentId);
    }
}
