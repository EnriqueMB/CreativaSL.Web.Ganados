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
        [HttpPost]
        public ActionResult DatatableGanadoParaVenta(string Id_venta)
        {
            try
            {
                VentaModels2 venta = new VentaModels2();
                _Venta2_Datos ventaDatos = new _Venta2_Datos();
                venta.Conexion = Conexion;

                venta.RespuestaAjax = new RespuestaAjax();
                venta.RespuestaAjax.Mensaje = ventaDatos.DatatableGanadoParaVenta(venta);
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

        #region VentaFlete
        [HttpGet]
        public ActionResult VentaFlete(string IDVenta)
        {
            try
            {
                Token.SaveToken();
                _Venta2_Datos VentaDatos = new _Venta2_Datos();
                VentaModels2 Venta = new VentaModels2();
                Venta.RespuestaAjax = new RespuestaAjax();
                Venta.Flete = new FleteModels();
                Venta.Flete.Trayecto = new TrayectoModels();
                Venta.Flete.VerificacionJaula = new VerificacionJaulaModels();

                string Id_venta = string.IsNullOrEmpty(IDVenta) ? string.Empty : IDVenta;

                if (Id_venta.Length == 0 || Id_venta.Length == 36)
                {
                    Venta.Conexion = Conexion;
                    Venta.Id_venta = Id_venta;
                    Venta = VentaDatos.GetVentaFlete(Venta);
                    if (Venta.RespuestaAjax.Success)
                    {
                        Venta.ListaClientes = VentaDatos.GetCatClientes(Venta);
                        Venta.ListaSucursales = VentaDatos.GetListadoSucursales(Venta);
                        Venta.Flete.ListaEmpresa = VentaDatos.GetListadoEmpresas(Venta);
                        Venta.Flete.ListaChofer = VentaDatos.GetChoferesXIDEmpresa(Venta);
                        Venta.Flete.ListaVehiculo = VentaDatos.GetVehiculosXIDEmpresa(Venta);

                        Venta.Flete.Trayecto.ListaLugarOrigen = VentaDatos.GetListadoLugaresEmpresa(Venta);
                        Venta.Flete.Trayecto.ListaLugarDestino = VentaDatos.GetListadoLugaresCliente(Venta);

                        return View(Venta);
                    }
                    else
                    {
                        TempData["typemessage"] = "2";
                        TempData["message"] = "No se puede cargar la vista, error: " + Venta.RespuestaAjax.Mensaje;
                        return View("Index");
                    }
                }
                else
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Verifique sus datos.";
                    return View("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista, error: " + ex.ToString();
                return View("Index");
            }
        }
        #endregion

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

        #region Combos
        #region Choferes
        [HttpPost]
        public ActionResult GetChoferesXIDEmpresa(string IDEmpresa)
        {
            try
            {
                _Venta2_Datos VentaDatos = new _Venta2_Datos();
                VentaModels2 Venta = new VentaModels2();
                Venta.Conexion = Conexion;
                Venta.Flete = new FleteModels();
                Venta.Flete.Id_empresa = IDEmpresa;
                Venta.Usuario = User.Identity.Name;
                Venta.Flete.ListaChofer = VentaDatos.GetChoferesXIDEmpresa(Venta);

                return Content(Venta.Flete.ListaChofer.ToJSON(), "application/json");
            }
            catch
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error. Por favor contacte a soporte técnico";
                return View("Index");
            }
        }
        #endregion
        #region Vehiculo
        [HttpPost]
        public ActionResult GetVehiculosXIDEmpresa(string IDEmpresa)
        {
            try
            {
                VentaModels2 Venta = new VentaModels2();
                _Venta2_Datos VentaDatos = new _Venta2_Datos();
                Venta.Conexion = Conexion;
                Venta.Flete = new FleteModels();
                Venta.Flete.Id_empresa = IDEmpresa;
                Venta.Usuario = User.Identity.Name;
                Venta.Flete.ListaVehiculo = VentaDatos.GetVehiculosXIDEmpresa(Venta);

                return Content(Venta.Flete.ListaVehiculo.ToJSON(), "application/json");
            }
            catch
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error. Por favor contacte a soporte técnico";
                return View("Index");
            }
        }
        #endregion
        #region Lugares de la empresa
        [HttpPost]
        public ActionResult GetListadoLugaresEmpresa(string IDEmpresa)
        {
            try
            {
                VentaModels2 Venta = new VentaModels2();
                _Venta2_Datos VentaDatos = new _Venta2_Datos();
                Venta.Conexion = Conexion;
                Venta.Flete = new FleteModels();
                Venta.Flete.Trayecto = new TrayectoModels();
                Venta.Flete.Id_empresa = IDEmpresa;
                Venta.Usuario = User.Identity.Name;
                Venta.Flete.Trayecto.ListaLugarOrigen = VentaDatos.GetListadoLugaresEmpresa(Venta);

                return Content(Venta.Flete.Trayecto.ListaLugarOrigen.ToJSON(), "application/json");
            }
            catch
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error. Por favor contacte a soporte técnico";
                return View("Index");
            }
        }
        #endregion
        #region Lugares del cliente
        [HttpPost]
        public ActionResult GetListadoLugaresCliente(string Id_cliente)
        {
            try
            {
                VentaModels2 Venta = new VentaModels2();
                _Venta2_Datos VentaDatos = new _Venta2_Datos();
                Venta.Conexion = Conexion;
                Venta.Flete = new FleteModels();
                Venta.Flete.Trayecto = new TrayectoModels();
                Venta.Id_cliente = Id_cliente;
                Venta.Usuario = User.Identity.Name;
                Venta.Flete.Trayecto.ListaLugarDestino = VentaDatos.GetListadoLugaresCliente(Venta);

                return Content(Venta.Flete.Trayecto.ListaLugarDestino.ToJSON(), "application/json");
            }
            catch
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error. Por favor contacte a soporte técnico";
                return View("Index");
            }
        }
        #endregion
        #endregion
    }
}
