using CreativaSL.Web.Ganados.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    public class FleteImpuestoController : Controller
    {
        private string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
        private FleteImpuestoModels FleteImpuesto;
        private _FleteImpuesto_Datos FleteImpuestoDatos;

        [HttpPost]
        public ActionResult ModalFleteImpuesto(string IDFlete, string IDFleteImpuesto)
        {
            {
                FleteImpuesto = new FleteImpuestoModels();
                FleteImpuestoDatos = new _FleteImpuesto_Datos();
                FleteImpuesto.IDFleteImpuesto = IDFleteImpuesto;
                FleteImpuesto.IDFlete = IDFlete;
                FleteImpuesto.Conexion = Conexion;
                FleteImpuesto = FleteImpuestoDatos.GetFleteImpuestoXIDFleteImpuesto(FleteImpuesto);
                FleteImpuesto.ListaImpuesto = FleteImpuestoDatos.GetListadoImpuesto(FleteImpuesto);
                FleteImpuesto.ListaTipoImpuesto = FleteImpuestoDatos.GetListadoTipoImpuesto(FleteImpuesto);
                FleteImpuesto.ListaTipoFactor = FleteImpuestoDatos.GetListadoTipoFactor(FleteImpuesto);

                return PartialView("ModalFleteImpuesto", FleteImpuesto);
            }
        }

        [HttpPost]
        public ContentResult TableJsonFleteImpuesto(string IDFlete)
        {
            FleteImpuesto = new FleteImpuestoModels();
            FleteImpuestoDatos = new _FleteImpuesto_Datos();
            FleteImpuesto.Conexion = Conexion;
            FleteImpuesto.IDFlete = IDFlete;
            FleteImpuesto.RespuestaAjax.Mensaje = FleteImpuestoDatos.GetJsonTableFleteImpuestoXIDFlete(FleteImpuesto);

            return Content(FleteImpuesto.RespuestaAjax.Mensaje, "application/json");
        }

        [HttpPost]
        public ActionResult AC_FleteImpuesto(FleteImpuestoModels FleteImpuesto)
        {
            try
            {
                if (FleteImpuesto.IDFlete.Length == 36)
                {
                    FleteImpuestoDatos = new _FleteImpuesto_Datos();
                    FleteImpuesto.Conexion = Conexion;
                    FleteImpuesto.Usuario = User.Identity.Name;
                    FleteImpuesto = FleteImpuestoDatos.FleteImpuesto_ac_FleteImpuesto(FleteImpuesto);

                    return Content(FleteImpuesto.RespuestaAjax.ToJSON(), "application/json");
                }
                else
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Flete no válido.";
                    return RedirectToAction("Index", "Flete");
                }
            }
            catch (Exception ex)
            {
                FleteImpuesto.RespuestaAjax.Mensaje = ex.ToString();
                FleteImpuesto.RespuestaAjax.Success = false;
                return Content(FleteImpuesto.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        [HttpPost]
        public ActionResult DEL_FleteImpuesto(FleteImpuestoModels FleteImpuesto)
        {
            try
            {
                if (FleteImpuesto.IDFleteImpuesto.Length == 36)
                {
                    FleteImpuestoDatos = new _FleteImpuesto_Datos();
                    FleteImpuesto.Conexion = Conexion;
                    FleteImpuesto.Usuario = User.Identity.Name;
                    FleteImpuesto = FleteImpuestoDatos.FleteImpuesto_del_FleteImpuesto(FleteImpuesto);

                    return Content(FleteImpuesto.RespuestaAjax.ToJSON(), "application/json");
                }
                else
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Flete no válido.";
                    return RedirectToAction("Index", "Flete");
                }
            }
            catch (Exception ex)
            {
                FleteImpuesto.RespuestaAjax.Mensaje = ex.ToString();
                FleteImpuesto.RespuestaAjax.Success = false;
                return Content(FleteImpuesto.RespuestaAjax.ToJSON(), "application/json");
            }
        }
    }
}