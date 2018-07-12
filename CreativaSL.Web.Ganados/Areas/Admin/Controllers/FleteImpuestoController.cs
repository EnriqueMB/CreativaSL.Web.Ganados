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
    public class FleteImpuestoController : Controller
    {
        private string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
        private TokenProcessor Token = TokenProcessor.GetInstance();
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

      
     
    }
}