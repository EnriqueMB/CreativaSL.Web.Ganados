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

        [HttpPost]
        public ActionResult Create(string id_sucursal, string id_cliente, List<VentaGeneralDetalleModels> listaProducto)
        {
            if( string.IsNullOrEmpty(id_sucursal) || string.IsNullOrEmpty(id_cliente) || listaProducto == null || id_sucursal.Length != 36 || id_cliente.Length != 36 || listaProducto.Count < 0)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Verifique sus datos.";
                return RedirectToAction("Index", "VentaGeneral");
            }

            if (Token.IsTokenValid())
            {
                _VentaGeneral_Datos oDatosVentaGeneral = new _VentaGeneral_Datos();
                VentaGeneralModels oVentaGeneral = new VentaGeneralModels();
                oVentaGeneral.Id_cliente = id_cliente;
                oVentaGeneral.Id_sucursal = id_sucursal;
                oVentaGeneral.ListaDetalles = listaProducto;

                string usuario = User.Identity.Name;
                RespuestaAjax oRespuesta = oDatosVentaGeneral.VentaGeneral_spCIDDB_ac(oVentaGeneral, conexion, usuario, 1);

                if (oRespuesta.Success)
                {
                    TempData["typemessage"] = "1";
                    TempData["message"] = "Los datos se guardaron correctamente.";
                    Token.ResetToken();
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Ocurrio un error al intentar guardar los datos. Intente más tarde.";

                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Verifique sus datos.";
                RespuestaAjax oRespuesta = new RespuestaAjax();
                oRespuesta.Success = false;

                return RedirectToAction("Index");
            }
        }

        #region Helper
        public void CargarListasDefault()
        {
            _Combos_Datos oDatosCombo = new _Combos_Datos();
            ViewBag.ListaSucursal = oDatosCombo.ObtenerComboSucursales(conexion);
            ViewBag.ListaClientes = oDatosCombo.ObtenerComboClientes(conexion);
            ViewBag.ListaTipoProductos = oDatosCombo.VentaGeneral_spCIDDB_get_catTipoProducto(conexion);
        }

        public ActionResult ComboVehiculos()
        {
            try
            {
                _Combos_Datos oDatosCombo = new _Combos_Datos();
                List<CatVehiculoModels> ListaVehiculos = oDatosCombo.Dbo_spCSLDB_Combo_get_CatVehiculosAll(conexion);

                return Content(ListaVehiculos.ToJSON(), "application/json");
            }
            catch
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error. Por favor contacte a soporte técnico";
                return Json("");
            }
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