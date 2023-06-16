using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace framework_4._7._2_B2C_MVC_WebApp.Controllers
{
    
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var authed = User.Identity.IsAuthenticated;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}