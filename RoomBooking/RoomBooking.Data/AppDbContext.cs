using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using RoomBooking.Data.Entities;

namespace RoomBooking.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>, IAppDbContext
    {
        public AppDbContext() : base("DefaultConnection")
        {
            Database.SetInitializer<AppDbContext>(new CreateDatabaseIfNotExists<AppDbContext>());
        }

        public virtual IDbSet<Site> Sites { get; set; }
        public virtual IDbSet<Room> Rooms { get; set; }
        public virtual IDbSet<Booking> Bookings { get; set; }

        public override DbSet Set(Type entityType)
        {
            return base.Set(entityType);
        }

        public new void SaveChanges()
        {
            base.SaveChanges();
        }
    }
}
