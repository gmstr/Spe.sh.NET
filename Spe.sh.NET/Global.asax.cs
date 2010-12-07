using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using Ninject.Modules;
using Spe.sh.NET.Data;
using Spe.sh.NET.Data.SQL;

namespace Spe.sh.NET
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class Application : Ninject.Web.Mvc.NinjectHttpApplication
    {
        public enum AppEnvironment
        {
            Development,
            Test,
            Production
        }
        public static AppEnvironment Environment
        {
            get
            {
#if DEBUG
                return AppEnvironment.Development;
#else

#endif
                return AppEnvironment.Production;
            }
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("favicon.ico");

            routes.MapRoute("Tweetdeck Shorten",
                "s",
                new { controller = "home", action = "shorten" });

            routes.MapRoute("Home",
                "",
                new { controller = "home", action = "index" });

            routes.MapRoute("Install",
                "install",
                new { controller = "install", action = "index" });

            routes.MapRoute("NotFound",
                "error/notfound",
                new { controller = "error", action = "notfound" });

            routes.MapRoute("Problem",
                "error/problem",
                new { controller = "error", action = "problem" });

            routes.MapRoute("Shortened",
                "{token}",
                new { controller = "home", action = "direct", token = "token" });
        }
        protected override void OnApplicationStarted()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterRoutes(RouteTable.Routes);
        }

        protected override Ninject.IKernel CreateKernel()
        {
            return Container;   
        }

        internal class SpeshModule : NinjectModule
        {
            public override void Load()
            {
                Bind<IUrlRepository>().To<SqlUrlRepository>();
            }
        }

        public static IUrlRepository Repository
        {
            get
            {
                return Container.Get<IUrlRepository>();
            }
        }

        static IKernel _container;
        public static IKernel Container
        {
            get
            {
                if (_container == null)
                {
                    _container = new StandardKernel(new SpeshModule());
                }
                return _container;
            }
        }
    }
}