using System;
using System.Linq;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using CustomerServiceRESTAPI.Controllers;
using CustomerServiceRESTAPI.Tests.Mocks;
using CustomerServiceRESTAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CustomerServiceRESTAPI.Tests.Controllers
{
    public class StartupFixture
    {
        public StartupFixture()
        {
            AutoMapperConfig.Config();
        }
    }

    [CollectionDefinition("StartupFixture collection")]
    public class StartupCollection : ICollectionFixture<StartupFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }

    [Collection("StartupFixture collection")]
    public class TicketsControllerTests
    {
        [Fact]
        public async void Create_Ticket()
        {
            var controller = new TicketsController(new TicketRepositoryMock(), new ClientRepositoryMock(), new InventoryServiceMock(), new HRServiceMock());
            var ticketForCreation = new TicketForCreationDto()
            {
                Title = "Example 1",
                Description = "yo this dont work",
                ProductSerialNumber = InventoryServiceMock.TestProduct.SerialNumber,
            };

            var result = await controller.Post(ticketForCreation, ClientRepositoryMock.TestClient.Id);

            var okResult = result.Should().BeOfType<CreatedAtRouteResult>().Subject;
            var ticket = okResult.Value.Should().BeAssignableTo<TicketWithClientAndAgentDto>().Subject;

            ticket.Title.Should().Be(ticketForCreation.Title);
        }

        [Fact]
        public void Get_All_Ticket()
        {
            var controller = new TicketsController(new TicketRepositoryMock(), new ClientRepositoryMock(), new InventoryServiceMock(), new HRServiceMock());

            var result = controller.Get(null);
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var tickets = okResult.Value.Should().BeAssignableTo<IEnumerable<TicketWithClientAndAgentDto>>().Subject;

            tickets.Count().Should().Be(1);
        }

        [Fact]
        public void Get_Ticket()
        {
            var controller = new TicketsController(new TicketRepositoryMock(), new ClientRepositoryMock(), new InventoryServiceMock(), new HRServiceMock());

            var result = controller.Get(TicketRepositoryMock.TestTicket.Id);
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var ticket = okResult.Value.Should().BeAssignableTo<TicketWithClientAndAgentDto>().Subject;

            ticket.Title.Should().Be(TicketRepositoryMock.TestTicket.Title);
        }

        [Fact]
        public void Update_Ticket()
        {
            var controller = new TicketsController(new TicketRepositoryMock(), new ClientRepositoryMock(), new InventoryServiceMock(), new HRServiceMock());
            var ticketForUpdate = new TicketForUpdateDto()
            {
                Title = "What is up my dudez",
                Description = "This kenuWarew wont even turn on dudez",
                Status = "inProgress"
            };

            var result = controller.Put(TicketRepositoryMock.TestTicket.Id, ticketForUpdate);
            var okResult = result.Should().BeOfType<NoContentResult>().Subject;
        }

        [Fact]
        public void Delete_Ticket()
        {
            var controller = new TicketsController(new TicketRepositoryMock(), new ClientRepositoryMock(), new InventoryServiceMock(), new HRServiceMock());

            var result = controller.Delete(TicketRepositoryMock.TestTicket.Id);
            var resultOk = result.Should().BeOfType<NoContentResult>().Subject;
        }
    }
}
