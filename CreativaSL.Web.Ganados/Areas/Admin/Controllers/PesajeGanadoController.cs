using CreativaSL.Web.Ganados.App_Start;
using CreativaSL.Web.Ganados.Models;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    public class PesajeGanadoController : Controller
    {
        private TokenProcessor Token = TokenProcessor.GetInstance();
        private string conexion = ConfigurationManager.AppSettings.Get("strConnection");

        // GET: Admin/PesajeGanado
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
                CargarListas();

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
        public ActionResult Create(PesajeGanadoModels oPesaje)
        {
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        _PesajeGanado_Datos oDatosPesaje = new _PesajeGanado_Datos();
                        string usuario = User.Identity.Name;
                        RespuestaAjax respuesta = oDatosPesaje.PesajeGanado_spCIDDB_ac(oPesaje, 1, usuario, conexion);
                        TempData["message"] = respuesta.Mensaje;

                        if (respuesta.Success)
                        {
                            TempData["typemessage"] = "1";
                            Token.ResetToken();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            TempData["typemessage"] = "2";
                            CargarListas();
                            return View(oPesaje);
                        }
                    }
                    else
                    {
                        CargarListas();
                        return View(oPesaje);
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error al intentar guardar los datos. Contacte a soporte técnico.";
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null || id == 0)
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Verifique sus datos.";
                    return RedirectToAction("Index");
                }

                Token.SaveToken();
                PesajeGanadoModels oPesaje = new PesajeGanadoModels();
                _PesajeGanado_Datos oDatosPesaje = new _PesajeGanado_Datos();
                oPesaje = oDatosPesaje.PesajeGanado_spCIDDB_get_pesajeGanadoXID(id.Value, conexion);
                if (oPesaje.Respuesta.Success)
                {
                    CargarListas();
                    return View(oPesaje);
                }
                else
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = oPesaje.Respuesta.Mensaje;
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista.";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Edit(PesajeGanadoModels oPesaje)
        {
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        _PesajeGanado_Datos oDatosPesaje = new _PesajeGanado_Datos();
                        string usuario = User.Identity.Name;
                        RespuestaAjax respuesta = oDatosPesaje.PesajeGanado_spCIDDB_ac(oPesaje, 2, usuario, conexion);
                        TempData["message"] = respuesta.Mensaje;

                        if (respuesta.Success)
                        {
                            TempData["typemessage"] = "1";
                            Token.ResetToken();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            CargarListas();
                            TempData["typemessage"] = "2";
                            return View(oPesaje);
                        }
                    }
                    else
                    {
                        CargarListas();
                        return View(oPesaje);
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error al intentar guardar los datos. Contacte a soporte técnico.";
                return RedirectToAction("Index");
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
                _PesajeGanado_Datos oDatosPesaje = new _PesajeGanado_Datos();
                string usuario = User.Identity.Name;
                respuesta = oDatosPesaje.PesajeGanado_spCIDDB_del(id.Value, conexion, usuario);

                return Content(respuesta.ToJSON(), "application/json");
            }
            catch
            {
                return View();
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
                _PesajeGanado_Datos oDatosPesaje = new _PesajeGanado_Datos();
                RespuestaAjax respuesta = new RespuestaAjax();
                string usuario = User.Identity.Name;
                respuesta = oDatosPesaje.pesajeGanado_spCIDDB_del_cobro(id_documentoPorCobrar, id_documentoPorCobrarDetallePagos, usuario, conexion);

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
        public ActionResult Transacciones(string id)
        {
            try
            {
                Token.SaveToken();
                _PesajeGanado_Datos oDatosPesaje = new _PesajeGanado_Datos();

                string Id = string.IsNullOrEmpty(id) ? string.Empty : id;

                if (!string.IsNullOrEmpty(id))
                {
                    TransaccionesModels oTransaccion = new TransaccionesModels();
                    oTransaccion = oDatosPesaje.PesajeGanado_spCIDDB_get_Transacciones(Id, conexion);
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

        private void CargarListas()
        {
            _Combos_Datos oDatosCombo = new _Combos_Datos();
            ViewBag.ListaSucursal = oDatosCombo.ObtenerComboSucursales(conexion);
            ViewBag.ListaClientes = oDatosCombo.ObtenerComboClientes(conexion);
        }

        public ActionResult DTPesajeGanado()
        {
            try
            {
                _PesajeGanado_Datos oDatosPesaje = new _PesajeGanado_Datos();
                string dt = oDatosPesaje.PesajeGanado_spCIDDB_index(conexion);

                return Content(dt, "application/json");
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public ContentResult DTDetallesDocumentoPorCobrarPesajeGanado(string Id, string Id_documentoCobrar)
        {
            try
            {
                _PesajeGanado_Datos oDatosPesajeGanado = new _PesajeGanado_Datos();
                string respuesta  = oDatosPesajeGanado.PesajeGanado_spCSLDB_get_DetallesDocumentoPorCobrar(Id, Id_documentoCobrar, conexion);
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
                _PesajeGanado_Datos oDatosPesaje = new _PesajeGanado_Datos();
                string respuesta = oDatosPesaje.PesajeGanado_spCSLDB_get_DetallesDocumentoPorCobrarCobros(Id, Id_documentoCobrar, conexion);
                return Content(respuesta, "application/json");
            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                return Content(Mensaje.ToJSON(), "application/json");
            }
        }

        [HttpPost]
        public ContentResult DTDetallesDocumentoPorCobrarVentaDeducciones(string Id, string Id_documentoCobrar)
        {
            try
            {
                _PesajeGanado_Datos oDatosPesaje = new _PesajeGanado_Datos();
                string respuesta  = oDatosPesaje.PesajeGanado_spCSLDB_Venta_DetallesDocumentoPorCobrarDeducciones(Id, Id_documentoCobrar, conexion);

                return Content(respuesta, "application/json");

            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
               
                return Content(Mensaje.ToJSON(), "application/json");
            }
        }

        [HttpPost]
        public ContentResult DTDetallesDocXcobrarPAGOS(int Id)
        {
            try
            {
                _PesajeGanado_Datos oDatosPesaje = new _PesajeGanado_Datos();
                string respuesta = oDatosPesaje.PesajeGanado_spCIDDB_get_DetallesDocXcobrarPAGOS(Id, conexion);

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
                _PesajeGanado_Datos oDatosPesaje = new _PesajeGanado_Datos();

                DocumentosPorCobrarDetallePagosModels DocumentoPorCobrarPago = new DocumentosPorCobrarDetallePagosModels();
                DocumentoPorCobrarPago.RespuestaAjax = new RespuestaAjax();

                string Id_venta = string.IsNullOrEmpty(id_1) ? string.Empty : id_1;
                string Id_documentoPorCobrarDetallePago = string.IsNullOrEmpty(id_2) ? string.Empty : id_2;

                if (Id_venta.Length == 36 && (Id_documentoPorCobrarDetallePago.Length == 0 || Id_documentoPorCobrarDetallePago.Length == 36))
                {
                    DocumentoPorCobrarPago.Conexion = conexion;
                    DocumentoPorCobrarPago.Id_padre = Id_venta;
                    DocumentoPorCobrarPago.Id_documentoPorCobrarDetallePagos = Id_documentoPorCobrarDetallePago;

                    DocumentoPorCobrarPago = oDatosPesaje.PesajeGanado_spCSLDB_get_GetDetalleDocumentoPago(DocumentoPorCobrarPago);

                    if (DocumentoPorCobrarPago.RespuestaAjax.Success)
                    {
                        if (string.IsNullOrEmpty(DocumentoPorCobrarPago.ImagenBase64))
                        {
                            DocumentoPorCobrarPago.ImagenBase64 = Auxiliar.SetDefaultImage();
                        }
                        
                        DocumentoPorCobrarPago.ExtensionImagenBase64 = Auxiliar.ObtenerExtensionImagenBase64(DocumentoPorCobrarPago.ImagenBase64);

                        _Combos_Datos oDatosCombo = new _Combos_Datos();

                        DocumentoPorCobrarPago.ListaFormaPagos = oDatosCombo.GetListadoCFDIFormaPago(DocumentoPorCobrarPago);
                        DocumentoPorCobrarPago = oDatosPesaje.PesajeGanado_spCSLDB_get_NombreCliente_Empresa(DocumentoPorCobrarPago);
                        DocumentoPorCobrarPago.TipoCuentaBancaria = 1;
                        DocumentoPorCobrarPago.ListaCuentasBancariasEmpresa = oDatosPesaje.PesajeGanado_spCIDDB_get_CuentasBancarias(DocumentoPorCobrarPago);
                        DocumentoPorCobrarPago.TipoCuentaBancaria = 2;
                        DocumentoPorCobrarPago.ListaCuentasBancariasProveedor = oDatosPesaje.PesajeGanado_spCIDDB_get_CuentasBancarias(DocumentoPorCobrarPago);
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
                            //DocumentoPorCobrarPago.ImagenBase64 = Auxiliar.ImageToBase64(DocumentoPorCobrarPago.HttpImagen);
                            Stream s = DocumentoPorCobrarPago.HttpImagen.InputStream;
                            
                            if (Path.GetExtension(DocumentoPorCobrarPago.HttpImagen.FileName).ToLower() == ".heic")
                            {
                                Image img = (Image)Auxiliar.ProcessFile(s);
                                Bitmap image = new Bitmap(ComprimirImagen.VaryQualityLevel((Image)img.Clone(), 35L));
                                DocumentoPorCobrarPago.ImagenBase64 = image.ToBase64String(ImageFormat.Jpeg);
                            }
                            else
                            {
                                Image img = new Bitmap(s);
                                Bitmap image = new Bitmap(ComprimirImagen.VaryQualityLevel((Image)img.Clone(), 35L));
                                DocumentoPorCobrarPago.ImagenBase64 = image.ToBase64String(img.RawFormat);
                            }

                        }
                    }
                    _PesajeGanado_Datos oDatosPesaje = new _PesajeGanado_Datos();

                    DocumentoPorCobrarPago = oDatosPesaje.PesajeGanado_spCSLDB_ac_detallesPago(DocumentoPorCobrarPago);

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
                    return RedirectToAction("Transacciones", "PesajeGanado", new { id = DocumentoPorCobrarPago.Id_padre.Trim() });
                }
                else
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Verifique sus datos.";
                    return RedirectToAction("Index", "PesajeGanado");
                }
            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                TempData["typemessage"] = "2";
                TempData["message"] = "Verifique sus datos, error: " + Mensaje;
                return RedirectToAction("Index", "PesajeGanado");
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

                _Comprobante_Datos oDatosComprobante = new _Comprobante_Datos();
                ComprobanteCabeceraModels Cabecera = new ComprobanteCabeceraModels();

                Cabecera = oDatosComprobante.Comprobante_spCSLDB_get_Cabecera(1, id.Value.ToString(), conexion);

                ListaComprobantePagosDetalles = oDatosComprobante.Comprobante_spCIDDB_get_detallesPagos(1, id.Value.ToString(), conexion);

                LocalReport Rtp = new LocalReport();
                Rtp.EnableExternalImages = true;
                Rtp.DataSources.Clear();
                string path = Path.Combine(Server.MapPath("~/Formatos"), "ComprobantePesajeGanado.rdlc");
                if (System.IO.File.Exists(path))
                {
                    Rtp.ReportPath = path;
                }
                else
                {
                    return RedirectToAction("Index");
                }
                ReportParameter[] Parametros = new ReportParameter[13];
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
                Parametros[11] = new ReportParameter("kilos", Cabecera.KilosPesajeGanado.ToString());
                Parametros[12] = new ReportParameter("costoPesaje", Cabecera.CostoPesajeGanado.ToString());
                
                Rtp.SetParameters(Parametros);
                Rtp.DataSources.Add(new ReportDataSource("ListaDetallesPagos", ListaComprobantePagosDetalles));

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

                _PesajeGanado_Datos oDatosPesajeGanado = new _PesajeGanado_Datos();
                PesajeGanadoDetalleModels oDetalles = new PesajeGanadoDetalleModels();
                oDetalles = oDatosPesajeGanado.PesajeGanado_spCIDDB_get_detalle(id.Value, conexion);

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
                _PesajeGanado_Datos oDatosPesaje = new _PesajeGanado_Datos();

                string usuario = User.Identity.Name;
                respuesta = oDatosPesaje.PesajeGanado_spCIDDB_finalizar(id.Value, conexion, usuario);

                return Content(respuesta.ToJSON(), "application/json");
            }
            catch
            {
                return View();
            }
        }
    }
}
