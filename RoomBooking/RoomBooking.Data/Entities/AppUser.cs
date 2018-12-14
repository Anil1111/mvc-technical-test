using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace RoomBooking.Data.Entities
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
