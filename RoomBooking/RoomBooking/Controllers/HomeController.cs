using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NLog;
using RoomBooking.Business;
using RoomBooking.Models;

namespace RoomBooking.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDashboardBusiness _dashboardBusiness;

        public HomeController(IDashboardBusiness dashboardBusiness )
        {
            _dashboardBusiness = dashboardBusiness;
        }

        // GET: Home
        public ActionResult Index()
        {
            var model = new DashboardViewModel()
            {
                DashboardData = _dashboardBusiness.GetDashboardData()
            };
            return View(model);
        }
    }

}