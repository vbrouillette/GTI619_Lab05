using GTI619_Lab5.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace GTI619_Lab5
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static ApplicationContext _context;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            _context = new ApplicationContext();
        }

        protected void Session_OnStart(object sender, EventArgs e)
        {
            // Code that runs when a new session is started
            //HttpContext.Current.Session.Timeout = _context.AuthentificationConfigs.First().GetTimeOutSession();
        }
    }
}
