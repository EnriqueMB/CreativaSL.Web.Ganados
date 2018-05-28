using CreativaSL.Web.Ganados.Filters;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CreativaSL.Web.Ganados.Models;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    [Autorizado]
    public class RptUltCompraProveedorController : Controller
    {
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
        // GET: Admin/RptUltCompraProveedor
        public ActionResult Index()
        {
            try
            {
                RptUltCompraProveedorModels Reporte = new RptUltCompraProveedorModels();
                return View(Reporte);
            }
            catch (Exception)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View();
            }
        }

        [HttpPost]
        public ActionResult ProximaCompra(string start, string end)
        {
            try
            {
                RptUltCompraProveedorModels Reporte = new RptUltCompraProveedorModels();
                DatosRptUltCompraProveedor_Datos RPTDatos = new DatosRptUltCompraProveedor_Datos();
                Reporte.fechaStart = Convert.ToDateTime(start);
                Reporte.fechaEnd = Convert.ToDateTime(end);
                Reporte.Conexion = Conexion;
                Reporte.ListaSiguienteCompras = RPTDatos.GetListaProximaCompras(Reporte);


                return Json(Reporte.ListaSiguienteCompras, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }
    }
}