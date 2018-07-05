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
                VentaModels2 venta = new VentaModels2();
                venta.RespuestaAjax.Mensaje = ex.ToString();
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
                venta.Id_venta = Id_venta;
                venta.RespuestaAjax.Mensaje = ventaDatos.DatatableGanadoParaVenta(venta);
                venta.RespuestaAjax.Success = true;

                return Content(venta.RespuestaAjax.Mensaje, "application/json");
            }
            catch (Exception ex)
            {
                VentaModels2 venta = new VentaModels2();
                venta.RespuestaAjax = new RespuestaAjax();
                venta.RespuestaAjax.Mensaje = ex.Message;
                venta.RespuestaAjax.Success = false;
                return Content(venta.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        [HttpPost]
        public ActionResult DatatableGanadoVendidoVivo(string Id_venta)
        {
            try
            {
                VentaModels2 venta = new VentaModels2();
                _Venta2_Datos ventaDatos = new _Venta2_Datos();
                venta.Conexion = Conexion;

                venta.RespuestaAjax = new RespuestaAjax();
                venta.Id_venta = Id_venta;
                venta.RespuestaAjax.Mensaje = ventaDatos.DatatableGanadoVendidoVivo(venta);
                venta.RespuestaAjax.Success = true;

                return Content(venta.RespuestaAjax.Mensaje, "application/json");
            }
            catch (Exception ex)
            {
                VentaModels2 venta = new VentaModels2();
                venta.RespuestaAjax = new RespuestaAjax();
                venta.RespuestaAjax.Mensaje = ex.Message;
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
                VentaModels2 venta = new VentaModels2();
                venta.RespuestaAjax = new RespuestaAjax();
                venta.RespuestaAjax.Mensaje = ex.Message;
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
                VentaModels2 venta = new VentaModels2();
                venta.RespuestaAjax = new RespuestaAjax();
                venta.RespuestaAjax.Mensaje = ex.Message;
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
                TempData["message"] = "No se puede cargar la vista, error: " + ex.Message;
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
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error al intentar guardar los datos. Contacte a soporte técnico.";
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
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista, error: " + ex.Message;
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
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error al intentar guardar los datos. Contacte a soporte técnico. Error: " + ex.Message;
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
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista, error: " + ex.Message;
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
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error al intentar guardar los datos. Contacte a soporte técnico: " + ex.Message;
                Venta.RespuestaAjax = new RespuestaAjax();
                Venta.RespuestaAjax.Success = false;

                return RedirectToAction("Index");
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
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista, error: " + ex.Message;
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

                    Evento.RespuestaAjax = new RespuestaAjax();
                    Evento.RespuestaAjax.Success = false;

                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error al intentar guardar los datos. Contacte a soporte técnico: " + ex.Message;
                Evento.RespuestaAjax = new RespuestaAjax();
                Evento.RespuestaAjax.Success = false;

                return RedirectToAction("Index");
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
        #endregion

        #region Modal evento
        [HttpPost]
        public ActionResult ModalEvento()
        {
            return PartialView("ModalEvento");
        }
        #endregion
    }
}
