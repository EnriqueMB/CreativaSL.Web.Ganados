using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CreativaSL.Web.Ganados.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
           // return View();
            //return RedirectToAction("Index", "HomeAdmin", new { Area = "Admin" });
            return RedirectToRoute(new { controller = "Home", action = "Index" });

        }
    }
}