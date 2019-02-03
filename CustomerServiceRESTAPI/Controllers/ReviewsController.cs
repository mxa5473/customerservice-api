using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using CustomerServiceRESTAPI.Models;
using CustomerServiceRESTAPI.Entities;
using CustomerServiceRESTAPI.Services;
using System.IdentityModel.Tokens.Jwt;

namespace CustomerServiceRESTAPI.Controllers
{
    [Route("api/[controller]")]
    public class ReviewsController : Controller
    {
        IDBRepository<Review> _reviewRepository;
        IDBRepository<Client> _clientRepository;

        public ReviewsController(IDBRepository<Review> reviewRepository, IDBRepository<Client> clientRepository)
        {
            _reviewRepository = reviewRepository;
            _clientRepository = clientRepository;
        }

        // GET: api/reviews
        [HttpGet]
        public IActionResult Get([FromQuery(Name = "clientId")]int clientId = -1, [FromQuery]int agentId = -1)
        {
            var reviews = _reviewRepository.GetAll();

            if (clientId != -1) reviews = reviews.Where(r => r.ClientId == clientId);
            if (agentId != -1) reviews = reviews.Where(r => r.AgentId == agentId);

            var results = Mapper.Map<IEnumerable<ReviewWithClientDto>>(reviews);

            return Ok(results);
        }

        // GET: api/reviews/1
        [HttpGet("{id}", Name = "GetReview")]
        public IActionResult Get(int id)
        {
            var review = _reviewRepository.Get(id);
            if (review == null) return NotFound();

            var result = Mapper.Map<ReviewWithClientDto>(review);
            return Ok(result);
        }

        // Get api/reviews
        [HttpPost]
        public IActionResult Post([FromBody]ReviewDtoForCreation review, [FromQuery(Name = "clientId")]int clientId)
        {
            var finalReview = Mapper.Map<Review>(review);
            var client = _clientRepository.Get(clientId);
            if (client == null) return NotFound("Could not find client");

            // Set default time created
            finalReview.DateCreated = DateTime.Now.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'");

            client.Reviews.Add(finalReview);
            if (!_clientRepository.Save()) return BadRequest("Could not create review");


            var result = AutoMapper.Mapper.Map<ReviewWithClientDto>(finalReview);
            return CreatedAtRoute("GetReview", new { id = finalReview.Id }, result);
        }

        // PUT api/reviews/1
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]ReviewDtoForUpdate reviewData)
        {
            if (reviewData == null) return BadRequest();

            var review = _reviewRepository.Get(id);
            if (review == null) return NotFound();

            review.Content = reviewData.Content == null ? review.Content : reviewData.Content;

            _reviewRepository.Update(review);
            if (!_reviewRepository.Save()) return BadRequest();

            return NoContent();
        }

        // DELETE api/reviews/1
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var review = _reviewRepository.Get(id);
            if (review == null) NotFound();

            _reviewRepository.Delete(review);
            if (!_reviewRepository.Save())
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}