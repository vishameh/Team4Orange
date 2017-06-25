using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventManager.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Events()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult LogIn()
        {
            return View();
        }
    }
}