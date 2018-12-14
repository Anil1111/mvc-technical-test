using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RoomBooking.Data.Entities;

namespace RoomBooking.Models
{
    public class DashboardViewModel
    {
        public IEnumerable<Room> DashboardData { get; set; }

        public IEnumerable<RoomsGroupedBySite> GroupedData
        {
            get
            {
                var results = from d in DashboardData
                    group d by d.SiteId
                    into s
                    select new RoomsGroupedBySite { SiteId = s.Key, Rooms = s.ToList()};
                return results;
            }
        }
    }

    public class RoomsGroupedBySite
    {
        public Guid SiteId { get; set; }

        public Site Site
        {
            get { return Rooms.First(x => x.SiteId == SiteId).Site; }
        }

        public List<Room> Rooms { get; set; }
    }
}