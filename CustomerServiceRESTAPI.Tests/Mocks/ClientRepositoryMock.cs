using System;
using System.Linq;
using CustomerServiceRESTAPI.Services;
using CustomerServiceRESTAPI.Entities;
using System.Collections.Generic;

namespace CustomerServiceRESTAPI.Tests.Mocks
{
    public class ClientRepositoryMock : IDBRepository<Client>
    {
        public static Client TestClient = new Client()
        {
            Id = 21,
            FirstName = "John",
            LastName = "Doe",
            Email = "johndoe@gmail.com",
            AddressLine1 = "150 rit",
            AddressLine2 = "apt 2",
            AddressCity = "Rochester",
            AddressState = "ny",
            AddressZipcode = "02121",
            AddressCountry = "USA"
        };

        List<Client> _clients = new List<Client>() {
            TestClient
        };

        public void Add(Client client)
        {
            _clients.Add(client);
        }

        public Client Get(int id)
        {
            return _clients.FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Client> GetAll()
        {
            return _clients;
        }

        public void Update(Client client)
        {
            var clientIndex = _clients.FindIndex(c => c.Id == client.Id);
            if (clientIndex == -1) return;

            _clients[clientIndex] = client;
        }

        public void Delete(Client client)
        {
            var clientIndex = _clients.FindIndex(c => c.Id == client.Id);
            if (clientIndex == -1) return;

            _clients.RemoveAt(clientIndex);
        }

        public bool Save()
        {
            return true;
        }
    }
}
