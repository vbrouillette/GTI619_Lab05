using GTI619_Lab5.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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
        private static ApplicationDbContext _context;
        private static UserManager<ApplicationUser> UserManager { get; set; }

        protected void Application_Start()
        {
            _context = new ApplicationDbContext();
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Session_OnStart(object sender, EventArgs e)
        {
            // Code that runs when a new session is started
            HttpContext.Current.Session.Timeout = _context.AuthentificationConfigs.First().TimeOutSession;
        }
    }
}
