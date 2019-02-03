using System.Collections.Generic;
using CustomerServiceRESTAPI.Entities;
using CustomerServiceRESTAPI.Services;
using System.Linq;

namespace CustomerServiceRESTAPI.Tests.Mocks
{
    public class TicketRepositoryMock : ITicketRepository
    {
        public static Ticket TestTicket = new Ticket()
        {
            Id = 45,
            Title = "WOO this phone wont work",
            Description = "How do I fix?",
            Status = "new",
            ProductSerialNumber = InventoryServiceMock.TestProduct.SerialNumber,
            ClientId = ClientRepositoryMock.TestClient.Id,
            Client = ClientRepositoryMock.TestClient,
            AgentId = HRServiceMock.TestAgent.Id
        };

        List<Ticket> _tickets = new List<Ticket>()
        {
            TestTicket
        };

        public void Add(Ticket ticket)
        {
            _tickets.Add(ticket);
        }

        public void Delete(Ticket ticket)
        {
            var ticketId = GetTicketIndex(ticket.Id);
            if (ticketId < 0) return;

            _tickets.RemoveAt(ticketId);
        }

        public Ticket Get(int ticketId)
        {
            return _tickets.FirstOrDefault<Ticket>(t => t.Id == ticketId);
        }

        public IEnumerable<Ticket> GetAll()
        {
            return _tickets;
        }

        public bool Save()
        {
            return true;
        }

        public void Update(Ticket ticket)
        {
            var ticketId = GetTicketIndex(ticket.Id);
            if (ticketId < 0) return;

            _tickets[ticketId] = ticket;
        }

        int GetTicketIndex(int ticketId)
        {
            return _tickets.FindIndex(t => t.Id == ticketId);
        }

        public IEnumerable<Ticket> GetAllByAgentId(int agentId)
        {
            return _tickets.Where(t => t.AgentId == agentId);
        }
        public IEnumerable<Ticket> GetAllByClientId(int clientId)
        {
            return _tickets.Where(t => t.ClientId == clientId);
        }

        public IEnumerable<Ticket> GetAllByAgent(int agentId)
        {
            return _tickets.Where(t => t.Id == agentId);
        }
    }
}
