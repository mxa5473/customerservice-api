using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CustomerServiceRESTAPI.Services;
using CustomerServiceRESTAPI.Entities;
using CustomerServiceRESTAPI.Models;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;

namespace CustomerServiceRESTAPI.Controllers
{
    [Route("api/[controller]")]
    public class TicketsController : Controller
    {
        ITicketRepository _ticketRepository;
        IDBRepository<Client> _clientRepository;
        IInventoryService _inventoryService;
        IHRService _hrService;

        public TicketsController(ITicketRepository ticketRepository, IDBRepository<Client> clientRepository, IInventoryService inventoryService, IHRService hrService)
        {
            _ticketRepository = ticketRepository;
            _clientRepository = clientRepository;
            _inventoryService = inventoryService;
            _hrService = hrService;
        }

        // GET: api/tickets
        [HttpGet]
        public IActionResult Get([FromQuery(Name = "productSerialNumber")]string productSerialNumber, [FromQuery(Name = "clientId")]int clientId = -1, [FromQuery(Name = "agentId")]int agentId = -1)
        {
            var allTickets = _ticketRepository.GetAll();
            var tickets = allTickets;

            if (clientId != -1) tickets = allTickets.Where(t => t.ClientId == clientId);
            if (agentId != -1) tickets = allTickets.Where(t => t.AgentId == agentId);
            if (productSerialNumber != null) tickets = tickets.Where(t => t.ProductSerialNumber == productSerialNumber);

            var result = AutoMapper.Mapper.Map<IEnumerable<TicketWithClientAndAgentDto>>(tickets);

            return Ok(result);
        }

        // GET api/tickets/5
        [HttpGet("{id}", Name = "GetTicket")]
        public IActionResult Get(int id)
        {
            var ticket = _ticketRepository.Get(id);
            if (ticket == null) return NotFound("Could not find ticket");

            var result = AutoMapper.Mapper.Map<TicketWithClientAndAgentDto>(ticket);
            return Ok(result);
        }

        // POST api/tickets
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]TicketForCreationDto ticketForCreation, [FromQuery(Name = "clientId")]int clientId)
        { 
            var ticket = AutoMapper.Mapper.Map<Ticket>(ticketForCreation);
            // Look up the client by id
            var client = _clientRepository.Get(clientId);
            // Ensure client exists
            if (client == null) return NotFound("Client not found");
            // Look up the product by serial number
            var productDetails = await _inventoryService.GetProductAsync(ticketForCreation.ProductSerialNumber);
            // Ensure the product exists
            if (productDetails == null)
            {
                return NotFound("Serial number not found");
            }
            // Ensure the product has been sold
            if (!(productDetails.Status == "sold"))
            {
                return BadRequest("This device has not been sold");
            }
            // Create the ticket
            // Tickets are set to Status=new by default
            ticket.Status = "new";
            // Tickets are associated with the product they were created for
            ticket.ProductSerialNumber = productDetails.SerialNumber;

            // Assign ticket to the agent with the lowest number of assigned tickets
            ticket.AgentId = (await _hrService.GetAgentsAsync()).OrderBy(a => _ticketRepository.GetAllByAgent(a.Id).Count()).First().Id;

            client.Tickets.Add(ticket);
            _clientRepository.Update(client);
            if (!_clientRepository.Save()) return BadRequest("Could not create ticket");
            var result = AutoMapper.Mapper.Map<TicketWithClientAndAgentDto>(ticket);
            return CreatedAtRoute("GetTicket", new { id = ticket.Id }, result);
        }

        // PUT api/tickets/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]TicketForUpdateDto ticketForUpdate)
        {
            var ticket = _ticketRepository.Get(id);
            if (ticket == null) return NotFound("Could not find ticket");

            ticket.Title = ticketForUpdate.Title != null ? ticketForUpdate.Title : ticket.Title;
            ticket.Description = ticketForUpdate.Description != null ? ticketForUpdate.Description : ticket.Description;
            ticket.Status = ticketForUpdate.Status != null ? ticketForUpdate.Status : ticket.Status;

            _ticketRepository.Update(ticket);
            if (!_ticketRepository.Save()) return BadRequest("Could not update ticket");

            return NoContent();
        }

        // DELETE api/tickets/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var ticket = _ticketRepository.Get(id);
            if (ticket == null) return NotFound("Could not find ticket");

            _ticketRepository.Delete(ticket);
            if (!_ticketRepository.Save()) return BadRequest("Could not remove ticket");

            return NoContent();
        }

        // POST api/tickets/5/replace
        [Route("{id}/replace")]
        [HttpPost]
        public async Task<IActionResult> Replace(int id)
        {
            // Look up the ticket
            var ticket = _ticketRepository.Get(id);
            if (ticket == null) return NotFound("Could not find ticket");
            // Place the device replacement request
            if ((await new SalesService().RequestReplacementDevice(ticket)))
            {
                // Request succeeded
                return Ok();
            }
            // Request failed
            return BadRequest("Sales' endpoint is still down");
        }
    }
}
