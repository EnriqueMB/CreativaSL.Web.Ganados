using CreativaSL.Web.Ganados.App_Start;
using CreativaSL.Web.Ganados.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    public class CatProveedorTransporteController : Controller
    {
        private TokenProcessor Token = TokenProcessor.GetInstance();
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");

        [HttpGet]
        public ActionResult Create()
        {
            try
            {
                Token.SaveToken();
                CatProveedorTransporteModels proveedor = new CatProveedorTransporteModels();
                return View(proveedor);
            }
            catch (Exception)
            {
                CatProveedorTransporteModels proveedor = new CatProveedorTransporteModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(proveedor);
            }
        }
    }
}