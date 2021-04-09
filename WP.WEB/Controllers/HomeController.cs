
using System.Collections.Generic;
using System.Web.Mvc;
using WP.BusinessLayer.Interfaces;
using WP.WEB.Models.ViewModels;

namespace WP.WEB.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index(int page = 1)
        {

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