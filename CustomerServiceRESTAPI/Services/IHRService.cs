using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerServiceRESTAPI.Entities;
using CustomerServiceRESTAPI.Models;

namespace CustomerServiceRESTAPI.Services
{
    public interface IHRService
    {
        Task<IEnumerable<AgentDto>> GetAgentsAsync();
        Task<AgentDto> GetAgentAsync(int id);
        Task<AuthHRDto> PostClientAsync(ClientForCreationDto client);
    }

    public class AuthHRDto
    {
        public string Token { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
    }
}
