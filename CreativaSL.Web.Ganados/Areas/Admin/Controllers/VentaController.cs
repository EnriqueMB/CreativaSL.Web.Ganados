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
        public ActionResult DatatableGanadoVendidoVivo(string Id_venta, string Id_eventoVenta)
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
        public ActionResult DatatableGanadoConEvento(string Id_venta, string Id_eventoVenta)
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
                TempData["message"] = "No se pudo cambiar el estatus de la venta.";
            }

            return RedirectToAction("Index");
        }
        #endregion
        #endregion

        // GET: Admin/Venta
        public ActionResult Index()
        {
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
        public ActionResult VentaGanado(string ListaIDGanadosParaVender, string IDVenta)
        {
            try
            {
                if (Token.IsTokenValid())
                {
                    _Venta2_Datos VentaDatos = new _Venta2_Datos();
                    VentaModels2 Venta = new VentaModels2();
                    Venta.Conexion = Conexion;
                    Venta.Usuario = User.Identity.Name;
                    Venta.ListaIDGanadosParaVender = ListaIDGanadosParaVender;
                    Venta.Id_venta = IDVenta;
                    Venta.RespuestaAjax = VentaDatos.AC_Ganado(Venta);

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
        public ActionResult VentaEvento(string IDVenta, string Id_eventoVenta)
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
                        if(string.IsNullOrEmpty(Venta.EventoVenta.ImagenBase64))
                        {
                            Venta.EventoVenta.ImagenMostrar = Auxiliar.SetDefaultImage();
                        }
                        else
                        {
                            Venta.EventoVenta.ImagenMostrar = Venta.EventoVenta.ImagenBase64;
                        }
                        Venta.EventoVenta.ExtensionImagenBase64 = Auxiliar.ObtenerExtensionImagenBase64(Venta.EventoVenta.ImagenMostrar);
                        //aqui pondriamos alguna lista o valores de cargar si esta todo correcto
                        Venta.EventoVenta.ListaDeTiposDeduccion = VentaDatos.GetTiposDeduccion(Venta);
                        Venta.EventoVenta.ListaTiposEventos = VentaDatos.GetTiposEventos(Venta);
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
                    if(Evento.HttpImagen != null)
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
        public ActionResult Del_Evento(string IDVenta, string Id_eventoVenta)
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
                //0 = nuevo, 36 = edit, si es diferente es un id no valido
                if (Id_venta.Length == 36)
                {
                    _Venta2_Datos VentaDatos = new _Venta2_Datos();
                    VentaModels2 Venta = new VentaModels2();
                    Venta.Id_venta = Id_venta;
                    Venta.Conexion = Conexion;
                    Venta.RespuestaAjax = new RespuestaAjax();
                    Venta = VentaDatos.GetVentaDocumentos(Venta);
                    if (Venta.RespuestaAjax.Success)
                    {
                        ViewBag.Id_venta = Id_venta;
                        ViewBag.Id_flete = Venta.Id_flete;
                        ViewBag.CobrarFlete = Venta.CobrarFlete;
                        return View();
                    }
                    else
                    {
                        TempData["typemessage"] = "2";
                        TempData["message"] = Venta.RespuestaAjax.Mensaje;
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
                Venta.Id_flete = id;
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
        #endregion
    }
}
