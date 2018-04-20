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
        public ActionResult ModalFleteImpuesto(string IDFleteImpuesto)
        {
            {
                FleteImpuesto = new FleteImpuestoModels();
                FleteImpuestoDatos = new _FleteImpuesto_Datos();
                FleteImpuesto.IDFleteImpuesto = IDFleteImpuesto;
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
                if (ModelState.IsValid)
                {
                    FleteImpuestoDatos = new _FleteImpuesto_Datos();
                    FleteImpuesto.Conexion = Conexion;
                    FleteImpuesto.Usuario = User.Identity.Name;


                    //Compra = CompraDatos.Compras_ac_Ganado(Compra);

                    //FleteImpuesto.RespuestaAjax.Mensaje = Compra.Mensaje;
                    //FleteImpuesto.RespuestaAjax.Success = Compra.Completado;

                    return Content(FleteImpuesto.RespuestaAjax.ToJSON(), "application/json");
                }
                else
                {
                    FleteImpuesto.RespuestaAjax.Mensaje = "Verifique su formulario.";
                    FleteImpuesto.RespuestaAjax.Success = false;
                    return Content(FleteImpuesto.RespuestaAjax.ToJSON(), "application/json");
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