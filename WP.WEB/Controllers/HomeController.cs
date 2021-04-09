﻿
using System.Web.Mvc;


namespace WP.WEB.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index(int page = 1)
        {

            return View();
        }

        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [Authorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}