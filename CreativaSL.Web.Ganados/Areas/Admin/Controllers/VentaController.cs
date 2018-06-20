using CreativaSL.Web.Ganados.App_Start;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using CreativaSL.Web.Ganados.Models;
using System.Configuration;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    public class VentaController : Controller
    {
        private TokenProcessor Token = TokenProcessor.GetInstance();
        private string Conexion = ConfigurationManager.AppSettings.Get("strConnection");


        [HttpPost]
        public ActionResult DatatableGanadoActual()
        {
            try
            {
                VentaModels2 venta = new VentaModels2();
                _Venta2_Datos ventaDatos = new _Venta2_Datos();
                venta.Conexion = Conexion;
                

                venta.RespuestaAjax = new RespuestaAjax();
                venta.RespuestaAjax.Mensaje = ventaDatos.DatatableGanadoActual(venta);
                venta.RespuestaAjax.Success = true;

                return Content(venta.RespuestaAjax.Mensaje, "application/json");

            }
            catch (Exception ex)
            {
                VentaModels2 venta = new VentaModels2();
                venta.RespuestaAjax.Mensaje = ex.ToString();
                venta.RespuestaAjax.Success = false;
                return Content(venta.RespuestaAjax.ToJSON(), "application/json");
            }
        }


        // GET: Admin/Venta
        public ActionResult Index()
        {
            return View();
        }

        #region Vista Ganado
        [HttpGet]
        public ActionResult VentaGanado()
        {
            try
            {
                Token.SaveToken();
                return View();
            }
            catch (Exception ex)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista, error: " + ex.ToString();
                throw ex;
            }
        }
        #endregion
   

        #region Modal evento
        [HttpPost]
        public ActionResult ModalEvento()
        {
            return PartialView("ModalEvento");
        }
        #endregion
    }
}
