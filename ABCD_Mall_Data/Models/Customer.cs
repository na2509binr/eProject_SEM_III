using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ABCD_Mall_Data.Models
{
    [Table("Customers")]
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }

        [StringLength(150)]
        public string CustomerName { get; set; }

        [MinLength(6)]
        public string Password { get; set; }

        public byte Gender { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [StringLength(150)]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Phone number is not in the correct format")]
        [Required]
        public string Phone { get; set; }

        [Required]
        [StringLength(150)]
        public string Address { get; set; }

        public ICollection<FeedBack> FeedBacks { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
