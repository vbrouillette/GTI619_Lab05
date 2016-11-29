using GTI619_Lab5.Entities;
using GTI619_Lab5.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web.Mvc;
using static GTI619_Lab5.Controllers.AccountController;

namespace GTI619_Lab5.Controllers
{
    public class CircleController : Controller
    {
        private ApplicationDbContext _context;
        public UserManager<ApplicationUser> UserManager { get; private set; }

        public CircleController()
        {
            _context = new ApplicationDbContext();
            UserManager = new MyUserManager(new UserStore<ApplicationUser>(_context));
        }

        //
        // GET: /Circle/
        [Authorize(Roles = "Cercle, Administrateur")]
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