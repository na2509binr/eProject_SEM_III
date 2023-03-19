using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ABCD_Mall_Online.Models
{
    public class FeedBack
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FeedbackId { get; set; }

        [Display(Name = "Customer")]
        public int? CustomerId { get; set; }

        [Required]
        public string Description { get; set; }

        public Customer Customer { get; set; }
    }
}
