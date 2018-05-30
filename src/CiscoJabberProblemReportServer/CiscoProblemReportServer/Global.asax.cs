using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using NConfig;
using NLog;

namespace CiscoProblemReportServer
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            NConfigurator.UsingFiles(@"~\Config\Operations.config").SetAsSystemDefault();
            LogManager.ReconfigExistingLoggers();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                try
                {
                    var logger = LogManager.GetCurrentClassLogger();
                    var exception = e.ExceptionObject as Exception;
                    logger.Error(exception, "Unhandled exception in AppDomain occured, rethrowing...");
                }
                catch
                {
                }
            };
        }
    }
}
