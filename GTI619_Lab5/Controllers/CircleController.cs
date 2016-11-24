using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GTI619_Lab5.Controllers
{
    public class CircleController : Controller
    {
        //
        // GET: /Circle/
        [Authorize(Roles = "Cercle, Administrateur")]
        public ActionResult Index()
        {
            return View();
        }
	}
}