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
    public class VentaGeneralController : Controller
    {
        private TokenProcessor Token = TokenProcessor.GetInstance();
        private string conexion = ConfigurationManager.AppSettings.Get("strConnection");
        // GET: Admin/VentaGeneral
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            try
            {
                Token.SaveToken();
                CargarListasDefault();

                return View();
            }
            catch (Exception)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("Index");
            }
        }

        #region Helper
        public void CargarListasDefault()
        {
            _Combos_Datos oDatosCombo = new _Combos_Datos();
            ViewBag.ListaSucursal = oDatosCombo.ObtenerComboSucursales(conexion);
            ViewBag.ListaClientes = oDatosCombo.ObtenerComboClientes(conexion);
        }
        #endregion

        #region DT
        public ActionResult DTIndex()
        {
            try
            {
                _VentaGeneral_Datos oDatosVentaGeneral = new _VentaGeneral_Datos();
                string dt = oDatosVentaGeneral.VentaGeneral_spCIDDB_index(conexion);

                return Content(dt, "application/json");
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

    }
}