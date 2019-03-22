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
using Microsoft.Reporting.WebForms;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    public class VentaController : Controller
    {
        private TokenProcessor Token = TokenProcessor.GetInstance();
        private string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
       
        #region Datatables
        [HttpPost]
        public ActionResult DatatableIndex()
        {
            try
            {
                VentaModels2 venta = new VentaModels2();
                _Venta2_Datos ventaDatos = new _Venta2_Datos();
                venta.Conexion = Conexion;

                venta.RespuestaAjax = new RespuestaAjax();
                venta.RespuestaAjax.Mensaje = ventaDatos.DatatableIndex(venta);
                venta.RespuestaAjax.Success = true;

                return Content(venta.RespuestaAjax.Mensaje, "application/json");

            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                VentaModels2 venta = new VentaModels2();
                venta.RespuestaAjax.Mensaje = Mensaje;
                venta.RespuestaAjax.Success = false;
                return Content(venta.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        [HttpPost]
        public ActionResult DatatableGanadoActual(string Id_sucursal)
        {
            try
            {
                VentaModels2 venta = new VentaModels2();
                _Venta2_Datos ventaDatos = new _Venta2_Datos();
                venta.Conexion = Conexion;
                venta.Id_sucursal = Id_sucursal;

                venta.RespuestaAjax = new RespuestaAjax();
                venta.RespuestaAjax.Mensaje = ventaDatos.DatatableGanadoActual(venta);
                venta.RespuestaAjax.Success = true;

                return Content(venta.RespuestaAjax.Mensaje, "application/json");

            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                VentaModels2 venta = new VentaModels2();
                venta.RespuestaAjax.Mensaje = Mensaje;
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
                venta.Id_venta = Id_venta;
                venta.RespuestaAjax.Mensaje = ventaDatos.DatatableGanadoParaVenta(venta);
                venta.RespuestaAjax.Success = true;

                return Content(venta.RespuestaAjax.Mensaje, "application/json");
            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                VentaModels2 venta = new VentaModels2();
                venta.RespuestaAjax = new RespuestaAjax();
                venta.RespuestaAjax.Mensaje = Mensaje;
                venta.RespuestaAjax.Success = false;
                return Content(venta.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        [HttpPost]
        public ActionResult DatatableGanadoVendidoVivo(string Id_venta, int Id_eventoVenta)
        {
            try
            {
                VentaModels2 venta = new VentaModels2();
                _Venta2_Datos ventaDatos = new _Venta2_Datos();
                venta.Conexion = Conexion;

                venta.RespuestaAjax = new RespuestaAjax();
                venta.EventoVenta = new EventoVentaModels();
                venta.EventoVenta.Id_venta = Id_venta;
                venta.EventoVenta.Id_eventoVenta = Id_eventoVenta;
                venta.RespuestaAjax.Mensaje = ventaDatos.DatatableGanadoVendidoVivo(venta);
                venta.RespuestaAjax.Success = true;

                return Content(venta.RespuestaAjax.Mensaje, "application/json");
            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                VentaModels2 venta = new VentaModels2();
                venta.RespuestaAjax = new RespuestaAjax();
                venta.RespuestaAjax.Mensaje = Mensaje;
                venta.RespuestaAjax.Success = false;
                return Content(venta.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        [HttpPost]
        public ActionResult DatatableGanadoConEvento(string Id_venta, int Id_eventoVenta)
        {
            try
            {
                VentaModels2 venta = new VentaModels2();
                _Venta2_Datos ventaDatos = new _Venta2_Datos();
                venta.Conexion = Conexion;

                venta.RespuestaAjax = new RespuestaAjax();
                venta.EventoVenta = new EventoVentaModels();
                venta.EventoVenta.Id_venta = Id_venta;
                venta.EventoVenta.Id_eventoVenta = Id_eventoVenta;
                venta.RespuestaAjax.Mensaje = ventaDatos.DatatableGanadoConEvento(venta);
                venta.RespuestaAjax.Success = true;

                return Content(venta.RespuestaAjax.Mensaje, "application/json");
            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                VentaModels2 venta = new VentaModels2();
                venta.RespuestaAjax = new RespuestaAjax();
                venta.RespuestaAjax.Mensaje = Mensaje;
                venta.RespuestaAjax.Success = false;
                return Content(venta.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        [HttpPost]
        public ActionResult DatatableEventos(string Id_venta)
        {
            try
            {
                VentaModels2 venta = new VentaModels2();
                _Venta2_Datos ventaDatos = new _Venta2_Datos();
                venta.Conexion = Conexion;

                venta.RespuestaAjax = new RespuestaAjax();
                venta.EventoVenta = new EventoVentaModels();
                venta.EventoVenta.Id_venta = Id_venta;
                venta.RespuestaAjax.Mensaje = ventaDatos.DatatableEventos(venta);
                venta.RespuestaAjax.Success = true;

                return Content(venta.RespuestaAjax.Mensaje, "application/json");
            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                VentaModels2 venta = new VentaModels2();
                venta.RespuestaAjax = new RespuestaAjax();
                venta.RespuestaAjax.Mensaje = Mensaje;
                venta.RespuestaAjax.Success = false;
                return Content(venta.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        [HttpPost]
        public ContentResult DatatableDetallesDocumentoPorCobrarVenta(string Id_venta, string Id_documentoCobrar)
        {
            try
            {
                _Venta2_Datos VentaDatos = new _Venta2_Datos();
                VentaModels2 Venta = new VentaModels2();
                Venta.RespuestaAjax = new RespuestaAjax();
                Venta.Conexion = Conexion;
                Venta.Id_venta = Id_venta;
                Venta.Id_documentoXCobrar = Id_documentoCobrar;
                Venta.RespuestaAjax.Mensaje = VentaDatos.DatatableDetallesDocumentoPorCobrarVenta(Venta);

                return Content(Venta.RespuestaAjax.Mensaje, "application/json");

            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                VentaModels2 Venta = new VentaModels2();
                Venta.RespuestaAjax = new RespuestaAjax();
                Venta.RespuestaAjax.Mensaje = Mensaje;
                return Content(Venta.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        [HttpPost]
        public ContentResult DatatableDetallesDocumentoPorCobrarVentaDeducciones(string Id_venta, string Id_documentoCobrar)
        {
            try
            {
                _Venta2_Datos VentaDatos = new _Venta2_Datos();
                VentaModels2 Venta = new VentaModels2();
                Venta.RespuestaAjax = new RespuestaAjax();
                Venta.Conexion = Conexion;
                Venta.Id_venta = Id_venta;
                Venta.Id_documentoXCobrar = Id_documentoCobrar;
                Venta.RespuestaAjax.Mensaje = VentaDatos.DatatableDetallesDocumentoPorCobrarVentaDeducciones(Venta);

                return Content(Venta.RespuestaAjax.Mensaje, "application/json");

            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                VentaModels2 Venta = new VentaModels2();
                Venta.RespuestaAjax = new RespuestaAjax();
                Venta.RespuestaAjax.Mensaje = Mensaje;
                return Content(Venta.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        [HttpPost]
        public ContentResult DatatableDetallesDocumentoPorCobrarFleteCobros(string Id_venta, string Id_documentoCobrar)
        {
            try
            {
                _Venta2_Datos VentaDatos = new _Venta2_Datos();
                VentaModels2 Venta = new VentaModels2();
                Venta.RespuestaAjax = new RespuestaAjax();
                Venta.Conexion = Conexion;
                Venta.Id_venta = Id_venta;
                Venta.Id_documentoXCobrar = Id_documentoCobrar;
                Venta.RespuestaAjax.Mensaje = VentaDatos.DatatableDetallesDocumentoPorCobrarVentaCobro(Venta);

                return Content(Venta.RespuestaAjax.Mensaje, "application/json");

            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                VentaModels2 Venta = new VentaModels2();
                Venta.RespuestaAjax = new RespuestaAjax();
                Venta.RespuestaAjax.Mensaje = Mensaje;
                return Content(Venta.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        [HttpPost]
        public ContentResult DatatableImpuestoXIdDocDetalle(string Id1, string Id2)
        {
            ImpuestoModels Impuesto = new ImpuestoModels();
            _Venta2_Datos VentaDatos = new _Venta2_Datos();
            Impuesto.Conexion = Conexion;
            Impuesto.IDModulo = Id1;
            Impuesto.Id_detalleDoctoCobrar = Id2;
            Impuesto.RespuestaAjax.Mensaje = VentaDatos.DatatableImpuestoXIDDocumentoDetalle(Impuesto);

            return Content(Impuesto.RespuestaAjax.Mensaje, "application/json");
        }
        [HttpPost]
        public ActionResult DatatableDocumentos(string Id_venta)
        {
            try
            {
                VentaModels2 Venta = new VentaModels2();
                _Venta2_Datos VentaDatos = new _Venta2_Datos();
                Venta.Conexion = Conexion;
                Venta.Id_venta = Id_venta;
                Venta.RespuestaAjax = new RespuestaAjax();
                Venta.RespuestaAjax.Mensaje = VentaDatos.DatatableDocumentos(Venta);
                Venta.RespuestaAjax.Success = true;

                return Content(Venta.RespuestaAjax.Mensaje, "application/json");

            }
            catch (Exception ex)
            {
                VentaModels2 Venta = new VentaModels2();
                Venta.RespuestaAjax = new RespuestaAjax();
                Venta.RespuestaAjax.Mensaje = ex.Message;
                Venta.RespuestaAjax.Success = false;
                return Content(Venta.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        [HttpPost]
        public ActionResult DatatableGeneralesGanado(string Id_venta)
        {
            try
            {
                _Venta2_Datos VentaDatos= new _Venta2_Datos();
                VentaModels2 Venta = new VentaModels2();
                Venta.Conexion = Conexion;
                Venta.Id_venta = Id_venta;
                Venta.RespuestaAjax = new RespuestaAjax();
                Venta.RespuestaAjax.Mensaje = VentaDatos.DatatableGeneralesGanado(Venta);
                Venta.RespuestaAjax.Success = true;

                return Content(Venta.RespuestaAjax.Mensaje, "application/json");

            }
            catch (Exception ex)
            {
                VentaModels2 Venta = new VentaModels2();
                Venta.RespuestaAjax = new RespuestaAjax();
                Venta.RespuestaAjax.Mensaje = ex.Message;
                Venta.RespuestaAjax.Success = false;
                return Content(Venta.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        [HttpPost]
        public ActionResult DatatableGanadoMacho(string Id_venta)
        {
            try
            {
                _Venta2_Datos VentaDatos = new _Venta2_Datos();
                VentaModels2 Venta = new VentaModels2();
                Venta.Conexion = Conexion;
                Venta.Id_venta = Id_venta;
                Venta.RespuestaAjax = new RespuestaAjax();
                Venta.RespuestaAjax.Mensaje = VentaDatos.DatatableGanadoMacho(Venta);
                Venta.RespuestaAjax.Success = true;

                return Content(Venta.RespuestaAjax.Mensaje, "application/json");

            }
            catch (Exception ex)
            {
                VentaModels2 Venta = new VentaModels2();
                Venta.RespuestaAjax = new RespuestaAjax();
                Venta.RespuestaAjax.Mensaje = ex.Message;
                Venta.RespuestaAjax.Success = false;
                return Content(Venta.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        [HttpPost]
        public ActionResult DatatableGanadoHembra(string Id_venta)
        {
            try
            {
                _Venta2_Datos VentaDatos = new _Venta2_Datos();
                VentaModels2 Venta = new VentaModels2();
                Venta.Conexion = Conexion;
                Venta.Id_venta = Id_venta;
                Venta.RespuestaAjax = new RespuestaAjax();
                Venta.RespuestaAjax.Mensaje = VentaDatos.DatatableGanadoHembra(Venta);
                Venta.RespuestaAjax.Success = true;

                return Content(Venta.RespuestaAjax.Mensaje, "application/json");

            }
            catch (Exception ex)
            {
                VentaModels2 Venta = new VentaModels2();
                Venta.RespuestaAjax = new RespuestaAjax();
                Venta.RespuestaAjax.Mensaje = ex.Message;
                Venta.RespuestaAjax.Success = false;
                return Content(Venta.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        [HttpPost]
        public ActionResult DatatableDetallesDocXcobrar(string Id_venta)
        {
            try
            {
                _Venta2_Datos VentaDatos = new _Venta2_Datos();
                VentaModels2 Venta = new VentaModels2();
                Venta.Conexion = Conexion;
                Venta.Id_venta = Id_venta;
                Venta.RespuestaAjax = new RespuestaAjax();
                Venta.RespuestaAjax.Mensaje = VentaDatos.DatatableDetallesDocXcobrar(Venta);
                Venta.RespuestaAjax.Success = true;

                return Content(Venta.RespuestaAjax.Mensaje, "application/json");

            }
            catch (Exception ex)
            {
                VentaModels2 Venta = new VentaModels2();
                Venta.RespuestaAjax = new RespuestaAjax();
                Venta.RespuestaAjax.Mensaje = ex.Message;
                Venta.RespuestaAjax.Success = false;
                return Content(Venta.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        [HttpPost]
        public ActionResult DatatableDetalleDocXCobraPagos(string Id_venta)
        {
            try
            {
                _Venta2_Datos VentaDatos = new _Venta2_Datos();
                VentaModels2 Venta = new VentaModels2();
                Venta.Conexion = Conexion;
                Venta.Id_venta = Id_venta;
                Venta.RespuestaAjax = new RespuestaAjax();
                Venta.RespuestaAjax.Mensaje = VentaDatos.DatatableDetalleDocXCobrarPagos(Venta);
                Venta.RespuestaAjax.Success = true;

                return Content(Venta.RespuestaAjax.Mensaje, "application/json");

            }
            catch (Exception ex)
            {
                VentaModels2 Venta = new VentaModels2();
                Venta.RespuestaAjax = new RespuestaAjax();
                Venta.RespuestaAjax.Mensaje = ex.Message;
                Venta.RespuestaAjax.Success = false;
                return Content(Venta.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        
        #endregion

        #region Otros
        #region Cambiar estatus
        [HttpGet]
        public ActionResult CambiarEstatus(string IDVenta)
        {
            VentaModels2 Venta = new VentaModels2();
            _Venta2_Datos VentaDatos = new _Venta2_Datos();
            Venta.Conexion = Conexion;
            Venta.Id_venta = IDVenta;
            Venta.Usuario = User.Identity.Name;
            Venta.RespuestaAjax = new RespuestaAjax();
            Venta.RespuestaAjax = VentaDatos.CambiarEstatusCompra(Venta);

            if (Venta.RespuestaAjax.Success)
            {
                TempData["typemessage"] = "1";
                TempData["message"] = "Estatus cambiado con éxito";
            }
            else
            {
                TempData["typemessage"] = "2";
                if (string.IsNullOrEmpty(Venta.RespuestaAjax.Mensaje))
                {
                    TempData["message"] = "No se pudo cambiar el estatus de la venta.";
                }
                else
                {
                    TempData["message"] = Venta.RespuestaAjax.Mensaje;
                }
            }

            return RedirectToAction("Index");
        }
        #endregion

        #endregion

        // GET: Admin/Venta
        public ActionResult Index()
        {
            Token.SaveToken();
            return View();
        }

        #region Vista Flete
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
                        Venta.Flete.ListaFormaPago = VentaDatos.GetListadoFormaPagos(Venta);
                        Venta.Flete.ListaMetodoPago = VentaDatos.GetListadoMetodoPago(Venta);
                        Venta.Flete.Trayecto.ListaLugarOrigen = VentaDatos.GetListadoLugaresEmpresa(Venta);
                        Venta.Flete.Trayecto.ListaLugarDestino = VentaDatos.GetListadoLugaresCliente(Venta);
                        Venta.Flete.ListaChoferAuxiliar = VentaDatos.GetChoferesAuxiliares(Venta);
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
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista, error: " + Mensaje;
                return View("Index");
            }
        }
        [HttpPost]
        public ActionResult VentaFlete(VentaModels2 Venta)
        {
            try
            {
                if (Token.IsTokenValid())
                {
                    _Venta2_Datos VentaDatos = new _Venta2_Datos();
                    Venta.Conexion = Conexion;
                    Venta.Opcion = 1;
                    Venta.Usuario = User.Identity.Name;
                    Venta.RespuestaAjax = VentaDatos.AC_Flete(Venta);

                    if (Venta.RespuestaAjax.Success)
                    {
                        TempData["typemessage"] = "1";
                        TempData["message"] = "Los datos se guardarón correctamente.";
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

                    Venta.RespuestaAjax = new RespuestaAjax();
                    Venta.RespuestaAjax.Success = false;

                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error al intentar guardar los datos. Contacte a soporte técnico. Error: " + Mensaje;
                Venta.RespuestaAjax = new RespuestaAjax();
                Venta.RespuestaAjax.Success = false;

                return RedirectToAction("Index");
            }
        }
        #endregion

        #region Vista Ganado
        [HttpGet]
        public ActionResult VentaGanado(string IDVenta)
        {
            try
            {
                Token.SaveToken();
                _Venta2_Datos VentaDatos = new _Venta2_Datos();
                VentaModels2 Venta = new VentaModels2();
                Venta.RespuestaAjax = new RespuestaAjax();

                string Id_venta = string.IsNullOrEmpty(IDVenta) ? string.Empty : IDVenta;

                if (Id_venta.Length == 0 || Id_venta.Length == 36)
                {
                    Venta.Conexion = Conexion;
                    Venta.Id_venta = IDVenta;
                    Venta = VentaDatos.GetVentaGanado(Venta);
                    if (Venta.RespuestaAjax.Success)
                    {
                        ViewBag.TipoVenta = Venta.TipoVenta == 1 ? "Directa" : Venta.TipoVenta == 2 ? "Por rango de peso" : "Error";
                        ViewBag.ListaDePrecios = VentaDatos.GetListadoPrecioRangoPeso(Venta.Id_venta, Conexion);
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
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista, error: " + Mensaje;
                return View("Index");
            }
        }
        [HttpPost]
        //public ActionResult VentaGanado(string ListaIDGanadosParaVender, string IDVenta, decimal ME, decimal montoTotal)
        public ActionResult VentaGanado(List<GanadoParaVender> ListaGanadosParaVender, string IDVenta, decimal montoTotal)
        {
            try
            {
                if (Token.IsTokenValid())
                {
                    _Venta2_Datos VentaDatos = new _Venta2_Datos();
                    VentaModels2 Venta = new VentaModels2();
                    Venta.Conexion = Conexion;
                    Venta.Usuario = User.Identity.Name;
                    //Venta.ListaIDGanadosParaVender = ListaIDGanadosParaVender;
                    Venta.Id_venta = IDVenta;
                    //Venta.ME = ME;
                    Venta.MontoTotalGanado = montoTotal;
                    Venta.RespuestaAjax = new RespuestaAjax();
                    VentaDatos.AC_Ganado(Venta, ListaGanadosParaVender);

                    if (Venta.RespuestaAjax.Success)
                    {
                        TempData["typemessage"] = "1";
                        TempData["message"] = "Los datos se guardarón correctamente.";
                        Token.ResetToken();

                        return Content(Venta.RespuestaAjax.ToJSON(), "application/json");
                    }
                    else
                    {
                        TempData["typemessage"] = "2";
                        TempData["message"] = "Ocurrio un error al intentar guardar los datos. Intente más tarde.";

                        return Content(Venta.RespuestaAjax.ToJSON(), "application/json");
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Verifique sus datos.";
                    VentaModels2 Venta = new VentaModels2();
                    Venta.RespuestaAjax = new RespuestaAjax();
                    Venta.RespuestaAjax.Success = false;

                    return Content(Venta.RespuestaAjax.ToJSON(), "application/json");
                }
            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error al intentar guardar los datos. Contacte a soporte técnico. Error: " + Mensaje;
                VentaModels2 Venta = new VentaModels2();
                Venta.RespuestaAjax = new RespuestaAjax();
                Venta.RespuestaAjax.Success = false;

                return RedirectToAction("Index");
            }

        }
        #endregion

        #region Vista Evento Recepcion
        [HttpGet]
        public ActionResult VentaEventoRecepcion(string IDVenta)
        {
            try
            {
                Token.SaveToken();
                _Venta2_Datos VentaDatos = new _Venta2_Datos();
                VentaModels2 Venta = new VentaModels2();
                Venta.RespuestaAjax = new RespuestaAjax();
                Venta.RecepcionOrigen = new RecepcionOrigenVentaModels();

                string Id_venta = string.IsNullOrEmpty(IDVenta) ? string.Empty : IDVenta;
                //0 = nuevo, 36 = edit, si es diferente es un id no valido
                if (Id_venta.Length == 0 || Id_venta.Length == 36)
                {
                    Venta.Conexion = Conexion;
                    Venta.Id_venta = Id_venta;
                    Venta = VentaDatos.GetVentaEventoRecepcion(Venta);
                    if (Venta.RespuestaAjax.Success)
                    {
                        //aqui pondriamos alguna lista o valores de cargar si esta todo correcto
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
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista, error: " + Mensaje;
                return View("Index");
            }
        }
        [HttpPost]
        public ActionResult VentaEventoRecepcion(VentaModels2 Venta)
        {
            try
            {
                if (Token.IsTokenValid())
                {
                    _Venta2_Datos VentaDatos = new _Venta2_Datos();
                    Venta.Conexion = Conexion;
                    Venta.Usuario = User.Identity.Name;
                    Venta.RespuestaAjax = VentaDatos.AC_Recepcion(Venta);

                    return Content(Venta.RespuestaAjax.ToJSON(), "application/json");
                }
                else
                {
                    Venta.RespuestaAjax = new RespuestaAjax();
                    Venta.RespuestaAjax.Success = false;
                    Venta.RespuestaAjax.Mensaje = "Verifique sus datos.";
                    return Content(Venta.RespuestaAjax.ToJSON(), "application/json");
                }
            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                Venta.RespuestaAjax = new RespuestaAjax();
                Venta.RespuestaAjax.Success = false;
                Venta.RespuestaAjax.Mensaje = "Ocurrio un error al intentar guardar los datos. Contacte a soporte técnico: " + Mensaje;

                return Content(Venta.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        #endregion

        #region Vista Evento
        [HttpGet]
        public ActionResult VentaEvento(string IDVenta, int Id_eventoVenta)
        {
            try
            {
                Token.SaveToken();
                _Venta2_Datos VentaDatos = new _Venta2_Datos();
                VentaModels2 Venta = new VentaModels2();
                Venta.RespuestaAjax = new RespuestaAjax();
                Venta.EventoVenta = new EventoVentaModels();

                string Id_venta = string.IsNullOrEmpty(IDVenta) ? string.Empty : IDVenta;
                //0 = nuevo, 36 = edit, si es diferente es un id no valido
                if (Id_venta.Length == 0 || Id_venta.Length == 36)
                {
                    Venta.Conexion = Conexion;
                    Venta.EventoVenta.Id_venta = Id_venta;
                    Venta.EventoVenta.Id_eventoVenta = Id_eventoVenta;
                    Venta = VentaDatos.GetVentaEvento(Venta);
                    if (Venta.RespuestaAjax.Success)
                    {
                        if (string.IsNullOrEmpty(Venta.EventoVenta.ImagenBase64))
                        {
                            Venta.EventoVenta.ImagenMostrar = Auxiliar.SetDefaultImage();
                        }
                        else
                        {
                            Venta.EventoVenta.ImagenMostrar = Venta.EventoVenta.ImagenBase64;
                        }
                        Venta.EventoVenta.ExtensionImagenBase64 = Auxiliar.ObtenerExtensionImagenBase64(Venta.EventoVenta.ImagenMostrar);
                        //aqui pondriamos alguna lista o valores de cargar si esta todo correcto
                        Venta.EventoVenta.ListaDeTiposDeduccion = VentaDatos.GetTiposDeduccionVentaGanado(Venta);
                        Venta.EventoVenta.ListaTiposEventos = VentaDatos.GetTiposEventos(Venta);
                        _CatDeduccion_Datos oDatos = new _CatDeduccion_Datos();
                        ViewBag.ListaDeducciones = oDatos.SpCIDDB_Combo_get_CatDeduccion(Conexion);
                        return View(Venta.EventoVenta);
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
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista, error: " + Mensaje;
                return View("Index");
            }
        }
        [HttpPost]
        public ActionResult VentaEvento(EventoVentaModels Evento)
        {
            try
            {
                if (Token.IsTokenValid())
                {
                    _Venta2_Datos VentaDatos = new _Venta2_Datos();
                    Evento.Conexion = Conexion;
                    Evento.Usuario = User.Identity.Name;
                    if (Evento.HttpImagen != null)
                    {
                        Evento.ImagenBase64 = Auxiliar.ImageToBase64(Evento.HttpImagen);
                    }

                    Evento.RespuestaAjax = VentaDatos.AC_Evento(Evento);

                    if (Evento.RespuestaAjax.Success)
                    {
                        TempData["typemessage"] = "1";
                        TempData["message"] = "Los datos se guardarón correctamente.";
                        Token.ResetToken();

                        return Content(Evento.RespuestaAjax.ToJSON(), "application/json");
                    }
                    else
                    {
                        TempData["typemessage"] = "2";
                        TempData["message"] = "Ocurrio un error al intentar guardar los datos. Intente más tarde.";

                        return Content(Evento.RespuestaAjax.ToJSON(), "application/json");
                    }
                }
                else
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Verifique sus datos.";

                    Evento.RespuestaAjax = new RespuestaAjax();
                    Evento.RespuestaAjax.Success = false;

                    return Content(Evento.RespuestaAjax.ToJSON(), "application/json");
                }
            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error al intentar guardar los datos. Contacte a soporte técnico: " + Mensaje;
                Evento.RespuestaAjax = new RespuestaAjax();
                Evento.RespuestaAjax.Success = false;

                return Content(Evento.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        [HttpPost]
        public ActionResult Del_Evento(string IDVenta, int Id_eventoVenta)
        {
            try
            {
                Token.SaveToken();
                _Venta2_Datos VentaDatos = new _Venta2_Datos();
                VentaModels2 Venta = new VentaModels2();
                Venta.EventoVenta = new EventoVentaModels();
                Venta.EventoVenta.RespuestaAjax = new RespuestaAjax();

                //no puede ser null o vacio y debe de tener una longitud de 36, si no marcar error
                if (string.IsNullOrEmpty(IDVenta) || IDVenta.Length != 36)
                {
                    Venta.EventoVenta.RespuestaAjax = new RespuestaAjax();
                    Venta.EventoVenta.RespuestaAjax.Success = false;
                    Venta.EventoVenta.RespuestaAjax.Mensaje = "No se puede eliminar el evento, verifique su evento.";

                    return Content(Venta.EventoVenta.RespuestaAjax.ToJSON(), "application/json");
                }
                else
                {
                    Venta.EventoVenta.Id_venta = IDVenta;
                    Venta.EventoVenta.Id_eventoVenta = Id_eventoVenta;
                    Venta.EventoVenta.Usuario = User.Identity.Name;
                    Venta.EventoVenta.Conexion = Conexion;
                    Venta.EventoVenta.RespuestaAjax = VentaDatos.Del_Evento(Venta.EventoVenta);

                    return Content(Venta.EventoVenta.RespuestaAjax.ToJSON(), "application/json");
                }
            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                VentaModels2 Venta = new VentaModels2();
                Venta.EventoVenta = new EventoVentaModels();
                Venta.EventoVenta.RespuestaAjax = new RespuestaAjax();
                Venta.EventoVenta.RespuestaAjax.Success = false;
                Venta.EventoVenta.RespuestaAjax.Mensaje = "Contacte con soporte técnico, se ha producido el siguiente error: " + Mensaje;

                return Content(Venta.EventoVenta.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        #endregion

        #region Vista Edit
        public ActionResult Edit(string IDVenta, int IDEstatus)
        {
            switch (IDEstatus)
            {
                case 1:
                    return RedirectToAction("VentaFlete", "Venta", new { IDVenta = IDVenta });
                case 2:
                    return RedirectToAction("VentaGanado", "Venta", new { IDVenta = IDVenta });
                //case 3:
                //    return RedirectToAction("GanadoCompra", "Compra", new { IDCompra = Compra.IDCompra });
                case 4:
                    return RedirectToAction("VentaEventoRecepcion", "Venta", new { IDVenta = IDVenta });
                case 6:
                    return RedirectToAction("VentaGanado", "Venta", new { IDVenta = IDVenta });
                default:
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Verifique sus datos.";
                    return RedirectToAction("Index");
            }
        }
        #endregion

        #region Combos
        #region Choferes
        [HttpPost]
        public ActionResult GetChoferesXIDEmpresa(string IDEmpresa, string Id_sucursal)
        {
            try
            {
                _Venta2_Datos VentaDatos = new _Venta2_Datos();
                VentaModels2 Venta = new VentaModels2();
                Venta.Conexion = Conexion;
                Venta.Flete = new FleteModels();
                Venta.Flete.Id_empresa = IDEmpresa;
                Venta.Id_sucursal = Id_sucursal;
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

        [HttpPost]
        public ActionResult GetChoferesAuxilar(string Id_sucursal)
        {
            try
            {
                _Venta2_Datos VentaDatos = new _Venta2_Datos();
                VentaModels2 Venta = new VentaModels2();
                Venta.Conexion = Conexion;
                Venta.Flete = new FleteModels();
                Venta.Id_sucursal = Id_sucursal;
                Venta.Usuario = User.Identity.Name;
                Venta.Flete.ListaChoferAuxiliar = VentaDatos.GetChoferesAuxiliares(Venta);

                return Content(Venta.Flete.ListaChoferAuxiliar.ToJSON(), "application/json");
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
        public ActionResult GetVehiculosXIDEmpresa(string IDEmpresa, string Id_sucursal)
        {
            try
            {
                VentaModels2 Venta = new VentaModels2();
                _Venta2_Datos VentaDatos = new _Venta2_Datos();
                Venta.Conexion = Conexion;
                Venta.Flete = new FleteModels();
                Venta.Flete.Id_empresa = IDEmpresa;
                Venta.Usuario = User.Identity.Name;
                Venta.Id_sucursal = Id_sucursal;
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
        #region Empresas
        [HttpPost]
        public ActionResult GetEmpresas(string opcion)
        {
            try
            {
                _Venta2_Datos VentaDatos = new _Venta2_Datos();
                VentaModels2 Venta = new VentaModels2();
                Venta.Conexion = Conexion;
                Venta.Flete = new FleteModels();
                int opcion_flete = 0;
                int.TryParse(opcion, out opcion_flete);

                Venta.Usuario = User.Identity.Name;
                Venta.CobrarFlete = opcion_flete;
                Venta.Flete.ListaEmpresa = VentaDatos.GetListadoEmpresas(Venta);

                return Content(Venta.Flete.ListaEmpresa.ToJSON(), "application/json");
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

        #region Vista Documentos
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id_1">Id de la venta</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult VentaDocumentos(string Id_1)
        {
            try
            {
                Token.SaveToken();

                string Id_venta = string.IsNullOrEmpty(Id_1) ? string.Empty : Id_1;
                //0 = nuevo, 36 = edit
                if (Id_venta.Length == 36)
                {
                    _Venta2_Datos VentaDatos = new _Venta2_Datos();
                    VentaModels2 Venta = new VentaModels2();
                    DocumentoModels Documentacion = new DocumentoModels();

                    Venta.Conexion = Conexion;
                    Documentacion.Id_servicio = Id_venta;
                    Documentacion.Conexion = Conexion;
                    Documentacion.RespuestaAjax = new RespuestaAjax();
                    Documentacion = VentaDatos.GetVentaDocumentos(Documentacion);

                    if (Documentacion.RespuestaAjax.Success)
                    {
                        Documentacion.ListaConceptosSalidaDeduccionCobro = VentaDatos.GetTiposDeduccionCobro(Venta);
                        _CatDeduccion_Datos oDatos = new _CatDeduccion_Datos();
                        ViewBag.ListaDeducciones = oDatos.SpCIDDB_Combo_get_CatDeduccion(Conexion);
                        return View(Documentacion);
                    }
                    else
                    {
                        TempData["typemessage"] = "2";
                        TempData["message"] = Documentacion.RespuestaAjax.Mensaje;
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
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista, error: " + Mensaje;
                return View("Index");
            }
        }
        [HttpPost]
        public ActionResult AC_CostoDocumentos(DocumentoModels Documento)
        {
            try
            {
                Documento.RespuestaAjax = new RespuestaAjax();
                if (Token.IsTokenValid())
                {
                    _Venta2_Datos VentaDatos = new _Venta2_Datos();
                    Documento.Conexion = Conexion;
                    Documento.Usuario = User.Identity.Name;
                    Documento = VentaDatos.AC_CostoDocumentos(Documento);
                    Token.ResetToken();
                    Token.SaveToken();
                    if (!Documento.RespuestaAjax.Success)
                    {
                        TempData["typemessage"] = "2";
                        TempData["message"] = Documento.RespuestaAjax.Mensaje;
                    }
                    return Content(Documento.RespuestaAjax.ToJSON(), "application/json");
                }
                else
                {
                    Documento.RespuestaAjax.Success = false;
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Verifique sus datos.";
                    return Content(Documento.RespuestaAjax.ToJSON(), "application/json");
                }
            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                Documento.RespuestaAjax.Success = false;
                TempData["typemessage"] = "2";
                TempData["message"] = "Verifique sus datos, error: " + Mensaje;
                return Content(Documento.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        #endregion

        #region Vista Documento 
        [HttpGet]
        public ActionResult VentaDocumento(string Id_venta, string IDDocumento)
        {
            {
                if(string.IsNullOrEmpty(Id_venta) || string.IsNullOrEmpty(IDDocumento))
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Verifique sus datos.";
                    return View("Index");
                }

                if (Id_venta.Length == 0 || Id_venta.Length == 36)
                {
                    _Venta2_Datos VentaDatos = new _Venta2_Datos();
                    DocumentoModels Documento = new DocumentoModels();
                    Documento.Id_servicio = Id_venta;
                    Documento.IDDocumento = IDDocumento;
                    Documento.Conexion = Conexion;

                    Documento = VentaDatos.GetDocumentoXIDDocumento(Documento);
                    Documento.ListaTipoDocumentos = VentaDatos.GetListaTiposDocumentos(Documento);

                    return View(Documento);
                }
                else
                {
                    return View("Index");
                }
            }
        }
        [HttpPost]
        public ActionResult VentaDocumento(DocumentoModels Documento)
        {
            try
            {
                if (Token.IsTokenValid())
                {
                    if (Documento.Id_servicio.Length == 36)
                    {
                        _Venta2_Datos VentaDatos = new _Venta2_Datos();
                        Documento.Conexion = Conexion;
                        Documento.Usuario = User.Identity.Name;

                        if (Documento.ImagenPost != null)
                        {
                            Documento.ImagenServer = Auxiliar.ImageToBase64(Documento.ImagenPost);
                        }
                        Documento.RespuestaAjax = new RespuestaAjax();
                        Documento = VentaDatos.AC_Documento(Documento);
                        if (Documento.RespuestaAjax.Success)
                        {
                            Token.ResetToken();
                            TempData["typemessage"] = "1";
                            TempData["message"] = Documento.RespuestaAjax.Mensaje;
                            return RedirectToAction("VentaDocumentos", "Venta", new { Id_1 = Documento.Id_servicio });
                        }
                        else
                        {
                            TempData["typemessage"] = "2";
                            TempData["message"] = Documento.RespuestaAjax.Mensaje;
                            return RedirectToAction("Index", "Venta");
                        }
                    }
                    else
                    {
                        TempData["typemessage"] = "2";
                        TempData["message"] = "Verifique sus datos.";
                        return RedirectToAction("Index", "Venta");
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Venta");
                }
            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista, error: " + Mensaje;
                return RedirectToAction("Index", "Venta");
            }
        }
        [HttpPost]
        public ActionResult DEL_Documento(DocumentoModels Documento)
        {
            try
            {
                Documento.RespuestaAjax = new RespuestaAjax();
                if (Token.IsTokenValid())
                {
                    if (Documento.Id_servicio.Length == 36)
                    {
                        _Venta2_Datos VentaDatos = new _Venta2_Datos();
                        Documento.Conexion = Conexion;
                        Documento.Usuario = User.Identity.Name;
                        Documento = VentaDatos.DEL_DocumentoXIDDocumento(Documento);
                        Token.ResetToken();
                        Token.SaveToken();
                        return Content(Documento.RespuestaAjax.ToJSON(), "application/json");
                    }
                    else
                    {
                        Documento.RespuestaAjax.Success = false;
                        TempData["typemessage"] = "2";
                        TempData["message"] = "Verifique sus datos.";
                        return Content(Documento.RespuestaAjax.ToJSON(), "application/json");
                    }
                }
                else
                {
                    Documento.RespuestaAjax.Success = false;
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Verifique sus datos.";
                    return Content(Documento.RespuestaAjax.ToJSON(), "application/json");
                }
            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                Documento.RespuestaAjax.Mensaje = Mensaje;
                Documento.RespuestaAjax.Success = false;
                return Content(Documento.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        #endregion

        #region Carta Porte
        public ActionResult CartaPorte(string id)
        {
            try
            {

                Reporte_Datos R = new Reporte_Datos();
                CartaPorteModels reporte = new CartaPorteModels();
                _Venta2_Datos reporteDatos = new _Venta2_Datos();
                VentaModels2 Venta = new VentaModels2();
                Venta.Id_flete = id;
                Venta.Conexion = Conexion;
                reporte = reporteDatos.GetCartaPorte(Venta);
                reporte.ListaDetallesCartaPorte = reporteDatos.GetCartaPorteDetalles(Venta);
                LocalReport Rtp = new LocalReport();
                Rtp.EnableExternalImages = true;
                Rtp.DataSources.Clear();
                string path = Path.Combine(Server.MapPath("~/Formatos"), "CartaPorte.rdlc");
                if (System.IO.File.Exists(path))
                {
                    Rtp.ReportPath = path;
                }
                else
                {
                    return RedirectToAction("Index", "Venta");
                }
                ReportParameter[] Parametros = new ReportParameter[22];
                Parametros[0] = new ReportParameter("numFolio", reporte.Folio);
                Parametros[1] = new ReportParameter("nombreCliente", reporte.NombreCliente);
                Parametros[2] = new ReportParameter("RFC", reporte.RFCCliente);
                Parametros[3] = new ReportParameter("nombreConductor", reporte.NombreConductor);
                Parametros[4] = new ReportParameter("nombreVehiculo", reporte.Vehiculo);
                Parametros[5] = new ReportParameter("placasVehiculo", reporte.PlacaVehiculo);
                Parametros[6] = new ReportParameter("LogoRFC", reporte.LogoRFC);
                Parametros[7] = new ReportParameter("kilometros", reporte.Kilometros);
                Parametros[8] = new ReportParameter("nombreRemitente", reporte.Remitente);
                Parametros[9] = new ReportParameter("nombreDestinatario", reporte.Destinatario);
                Parametros[10] = new ReportParameter("domicilioRemitente", reporte.DomicilioRemitente);
                Parametros[11] = new ReportParameter("domicilioDestinatario", reporte.DomicilioDestinatario);
                Parametros[12] = new ReportParameter("recogeraEn", reporte.LugarOrigen);
                Parametros[13] = new ReportParameter("recibiraEn", reporte.LugarDestino);
                Parametros[14] = new ReportParameter("fechaEntrega", reporte.FechaEntrega.ToString());
                Parametros[15] = new ReportParameter("pesoAproximado", reporte.PesoAproximado.ToString());
                Parametros[16] = new ReportParameter("importeConLetras", reporte.ImporteConLetra);
                Parametros[17] = new ReportParameter("total", reporte.Total.ToString());
                Parametros[18] = new ReportParameter("condicionPago", reporte.CondicionPago);
                Parametros[19] = new ReportParameter("FormasPago", reporte.FormaPago);
                Parametros[20] = new ReportParameter("metodoPago", reporte.MetodoPago);
                Parametros[21] = new ReportParameter("subtotal", reporte.Total.ToString());

                Rtp.SetParameters(Parametros);
                Rtp.DataSources.Add(new ReportDataSource("DetallesCartaPorte", reporte.ListaDetallesCartaPorte));
                string reportType = "PDF";
                string mimeType;
                string encoding;
                string fileNameExtension;

                string deviceInfo = "<DeviceInfo>" +
                "  <OutputFormat>" + id + "</OutputFormat>" +
                "</DeviceInfo>";

                Warning[] warnings;
                string[] streams;
                byte[] renderedBytes;

                renderedBytes = Rtp.Render(
                    reportType,
                    deviceInfo,
                    out mimeType,
                    out encoding,
                    out fileNameExtension,
                    out streams,
                    out warnings);

                return File(renderedBytes, mimeType);
            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista, error: " + Mensaje;
                return View("Index");
            }
        }
        #endregion

        #region ReporteGanado
        public ActionResult ReporteGanado(string id)
        {
            try
            {

                Reporte_Datos R = new Reporte_Datos();
                List<ReporteGanadoModels> Listareporte = new List<ReporteGanadoModels>();
                _Venta2_Datos reporteDatos = new _Venta2_Datos();
                VentaModels2 Venta = new VentaModels2();
                CatEmpresaModels Empresa = new CatEmpresaModels();
                _CatEmpresa_Datos EmpresaDatos = new _CatEmpresa_Datos();
                Venta.Id_venta = id;
                Venta.Conexion = Conexion;
                Empresa.Conexion = Conexion;
                Listareporte = reporteDatos.GetReporteGanadoDetalles(Venta);
                Empresa = EmpresaDatos.GetDatosEmpresaPrincipal(Empresa);
                DatosGeneralesGanados datos = new DatosGeneralesGanados();
                datos = Auxiliar.ObtenerDatosGeneralesGanado(datos, Listareporte);
                LocalReport Rtp = new LocalReport();
                Rtp.EnableExternalImages = true;
                Rtp.DataSources.Clear();
                string path = Path.Combine(Server.MapPath("~/Formatos"), "ListadoGanado.rdlc");
                if (System.IO.File.Exists(path))
                {
                    Rtp.ReportPath = path;
                }
                else
                {
                    return RedirectToAction("Index", "Venta");
                }
                ReporteGanadoModels ReporteGanado = new ReporteGanadoModels();

                string GeneralesEmpresa = "<b>Representante: </b>" + Empresa.Representante + "<br/>";
                GeneralesEmpresa += "<b>RFC: </b>" + Empresa.RFC + "<br/>";
                GeneralesEmpresa += "<b>Horario de atención: </b>" + Empresa.HorarioAtencion + "<br/>";
                string Telefonos = string.IsNullOrEmpty(Empresa.NumTelefonico1) ? string.Empty : Empresa.NumTelefonico1;
                Telefonos += string.IsNullOrEmpty(Empresa.NumTelefonico2) ? string.Empty : " " + Empresa.NumTelefonico1;
                if (!string.IsNullOrEmpty(Telefonos))
                    GeneralesEmpresa += "<b>Teléfono(s): </b>" + Telefonos + "<br/>";
                if (!string.IsNullOrEmpty(Empresa.Email))
                    GeneralesEmpresa += "<b>Email: </b>" + Empresa.Email;

                ReportParameter[] Parametros = new ReportParameter[13];
                Parametros[0] = new ReportParameter("LogoEmpresa", Empresa.LogoEmpresa);
                Parametros[1] = new ReportParameter("NombreEmpresa", Empresa.RazonFiscal);
                Parametros[2] = new ReportParameter("DireccionEmpresa", Empresa.DireccionFiscal);
                Parametros[3] = new ReportParameter("GeneralesEmpresa", GeneralesEmpresa);
                Parametros[4] = new ReportParameter("TotalGanadoMachos", datos.TotalGanadoMachos.ToString());
                Parametros[5] = new ReportParameter("TotalGanadoHembras", datos.TotalGanadoHembras.ToString());
                Parametros[6] = new ReportParameter("TotalGanado", datos.TotalGanados.ToString());
                Parametros[7] = new ReportParameter("TotalKilosGanadoMachos", datos.StringTotalKilosGanadoMachos);
                Parametros[8] = new ReportParameter("TotalKilosGanadoHembras", datos.StringTotalKilosGanadoHembras);
                Parametros[9] = new ReportParameter("TotalKilosGanados", datos.StringTotalKilosGanados);
                Parametros[10] = new ReportParameter("TotalMermaGanadoMachos", datos.StringTotalMermaGanadoMachos);
                Parametros[11] = new ReportParameter("TotalMermaGanadoHembras", datos.StringTotalMermaGanadoHembras);
                Parametros[12] = new ReportParameter("TotalMermaGanados", datos.StringTotalMermaGanados);

                Rtp.SetParameters(Parametros);
                Rtp.DataSources.Add(new ReportDataSource("ListaGanado", Listareporte));
                Rtp.Refresh();
                string reportType = "EXCEL";
                string mimeType;
                string encoding;
                string fileNameExtension;

                string deviceInfo = "<DeviceInfo>" +
                "  <OutputFormat>" + id + "</OutputFormat>" +
                "</DeviceInfo>";

                Warning[] warnings;
                string[] streams;
                byte[] renderedBytes;

                renderedBytes = Rtp.Render(
                    reportType,
                    deviceInfo,
                    out mimeType,
                    out encoding,
                    out fileNameExtension,
                    out streams,
                    out warnings);

                return File(renderedBytes, mimeType);
            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");

                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista, error: " + Mensaje;
                return View("Index");
            }
        }

        public ActionResult ReporteGanadoV2(string id)
        {
            try
            {

                Reporte_Datos R = new Reporte_Datos();
                List<ReporteGanadoModels> Listareporte = new List<ReporteGanadoModels>();
                _Venta2_Datos reporteDatos = new _Venta2_Datos();
                VentaModels2 Venta = new VentaModels2();
                ReporteCabeceraGanado Cabezera = new ReporteCabeceraGanado();
                List<CatFierroModels> ListaFierros = new List<CatFierroModels>();

                Venta.Id_venta = id;
                Venta.Conexion = Conexion;
                Listareporte = reporteDatos.GetReporteGanadoDetalles(Venta);
                Cabezera = reporteDatos.GetReporteCabeceraGanadoDetalles(Venta);
                ListaFierros = reporteDatos.GetReporteFierrosVenta(Venta);

                LocalReport Rtp = new LocalReport();
                Rtp.EnableExternalImages = true;
                Rtp.DataSources.Clear();
                string path = Path.Combine(Server.MapPath("~/Formatos"), "ListadoGanadoV2.rdlc");
                if (System.IO.File.Exists(path))
                {
                    Rtp.ReportPath = path;
                }
                else
                {
                    return RedirectToAction("Index", "Venta");
                }
                ReporteGanadoModels ReporteGanado = new ReporteGanadoModels();

                ReportParameter[] Parametros = new ReportParameter[19];
                Parametros[0] = new ReportParameter("NombreChofer", Cabezera.NombreChofer);
                Parametros[1] = new ReportParameter("UnidadVehiculo", Cabezera.UnidadVehiculo);
                Parametros[2] = new ReportParameter("ModeloVehiculo", Cabezera.ModeloVehiculo);
                Parametros[3] = new ReportParameter("MarcaVehiculo", Cabezera.MarcaVehiculo);
                Parametros[4] = new ReportParameter("ColorVehiculo", Cabezera.ColorVehiculo);
                Parametros[5] = new ReportParameter("CapacidadVehiculo", Cabezera.CapacidadVehiculo);
                Parametros[6] = new ReportParameter("GPS", Cabezera.GPS);
                Parametros[7] = new ReportParameter("FechaHoraSalida", Cabezera.FechaHoraSalida.ToString());
                Parametros[8] = new ReportParameter("FechaHoraEmbarque", Cabezera.FechaHoraEmbarque.ToString());
                Parametros[9] = new ReportParameter("LugarOrigen", Cabezera.LugarOrigen);
                Parametros[10] = new ReportParameter("LugarDestino", Cabezera.LugarDestino);
                Parametros[11] = new ReportParameter("PSGOrigen", Cabezera.PSGOrigen);
                Parametros[12] = new ReportParameter("PSGDestino", Cabezera.PSGDestino);
                Parametros[13] = new ReportParameter("TotalGanadoMachos", Cabezera.TotalGanadoMachos.ToString());
                Parametros[14] = new ReportParameter("TotalGanadoHembras", Cabezera.TotalGanadoHembras.ToString());
                Parametros[15] = new ReportParameter("TotalGanado", Cabezera.TotalGanado.ToString());
                Parametros[16] = new ReportParameter("TotalKilosGanado", Cabezera.TotalKilosGanado.ToString("N2"));
                Parametros[17] = new ReportParameter("PlacaTracto", Cabezera.PlacaTracto);
                Parametros[18] = new ReportParameter("PlacaJaula", Cabezera.PlacaJaula);

                Rtp.SetParameters(Parametros);
                Rtp.DataSources.Add(new ReportDataSource("ListaGanado", Listareporte));
                Rtp.DataSources.Add(new ReportDataSource("ListaFierros", ListaFierros));
                Rtp.Refresh();
                string reportType = "EXCEL";
                string mimeType;
                string encoding;
                string fileNameExtension;

                string deviceInfo = "<DeviceInfo>" +
                "  <OutputFormat>" + id + "</OutputFormat>" +
                "</DeviceInfo>";

                Warning[] warnings;
                string[] streams;
                byte[] renderedBytes;

                renderedBytes = Rtp.Render(
                    reportType,
                    deviceInfo,
                    out mimeType,
                    out encoding,
                    out fileNameExtension,
                    out streams,
                    out warnings);

                return File(renderedBytes, mimeType);
            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");

                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista, error: " + Mensaje;
                return View("Index");
            }
        }
        #endregion

        #region Vista transacciones
        [HttpGet]
        public ActionResult VentaTransacciones(string IDVenta)
        {
            try
            {
                Token.SaveToken();
                _Venta2_Datos VentaDatos = new _Venta2_Datos();

                string Id_venta = string.IsNullOrEmpty(IDVenta) ? string.Empty : IDVenta;

                if (Id_venta.Length == 36)
                {
                    VentaTransacionesModels TransaccionesVenta = new VentaTransacionesModels();
                    TransaccionesVenta.Conexion = Conexion;
                    TransaccionesVenta.Id_venta = Id_venta;
                    TransaccionesVenta.RespuestaAjax = new RespuestaAjax();
                    TransaccionesVenta = VentaDatos.GetTransacciones(TransaccionesVenta);
                    if (TransaccionesVenta.RespuestaAjax.Success)
                    {
                        return View(TransaccionesVenta);
                    }
                    else
                    {
                        TempData["typemessage"] = "2";
                        TempData["message"] = "No se puede cargar la vista, error: " + TransaccionesVenta.RespuestaAjax.Mensaje;
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
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista, error: " + Mensaje;
                return View("Index");
            }
        }
        [HttpPost]
        public ActionResult DelComprobante(DocumentosPorCobrarDetallePagosModels DocumentoPorCobrarPago)
        {
            try
            {

                if (Token.IsTokenValid())
                {
                    DocumentoPorCobrarPago.Usuario = User.Identity.Name;
                    DocumentoPorCobrarPago.Conexion = Conexion;
                    DocumentoPorCobrarPago.RespuestaAjax = new RespuestaAjax();
                    _Venta2_Datos VentaDatos = new _Venta2_Datos();
                    DocumentoPorCobrarPago = VentaDatos.DEL_ComprobanteCobro(DocumentoPorCobrarPago);

                    if (DocumentoPorCobrarPago.RespuestaAjax.Success)
                    {
                        TempData["typemessage"] = "1";
                        TempData["message"] = DocumentoPorCobrarPago.RespuestaAjax.Mensaje;
                    }
                    else
                    {
                        TempData["typemessage"] = "2";
                        TempData["message"] = DocumentoPorCobrarPago.RespuestaAjax.Mensaje;
                    }

                    return Content(DocumentoPorCobrarPago.RespuestaAjax.ToJSON(), "application/json");
                }
                else
                {
                    DocumentoPorCobrarPago.RespuestaAjax = new RespuestaAjax();
                    DocumentoPorCobrarPago.RespuestaAjax.Success = false;

                    TempData["typemessage"] = "2";
                    TempData["message"] = "Verifique sus datos";

                    return Content(DocumentoPorCobrarPago.RespuestaAjax.ToJSON(), "application/json");
                }
            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                DocumentoPorCobrarPago.RespuestaAjax = new RespuestaAjax();
                DocumentoPorCobrarPago.RespuestaAjax.Mensaje = "Verifique sus datos, error: " + Mensaje;
                return Content(DocumentoPorCobrarPago.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        [HttpPost]
        public ActionResult DEL_Impuesto(ImpuestoModels ImpuestoProducto)
        {
            try
            {
                if (ImpuestoProducto.IDImpuesto.Length == 36)
                {
                    if (Token.IsTokenValid())
                    {
                        _Venta2_Datos VentaDatos = new _Venta2_Datos();
                        ImpuestoProducto.Conexion = Conexion;
                        ImpuestoProducto.Usuario = User.Identity.Name;
                        ImpuestoProducto = VentaDatos.Del_Impuesto(ImpuestoProducto);

                        if (ImpuestoProducto.RespuestaAjax.Success)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = ImpuestoProducto.RespuestaAjax.Mensaje;
                            return Content(ImpuestoProducto.RespuestaAjax.ToJSON(), "application/json");
                        }
                        else
                        {
                            TempData["typemessage"] = "2";
                            TempData["message"] = ImpuestoProducto.RespuestaAjax.Mensaje;
                            return Content(ImpuestoProducto.RespuestaAjax.ToJSON(), "application/json");
                        }
                    }
                    else
                    {
                        ImpuestoProducto.RespuestaAjax.Success = false;
                        TempData["typemessage"] = "2";
                        TempData["message"] = "Impuesto no válido.";
                        return Content(ImpuestoProducto.RespuestaAjax.ToJSON(), "application/json");
                    }
                }
                else
                {
                    ImpuestoProducto.RespuestaAjax.Success = false;
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Impuesto no válido.";
                    return Content(ImpuestoProducto.RespuestaAjax.ToJSON(), "application/json");
                }
            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                ImpuestoProducto.RespuestaAjax.Success = false;
                TempData["typemessage"] = "2";
                TempData["message"] = "Contacte con soporte técnico, error: " + Mensaje;
                return Content(ImpuestoProducto.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        #endregion

        #region Vista Cobro
        /// <summary>
        /// Vista que actualiza o crea un cobro
        /// </summary>
        /// <param name="id_1">El id de la venta</param>
        /// <param name="id_2">El id del documento por cobrar detalle pago</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult VentaCobro(string id_1, string id_2)
        {
            try
            {
                Token.SaveToken();
                _Venta2_Datos VentaDatos = new _Venta2_Datos();
                DocumentosPorCobrarDetallePagosModels DocumentoPorCobrarPago = new DocumentosPorCobrarDetallePagosModels();
                DocumentoPorCobrarPago.RespuestaAjax = new RespuestaAjax();

                string Id_venta = string.IsNullOrEmpty(id_1) ? string.Empty : id_1;
                string Id_documentoPorCobrarDetallePago = string.IsNullOrEmpty(id_2) ? string.Empty : id_2;

                if (Id_venta.Length == 36 && (Id_documentoPorCobrarDetallePago.Length == 0 || Id_documentoPorCobrarDetallePago.Length == 36))
                {
                    DocumentoPorCobrarPago.Conexion = Conexion;
                    DocumentoPorCobrarPago.Id_padre = Id_venta;
                    DocumentoPorCobrarPago.Id_documentoPorCobrarDetallePagos = Id_documentoPorCobrarDetallePago;

                    DocumentoPorCobrarPago = VentaDatos.GetDetalleDocumentoPago(DocumentoPorCobrarPago);

                    if (DocumentoPorCobrarPago.RespuestaAjax.Success)
                    {
                        if (string.IsNullOrEmpty(DocumentoPorCobrarPago.ImagenBase64))
                        {
                            DocumentoPorCobrarPago.ImagenMostrar = Auxiliar.SetDefaultImage();
                        }
                        else
                        {
                            DocumentoPorCobrarPago.ImagenMostrar = DocumentoPorCobrarPago.ImagenBase64;
                        }
                        DocumentoPorCobrarPago.ExtensionImagenBase64 = Auxiliar.ObtenerExtensionImagenBase64(DocumentoPorCobrarPago.ImagenMostrar);


                        DocumentoPorCobrarPago.ListaFormaPagos = VentaDatos.GetListadoCFDIFormaPago(DocumentoPorCobrarPago);
                        DocumentoPorCobrarPago = VentaDatos.GetNombreEmpresaCliente(DocumentoPorCobrarPago);
                        DocumentoPorCobrarPago.TipoCuentaBancaria = 1;
                        DocumentoPorCobrarPago.ListaCuentasBancariasEmpresa = VentaDatos.GetListadoCuentasBancarias(DocumentoPorCobrarPago);
                        DocumentoPorCobrarPago.TipoCuentaBancaria = 2;
                        DocumentoPorCobrarPago.ListaCuentasBancariasProveedor = VentaDatos.GetListadoCuentasBancarias(DocumentoPorCobrarPago);
                        return View(DocumentoPorCobrarPago);
                    }
                    else
                    {
                        TempData["typemessage"] = "2";
                        TempData["message"] = "No se puede cargar la vista, error: " + DocumentoPorCobrarPago.RespuestaAjax.Mensaje;
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
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista, error: " + Mensaje;
                return View("Index");
            }
        }
        [HttpPost]
        public ActionResult VentaCobro(DocumentosPorCobrarDetallePagosModels DocumentoPorCobrarPago)
        {
            try
            {
                if (Token.IsTokenValid())
                {

                    DocumentoPorCobrarPago.Usuario = User.Identity.Name;
                    DocumentoPorCobrarPago.Conexion = Conexion;
                    DocumentoPorCobrarPago.RespuestaAjax = new RespuestaAjax();
                    if (DocumentoPorCobrarPago.Bancarizado)
                    {
                        if (DocumentoPorCobrarPago.HttpImagen == null)
                        {
                            DocumentoPorCobrarPago.ImagenBase64 = DocumentoPorCobrarPago.ImagenMostrar;
                        }
                        else
                        {
                            DocumentoPorCobrarPago.ImagenBase64 = Auxiliar.ImageToBase64(DocumentoPorCobrarPago.HttpImagen);
                        }
                    }
                    _Venta2_Datos VentaDatos = new _Venta2_Datos();

                    DocumentoPorCobrarPago = VentaDatos.AC_ComprobanteCobro(DocumentoPorCobrarPago);

                    if (DocumentoPorCobrarPago.RespuestaAjax.Success)
                    {
                        TempData["typemessage"] = "1";
                        TempData["message"] = "Datos guardados correctamente.";
                        Token.ResetToken();
                    }
                    else
                    {
                        TempData["typemessage"] = "2";
                        TempData["message"] = DocumentoPorCobrarPago.RespuestaAjax.Mensaje;

                    }
                    return RedirectToAction("VentaTransacciones", "Venta", new { IDVenta = DocumentoPorCobrarPago.Id_padre });
                }
                else
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Verifique sus datos.";
                    return RedirectToAction("Index", "Venta");
                }
            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                TempData["typemessage"] = "2";
                TempData["message"] = "Verifique sus datos, error: " + Mensaje;
                return RedirectToAction("Index", "Venta");
            }
        }
        #endregion

        #region Vista Producto/Servicio
        [HttpGet]
        public ActionResult VentaProductoServicio(string Id_venta, string Id_documentoPorCobrar, string Id_detalleDocumento)
        {
            try
            {
                DocumentosPorCobrarDetalleModels DocumentoPorCobrarDetalle = new DocumentosPorCobrarDetalleModels();
                _Venta2_Datos VentaDatos = new _Venta2_Datos();


                //0 = nuevo, 36 = editar, pero ambos son válidos
                if ((Id_venta.Length == 36) && (Id_documentoPorCobrar.Length == 36) && (Id_detalleDocumento.Length == 0 || Id_detalleDocumento.Length == 36 || string.IsNullOrEmpty(Id_detalleDocumento)))
                {
                    Token.SaveToken();
                    DocumentoPorCobrarDetalle.Id_servicio = Id_venta;
                    DocumentoPorCobrarDetalle.Id_documentoCobrar = Id_documentoPorCobrar;
                    DocumentoPorCobrarDetalle.Id_detalleDoctoCobrar = Id_detalleDocumento;
                    DocumentoPorCobrarDetalle.Conexion = Conexion;
                    DocumentoPorCobrarDetalle.RespuestaAjax = new RespuestaAjax();
                    DocumentoPorCobrarDetalle = VentaDatos.GetDetalleDocumentoPorCobrar(DocumentoPorCobrarDetalle);
                    DocumentoPorCobrarDetalle.ListaTipoClasificacionCobro = VentaDatos.GetListadoTipoClasificacion(DocumentoPorCobrarDetalle);
                    DocumentoPorCobrarDetalle.ListaProductosServiciosCFDI = VentaDatos.GetListadoCFDIProductosServiciosCompra(DocumentoPorCobrarDetalle);

                    DocumentoPorCobrarDetalle.ListaAlmacen = VentaDatos.GetAlmacenesHabilitados(DocumentoPorCobrarDetalle);
                    DocumentoPorCobrarDetalle.ListaProductos = VentaDatos.GetProductosAlmacen(DocumentoPorCobrarDetalle);

                    return View(DocumentoPorCobrarDetalle);
                }
                else
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "No se puede cargar la vista, verifique sus datos.";
                    return View("Index");
                }
            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                TempData["typemessage"] = "2";
                TempData["message"] = "Verifique sus datos, error: " + Mensaje;
                return View("Index");
            }
        }
        [HttpPost]
        public ActionResult VentaProductoServicio(DocumentosPorCobrarDetalleModels DocumentoPorCobrarDetalle)
        {
            try
            {
                if (Token.IsTokenValid())
                {
                    _Venta2_Datos VentaDatos = new _Venta2_Datos();
                    DocumentoPorCobrarDetalle.Conexion = Conexion;
                    DocumentoPorCobrarDetalle.Usuario = User.Identity.Name;
                    DocumentoPorCobrarDetalle.RespuestaAjax = new RespuestaAjax();
                    DocumentoPorCobrarDetalle = VentaDatos.AC_ProductoServicio(DocumentoPorCobrarDetalle);

                    if (DocumentoPorCobrarDetalle.RespuestaAjax.Success)
                    {
                        TempData["typemessage"] = "1";
                        TempData["message"] = DocumentoPorCobrarDetalle.RespuestaAjax.Mensaje;
                        Token.ResetToken();
                        return RedirectToAction("VentaTransacciones", "Venta", new { IDVenta = DocumentoPorCobrarDetalle.Id_servicio });
                    }
                    else
                    {
                        TempData["typemessage"] = "2";
                        TempData["message"] = DocumentoPorCobrarDetalle.RespuestaAjax.Mensaje;
                        return RedirectToAction("VentaProductoServicio", "Venta", new { IDVenta = DocumentoPorCobrarDetalle.Id_servicio, Id_documentoPorCobrar = DocumentoPorCobrarDetalle.Id_documentoCobrar, Id_detalleDocumento = DocumentoPorCobrarDetalle.Id_detalleDoctoCobrar });
                    }
                }
                else
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Verifique sus datos.";
                    return RedirectToAction("Index", "Venta");
                }
            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                TempData["typemessage"] = "2";
                TempData["message"] = "Verifique sus datos, error: " + Mensaje;
                return RedirectToAction("Index", "Venta");
            }
        }
        [HttpPost]
        public ActionResult Del_ProductoServicio(DocumentosPorCobrarDetalleModels documento)
        {
            try
            {
                if (Token.IsTokenValid())
                {
                    _Venta2_Datos VentaDatos = new _Venta2_Datos();
                    documento.Conexion = Conexion;
                    documento.Usuario = User.Identity.Name;
                    documento.RespuestaAjax = new RespuestaAjax();
                    documento = VentaDatos.DEL_ProductoServicioCompra(documento);

                    if (documento.RespuestaAjax.Success)
                    {
                        TempData["typemessage"] = "1";
                        TempData["message"] = documento.RespuestaAjax.Mensaje;
                        Token.ResetToken();
                    }
                    else
                    {
                        TempData["typemessage"] = "2";
                        TempData["message"] = documento.RespuestaAjax.Mensaje;
                    }

                    return Content(documento.RespuestaAjax.ToJSON(), "application/json");
                }
                else
                {
                    documento.RespuestaAjax = new RespuestaAjax();
                    documento.RespuestaAjax.Success = false;

                    TempData["typemessage"] = "2";
                    TempData["message"] = "Verifique sus datos";

                    return Content(documento.RespuestaAjax.ToJSON(), "application/json");
                }

            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                documento.RespuestaAjax = new RespuestaAjax();
                documento.RespuestaAjax.Success = false;

                TempData["typemessage"] = "2";
                TempData["message"] = Mensaje;

                return Content(documento.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        [HttpGet]
        public ActionResult Edit_FleteProductoServicio(string Id_flete, string Id_documentoPorCobrar, string Id_detalleDocumento)
        {
            try
            {
                DocumentosPorCobrarDetalleModels DocumentoPorCobrarDetalle = new DocumentosPorCobrarDetalleModels();
                _Flete_Datos FleteDatos = new _Flete_Datos();


                //0 = nuevo, 36 = editar, pero ambos son válidos
                if ((Id_flete.Length == 36) && (Id_documentoPorCobrar.Length == 36) && (Id_detalleDocumento.Length == 0 || Id_detalleDocumento.Length == 36 || string.IsNullOrEmpty(Id_detalleDocumento)))
                {
                    Token.SaveToken();
                    DocumentoPorCobrarDetalle.Id_servicio = Id_flete;
                    DocumentoPorCobrarDetalle.Id_documentoCobrar = Id_documentoPorCobrar;
                    DocumentoPorCobrarDetalle.Id_detalleDoctoCobrar = Id_detalleDocumento;
                    DocumentoPorCobrarDetalle.Conexion = Conexion;
                    DocumentoPorCobrarDetalle.RespuestaAjax = new RespuestaAjax();
                    DocumentoPorCobrarDetalle = FleteDatos.GetDetalleDocumentoPorCobrarEdit(DocumentoPorCobrarDetalle);
                    DocumentoPorCobrarDetalle.ListaProductosServiciosCFDI = FleteDatos.GetListadoCFDIProductosServiciosCompra(DocumentoPorCobrarDetalle);

                    return View(DocumentoPorCobrarDetalle);
                }
                else
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "No se puede cargar la vista, verifique sus datos.";
                    return View("Index");
                }
            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                TempData["typemessage"] = "2";
                TempData["message"] = "Verifique sus datos, error: " + Mensaje;
                return View("Index");
            }
        }
        #endregion

        #region Vista impuesto productoServicio
        /// <summary>
        /// Muestra los impuestos de un producto
        /// </summary>
        /// <param name="Id1">Id de la venta</param>
        /// <param name="Id2">Id del detalle Documento por Cobrar</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult VentaImpuestoProductoServicio(string Id_1, string Id_2)
        {
            try
            {
                ImpuestoModels DocumentoPorCobrarDetalleImpuesto = new ImpuestoModels();
                _Venta2_Datos VentaDatos = new _Venta2_Datos();

                if ((Id_1.Length == 36) && (Id_2.Length == 36))
                {
                    Token.SaveToken();
                    DocumentoPorCobrarDetalleImpuesto.IDModulo = Id_1;
                    DocumentoPorCobrarDetalleImpuesto.Id_detalleDoctoCobrar = Id_2;
                    DocumentoPorCobrarDetalleImpuesto.Conexion = Conexion;
                    DocumentoPorCobrarDetalleImpuesto.RespuestaAjax = new RespuestaAjax();
                    DocumentoPorCobrarDetalleImpuesto = VentaDatos.Get_AC_Impuestos(DocumentoPorCobrarDetalleImpuesto);

                    return View(DocumentoPorCobrarDetalleImpuesto);
                }
                else
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "No se puede cargar la vista, verifique sus datos.";
                    return View("Index");
                }
            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                TempData["typemessage"] = "2";
                TempData["message"] = "Verifique sus datos, error: " + Mensaje;
                return View("Index");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id1">Id flete</param>
        /// <param name="Id2">Id documento detalle</param>
        /// <param name="Id3">Id documento detalle impuesto</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Venta_ProductoServicio(string Id1, string Id2, string Id3)
        {
            try
            {
                Token.SaveToken();

                string Id_modulo = string.IsNullOrEmpty(Id1) ? string.Empty : Id1;
                string Id_detalleDocumento = string.IsNullOrEmpty(Id2) ? string.Empty : Id2;

                if (string.IsNullOrEmpty(Id_modulo) || string.IsNullOrEmpty(Id_detalleDocumento))
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Verifique sus datos.";
                    return View("Index");
                }
                else
                {
                    string Id_Impuesto = string.IsNullOrEmpty(Id3) ? string.Empty : Id3;

                    if (Id_Impuesto.Length == 0 || Id_Impuesto.Length == 36)
                    {
                        ImpuestoModels Impuesto = new ImpuestoModels();
                        _Venta2_Datos VentaDatos = new _Venta2_Datos();

                        Impuesto.Conexion = Conexion;
                        Impuesto.IDModulo = Id1;
                        Impuesto.Id_detalleDoctoCobrar = Id2;
                        Impuesto.IDImpuesto = Id3;

                        Impuesto.Conexion = Conexion;
                        Impuesto = VentaDatos.GetImpuestoXIDImpuesto(Impuesto);

                        if (Impuesto.RespuestaAjax.Success)
                        {
                            Impuesto.ListaImpuesto = VentaDatos.GetListadoImpuesto(Impuesto);
                            Impuesto.ListaTipoImpuesto = VentaDatos.GetListadoTipoImpuesto(Impuesto);
                            Impuesto.ListaTipoFactor = VentaDatos.GetListadoTipoFactor(Impuesto);
                        }
                        else
                        {
                            TempData["typemessage"] = "2";
                            TempData["message"] = "No se puede cargar la vista, error: " + Impuesto.RespuestaAjax.Mensaje;
                            return View("Index");
                        }

                        return View(Impuesto);
                    }
                    else
                    {
                        TempData["typemessage"] = "2";
                        TempData["message"] = "Verifique sus datos.";
                        return View("Index");
                    }
                }
            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista, error: " + Mensaje;
                return View("Index");
            }
        }
        [HttpPost]
        public ActionResult Venta_ProductoServicio(ImpuestoModels ImpuestoProducto)
        {
            try
            {
                if (Token.IsTokenValid())
                {
                    _Venta2_Datos VentaDatos = new _Venta2_Datos();
                    ImpuestoProducto.Conexion = Conexion;
                    ImpuestoProducto.Usuario = User.Identity.Name;
                    ImpuestoProducto.RespuestaAjax = new RespuestaAjax();
                    ImpuestoProducto = VentaDatos.AC_ImpuestoProductoServicio(ImpuestoProducto);

                    if (ImpuestoProducto.RespuestaAjax.Success)
                    {
                        TempData["typemessage"] = "1";
                        TempData["message"] = ImpuestoProducto.RespuestaAjax.Mensaje;
                        Token.ResetToken();
                        return RedirectToAction("VentaImpuestoProductoServicio", "Venta", new { Id_1 = ImpuestoProducto.IDModulo, Id_2 = ImpuestoProducto.Id_detalleDoctoCobrar });
                    }
                    else
                    {
                        TempData["typemessage"] = "2";
                        TempData["message"] = ImpuestoProducto.RespuestaAjax.Mensaje;
                        return RedirectToAction("VentaImpuestoProductoServicio", "Venta", new { Id_1 = ImpuestoProducto.IDModulo, Id_2 = ImpuestoProducto.Id_detalleDoctoCobrar });
                    }
                }
                else
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Verifique sus datos.";
                    return RedirectToAction("Index", "Venta");
                }
            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                TempData["typemessage"] = "2";
                TempData["message"] = "Verifique sus datos, error: " + Mensaje;
                return RedirectToAction("Index", "Venta");
            }
        }
        #endregion

        #region Vista detalles
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id_1">Id de la venta</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult VentaDetalles(string Id_1)
        {
            try
            {
                if (string.IsNullOrEmpty(Id_1))
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "No se puede cargar la vista, verifique sus datos.";
                    return View("Index");
                }
                if(Id_1.Length != 36)
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "No se puede cargar la vista, verifique sus datos.";
                    return View("Index");
                }

                Token.SaveToken();
                VentaDetalleModels VentaDetalle = new VentaDetalleModels();
                _Venta2_Datos VentaDatos = new _Venta2_Datos();
                VentaDetalle.Id_venta = Id_1;
                VentaDetalle.Conexion = Conexion;
                VentaDetalle = VentaDatos.Get_detallesVenta(VentaDetalle);

                VentaTransacionesModels Transacciones = new VentaTransacionesModels();
                Transacciones.RespuestaAjax = new RespuestaAjax();
                Transacciones.Conexion = Conexion;
                Transacciones.Id_venta = Id_1;
                Transacciones = VentaDatos.GetTransacciones(Transacciones);

                VentaDetalle.Total = Transacciones.DocumentosPorCobrar.Total;
                VentaDetalle.TotalPercepciones = Transacciones.DocumentosPorCobrar.TotalPercepciones;
                VentaDetalle.TotalDeducciones = Transacciones.DocumentosPorCobrar.TotalDeducciones;
                VentaDetalle.Impuestos = Transacciones.DocumentosPorCobrar.Impuestos;
                VentaDetalle.Subtotal = Transacciones.Subtotal;

                VentaDetalle.Pendiente = Transacciones.DocumentosPorCobrar.Pendiente;
                VentaDetalle.Pagos = Transacciones.DocumentosPorCobrar.Pagos;
                VentaDetalle.Cambio = Transacciones.DocumentosPorCobrar.Cambio;

                return View(VentaDetalle);
            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                TempData["typemessage"] = "2";
                TempData["message"] = "Verifique sus datos, error: " + Mensaje;
                return View("Index");
            }
        }

        [HttpGet]
        public ActionResult VentaDetallesEvento(string IDVenta, int Id_eventoVenta)
        {
            try
            {
                Token.SaveToken();
                _Venta2_Datos VentaDatos = new _Venta2_Datos();
                VentaModels2 Venta = new VentaModels2();
                Venta.RespuestaAjax = new RespuestaAjax();
                Venta.EventoVenta = new EventoVentaModels();

                string Id_venta = string.IsNullOrEmpty(IDVenta) ? string.Empty : IDVenta;
                //0 = nuevo, 36 = edit, si es diferente es un id no valido
                if (Id_venta.Length == 0 || Id_venta.Length == 36)
                {
                    Venta.Conexion = Conexion;
                    Venta.EventoVenta.Id_venta = Id_venta;
                    Venta.EventoVenta.Id_eventoVenta = Id_eventoVenta;
                    Venta = VentaDatos.GetVentaEvento(Venta);
                    if (Venta.RespuestaAjax.Success)
                    {
                        if (string.IsNullOrEmpty(Venta.EventoVenta.ImagenBase64))
                        {
                            Venta.EventoVenta.ImagenMostrar = Auxiliar.SetDefaultImage();
                        }
                        else
                        {
                            Venta.EventoVenta.ImagenMostrar = Venta.EventoVenta.ImagenBase64;
                        }
                        Venta.EventoVenta.ExtensionImagenBase64 = Auxiliar.ObtenerExtensionImagenBase64(Venta.EventoVenta.ImagenMostrar);
                        //aqui pondriamos alguna lista o valores de cargar si esta todo correcto
                        Venta.EventoVenta.ListaDeTiposDeduccion = VentaDatos.GetTiposDeduccionVentaGanado(Venta);
                        Venta.EventoVenta.ListaTiposEventos = VentaDatos.GetTiposEventos(Venta);
                        _CatDeduccion_Datos oDatos = new _CatDeduccion_Datos();
                        ViewBag.ListaDeducciones = oDatos.SpCIDDB_Combo_get_CatDeduccion(Conexion);

                        return View(Venta.EventoVenta);
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
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista, error: " + Mensaje;
                return View("Index");
            }
        }
        #endregion

        #region Delete Venta
        [HttpPost]
        public ActionResult DEL_Venta(VentaModels2 Venta)
        {
            try
            {
                if (Venta.Id_venta.Length == 36)
                {
                    if (Token.IsTokenValid())
                    {
                        _Venta2_Datos VentaDatos = new _Venta2_Datos();
                        Venta.Conexion = Conexion;
                        Venta.Usuario = User.Identity.Name;
                        Venta = VentaDatos.Venta_del_Venta(Venta);

                        if (Venta.RespuestaAjax.Success)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = Venta.RespuestaAjax.Mensaje;
                            return Content(Venta.RespuestaAjax.ToJSON(), "application/json");
                        }
                        else
                        {
                            TempData["typemessage"] = "2";
                            TempData["message"] = Venta.RespuestaAjax.Mensaje;
                            return Content(Venta.RespuestaAjax.ToJSON(), "application/json");
                        }
                    }
                    else
                    {
                        Venta.RespuestaAjax.Success = false;
                        TempData["typemessage"] = "2";
                        TempData["message"] = "Verifique sus datos.";
                        return Content(Venta.RespuestaAjax.ToJSON(), "application/json");
                    }
                }
                else
                {
                    Venta.RespuestaAjax.Success = false;
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Venta no válida.";
                    return Content(Venta.RespuestaAjax.ToJSON(), "application/json");
                }
            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                Venta.RespuestaAjax.Success = false;
                TempData["typemessage"] = "2";
                TempData["message"] = "Contacte con soporte técnico, error: " + Mensaje;
                return Content(Venta.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        #endregion

        [HttpPost]
        public ActionResult GetClientesXIdSucursal(string Id_sucursal)
        {
            try
            {
                VentaModels2 Venta = new VentaModels2();
                _Venta2_Datos VentaDatos = new _Venta2_Datos();
                Venta.Conexion = Conexion;
                Venta.Id_sucursal = Id_sucursal;
                
                Venta.ListaClientes = VentaDatos.GetCatClientes(Venta);

                return Content(Venta.ListaClientes.ToJSON(), "application/json");
            }
            catch
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error. Por favor contacte a soporte técnico";
                return Json("");
            }
        }

        public ActionResult ModalListaPrecios(string id_venta)
        {
            _Venta2_Datos Datos = new _Venta2_Datos();
            ViewBag.ListaRangoPeso = Datos.GetListadoPrecioRangoPeso(id_venta, Conexion);

            return PartialView("ModalListaPrecios");
        }

        #region Comprobante venta
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Id venta</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ComprobanteVenta(string id)
        {
            try
            {
                Reporte_Datos R = new Reporte_Datos();
                List<ComprobanteVentaDetallesModels> ListaComprobanteVentaDetalles = new List<ComprobanteVentaDetallesModels>();
                List<ComprobanteVentaPagosModels> ListaComprobanteVentaPagosDetalles = new List<ComprobanteVentaPagosModels>();

                _Venta2_Datos Datos = new _Venta2_Datos();
                VentaModels2 Venta = new VentaModels2();
                ComprobanteVentaCabeceraModels Cabecera = new ComprobanteVentaCabeceraModels();
                Venta.Id_venta = id;
                Venta.Conexion = Conexion;
                Cabecera = Datos.GetComprobanteVentaCabecera(Venta);
                ListaComprobanteVentaDetalles = Datos.GetComprobanteVentaDetalles(Venta);
                ListaComprobanteVentaPagosDetalles = Datos.GetComprobanteVentaDetallesPagos(Venta);

                LocalReport Rtp = new LocalReport();
                Rtp.EnableExternalImages = true;
                Rtp.DataSources.Clear();
                string path = Path.Combine(Server.MapPath("~/Formatos"), "ComprobanteVenta.rdlc");
                if (System.IO.File.Exists(path))
                {
                    Rtp.ReportPath = path;
                }
                else
                {
                    return RedirectToAction("Index", "Venta");
                }
                ReportParameter[] Parametros = new ReportParameter[8];
                Parametros[0] = new ReportParameter("urlLogo", Cabecera.LogoEmpresa);
                Parametros[1] = new ReportParameter("nombreEmpresa", Cabecera.NombreEmpresa);
                Parametros[2] = new ReportParameter("rubroEmpresa", Cabecera.RubroEmpresa);
                Parametros[3] = new ReportParameter("direccionEmpresa", Cabecera.DireccionEmpresa);
                Parametros[4] = new ReportParameter("folio", Cabecera.Folio);
                //Parametros[6] = new ReportParameter("nombreProveedor", Cabecera.NombreProveedor);
                //Parametros[7] = new ReportParameter("telefonoProveedor", Cabecera.TelefonoProveedor);
                //Parametros[8] = new ReportParameter("rfcProveedor", Cabecera.RFCProveedor);
                Parametros[5] = new ReportParameter("diaImpresion", Cabecera.DiaImpresion);
                Parametros[6] = new ReportParameter("mesImpresion", Cabecera.MesImpresion);
                Parametros[7] = new ReportParameter("annoImpresion", Cabecera.AnnoImpresion);

                Rtp.SetParameters(Parametros);
                Rtp.DataSources.Add(new ReportDataSource("ListaDetalles", ListaComprobanteVentaDetalles));
                Rtp.DataSources.Add(new ReportDataSource("ListaDetallesPagos", ListaComprobanteVentaPagosDetalles));

                string reportType = "PDF";
                string mimeType;
                string encoding;
                string fileNameExtension;

                string deviceInfo = "<DeviceInfo>" +
                "  <OutputFormat>ComprobanteVenta</OutputFormat>" +
                "</DeviceInfo>";

                Warning[] warnings;
                string[] streams;
                byte[] renderedBytes;

                renderedBytes = Rtp.Render(
                    reportType,
                    deviceInfo,
                    out mimeType,
                    out encoding,
                    out fileNameExtension,
                    out streams,
                    out warnings);

                return File(renderedBytes, mimeType);
            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista, error: " + Mensaje;
                return View("Index");

            }
        }
        #endregion

        #region DatosEnvioCorreo

        [HttpGet]
        public ActionResult VentaEnvioCorreo(string Id_1)
        {
            try
            {
                if (string.IsNullOrEmpty(Id_1))
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "No se puede cargar la vista, verifique sus datos.";
                    return View("Index");
                }
                if (Id_1.Length != 36)
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "No se puede cargar la vista, verifique sus datos.";
                    return View("Index");
                }

                Token.SaveToken();
                VentaEnvioCorreo VentaCorreo = new VentaEnvioCorreo();
                _Venta2_Datos VentaDatos = new _Venta2_Datos();
                VentaCorreo.Conexion = Conexion;
                VentaCorreo.IdVenta = Id_1;
                VentaCorreo = VentaDatos.GetVentaEnvioCorreo(VentaCorreo);
                VentaCorreo = VentaDatos.ObtenerComboCorreoClientesContacto(VentaCorreo);
               
                return View(VentaCorreo);
            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                TempData["typemessage"] = "2";
                TempData["message"] = "Verifique sus datos, error: " + Mensaje;
                return View("Index");
            }
        }

        [HttpPost]
        public ActionResult VentaEnvioCorreo(VentaEnvioCorreo Venta)
        {
            try
            {
                ConfiguracionModels Configuracion = new ConfiguracionModels();
                _Configuracion_Datos ConfiguracionDatos = new _Configuracion_Datos();
                Configuracion.Conexion = Conexion;
                Configuracion.idTicket = 1;
                Configuracion = ConfiguracionDatos.ObtenerTicket(Configuracion);

                int pesoHembra = 0, PesoMacho = 0, TotalGeneral = 0;
                pesoHembra = Convert.ToInt32(Venta.PesoHembra);
                PesoMacho = Convert.ToInt32(Venta.PesoMachos);
                TotalGeneral = Convert.ToInt32(Venta.TotalGeneral);
                Comun.EnviarCorreo(
                    Configuracion.CorreoTxt
                   , Configuracion.Password
                   , Venta.Correo
                   , Venta.Asunto
                   , Comun.GenerarHtmlCorreoJaula(Venta.FechaEmbarque, Venta.ProveedorVenta, Venta.NombreChofer, Venta.ChoferAuxiliar, Venta.CabezaHembras, pesoHembra, Venta.CabezaMachos,
                                                  PesoMacho, TotalGeneral, Venta.LugarDestino, Venta.TelefonoMovil, Venta.Modelo, Venta.Color, Venta.Placas, Venta.GPS, Venta.PlacasJaulas, Venta.ColorJaula, Venta.Marca)
                   , false
                   , ""
                   , Configuracion.HtmlTxt
                   , Configuracion.HostTxt
                   , Convert.ToInt32(Configuracion.PortTxt)
                   , Configuracion.EnableSslTxt);

                TempData["typemessage"] = "1";
                TempData["message"] = "El correo fue enviado con exito.";
                return View("Index");
            }
            catch (Exception)
            {

                throw;
            }
        }
            #endregion
    }
}
