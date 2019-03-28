using CreativaSL.Web.Ganados.App_Start;
using CreativaSL.Web.Ganados.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
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
                if (id == null || id == 0)
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Verifique sus datos.";
                    return RedirectToAction("Index");
                }
                _PesajeGanado_Datos oDatosPesaje = new _PesajeGanado_Datos();
                RespuestaAjax respuesta = new RespuestaAjax();
                string usuario = User.Identity.Name;
                respuesta = oDatosPesaje.PesajeGanado_spCIDDB_del(id.Value, conexion, usuario);

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
                            DocumentoPorCobrarPago.ImagenMostrar = Auxiliar.SetDefaultImage();
                        }
                        else
                        {
                            DocumentoPorCobrarPago.ImagenMostrar = DocumentoPorCobrarPago.ImagenBase64;
                        }
                        DocumentoPorCobrarPago.ExtensionImagenBase64 = Auxiliar.ObtenerExtensionImagenBase64(DocumentoPorCobrarPago.ImagenMostrar);

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
                        if (DocumentoPorCobrarPago.HttpImagen == null)
                        {
                            DocumentoPorCobrarPago.ImagenBase64 = DocumentoPorCobrarPago.ImagenMostrar;
                        }
                        else
                        {
                            DocumentoPorCobrarPago.ImagenBase64 = Auxiliar.ImageToBase64(DocumentoPorCobrarPago.HttpImagen);
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
    }
}