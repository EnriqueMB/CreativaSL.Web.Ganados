using CreativaSL.Web.Ganados.App_Start;
using CreativaSL.Web.Ganados.Models;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
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

                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult Transacciones(string id)
        {
            try
            {
                Token.SaveToken();
                _VentaGeneral_Datos oDatosVentaGeneral = new _VentaGeneral_Datos();

                string Id = string.IsNullOrEmpty(id) ? string.Empty : id;

                if (!string.IsNullOrEmpty(id))
                {
                    TransaccionesModels oTransaccion = new TransaccionesModels();
                    oTransaccion = oDatosVentaGeneral.VentaGeneral_spCIDDB_get_Transacciones(Id, conexion);
                    if (oTransaccion.Respuesta.Success)
                    {
                        return View(oTransaccion);
                    }
                    else
                    {
                        TempData["typemessage"] = "2";
                        TempData["message"] = "No se puede cargar la vista, error: " + oTransaccion.Respuesta.Mensaje;
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
        public ContentResult DTDetallesDocumentoPorCobrar(string Id, string Id_documentoCobrar)
        {
            try
            {
                _VentaGeneral_Datos oDatosVentaGeneral = new _VentaGeneral_Datos();
                string respuesta = oDatosVentaGeneral.VentaGeneral_spCSLDB_get_DetallesDocumentoPorCobrar(Id, Id_documentoCobrar, conexion);
                return Content(respuesta, "application/json");
            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                return Content(Mensaje.ToJSON(), "application/json");
            }
        }

        [HttpPost]
        public ContentResult DTDetallesDocumentoPorCobrarCobros(string Id, string Id_documentoCobrar)
        {
            try
            {
                _VentaGeneral_Datos oDatosVentaGeneral = new _VentaGeneral_Datos();
                string respuesta = oDatosVentaGeneral.VentaGeneral_spCIDDB_get_DetallesDocumentoPorCobrarCobros(Id, Id_documentoCobrar, conexion);
                return Content(respuesta, "application/json");
            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                return Content(Mensaje.ToJSON(), "application/json");
            }
        }

        [HttpPost]
        public ContentResult DTDetallesDocumentoPorCobrarDeducciones(string Id, string Id_documentoCobrar)
        {
            try
            {
                _PesajeGanado_Datos oDatosPesaje = new _PesajeGanado_Datos();
                string respuesta = oDatosPesaje.PesajeGanado_spCSLDB_Venta_DetallesDocumentoPorCobrarDeducciones(Id, Id_documentoCobrar, conexion);

                return Content(respuesta, "application/json");

            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");

                return Content(Mensaje.ToJSON(), "application/json");
            }
        }

        [HttpGet]
        public ActionResult Cobro(string id_1, string id_2)
        {
            try
            {
                Token.SaveToken();
                _VentaGeneral_Datos oDatosVentaGeneral = new _VentaGeneral_Datos();

                DocumentosPorCobrarDetallePagosModels DocumentoPorCobrarPago = new DocumentosPorCobrarDetallePagosModels();
                DocumentoPorCobrarPago.RespuestaAjax = new RespuestaAjax();

                string Id_venta = string.IsNullOrEmpty(id_1) ? string.Empty : id_1;
                string Id_documentoPorCobrarDetallePago = string.IsNullOrEmpty(id_2) ? string.Empty : id_2;

                if (Id_venta.Length == 36 && (Id_documentoPorCobrarDetallePago.Length == 0 || Id_documentoPorCobrarDetallePago.Length == 36))
                {
                    DocumentoPorCobrarPago.Conexion = conexion;
                    DocumentoPorCobrarPago.Id_padre = Id_venta;
                    DocumentoPorCobrarPago.Id_documentoPorCobrarDetallePagos = Id_documentoPorCobrarDetallePago;

                    DocumentoPorCobrarPago = oDatosVentaGeneral.VentaGeneral_spCIDDB_get_GetDetalleDocumentoPago(DocumentoPorCobrarPago);

                    if (DocumentoPorCobrarPago.RespuestaAjax.Success)
                    {
                        if (string.IsNullOrEmpty(DocumentoPorCobrarPago.ImagenBase64))
                        {
                            DocumentoPorCobrarPago.ImagenBase64 = Auxiliar.SetDefaultImage();
                        }

                        DocumentoPorCobrarPago.ExtensionImagenBase64 = Auxiliar.ObtenerExtensionImagenBase64(DocumentoPorCobrarPago.ImagenBase64);

                        _Combos_Datos oDatosCombo = new _Combos_Datos();

                        DocumentoPorCobrarPago.ListaFormaPagos = oDatosCombo.GetListadoCFDIFormaPago(DocumentoPorCobrarPago);
                        DocumentoPorCobrarPago = oDatosVentaGeneral.VentaGeneral_spCIDDB_get_NombreCliente_Empresa(DocumentoPorCobrarPago);
                        DocumentoPorCobrarPago.TipoCuentaBancaria = 1;
                        DocumentoPorCobrarPago.ListaCuentasBancariasEmpresa = oDatosVentaGeneral.VentaGeneral_spCIDDB_get_CuentasBancarias(DocumentoPorCobrarPago);
                        DocumentoPorCobrarPago.TipoCuentaBancaria = 2;
                        DocumentoPorCobrarPago.ListaCuentasBancariasProveedor = oDatosVentaGeneral.VentaGeneral_spCIDDB_get_CuentasBancarias(DocumentoPorCobrarPago);
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
        public ActionResult Cobro(DocumentosPorCobrarDetallePagosModels DocumentoPorCobrarPago)
        {
            try
            {
                if (Token.IsTokenValid())
                {

                    DocumentoPorCobrarPago.Usuario = User.Identity.Name;
                    DocumentoPorCobrarPago.Conexion = conexion;
                    DocumentoPorCobrarPago.RespuestaAjax = new RespuestaAjax();
                    if (DocumentoPorCobrarPago.Bancarizado)
                    {
                        if (DocumentoPorCobrarPago.HttpImagen != null)
                        {
                            DocumentoPorCobrarPago.ImagenBase64 = Auxiliar.ImageToBase64(DocumentoPorCobrarPago.HttpImagen);
                        }
                    }
                    _VentaGeneral_Datos oDatosVentaGeneral = new _VentaGeneral_Datos();

                    DocumentoPorCobrarPago = oDatosVentaGeneral.VentaGeneral_spCIDDB_ac_detallesPago(DocumentoPorCobrarPago);

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
                    return RedirectToAction("Transacciones", "VentaGeneral", new { id = DocumentoPorCobrarPago.Id_padre.Trim() });
                }
                else
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Verifique sus datos.";
                    return RedirectToAction("Index", "VentaGeneral");
                }
            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                TempData["typemessage"] = "2";
                TempData["message"] = "Verifique sus datos, error: " + Mensaje;
                return RedirectToAction("Index", "VentaGeneral");
            }
        }

        [HttpPost]
        public ActionResult DeleteCobro(string id_documentoPorCobrarDetallePagos, string id_documentoPorCobrar)
        {
            try
            {
                if (string.IsNullOrEmpty(id_documentoPorCobrar) || string.IsNullOrEmpty(id_documentoPorCobrarDetallePagos) || id_documentoPorCobrar.Length != 36 || id_documentoPorCobrarDetallePagos.Length != 36)
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Verifique sus datos.";
                    return RedirectToAction("Index");
                }
                _VentaGeneral_Datos oDatosVentaGeneral = new _VentaGeneral_Datos();
                RespuestaAjax respuesta = new RespuestaAjax();
                string usuario = User.Identity.Name;
                respuesta = oDatosVentaGeneral.VentaGeneral_spCIDDB_del_cobro(id_documentoPorCobrar, id_documentoPorCobrarDetallePagos, usuario, conexion);

                TempData["message"] = respuesta.Mensaje;

                if (respuesta.Success)
                {
                    TempData["typemessage"] = "1";
                }
                else
                {
                    TempData["typemessage"] = "2";
                }

                return Content(respuesta.ToJSON(), "application/json");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Comprobante(int? id)
        {
            try
            {
                if (id == null)
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Verifique sus datos";
                    return View("Index");
                }

                Reporte_Datos R = new Reporte_Datos();
                List<ComprobantePagosModels> ListaComprobantePagosDetalles = new List<ComprobantePagosModels>();
                List<VentaGeneralComprobanteDetalleModels> ListaComprobanteDetalles = new List<VentaGeneralComprobanteDetalleModels>();

                _Comprobante_Datos oDatosComprobante = new _Comprobante_Datos();
                _VentaGeneral_Datos oDatosVentaGeneral = new _VentaGeneral_Datos();
                ComprobanteCabeceraModels Cabecera = new ComprobanteCabeceraModels();

                Cabecera = oDatosComprobante.Comprobante_spCSLDB_get_Cabecera(2, id.Value.ToString(), conexion);
                ListaComprobantePagosDetalles = oDatosComprobante.Comprobante_spCIDDB_get_detallesPagos(2, id.Value.ToString(), conexion);
                ListaComprobanteDetalles = oDatosVentaGeneral.VentaGeneral_spCIDDB_Comprobante_get_detalles(conexion, id.Value);

                LocalReport Rtp = new LocalReport();
                Rtp.EnableExternalImages = true;
                Rtp.DataSources.Clear();
                string path = Path.Combine(Server.MapPath("~/Formatos"), "ComprobanteVentaGeneral.rdlc");
                if (System.IO.File.Exists(path))
                {
                    Rtp.ReportPath = path;
                }
                else
                {
                    return RedirectToAction("Index");
                }
                ReportParameter[] Parametros = new ReportParameter[11];
                Parametros[0] = new ReportParameter("urlLogo", Cabecera.LogoEmpresa);
                Parametros[1] = new ReportParameter("nombreEmpresa", Cabecera.NombreEmpresa);
                Parametros[2] = new ReportParameter("rubroEmpresa", Cabecera.RubroEmpresa);
                Parametros[3] = new ReportParameter("direccionEmpresa", Cabecera.DireccionEmpresa);
                Parametros[4] = new ReportParameter("folio", Cabecera.Folio);
                Parametros[5] = new ReportParameter("nombreCliente", Cabecera.NombreCliente);
                Parametros[6] = new ReportParameter("telefonoCliente", Cabecera.TelefonoCliente);
                Parametros[7] = new ReportParameter("rfcCliente", Cabecera.RFCCliente);
                Parametros[8] = new ReportParameter("diaImpresion", Cabecera.DiaImpresion);
                Parametros[9] = new ReportParameter("mesImpresion", Cabecera.MesImpresion);
                Parametros[10] = new ReportParameter("annoImpresion", Cabecera.AnnoImpresion);

                Rtp.SetParameters(Parametros);
                Rtp.DataSources.Add(new ReportDataSource("ListaDetalles", ListaComprobanteDetalles));
                Rtp.DataSources.Add(new ReportDataSource("ListaDetallesCobros", ListaComprobantePagosDetalles));

                string reportType = "PDF";
                string mimeType;
                string encoding;
                string fileNameExtension;

                string deviceInfo = "<DeviceInfo>" +
                "  <OutputFormat>Comprobante</OutputFormat>" +
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

        [HttpGet]
        public ActionResult Detalles(int? id)
        {

            try
            {
                if (id == null)
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "No se puede cargar la vista, verifique sus datos.";
                    return View("Index");
                }

                _VentaGeneral_Datos oDatosVentaGeneral = new _VentaGeneral_Datos();
                VentaGeneralVistaDetallesModels oDetalles = new VentaGeneralVistaDetallesModels();
                oDetalles = oDatosVentaGeneral.VentaGeneral_spCIDDB_get_detalle(id.Value, conexion);

                return View(oDetalles);
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
        public ActionResult DTDetallesDocXcobrarPAGOS(int Id)
        {
            try
            {
                _VentaGeneral_Datos oDatosVentaGeneral = new _VentaGeneral_Datos();
                string respuesta = oDatosVentaGeneral.VentaGeneral_spCIDDB_get_DetallesDocXcobrarPAGOS(Id, conexion);

                return Content(respuesta, "application/json");

            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");

                return Content(Mensaje.ToJSON(), "application/json");
            }
        }

        [HttpPost]
        public ActionResult Delete(int? id)
        {
            try
            {
                RespuestaAjax respuesta = new RespuestaAjax();
                if (id == null || id == 0)
                {
                    respuesta.Success = false;
                    respuesta.Mensaje = "Verifique sus datos.";
                    return Content(respuesta.ToJSON(), "application/json");
                }
                _VentaGeneral_Datos oDatosVentaGeneral = new _VentaGeneral_Datos();
                string usuario = User.Identity.Name;
                respuesta = oDatosVentaGeneral.VentaGeneral_spCIDDB_del(id.Value, conexion, usuario);

                return Content(respuesta.ToJSON(), "application/json");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Finalizar(int? id)
        {
            try
            {
                RespuestaAjax respuesta = new RespuestaAjax();
                if (id == null || id == 0)
                {
                    respuesta.Success = false;
                    respuesta.Mensaje = "Verifique sus datos.";
                    return Content(respuesta.ToJSON(), "application/json");
                }
                _VentaGeneral_Datos oDatosVentaGeneral = new _VentaGeneral_Datos();

                string usuario = User.Identity.Name;
                respuesta = oDatosVentaGeneral.VentaGeneral_spCIDDB_finalizar(id.Value, conexion, usuario);

                return Content(respuesta.ToJSON(), "application/json");
            }
            catch
            {
                return View();
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