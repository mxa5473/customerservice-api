using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CustomerServiceRESTAPI.Models;
using CustomerServiceRESTAPI.Services;

namespace CustomerServiceRESTAPI.Tests.Mocks
{
    class HRServiceMock : IHRService
    {

        public static AgentDto TestAgent = new AgentDto()
        {
            Id = 1,
            FirstName = "John",
            LastName = "Smith",
            DateOfBirth = "5/5/1990",
            RegionName = "North America",
            RoleName = "Service Agent",
            DepartmentName = "Customer Service",
            PositionName = "Agent"
        };

        IEnumerable<AgentDto> _agents = new List<AgentDto>()
        {
            TestAgent
        };

        public Task<AgentDto> GetAgentAsync(int id)
        {
            return Task.Run(async () => TestAgent);
        }

        public Task<IEnumerable<AgentDto>> GetAgentsAsync()
        {
            return Task.Run(async () => _agents);
        }

        public Task<AuthHRDto> PostClientAsync(ClientForCreationDto client)
        {
            return Task.Run(async () => new AuthHRDto()
            {
                Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJhY2NvdW50VHlwZSI6ImNsaWVudCIsImlkIjoiMSJ9.D_FzD3ssFJFx9MZBVIoThMC5T3HiM86kSKOcoBmIvuk",
            });
        }
    }
}
