using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CustomerServiceRESTAPI.Services;
using CustomerServiceRESTAPI.Models;
using CustomerServiceRESTAPI.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomerServiceRESTAPI.Controllers
{
    [Route("api/[controller]")]
    public class CommentsController : Controller
    {
        IDBRepository<Comment> _commentRepository;

        public CommentsController(IDBRepository<Comment> commentRepository)
        {
            _commentRepository = commentRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var comments = _commentRepository.GetAll();
            var result = AutoMapper.Mapper.Map<IEnumerable<CommentWithTicketsDto>>(comments);

            return Ok(result);
        }

        [HttpGet("{id}", Name="GetComment")]
        public IActionResult Get(int id)
        {
            var comment = _commentRepository.Get(id);
            if (comment == null) return NotFound("Comment not found");

            var result = AutoMapper.Mapper.Map<CommentWithTicketsDto>(comment);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post([FromBody]CommentForCreationDto commentForCreation, [FromQuery]int agentId = -1, [FromQuery]int clientId = -1)
        {
            if (clientId == -1 && agentId == -1) return BadRequest("ClientId or AgentId is required");

            var comment = AutoMapper.Mapper.Map<Comment>(commentForCreation);
            comment.ClientId = clientId;
            comment.AgentId = agentId;
            //comment.DateCreated = DateTime.Now.ToString();

            _commentRepository.Add(comment);
            if (!_commentRepository.Save()) return BadRequest("Could not create comment");

            var result = AutoMapper.Mapper.Map<CommentWithTicketsDto>(comment);
            return CreatedAtRoute("GetComment", new { id = comment.Id }, result);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]CommentForUpdateDto commentForUpdate)
        {
            var comment = _commentRepository.Get(id);
            if (comment == null) return NotFound("Comment not found");

            comment.Content = commentForUpdate.Content != null ? commentForUpdate.Content : comment.Content;

            _commentRepository.Update(comment);
            if (!_commentRepository.Save()) return BadRequest("Could not update comment");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var comment = _commentRepository.Get(id);
            if (comment == null) return NotFound("Comment not found");

            _commentRepository.Delete(comment);
            if (!_commentRepository.Save()) return BadRequest("Could not delete client");

            return NoContent();
        }
    }
}