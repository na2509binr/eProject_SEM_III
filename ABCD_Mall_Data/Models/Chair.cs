using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ABCD_Mall_Data.Models
{
    [Table("Chairs")]
    public class Chair
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ChairId { get; set; }

        [Display(Name ="Room")]
        public int? RoomId { get; set; }

        [StringLength(150)]
        [Required]
        public string NameChair { get; set; }

        [Required]
        public float Price { get; set; }

        public byte Status { get; set; }

        public Room Rooms { get; set; }
    }
}
