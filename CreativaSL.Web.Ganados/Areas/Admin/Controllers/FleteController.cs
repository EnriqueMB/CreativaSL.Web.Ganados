using CreativaSL.Web.Ganados.App_Start;
using CreativaSL.Web.Ganados.Filters;
using CreativaSL.Web.Ganados.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using System.Web.UI.WebControls;
using System.IO;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    [Autorizado]
    public class FleteController : Controller
    {
        private string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
        private TokenProcessor Token = TokenProcessor.GetInstance();
        private FleteModels Flete;
        private _Flete_Datos FleteDatos;

        // GET: Admin/Flete
        public ActionResult Index()
        {
            try
            {
                Flete = new FleteModels();
                Token.SaveToken();
                return View(Flete);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /********************************************************************/
        //Funcion Json Table
        #region Funcion index Json
        [HttpPost]
        public ActionResult JsonIndex()
        {
            try
            {
                Flete = new FleteModels();
                FleteDatos = new _Flete_Datos();
                Flete.Conexion = Conexion;

                Flete.RespuestaAjax.Mensaje = FleteDatos.GetFleteIndexDataTable(Flete);
                Flete.RespuestaAjax.Success = true;

                return Content(Flete.RespuestaAjax.Mensaje, "application/json");

            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                Flete.RespuestaAjax.Mensaje = Mensaje;
                Flete.RespuestaAjax.Success = false;
                return Content(Flete.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        #endregion
        #region Funcion Json Documentos
        [HttpPost]
        public ActionResult TableJsonDocumentos(string Id_flete)
        {
            try
            {
                Flete = new FleteModels();
                FleteDatos = new _Flete_Datos();
                Flete.Conexion = Conexion;
                Flete.id_flete = Id_flete;

                Flete.RespuestaAjax.Mensaje = FleteDatos.GetDocumentosDataTable(Flete);
                Flete.RespuestaAjax.Success = true;

                return Content(Flete.RespuestaAjax.Mensaje, "application/json");

            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                Flete.RespuestaAjax.Mensaje = Mensaje;
                Flete.RespuestaAjax.Success = false;
                return Content(Flete.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        #endregion
        #region Funcion Json GanadoActual
        [HttpPost]
        public ActionResult TableJsonGanadoActual()
        {
            try
            {
                Flete = new FleteModels();
                FleteDatos = new _Flete_Datos();
                Flete.Conexion = Conexion;

                Flete.RespuestaAjax.Mensaje = FleteDatos.GetGanadoDataTable(Flete);
                Flete.RespuestaAjax.Success = true;

                return Content(Flete.RespuestaAjax.Mensaje, "application/json");

            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                Flete.RespuestaAjax.Mensaje = Mensaje;
                Flete.RespuestaAjax.Success = false;
                return Content(Flete.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        #endregion
        #region Funcion Json GanadoActualXIDFlete
        [HttpPost]
        public ActionResult TableJsonProductoGanadoXIDFlete(string IDFlete)
        {
            try
            {
                Flete = new FleteModels();
                FleteDatos = new _Flete_Datos();
                Flete.Conexion = Conexion;
                Flete.id_flete = IDFlete;
                Flete.RespuestaAjax.Mensaje = FleteDatos.GetProductoGanadoXIDFlete(Flete);
                Flete.RespuestaAjax.Success = true;

                return Content(Flete.RespuestaAjax.Mensaje, "application/json");

            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                Flete.RespuestaAjax.Mensaje = Mensaje;
                Flete.RespuestaAjax.Success = false;
                return Content(Flete.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        #endregion
        #region Funcion Json GanadoActualXIDFlete
        [HttpPost]
        public ActionResult TableJsonProductoGanadoNOPropioXIDFlete(string IDFlete)
        {
            try
            {
                Flete = new FleteModels();
                FleteDatos = new _Flete_Datos();
                Flete.Conexion = Conexion;
                Flete.id_flete = IDFlete;
                Flete.RespuestaAjax.Mensaje = FleteDatos.GetProductoGanadoNOPropioXIDFlete(Flete);
                Flete.RespuestaAjax.Success = true;

                return Content(Flete.RespuestaAjax.Mensaje, "application/json");

            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                Flete.RespuestaAjax.Mensaje = Mensaje;
                Flete.RespuestaAjax.Success = false;
                return Content(Flete.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        #endregion
        #region Funcion Json Evento
        [HttpPost]
        public ActionResult TableJsonFleteEventos(string IDFlete)
        {
            try
            {
                Flete = new FleteModels();
                FleteDatos = new _Flete_Datos();
                Flete.Conexion = Conexion;
                Flete.id_flete = IDFlete;
                Flete.RespuestaAjax.Mensaje = FleteDatos.GetEventoXIDFlete(Flete);
                Flete.RespuestaAjax.Success = true;

                return Content(Flete.RespuestaAjax.Mensaje, "application/json");

            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                Flete.RespuestaAjax.Mensaje = Mensaje;
                Flete.RespuestaAjax.Success = false;
                return Content(Flete.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        #endregion
        #region Funcion Json GanadoSinAccidenteXIDEvento
        [HttpPost]
        public ActionResult TableJsonProductoGanadoCargadoLibreEvento(string IDFlete, string IDEvento)
        {
            try
            {
                EventoEnvioModels Evento = new EventoEnvioModels();
                FleteDatos = new _Flete_Datos();
                Evento.Conexion = Conexion;
                Evento.IDEvento = int.Parse(IDEvento);
                Evento.IDEnvio = IDFlete;
                Evento.RespuestaAjax = new RespuestaAjax();
                Evento.RespuestaAjax.Mensaje = Auxiliar.SqlReaderToJson(FleteDatos.GetProductoGanadoNoAccidentadoXIDEvento(Evento));
                Evento.RespuestaAjax.Success = true;

                return Content(Evento.RespuestaAjax.Mensaje, "application/json");

            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                Flete.RespuestaAjax.Mensaje = Mensaje;
                Flete.RespuestaAjax.Success = false;
                return Content(Flete.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        #endregion
        #region Funcion Json GanadoConAccidenteXIDEvento
        [HttpPost]
        public ActionResult TableJsonProductoGanadoCargadoConEvento(string IDFlete, string IDEvento)
        {
            try
            {
                EventoEnvioModels Evento = new EventoEnvioModels();
                FleteDatos = new _Flete_Datos();
                Evento.Conexion = Conexion;
                Evento.IDEvento = int.Parse(IDEvento);
                Evento.IDEnvio = IDFlete;
                Evento.RespuestaAjax = new RespuestaAjax();
                Evento.RespuestaAjax.Mensaje = Auxiliar.SqlReaderToJson(FleteDatos.GetProductoGanadoAccidentadoXIDEvento(Evento));
                Evento.RespuestaAjax.Success = true;

                return Content(Evento.RespuestaAjax.Mensaje, "application/json");

            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                Flete.RespuestaAjax.Mensaje = Mensaje;
                Flete.RespuestaAjax.Success = false;
                return Content(Flete.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        #endregion
        #region Json Flete impuesto
        [HttpPost]
        public ContentResult TableJsonFleteImpuesto(string IDFlete)
        {
            FleteImpuestoModels FleteImpuesto = new FleteImpuestoModels();
            _FleteImpuesto_Datos FleteImpuestoDatos = new _FleteImpuesto_Datos();
            FleteImpuesto.Conexion = Conexion;
            FleteImpuesto.IDFlete = IDFlete;
            FleteImpuesto.RespuestaAjax.Mensaje = FleteImpuestoDatos.GetJsonTableFleteImpuestoXIDFlete(FleteImpuesto);

            return Content(FleteImpuesto.RespuestaAjax.Mensaje, "application/json");
        }
        #endregion
        #region Json Flete impuesto x id documento detalle
        [HttpPost]
        public ContentResult TableJsonFleteImpuestoXIdDocDetalle(string Id1, string Id2)
        {
            FleteImpuestoModels FleteImpuesto = new FleteImpuestoModels();
            _FleteImpuesto_Datos FleteImpuestoDatos = new _FleteImpuesto_Datos();
            FleteImpuesto.Conexion = Conexion;
            FleteImpuesto.IDFlete = Id1;
            FleteImpuesto.Id_detalleDoctoCobrar = Id2;
            FleteImpuesto.RespuestaAjax.Mensaje = FleteImpuestoDatos.GetJsonTableFleteImpuestoXIDDocumentoDetalle(FleteImpuesto);

            return Content(FleteImpuesto.RespuestaAjax.Mensaje, "application/json");
        }
        #endregion
        #region DatatableDetallesDocumentoPorCobrarFlete
        [HttpPost]
        public ContentResult DatatableDetallesDocumentoPorCobrarFlete(string Id_flete, string Id_documentoCobrar)
        {
            try
            {
                Flete = new FleteModels();
                FleteDatos = new _Flete_Datos();
                Flete.Conexion = Conexion;
                Flete.id_flete = Id_flete;
                Flete.Id_documentoPorCobrar = Id_documentoCobrar;
                Flete.RespuestaAjax.Mensaje = FleteDatos.DatatableDetallesDocumentoPorCobrarFlete(Flete);

                return Content(Flete.RespuestaAjax.Mensaje, "application/json");

            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                Flete.RespuestaAjax.Mensaje = Mensaje;
                return Content(Flete.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        #endregion
        #region DatatableDetallesDocumentoPorCobrarFlete
        [HttpPost]
        public ContentResult DatatableDetallesDocumentoPorCobrarFleteDeduccions(string Id_flete, string Id_documentoCobrar)
        {
            try
            {
                Flete = new FleteModels();
                FleteDatos = new _Flete_Datos();
                Flete.Conexion = Conexion;
                Flete.id_flete = Id_flete;
                Flete.Id_documentoPorCobrar = Id_documentoCobrar;
                Flete.RespuestaAjax.Mensaje = FleteDatos.DatatableDetallesDocumentoPorCobrarFleteDeduccions(Flete);

                return Content(Flete.RespuestaAjax.Mensaje, "application/json");

            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                Flete.RespuestaAjax.Mensaje = Mensaje;
                return Content(Flete.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        #endregion
        #region DatatableDetallesDocumentoPorCobrarFleteCobros
        [HttpPost]
        public ContentResult DatatableDetallesDocumentoPorCobrarFleteCobros(string Id_flete, string Id_documentoCobrar)
        {
            try
            {
                Flete = new FleteModels();
                FleteDatos = new _Flete_Datos();
                Flete.Conexion = Conexion;
                Flete.id_flete = Id_flete;
                Flete.Id_documentoPorCobrar = Id_documentoCobrar;
                Flete.RespuestaAjax.Mensaje = FleteDatos.DatatableDetallesDocumentoPorCobrarFleteCobro(Flete);

                return Content(Flete.RespuestaAjax.Mensaje, "application/json");

            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                Flete.RespuestaAjax.Mensaje = Mensaje;
                return Content(Flete.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        #endregion
        [HttpPost]
        public ActionResult DatatableGanadoEnFlete(string Id_flete, int Id_eventoFlete)
        {
            try
            {
                _Flete_Datos FleteDatos = new _Flete_Datos();
 
                EventoFleteModels EventoFlete = new EventoFleteModels();
                EventoFlete.RespuestaAjax = new RespuestaAjax();
                EventoFlete.Conexion = Conexion;
                EventoFlete.Id_flete = Id_flete;
                EventoFlete.Id_eventoFlete = Id_eventoFlete;
                EventoFlete.RespuestaAjax.Mensaje = FleteDatos.DatatableGanadoEnFlete(EventoFlete);
                EventoFlete.RespuestaAjax.Success = true;

                return Content(EventoFlete.RespuestaAjax.Mensaje, "application/json");
            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                EventoFleteModels EventoFlete = new EventoFleteModels();
                EventoFlete.RespuestaAjax = new RespuestaAjax();
                EventoFlete.RespuestaAjax.Mensaje = Mensaje;
                EventoFlete.RespuestaAjax.Success = false;
                return Content(EventoFlete.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        [HttpPost]
        public ActionResult DatatableGanadoConEvento(string Id_flete, int Id_eventoFlete)
        {
            try
            {
                _Flete_Datos FleteDatos = new _Flete_Datos();
                EventoFleteModels EventoFlete = new EventoFleteModels();
                EventoFlete.RespuestaAjax = new RespuestaAjax();
                EventoFlete.Id_flete = Id_flete;
                EventoFlete.Conexion = Conexion;
                EventoFlete.Id_eventoFlete = Id_eventoFlete;
                EventoFlete.RespuestaAjax.Mensaje = FleteDatos.DatatableGanadoConEvento(EventoFlete);
                EventoFlete.RespuestaAjax.Success = true;

                return Content(EventoFlete.RespuestaAjax.Mensaje, "application/json");
            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                EventoFleteModels EventoFlete = new EventoFleteModels();
                EventoFlete.RespuestaAjax = new RespuestaAjax();
                EventoFlete.RespuestaAjax.Mensaje = Mensaje;
                EventoFlete.RespuestaAjax.Success = false;
                return Content(EventoFlete.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        [HttpGet]
        public ActionResult AC_Flete(string IDFlete, int? opcion)
        {
            Token.SaveToken();
            Flete = new FleteModels
            {
                Conexion = Conexion
            };
            FleteDatos = new _Flete_Datos();
            //cargarmos los datos del flete x id
            if (!string.IsNullOrEmpty(IDFlete) && IDFlete.Length == 36)
            {
                Flete.id_flete = IDFlete;
                ViewBag.OpcionImpuesto = opcion;
                Flete = FleteDatos.Flete_get_ACFlete(Flete);
            }
            Flete.ListaEmpresa = FleteDatos.GetListadoEmpresas(Flete);
            Flete.ListaCliente = FleteDatos.GetListadoClientes(Flete);
            Flete.ListaChofer = FleteDatos.GetListadoChoferes(Flete);
            Flete.ListaVehiculo = FleteDatos.GetListadoVehiculos(Flete);
            Flete.Trayecto.ListaRemitente = FleteDatos.GetListadoClientes(Flete);
            Flete.Trayecto.ListaDestinatario = FleteDatos.GetListadoClientes(Flete);
            Flete.Trayecto.ListaLugarOrigen = FleteDatos.GetListadoLugaresXIDProveedorIDCliente(Flete, Flete.Trayecto.Remitente.IDCliente);
            Flete.Trayecto.ListaLugarDestino = FleteDatos.GetListadoLugaresXIDProveedorIDCliente(Flete, Flete.Trayecto.Destinatario.IDCliente);
            Flete.ListaFormaPago = FleteDatos.GetListadoFormaPagos(Flete);
            Flete.ListaMetodoPago = FleteDatos.GetListadoMetodoPago(Flete);
            Flete.ListaSucursales = FleteDatos.GetListadoSucursales(Flete);

            return View(Flete);
        }
        [HttpGet]
        public ActionResult AC_FleteRecepcion(string IDFlete)
        {
            Token.SaveToken();
            Flete = new FleteModels
            {
                Conexion = Conexion
            };
            FleteDatos = new _Flete_Datos();
            Flete.RecepcionDestino = new RecepcionDestinoModels();
            Flete.RecepcionOrigen = new RecepcionOrigenModels();
            Flete.RecepcionOrigen.Inicializar();
            Flete.RecepcionDestino.Inicializar();

            if (!string.IsNullOrEmpty(IDFlete) && IDFlete.Length == 36)
            {
                Flete.id_flete = IDFlete;
                Flete = FleteDatos.Flete_get_Recepcion(Flete);
            }
            Flete.RecepcionDestino.IDFlete = IDFlete;
            Flete.RecepcionOrigen.IDFlete = IDFlete;

            return View(Flete);
        }
        [HttpGet]
        public ActionResult Edit(string IDFlete)
        {
            Token.SaveToken();
            Flete = new FleteModels();
            FleteDatos = new _Flete_Datos();
            Flete.Conexion = Conexion;
            Flete.id_flete = IDFlete;
            Flete = FleteDatos.GetEstatusFleteXIDFlete(Flete);

            switch (Flete.Estatus)
            {
                case 0:
                    return RedirectToAction("AC_Flete", "Flete", new { IDFlete = Flete.id_flete });
                case 1:
                    return RedirectToAction("AC_FleteRecepcion", "Flete", new { IDFlete = Flete.id_flete });
                default:
                    return View(Flete);
            }
        }
        [HttpGet]
        public ActionResult EnviarFlete(string IDFlete)
        {
            if (IDFlete.Length == 36)
            {
                Flete = new FleteModels();
                FleteDatos = new _Flete_Datos();
                //Asigno valores para los querys
                Flete.Conexion = Conexion;
                Flete.id_flete = IDFlete;
                Flete.Usuario = User.Identity.Name;
                //Paso al siguiente paso
                Flete = FleteDatos.Flete_a_CambiarEstatusFleteXIDFlete(Flete);

                if (Flete.RespuestaAjax.Success)
                {
                    TempData["typemessage"] = "1";
                }
                else
                {
                    TempData["typemessage"] = "2";
                }

                TempData["message"] = Flete.RespuestaAjax.Mensaje;

                return RedirectToAction("Index", "Flete");
            }
            else
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Flete no válido.";
                return RedirectToAction("Index", "Flete");
            }
        }

        #region Vista Flete impuesto
        [HttpGet]
        public ActionResult AC_FleteImpuestos(string IDFlete, string IDFleteImpuesto)
        {
            try
            {
                Token.SaveToken();

                string Id_flete = string.IsNullOrEmpty(IDFlete) ? string.Empty : IDFlete;

                if (string.IsNullOrEmpty(Id_flete))
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Verifique sus datos.";
                    return View("Index");
                }
                else
                {
                    string Id_fleteImpuesto = string.IsNullOrEmpty(IDFleteImpuesto) ? string.Empty : IDFleteImpuesto;

                    if (Id_fleteImpuesto.Length == 0 || Id_fleteImpuesto.Length == 36)
                    {
                        FleteImpuestoModels FleteImpuesto = new FleteImpuestoModels();
                        _FleteImpuesto_Datos FleteImpuestoDatos = new _FleteImpuesto_Datos();

                        FleteImpuesto.Conexion = Conexion;
                        FleteImpuesto.IDFleteImpuesto = IDFleteImpuesto;
                        FleteImpuesto.IDFlete = IDFlete;
                        FleteImpuesto.Conexion = Conexion;
                        FleteImpuesto = FleteImpuestoDatos.GetFleteImpuestoXIDFleteImpuesto(FleteImpuesto);

                        if (FleteImpuesto.RespuestaAjax.Success)
                        {
                            FleteImpuesto.ListaImpuesto = FleteImpuestoDatos.GetListadoImpuesto(FleteImpuesto);
                            FleteImpuesto.ListaTipoImpuesto = FleteImpuestoDatos.GetListadoTipoImpuesto(FleteImpuesto);
                            FleteImpuesto.ListaTipoFactor = FleteImpuestoDatos.GetListadoTipoFactor(FleteImpuesto);
                        }
                        else
                        {
                            TempData["typemessage"] = "2";
                            TempData["message"] = "No se puede cargar la vista, error: " + FleteImpuesto.RespuestaAjax.Mensaje;
                            return View("Index");
                        }

                        return View(FleteImpuesto);
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
        public ActionResult AC_FleteImpuestos(FleteImpuestoModels FleteImpuesto)
        {
            try
            {
                if (FleteImpuesto.IDFlete.Length == 36)
                {
                    if (Token.IsTokenValid())
                    {
                         _FleteImpuesto_Datos FleteImpuestoDatos;
                        FleteImpuestoDatos = new _FleteImpuesto_Datos();
                        FleteImpuesto.Conexion = Conexion;
                        FleteImpuesto.Usuario = User.Identity.Name;
                        FleteImpuesto = FleteImpuestoDatos.FleteImpuesto_ac_FleteImpuesto(FleteImpuesto);

                        if (FleteImpuesto.RespuestaAjax.Success)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = FleteImpuesto.RespuestaAjax.Mensaje;
                            return RedirectToAction("AC_Flete", "Flete", new { IDFlete = FleteImpuesto.IDFlete, opcion = 1 });
                        }
                        else
                        {
                            TempData["typemessage"] = "2";
                            TempData["message"] = FleteImpuesto.RespuestaAjax.Mensaje;
                            return RedirectToAction("Index", "Flete");
                        }
                    }
                    else
                    {
                        TempData["typemessage"] = "2";
                        TempData["message"] = "Flete no válido.";
                        return RedirectToAction("Index", "Flete");
                    }
                   
                }
                else
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Flete no válido.";
                    return RedirectToAction("Index", "Flete");
                }
            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                FleteImpuesto.RespuestaAjax.Mensaje = Mensaje;
                FleteImpuesto.RespuestaAjax.Success = false;
                return RedirectToAction("Index", "Flete");
            }
        }
        [HttpPost]
        public ActionResult DEL_FleteImpuesto(FleteImpuestoModels FleteImpuesto)
        {
            try
            {
                if (FleteImpuesto.IDFleteImpuesto.Length == 36)
                {
                    if (Token.IsTokenValid())
                    {
                        _FleteImpuesto_Datos FleteImpuestoDatos = new _FleteImpuesto_Datos();
                        FleteImpuesto.Conexion = Conexion;
                        FleteImpuesto.Usuario = User.Identity.Name;
                        FleteImpuesto = FleteImpuestoDatos.FleteImpuesto_del_FleteImpuesto(FleteImpuesto);

                        if(FleteImpuesto.RespuestaAjax.Success)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = FleteImpuesto.RespuestaAjax.Mensaje;
                            return Content(FleteImpuesto.RespuestaAjax.ToJSON(), "application/json");
                        }
                        else
                        {
                            TempData["typemessage"] = "2";
                            TempData["message"] = FleteImpuesto.RespuestaAjax.Mensaje;
                            return Content(FleteImpuesto.RespuestaAjax.ToJSON(), "application/json");
                        }
                    }
                    else
                    {
                        FleteImpuesto.RespuestaAjax.Success = false;
                        TempData["typemessage"] = "2";
                        TempData["message"] = "Impuesto no válido.";
                        return Content(FleteImpuesto.RespuestaAjax.ToJSON(), "application/json");
                    }
                }
                else
                {
                    FleteImpuesto.RespuestaAjax.Success = false;
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Impuesto no válido.";
                    return Content(FleteImpuesto.RespuestaAjax.ToJSON(), "application/json");
                }
            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                FleteImpuesto.RespuestaAjax.Success = false;
                TempData["typemessage"] = "2";
                TempData["message"] = "Contacte con soporte técnico, error: " + Mensaje;
                return Content(FleteImpuesto.RespuestaAjax.ToJSON(), "application/json");
            }
        }

        #endregion
        
        #region Vista Evento
        [HttpGet]
        public ActionResult AC_FleteEvento(string IDFlete, int Id_eventoFlete)
        {
            try
            {
                Token.SaveToken();
                _Flete_Datos FleteDatos = new _Flete_Datos();
                EventoFleteModels EventoFlete = new EventoFleteModels();
                EventoFlete.RespuestaAjax = new RespuestaAjax();

                string Id_flete = string.IsNullOrEmpty(IDFlete) ? string.Empty : IDFlete;
                //0 = nuevo, 36 = edit, si es diferente es un id no valido
                if (Id_flete.Length == 0 || Id_flete.Length == 36)
                {
                    EventoFlete.Conexion = Conexion;
                    EventoFlete.Id_flete = Id_flete;
                    EventoFlete.Id_eventoFlete = Id_eventoFlete;
                    EventoFlete = FleteDatos.GetFleteEvento(EventoFlete);
                    if (EventoFlete.RespuestaAjax.Success)
                    {
                        if (string.IsNullOrEmpty(EventoFlete.ImagenBase64))
                        {
                            EventoFlete.ImagenMostrar = Auxiliar.SetDefaultImage();
                        }
                        else
                        {
                            EventoFlete.ImagenMostrar = EventoFlete.ImagenBase64;
                        }
                        EventoFlete.ExtensionImagenBase64 = Auxiliar.ObtenerExtensionImagenBase64(EventoFlete.ImagenMostrar);
                        //aqui pondriamos alguna lista o valores de cargar si esta todo correcto
                        EventoFlete.ListaDeTiposDeduccion = FleteDatos.GetTiposDeduccion(EventoFlete);
                        EventoFlete.ListaTiposEventos = FleteDatos.GetTiposEventos(EventoFlete);
                        return View(EventoFlete);
                    }
                    else
                    {
                        TempData["typemessage"] = "2";
                        TempData["message"] = "No se puede cargar la vista, error: " + EventoFlete.RespuestaAjax.Mensaje;
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
        public ActionResult AC_FleteEvento(EventoFleteModels EventoFlete)
        {
            try
            {
                if (Token.IsTokenValid())
                {
                    _Flete_Datos FleteDatos = new _Flete_Datos();
                    EventoFlete.Conexion = Conexion;
                    EventoFlete.Usuario = User.Identity.Name;
                    if (EventoFlete.HttpImagen != null)
                    {
                        EventoFlete.ImagenBase64 = Auxiliar.ImageToBase64(EventoFlete.HttpImagen);
                    }

                    EventoFlete.RespuestaAjax = FleteDatos.AC_Evento(EventoFlete);

                    if (EventoFlete.RespuestaAjax.Success)
                    {
                        TempData["typemessage"] = "1";
                        TempData["message"] = EventoFlete.RespuestaAjax.Mensaje;
                        Token.ResetToken();

                        return Content(EventoFlete.RespuestaAjax.ToJSON(), "application/json");
                    }
                    else
                    {
                        TempData["typemessage"] = "2";
                        TempData["message"] = EventoFlete.RespuestaAjax.Mensaje;

                        return Content(EventoFlete.RespuestaAjax.ToJSON(), "application/json");
                    }
                }
                else
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Verifique sus datos.";

                    EventoFlete.RespuestaAjax = new RespuestaAjax();
                    EventoFlete.RespuestaAjax.Success = false;

                    return Content(EventoFlete.RespuestaAjax.ToJSON(), "application/json");
                }
            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error al intentar guardar los datos. Contacte a soporte técnico: " + Mensaje;
                EventoFlete.RespuestaAjax = new RespuestaAjax();
                EventoFlete.RespuestaAjax.Success = false;

                return Content(EventoFlete.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        #endregion

        #region Vista AC_FleteTransacciones
        [HttpGet]
        public ActionResult AC_FleteTransacciones(string IDFlete)
        {
            try
            {
                Token.SaveToken();
                _Flete_Datos FleteDatos = new _Flete_Datos();

                string Id_flete = string.IsNullOrEmpty(IDFlete) ? string.Empty : IDFlete;
                
                if (Id_flete.Length == 36)
                {
                    TransaccionesFleteModels TransaccionesFlete = new TransaccionesFleteModels();
                    TransaccionesFlete.Conexion = Conexion;
                    TransaccionesFlete.Id_flete = Id_flete;
                    TransaccionesFlete.RespuestaAjax = new RespuestaAjax();
                    TransaccionesFlete = FleteDatos.GetTransacciones(TransaccionesFlete);

                    if (TransaccionesFlete.RespuestaAjax.Success)
                    {
                        return View(TransaccionesFlete);
                    }
                    else
                    {
                        TempData["typemessage"] = "2";
                        TempData["message"] = "No se puede cargar la vista, error: " + TransaccionesFlete.RespuestaAjax.Mensaje;
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

        #region Vista Producto/Servicio
        [HttpGet]
        public ActionResult AC_FleteProductoServicio(string Id_flete, string Id_documentoPorCobrar, string Id_detalleDocumento)
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
                    DocumentoPorCobrarDetalle = FleteDatos.GetDetalleDocumentoPorCobrar(DocumentoPorCobrarDetalle);
                    DocumentoPorCobrarDetalle.ListaTipoClasificacionCobro = FleteDatos.GetListadoTipoClasificacion(DocumentoPorCobrarDetalle);
                    DocumentoPorCobrarDetalle.ListaProductosServiciosCFDI = FleteDatos.GetListadoCFDIProductosServiciosCompra(DocumentoPorCobrarDetalle);

                    DocumentoPorCobrarDetalle.ListaAlmacen = FleteDatos.GetAlmacenesHabilitados(DocumentoPorCobrarDetalle);
                    DocumentoPorCobrarDetalle.ListaProductos = FleteDatos.GetProductosAlmacen(DocumentoPorCobrarDetalle);



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
        public ActionResult AC_FleteProductoServicio(DocumentosPorCobrarDetalleModels DocumentoPorCobrarDetalle)
        {
            try
            {
                if (Token.IsTokenValid())
                {
                    _Flete_Datos FleteDatos = new _Flete_Datos();
                    DocumentoPorCobrarDetalle.Conexion = Conexion;
                    DocumentoPorCobrarDetalle.Usuario = User.Identity.Name;
                    DocumentoPorCobrarDetalle.RespuestaAjax = new RespuestaAjax();
                    DocumentoPorCobrarDetalle = FleteDatos.AC_ProductoServicio_Compra(DocumentoPorCobrarDetalle);

                    if (DocumentoPorCobrarDetalle.RespuestaAjax.Success)
                    {
                        TempData["typemessage"] = "1";
                        TempData["message"] = DocumentoPorCobrarDetalle.RespuestaAjax.Mensaje;
                        Token.ResetToken();
                        return RedirectToAction("AC_FleteTransacciones", "Flete", new { IDFlete = DocumentoPorCobrarDetalle.Id_servicio });
                    }
                    else
                    {
                        TempData["typemessage"] = "2";
                        TempData["message"] = DocumentoPorCobrarDetalle.RespuestaAjax.Mensaje;
                        return RedirectToAction("AC_FleteProductoServicio", "Flete", new { Id_flete = DocumentoPorCobrarDetalle.Id_servicio, Id_documentoPorCobrar = DocumentoPorCobrarDetalle.Id_documentoCobrar, Id_detalleDocumento = DocumentoPorCobrarDetalle.Id_detalleDoctoCobrar });
                    }
                }
                else
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Verifique sus datos.";
                    return RedirectToAction("Index", "Flete");
                }
            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                TempData["typemessage"] = "2";
                TempData["message"] = "Verifique sus datos, error: " + Mensaje;
                return RedirectToAction("Index", "Flete");
            }
        }
        [HttpPost]
        public ActionResult Del_ProductoServicio(DocumentosPorCobrarDetalleModels documento)
        {
            try
            {
                if(Token.IsTokenValid())
                {
                    _Flete_Datos FleteDatos = new _Flete_Datos();
                    documento.Conexion = Conexion;
                    documento.Usuario = User.Identity.Name;
                    documento.RespuestaAjax = new RespuestaAjax();
                    documento = FleteDatos.DEL_ProductoServicioCompra(documento);

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
        #region Del comprobante COBRO
        public ActionResult DelComprobante(DocumentosPorCobrarDetallePagosModels DocumentoPorCobrarPago)
        {
            try
            {

                if (Token.IsTokenValid())
                {
                    DocumentoPorCobrarPago.Usuario = User.Identity.Name;
                    DocumentoPorCobrarPago.Conexion = Conexion;
                    DocumentoPorCobrarPago.RespuestaAjax = new RespuestaAjax();
                    _Flete_Datos FleteDatos = new _Flete_Datos();
                    DocumentoPorCobrarPago = FleteDatos.DEL_ComprobanteCobro(DocumentoPorCobrarPago);

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
        #endregion
        #endregion

        #region Vista impuesto productoServicio
        /// <summary>
        /// Muestra los impuestos de un producto
        /// </summary>
        /// <param name="Id1">Id del flete</param>
        /// <param name="Id2">Id del detalle Documento por Cobrar</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AC_FleteImpuestoProductoServicio(string Id_1, string Id_2)
        {
            try
            {
                FleteImpuestoModels DocumentoPorCobrarDetalleImpuesto = new FleteImpuestoModels();
                _Flete_Datos FleteDatos = new _Flete_Datos();

                if (( Id_1.Length == 36 ) && ( Id_2.Length == 36 ))
                {
                    Token.SaveToken();
                    DocumentoPorCobrarDetalleImpuesto.IDFlete = Id_1;
                    DocumentoPorCobrarDetalleImpuesto.Id_detalleDoctoCobrar = Id_2;
                    DocumentoPorCobrarDetalleImpuesto.Conexion = Conexion;
                    DocumentoPorCobrarDetalleImpuesto.RespuestaAjax = new RespuestaAjax();
                    DocumentoPorCobrarDetalleImpuesto = FleteDatos.Get_AC_FleteImpuestos(DocumentoPorCobrarDetalleImpuesto);

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
        public ActionResult AC_FleteImpuesto_ProductoServicio(string Id1, string Id2 , string Id3)
        {
            try
            {
                Token.SaveToken();

                string Id_flete = string.IsNullOrEmpty(Id1) ? string.Empty : Id1;
                string Id_detalleDocumento = string.IsNullOrEmpty(Id2) ? string.Empty : Id2;

                if (string.IsNullOrEmpty(Id_flete) || string.IsNullOrEmpty(Id_detalleDocumento))
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Verifique sus datos.";
                    return View("Index");
                }
                else
                {
                    string Id_fleteImpuesto = string.IsNullOrEmpty(Id3) ? string.Empty : Id3;

                    if (Id_fleteImpuesto.Length == 0 || Id_fleteImpuesto.Length == 36)
                    {
                        FleteImpuestoModels FleteImpuesto = new FleteImpuestoModels();
                        _FleteImpuesto_Datos FleteImpuestoDatos = new _FleteImpuesto_Datos();

                        FleteImpuesto.Conexion = Conexion;
                        FleteImpuesto.IDFlete = Id1;
                        FleteImpuesto.Id_detalleDoctoCobrar = Id2;
                        FleteImpuesto.IDFleteImpuesto = Id3;

                        FleteImpuesto.Conexion = Conexion;
                        FleteImpuesto = FleteImpuestoDatos.GetFleteImpuestoXIDFleteImpuesto(FleteImpuesto);

                        if (FleteImpuesto.RespuestaAjax.Success)
                        {
                            FleteImpuesto.ListaImpuesto = FleteImpuestoDatos.GetListadoImpuesto(FleteImpuesto);
                            FleteImpuesto.ListaTipoImpuesto = FleteImpuestoDatos.GetListadoTipoImpuesto(FleteImpuesto);
                            FleteImpuesto.ListaTipoFactor = FleteImpuestoDatos.GetListadoTipoFactor(FleteImpuesto);
                        }
                        else
                        {
                            TempData["typemessage"] = "2";
                            TempData["message"] = "No se puede cargar la vista, error: " + FleteImpuesto.RespuestaAjax.Mensaje;
                            return View("Index");
                        }

                        return View(FleteImpuesto);
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
        public ActionResult AC_FleteImpuesto_ProductoServicio(FleteImpuestoModels FleteImpuesto)
        {
            try
            {
                if (Token.IsTokenValid())
                {
                    _Flete_Datos FleteDatos = new _Flete_Datos();
                    FleteImpuesto.Conexion = Conexion;
                    FleteImpuesto.Usuario = User.Identity.Name;
                    FleteImpuesto.RespuestaAjax = new RespuestaAjax();
                    FleteImpuesto = FleteDatos.Flete_ac_ImpuestoProductoServicio(FleteImpuesto);

                    if (FleteImpuesto.RespuestaAjax.Success)
                    {
                        TempData["typemessage"] = "1";
                        TempData["message"] = FleteImpuesto.RespuestaAjax.Mensaje;
                        Token.ResetToken();
                        return RedirectToAction("AC_FleteImpuestoProductoServicio", "Flete", new { Id_1 = FleteImpuesto.IDFlete, Id_2 = FleteImpuesto.Id_detalleDoctoCobrar });
                    }
                    else
                    {
                        TempData["typemessage"] = "2";
                        TempData["message"] = FleteImpuesto.RespuestaAjax.Mensaje;
                        return RedirectToAction("AC_FleteImpuestoProductoServicio", "Flete", new { Id_1 = FleteImpuesto.IDFlete, Id_2 = FleteImpuesto.Id_detalleDoctoCobrar });
                    }
                }
                else
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Verifique sus datos.";
                    return RedirectToAction("Index", "Flete");
                }
            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                TempData["typemessage"] = "2";
                TempData["message"] = "Verifique sus datos, error: " + Mensaje;
                return RedirectToAction("Index", "Flete");
            }
        }
        #endregion

        #region Vista Cobro
        /// <summary>
        /// Vista que actualiza o crea un cobro
        /// </summary>
        /// <param name="id_1">El id del flete</param>
        /// <param name="id_2">El id del documento por cobrar detalle pago</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AC_FleteCobro(string id_1, string id_2)
        {
            try
            {
                Token.SaveToken();
                _Flete_Datos FleteDatos = new _Flete_Datos();
                DocumentosPorCobrarDetallePagosModels DocumentoPorCobrarPago = new DocumentosPorCobrarDetallePagosModels();
                DocumentoPorCobrarPago.RespuestaAjax = new RespuestaAjax();

                string Id_flete = string.IsNullOrEmpty(id_1) ? string.Empty : id_1;
                string Id_documentoPorCobrarDetallePago = string.IsNullOrEmpty(id_2) ? string.Empty : id_2;

                if (Id_flete.Length == 36 && (Id_documentoPorCobrarDetallePago.Length == 0 || Id_documentoPorCobrarDetallePago.Length == 36)) 
                {
                    DocumentoPorCobrarPago.Conexion = Conexion;
                    DocumentoPorCobrarPago.Id_padre = Id_flete;
                    DocumentoPorCobrarPago.Id_documentoPorCobrarDetallePagos = Id_documentoPorCobrarDetallePago;

                    DocumentoPorCobrarPago = FleteDatos.GetDetalleDocumentoPago(DocumentoPorCobrarPago);

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


                        DocumentoPorCobrarPago.ListaFormaPagos = FleteDatos.GetListadoCFDIFormaPago(DocumentoPorCobrarPago);
                        DocumentoPorCobrarPago = FleteDatos.GetNombreEmpresaProveedorCliente(DocumentoPorCobrarPago);
                        DocumentoPorCobrarPago.TipoCuentaBancaria = 1;
                        DocumentoPorCobrarPago.ListaCuentasBancariasEmpresa = FleteDatos.GetListadoCuentasBancarias(DocumentoPorCobrarPago);
                        DocumentoPorCobrarPago.TipoCuentaBancaria = 2;
                        DocumentoPorCobrarPago.ListaCuentasBancariasProveedor = FleteDatos.GetListadoCuentasBancarias(DocumentoPorCobrarPago);
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
        public ActionResult AC_FleteCobro(DocumentosPorCobrarDetallePagosModels DocumentoPorCobrarPago)
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
                    _Flete_Datos FleteDatos = new _Flete_Datos();

                    DocumentoPorCobrarPago = FleteDatos.AC_ComprobanteCobro(DocumentoPorCobrarPago);

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
                    return RedirectToAction("AC_FleteTransacciones", "Flete", new { IDFlete = DocumentoPorCobrarPago.Id_padre });
                }
                else
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Verifique sus datos.";
                    return RedirectToAction("Index", "Flete");
                }
            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                TempData["typemessage"] = "2";
                TempData["message"] = "Verifique sus datos, error: " + Mensaje;
                return RedirectToAction("Index", "Flete");
            }
        }
        #endregion

        #region Funciones AC_DEL
        #region Cliente
        public ActionResult AC_Cliente(FleteModels Flete)
        {
            try
            {
                if (Token.IsTokenValid())
                {
                    FleteDatos = new _Flete_Datos();
                    Flete.Conexion = Conexion;
                    Flete.Usuario = User.Identity.Name;
                    Flete.RespuestaAjax = new RespuestaAjax();
                    Flete.RespuestaAjax = FleteDatos.Flete_ac_FleteCliente(Flete);
                    if (Flete.RespuestaAjax.Success)
                    {
                        Flete.RespuestaAjax.Mensaje.ToJSON();
                    }

                    Token.ResetToken();
                    Token.SaveToken();
                    return Content(Flete.RespuestaAjax.ToJSON(), "application/json");
                }
                else
                {
                    return RedirectToAction("Index", "Flete");
                }

            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                Flete.RespuestaAjax.Mensaje = Mensaje;
                Flete.RespuestaAjax.Success = false;
                return Content(Flete.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        #endregion
        #region Trayecto
        public ActionResult AC_Trayecto(FleteModels FleteActual)
        {
            try
            {
                if (Token.IsTokenValid())
                {
                    if (FleteActual.id_flete.Length == 36)
                    {
                        FleteDatos = new _Flete_Datos();
                        FleteActual.Conexion = Conexion;
                        FleteActual.Usuario = User.Identity.Name;
                        FleteActual = FleteDatos.Flete_ac_FleteTrayecto(FleteActual);

                        Token.ResetToken();
                        Token.SaveToken();
                        return Content(FleteActual.RespuestaAjax.ToJSON(), "application/json");
                    }
                    else
                    {
                        TempData["typemessage"] = "2";
                        TempData["message"] = "Flete no válido.";
                        return RedirectToAction("Index", "Flete");
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Flete");
                }
            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                FleteActual.RespuestaAjax.Mensaje = Mensaje;
                FleteActual.RespuestaAjax.Success = false;
                return Content(FleteActual.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        #endregion
        #region Cobro
        public ActionResult AC_Cobro(FleteModels Flete)
        {
            try
            {
                if (Token.IsTokenValid())
                {
                    if (Flete.id_flete.Length == 36)
                    {
                        FleteDatos = new _Flete_Datos();
                        Flete.Conexion = Conexion;
                        Flete.Usuario = User.Identity.Name;
                        Flete.RespuestaAjax = new RespuestaAjax();
                        Flete.RespuestaAjax = FleteDatos.Flete_ac_FleteCobro(Flete);
                        Token.ResetToken();
                        Token.SaveToken(); ;

                        return Content(Flete.RespuestaAjax.ToJSON(), "application/json");
                    }
                    else
                    {
                        TempData["typemessage"] = "2";
                        TempData["message"] = "Flete no válido.";
                        return RedirectToAction("Index", "Flete");
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Flete");
                }
            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                Flete.RespuestaAjax.Mensaje = Mensaje;
                Flete.RespuestaAjax.Success = false;
                return Content(Flete.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        #endregion
     
        #region Producto Ganado Externo
        public ActionResult AC_ProductoGanadoExterno(string IDFlete, string IDProducto, string numArete, string genero, string peso, string posicionNode)
        {
            try
            {
                if (Token.IsTokenValid())
                {
                    FleteDatos = new _Flete_Datos();
                    Flete_ProductoModels ganado = new Flete_ProductoModels();
                    ganado.RespuestaAjax = new RespuestaAjax();
                    ganado.Conexion = Conexion;
                    ganado.Usuario = User.Identity.Name;
                    ganado.ID_Flete = IDFlete;
                    ganado.ID_Producto = IDProducto;
                    ganado.NumArete = numArete;
                    ganado.Genero = genero;
                    ganado.PesoAproximado = double.Parse(peso);

                    ganado = FleteDatos.AC_ProductoGanadoExterno(ganado);
                    if(ganado.RespuestaAjax.Success)
                        ganado.RespuestaAjax.Mensaje = "{\"IDProducto\": \""+ganado.RespuestaAjax.Mensaje+ "\", \"posicionNode\": \"" + posicionNode + "\"}";
                    else
                        ganado.RespuestaAjax.Mensaje = "{\"Mensaje\": \"" + ganado.RespuestaAjax.Mensaje + "\", \"posicionNode\": \"" + posicionNode + "\"}";

                    Token.ResetToken();
                    Token.SaveToken();
                    return Content(ganado.RespuestaAjax.ToJSON(), "application/json");
                }
                else
                {
                    return RedirectToAction("Index", "Flete");
                }
            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                Flete.RespuestaAjax = new RespuestaAjax();
                Flete.RespuestaAjax.Mensaje = Mensaje;
                Flete.RespuestaAjax.Success = false;

                return Content(Flete.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        public ActionResult DEL_ProductoGanadoExterno(string IDFlete, string IDProducto)
        {
            try
            {
                if (Token.IsTokenValid())
                {
                    FleteDatos = new _Flete_Datos();
                    Flete_ProductoModels ganado = new Flete_ProductoModels();
                    ganado.RespuestaAjax = new RespuestaAjax();
                    ganado.Conexion = Conexion;
                    ganado.Usuario = User.Identity.Name;
                    ganado.ID_Flete = IDFlete;
                    ganado.ID_Producto = IDProducto;
                    ganado = FleteDatos.DEL_ProductoGanadoExterno(ganado);

                    Token.ResetToken();
                    Token.SaveToken();
                    return Content(ganado.RespuestaAjax.ToJSON(), "application/json");
                }
                else
                {
                    return RedirectToAction("Index", "Flete");
                }
            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                Flete.RespuestaAjax = new RespuestaAjax();
                Flete.RespuestaAjax.Mensaje = Mensaje;
                Flete.RespuestaAjax.Success = false;

                return Content(Flete.RespuestaAjax.ToJSON(), "application/json");
            }
        }


        #endregion
        #region Producto Ganado Propio (Grupo Ocampo)
        public ActionResult C_DEL_ProductoGanado(string[] ganados, int opcion, string idFlete)
        {
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ganados.Length > 0)
                    {
                        string ListadoIDGanado = string.Empty;
                        if (opcion == 1)
                        {
                            for (int i = 0; i < ganados.Length; i++)
                            {
                                ListadoIDGanado += ganados[i] + ",";
                            }
                        }
                        else if (opcion == 2)
                            ListadoIDGanado = ganados[0];

                        FleteDatos = new _Flete_Datos();
                        Flete_ProductoModels Flete_ProductoGanado = new Flete_ProductoModels
                        {
                            Usuario = User.Identity.Name,
                            Conexion = Conexion,
                            ListaStringIDProductoSeleccionado = ListadoIDGanado,
                            ID_Flete = idFlete
                        };
                        if (opcion == 1)
                            Flete_ProductoGanado = FleteDatos.Flete_c_ProductoGanado(Flete_ProductoGanado);
                        else if (opcion == 2)
                            Flete_ProductoGanado = FleteDatos.Flete_del_ProductoGanado(Flete_ProductoGanado);

                        Token.ResetToken();
                        Token.SaveToken();
                        return Content(Flete_ProductoGanado.RespuestaAjax.ToJSON(), "application/json");
                    }
                    else
                    {
                        TempData["typemessage"] = "2";
                        TempData["message"] = "Flete no válido.";
                        return RedirectToAction("Index", "Flete");
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Flete");
                }
            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                Flete.RespuestaAjax = new RespuestaAjax();
                Flete.RespuestaAjax.Mensaje = Mensaje; 
                Flete.RespuestaAjax.Success = false;
                return Content(Flete.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        #endregion
        #region Evento
        public ActionResult AC_Evento(EventoEnvioModels Evento)
        {
            try
            {
                if (Token.IsTokenValid())
                {
                    if (Evento.IDEnvio.Length == 36)
                    {
                        FleteDatos = new _Flete_Datos();
                        Evento.Conexion = Conexion;
                        Evento.Usuario = User.Identity.Name;
                        Evento.RespuestaAjax = new RespuestaAjax();
                        
                        //verificamos si tiene alguna imagen
                        if(Evento.HttpImagen != null)
                        {
                            Evento.ImagenBase64 = Auxiliar.ImageToBase64(Evento.HttpImagen);
                        }
                        else
                        {
                            //ya que no es obligatorio la imagen
                            Evento.ImagenBase64 = Evento.ImagenMostrar;
                        }

                        //Evento = FleteDatos.AC_Evento(Evento);
                        Token.ResetToken();
                        Token.SaveToken();
                        return Content(Evento.RespuestaAjax.ToJSON(), "application/json");
                    }
                    else
                    {
                        TempData["typemessage"] = "2";
                        TempData["message"] = "Flete no válido.";
                        return RedirectToAction("Index", "Flete");
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Flete");
                }
            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                Evento.RespuestaAjax.Mensaje = Mensaje;
                Evento.RespuestaAjax.Success = false;
                return Content(Evento.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        public ActionResult DEL_Evento(int IDEvento, string IDFlete)
        {
            try
            {
                if (Token.IsTokenValid())
                {
                    if (IDFlete.Length == 36)
                    {
                        FleteDatos = new _Flete_Datos();
                        EventoEnvioModels Evento = new EventoEnvioModels();
                        Evento.Conexion = Conexion;
                        Evento.Usuario = User.Identity.Name;
                        Evento.IDEvento = IDEvento;
                        Evento.IDEnvio = IDFlete;
                        Evento.RespuestaAjax = new RespuestaAjax();

                        Evento = FleteDatos.DEL_Evento(Evento);
                        Token.ResetToken();
                        Token.SaveToken();
                        return Content(Evento.RespuestaAjax.ToJSON(), "application/json");
                    }
                    else
                    {
                        TempData["typemessage"] = "2";
                        TempData["message"] = "Flete no válido.";
                        return RedirectToAction("Index", "Flete");
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Flete");
                }
            }
            catch (Exception ex)
            {
                EventoEnvioModels Evento = new EventoEnvioModels();
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                Evento.RespuestaAjax.Mensaje = Mensaje;
                Evento.RespuestaAjax.Success = false;
                return Content(Evento.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        #endregion
        #region Recepción destino
        public ActionResult AC_RecepcionDestino(FleteModels Flete)
        {
            try
            {
                if (Token.IsTokenValid())
                {
                    if (Flete.RecepcionDestino.IDFlete.Length > 0)
                    {
                        FleteDatos = new _Flete_Datos();
                        Flete.RecepcionDestino.Conexion = Conexion;
                        Flete.RecepcionDestino.Usuario = User.Identity.Name;
                        Flete = FleteDatos.AC_RecepcionDestino(Flete);
                        Token.ResetToken();
                        Token.SaveToken();
                        return Content(Flete.RecepcionDestino.RespuestaAjax.ToJSON(), "application/json");
                    }
                    else
                    {
                        TempData["typemessage"] = "2";
                        TempData["message"] = "Flete no válido.";
                        return RedirectToAction("Index", "Flete");
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Flete");
                }
            }
            catch (Exception ex)
            {
                Flete.RespuestaAjax = new RespuestaAjax();
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                Flete.RespuestaAjax.Mensaje = Mensaje;
                Flete.RespuestaAjax.Success = false;
                return Content(Flete.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        #endregion
        #region Recepción origen
        public ActionResult AC_RecepcionOrigen(FleteModels Flete)
        {
            try
            {
                if (Token.IsTokenValid())
                {
                    if (Flete.RecepcionOrigen.IDFlete.Length > 0)
                    {
                        FleteDatos = new _Flete_Datos();
                        Flete.RecepcionOrigen.Conexion = Conexion;
                        Flete.RecepcionOrigen.Usuario = User.Identity.Name;
                        Flete = FleteDatos.AC_RecepcionOrigen(Flete);
                        Token.ResetToken();
                        Token.SaveToken();
                        return Content(Flete.RecepcionOrigen.RespuestaAjax.ToJSON(), "application/json");
                    }
                    else
                    {
                        TempData["typemessage"] = "2";
                        TempData["message"] = "Flete no válido.";
                        return RedirectToAction("Index", "Flete");
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Flete");
                }
            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                Flete.RespuestaAjax = new RespuestaAjax();
                Flete.RespuestaAjax.Mensaje = Mensaje;
                Flete.RespuestaAjax.Success = false;
                return Content(Flete.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        #endregion

        #endregion
        
        #region Modales
        [HttpPost]
        public ActionResult ModalDocumento(string IDFlete, string IDDocumento)
        {
            {
                FleteDatos = new _Flete_Datos();
                Flete_TipoDocumentoModels Flete_TipoDocumento = new Flete_TipoDocumentoModels();
                Flete_TipoDocumento.IDFlete = IDFlete;
                Flete_TipoDocumento.IDDocumento = IDDocumento;
                Flete_TipoDocumento.Conexion = Conexion;

                Flete_TipoDocumento = FleteDatos.GetDocumentoXIDDocumento(Flete_TipoDocumento);
                Flete_TipoDocumento.ListaTipoDocumentos = FleteDatos.GetListaTiposDocumentos(Flete_TipoDocumento);

                return PartialView("ModalDocumentos", Flete_TipoDocumento);
            }
        }
        [HttpPost]
        public ActionResult ModalProductoGanado()
        {
            return PartialView("ModalProductoGanado");
        }
        [HttpPost]
        public ActionResult ModalEvento(string IDFlete, string IDEvento)
        {
            {
                FleteDatos = new _Flete_Datos();
                EventoEnvioModels Evento = new EventoEnvioModels();
                Evento.IDEnvio = IDFlete;
                Evento.IDEvento = int.Parse(IDEvento);
                Evento.Conexion = Conexion;

                Evento = FleteDatos.GetEventoXIDEventoXIDFlete(Evento);
                Evento.ListaEventos = FleteDatos.GetListaTiposEventos(Evento);

                if (string.IsNullOrEmpty(Evento.ImagenBase64))
                    Evento.ImagenMostrar = Auxiliar.SetDefaultImage();
                else
                    Evento.ImagenMostrar = Evento.ImagenBase64;

                if (string.Equals(IDEvento, "0"))
                {
                    Evento.FechaDeteccion = DateTime.Today;
                    Evento.HoraDetecccion = DateTime.Now.TimeOfDay;
                }

                Evento.ExtensionImagenBase64 = Auxiliar.ObtenerExtensionImagenBase64(Evento.ImagenMostrar);

                return PartialView("ModalEvento", Evento);
            }
        }
        #endregion

        #region funciones combo
        
        [HttpPost]
        public ActionResult GetChoferesXIDEmpresa(string IDEmpresa)
        {
            try
            {
                Flete = new FleteModels();
                FleteDatos = new _Flete_Datos();
                Flete.Conexion = Conexion;
                Flete.Empresa.IDEmpresa = IDEmpresa;
                Flete.Usuario = User.Identity.Name;
                Flete.Chofer.ListaChoferes = FleteDatos.GetChoferesXIDEmpresa(Flete);

                return Content(Flete.Chofer.ListaChoferes.ToJSON(), "application/json");
            }
            catch
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error. Por favor contacte a soporte técnico";
                return Json("");
            }
        }
        [HttpPost]
        public ActionResult GetVehiculosXIDEmpresa(string IDEmpresa)
        {
            try
            {
                Flete = new FleteModels();
                FleteDatos = new _Flete_Datos();
                Flete.Conexion = Conexion;
                Flete.Empresa.IDEmpresa = IDEmpresa;
                Flete.Usuario = User.Identity.Name;
                Flete.Vehiculo.listaVehiculos = FleteDatos.GetVehiculosXIDEmpresa(Flete);

                return Content(Flete.Vehiculo.listaVehiculos.ToJSON(), "application/json");
            }
            catch
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error. Por favor contacte a soporte técnico";
                return Json("");
            }
        }
        [HttpPost]
        public ActionResult GetJaulasXIDEmpresa(string IDEmpresa)
        {
            try
            {
                Flete = new FleteModels();
                FleteDatos = new _Flete_Datos();
                Flete.Conexion = Conexion;
                Flete.Empresa.IDEmpresa = IDEmpresa;
                Flete.Usuario = User.Identity.Name;
                Flete.Jaula.listaJaulas = FleteDatos.GetJaulasXIDEmpresa(Flete);

                return Content(Flete.Jaula.listaJaulas.ToJSON(), "application/json");
            }
            catch
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error. Por favor contacte a soporte técnico";
                return Json("");
            }
        }
        [HttpPost]
        public ActionResult GetRemolquesXIDEmpresa(string IDEmpresa)
        {
            try
            {
                Flete = new FleteModels();
                FleteDatos = new _Flete_Datos();
                Flete.Conexion = Conexion;
                Flete.Empresa.IDEmpresa = IDEmpresa;
                Flete.Usuario = User.Identity.Name;
                Flete.Remolque.listaRemolque = FleteDatos.GetRemolquesXIDEmpresa(Flete);

                return Content(Flete.Remolque.listaRemolque.ToJSON(), "application/json");
            }
            catch
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error. Por favor contacte a soporte técnico";
                return Json("");
            }
        }
        [HttpPost]
        public ActionResult GetLugarXIDRemitente(string IDRemitente)
        {
            try
            {
                Flete = new FleteModels();
                FleteDatos = new _Flete_Datos();
                Flete.Conexion = Conexion;
                Flete.Usuario = User.Identity.Name;
                Flete.Trayecto.ListaLugarOrigen = FleteDatos.GetListadoLugaresXIDProveedorIDCliente(Flete, IDRemitente);

                return Content(Flete.Trayecto.ListaLugarOrigen.ToJSON(), "application/json");
            }
            catch
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error. Por favor contacte a soporte técnico";
                return Json("");
            }
        }
        [HttpPost]
        public ActionResult GetLugarXIDDestino(string IDDestino)
        {
            try
            {
                Flete = new FleteModels();
                FleteDatos = new _Flete_Datos();
                Flete.Conexion = Conexion;
                Flete.Usuario = User.Identity.Name;
                Flete.Trayecto.ListaLugarDestino = FleteDatos.GetListadoLugaresXIDProveedorIDCliente(Flete, IDDestino);

                return Content(Flete.Trayecto.ListaLugarDestino.ToJSON(), "application/json");
            }
            catch
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error. Por favor contacte a soporte técnico";
                return Json("");
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
                _Flete_Datos reporteDatos = new _Flete_Datos();
                FleteModels Flete = new FleteModels();
                Flete.id_flete = id;
                Flete.Conexion = Conexion;
                reporte = reporteDatos.GetCartaPorte(Flete);
                reporte.ListaDetallesCartaPorte = reporteDatos.GetCartaPorteDetalles(Flete);
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
                    return RedirectToAction("Index", "Flete");
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

        #region ReporteGanadoPropio
        public ActionResult ReporteGanadoPropioV2(string id)
        {
            try
            {

                Reporte_Datos R = new Reporte_Datos();
                List<ReporteGanadoModels> Listareporte = new List<ReporteGanadoModels>();
                _Flete_Datos reporteDatos = new _Flete_Datos();
                FleteModels Flete = new FleteModels();
                CatEmpresaModels Empresa = new CatEmpresaModels();
                ReporteCabeceraGanado Cabezera = new ReporteCabeceraGanado();
                _CatEmpresa_Datos EmpresaDatos = new _CatEmpresa_Datos();
                List<CatFierroModels> ListaFierros = new List<CatFierroModels>();

                Flete.id_flete = id;
                Flete.Conexion = Conexion;
                Empresa.Conexion = Conexion;
                Listareporte = reporteDatos.GetReporteGanadoDetalles(Flete);
                Cabezera = reporteDatos.GetReporteCabeceraGanadoDetalles(Flete);
                ListaFierros = reporteDatos.GetReporteFierrosVenta(Flete);

                Empresa = EmpresaDatos.GetDatosEmpresaPrincipal(Empresa);
                DatosGeneralesGanados datos = new DatosGeneralesGanados();
                datos = Auxiliar.ObtenerDatosGeneralesGanado(datos, Listareporte);
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
                    return RedirectToAction("Index", "Flete");
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
                Parametros[16] = new ReportParameter("TotalKilosGanado", Convert.ToInt32(Cabezera.TotalKilosGanado).ToString());
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
        public ActionResult ReporteGanadoPropio(string id)
        {
            try
            {

                Reporte_Datos R = new Reporte_Datos();
                List<ReporteGanadoModels> Listareporte = new List<ReporteGanadoModels>();
                _Flete_Datos reporteDatos = new _Flete_Datos();
                FleteModels Flete = new FleteModels();
                CatEmpresaModels Empresa = new CatEmpresaModels();
                _CatEmpresa_Datos EmpresaDatos = new _CatEmpresa_Datos();
                Flete.id_flete = id;
                Flete.Conexion = Conexion;
                Empresa.Conexion = Conexion;
                Listareporte = reporteDatos.GetReporteGanadoDetalles(Flete);
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
                    return RedirectToAction("Index", "Flete");
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

        #region Vista Documentos

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id_1">Id del flete</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AC_FleteDocumentos(string Id_1)
        {
            try
            {
                Token.SaveToken();

                string Id_compra = string.IsNullOrEmpty(Id_1) ? string.Empty : Id_1;
                //0 = nuevo, 36 = edit, si es diferente es un id no valido
                if (Id_compra.Length == 36)
                {
                    _Flete_Datos FleteDatos = new _Flete_Datos();
                    DocumentoModels Documento = new DocumentoModels();
                    Documento.Id_servicio = Id_1;
                    Documento.Conexion = Conexion;
                    Documento = FleteDatos.GetGeneralesDocumentosFlete(Documento);
                    Documento.ListaConceptosSalidaDeduccion = FleteDatos.GetListadoTipoClasificacionPago(Documento);

                    return View(Documento);
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
                    _Flete_Datos FleteDatos = new _Flete_Datos();
                    Documento.Conexion = Conexion;
                    Documento.Usuario = User.Identity.Name;
                    Documento = FleteDatos.AC_CostoDocumentos(Documento);
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
        public ActionResult AC_FleteDocumento(string Id_flete, string IDDocumento)
        {
            {
                if (string.IsNullOrEmpty(Id_flete) || string.IsNullOrEmpty(IDDocumento))
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Verifique sus datos.";
                    return View("Index");
                }

                if (Id_flete.Length == 0 || Id_flete.Length == 36)
                {
                    _Flete_Datos FleteDatos = new _Flete_Datos();
                    DocumentoModels Documento = new DocumentoModels();
                    Documento.Id_servicio = Id_flete;
                    Documento.IDDocumento = IDDocumento;
                    Documento.Conexion = Conexion;

                    Documento = FleteDatos.GetDocumentoXIDDocumento(Documento);
                    Documento.ListaTipoDocumentos = FleteDatos.GetListaTiposDocumentos(Documento);

                    return View(Documento);
                }
                else
                {
                    return View("Index");
                }
            }
        }
        [HttpPost]
        public ActionResult AC_FleteDocumento(DocumentoModels Documento)
        {
            try
            {
                if (Token.IsTokenValid())
                {
                    if (Documento.Id_servicio.Length == 36)
                    {
                        _Flete_Datos FleteDatos = new _Flete_Datos();
                        Documento.Conexion = Conexion;
                        Documento.Usuario = User.Identity.Name;

                        if (Documento.ImagenPost != null)
                        {
                            Documento.ImagenServer = Auxiliar.ImageToBase64(Documento.ImagenPost);
                        }
                        Documento.RespuestaAjax = new RespuestaAjax();
                        Documento = FleteDatos.AC_Documento(Documento);
                        if (Documento.RespuestaAjax.Success)
                        {
                            Token.ResetToken();
                            TempData["typemessage"] = "1";
                            TempData["message"] = Documento.RespuestaAjax.Mensaje;
                            return RedirectToAction("AC_FleteDocumentos", "Flete", new { Id_1 = Documento.Id_servicio });
                        }
                        else
                        {
                            TempData["typemessage"] = "2";
                            TempData["message"] = Documento.RespuestaAjax.Mensaje;
                            return RedirectToAction("Index", "Flete");
                        }
                    }
                    else
                    {
                        TempData["typemessage"] = "2";
                        TempData["message"] = "Verifique sus datos.";
                        return RedirectToAction("Index", "Flete");
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Flete");
                }
            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista, error: " + Mensaje;
                return RedirectToAction("Index", "Flete");
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
                        _Flete_Datos FleteDatos = new _Flete_Datos();
                        Documento.Conexion = Conexion;
                        Documento.Usuario = User.Identity.Name;
                        Documento = FleteDatos.DEL_DocumentoXIDDocumento(Documento);
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

        #region Vista Detalles
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id_1">Id del flete</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Detalles_Flete(string Id_1)
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
                FleteDetallesModels FleteDetalle = new FleteDetallesModels();
                _Flete_Datos FleteDatos = new _Flete_Datos();
                FleteDetalle.Id_flete = Id_1;
                FleteDetalle.Conexion = Conexion;
                FleteDetalle = FleteDatos.Get_detallesFlete(FleteDetalle);

                TransaccionesFleteModels Transacciones = new TransaccionesFleteModels();
                Transacciones.RespuestaAjax = new RespuestaAjax();
                Transacciones.Conexion = Conexion;
                Transacciones.Id_flete = Id_1;
                Transacciones = FleteDatos.GetTransacciones(Transacciones);

                FleteDetalle.Total = Transacciones.DocumentosPorCobrar.Total;
                FleteDetalle.TotalPercepciones = Transacciones.DocumentosPorCobrar.TotalPercepciones;
                FleteDetalle.TotalDeducciones = Transacciones.DocumentosPorCobrar.TotalDeducciones;
                FleteDetalle.Impuestos = Transacciones.DocumentosPorCobrar.Impuestos;
                FleteDetalle.Subtotal = Transacciones.Subtotal;

                FleteDetalle.Pendiente = Transacciones.DocumentosPorCobrar.Pendiente;
                FleteDetalle.Pagos = Transacciones.DocumentosPorCobrar.Pagos;
                FleteDetalle.Cambio = Transacciones.DocumentosPorCobrar.Cambio;

                return View(FleteDetalle);
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
        public ActionResult DatatableGeneralesGanado(string Id_flete)
        {
            try
            {
                Flete = new FleteModels();
                FleteDatos = new _Flete_Datos();
                Flete.Conexion = Conexion;
                Flete.id_flete = Id_flete;
                Flete.RespuestaAjax.Mensaje = FleteDatos.DatatableGeneralesGanado(Flete);
                Flete.RespuestaAjax.Success = true;

                return Content(Flete.RespuestaAjax.Mensaje, "application/json");

            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                Flete.RespuestaAjax.Mensaje = Mensaje;
                Flete.RespuestaAjax.Success = false;
                return Content(Flete.RespuestaAjax.ToJSON(), "application/json");
            }
        }

        [HttpPost]
        public ActionResult DatatableGanadoMacho(string Id_flete)
        {
            try
            {
                Flete = new FleteModels();
                FleteDatos = new _Flete_Datos();
                Flete.Conexion = Conexion;
                Flete.id_flete = Id_flete;
                Flete.RespuestaAjax.Mensaje = FleteDatos.DatatableGanadoMacho(Flete);
                Flete.RespuestaAjax.Success = true;

                return Content(Flete.RespuestaAjax.Mensaje, "application/json");

            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                Flete.RespuestaAjax.Mensaje = Mensaje;
                Flete.RespuestaAjax.Success = false;
                return Content(Flete.RespuestaAjax.ToJSON(), "application/json");
            }
        }

        [HttpPost]
        public ActionResult DatatableGanadoHembra(string Id_flete)
        {
            try
            {
                Flete = new FleteModels();
                FleteDatos = new _Flete_Datos();
                Flete.Conexion = Conexion;
                Flete.id_flete = Id_flete;
                Flete.RespuestaAjax.Mensaje = FleteDatos.DatatableGanadoHembra(Flete);
                Flete.RespuestaAjax.Success = true;

                return Content(Flete.RespuestaAjax.Mensaje, "application/json");

            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                Flete.RespuestaAjax.Mensaje = Mensaje;
                Flete.RespuestaAjax.Success = false;
                return Content(Flete.RespuestaAjax.ToJSON(), "application/json");
            }
        }

        [HttpPost]
        public ActionResult DatatableDetallesDocXcobrar(string Id_flete)
        {
            try
            {
                _Flete_Datos FleteDatos = new _Flete_Datos();
                FleteModels Flete = new FleteModels();
                Flete.Conexion = Conexion;
                Flete.id_flete = Id_flete;
                Flete.RespuestaAjax = new RespuestaAjax();
                Flete.RespuestaAjax.Mensaje = FleteDatos.DatatableDetallesDocXcobrar(Flete);
                Flete.RespuestaAjax.Success = true;

                return Content(Flete.RespuestaAjax.Mensaje, "application/json");

            }
            catch (Exception ex)
            {
                FleteModels Flete = new FleteModels();
                Flete.RespuestaAjax = new RespuestaAjax();
                Flete.RespuestaAjax.Mensaje = ex.Message;
                Flete.RespuestaAjax.Success = false;
                return Content(Flete.RespuestaAjax.ToJSON(), "application/json");
            }
        }

        [HttpPost]
        public ActionResult DatatableDetalleDocXCobraPagos(string Id_flete)
        {
            try
            {
                _Flete_Datos FleteDatos = new _Flete_Datos();
                FleteModels Flete = new FleteModels();
                Flete.Conexion = Conexion;
                Flete.id_flete = Id_flete;
                Flete.RespuestaAjax = new RespuestaAjax();
                Flete.RespuestaAjax.Mensaje = FleteDatos.DatatableDetalleDocXCobrarPagos(Flete);
                Flete.RespuestaAjax.Success = true;

                return Content(Flete.RespuestaAjax.Mensaje, "application/json");

            }
            catch (Exception ex)
            {
                FleteModels Flete = new FleteModels();
                Flete.RespuestaAjax = new RespuestaAjax();
                Flete.RespuestaAjax.Mensaje = ex.Message;
                Flete.RespuestaAjax.Success = false;
                return Content(Flete.RespuestaAjax.ToJSON(), "application/json");
            }
        }

        [HttpPost]
        public ActionResult DatatableDocumentos(string Id_flete)
        {
            try
            {
                _Flete_Datos FleteDatos = new _Flete_Datos();
                FleteModels Flete = new FleteModels();
                Flete.Conexion = Conexion;
                Flete.id_flete = Id_flete;
                Flete.RespuestaAjax = new RespuestaAjax();
                Flete.RespuestaAjax.Mensaje = FleteDatos.GetDocumentosDataTable(Flete);
                Flete.RespuestaAjax.Success = true;

                return Content(Flete.RespuestaAjax.Mensaje, "application/json");

            }
            catch (Exception ex)
            {
                FleteModels Flete = new FleteModels();
                Flete.RespuestaAjax = new RespuestaAjax();
                Flete.RespuestaAjax.Mensaje = ex.Message;
                Flete.RespuestaAjax.Success = false;
                return Content(Flete.RespuestaAjax.ToJSON(), "application/json");
            }
        }

        [HttpGet]
        public ActionResult Detalles_Flete_Eventos(string IDFlete, int Id_eventoFlete)
        {
            try
            {
                Token.SaveToken();
                _Flete_Datos FleteDatos = new _Flete_Datos();
                EventoFleteModels EventoFlete = new EventoFleteModels();
                EventoFlete.RespuestaAjax = new RespuestaAjax();

                string Id_flete = string.IsNullOrEmpty(IDFlete) ? string.Empty : IDFlete;
                //0 = nuevo, 36 = edit, si es diferente es un id no valido
                if (Id_flete.Length == 0 || Id_flete.Length == 36)
                {
                    EventoFlete.Conexion = Conexion;
                    EventoFlete.Id_flete = Id_flete;
                    EventoFlete.Id_eventoFlete = Id_eventoFlete;
                    EventoFlete = FleteDatos.GetFleteEvento(EventoFlete);
                    if (EventoFlete.RespuestaAjax.Success)
                    {
                        if (string.IsNullOrEmpty(EventoFlete.ImagenBase64))
                        {
                            EventoFlete.ImagenMostrar = Auxiliar.SetDefaultImage();
                        }
                        else
                        {
                            EventoFlete.ImagenMostrar = EventoFlete.ImagenBase64;
                        }
                        EventoFlete.ExtensionImagenBase64 = Auxiliar.ObtenerExtensionImagenBase64(EventoFlete.ImagenMostrar);
                        //aqui pondriamos alguna lista o valores de cargar si esta todo correcto
                        EventoFlete.ListaDeTiposDeduccion = FleteDatos.GetTiposDeduccion(EventoFlete);
                        EventoFlete.ListaTiposEventos = FleteDatos.GetTiposEventos(EventoFlete);
                        return View(EventoFlete);
                    }
                    else
                    {
                        TempData["typemessage"] = "2";
                        TempData["message"] = "No se puede cargar la vista, error: " + EventoFlete.RespuestaAjax.Mensaje;
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

        #region Delete Flete
        [HttpPost]
        public ActionResult DEL_Flete(FleteModels Flete)
        {
            try
            {
                if (Flete.id_flete.Length == 36)
                {
                    if (Token.IsTokenValid())
                    {
                        _Flete_Datos FleteDatos = new _Flete_Datos();
                        Flete.Conexion = Conexion;
                        Flete.Usuario = User.Identity.Name;
                        Flete = FleteDatos.Flete_del_Flete(Flete);

                        if (Flete.RespuestaAjax.Success)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = Flete.RespuestaAjax.Mensaje;
                            return Content(Flete.RespuestaAjax.ToJSON(), "application/json");
                        }
                        else
                        {
                            TempData["typemessage"] = "2";
                            TempData["message"] = Flete.RespuestaAjax.Mensaje;
                            return Content(Flete.RespuestaAjax.ToJSON(), "application/json");
                        }
                    }
                    else
                    {
                        Flete.RespuestaAjax.Success = false;
                        TempData["typemessage"] = "2";
                        TempData["message"] = "Verifique sus datos.";
                        return Content(Flete.RespuestaAjax.ToJSON(), "application/json");
                    }
                }
                else
                {
                    Flete.RespuestaAjax.Success = false;
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Flete no válido.";
                    return Content(Flete.RespuestaAjax.ToJSON(), "application/json");
                }
            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                Flete.RespuestaAjax.Success = false;
                TempData["typemessage"] = "2";
                TempData["message"] = "Contacte con soporte técnico, error: " + Mensaje;
                return Content(Flete.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        #endregion

        #region Finalizar Flete
        [HttpGet]
        public ActionResult FinalizarFlete(string id_flete)
        {
            if (id_flete.Length == 36)
            {
                Flete = new FleteModels();
                FleteDatos = new _Flete_Datos();
                //Asigno valores para los querys
                Flete.Conexion = Conexion;
                Flete.id_flete = id_flete;
                Flete.Usuario = User.Identity.Name;
                //Paso al siguiente paso
                Flete = FleteDatos.Flete_a_CambiarEstatusFleteXIDFlete(Flete);

                if (Flete.RespuestaAjax.Success)
                {
                    TempData["typemessage"] = "1";
                }
                else
                {
                    TempData["typemessage"] = "2";
                }

                TempData["message"] = Flete.RespuestaAjax.Mensaje;

                return RedirectToAction("Index", "Flete");
            }
            else
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Flete no válido.";
                return RedirectToAction("Index", "Flete");
            }
        }
        #endregion
    }
}
