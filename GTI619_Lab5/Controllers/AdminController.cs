using GTI619_Lab5.DAL;
using GTI619_Lab5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GTI619_Lab5.Controllers
{
    public class AdminController : Controller
    {

        private ApplicationContext _context;

        public AdminController()
        {
            _context = new ApplicationContext();
        }

        //
        // GET: /Admin/
        public void UpdateConfig(UpdateConfigModel model)
        {
            var config = _context.AuthentificationConfigs.First();
            config.TimeOutSession = model.TimeOutSession;

            _context.SaveChanges();
        }
	}
}