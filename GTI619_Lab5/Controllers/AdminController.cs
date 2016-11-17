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

        [HttpPost]
        public void UpdateConfig(UpdateConfigModel model)
        {
            var config = _context.AuthentificationConfigs.First();
            config.SetTimeOutSession(model.TimeOutSession);
            config.IsBlockAfterTwoTries = model.IsBlockAfterTwoTries;
            config.IsLowerCase = model.IsLowerCase;
            config.IsNumber = model.IsNumber;
            config.IsPeriodic = model.IsPeriodic;
            config.IsSpecialCase = model.IsSpecialCase;
            config.IsUpperCase = model.IsUpperCase;
            config.SetMaxLenght(model.MaxLenght);
            config.SetMinLenght(model.MinLenght);
            config.SetNbrTry(model.NbrTry);
            config.SetPeriodPeriodic(model.PeriodPeriodic);
            config.SetTryDownPeriod(model.TryDownPeriod);          
            _context.SaveChanges();
        }
	}
}