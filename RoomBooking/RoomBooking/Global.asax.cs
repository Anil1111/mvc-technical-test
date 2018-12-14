using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using RoomBooking.Business;
using RoomBooking.Data;
using RoomBooking.Data.Entities;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;

namespace RoomBooking
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            var container = ConfigureDI(new Container());
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
        private Container ConfigureDI(Container container)
        {
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            container.Register<IAppDbContext, AppDbContext>(Lifestyle.Scoped);
            container.Register<ICustomSqlExecution, CustomSqlExecution>(Lifestyle.Scoped);

            container.Register<IRoomRepository, RoomRepository>(Lifestyle.Scoped);

            container.Register<IAuthenticationBusiness, AuthenticationBusiness>(Lifestyle.Scoped);
            container.Register<IDashboardBusiness, DashboardBusiness>(Lifestyle.Scoped);


            container.RegisterMvcControllers();
            container.RegisterMvcIntegratedFilterProvider();
            container.Verify();
            return container;
        }
    }
}
