using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CustomerServiceRESTAPI.Controllers;
using CustomerServiceRESTAPI.Models;
using CustomerServiceRESTAPI.Services;
using CustomerServiceRESTAPI.Tests.Mocks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace CustomerServiceRESTAPI.Tests.Controllers
{
    [Collection("StartupFixture collection")]
    public class ReviewsControllerTests
    {

        [Fact]
        public void Create_Review()
        {
            var controller = new ReviewsController(new ReviewRepositoryMock(), new ClientRepositoryMock());

            var reviewForCreation = new ReviewDtoForCreation()
            {
                AgentId = HRServiceMock.TestAgent.Id,
                Content = "This agent sucks!"
            };

            var result = controller.Post(reviewForCreation, ClientRepositoryMock.TestClient.Id);

            var okResult = result.Should().BeOfType<CreatedAtRouteResult>().Subject;
            var review = okResult.Value.Should().BeAssignableTo<ReviewWithClientDto>().Subject;

            review.Content.Should().Be(reviewForCreation.Content);

        }
        [Fact]
        public void Get_All_Review()
        {
            var controller = new ReviewsController(new ReviewRepositoryMock(), new ClientRepositoryMock());
        
            var result = controller.Get();
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var reviews = okResult.Value.Should().BeAssignableTo<IEnumerable<ReviewWithClientDto>>().Subject;
            reviews.Count().Should().Be(1);
        }

        [Fact]
        public void Get_Review()
        {
            var controller = new ReviewsController(new ReviewRepositoryMock(), new ClientRepositoryMock());

            var result = controller.Get(ReviewRepositoryMock.TestReview.Id);
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var review = okResult.Value.Should().BeAssignableTo<ReviewWithClientDto>().Subject;

            review.Content.Should().Be(ReviewRepositoryMock.TestReview.Content);
        }


        [Fact]
        public void Update_Review()
        {
            var controller = new ReviewsController(new ReviewRepositoryMock(), new ClientRepositoryMock());

            var reviewForUpdate = new ReviewDtoForUpdate()
            {
                Content = "Nevermind, this agent is awesome!"
            };

            var result = controller.Put(ReviewRepositoryMock.TestReview.Id, reviewForUpdate);
            var okResult = result.Should().BeOfType<NoContentResult>().Subject;
        }

        [Fact]
        public void Delete_Review()
        {
            var controller = new ReviewsController(new ReviewRepositoryMock(), new ClientRepositoryMock());

            var result = controller.Delete(ClientRepositoryMock.TestClient.Id);
            Assert.IsType<NoContentResult>(result);
        }
    }

}
