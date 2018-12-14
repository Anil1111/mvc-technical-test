using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoomBooking.Data;
using RoomBooking.Data.Entities;

namespace RoomBooking.Business
{
    public interface IDashboardBusiness
    {
        IEnumerable<Room> GetDashboardData();
    }

    public class DashboardBusiness : IDashboardBusiness
    {
        private readonly IRoomRepository _roomRepository;

        public DashboardBusiness(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public IEnumerable<Room> GetDashboardData()
        {
            return _roomRepository.GetAllRoomsWithCurrentBookings();
        }
    }
}
