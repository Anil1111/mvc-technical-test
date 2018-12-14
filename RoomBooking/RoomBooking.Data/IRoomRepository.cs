using System.Collections.Generic;
using RoomBooking.Data.Entities;

namespace RoomBooking.Data
{
    public interface IRoomRepository
    {
        IEnumerable<Room> GetAllRoomsWithCurrentBookings();
    }
}