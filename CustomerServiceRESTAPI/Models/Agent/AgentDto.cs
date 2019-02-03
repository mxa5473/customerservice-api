using System;
namespace CustomerServiceRESTAPI.Models
{
    public class AgentDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public string RegionName { get; set; }
        public string RoleName { get; set; }
        public string DepartmentName { get; set; }
        public string PositionName { get; set; }
    }
}
