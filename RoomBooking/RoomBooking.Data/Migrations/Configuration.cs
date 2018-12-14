using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RoomBooking.Data.Entities;

namespace RoomBooking.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<RoomBooking.Data.AppDbContext>
    {
        private Random random;

        public Configuration()
        {
            random = new Random(DateTime.Now.Millisecond);
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(RoomBooking.Data.AppDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            var userManager = new UserManager<AppUser>(new UserStore<AppUser>(context));
            var defaultUser = userManager.FindByEmailAsync("john.smith@bookwisesolutions.com");
            defaultUser.Wait();

            if (defaultUser.Result == null)
            {
                userManager.CreateAsync(new AppUser()
                {
                    Name = "John Smith",
                    Email = "john.smith@bookwisesolutions.com",
                    UserName = "john.smith@bookwisesolutions.com"
                }, "test1234").Wait();
            }
            userManager.Dispose();

            context.Sites.AddOrUpdate(x => x.Description, new Site()
            {
                Description = "Site 1",
                Rooms = new List<Room>()
                {
                    new Room(){Description = "Anslow",EnforcedBookingGap = 15,MaxOccupancy = 6},
                    new Room(){Description = "Doveridge", MaxOccupancy = 6},
                    new Room(){Description = "Hatton",EnforcedBookingGap = 5,MaxOccupancy = 4},
                    new Room(){Description = "Hilton",MaxOccupancy = 30},
                    new Room(){Description = "Marchington",EnforcedBookingGap = 15,MaxOccupancy = 10},
                }
            });

            context.Sites.AddOrUpdate(x => x.Description, new Site()
            {
                Description = "Site 2",
                Rooms = new List<Room>()
                {
                    new Room(){Description = "Tutbury",EnforcedBookingGap = 10,MaxOccupancy = 15},
                    new Room(){Description = "Hanbury", MaxOccupancy = 6},
                    new Room(){Description = "Sudbury",EnforcedBookingGap = 5,MaxOccupancy = 4},
                    new Room(){Description = "Ticknall",MaxOccupancy = 25},
                    new Room(){Description = "Repton",EnforcedBookingGap = 10,MaxOccupancy = 10},
                    new Room(){Description = "Willington",EnforcedBookingGap = 15,MaxOccupancy = 50},
                    new Room(){Description = "Fradley",EnforcedBookingGap = 15,MaxOccupancy = 50},
                }
            });

            context.SaveChanges();

            CreateBookings(context);

        }

        private void CreateBookings(AppDbContext context)
        {
            var user = context.Users.First().Id;
            var randomDates = GenerateRandomDates(5000);
            GenerateAppointmentsAcrossDates(randomDates, user, context);

        }

        private void GenerateAppointmentsAcrossDates(List<DateTime> randomDates,string user, AppDbContext context)
        {
            var apptDesc = new string[]
            {
                "Maternity Clinic",
                "Internal Meeting",
                "6 Month Review",
                "Oncology Dept",
                "Pharmacy Training",
                "Internal Training",
                "Paediatric Endocrine",
                "Paediatric Dermatology",
                "Paediatric Cystic Fibrosis",
                "Education Centre - Trainee Doctor Appraisal",
                "Clinical Immunology & Allergy",
                "TIA/STROKE",
                "Family Lipid Clinic (Dr Gan/Dr Abitha)",
                "Weight Management grp",
                "Clinical Trials Haematologist 1 & 4"
            };
            foreach (var date in randomDates)
            {
                var rooms = context.Rooms.ToList();
                rooms.Shuffle();
                var apptStartTime = date.AddHours(random.Next(8, 15));
                var apptFinishTime = apptStartTime.AddHours(random.Next(1, 3));


                var room = rooms.FirstOrDefault(
                    x => x.Bookings == null || !x.Bookings.Any(b => b.StartTime >= apptStartTime.Date &&
                                              b.FinishTime <= apptStartTime.Date.AddDays(1)));
                if(room == null)
                    continue;

                
                context.Bookings.AddOrUpdate(new Booking()
                {
                    CreatedByUserId = user,
                    CreatedTimestamp = DateTime.Now,
                    StartTime = apptStartTime,
                    FinishTime = apptFinishTime,
                    Description = apptDesc[random.Next(0, apptDesc.Length)],
                    NumberOfAttendees = random.Next(1, room.MaxOccupancy.GetValueOrDefault(10)),
                    RoomId = rooms.First(x => x.MaxOccupancy == null || x.MaxOccupancy > 2).Id,
                    Status = GetWeightedBookingStatus()
                });
                context.SaveChanges();
            }
        }

        private BookingStatus GetWeightedBookingStatus()
        {
            var number = random.Next(0, 100);
            if(number % 5 == 0)
                return BookingStatus.CANCELLED;
            if(number % 3 == 0)
                return BookingStatus.PROVISIONAL;
            return BookingStatus.CONFIRMED;
        }

        private List<DateTime> GenerateRandomDates(int count)
        {
            var result = new List<DateTime>();
            for (int i = 0; i < count; i++)
            {
                result.Add(DateTime.Today.AddDays(random.Next(0, 365)));
            }
            return result;
        }
    }

    public static class ListExtensions
    {
        private static Random rng = new Random();
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
