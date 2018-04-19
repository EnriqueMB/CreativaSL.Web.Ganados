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
        private _FleteImpuesto FleteImpuestoDatos;

        [HttpPost]
        public ActionResult ModalFleteImpuesto(string IDFleteImpuesto)
        {
            {
                FleteImpuesto = new FleteImpuestoModels();
                FleteImpuestoDatos = new _FleteImpuesto();
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
            FleteImpuestoDatos = new _FleteImpuesto();
            FleteImpuesto.Conexion = Conexion;
            FleteImpuesto.IDFlete = IDFlete;
            FleteImpuesto.RespuestaAjax.Mensaje = Auxiliar.SqlReaderToJson(FleteImpuestoDatos.GetJsonTableFleteImpuestoXIDFlete(FleteImpuesto));

            return Content(FleteImpuesto.RespuestaAjax.Mensaje, "application/json");
        }
    }
}