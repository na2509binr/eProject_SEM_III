using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ABCD_Mall_Online.Models
{
    public class Ticket
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string TicketId { get; set; }

        [Display(Name = "Schedule")]
        public int? ScheduleId { get; set; }

        [Display(Name = "Chair")]
        public int? ChairId { get; set; }

        public Schedule Schedules { get; set; }

        public Chair Chairs { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
