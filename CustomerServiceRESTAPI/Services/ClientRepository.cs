using System;
using System.Linq;
using System.Collections.Generic;
using CustomerServiceRESTAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomerServiceRESTAPI.Services
{
    public class ClientRepository : IDBRepository<Client>
    {
        Context _context;

        public ClientRepository(Context context)
        {
            _context = context;
        }

        public void Add(Client client)
        {
            _context.Clients.Add(client);
        }

        public Client Get(int id)
        {
            return _context.Clients.Include(c => c.Tickets).Include(c => c.Reviews).FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Client> GetAll()
        {
            return _context.Clients.Include(c => c.Tickets).Include(c => c.Reviews).ToList();
        }

        public void Delete(Client client)
        {
            _context.Clients.Remove(client);
        }

        public void Update(Client client)
        {
            _context.Clients.Update(client);
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}
