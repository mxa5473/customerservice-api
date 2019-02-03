using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerServiceRESTAPI.Entities
{
    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        public string DateCreated { get; set; }

        [ForeignKey("TicketId")]
        public Ticket Ticket{ get; set; } 
        public int TicketId { get;  set;}

        public int ClientId { get; set; }

        public int AgentId { get; set; }

    }
}
