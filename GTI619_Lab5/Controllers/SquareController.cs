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
    public class SquareController : Controller
    {
        private ApplicationDbContext _context;
        public UserManager<ApplicationUser> UserManager { get; private set; }

        public SquareController()
        {
            _context = new ApplicationDbContext();
            UserManager = new MyUserManager(new UserStore<ApplicationUser>(_context));
        }

        //
        // GET: /Square/
        [Authorize(Roles = "Carrée, Administrateur")]
        public ActionResult Index()
        {
            if (UserManager.FindById(User.Identity.GetUserId()).NeedNewPassword)
            {
                return RedirectToAction("Manage", "Account", new { Message = ManageMessageId.HaveToChange });
            }

            return View();
        }
	}
}