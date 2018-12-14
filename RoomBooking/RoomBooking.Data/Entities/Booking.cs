using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomBooking.Data.Entities
{
    public class Booking : Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime FinishTime { get; set; }

        [Required]
        public int NumberOfAttendees { get; set; }

        [Required]
        public BookingStatus Status { get; set; }

        [Required]
        public DateTime CreatedTimestamp { get; set; }

        [Required]
        [ForeignKey("CreatedByUser")]
        public string CreatedByUserId { get; set; }

        public virtual AppUser CreatedByUser { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [ForeignKey("Room")]
        public Guid RoomId { get; set; }

        public virtual Room Room { get; set; }

        public string ToBookingTimeString()
        {
            return $"{StartTime.ToShortTimeString()} - {FinishTime.ToShortTimeString()}";
        }
    }
}
