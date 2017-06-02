﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using PaperWall.Core.Infrastructure;
using PaperWall.Core.Repository;
using PaperWall.Infrastructure.Unity;
using PaperWall.Infrastructure.Web;

namespace Paperwall.Api
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            ModelBinders.Binders.Add(typeof(double), new DoubleModelBinder());

            IoC.Initialize(new UnityDependencyResolver());

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Session_Start()
        {
            IoC.Resolve<IMessageRepository>().Initialize();
        }

        protected void Session_End()
        {
            IoC.Resolve<IMessageRepository>().Finalize();
        }
    }
}