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

                Flete.RespuestaAjax.Mensaje = Auxiliar.SqlReaderToJson(FleteDatos.GetFleteIndexDataTable(Flete));
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

                Flete.RespuestaAjax.Mensaje = Auxiliar.SqlReaderToJson(FleteDatos.GetDocumentosDataTable(Flete));
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
        #region Funcion Json GanadoActual
        [HttpPost]
        public ActionResult TableJsonGanadoActual()
        {
            try
            {
                Flete = new FleteModels();
                FleteDatos = new _Flete_Datos();
                Flete.Conexion = Conexion;

                Flete.RespuestaAjax.Mensaje = Auxiliar.SqlReaderToJson(FleteDatos.GetGanadoDataTable(Flete));
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
                Flete.RespuestaAjax.Mensaje = Auxiliar.SqlReaderToJson(FleteDatos.GetProductoGanadoXIDFlete(Flete));
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
                Flete.RespuestaAjax.Mensaje = Auxiliar.SqlReaderToJson(FleteDatos.GetProductoGanadoNOPropioXIDFlete(Flete));
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
                Flete.RespuestaAjax.Mensaje = ex.ToString();
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
        /********************************************************************/
        [HttpGet]
        public ActionResult AC_Flete(string IDFlete)
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
                Flete = FleteDatos.Flete_get_ACFlete(Flete);
            }
            Flete.ListaEmpresa = FleteDatos.GetListadoEmpresas(Flete);
            Flete.ListaCliente = FleteDatos.GetListadoClientes(Flete);
            Flete.ListaChofer = FleteDatos.GetListadoChoferes(Flete);
            Flete.ListaVehiculo = FleteDatos.GetListadoVehiculos(Flete);
            Flete.ListaJaula = FleteDatos.GetListadoJaulas(Flete);
            Flete.ListaRemolque = FleteDatos.GetListadoRemolque(Flete);
            Flete.Trayecto.ListaRemitente = FleteDatos.GetListadoClientes(Flete);
            Flete.Trayecto.ListaDestinatario = FleteDatos.GetListadoClientes(Flete);
            Flete.Trayecto.ListaLugarOrigen = FleteDatos.GetListadoLugaresXIDProveedorIDCliente(Flete, Flete.Trayecto.Remitente.IDCliente);
            Flete.Trayecto.ListaLugarDestino = FleteDatos.GetListadoLugaresXIDProveedorIDCliente(Flete, Flete.Trayecto.Destinatario.IDCliente);
            Flete.ListaFormaPago = FleteDatos.GetListadoFormaPagos(Flete);
            Flete.ListaMetodoPago = FleteDatos.GetListadoMetodoPago(Flete);

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
            //cargarmos los datos del flete x id
            if (!string.IsNullOrEmpty(IDFlete) && IDFlete.Length == 36)
            {
                Flete.id_flete = IDFlete;
                Flete = FleteDatos.Flete_get_ACFlete(Flete);
            }

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
        /********************************************************************/
        //Funciones AC_DEL
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
                    Flete = FleteDatos.Flete_ac_FleteCliente(Flete);
                    if (Flete.RespuestaAjax.Success)
                        Flete.RespuestaAjax.Mensaje.ToJSON();

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
        public ActionResult A_Cobro(FleteModels Flete)
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
                        Flete = FleteDatos.Flete_a_FleteCobro(Flete);
                        Token.ResetToken();

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
                            Evento.ImagenBase64 = null;
                        }

                        Evento = FleteDatos.AC_Evento(Evento);
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
        #endregion
        #endregion
        /********************************************************************/
        //Funciones Modal
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
        /********************************************************************/
        //Funciones Combo
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
        /********************************************************************/
    }
}
