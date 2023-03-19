using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ABCD_Mall_Online.Models
{
    public class Room
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoomId { get; set; }

        [StringLength(150)]
        public string Name { get; set; }

        public int TotalChair { get; set; }

        public ICollection<Schedule> Schedules { get; set; }
        public ICollection<Chair> Chairs { get; set; }
    }
}
