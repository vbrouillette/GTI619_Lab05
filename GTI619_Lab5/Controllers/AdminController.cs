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

        [Authorize(Roles = "Administrateur")]
        public ActionResult Manage()
        {
            var config = _context.AuthentificationConfigs.First();
            var loginConfig = _context.LoginConfigs.First();

            var updateConfigModel = new UpdateConfigModel()
            {
                TimeOutSession = config.TimeOutSession,
                IsLowerCase = config.IsLowerCase,
                IsNumber = config.IsNumber,
                IsPeriodic = config.IsPeriodic,
                IsSpecialCase = config.IsSpecialCase,
                IsUpperCase = config.IsUpperCase,
                MaxLenght = config.MaxLenght,
                MinLenght = config.MinLenght,
                PeriodPeriodic = config.PeriodPeriodic,
                NbrLastPasswords = config.NbrLastPasswords,
                DelayBetweenBlocks = loginConfig.DelayBetweenBlocks,
                DelayBetweenFailedAuthentication = loginConfig.DelayBetweenFailedAuthentication,
                MaxBlocksBeforeAdmin = loginConfig.MaxBlocksBeforeAdmin,
                NbAttemptsBeforeBlocking=loginConfig.NbAttemptsBeforeBlocking
            };
            return View(updateConfigModel);

        }

        [HttpPost]
        [Authorize(Roles = "Administrateur")]
        public ActionResult UpdateConfig(UpdateConfigModel model)
        {
            var config = _context.AuthentificationConfigs.First();
            config.TimeOutSession = model.TimeOutSession;
            config.IsLowerCase = model.IsLowerCase;
            config.IsNumber = model.IsNumber;
            config.IsPeriodic = model.IsPeriodic;
            config.IsSpecialCase = model.IsSpecialCase;
            config.IsUpperCase = model.IsUpperCase;
            config.MaxLenght = model.MaxLenght;
            config.MinLenght = model.MinLenght;
            config.PeriodPeriodic = model.PeriodPeriodic;
            config.NbrLastPasswords = model.NbrLastPasswords;

            var loginConfig = _context.LoginConfigs.First();
            loginConfig.DelayBetweenBlocks = model.DelayBetweenBlocks;
            loginConfig.DelayBetweenFailedAuthentication = model.DelayBetweenFailedAuthentication;
            loginConfig.MaxBlocksBeforeAdmin = model.MaxBlocksBeforeAdmin;
            loginConfig.NbAttemptsBeforeBlocking = model.NbAttemptsBeforeBlocking;

            _context.Entry(config).State = System.Data.Entity.EntityState.Modified;
            _context.Entry(loginConfig).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();         

            return RedirectToAction("Manage");
        }
    }
}