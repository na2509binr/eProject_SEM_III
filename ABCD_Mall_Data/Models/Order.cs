using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ABCD_Mall_Data.Models
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrdersId { get; set; }

        [Display(Name ="Customer")]
        public int? CustomerId { get; set; }

        [Display(Name = "Ticket")]
        public string TicketId { get; set; }

        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }

        [Required]
        public int Count { get; set; }

        public float TotalPrice { get; set; }

        public Customer Customers { get; set; }

        public Ticket Tickets { get; set; }


    }
}
