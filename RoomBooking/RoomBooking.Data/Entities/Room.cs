using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomBooking.Data.Entities
{
    public class Room : Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public string Description { get; set; }

        /// <summary>
        /// A room can optionally have a maximum number of occupants
        /// </summary>
        public int? MaxOccupancy { get; set; }
        /// <summary>
        /// A room can have an optional enforced booking gap (in minutes), this will prevent bookings being created "back to back"
        /// </summary>
        public int? EnforcedBookingGap { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }

        [ForeignKey("Site")]
        public Guid SiteId { get; set; }

        public virtual Site Site { get; set; }
    }
}
