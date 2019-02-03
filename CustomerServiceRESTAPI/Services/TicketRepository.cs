using System;
using System.Linq;
using System.Collections.Generic;
using CustomerServiceRESTAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomerServiceRESTAPI.Services
{
    public class TicketRepository : ITicketRepository
    {
        Context _context;

        public TicketRepository(Context context)
        {
            _context = context;
        }

        public void Add(Ticket ticket)
        {
            _context.Tickets.Add(ticket);
        }

        public Ticket Get(int id)
        {
            return GetAll().FirstOrDefault(t => t.Id == id);
        }

        public IEnumerable<Ticket> GetAll()
        {
            return _context.Tickets.Include(t => t.Client).Include(t => t.Comments).ToList();
        }

        public IEnumerable<Ticket> GetAllByAgent(int agentId)
        {
            return _context.Tickets.Where(t => t.AgentId == agentId).ToList();
        }

        public void Update(Ticket ticket)
        {
            _context.Tickets.Update(ticket);
        }

        public void Delete(Ticket ticket)
        {
            _context.Tickets.Remove(ticket);
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}
