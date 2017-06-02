using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using PaperWall.Core.Infrastructure;
using PaperWall.Core.Repository;
using PaperWall.Infrastructure.Unity;
using PaperWall.Infrastructure.Web;

namespace PaperWall.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Message", action = "Test", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Session_Start()
        {
            IoC.Resolve<IMessageRepository>().Initialize();
        }

        protected void Session_End()
        {
            IoC.Resolve<IMessageRepository>().Finalize();
        }

        protected void Application_Start()
        {
            ModelBinders.Binders.Add(typeof(double), new DoubleModelBinder());

            IoC.Initialize(new UnityDependencyResolver());
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}