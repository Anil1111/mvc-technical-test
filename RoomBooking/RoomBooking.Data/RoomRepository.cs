using System.Collections.Generic;
using Dapper;
using RoomBooking.Data.Entities;

namespace RoomBooking.Data
{
    public class RoomRepository : IRoomRepository
    {
        private readonly ICustomSqlExecution _customSqlExecution;

        public RoomRepository(ICustomSqlExecution customSqlExecution)
        {
            _customSqlExecution = customSqlExecution;
        }
        public IEnumerable<Room> GetAllRoomsWithCurrentBookings()
        {
            var sql =
                "select r.Id, r.Description, r.SiteId, b.Id, b.StartTime, b.FinishTime, b.Description, s.Id, s.Description " +
                "from Rooms r " +
                "left join Bookings b on b.RoomId = r.Id " +
                "and b.StartTime <= GETDATE() and b.FinishTime >= GETDATE() " +
                "inner join Sites s on r.SiteId = s.Id;";
            var rooms = _customSqlExecution.SqlConnection().Query<Room, Booking, Site, Room>(sql,
                (room, booking, site) =>
                {
                    room.Site = site;
                    room.Bookings = new List<Booking>();
                    if (booking != null)
                    {
                        room.Bookings.Add(booking);
                    }
                    return room;
                });

            return rooms; 
        }

    }
}