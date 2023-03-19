using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ABCD_Mall_Online.Models
{
    public class Schedule
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ScheduleId { get; set; }

        [Display(Name = "Film")]
        public int? FilmId { get; set; }

        [Display(Name = "Room")]
        public int? RoomId { get; set; }

        [DataType(DataType.Date)]
        public DateTime ShowDate { get; set; }

        [DataType(DataType.Time)]
        public DateTime Time { get; set; }

        public Film Films { get; set; }

        public Room Rooms { get; set; }
    }
}
