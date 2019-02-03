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

namespace CustomerServiceRESTAPI.Tests.Controllers
{
    [Collection("StartupFixture collection")]
    public class CommentsControllerTests
    {
        [Fact]
        public void Create_Comment()
        {
            var controller = new CommentsController(new CommentRepositoryMock());

            var commentForCreationAgent = new CommentForCreationDto()
            {
                Content = "There is an issue with this item!",
                TicketId = TicketRepositoryMock.TestTicket.Id

            };

            var commentForCreationClient = new CommentForCreationDto()
            {
                Content = "There is an issue with this item!",
                TicketId = TicketRepositoryMock.TestTicket.Id
            };

            var result = controller.Post(commentForCreationAgent, -1,  ClientRepositoryMock.TestClient.Id);

            var okResult = result.Should().BeOfType<CreatedAtRouteResult>().Subject;
            var comment = okResult.Value.Should().BeAssignableTo<CommentWithTicketsDto>().Subject;
                    }

        [Fact]
        public void Get_All_Comment()
        {
            var controller = new CommentsController(new CommentRepositoryMock());

            var result = controller.Get();
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var comments = okResult.Value.Should().BeAssignableTo<IEnumerable<CommentWithTicketsDto>>().Subject;
            comments.Count().Should().Be(1);
        }

        [Fact]
        public void Get_Comment()
        {
            var controller = new CommentsController(new CommentRepositoryMock());

            var result = controller.Get(CommentRepositoryMock.TestComment.Id);
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var comment = okResult.Value.Should().BeAssignableTo<CommentWithTicketsDto>().Subject;

        }

        [Fact]
        public void Update_Comment()
        {
            var controller = new CommentsController(new CommentRepositoryMock());
            var commentForUpdate = new CommentForUpdateDto()
            {
                Content = "There is an issue with this item!"
            };

            var result = controller.Put(CommentRepositoryMock.TestComment.Id, commentForUpdate);
            var okResult = result.Should().BeOfType<NoContentResult>().Subject;
        }

        [Fact]
        public void Delete_Comment()
        {
            var controller = new CommentsController(new CommentRepositoryMock());

            var result = controller.Delete(CommentRepositoryMock.TestComment.Id);
            var OkResult = result.Should().BeOfType<NoContentResult>().Subject;
        }
    }
}
