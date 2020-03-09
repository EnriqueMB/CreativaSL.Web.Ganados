using CreativaSL.Web.Ganados.App_Start;
using CreativaSL.Web.Ganados.Models;
using CreativaSL.Web.Ganados.Models.Datatable;
using System;
using System.Configuration;
using System.Web.Mvc;
using CreativaSL.Web.Ganados.ViewModel.Venta;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    public class CostoExtraController : Controller
    {
        private TokenProcessor Token = TokenProcessor.GetInstance();
        private string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
        // GET: Admin/CostoExtra
        public ActionResult Index()
        {
            Token.SaveToken();
            return View();
        }
        [HttpPost]
        public ActionResult DatatableIndex(DataTableAjaxPostModel dataTableAjaxPostModel)
        {
            try
            {
                VentaModels2 venta = new VentaModels2();
                _CostoExtra_Datos costoDatos = new _CostoExtra_Datos();
                venta.Conexion = Conexion;

                venta.RespuestaAjax = new RespuestaAjax();
                venta.RespuestaAjax.Mensaje = costoDatos.DatatableIndex(venta, dataTableAjaxPostModel);
                venta.RespuestaAjax.Success = true;

                return Content(venta.RespuestaAjax.Mensaje, "application/json");

            }
            catch (Exception ex)
            {
                var Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                var venta = new VentaModels2();
                venta.RespuestaAjax.Mensaje = Mensaje;
                return Content(venta.RespuestaAjax.Mensaje, "application/json");
            }
        }

        [HttpGet]
        public ActionResult CostosExtra(string idVenta)
        {
            if (string.IsNullOrEmpty(idVenta))
            {
                return RedirectToAction("Index");
            }
            Token.SaveToken();
            CostosExtrasViewModel costosExtrasViewModel = new CostosExtrasViewModel();
            _Venta2_Datos venta2_Datos = new _Venta2_Datos();

            costosExtrasViewModel = venta2_Datos.GetCostosExtrasViewModel(Conexion, idVenta);

            return View(costosExtrasViewModel);
        }

        [HttpGet]
        public ActionResult CostoExtra_AC(string idVenta, string id)
        {
            if (string.IsNullOrEmpty(idVenta) || string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index");
            }

            _Venta2_Datos venta2_Datos = new _Venta2_Datos();

            CostoExtra costoExtra = venta2_Datos.GetCostoExtra(idVenta, id, Conexion);
            costoExtra.IdVenta = idVenta;

            return View(costoExtra);
        }

        [HttpPost]
        public ActionResult CostoExtra_AC(CostoExtra costoExtra)
        {
            RespuestaAjax respuestaAjax = new RespuestaAjax
            {
                Success = false,
                Mensaje = "Ocurrio un error al intentar guardar los datos. Contacte a soporte técnico."
            };

            if (ModelState.IsValid)
            {
                if (Token.IsTokenValid())
                {
                    _Venta2_Datos venta2_Datos = new _Venta2_Datos();
                    respuestaAjax = venta2_Datos.CostosExtras_AC(costoExtra, Conexion, User.Identity.Name);
                }
            }
            TempData["typemessage"] = respuestaAjax.Success ? "1" : "2";
            TempData["message"] = respuestaAjax.Mensaje;

            if (respuestaAjax.Success)
            {
                return RedirectToAction("CostosExtra", "CostoExtra", new { idVenta = costoExtra.IdVenta });
            }
            else
            {
                return View(costoExtra);
            }
        }

        [HttpPost]
        public ActionResult DeleteCostoExtra(string id, string idVenta)
        {
            RespuestaAjax respuestaAjax = new RespuestaAjax
            {
                Success = false,
                Mensaje = "Ocurrio un error al intentar guardar los datos."
            };

            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(idVenta))
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Verifique sus datos.";
                return Content(respuestaAjax.ToJSON(), "application/json");
            }

            _Venta2_Datos venta2_Datos = new _Venta2_Datos();
            respuestaAjax = venta2_Datos.CostosExtras_Del(id, idVenta, User.Identity.Name, Conexion);

            TempData["typemessage"] = respuestaAjax.Success ? "1" : "2";
            TempData["message"] = respuestaAjax.Mensaje;

            return Content(respuestaAjax.ToJSON(), "application/json");
        }
    }
}