using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    public class CajaChicaController : Controller
    {
        // GET: Admin/CajaChica
        public ActionResult Index()
        {
            return View();
        }

        // GET: Admin/CajaChica/Create
        public ActionResult Create()
        {
            //Obtener datos de la caja
            return View();
        }


        // GET: Admin/CajaChica/ArqueoCaja
        public ActionResult ArqueoCaja(string IdCajaChica)
        {
            //Obtener datos de la caja
            return View();
        }
        

    }
}
