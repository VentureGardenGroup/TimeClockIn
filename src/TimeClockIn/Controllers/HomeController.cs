using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeClockIn.Models;
using TimeClockIn.Repository;

namespace TimeClockIn.Controllers
{
    public class HomeController : Controller
    {
        LocationRepository LR = new LocationRepository();

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Test()
        {

            List<Location> loc = LR.Get();
            return View(loc);
        }

        public ActionResult WebLocation()
        {
            
            return View();
        }
    }
}