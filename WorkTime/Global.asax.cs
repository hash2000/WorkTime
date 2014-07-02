using Autofac;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WorkTime.Models;
using WorkTime.Utils.Automapper;
using WorkTime.Utils.IoC;
using WorkTime.Utils.MVC.Controllers;

namespace WorkTime
{
    public class MvcApplication : System.Web.HttpApplication
    {

        protected void Application_Start()
        {

            var currentassembly = System.Reflection.Assembly.GetExecutingAssembly();
            var builder = new ContainerBuilder();
            // Autofac конфигуратор

            AutofacConfigurator.ConfigureDbContextEntity<ContributorsEntities>(builder);
            AutofacConfigurator.Configure(currentassembly, builder);
            //_containerProvider = new ContainerProvider(Container);


            DependencyResolver.SetResolver(new AutofacDependencyResolver(AutofacConfigurator.Build(builder)));

            // Automapper конфигурация
            AutoMapperConfigurator.Configure();


            ControllerBuilder.Current.SetControllerFactory(typeof(ProtectedControllerFactory));


            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
