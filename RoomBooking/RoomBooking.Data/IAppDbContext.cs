using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using RoomBooking.Data.Entities;

namespace RoomBooking.Data
{
    public interface IAppDbContext
    {
        DbSet Set(Type T);

        IDbSet<AppUser> Users { get; set; }
        IDbSet<IdentityRole> Roles { get; set; }
        Database Database { get; }
        DbChangeTracker ChangeTracker { get; }
        DbContextConfiguration Configuration { get; }
        IDbSet<Site> Sites { get; set; }
        IDbSet<Room> Rooms { get; set; }
        IDbSet<Booking> Bookings { get; set; }
        void SaveChanges();
    }
}