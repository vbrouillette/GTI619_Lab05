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

            filters.Add(new HandleErrorAttribute());
            filters.Add(new RedirectOnSessionTimeOut());
            filters.Add(new ValidateUserPhone(context));
            filters.Add(new ValidatePasswordChangeNeeded(context));
            filters.Add(new RedirectOnNeedNewPass(context));
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

    public class AllowInvalidUserPhone : ActionFilterAttribute { } // Use to skip follwing filter
    public class ValidateUserPhone : ActionFilterAttribute
    {
        public ApplicationDbContext context { get; private set; }

        public ValidateUserPhone(ApplicationDbContext context)
        {
            this.context = context;
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var config = context.Set<AuthentificationConfig>().AsNoTracking().FirstOrDefault();

            if (config.StrongAuthentication && !filterContext.ActionDescriptor.GetCustomAttributes(typeof(AllowInvalidUserPhone), false).Any())
            {
                var userId = ((Controller)filterContext.Controller).User.Identity.GetUserId();
                if (userId != null)
                {
                    var u = context.Set<ApplicationUser>().AsNoTracking().Where(c => c.Id == userId).First();

                    if (!u.Validated)
                    {
                        filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary
                        {
                            { "controller", "Account" },
                            { "action", "ValidatePhone" },
                            { "returnUrl", filterContext.HttpContext.Request.Path }
                        });
                    }
                }
            }

            base.OnActionExecuted(filterContext);
        }
    }

    public class AllowNeedNewPass : ActionFilterAttribute { } // Use to skip follwing filter
    public class RedirectOnNeedNewPass : ActionFilterAttribute
    {
        public ApplicationDbContext context { get; private set; }

        public RedirectOnNeedNewPass(ApplicationDbContext context)
        {
            this.context = context;
        }


        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (!filterContext.ActionDescriptor.GetCustomAttributes(typeof(AllowNeedNewPass), false).Any())
            {
                var userId = ((Controller)filterContext.Controller).User.Identity.GetUserId();
                if (userId != null)
                {
                    var u = context.Set<ApplicationUser>().AsNoTracking().Where(c => c.Id == userId).First();

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

            base.OnActionExecuted(filterContext);
        }
    }

    public class ValidatePasswordChangeNeeded : ActionFilterAttribute
    {
        private ApplicationDbContext _context;

        public ValidatePasswordChangeNeeded(ApplicationDbContext context)
        {
            _context = context;
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                var userId = ((Controller)filterContext.Controller).User.Identity.GetUserId();
                if (userId != null)
                {
                    var user = _context.Set<ApplicationUser>().AsNoTracking().Where(c => c.Id == userId).FirstOrDefault();

                    if (user != null)
                    {
                        var config = _context.Set<AuthentificationConfig>().First();
                        if (config.IsPeriodic && !user.NeedNewPassword)
                        {
                            var lastPasswordChangeDate = _context.PasswordStores.OrderByDescending(c => c.creationDate).First(c => c.userId == user.Id).creationDate;
                            var timeSinceLastChange = DateTime.Now.Subtract(lastPasswordChangeDate);

                            if (timeSinceLastChange.TotalMinutes > config.PeriodPeriodic)
                            {
                                user.NeedNewPassword = true;
                                _context.Entry(user).State = System.Data.Entity.EntityState.Modified;
                                _context.SaveChanges();
                            }
                        }
                    }
                }
                
            }

            base.OnResultExecuted(filterContext);
        }
    }
}
