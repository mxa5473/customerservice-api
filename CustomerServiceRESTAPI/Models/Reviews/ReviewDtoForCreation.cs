using System;
using System.ComponentModel.DataAnnotations;

namespace CustomerServiceRESTAPI.Models
{
    public class ReviewDtoForCreation
    {
        [Required(ErrorMessage = "Agent Id is required")]
        public int AgentId { get; set; }

        [Required(ErrorMessage = "Content is required")]
        public string Content { get; set; }
    }
}
