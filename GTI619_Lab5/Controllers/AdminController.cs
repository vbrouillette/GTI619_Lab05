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

        public ActionResult Manage()
        {
            return View();
        }

        [HttpPost]
        public void UpdateConfig(UpdateConfigModel model)
        {
            var config = _context.AuthentificationConfigs.First();
            config.TimeOutSession = model.TimeOutSession;
            config.IsBlockAfterTwoTries = model.IsBlockAfterTwoTries;
            config.IsLowerCase = model.IsLowerCase;
            config.IsNumber = model.IsNumber;
            config.IsPeriodic = model.IsPeriodic;
            config.IsSpecialCase = model.IsSpecialCase;
            config.IsUpperCase = model.IsUpperCase;
            config.MaxLenght = model.MaxLenght;
            config.MinLenght = model.MinLenght;
            config.NbrTry = model.NbrTry;
            config.PeriodPeriodic = model.PeriodPeriodic;
            config.TryDownPeriod = model.TryDownPeriod;

            _context.Entry(config).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }
    }
}