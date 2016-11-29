using GTI619_Lab5.Entities;
using GTI619_Lab5.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static GTI619_Lab5.Controllers.AccountController;

namespace GTI619_Lab5
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            var context = new ApplicationDbContext();
            var userManager = new MyUserManager(new UserStore<ApplicationUser>(context));

            filters.Add(new HandleErrorAttribute());
            filters.Add(new RedirectOnSessionTimeOut());
            filters.Add(new ValidatePasswordChangeNeeded(context, userManager));
            filters.Add(new RedirectOnNeedNewPass(userManager));
        }
    }

    public class AllowSessionTimeOut : ActionFilterAttribute { } // Use to skip follwing filter
    public class RedirectOnSessionTimeOut : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            
            if (!filterContext.ActionDescriptor.GetCustomAttributes(typeof(AllowSessionTimeOut), false).Any())
            {
                HttpSessionStateBase session = filterContext.HttpContext.Session;

                if (filterContext.HttpContext.User.Identity.IsAuthenticated &&
                    session.IsNewSession)
                {
                    
                    filterContext.HttpContext.GetOwinContext().Authentication.SignOut();

                    filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary
                    {
                        { "controller", "Account" },
                        { "action", "Login" },
                        { "message", "Session expirée!" },
                        { "returnUrl", filterContext.HttpContext.Request.Path }
                    });
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }

    public class AllowNeedNewPass : ActionFilterAttribute { } // Use to skip follwing filter
    public class RedirectOnNeedNewPass : ActionFilterAttribute
    {
        public UserManager<ApplicationUser> UserManager { get; private set; }

        public RedirectOnNeedNewPass(UserManager<ApplicationUser> userManager)
        {
            this.UserManager = userManager;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.ActionDescriptor.GetCustomAttributes(typeof(AllowNeedNewPass), false).Any())
            {
                var userId = ((Controller)filterContext.Controller).User.Identity.GetUserId();
                if (userId != null)
                {
                    var u = UserManager.FindById(userId);

                    if (u.NeedNewPassword)
                    {
                        filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary
                        {
                            { "controller", "Account" },
                            { "action", "Manage" },
                            { "Message", ManageMessageId.HaveToChange }
                        });
                    }
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }

    public class ValidatePasswordChangeNeeded : ActionFilterAttribute
    {
        private UserManager<ApplicationUser> UserManager;
        private ApplicationDbContext _context;

        public ValidatePasswordChangeNeeded(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            this.UserManager = userManager;
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                var user = UserManager.FindById(filterContext.HttpContext.User.Identity.GetUserId());
                var config = _context.AuthentificationConfigs.First();
                if (config.IsPeriodic && !user.NeedNewPassword)
                {
                    var lastPasswordChangeDate = DateTime.Now.Subtract(TimeSpan.FromMinutes(5)); // TODO Fetch from DB.
                    var timeSinceLastChange = DateTime.Now.Subtract(lastPasswordChangeDate);

                    if (timeSinceLastChange.TotalMinutes > config.PeriodPeriodic)
                    {
                        user.NeedNewPassword = true;
                        UserManager.Update(user);
                        _context.SaveChanges();
                    }
                }
            }

            base.OnResultExecuted(filterContext);
        }
    }
}
