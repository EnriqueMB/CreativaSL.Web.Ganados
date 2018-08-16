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
using Microsoft.Reporting.WebForms;

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
                    //Compra.DocumentosPorCobrarDetallePagos = new DocumentosPorCobrarDetallePagosModels();
                    Compra = CompraDatos.GetCompraEmbarque(Compra);
                }
                //if (string.IsNullOrEmpty(Compra.DocumentosPorCobrarDetallePagos.ImagenBase64))
                //{
                //    Compra.DocumentosPorCobrarDetallePagos.ImagenMostrar = Auxiliar.SetDefaultImage();
                //}
                //else
                //{
                //    Compra.DocumentosPorCobrarDetallePagos.ImagenMostrar = Compra.DocumentosPorCobrarDetallePagos.ImagenBase64;
                //}
                //Compra.DocumentosPorCobrarDetallePagos.ExtensionImagenBase64 = Auxiliar.ObtenerExtensionImagenBase64(Compra.DocumentosPorCobrarDetallePagos.ImagenMostrar);
                //Compra.DocumentosPorCobrarDetallePagos.ListaCuentasBancariasEmpresa = CompraDatos.GetListadoCuentasBancariasGrupoOcampo(Compra);
                //Compra.DocumentosPorCobrarDetallePagos.ListaCuentasBancariasProveedor = CompraDatos.GetListadoCuentasBancariasProveedorXIDProveedor(Compra);
                Compra.ListaEmpresas = CompraDatos.GetListadoEmpresas(Compra);
                Compra.ListaSucursales = CompraDatos.GetListadoSucursales(Compra);
                Compra.ListaProveedores = CompraDatos.GetListaProveedores(Compra);
                Compra.ListaLugares = CompraDatos.GetListadoLugaresLugarXIDEmpresa(Compra);
                Compra.ListaChoferes = CompraDatos.GetChoferesXIDEmpresa(Compra);
                Compra.ListaVehiculos = CompraDatos.GetVehiculosXIDEmpresa(Compra);
                Compra.ListaLugaresProveedor = CompraDatos.GetListadoLugaresProveedorXIDProveedor(Compra);
                //Compra.Flete.ListaMetodoPago = CompraDatos.GetMetodosPagos(Compra);
                //Compra.Flete.ListaFormaPago = CompraDatos.GetListadoCFDIFormaPago(Compra);
                //Compra.ListaFierros = CompraDatos.GetListadoFierros(Compra);
                //Compra.Fierro.ImgFierro = Auxiliar.SetDefaultImage();
                //Compra.Fierro.Extension = Auxiliar.ObtenerExtensionImagenBase64(Compra.Fierro.ImgFierro);
                //Compra.ListaFierrosString = Compra.ListaFierros.ToJSON();

                return View(Compra);
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
                    if (Compra.RecepcionOrigen == null)
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
                string[] camposValidar = { "IDProveedor", "IDSucursal", "IDPLugarProveedor", "FechaHoraProgramada" };
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

                    TempData["typemessage"] = "2";
                    TempData["message"] = "Verifique sus datos.";
                    Compra.RespuestaAjax.Success = false;
                    return Content(Compra.RespuestaAjax.ToJSON(), "application/json");
                }
            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                Compra.RespuestaAjax.Mensaje = Mensaje;
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
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                Compra.RespuestaAjax.Mensaje = Mensaje;
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
                string id_ganado = IDGanado.Trim();
                int id_ganadoProgramado = 0;
                bool valido = int.TryParse(id_ganado, out id_ganadoProgramado);

                if (valido)
                {
                    Compra.Id_ganadoProgramado = id_ganadoProgramado;
                    Compra.IDCompra = IDCompra;
                    Compra.Conexion = Conexion;
                    Compra.Usuario = User.Identity.Name;

                    Compra = CompraDatos.Compras_del_GanadoProgramado2(Compra);
                    return Content(Compra.RespuestaAjax.ToJSON(), "application/json");
                }
                else if(IDCompra.Length == 36)
                {
                    Compra.IDCompra = IDCompra;
                    Compra.Ganado.id_Ganados = IDGanado;
                    Compra.Ganado.CompraGanado.Id_detalleDocumentoPorCobrar = Id_detalleDocumentoPorCobrar;
                    Compra.Conexion = Conexion;
                    Compra.Usuario = User.Identity.Name;

                    Compra = CompraDatos.Compras_del_Ganado(Compra);
                    return Content(Compra.RespuestaAjax.ToJSON(), "application/json");
                }
                else
                {
                    Compra.RespuestaAjax.Success = false;
                    Compra.RespuestaAjax.Mensaje = "Verifique sus datos.";
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
        #region Ganado Programado
        [HttpPost]
        public ActionResult AC_GanadoProgramado(string IDCompra, string IDGanado, string numArete, string id_genero,
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
                Compra.Ganado.IDFierro1 = id_fierro1;
                Compra.Ganado.IDFierro2 = id_fierro2;
                Compra.Ganado.IDFierro3 = id_fierro3;

                Compra.Conexion = Conexion;
                Compra.Usuario = User.Identity.Name;
                Compra = CompraDatos.Compras_ac_GanadoProgramado(Compra, indiceActual);
               
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
        public ActionResult DEL_GanadoProgramado(string IDCompra, string IDGanado)
        {
            try
            {
                CompraDatos = new _Compra_Datos();
                Compra = new CompraModels();
                Compra.IDCompra = IDCompra;
                Compra.Ganado.id_Ganados = IDGanado;
                Compra.Conexion = Conexion;
                Compra.Usuario = User.Identity.Name;

                Compra = CompraDatos.Compras_del_GanadoProgramado(Compra);

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
        #region Del impuesto documento por cobrar
        [HttpPost]
        public ActionResult DEL_ImpuestoCobro(CompraImpuestoModels CompraImpuesto)
        {
            try
            {
                if (CompraImpuesto.IDImpuesto.Length == 36)
                {
                    if (Token.IsTokenValid())
                    {
                        CompraDatos = new _Compra_Datos();
                        CompraImpuesto.Conexion = Conexion;
                        CompraImpuesto.Usuario = User.Identity.Name;
                        CompraImpuesto = CompraDatos.Compra_del_ImpuestoDocPorCobrar(CompraImpuesto);

                        if (CompraImpuesto.RespuestaAjax.Success)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = CompraImpuesto.RespuestaAjax.Mensaje;
                            return Content(CompraImpuesto.RespuestaAjax.ToJSON(), "application/json");
                        }
                        else
                        {
                            TempData["typemessage"] = "2";
                            TempData["message"] = CompraImpuesto.RespuestaAjax.Mensaje;
                            return Content(CompraImpuesto.RespuestaAjax.ToJSON(), "application/json");
                        }
                    }
                    else
                    {
                        CompraImpuesto.RespuestaAjax.Success = false;
                        TempData["typemessage"] = "2";
                        TempData["message"] = "Impuesto no válido.";
                        return Content(CompraImpuesto.RespuestaAjax.ToJSON(), "application/json");
                    }
                }
                else
                {
                    CompraImpuesto.RespuestaAjax.Success = false;
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Impuesto no válido.";
                    return Content(CompraImpuesto.RespuestaAjax.ToJSON(), "application/json");
                }
            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                CompraImpuesto.RespuestaAjax.Success = false;
                TempData["typemessage"] = "2";
                TempData["message"] = "Contacte con soporte técnico, error: " + Mensaje;
                return Content(CompraImpuesto.RespuestaAjax.ToJSON(), "application/json");
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
        #region Empresa
        [HttpPost]
        public ActionResult GetEmpresaXTipoFlete(string TipoFlete)
        {
            try
            {
                Compra = new CompraModels();
                CompraDatos = new _Compra_Datos();
                Compra.Conexion = Conexion;
                Compra.TipoFlete = int.Parse(TipoFlete);
                Compra.Usuario = User.Identity.Name;
                Compra.ListaEmpresas = CompraDatos.GetListadoEmpresas(Compra);

                return Content(Compra.ListaEmpresas.ToJSON(), "application/json");
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
                if(string.IsNullOrEmpty(Compra.RespuestaAjax.Mensaje))
                {
                    TempData["message"] = "No se pudo cambiar el estatus de la compra";
                }
                else
                {
                    TempData["message"] = Compra.RespuestaAjax.Mensaje;
                }
                TempData["typemessage"] = "2";
                
            }

            return RedirectToAction("Index", "Compra");
        }

        #region Vista Transacciones
        [HttpGet]
        public ActionResult Transacciones(string IDCompra)
        {
            try
            {
                if(string.IsNullOrEmpty(IDCompra))
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "No se puede cargar la vista, verifique sus datos.";
                    return RedirectToAction("Index", "Compra");
                }
                else if (IDCompra.Length == 36)
                {
                    Token.SaveToken();
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
                return RedirectToAction("Index", "Compra");
            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista, error: " + Mensaje;
                return RedirectToAction("Index", "Compra");
            }
        }
        #region Documentos por cobrar Detalles 
        [HttpPost]
        public ActionResult DatatableDocumentosPorCobrarDetalles(DocumentosPorCobrarDetalleModels Documento)
        {
            try
            {
                CompraDatos = new _Compra_Datos();
                Documento.Conexion = Conexion;
                Documento.Usuario = User.Identity.Name;
                Documento.RespuestaAjax = new RespuestaAjax();

                Documento.RespuestaAjax.Mensaje = CompraDatos.DatatableDocumentosPorCobrarDetalles(Documento);
                Documento.RespuestaAjax.Success = true;

                return Content(Documento.RespuestaAjax.Mensaje, "application/json");

            }
            catch (Exception ex)
            {
                Documento.RespuestaAjax = new RespuestaAjax();
                Documento.RespuestaAjax.Mensaje = ex.ToString();
                Documento.RespuestaAjax.Success = false;
                return Content(Documento.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        #region documentos por cobrar Detalles Pagos

        [HttpPost]
        public ActionResult DatatableDocumentosPorCobrarDetallesPagos(DocumentosPorCobrarDetallePagosModels DocumentoPagos)
        {
            try
            {
                CompraDatos = new _Compra_Datos();
                DocumentoPagos.Conexion = Conexion;
                DocumentoPagos.Usuario = User.Identity.Name;
                DocumentoPagos.RespuestaAjax = new RespuestaAjax();

                DocumentoPagos.RespuestaAjax.Mensaje = CompraDatos.DatatableDocumentosPorCobrarDetallesPagos(DocumentoPagos);
                DocumentoPagos.RespuestaAjax.Success = true;

                return Content(DocumentoPagos.RespuestaAjax.Mensaje, "application/json");

            }
            catch (Exception ex)
            {
                DocumentoPagos.RespuestaAjax = new RespuestaAjax();
                DocumentoPagos.RespuestaAjax.Mensaje = ex.ToString();
                DocumentoPagos.RespuestaAjax.Success = false;
                return Content(DocumentoPagos.RespuestaAjax.ToJSON(), "application/json");
            }
        }

        #endregion

        #region documentos por pagar Detalles Pagos

        [HttpPost]
        public ActionResult DatatableDocumentosPorPagarDetallesPagos(DocumentoPorPagarDetallePagosModels DocumentoPagos)
        {
            try
            {
                CompraDatos = new _Compra_Datos();
                DocumentoPagos.Conexion = Conexion;
                DocumentoPagos.Usuario = User.Identity.Name;
                DocumentoPagos.RespuestaAjax = new RespuestaAjax();

                DocumentoPagos.RespuestaAjax.Mensaje = CompraDatos.DatatableDocumentosPorPagarDetallesPagos(DocumentoPagos);
                DocumentoPagos.RespuestaAjax.Success = true;

                return Content(DocumentoPagos.RespuestaAjax.Mensaje, "application/json");

            }
            catch (Exception ex)
            {
                DocumentoPagos.RespuestaAjax = new RespuestaAjax();
                DocumentoPagos.RespuestaAjax.Mensaje = ex.ToString();
                DocumentoPagos.RespuestaAjax.Success = false;
                return Content(DocumentoPagos.RespuestaAjax.ToJSON(), "application/json");
            }
        }

        #endregion

        #region Documentos por pagar Detalles 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id_1">Documento por pagar</param>
        /// <param name="Id_2">Id de la compra</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DatatableDocumentosPorPagarDetalles(string Id_1, string Id_2)
        {
            try
            {
                CompraDatos = new _Compra_Datos();
                string Mensaje = CompraDatos.DatatableDocumentosPorPagarDetalles(Id_1, Id_2, Conexion);

                return Content(Mensaje, "application/json");

            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                return Content(Mensaje, "application/json");
            }
        }

        #endregion
        #endregion
        #endregion

        #region Vista impuesto documento por cobrar
        /// <summary>
        /// Muestra los impuestos de un producto
        /// </summary>
        /// <param name="Id1">Id de la compra</param>
        /// <param name="Id2">Id del detalle Documento por Cobrar</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ImpuestosProductoServicioCompra(string Id_1, string Id_2)
        {
            try
            {
                CompraImpuestoModels DocumentoPorCobrarDetalleImpuesto = new CompraImpuestoModels();
                CompraDatos = new _Compra_Datos();

                if ((Id_1.Length == 36) && (Id_2.Length == 36))
                {
                    Token.SaveToken();
                    DocumentoPorCobrarDetalleImpuesto.IDCompra = Id_1;
                    DocumentoPorCobrarDetalleImpuesto.Id_detalleDoctoCobrar = Id_2;
                    DocumentoPorCobrarDetalleImpuesto.Conexion = Conexion;
                    DocumentoPorCobrarDetalleImpuesto.RespuestaAjax = new RespuestaAjax();
                    DocumentoPorCobrarDetalleImpuesto = CompraDatos.Get_ImpuestosProductoServicioCompra(DocumentoPorCobrarDetalleImpuesto);

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
        /// <param name="Id1">Id compra</param>
        /// <param name="Id2">Id documento detalle</param>
        /// <param name="Id3">Id documento detalle impuesto</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ImpuestoProductoServicioACCompra(string Id1, string Id2, string Id3)
        {
            try
            {
                Token.SaveToken();

                string Id_compra = string.IsNullOrEmpty(Id1) ? string.Empty : Id1;
                string Id_detalleDocumento = string.IsNullOrEmpty(Id2) ? string.Empty : Id2;

                if (string.IsNullOrEmpty(Id_compra) || string.IsNullOrEmpty(Id_detalleDocumento))
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Verifique sus datos.";
                    return View("Index");
                }
                else
                {
                    string Id_impuesto = string.IsNullOrEmpty(Id3) ? string.Empty : Id3;

                    if (Id_impuesto.Length == 0 || Id_impuesto.Length == 36)
                    {
                        CompraImpuestoModels CompraImpuesto = new CompraImpuestoModels();
                        CompraDatos = new _Compra_Datos();

                        CompraImpuesto.Conexion = Conexion;
                        CompraImpuesto.IDCompra = Id1;
                        CompraImpuesto.Id_detalleDoctoCobrar = Id2;
                        CompraImpuesto.IDImpuesto = Id3;

                        CompraImpuesto.Conexion = Conexion;
                        CompraImpuesto = CompraDatos.GetImpuestoXIDImpuesto(CompraImpuesto);

                        if (CompraImpuesto.RespuestaAjax.Success)
                        {
                            CompraImpuesto.ListaImpuesto = CompraDatos.GetListadoImpuesto(CompraImpuesto);
                            CompraImpuesto.ListaTipoImpuesto = CompraDatos.GetListadoTipoImpuesto(CompraImpuesto);
                            CompraImpuesto.ListaTipoFactor = CompraDatos.GetListadoTipoFactor(CompraImpuesto);
                        }
                        else
                        {
                            TempData["typemessage"] = "2";
                            TempData["message"] = "No se puede cargar la vista, error: " + CompraImpuesto.RespuestaAjax.Mensaje;
                            return View("Index");
                        }

                        return View(CompraImpuesto);
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
        public ActionResult ImpuestoProductoServicioACCompra(CompraImpuestoModels CompraImpuesto)
        {
            try
            {
                if (Token.IsTokenValid())
                {
                    CompraDatos = new _Compra_Datos();
                    CompraImpuesto.Conexion = Conexion;
                    CompraImpuesto.Usuario = User.Identity.Name;
                    CompraImpuesto.RespuestaAjax = new RespuestaAjax();
                    CompraImpuesto = CompraDatos.Compra_ac_ImpuestoProductoServicio(CompraImpuesto);

                    if (CompraImpuesto.RespuestaAjax.Success)
                    {
                        TempData["typemessage"] = "1";
                        TempData["message"] = CompraImpuesto.RespuestaAjax.Mensaje;
                        Token.ResetToken();
                        return RedirectToAction("ImpuestosProductoServicioCompra", "Compra", new { Id_1 = CompraImpuesto.IDCompra, Id_2 = CompraImpuesto.Id_detalleDoctoCobrar });
                    }
                    else
                    {
                        TempData["typemessage"] = "2";
                        TempData["message"] = CompraImpuesto.RespuestaAjax.Mensaje;
                        return RedirectToAction("ImpuestosProductoServicioCompra", "Compra", new { Id_1 = CompraImpuesto.IDCompra, Id_2 = CompraImpuesto.Id_detalleDoctoCobrar });
                    }
                }
                else
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Verifique sus datos.";
                    return RedirectToAction("Index", "Compra");
                }
            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                TempData["typemessage"] = "2";
                TempData["message"] = "Verifique sus datos, error: " + Mensaje;
                return RedirectToAction("Index", "Compra");
            }
        }
        #endregion

        #region Vista ProductoServicio
        [HttpGet]
        public ActionResult ProductoServicioCompra(string Id_compra, string Id_documentoPorCobrar, string Id_detalleDocumento)
        {
            try
            {
                DocumentosPorCobrarDetalleModels DocumentoPorCobrarDetalle = new DocumentosPorCobrarDetalleModels();
                CompraDatos = new _Compra_Datos();

                //0 = nuevo, 36 = editar, pero ambos son válidos
                if ((Id_compra.Length == 36) && (Id_documentoPorCobrar.Length == 36) && (Id_detalleDocumento.Length == 0 || Id_detalleDocumento.Length == 36 || string.IsNullOrEmpty(Id_detalleDocumento)))
                {
                    Token.SaveToken();
                    DocumentoPorCobrarDetalle.Id_servicio = Id_compra;
                    DocumentoPorCobrarDetalle.Id_documentoCobrar = Id_documentoPorCobrar;
                    DocumentoPorCobrarDetalle.Id_detalleDoctoCobrar = Id_detalleDocumento;
                    DocumentoPorCobrarDetalle.Conexion = Conexion;
                    DocumentoPorCobrarDetalle.RespuestaAjax = new RespuestaAjax();
                    DocumentoPorCobrarDetalle = CompraDatos.GetDetalleDocumentoPorCobrar(DocumentoPorCobrarDetalle);
                    DocumentoPorCobrarDetalle.ListaTipoClasificacionCobro = CompraDatos.GetListadoTipoClasificacion(DocumentoPorCobrarDetalle);
                    DocumentoPorCobrarDetalle.ListaProductosServiciosCFDI = CompraDatos.GetListadoCFDIProductosServiciosCompra(DocumentoPorCobrarDetalle);

                    DocumentoPorCobrarDetalle.ListaAlmacen = CompraDatos.GetAlmacenesHabilitados(DocumentoPorCobrarDetalle);
                    DocumentoPorCobrarDetalle.ListaProductos = CompraDatos.GetProductosAlmacen(DocumentoPorCobrarDetalle);

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
        public ActionResult ProductoServicioCompra(DocumentosPorCobrarDetalleModels DocumentoPorCobrarDetalle)
        {
            try
            {
                if (Token.IsTokenValid())
                {
                    CompraDatos = new _Compra_Datos();
                    DocumentoPorCobrarDetalle.Conexion = Conexion;
                    DocumentoPorCobrarDetalle.Usuario = User.Identity.Name;
                    DocumentoPorCobrarDetalle.RespuestaAjax = new RespuestaAjax();
                    DocumentoPorCobrarDetalle = CompraDatos.AC_ProductoServicio(DocumentoPorCobrarDetalle);

                    if (DocumentoPorCobrarDetalle.RespuestaAjax.Success)
                    {
                        TempData["typemessage"] = "1";
                        TempData["message"] = DocumentoPorCobrarDetalle.RespuestaAjax.Mensaje;
                        Token.ResetToken();
                        return RedirectToAction("Transacciones", "Compra", new { IDCompra = DocumentoPorCobrarDetalle.Id_servicio });
                    }
                    else
                    {
                        TempData["typemessage"] = "2";
                        TempData["message"] = DocumentoPorCobrarDetalle.RespuestaAjax.Mensaje;
                        return RedirectToAction("ProductoServicioCompra", "Compra", new { Id_compra = DocumentoPorCobrarDetalle.Id_servicio, Id_documentoPorCobrar = DocumentoPorCobrarDetalle.Id_documentoCobrar, Id_detalleDocumento = DocumentoPorCobrarDetalle.Id_detalleDoctoCobrar });
                    }
                }
                else
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Verifique sus datos.";
                    return RedirectToAction("Index", "Compra");
                }
            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                TempData["typemessage"] = "2";
                TempData["message"] = "Verifique sus datos, error: " + Mensaje;
                return RedirectToAction("Index", "Compra");
            }
        }
        [HttpPost]
        public ActionResult Del_ProductoServicio(DocumentosPorCobrarDetalleModels documento)
        {
            try
            {
                if (Token.IsTokenValid())
                {
                    CompraDatos = new _Compra_Datos();
                    documento.Conexion = Conexion;
                    documento.Usuario = User.Identity.Name;
                    documento.RespuestaAjax = new RespuestaAjax();
                    documento = CompraDatos.DEL_ProductoServicioCompra(documento);

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
        public ActionResult EditProductoServicioCompra(string Id_compra, string Id_documentoPorCobrar, string Id_detalleDocumento)
        {
            try
            {
                DocumentosPorCobrarDetalleModels DocumentoPorCobrarDetalle = new DocumentosPorCobrarDetalleModels();
                CompraDatos = new _Compra_Datos();

                //0 = nuevo, 36 = editar, pero ambos son válidos
                if ((Id_compra.Length == 36) && (Id_documentoPorCobrar.Length == 36) && (Id_detalleDocumento.Length == 0 || Id_detalleDocumento.Length == 36 || string.IsNullOrEmpty(Id_detalleDocumento)))
                {
                    Token.SaveToken();
                    DocumentoPorCobrarDetalle.Id_servicio = Id_compra;
                    DocumentoPorCobrarDetalle.Id_documentoCobrar = Id_documentoPorCobrar;
                    DocumentoPorCobrarDetalle.Id_detalleDoctoCobrar = Id_detalleDocumento;
                    DocumentoPorCobrarDetalle.Conexion = Conexion;
                    DocumentoPorCobrarDetalle.RespuestaAjax = new RespuestaAjax();
                    DocumentoPorCobrarDetalle = CompraDatos.GetDetalleDocumentoPorCobrar(DocumentoPorCobrarDetalle);
                    DocumentoPorCobrarDetalle.ListaProductosServiciosCFDI = CompraDatos.GetListadoCFDIProductosServiciosCompra(DocumentoPorCobrarDetalle);

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

        #region Vista Detalles

        [HttpGet]
        public ActionResult Details(string IDCompra)
        {
            try
            {
                if (string.IsNullOrEmpty(IDCompra))
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Verifique sus datos";
                    return View("Index");
                }
                else if (IDCompra.Length == 36)
                {
                    Compra = new CompraModels();
                    CompraDatos = new _Compra_Datos();
                    Compra.IDCompra = IDCompra;
                    Compra.Conexion = Conexion;
                    Compra.Usuario = User.Identity.Name;
                    Compra = CompraDatos.GetDetails(Compra);
                    return View(Compra);
                }
                TempData["typemessage"] = "2";
                TempData["message"] = "Verifique sus datos";
                return RedirectToAction("Index", "Compra");
            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista, error: " + Mensaje;
            }
            return View("Index");
        }

        [HttpGet]
        public ActionResult EventoDetallesCompra_2(string IDCompra, int Id_eventoCompra)
        {
            {
                if (!string.IsNullOrEmpty(IDCompra))
                {
                    CompraDatos = new _Compra_Datos();
                    EventoCompraModels Evento = new EventoCompraModels();
                    Evento.Id_compra = IDCompra;
                    Evento.Id_eventoCompra = Id_eventoCompra;
                    Evento.Conexion = Conexion;

                    Evento = CompraDatos.GetEventoCompra(Evento);
                    Evento.ListaTiposEventos = CompraDatos.GetListaEventos(Conexion);

                    if (string.IsNullOrEmpty(Evento.ImagenBase64))
                    {
                        Evento.ImagenMostrar = Auxiliar.SetDefaultImage();
                    }
                    else
                    {
                        Evento.ImagenMostrar = Evento.ImagenBase64;
                    }

                    return View(Evento);
                }
                else
                {
                    return View("Index");
                }
            }
        }

        #endregion


        #region Datatable

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
        public ActionResult DatatableDetalleDocXCobrarPagos(string IDCompra)
        {
            try
            {
                CompraDatos = new _Compra_Datos();
                Compra = new CompraModels();
                Compra.Conexion = Conexion;
                Compra.IDCompra = IDCompra;
                Compra.RespuestaAjax.Mensaje = CompraDatos.DatatableDetalleDocXCobrarPagos(Compra);
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
        public ActionResult DatatableDetalleDocXPagarPagos(string IDCompra)
        {
            try
            {
                CompraDatos = new _Compra_Datos();
                Compra = new CompraModels();
                Compra.Conexion = Conexion;
                Compra.IDCompra = IDCompra;
                Compra.RespuestaAjax.Mensaje = CompraDatos.DatatableDetalleDocXPagarPagos(Compra);
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
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                Compra = new CompraModels();
                Compra.RespuestaAjax.Mensaje = Mensaje;
                Compra.RespuestaAjax.Success = false;
                return Content(Compra.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        #endregion
        #region DatatableGanadoProgramado 
        [HttpPost]
        public ActionResult DatatableGanadoProgramado(string IDCompra)
        {
            try
            {
                IDCompra = IDCompra.Trim();
                Compra = new CompraModels();
                CompraDatos = new _Compra_Datos();
                Compra.Conexion = Conexion;
                Compra.IDCompra = IDCompra;

                Compra.RespuestaAjax = new RespuestaAjax();
                Compra.RespuestaAjax.Mensaje = CompraDatos.GetGanadoProgramado(Compra);
                Compra.RespuestaAjax.Success = true;

                return Content(Compra.RespuestaAjax.Mensaje, "application/json");

            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                Compra = new CompraModels();
                Compra.RespuestaAjax.Mensaje = Mensaje;
                Compra.RespuestaAjax.Success = false;
                return Content(Compra.RespuestaAjax.ToJSON(), "application/json");
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
        public ActionResult TableJsonGanadoCompra(string IDCompra)
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
        #region Datatables documentos por cobrar impuestos 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id1">ID de la compra</param>
        /// <param name="Id2">ID detalle del producto o servicio</param>
        /// <returns></returns>
        [HttpPost]
        public ContentResult DatatableImpuestoXIdDocDetalle(string Id1, string Id2)
        {
            CompraImpuestoModels CompraImpuesto = new CompraImpuestoModels();
            CompraDatos = new _Compra_Datos();
            CompraImpuesto.Conexion = Conexion;
            CompraImpuesto.IDCompra = Id1;
            CompraImpuesto.Id_detalleDoctoCobrar = Id2;
            CompraImpuesto.RespuestaAjax.Mensaje = CompraDatos.DatatableImpuestoXIdDocDetalle(CompraImpuesto);

            return Content(CompraImpuesto.RespuestaAjax.Mensaje, "application/json");
        }
        #endregion

        #endregion

        #region Vista Documentos
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id_1">Id de la compra</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DocumentosCompra(string Id_1)
        {
            try
            {
                Token.SaveToken();

                string Id_compra = string.IsNullOrEmpty(Id_1) ? string.Empty : Id_1;
                //0 = nuevo, 36 = edit, si es diferente es un id no valido
                if (Id_compra.Length == 36)
                {
                    _Compra_Datos CompraDatos = new _Compra_Datos();
                    DocumentoModels Documento = new DocumentoModels();
                    Documento.Id_servicio = Id_1;
                    Documento.Conexion = Conexion;
                    Documento = CompraDatos.GetGeneralesDocumentosCompra(Documento);
                    Documento.ListaConceptosSalidaDeduccion = CompraDatos.GetListadoTipoClasificacionPago(Documento);

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
                    CompraDatos = new _Compra_Datos();
                    Documento.Conexion = Conexion;
                    Documento.Usuario = User.Identity.Name;
                    Documento = CompraDatos.AC_CostoDocumentos(Documento);
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
        public ActionResult DocumentoCompra(string IDCompra, string IDDocumento)
        {
            {
                if(IDCompra.Length == 0 || IDCompra.Length == 36)
                {
                    CompraDatos = new _Compra_Datos();
                    DocumentoModels Documento = new DocumentoModels();
                    Documento.Id_servicio = IDCompra;
                    Documento.IDDocumento = IDDocumento;
                    Documento.Conexion = Conexion;

                    Documento = CompraDatos.GetDocumentoXIDDocumento(Documento);
                    Documento.ListaTipoDocumentos = CompraDatos.GetListaTiposDocumentos(Documento);

                    return View(Documento);
                }
                else
                {
                    return View("Index");
                }
            }
        }
        [HttpPost]
        public ActionResult DocumentoCompra(DocumentoModels Documento)
        {
            try
            {
                if (Token.IsTokenValid())
                {
                    if (Documento.Id_servicio.Length == 36)
                    {
                        CompraDatos = new _Compra_Datos();
                        Documento.Conexion = Conexion;
                        Documento.Usuario = User.Identity.Name;

                        if (Documento.ImagenPost != null)
                        {
                            Documento.ImagenServer = Auxiliar.ImageToBase64(Documento.ImagenPost);
                        }
                        Documento.RespuestaAjax = new RespuestaAjax();
                        Documento = CompraDatos.AC_Documento(Documento);
                        if (Documento.RespuestaAjax.Success)
                        {
                            Token.ResetToken();
                            TempData["typemessage"] = "1";
                            TempData["message"] = Documento.RespuestaAjax.Mensaje;
                            return RedirectToAction("DocumentosCompra", "Compra", new { Id_1 = Documento.Id_servicio });
                        }
                        else
                        {
                            TempData["typemessage"] = "2";
                            TempData["message"] = Documento.RespuestaAjax.Mensaje;
                            return RedirectToAction("Index", "Compra");
                        }
                    }
                    else
                    {
                        TempData["typemessage"] = "2";
                        TempData["message"] = "Verifique sus datos.";
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
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista, error: " + Mensaje;
                return RedirectToAction("Index", "Compra");
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

        #region Vista Cobro
        /// <summary>
        /// Vista que actualiza o crea un cobro
        /// </summary>
        /// <param name="id_1">El id de la compra</param>
        /// <param name="id_2">El id del documento por cobrar detalle pago</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CobroCompra(string id_1, string id_2)
        {
            try
            {
                Token.SaveToken();
                CompraDatos = new _Compra_Datos();
                DocumentosPorCobrarDetallePagosModels DocumentoPorCobrarPago = new DocumentosPorCobrarDetallePagosModels();
                DocumentoPorCobrarPago.RespuestaAjax = new RespuestaAjax();

                string Id_compra = string.IsNullOrEmpty(id_1) ? string.Empty : id_1;
                string Id_documentoPorCobrarDetallePago = string.IsNullOrEmpty(id_2) ? string.Empty : id_2;

                if (Id_compra.Length == 36 && (Id_documentoPorCobrarDetallePago.Length == 0 || Id_documentoPorCobrarDetallePago.Length == 36))
                {
                    DocumentoPorCobrarPago.Conexion = Conexion;
                    DocumentoPorCobrarPago.Id_padre = Id_compra;
                    DocumentoPorCobrarPago.Id_documentoPorCobrarDetallePagos = Id_documentoPorCobrarDetallePago;

                    DocumentoPorCobrarPago = CompraDatos.GetDetalleDocumentoPago(DocumentoPorCobrarPago);

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

                        Compra = new CompraModels();
                        Compra.Conexion = Conexion;
                        DocumentoPorCobrarPago.ListaFormaPagos = CompraDatos.GetListadoCFDIFormaPago(Compra);
                        DocumentoPorCobrarPago = CompraDatos.GetNombreEmpresaCliente(DocumentoPorCobrarPago);
                        DocumentoPorCobrarPago.TipoCuentaBancaria = 1;
                        DocumentoPorCobrarPago.ListaCuentasBancariasEmpresa = CompraDatos.GetListadoCuentasBancarias(DocumentoPorCobrarPago);
                        DocumentoPorCobrarPago.TipoCuentaBancaria = 2;
                        DocumentoPorCobrarPago.ListaCuentasBancariasProveedor = CompraDatos.GetListadoCuentasBancarias(DocumentoPorCobrarPago);
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
        public ActionResult CobroCompra(DocumentosPorCobrarDetallePagosModels DocumentoPorCobrarPago)
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
                    CompraDatos = new _Compra_Datos();

                    DocumentoPorCobrarPago = CompraDatos.AC_ComprobanteCobro(DocumentoPorCobrarPago);

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
                    return RedirectToAction("Transacciones", "Compra", new { IDCompra = DocumentoPorCobrarPago.Id_padre });
                }
                else
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Verifique sus datos.";
                    return RedirectToAction("Index", "Compra");
                }
            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                TempData["typemessage"] = "2";
                TempData["message"] = "Verifique sus datos, error: " + Mensaje;
                return RedirectToAction("Index", "Compra");
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
                    CompraDatos = new _Compra_Datos();
                    DocumentoPorCobrarPago = CompraDatos.DEL_ComprobanteCobro(DocumentoPorCobrarPago);

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

        #region Vista Pago
        /// <summary>
        /// Vista que actualiza o crea un cobro
        /// </summary>
        /// <param name="id_1">El id de la compra</param>
        /// <param name="id_2">El id del documento por pagar detalle pago</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PagoCompra(string id_1, string id_2)
        {
            try
            {
                Token.SaveToken();
                CompraDatos = new _Compra_Datos();
                DocumentoPorPagarDetallePagosModels DocumentoPorPagarPago = new DocumentoPorPagarDetallePagosModels();
                DocumentoPorPagarPago.RespuestaAjax = new RespuestaAjax();

                string Id_compra = string.IsNullOrEmpty(id_1) ? string.Empty : id_1;
                string Id_documentoPorPagarDetallePago = string.IsNullOrEmpty(id_2) ? string.Empty : id_2;

                if (Id_compra.Length == 36 && (Id_documentoPorPagarDetallePago.Length == 0 || Id_documentoPorPagarDetallePago.Length == 36))
                {
                    DocumentoPorPagarPago.Conexion = Conexion;
                    DocumentoPorPagarPago.Id_padre = Id_compra;
                    DocumentoPorPagarPago.Id_documentoPorPagarDetallePagos = Id_documentoPorPagarDetallePago;

                    DocumentoPorPagarPago = CompraDatos.GetDetalleDocumentoXPagar(DocumentoPorPagarPago);

                    if (DocumentoPorPagarPago.RespuestaAjax.Success)
                    {
                        if (string.IsNullOrEmpty(DocumentoPorPagarPago.ImagenBase64))
                        {
                            DocumentoPorPagarPago.ImagenMostrar = Auxiliar.SetDefaultImage();
                        }
                        else
                        {
                            DocumentoPorPagarPago.ImagenMostrar = DocumentoPorPagarPago.ImagenBase64;
                        }
                        DocumentoPorPagarPago.ExtensionImagenBase64 = Auxiliar.ObtenerExtensionImagenBase64(DocumentoPorPagarPago.ImagenMostrar);

                        Compra = new CompraModels();
                        Compra.Conexion = Conexion;
                        DocumentoPorPagarPago.ListaFormaPagos = CompraDatos.GetListadoCFDIFormaPago(Compra);
                        DocumentoPorPagarPago = CompraDatos.GetNombreEmpresaCliente_PorPagar(DocumentoPorPagarPago);
                        DocumentoPorPagarPago.TipoCuentaBancaria = 1;
                        DocumentoPorPagarPago.ListaCuentasBancariasEmpresa = CompraDatos.GetListadoCuentasBancarias_Pagos(DocumentoPorPagarPago);
                        DocumentoPorPagarPago.TipoCuentaBancaria = 2;
                        DocumentoPorPagarPago.ListaCuentasBancariasProveedor = CompraDatos.GetListadoCuentasBancarias_Pagos(DocumentoPorPagarPago);
                        return View(DocumentoPorPagarPago);
                    }
                    else
                    {
                        TempData["typemessage"] = "2";
                        TempData["message"] = "No se puede cargar la vista, error: " + DocumentoPorPagarPago.RespuestaAjax.Mensaje;
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
        public ActionResult PagoCompra(DocumentoPorPagarDetallePagosModels DocumentoPorPagarPago)
        {
            try
            {
                if (Token.IsTokenValid())
                {

                    DocumentoPorPagarPago.Usuario = User.Identity.Name;
                    DocumentoPorPagarPago.Conexion = Conexion;
                    DocumentoPorPagarPago.RespuestaAjax = new RespuestaAjax();
                    if (DocumentoPorPagarPago.Bancarizado)
                    {
                        if (DocumentoPorPagarPago.HttpImagen == null)
                        {
                            DocumentoPorPagarPago.ImagenBase64 = DocumentoPorPagarPago.ImagenMostrar;
                        }
                        else
                        {
                            DocumentoPorPagarPago.ImagenBase64 = Auxiliar.ImageToBase64(DocumentoPorPagarPago.HttpImagen);
                        }
                    }
                    CompraDatos = new _Compra_Datos();

                    DocumentoPorPagarPago = CompraDatos.AC_ComprobantePago(DocumentoPorPagarPago);

                    if (DocumentoPorPagarPago.RespuestaAjax.Success)
                    {
                        TempData["typemessage"] = "1";
                        TempData["message"] = "Datos guardados correctamente.";
                        Token.ResetToken();
                    }
                    else
                    {
                        TempData["typemessage"] = "2";
                        TempData["message"] = DocumentoPorPagarPago.RespuestaAjax.Mensaje;

                    }
                    return RedirectToAction("Transacciones", "Compra", new { IDCompra = DocumentoPorPagarPago.Id_padre });
                }
                else
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Verifique sus datos.";
                    return RedirectToAction("Index", "Compra");
                }
            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                TempData["typemessage"] = "2";
                TempData["message"] = "Verifique sus datos, error: " + Mensaje;
                return RedirectToAction("Index", "Compra");
            }
        }
        #region Del comprobante PAGO
        public ActionResult DelComprobantePago(DocumentoPorPagarDetallePagosModels DocumentoPorPagarPago)
        {
            try
            {
                if (Token.IsTokenValid())
                {
                    DocumentoPorPagarPago.Usuario = User.Identity.Name;
                    DocumentoPorPagarPago.Conexion = Conexion;
                    DocumentoPorPagarPago.RespuestaAjax = new RespuestaAjax();
                    CompraDatos = new _Compra_Datos();
                    DocumentoPorPagarPago = CompraDatos.DEL_ComprobantePago(DocumentoPorPagarPago);

                    if (DocumentoPorPagarPago.RespuestaAjax.Success)
                    {
                        TempData["typemessage"] = "1";
                        TempData["message"] = DocumentoPorPagarPago.RespuestaAjax.Mensaje;
                    }
                    else
                    {
                        TempData["typemessage"] = "2";
                        TempData["message"] = DocumentoPorPagarPago.RespuestaAjax.Mensaje;
                    }

                    return Content(DocumentoPorPagarPago.RespuestaAjax.ToJSON(), "application/json");
                }
                else
                {
                    DocumentoPorPagarPago.RespuestaAjax = new RespuestaAjax();
                    DocumentoPorPagarPago.RespuestaAjax.Success = false;

                    TempData["typemessage"] = "2";
                    TempData["message"] = "Verifique sus datos";

                    return Content(DocumentoPorPagarPago.RespuestaAjax.ToJSON(), "application/json");
                }
            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                DocumentoPorPagarPago.RespuestaAjax = new RespuestaAjax();
                DocumentoPorPagarPago.RespuestaAjax.Mensaje = "Verifique sus datos, error: " + Mensaje;
                return Content(DocumentoPorPagarPago.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        #endregion
        #endregion

        #region Otros
        #region ImagenesProveedor
        public ActionResult ImagenesProveedor(string id)
        {
            try
            {

                Reporte_Datos R = new Reporte_Datos();
                CompraModels Compra = new CompraModels();
                _Compra_Datos reporteDatos = new _Compra_Datos();
               
                Compra.IDCompra = id;
                Compra.Conexion = Conexion;
                Compra = reporteDatos.GetImagenesProveedor(Compra);

                LocalReport Rtp = new LocalReport();
                Rtp.EnableExternalImages = true;
                Rtp.DataSources.Clear();
                string path = Path.Combine(Server.MapPath("~/Formatos"), "ImagenesProveedor.rdlc");
                if (System.IO.File.Exists(path))
                {
                    Rtp.ReportPath = path;
                }
                else
                {
                    return RedirectToAction("Index", "Compra");
                }
                ReportParameter[] Parametros = new ReportParameter[3];
                Parametros[0] = new ReportParameter("IneProveedor", Compra.Proveedor.ImgINE);
                Parametros[1] = new ReportParameter("ManifestacionFierroProveedor", Compra.Proveedor.ImgManifestacionFierro);
                Parametros[2] = new ReportParameter("UPPProveedor", Compra.UPP);

                Rtp.SetParameters(Parametros);
                
                string reportType = "PDF";
                string mimeType;
                string encoding;
                string fileNameExtension;

                string deviceInfo = "<DeviceInfo>" +
                "  <OutputFormat>" + "Imagenes Proveedor "+ "</OutputFormat>" +
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
        #region Ganado Programado
        public ActionResult GanadoProgramado(string id)
        {
            try
            {

                Reporte_Datos R = new Reporte_Datos();
                CompraModels Compra = new CompraModels();
                _Compra_Datos reporteDatos = new _Compra_Datos();
                List<ReporteGanadoModels> Reporte = new List<ReporteGanadoModels>();
                CatEmpresaModels Empresa = new CatEmpresaModels();
                _CatEmpresa_Datos EmpresaDatos = new _CatEmpresa_Datos();
                Empresa.Conexion = Conexion;
                Empresa = EmpresaDatos.GetDatosEmpresaPrincipal(Empresa);

                Compra.IDCompra = id;
                Compra.Conexion = Conexion;
                Reporte = reporteDatos.GetReporteGanadoDetalles(Compra);

                int TotalGanadoMachos = 0, TotalGanadoHembras = 0, TotalGanado = 0;

                for (int i = 0; i < Reporte.Count; i++)
                {
                    if (string.Equals(Reporte[i].Genero.Trim(), "MACHO"))
                    {
                        TotalGanadoMachos += 1;
                    }
                    else if (string.Equals(Reporte[i].Genero.Trim(), "HEMBRA"))
                    {
                        TotalGanadoHembras += 1;
                    }
                }

                TotalGanado = TotalGanadoMachos + TotalGanadoHembras;

                LocalReport Rtp = new LocalReport();
                Rtp.EnableExternalImages = true;
                Rtp.DataSources.Clear();
                string path = Path.Combine(Server.MapPath("~/Formatos"), "ListadoGanadoParaCompra.rdlc");
                if (System.IO.File.Exists(path))
                {
                    Rtp.ReportPath = path;
                }
                else
                {
                    return RedirectToAction("Index", "Compra");
                }

                string GeneralesEmpresa = "<b>Representante: </b>" + Empresa.Representante + "<br/>";
                GeneralesEmpresa += "<b>RFC: </b>" + Empresa.RFC + "<br/>";
                GeneralesEmpresa += "<b>Horario de atención: </b>" + Empresa.HorarioAtencion + "<br/>";
                string Telefonos = string.IsNullOrEmpty(Empresa.NumTelefonico1) ? string.Empty : Empresa.NumTelefonico1;
                Telefonos += string.IsNullOrEmpty(Empresa.NumTelefonico2) ? string.Empty : " " + Empresa.NumTelefonico1;
                if (!string.IsNullOrEmpty(Telefonos))
                    GeneralesEmpresa += "<b>Teléfono(s): </b>" + Telefonos + "<br/>";
                if (!string.IsNullOrEmpty(Empresa.Email))
                    GeneralesEmpresa += "<b>Email: </b>" + Empresa.Email;

                ReportParameter[] Parametros = new ReportParameter[7];
                Parametros[0] = new ReportParameter("LogoEmpresa", Empresa.LogoEmpresa);
                Parametros[1] = new ReportParameter("NombreEmpresa", Empresa.RazonFiscal);
                Parametros[2] = new ReportParameter("DireccionEmpresa", Empresa.DireccionFiscal);
                Parametros[3] = new ReportParameter("GeneralesEmpresa", GeneralesEmpresa);
                Parametros[4] = new ReportParameter("TotalGanadoMachos", TotalGanadoMachos.ToString());
                Parametros[5] = new ReportParameter("TotalGanadoHembras", TotalGanadoHembras.ToString());
                Parametros[6] = new ReportParameter("TotalGanado", TotalGanado.ToString());

                Rtp.SetParameters(Parametros);
                Rtp.DataSources.Add(new ReportDataSource("ListaGanado", Reporte));
                Rtp.Refresh();

                string reportType = "EXCEL";
                string mimeType;
                string encoding;
                string fileNameExtension;

                string deviceInfo = "<DeviceInfo>" +
                "  <OutputFormat>" + "Ganado programado" + "</OutputFormat>" +
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id_1">Id del fierro</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetFierroXIDFierro(string Id_1)
        {
            try
            {
                CompraDatos = new _Compra_Datos();
                Compra = new CompraModels();
                Compra.Conexion = Conexion;
                Compra.Fierro.IDFierro = Id_1;
                Compra.RespuestaAjax = CompraDatos.GetFierroXIDFierro(Compra);

                return Content(Compra.RespuestaAjax.ToJSON(), "application/json");

            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                Compra.RespuestaAjax.Mensaje = Mensaje;
                Compra.RespuestaAjax.Success = false;
                return Content(Compra.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        #region Comprobante compra
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id_1">Id compra</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ComprobanteCompra(string Id_1)
        {
            try
            {
                Reporte_Datos R = new Reporte_Datos();
                List<ComprobanteCompraDetallesModels> ListaComprobanteCompraDetalles = new List<ComprobanteCompraDetallesModels>();
                CompraDatos = new _Compra_Datos();
                Compra = new CompraModels();
                ComprobanteCompraCabeceraModels Cabecera = new ComprobanteCompraCabeceraModels();
                Compra.IDCompra = Id_1;
                Compra.Conexion = Conexion;
                Cabecera = CompraDatos.GetComprobanteCompraCabecera(Compra);
                ListaComprobanteCompraDetalles = CompraDatos.GetComprobanteCompraDetalles(Compra);                

                LocalReport Rtp = new LocalReport();
                Rtp.EnableExternalImages = true;
                Rtp.DataSources.Clear();
                string path = Path.Combine(Server.MapPath("~/Formatos"), "ComprobanteCompra.rdlc");
                if (System.IO.File.Exists(path))
                {
                    Rtp.ReportPath = path;
                }
                else
                {
                    return RedirectToAction("Index", "Compra");
                }
                ReportParameter[] Parametros = new ReportParameter[12];
                Parametros[0] = new ReportParameter("urlLogo", Cabecera.UrlLogo);
                Parametros[1] = new ReportParameter("nombreEmpresa", Cabecera.NombreEmpresa);
                Parametros[2] = new ReportParameter("rubroEmpresa", Cabecera.RubroEmpresa);
                Parametros[3] = new ReportParameter("telefonoEmpresa", Cabecera.TelefonoEmpresa);
                Parametros[4] = new ReportParameter("direccionEmpresa", Cabecera.DireccionEmpresa);
                Parametros[5] = new ReportParameter("folio", Cabecera.Folio);
                Parametros[6] = new ReportParameter("nombreProveedor", Cabecera.NombreProveedor);
                Parametros[7] = new ReportParameter("telefonoProveedor", Cabecera.TelefonoProveedor);
                Parametros[8] = new ReportParameter("rfcProveedor", Cabecera.RFCProveedor);
                Parametros[9] = new ReportParameter("diaImpresion", Cabecera.DiaImpresion);
                Parametros[10] = new ReportParameter("mesImpresion", Cabecera.MesImpresion);
                Parametros[11] = new ReportParameter("annoImpresion", Cabecera.AnnoImpresion);

                Rtp.SetParameters(Parametros);
                Rtp.DataSources.Add(new ReportDataSource("ComprobanteCompraDetalles", ListaComprobanteCompraDetalles));
                string reportType = "PDF";
                string mimeType;
                string encoding;
                string fileNameExtension;

                string deviceInfo = "<DeviceInfo>" +
                "  <OutputFormat>ComprobanteCompra</OutputFormat>" +
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
            }
            return View(Compra);
        }
        #endregion
        #endregion

        #region Vista Evento

        [HttpPost]
        public ActionResult DatatableGanadoDisponibleXIDCompra(string IDCompra)
        {
            try
            {
                CompraDatos = new _Compra_Datos();
                Compra = new CompraModels();
                Compra.Conexion = Conexion;
                Compra.IDCompra = IDCompra;
                Compra.RespuestaAjax.Mensaje = CompraDatos.DatatableGanadoDisponibleXIDCompra(Compra);
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
        public ActionResult DatatableGanadoEventoXIDCompra(string IDCompra, int Id_eventoCompra)
        {
            try
            {
                CompraDatos = new _Compra_Datos();
                EventoCompraModels Evento = new EventoCompraModels();
                Evento.Id_compra = IDCompra;
                Evento.Id_eventoCompra = Id_eventoCompra;
                Evento.Conexion = Conexion;
                Evento.RespuestaAjax = new RespuestaAjax();
                Evento.RespuestaAjax.Mensaje = CompraDatos.DatatableGanadoEventoXIDCompra(Evento);
                Evento.RespuestaAjax.Success = true;

                return Content(Evento.RespuestaAjax.Mensaje, "application/json");

            }
            catch (Exception ex)
            {
                Compra.RespuestaAjax.Mensaje = ex.ToString();
                Compra.RespuestaAjax.Success = false;
                return Content(Compra.RespuestaAjax.ToJSON(), "application/json");
            }
        }

        [HttpGet]
        public ActionResult EventoCompra(string IDCompra, int Id_eventoCompra)
        {
            {
                if (!string.IsNullOrEmpty(IDCompra))
                {
                    CompraDatos = new _Compra_Datos();
                    EventoCompraModels Evento = new EventoCompraModels();
                    Evento.Id_compra = IDCompra;
                    Evento.Id_eventoCompra = Id_eventoCompra;
                    Evento.Conexion = Conexion;

                    Evento = CompraDatos.GetEventoCompra(Evento);
                    Evento.ListaTiposEventos = CompraDatos.GetListaEventos(Conexion);

                    if(string.IsNullOrEmpty(Evento.ImagenBase64))
                    {
                        Evento.ImagenMostrar = Auxiliar.SetDefaultImage();
                    }
                    else
                    {
                        Evento.ImagenMostrar = Evento.ImagenBase64;
                    }
                    Token.SaveToken();
                    return View(Evento);
                }
                else
                {
                    return View("Index");
                }
            }
        }

        [HttpPost]
        public ActionResult EventoCompra(EventoCompraModels Evento)
        {
            try
            {
                if (Token.IsTokenValid())
                {
                    CompraDatos = new _Compra_Datos();
                    Evento.Conexion = Conexion;
                    Evento.Usuario = User.Identity.Name;
                    Evento.RespuestaAjax = new RespuestaAjax();

                    if(Evento.HttpImagen != null)
                    {
                        Evento.ImagenBase64 = Auxiliar.ImageToBase64(Evento.HttpImagen);
                    }

                    Evento = CompraDatos.Compra_a_Evento(Evento);

                    if (Evento.RespuestaAjax.Success)
                    {
                        TempData["typemessage"] = "1";
                        TempData["message"] = Evento.RespuestaAjax.Mensaje;
                        Token.ResetToken();
                    }
                    else
                    {
                        TempData["typemessage"] = "2";
                        TempData["message"] = Evento.RespuestaAjax.Mensaje;
                        Token.ResetToken();
                    }
                    return Content(Evento.RespuestaAjax.ToJSON(), "application/json");
                }
                else
                {
                    Evento.RespuestaAjax = new RespuestaAjax();
                    Evento.Success = false;
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Verifique sus datos.";
                    return Content(Evento.RespuestaAjax.ToJSON(), "application/json");
                }
            }
            catch(Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                Evento.RespuestaAjax = new RespuestaAjax();
                Evento.Success = false;
                TempData["typemessage"] = "2";
                TempData["message"] = "Verifique sus datos. Error: " + Mensaje;
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
                        Evento.IDCompra = IDCompra;
                        Evento.RespuestaAjax = new RespuestaAjax();
                        Evento = CompraDatos.DEL_Evento(Evento);
                        Token.ResetToken();
                        Token.SaveToken();

                        return Content(Evento.RespuestaAjax.ToJSON(), "application/json");
                    }
                    else
                    {
                        EventoEnvioModels Evento = new EventoEnvioModels();
                        Evento.RespuestaAjax = new RespuestaAjax();
                        Evento.RespuestaAjax.Success = false;
                        Evento.RespuestaAjax.Mensaje = "Verifique sus datos";
                        return Content(Evento.RespuestaAjax.ToJSON(), "application/json");
                    }
                }
                else
                {
                    EventoEnvioModels Evento = new EventoEnvioModels();
                    Evento.RespuestaAjax = new RespuestaAjax();
                    Evento.RespuestaAjax.Success = false;
                    Evento.RespuestaAjax.Mensaje = "Verifique sus datos";
                    return Content(Evento.RespuestaAjax.ToJSON(), "application/json");
                }
            }
            catch (Exception ex)
            {
                EventoEnvioModels Evento = new EventoEnvioModels();
                Evento.RespuestaAjax = new RespuestaAjax();
                Evento.RespuestaAjax.Success = false;
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                Evento.RespuestaAjax.Mensaje = Mensaje;
                return Content(Evento.RespuestaAjax.ToJSON(), "application/json");
            }
        }

        [HttpGet]
        public ActionResult EventoDetallesCompra(string IDCompra, int Id_eventoCompra)
        {
            {
                if (!string.IsNullOrEmpty(IDCompra))
                {
                    CompraDatos = new _Compra_Datos();
                    EventoCompraModels Evento = new EventoCompraModels();
                    Evento.Id_compra = IDCompra;
                    Evento.Id_eventoCompra = Id_eventoCompra;
                    Evento.Conexion = Conexion;

                    Evento = CompraDatos.GetEventoCompra(Evento);
                    Evento.ListaTiposEventos = CompraDatos.GetListaEventos(Conexion);

                    if (string.IsNullOrEmpty(Evento.ImagenBase64))
                    {
                        Evento.ImagenMostrar = Auxiliar.SetDefaultImage();
                    }
                    else
                    {
                        Evento.ImagenMostrar = Evento.ImagenBase64;
                    }

                    return View(Evento);
                }
                else
                {
                    return View("Index");
                }
            }
        }
        
        #endregion


    }
}
