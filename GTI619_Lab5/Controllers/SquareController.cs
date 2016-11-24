﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GTI619_Lab5.Controllers
{
    public class SquareController : Controller
    {
        //
        // GET: /Square/
        [Authorize(Roles = "Carrée, Administrateur")]
        public ActionResult Index()
        {
            return View();
        }
	}
}