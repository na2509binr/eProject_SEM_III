using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ABCD_Mall_Online.Models
{
    public class Store
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StoreId { get; set; }

        [StringLength(150)]
        [Required]
        public string Name { get; set; }


        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Phone number is not in the correct format")]
        [Required]
        public string Phone { get; set; }

        [Required]
        [StringLength(150)]
        public string Address { get; set; }

        public string image { get; set; }

        [Required]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Category")]
        [Required]
        public string CategoryId { get; set; }

        public Category Categories { get; set; }
    }
}
