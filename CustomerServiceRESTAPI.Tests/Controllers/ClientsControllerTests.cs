using System;
using System.Linq;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using CustomerServiceRESTAPI.Controllers;
using CustomerServiceRESTAPI.Models;
using CustomerServiceRESTAPI.Tests.Mocks;
using Microsoft.AspNetCore.Mvc;
using CustomerServiceRESTAPI.Services;
using System.Threading.Tasks;

namespace CustomerServiceRESTAPI.Tests.Controllers
{
    [Collection("StartupFixture collection")]
    public class ClientsControllerTests
    {
        [Fact]
        public async void Create_Client()
        {
            var controller = new ClientsController(new ClientRepositoryMock(), new HRServiceMock());
            var clientForCreation = new ClientForCreationDto()
            {
                FirstName = "Steven",
                LastName = "BOI",
                Email = "Hello",
                Password = "mypasswkrd",
                Address = new Address()
                {
                    Line1 = "45 some st",
                    Line2 = "apt 2",
                    City = "Roc",
                    State = "MA",
                    Zipcode = "02142",
                    Country = "US"
                }
            };


            var result = await controller.Post(clientForCreation);

            var okResult = result.Should().BeOfType<CreatedAtRouteResult>().Subject;
            var client = okResult.Value.Should().BeAssignableTo<ClientWithTicketsAndReviewsDto>().Subject;

            client.Address.City.Should().Be(clientForCreation.Address.City);
        }

        [Fact]
        public void Get_All_Client()
        {
            var controller = new ClientsController(new ClientRepositoryMock(), new HRServiceMock());

            var result = controller.Get();
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var clients = okResult.Value.Should().BeAssignableTo<IEnumerable<ClientWithTicketsAndReviewsDto>>().Subject;
            clients.Count().Should().Be(1);
        }

        [Fact]
        public void Get_Client()
        {
            var controller = new ClientsController(new ClientRepositoryMock(), new HRServiceMock());

            var result = controller.Get(ClientRepositoryMock.TestClient.Id);
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var client = okResult.Value.Should().BeAssignableTo<ClientWithTicketsAndReviewsDto>().Subject;

            client.Address.City.Should().Be(ClientRepositoryMock.TestClient.AddressCity);
        }

        [Fact]
        public void Update_Client()
        {
            var controller = new ClientsController(new ClientRepositoryMock(), new HRServiceMock());
            var clientForUpdate = new ClientForUpdateDto()
            {
                FirstName = "Steven",
                LastName = "Hoe",
                Email = "cgs@gmail.com"
            };

            var result = controller.Put(ClientRepositoryMock.TestClient.Id, clientForUpdate);
            var okResult = result.Should().BeOfType<NoContentResult>().Subject;
        }

        [Fact]
        public void Delete_Client()
        {
            var controller = new ClientsController(new ClientRepositoryMock(), new HRServiceMock());

            var result = controller.Delete(ClientRepositoryMock.TestClient.Id);
            var OkResult = result.Should().BeOfType<NoContentResult>().Subject;
        }
    }
}
