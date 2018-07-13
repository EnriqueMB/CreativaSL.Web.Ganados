using CreativaSL.Web.Ganados.App_Start;
using CreativaSL.Web.Ganados.Filters;
using CreativaSL.Web.Ganados.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
                Flete.RespuestaAjax.Mensaje = ex.Message;
                Flete.RespuestaAjax.Success = false;
                return Content(Flete.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        #endregion
        #region Funcion Json Documentos
        [HttpPost]
        public ActionResult TableJsonDocumentos(string IDFlete)
        {
            try
            {
                Flete = new FleteModels();
                FleteDatos = new _Flete_Datos();
                Flete.Conexion = Conexion;
                Flete.id_flete = IDFlete;

                Flete.RespuestaAjax.Mensaje = FleteDatos.GetDocumentosDataTable(Flete);
                Flete.RespuestaAjax.Success = true;

                return Content(Flete.RespuestaAjax.Mensaje, "application/json");

            }
            catch (Exception ex)
            {
                Flete.RespuestaAjax.Mensaje = ex.Message;
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
                Flete.RespuestaAjax.Mensaje = ex.ToString();
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
                Flete.RespuestaAjax.Mensaje = ex.ToString();
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
                Flete.RespuestaAjax.Mensaje = ex.ToString();
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
                Flete.RespuestaAjax.Mensaje = Auxiliar.SqlReaderToJson(FleteDatos.GetEventoXIDFlete(Flete));
                Flete.RespuestaAjax.Success = true;

                return Content(Flete.RespuestaAjax.Mensaje, "application/json");

            }
            catch (Exception ex)
            {
                Flete.RespuestaAjax.Mensaje = ex.ToString();
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
                Flete.RespuestaAjax.Mensaje = ex.Message;
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
                Flete.RespuestaAjax.Mensaje = ex.ToString();
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
                Flete.RespuestaAjax.Mensaje = ex.Message;
                return Content(Flete.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        #endregion
        [HttpPost]
        public ActionResult DatatableGanadoEnFlete(string Id_flete, string Id_eventoFlete)
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
                EventoFleteModels EventoFlete = new EventoFleteModels();
                EventoFlete.RespuestaAjax = new RespuestaAjax();
                EventoFlete.RespuestaAjax.Mensaje = ex.Message;
                EventoFlete.RespuestaAjax.Success = false;
                return Content(EventoFlete.RespuestaAjax.ToJSON(), "application/json");
            }
        }

        [HttpPost]
        public ActionResult DatatableGanadoConEvento(string Id_flete, string Id_eventoFlete)
        {
            try
            {
                _Flete_Datos FleteDatos = new _Flete_Datos();
                EventoFleteModels EventoFlete = new EventoFleteModels();
                EventoFlete.RespuestaAjax = new RespuestaAjax();
                EventoFlete.Id_flete = Id_flete;
                EventoFlete.Id_eventoFlete = Id_eventoFlete;
                EventoFlete.RespuestaAjax.Mensaje = FleteDatos.DatatableGanadoConEvento(EventoFlete);
                EventoFlete.RespuestaAjax.Success = true;

                return Content(EventoFlete.RespuestaAjax.Mensaje, "application/json");
            }
            catch (Exception ex)
            {
                EventoFleteModels EventoFlete = new EventoFleteModels();
                EventoFlete.RespuestaAjax = new RespuestaAjax();
                EventoFlete.RespuestaAjax.Mensaje = ex.Message;
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
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista, error: " + ex.Message;
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
                FleteImpuesto.RespuestaAjax.Mensaje = ex.Message;
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
                FleteImpuesto.RespuestaAjax.Success = false;
                TempData["typemessage"] = "2";
                TempData["message"] = "Contacte con soporte técnico, error: " + ex.Message;
                return Content(FleteImpuesto.RespuestaAjax.ToJSON(), "application/json");
            }
        }

        #endregion
        #region Vista Evento
        [HttpGet]
        public ActionResult AC_FleteEvento(string IDFlete, string Id_eventoFlete)
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
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista, error: " + ex.Message;
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
                        TempData["message"] = "Los datos se guardarón correctamente.";
                        Token.ResetToken();

                        return Content(EventoFlete.RespuestaAjax.ToJSON(), "application/json");
                    }
                    else
                    {
                        TempData["typemessage"] = "2";
                        TempData["message"] = "Ocurrio un error al intentar guardar los datos. Intente más tarde.";

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
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error al intentar guardar los datos. Contacte a soporte técnico: " + ex.Message;
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
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista, error: " + ex.Message;
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
                TempData["typemessage"] = "2";
                TempData["message"] = "Verifique sus datos, error: " + ex.Message;
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
                TempData["typemessage"] = "2";
                TempData["message"] = "Verifique sus datos, error: " + ex.Message;
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
                documento.RespuestaAjax = new RespuestaAjax();
                documento.RespuestaAjax.Success = false;

                TempData["typemessage"] = "2";
                TempData["message"] = ex.Message;

                return Content(documento.RespuestaAjax.ToJSON(), "application/json");
            }
        }
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

                //0 = nuevo, 36 = editar, pero ambos son válidos
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
                TempData["typemessage"] = "2";
                TempData["message"] = "Verifique sus datos, error: " + ex.Message;
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
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista, error: " + ex.Message;
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
                TempData["typemessage"] = "2";
                TempData["message"] = "Verifique sus datos, error: " + ex.Message;
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

                    return Content(Flete.RespuestaAjax.ToJSON(), "application/json");
                }
                else
                {
                    return RedirectToAction("Index", "Flete");
                }

            }
            catch (Exception ex)
            {
                Flete.RespuestaAjax.Mensaje = ex.ToString();
                Flete.RespuestaAjax.Success = false;
                return Content(Flete.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        #endregion
        #region Trayecto
        public ActionResult AC_Trayecto(FleteModels Flete)
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
                        Flete = FleteDatos.Flete_ac_FleteTrayecto(Flete);

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
                Flete.RespuestaAjax.Mensaje = ex.ToString();
                Flete.RespuestaAjax.Success = false;
                return Content(Flete.RespuestaAjax.ToJSON(), "application/json");
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
                Flete.RespuestaAjax.Mensaje = ex.ToString();
                Flete.RespuestaAjax.Success = false;
                return Content(Flete.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        #endregion
        #region Documento
        public ActionResult AC_Documento(Flete_TipoDocumentoModels Documento)
        {
            try
            {
                if (Token.IsTokenValid())
                {
                    if (Documento.IDFlete.Length == 36)
                    {
                        FleteDatos = new _Flete_Datos();
                        Documento.Conexion = Conexion;
                        Documento.Usuario = User.Identity.Name;
                        //Verifico input
                        if (Documento.ImagenPost != null)
                            Documento.Imagen = Auxiliar.ImageToBase64(Documento.ImagenPost);
                        else
                        {
                            //ya que no es obligatorio la imagen
                            Documento.Imagen = Documento.MostrarImagen;
                        }
                        Documento = FleteDatos.Flete_ac_Documento(Documento);
                        Token.ResetToken();
                        Token.SaveToken();
                        return Content(Documento.RespuestaAjax.ToJSON(), "application/json");
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
                Flete.RespuestaAjax.Mensaje = ex.ToString();
                Flete.RespuestaAjax.Success = false;
                return Content(Flete.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        public ActionResult DEL_Documento(Flete_TipoDocumentoModels Documento)
        {
            try
            {
                if (Token.IsTokenValid())
                {
                    if (Documento.IDDocumento.Length == 36)
                    {
                        FleteDatos = new _Flete_Datos();
                        Documento.Conexion = Conexion;
                        Documento.Usuario = User.Identity.Name;
                        Documento = FleteDatos.Flete_del_DocumentoXIDDocumento(Documento);
                        Token.ResetToken();
                        Token.SaveToken();
                        return Content(Documento.RespuestaAjax.ToJSON(), "application/json");
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
                Documento.RespuestaAjax.Mensaje = ex.ToString();
                Documento.RespuestaAjax.Success = false;
                return Content(Documento.RespuestaAjax.ToJSON(), "application/json");
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
                Flete.RespuestaAjax = new RespuestaAjax();
                Flete.RespuestaAjax.Mensaje = ex.ToString();
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
                Flete.RespuestaAjax = new RespuestaAjax();
                Flete.RespuestaAjax.Mensaje = ex.ToString();
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
                Flete.RespuestaAjax = new RespuestaAjax();
                Flete.RespuestaAjax.Mensaje = ex.ToString(); 
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
                Evento.RespuestaAjax.Mensaje = ex.ToString();
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
                Evento.RespuestaAjax.Mensaje = ex.ToString();
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
                Flete.RespuestaAjax.Mensaje = ex.ToString();
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
                Flete.RespuestaAjax = new RespuestaAjax();
                Flete.RespuestaAjax.Mensaje = ex.ToString();
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
    }
}
