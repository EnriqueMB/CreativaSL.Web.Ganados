using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CreativaSL.Web.Ganados.Filters;
using CreativaSL.Web.Ganados.Models;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using CreativaSL.Web.Ganados.App_Start;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    [Autorizado]
    public class CompraController : Controller
    {
        private string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
        private TokenProcessor Token = TokenProcessor.GetInstance();
        private CompraModels Compra;
        private _Compra_Datos CompraDatos;

        #region AgendarCompra
        [HttpGet]
        public ActionResult AgendarCompra(string IDCompra)
        {
            try
            {
                Token.SaveToken();
                Compra = new CompraModels();
                CompraDatos = new _Compra_Datos();
                Compra.Conexion = Conexion;
                Compra.DocumentosPorCobrarDetallePagos = new DocumentosPorCobrarDetallePagosModels();


                if (!string.IsNullOrEmpty(IDCompra))
                {
                    Compra.IDCompra = IDCompra;
                    Compra = CompraDatos.GetCompraProgramada(Compra);
                    Compra.DocumentosPorCobrarDetallePagos = new DocumentosPorCobrarDetallePagosModels();
                    Compra = CompraDatos.GetCompraEmbarque(Compra);
                }
                if (string.IsNullOrEmpty(Compra.DocumentosPorCobrarDetallePagos.ImagenBase64))
                {
                    Compra.DocumentosPorCobrarDetallePagos.ImagenMostrar = Auxiliar.SetDefaultImage();
                }
                else
                {
                    Compra.DocumentosPorCobrarDetallePagos.ImagenMostrar = Compra.DocumentosPorCobrarDetallePagos.ImagenBase64;
                }
                Compra.DocumentosPorCobrarDetallePagos.ExtensionImagenBase64 = Auxiliar.ObtenerExtensionImagenBase64(Compra.DocumentosPorCobrarDetallePagos.ImagenMostrar);
                Compra.DocumentosPorCobrarDetallePagos.ListaCuentasBancariasEmpresa = CompraDatos.GetListadoCuentasBancariasGrupoOcampo(Compra);
                Compra.DocumentosPorCobrarDetallePagos.ListaCuentasBancariasProveedor = CompraDatos.GetListadoCuentasBancariasProveedorXIDProveedor(Compra);
                Compra.ListaEmpresas = CompraDatos.GetListadoEmpresas(Compra);
                Compra.ListaSucursales = CompraDatos.GetListadoSucursales(Compra);
                Compra.ListaProveedores = CompraDatos.GetListaProveedores(Compra);
                Compra.ListaLugares = CompraDatos.GetListadoLugaresLugarXIDEmpresa(Compra);
                Compra.ListaChoferes = CompraDatos.GetChoferesXIDEmpresa(Compra);
                Compra.ListaVehiculos = CompraDatos.GetVehiculosXIDEmpresa(Compra);
                Compra.ListaLugaresProveedor = CompraDatos.GetListadoLugaresProveedorXIDProveedor(Compra);
                Compra.Flete.ListaMetodoPago = CompraDatos.GetMetodosPagos(Compra);
                Compra.Flete.ListaFormaPago = CompraDatos.GetListadoCFDIFormaPago(Compra);

                return View(Compra);
            }
            catch (Exception ex)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista, error: " + ex.Message;
                return View("Index");
            }
            
        }
        #endregion
        #region CompraGanado
        [HttpGet]
        public ActionResult GanadoCompra(string IDCompra)
        {
            Token.SaveToken();
            if (string.IsNullOrEmpty(IDCompra))
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista.";
                return RedirectToAction("Index");
            }
            else
            {
                try
                {
                    Compra = new CompraModels();
                    CompraDatos = new _Compra_Datos();
                    Compra.IDCompra = IDCompra;
                    Compra.Conexion = Conexion;
                    Compra = CompraDatos.GetGanadoCompra(Compra);
                    Compra.ListadoPrecioRangoPesoString = CompraDatos.GetListadoPrecioRangoPeso(Compra).ToJSON();
                    Compra.ListaCorralesString = CompraDatos.GetListaCorrales(Compra).ToJSON();
                    Compra.ListaFierrosString = CompraDatos.GetListaFierros(Compra).ToJSON();

                    return View(Compra);
                }
                catch (Exception ex)
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "No se puede cargar la vista, error: " + ex.ToString();
                    return View(Compra);
                }
            }
        }
        #endregion
        #region RecepcionCompra
        [HttpGet]
        public ActionResult RecepcionCompra(string IDCompra)
        {
            Token.SaveToken();
            if (string.IsNullOrEmpty(IDCompra))
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista.";
                return RedirectToAction("Index");
            }
            else
            {
                try
                {
                    Compra = new CompraModels();
                    CompraDatos = new _Compra_Datos();
                    Compra.IDCompra = IDCompra;
                    Compra.Conexion = Conexion;
                    Compra = CompraDatos.GetRecepcionCompra(Compra);
                    if(Compra.RecepcionOrigen == null)
                    {
                        Compra.RecepcionOrigen = new RecepcionOrigenModels();
                        Compra.RecepcionOrigen.Inicializar();
                    }

                    return View(Compra);
                }
                catch (Exception ex)
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "No se puede cargar la vista, error: " + ex.ToString();
                    return View(Compra);
                }
            }
        }
        #endregion
        #region Funcion Json Documentos
        [HttpPost]
        public ActionResult TableJsonDocumentos(string IDCompra)
        {
            try
            {
                Compra = new CompraModels();
                CompraDatos = new _Compra_Datos();
                Compra.Conexion = Conexion;
                Compra.IDCompra = IDCompra;

                Compra.RespuestaAjax.Mensaje = CompraDatos.GetDocumentosDataTable(Compra);
                Compra.RespuestaAjax.Success = true;

                return Content(Compra.RespuestaAjax.Mensaje, "application/json");

            }
            catch (Exception ex)
            {
                Compra.RespuestaAjax.Mensaje = ex.ToString();
                Compra.RespuestaAjax.Success = false;
                return Content(Compra.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        public ActionResult TableJsonDocumentosDetalles(string IDCompra)
        {
            try
            {
                Compra = new CompraModels();
                CompraDatos = new _Compra_Datos();
                Compra.Conexion = Conexion;
                Compra.IDCompra = IDCompra;

                Compra.RespuestaAjax.Mensaje = CompraDatos.GetDocumentosPorCobrarDetalles(Compra);
                Compra.RespuestaAjax.Success = true;

                return Content(Compra.RespuestaAjax.Mensaje, "application/json");

            }
            catch (Exception ex)
            {
                Compra.RespuestaAjax.Mensaje = ex.ToString();
                Compra.RespuestaAjax.Success = false;
                return Content(Compra.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        #endregion
        #region Funcion Json Index
        [HttpPost]
        public ActionResult JsonIndex(CompraModels Compra)
        {
            try
            {
                CompraDatos = new _Compra_Datos();
                Compra.Conexion = Conexion;

                Compra.RespuestaAjax.Mensaje = CompraDatos.ObtenerCompraIndexDataTable(Compra);
                Compra.RespuestaAjax.Success = true;

                return Content(Compra.RespuestaAjax.Mensaje, "application/json");

            }
            catch (Exception ex)
            {
                Compra.RespuestaAjax.Mensaje = ex.ToString();
                Compra.RespuestaAjax.Success = false;
                return Content(Compra.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        #endregion
        #region Funcion Json Ganado
        [HttpPost]
        public ActionResult TableJsonGanadoCompra(string  IDCompra)
        {
            try
            {
                CompraDatos = new _Compra_Datos();
                Compra = new CompraModels();
                Compra.Conexion = Conexion;
                Compra.IDCompra = IDCompra;
                Compra.RespuestaAjax.Mensaje = CompraDatos.TableJsonGanadoCompra(Compra);
                Compra.RespuestaAjax.Success = true;

                return Content(Compra.RespuestaAjax.Mensaje, "application/json");

            }
            catch (Exception ex)
            {
                Compra.RespuestaAjax.Mensaje = ex.ToString();
                Compra.RespuestaAjax.Success = false;
                return Content(Compra.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        #endregion
        #region Funcion Json Eventos
        [HttpPost]
        public ActionResult TableJsonEventosCompra(string IDCompra)
        {
            try
            {
                CompraDatos = new _Compra_Datos();
                Compra = new CompraModels();
                Compra.Conexion = Conexion;
                Compra.IDCompra = IDCompra;
                Compra.RespuestaAjax.Mensaje = CompraDatos.TableJsonEventoCompra(Compra);
                Compra.RespuestaAjax.Success = true;

                return Content(Compra.RespuestaAjax.Mensaje, "application/json");

            }
            catch (Exception ex)
            {
                Compra.RespuestaAjax.Mensaje = ex.ToString();
                Compra.RespuestaAjax.Success = false;
                return Content(Compra.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        #endregion
        #region Funcion Json GanadoSinAccidenteXIDEvento
        [HttpPost]
        public ActionResult TableJsonProductoGanadoCargadoLibreEvento(string IDCompra, int IDEvento)
        {
            try
            {
                EventoEnvioModels Evento = new EventoEnvioModels();
                CompraDatos = new _Compra_Datos();
                Evento.Conexion = Conexion;
                Evento.IDCompra = IDCompra;
                Evento.IDEvento = IDEvento;
                Evento.RespuestaAjax = new RespuestaAjax();
                Evento.RespuestaAjax.Mensaje = CompraDatos.GetProductoGanadoNoAccidentadoXIDCompra(Evento);
                Evento.RespuestaAjax.Success = true;

                return Content(Evento.RespuestaAjax.Mensaje, "application/json");

            }
            catch (Exception ex)
            {
                Compra = new CompraModels();
                Compra.RespuestaAjax.Mensaje = ex.ToString();
                Compra.RespuestaAjax.Success = false;
                return Content(Compra.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        #endregion
        #region Funcion Json GanadoConAccidenteXIDEvento
        [HttpPost]
        public ActionResult TableJsonProductoGanadoCargadoConEvento(string IDCompra, int IDEvento)
        {
            try
            {
                EventoEnvioModels Evento = new EventoEnvioModels();
                CompraDatos = new _Compra_Datos();
                Evento.Conexion = Conexion;
                Evento.IDCompra = IDCompra;
                Evento.IDEvento = IDEvento;
                Evento.RespuestaAjax = new RespuestaAjax();
                Evento.RespuestaAjax.Mensaje = CompraDatos.GetProductoGanadoAccidentadoXIDEvento(Evento);
                Evento.RespuestaAjax.Success = true;

                return Content(Evento.RespuestaAjax.Mensaje, "application/json");

            }
            catch (Exception ex)
            {
                Compra = new CompraModels();
                Compra.RespuestaAjax.Mensaje = ex.ToString();
                Compra.RespuestaAjax.Success = false;
                return Content(Compra.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        #endregion
        #region Index
        // GET: Admin/Compra
        public ActionResult Index()
        {
            try
            {
                Compra = new CompraModels();
                return View(Compra);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Edit
        [HttpGet]
        public ActionResult Edit(string IDCompra)
        {
            Compra = new CompraModels();
            CompraDatos = new _Compra_Datos();
            //Asigno valores para los querys
            Compra.Conexion = Conexion;
            Compra.IDCompra = IDCompra;
            //Obtengo los datos de la compra
            Compra.Estatus = CompraDatos.GetEstatusCompra(Compra);

            switch (Compra.Estatus)
            {
                case 0:
                    return RedirectToAction("AgendarCompra", "Compra", new { IDCompra = Compra.IDCompra });
                case 1:
                    return RedirectToAction("AgendarCompra", "Compra", new { IDCompra = Compra.IDCompra });
                case 2:
                    return RedirectToAction("GanadoCompra", "Compra", new { IDCompra = Compra.IDCompra });
                case 3:
                    return RedirectToAction("RecepcionCompra", "Compra", new { IDCompra = Compra.IDCompra });
                default:
                    return View(Compra);
            }
        }
        #endregion
        #region Funciones AC DEL
        #region Proveedor
        [HttpPost]
        public ActionResult AC_Proveedor(CompraModels Compra)
        {
            try
            {
                string[] camposValidar = { "IDProveedor", "IDSucursal", "IDPLugarProveedor", "GanadosPactadoMachos", "GanadosPactadoHembras", "FechaHoraProgramada" };
                bool formularioValido = true;
                var errors = ModelState.Keys.Where(k => ModelState[k].Errors.Count > 0).Select(k => new { propertyName = k, errorMessage = ModelState[k].Errors[0].ErrorMessage });

                foreach (var item in errors)
                {
                    for (int j = 0; j < camposValidar.Count(); j++)
                    {

                        if (string.Equals(item.propertyName, camposValidar[j]))
                        {
                            formularioValido = false;
                            break;
                        }
                    }
                }

                if (formularioValido)
                {
                    CompraDatos = new _Compra_Datos();
                    Compra.Conexion = Conexion;
                    Compra.Usuario = User.Identity.Name;
                    Compra = CompraDatos.Compras_ac_Proveedor(Compra);

                    Compra.RespuestaAjax.Mensaje = Compra.Mensaje;
                    Compra.RespuestaAjax.Success = Compra.Completado;

                    return Content(Compra.RespuestaAjax.ToJSON(), "application/json");
                }
                else
                {
                    Compra.RespuestaAjax.Mensaje = "Verifique su formulario.";
                    Compra.RespuestaAjax.Success = false;
                    return Content(Compra.RespuestaAjax.ToJSON(), "application/json");
                }
            }
            catch (Exception ex)
            {
                Compra.RespuestaAjax.Mensaje = ex.ToString();
                Compra.RespuestaAjax.Success = false;
                return Content(Compra.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        #endregion
        #region Flete
        [HttpPost]
        public ActionResult AC_Flete(CompraModels Compra)
        {
            try
            {
                string[] camposValidar = { "IDEmpresa", "IDSucursal", "IDChofer", "IDVehiculo", "Flete.kmInicialVehiculo" };
                bool formularioValido = true;
                var errors = ModelState.Keys.Where(k => ModelState[k].Errors.Count > 0).Select(k => new { propertyName = k, errorMessage = ModelState[k].Errors[0].ErrorMessage });

                foreach (var item in errors)
                {
                    for (int j = 0; j < camposValidar.Count(); j++)
                    {

                        if (string.Equals(item.propertyName, camposValidar[j]))
                        {
                            formularioValido = false;
                            break;
                        }
                    }
                }
                if (formularioValido)
                {
                    CompraDatos = new _Compra_Datos();
                    Compra.Conexion = Conexion;
                    Compra.Usuario = User.Identity.Name;
                    if(Compra.DocumentosPorCobrarDetallePagos.HttpImagen == null)
                    {
                        Compra.DocumentosPorCobrarDetallePagos.ImagenBase64 = Compra.DocumentosPorCobrarDetallePagos.ImagenMostrar;
                    }
                    else
                    {
                        Compra.DocumentosPorCobrarDetallePagos.ImagenBase64 = Auxiliar.ImageToBase64(Compra.DocumentosPorCobrarDetallePagos.HttpImagen);
                    }

                    Compra = CompraDatos.Compras_ac_Flete(Compra);

                    return Content(Compra.RespuestaAjax.ToJSON(), "application/json");
                }
                else
                {
                    Compra.RespuestaAjax.Mensaje = "Verifique su formulario.";
                    Compra.RespuestaAjax.Success = false;
                    return Content(Compra.RespuestaAjax.ToJSON(), "application/json");
                }
            }
            catch (Exception ex)
            {
                Compra.RespuestaAjax.Mensaje = ex.ToString();
                Compra.RespuestaAjax.Success = false;
                return Content(Compra.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        #endregion
        #region Ganado
        [HttpPost]
        public ActionResult AC_Ganado(string IDCompra, string IDGanado, string numArete, string id_genero,
            decimal peso, decimal repeso, decimal merma, decimal peso_pagar, decimal costo_kilo, int id_corral, string Id_detalleDocumentoPorCobrar,
            int indiceActual, string id_fierro1, string id_fierro2, string id_fierro3)
        {
            try
            {
                CompraDatos = new _Compra_Datos();
                Compra = new CompraModels();
                Compra.IDCompra = IDCompra;
                Compra.Ganado.id_Ganados = IDGanado;
                Compra.Ganado.numArete = numArete;
                Compra.Ganado.genero = id_genero;
                Compra.Ganado.IDEstatusGanado = 1;//PREDETERMINADO ADEMAS EN LA BD POR DEFAULT TAMBIEN SE INGRESA COMO 1 = SALUDABLE
                Compra.Ganado.CompraGanado.PesoInicial = peso;
                Compra.Ganado.CompraGanado.PesoFinal = repeso;
                Compra.Ganado.CompraGanado.Merma = merma;
                Compra.Ganado.CompraGanado.PesoPagado = peso_pagar;
                Compra.Ganado.CompraGanado.PrecioKilo = costo_kilo;
                Compra.Ganado.IDCorral = id_corral;
                Compra.Ganado.CompraGanado.Id_detalleDocumentoPorCobrar = Id_detalleDocumentoPorCobrar;
                Compra.Ganado.IDFierro1 = id_fierro1;
                Compra.Ganado.IDFierro2 = id_fierro2;
                Compra.Ganado.IDFierro3 = id_fierro3;

                Compra.Conexion = Conexion;
                Compra.Usuario = User.Identity.Name;
                Compra = CompraDatos.Compras_ac_Ganado(Compra, indiceActual);
                //Compra.RespuestaAjax.Mensaje = "{\"id_ganado\" : \"1\", \"id_detalleDoctoCobrar\" : \"0\", \"indiceActual\" : \"" + indiceActual + "\", \"CantidadMachos\" : \"1\", \"CantidadHembras\" : \"1\", \"CantidadTotal\" : \"1\", \"MermaMachos\" : \"1\" , \"MermaHembras\" : \"2\" , \"MermaTotal\" : \"3\", \"KilosMachos\" : \"4\" , \"KilosHembras\" : \"5\", \"KilosTotal\" : \"6\", \"MontoTotalGanado\" : \"7\"}";
                //Compra.RespuestaAjax.Success = true;

                return Content(Compra.RespuestaAjax.ToJSON(), "application/json");
            }
            catch (Exception ex)
            {
                Compra.RespuestaAjax.Mensaje = ex.ToString();
                Compra.RespuestaAjax.Success = false;
                return Content(Compra.RespuestaAjax.ToJSON(), "application/json");
            }
        }

        [HttpPost]
        public ActionResult DEL_Ganado(string IDCompra, string IDGanado, string Id_detalleDocumentoPorCobrar)
        {
            try
            {
                CompraDatos = new _Compra_Datos();
                Compra = new CompraModels();
                Compra.IDCompra = IDCompra;
                Compra.Ganado.id_Ganados = IDGanado;
                Compra.Ganado.CompraGanado.Id_detalleDocumentoPorCobrar = Id_detalleDocumentoPorCobrar;
                Compra.Conexion = Conexion;
                Compra.Usuario = User.Identity.Name;

                Compra = CompraDatos.Compras_del_Ganado(Compra);

                return Content(Compra.RespuestaAjax.ToJSON(), "application/json");
            }
            catch (Exception ex)
            {
                Compra.RespuestaAjax.Mensaje = ex.ToString();
                Compra.RespuestaAjax.Success = false;
                return Content(Compra.RespuestaAjax.ToJSON(), "application/json");
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
                    if (Evento.IDCompra.Length == 36)
                    {
                        CompraDatos = new _Compra_Datos();
                        Evento.Conexion = Conexion;
                        Evento.Usuario = User.Identity.Name;
                        Evento.RespuestaAjax = new RespuestaAjax();

                        //verificamos si tiene alguna imagen
                        if (Evento.HttpImagen != null)
                        {
                            Evento.ImagenBase64 = Auxiliar.ImageToBase64(Evento.HttpImagen);
                        }
                        else
                        {
                            //ya que no es obligatorio la imagen
                            Evento.ImagenBase64 = Evento.ImagenMostrar;
                        }

                        Evento = CompraDatos.AC_Evento(Evento);
                        Token.ResetToken();
                        Token.SaveToken();
                        return Content(Evento.RespuestaAjax.ToJSON(), "application/json");
                    }
                    else
                    {
                        TempData["typemessage"] = "2";
                        TempData["message"] = "Evento no válido.";
                        return RedirectToAction("Index", "Compra");
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Compra");
                }
            }
            catch (Exception ex)
            {
                Evento.RespuestaAjax.Mensaje = ex.ToString();
                Evento.RespuestaAjax.Success = false;
                return Content(Evento.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        public ActionResult DEL_Evento(int IDEvento, string IDCompra)
        {
            try
            {
                if (Token.IsTokenValid())
                {
                    if (IDCompra.Length == 36)
                    {
                        CompraDatos = new _Compra_Datos();
                        EventoEnvioModels Evento = new EventoEnvioModels();
                        Evento.Conexion = Conexion;
                        Evento.Usuario = User.Identity.Name;
                        Evento.IDEvento = IDEvento;
                        Evento.RespuestaAjax = new RespuestaAjax();

                        Evento = CompraDatos.DEL_Evento(Evento);
                        Token.ResetToken();
                        Token.SaveToken();
                        return Content(Evento.RespuestaAjax.ToJSON(), "application/json");
                    }
                    else
                    {
                        TempData["typemessage"] = "2";
                        TempData["message"] = "Evento no válido.";
                        return RedirectToAction("Index", "Compra");
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Compra");
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
        #region Recepción Origen
        public ActionResult AC_RecepcionOrigen(CompraModels Compra)
        {
            try
            {
                if (Token.IsTokenValid())
                {
                    if (Compra.IDCompra.Length == 36)
                    {
                        CompraDatos = new _Compra_Datos();
                        Compra.Conexion = Conexion;
                        Compra.Usuario = User.Identity.Name;
                        Compra.RespuestaAjax = new RespuestaAjax();


                        Compra = CompraDatos.AC_RecepcionOrigen(Compra);
                        Token.ResetToken();
                        Token.SaveToken();
                        return Content(Compra.RespuestaAjax.ToJSON(), "application/json");
                    }
                    else
                    {
                        TempData["typemessage"] = "2";
                        TempData["message"] = "Compra no válido.";
                        return RedirectToAction("Index", "Compra");
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Compra");
                }
            }
            catch (Exception ex)
            {
                Compra = new CompraModels();
                Compra.RespuestaAjax.Mensaje = ex.ToString();
                Compra.RespuestaAjax.Success = false;
                return Content(Compra.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        #endregion


        #endregion
        #region Funciones combo
        #region Chofer
        [HttpPost]
        public ActionResult GetChoferesXIDEmpresa(string IDEmpresa)
        {
            try
            {
                Compra = new CompraModels();
                CompraDatos = new _Compra_Datos();
                Compra.Conexion = Conexion;
                Compra.IDEmpresa = IDEmpresa;
                Compra.Usuario = User.Identity.Name;
                Compra.ListaChoferes = CompraDatos.GetChoferesXIDEmpresa(Compra);

                return Content(Compra.ListaChoferes.ToJSON(), "application/json");
            }
            catch
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error. Por favor contacte a soporte técnico";
                return Json("");
            }
        }
        #endregion
        #region Vehiculo
        [HttpPost]
        public ActionResult GetVehiculosXIDEmpresa(string IDEmpresa)
        {
            try
            {
                Compra = new CompraModels();
                CompraDatos = new _Compra_Datos();
                Compra.Conexion = Conexion;
                Compra.IDEmpresa = IDEmpresa;
                Compra.Usuario = User.Identity.Name;
                Compra.ListaVehiculos = CompraDatos.GetVehiculosXIDEmpresa(Compra);

                return Content(Compra.ListaVehiculos.ToJSON(), "application/json");
            }
            catch
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error. Por favor contacte a soporte técnico";
                return Json("");
            }
        }
        #endregion
        #region Jaula
        [HttpPost]
        public ActionResult GetJaulasXIDEmpresa(string IDEmpresa)
        {
            try
            {
                Compra = new CompraModels();
                CompraDatos = new _Compra_Datos();
                Compra.Conexion = Conexion;
                Compra.IDEmpresa = IDEmpresa;
                Compra.Usuario = User.Identity.Name;
                Compra.ListaJaulas = CompraDatos.GetJaulasXIDEmpresa(Compra);

                return Content(Compra.ListaJaulas.ToJSON(), "application/json");
            }
            catch
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error. Por favor contacte a soporte técnico";
                return Json("");
            }
        }
        #endregion
        #region Remolque
        [HttpPost]
        public ActionResult GetRemolquesXIDEmpresa(string IDEmpresa)
        {
            try
            {
                Compra = new CompraModels();
                CompraDatos = new _Compra_Datos();
                Compra.Conexion = Conexion;
                Compra.IDEmpresa = IDEmpresa;
                Compra.Usuario = User.Identity.Name;
                Compra.ListaRemolques = CompraDatos.GetRemolquesXIDEmpresa(Compra);

                return Content(Compra.ListaRemolques.ToJSON(), "application/json");
            }
            catch
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error. Por favor contacte a soporte técnico";
                return Json("");
            }
        }
        #endregion
        #region Lugar
        [HttpPost]
        public ActionResult GetLugaresProveedorXIDProveedor(string IDProveedor)
        {
            try
            {
                Compra = new CompraModels();
                CompraDatos = new _Compra_Datos();
                Compra.Conexion = Conexion;
                Compra.IDProveedor = IDProveedor;
                Compra.Usuario = User.Identity.Name;
                Compra.ListaLugaresProveedor = CompraDatos.GetListadoLugaresProveedorXIDProveedor(Compra);

                return Content(Compra.ListaLugaresProveedor.ToJSON(), "application/json");
            }
            catch
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error. Por favor contacte a soporte técnico";
                return Json("");
            }
        }
        [HttpPost]
        public ActionResult GetLugaresXIDEmpresa(string IDEmpresa)
        {
            try
            {
                Compra = new CompraModels();
                CompraDatos = new _Compra_Datos();
                Compra.Conexion = Conexion;
                Compra.IDEmpresa = IDEmpresa;
                Compra.Usuario = User.Identity.Name;
                Compra.ListaLugares = CompraDatos.GetListadoLugaresLugarXIDEmpresa(Compra);

                return Content(Compra.ListaLugares.ToJSON(), "application/json");
            }
            catch
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error. Por favor contacte a soporte técnico";
                return Json("");
            }
        }
        #endregion
        #region Cuentas bancarias proveedor
        [HttpPost]
        public ActionResult GetListadoCuentasBancariasProveedorXIDProveedor(string IDProveedor)
        {
            try
            {
                Compra = new CompraModels();
                CompraDatos = new _Compra_Datos();
                Compra.Conexion = Conexion;
                Compra.IDProveedor = IDProveedor;
                Compra.DocumentosPorCobrarDetallePagos = new DocumentosPorCobrarDetallePagosModels();
                Compra.DocumentosPorCobrarDetallePagos.ListaCuentasBancariasProveedor = CompraDatos.GetListadoCuentasBancariasProveedorXIDProveedor(Compra);

                return Content(Compra.DocumentosPorCobrarDetallePagos.ListaCuentasBancariasProveedor.ToJSON(), "application/json");
            }
            catch
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error. Por favor contacte a soporte técnico";
                return Json("");
            }
        }
        #endregion
        #endregion

        #region Modales
        #region ListaPrecios
        public ActionResult ModalListaPrecios(int IDTipoProveedor)
        {
            Compra = new CompraModels();
            CompraDatos = new _Compra_Datos();
            Compra.Proveedor.IDTipoProveedor = IDTipoProveedor;
            Compra.Conexion = Conexion;
            Compra.ListaRangoPeso = CompraDatos.GetListadoPrecioRangoPeso(Compra);

            return PartialView("ModalListadoPrecios", Compra);
        }
        #endregion
        #region Evento
        public ActionResult ModalEvento(string IDEvento)
        {
            CompraDatos = new _Compra_Datos();
            EventoEnvioModels Evento = new EventoEnvioModels();
            Evento.IDEvento = int.Parse(IDEvento);
            Evento.Conexion = Conexion;

            Evento = CompraDatos.GetEventoXIDEvento(Evento);
            Evento.ListaEventos = CompraDatos.GetListaTiposEventos(Evento);

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
        #endregion
        #region Documento
        [HttpPost]
        public ActionResult ModalDocumento(string IDCompra, string IDDocumento)
        {
            {
                CompraDatos = new _Compra_Datos();
                DocumentoCompraModels Documento = new DocumentoCompraModels();
                Documento.IDCompra = IDCompra;
                Documento.IDDocumento = IDDocumento;
                Documento.Conexion = Conexion;

                Documento = CompraDatos.GetDocumentoXIDDocumento(Documento);
                Documento.ListaTipoDocumentos = CompraDatos.GetListaTiposDocumentos(Documento);

                return PartialView("ModalDocumentos", Documento);
            }
        }
        #endregion
        #endregion

        #region Documento
        public ActionResult AC_Documento(DocumentoCompraModels Documento)
        {
            try
            {
                if (Token.IsTokenValid())
                {
                    if (Documento.IDCompra.Length == 36)
                    {
                        CompraDatos = new _Compra_Datos();
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
                        Documento = CompraDatos.AC_Documento(Documento);
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
        public ActionResult DEL_Documento(DocumentoCompraModels Documento)
        {
            try
            {
                if (Token.IsTokenValid())
                {
                    if (Documento.IDDocumento.Length == 36)
                    {
                        CompraDatos = new _Compra_Datos();
                        Documento.Conexion = Conexion;
                        Documento.Usuario = User.Identity.Name;
                        Documento = CompraDatos.DEL_DocumentoXIDDocumento(Documento);
                        Token.ResetToken();
                        Token.SaveToken();
                        return Content(Documento.RespuestaAjax.ToJSON(), "application/json");
                    }
                    else
                    {
                        TempData["typemessage"] = "2";
                        TempData["message"] = "Compra no válido.";
                        return RedirectToAction("Index", "Compra");
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Compra");
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

        [HttpGet]
        public ActionResult CambiarEstatus(string IDCompra)
        {
            Compra = new CompraModels();
            CompraDatos = new _Compra_Datos();
            Compra.Conexion = Conexion;
            Compra.IDCompra = IDCompra;
            Compra = CompraDatos.CambiarEstatusCompra(Compra);

            if (Compra.RespuestaAjax.Success)
            {
                TempData["typemessage"] = "1";
                TempData["message"] = "Estatus cambiado con éxito";
            }
            else
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "No se pudo cambiar el estatus de la compra";
            }

            return RedirectToAction("Index", "Compra");
        }
        [HttpGet]
        public ActionResult Transacciones(string IDCompra)
        {
            Compra = new CompraModels();
            CompraDatos = new _Compra_Datos();
            //Asigno valores para los querys
            Compra.Conexion = Conexion;
            Compra.IDCompra = IDCompra;
            //obtengo los generales
            Compra.DocumentoPorCobrar = CompraDatos.GetGeneralesDocumentoPorCobrar(Compra);
            Compra.Id_documentoPorCobrar = Compra.DocumentoPorCobrar.Id_documentoCobrar;
            Compra.DocumentoPorPagar = CompraDatos.GetGeneralesDocumentoPorPagar(Compra);
            Compra.Id_documentoPorPagar = Compra.DocumentoPorPagar.IDDocumentoPagar;

            return View(Compra);
        }
        /********************************************************************/
        //Funciones imagenes - fierro
        #region Imágenes
        [HttpPost]
        public ContentResult SaveImageFierro(string IDCompra)
        {
            string jsString = "{}";
            try
            {
                foreach (string file in Request.Files)
                {
                    var fileContent = Request.Files[file];
                    if (fileContent != null && fileContent.ContentLength > 0)
                    {
                        //Obtengo el stream
                        var stream = fileContent.InputStream;
                        //Genero un array de bytes
                        byte[] fileData = null;
                        using (var binaryReader = new BinaryReader(stream))
                        {
                            fileData = binaryReader.ReadBytes(fileContent.ContentLength);
                        }
                        //Realizo la convercion a base 64
                        string Base64 = Convert.ToBase64String(fileData);
                        //Ya tengo la imagen ahora la guardo en la bd
                        HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                        FormsAuthenticationTicket Ticket = FormsAuthentication.Decrypt(authCookie.Value);

                        Compra = new CompraModels();
                        CompraDatos = new _Compra_Datos();

                        Compra.IDCompra = IDCompra;
                        Compra.Conexion = Conexion;
                        Compra.Fierro.ImgFierro = Base64;
                        Compra.Fierro.NombreFierro = Request.Files[file].FileName;
                        Compra.IDUsuario = Ticket.Name;
                        Compra = CompraDatos.SaveImageFierro(Compra);

                        jsString = "{ \"initialPreview\":[\"<img class='file-preview-image' style='width: auto; height: auto; max-width: 100 %; max-height: 100%; ' src='data: image/png; base64, " + Base64 + "' />\"],\"initialPreviewConfig\":[{\"caption\":\"" + Compra.Fierro.NombreFierro + "\",\"size\":" + fileContent.ContentLength + ",\"width\":\"50px\",\"url\":\"DeleteImageFierro\",\"key\":\"" + Compra.Fierro.IDFierro + "\"}]}";

                        return Content(jsString, "application/json");
                    }
                }
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                jsString = "{\"Mensaje de error\": \"" + ex + "\"}";
                return Content(jsString, "application/json");
            }
            return Content(jsString, "application/json");
        }
        [HttpPost]
        public ContentResult DeleteImageFierro(string key)
        {
            string jsString;
            try
            {
                HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                FormsAuthenticationTicket Ticket = FormsAuthentication.Decrypt(authCookie.Value);

                Compra = new CompraModels();
                CompraDatos = new _Compra_Datos();
                Compra.Conexion = Conexion;
                Compra.Fierro.IDFierro = key;
                Compra.IDUsuario = Ticket.Name;
                Compra = CompraDatos.DeleteImageFierro(Compra);
                jsString = "{\"Mensaje\": \"" + Compra.Mensaje + "\"}";
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                jsString = "{\"Mensaje de error\": \"" + ex + "\"}";
            }
            return Content(jsString, "application/json");
        }
        #endregion
        /********************************************************************/
        [HttpGet]
        public ActionResult Details(string IDCompra)
        {
            try
            {
                if (IDCompra.Length == 36)
                {
                    Compra = new CompraModels();
                    CompraDatos = new _Compra_Datos();
                    Compra.IDCompra = IDCompra;
                    Compra.Conexion = Conexion;
                    Compra.Usuario = User.Identity.Name;
                    Compra = CompraDatos.GetDetails(Compra);
                    return View(Compra);
                }
                return RedirectToAction("Index", "Compra");
            }
            catch (Exception ex)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista, error: " + ex.ToString();
            }
            return View(Compra);
        }

        [HttpPost]
        public ActionResult JsonGeneralesGanado(string IDCompra)
        {
            try
            {
                CompraDatos = new _Compra_Datos();
                Compra = new CompraModels();
                Compra.Conexion = Conexion;
                Compra.IDCompra = IDCompra;
                Compra.RespuestaAjax.Mensaje = CompraDatos.JsonGeneralesGanado(Compra);
                Compra.RespuestaAjax.Success = true;

                return Content(Compra.RespuestaAjax.Mensaje, "application/json");

            }
            catch (Exception ex)
            {
                Compra.RespuestaAjax.Mensaje = ex.ToString();
                Compra.RespuestaAjax.Success = false;
                return Content(Compra.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        [HttpPost]
        public ActionResult JsonDetallesGanadoMacho(string IDCompra)
        {
            try
            {
                CompraDatos = new _Compra_Datos();
                Compra = new CompraModels();
                Compra.Conexion = Conexion;
                Compra.IDCompra = IDCompra;
                Compra.RespuestaAjax.Mensaje = CompraDatos.JsonDetallesGanadoMacho(Compra);
                Compra.RespuestaAjax.Success = true;

                return Content(Compra.RespuestaAjax.Mensaje, "application/json");

            }
            catch (Exception ex)
            {
                Compra.RespuestaAjax.Mensaje = ex.ToString();
                Compra.RespuestaAjax.Success = false;
                return Content(Compra.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        [HttpPost]
        public ActionResult JsonDetallesGanadoHembra(string IDCompra)
        {
            try
            {
                CompraDatos = new _Compra_Datos();
                Compra = new CompraModels();
                Compra.Conexion = Conexion;
                Compra.IDCompra = IDCompra;
                Compra.RespuestaAjax.Mensaje = CompraDatos.JsonDetallesGanadoHembra(Compra);
                Compra.RespuestaAjax.Success = true;

                return Content(Compra.RespuestaAjax.Mensaje, "application/json");

            }
            catch (Exception ex)
            {
                Compra.RespuestaAjax.Mensaje = ex.ToString();
                Compra.RespuestaAjax.Success = false;
                return Content(Compra.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        [HttpPost]
        public ActionResult JsonDetallesDocXpagar(string IDCompra)
        {
            try
            {
                CompraDatos = new _Compra_Datos();
                Compra = new CompraModels();
                Compra.Conexion = Conexion;
                Compra.IDCompra = IDCompra;
                Compra.RespuestaAjax.Mensaje = CompraDatos.JsonDetallesDocXpagar(Compra);
                Compra.RespuestaAjax.Success = true;

                return Content(Compra.RespuestaAjax.Mensaje, "application/json");

            }
            catch (Exception ex)
            {
                Compra.RespuestaAjax.Mensaje = ex.ToString();
                Compra.RespuestaAjax.Success = false;
                return Content(Compra.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        [HttpPost]
        public ActionResult JsonDetallesDocXcobrar(string IDCompra)
        {
            try
            {
                CompraDatos = new _Compra_Datos();
                Compra = new CompraModels();
                Compra.Conexion = Conexion;
                Compra.IDCompra = IDCompra;
                Compra.RespuestaAjax.Mensaje = CompraDatos.JsonDetallesDocXcobrar(Compra);
                Compra.RespuestaAjax.Success = true;

                return Content(Compra.RespuestaAjax.Mensaje, "application/json");

            }
            catch (Exception ex)
            {
                Compra.RespuestaAjax.Mensaje = ex.ToString();
                Compra.RespuestaAjax.Success = false;
                return Content(Compra.RespuestaAjax.ToJSON(), "application/json");
            }
        }


    }
}
