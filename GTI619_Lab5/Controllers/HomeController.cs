using GTI619_Lab5.Entities;
using GTI619_Lab5.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static GTI619_Lab5.Controllers.AccountController;

namespace GTI619_Lab5.Controllers
{

    public class HomeController : Controller
    {
        private ApplicationDbContext _context;
        public UserManager<ApplicationUser> UserManager { get; private set; }

        public HomeController()
        {
            _context = new ApplicationDbContext();
            UserManager = new MyUserManager(new UserStore<ApplicationUser>(_context));
        }

        public ActionResult Index()
        {               
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}