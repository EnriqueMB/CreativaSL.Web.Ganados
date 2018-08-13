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
    public class DocumentoXCobrarController : Controller
    {
        private TokenProcessor Token = TokenProcessor.GetInstance();
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
        // GET: Admin/DocumentosXCobrar
        public ActionResult Index()
        {
            try
            {
                DocumentosPorCobrarModels Documentos = new DocumentosPorCobrarModels();
                _DocumentoXCobrar_Datos DocumentosDatos = new _DocumentoXCobrar_Datos();
                Documentos.Conexion = Conexion;
                Documentos.ListaDocumentos = DocumentosDatos.ObtenerListaDocumentosCobrar(Documentos);
                return View(Documentos);
            }
            catch (Exception)
            {
                DocumentosPorCobrarModels Documentos = new DocumentosPorCobrarModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Documentos);
            }
        }

        // GET: Admin/DocumentosXCobrar/Details/5
        public ActionResult Details(int id)
        {
            //try
            //{
            //    DocumentosPorCobrarDetalleModels Documentos = new DocumentosPorCobrarDetalleModels();
            //    DocumentoXPagar_Datos DocumentosDatos = new DocumentoXPagar_Datos();
            //    Documentos.Conexion = Conexion;
            //    Documentos.IDDocumentoPagar = id;
            //    Documentos.IDTipoDocumento = id2;
            //    Documentos.listaDocumentosDetalle = DocumentosDatos.ObtenerDetalleListaDocumentosPagar(Documentos);
            //    return View(Documentos);
            //}
            //catch (Exception)
            //{
            //    DocumentoPorPagarDetalleModels Documentos = new DocumentoPorPagarDetalleModels();
            //    TempData["typemessage"] = "2";
            //    TempData["message"] = "No se puede cargar la vista";
            //    return View(Documentos);
            //}
            return View();
        }

        // GET: Admin/DocumentosXCobrar/Create
        public ActionResult Create()
        {
                return View();
            
        }

        // POST: Admin/DocumentosXCobrar/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/DocumentosXCobrar/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/DocumentosXCobrar/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/DocumentosXCobrar/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/DocumentosXCobrar/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }



        #region JSON tablas
        #region Json Documentos Detalles
        [HttpPost]
        public ActionResult JsonDocumentosDetallesCompra(DocumentosPorCobrarDetalleModels Documento)
        {
            try
            {
                _DocumentoXCobrar_Datos DocumentoDatos = new _DocumentoXCobrar_Datos();
                Documento.Conexion = Conexion;
                Documento.Usuario = User.Identity.Name;
                Documento.RespuestaAjax = new RespuestaAjax();

                Documento.RespuestaAjax.Mensaje = DocumentoDatos.GetDocumentosDetallesCompra(Documento);
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
        #endregion
        #region Json Documentos Detalles Pagos
        [HttpPost]
        public ActionResult JsonDocumentosDetallesCompraPagos(DocumentosPorCobrarDetallePagosModels DocumentoPagos)
        {
            try
            {
                _DocumentoXCobrar_Datos DocumentoDatos = new _DocumentoXCobrar_Datos();
                DocumentoPagos.Conexion = Conexion;
                DocumentoPagos.Usuario = User.Identity.Name;
                DocumentoPagos.RespuestaAjax = new RespuestaAjax();

                DocumentoPagos.RespuestaAjax.Mensaje = Auxiliar.SqlReaderToJson(DocumentoDatos.GetDocumentosDetallesCompraPagos(DocumentoPagos));
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
        #endregion

        #region Add Comprobante
        [HttpGet]
        public ActionResult AddComprobante(DocumentosPorCobrarDetallePagosModels DocumentoPorCobrarPago)
        {
            try
            {
                _DocumentoXCobrar_Datos DocCobrarDatos = new _DocumentoXCobrar_Datos();
                DocumentoPorCobrarPago.Usuario = User.Identity.Name;
                DocumentoPorCobrarPago.Conexion = Conexion;
                DocumentoPorCobrarPago.ListaAsignar = DocCobrarDatos.GetListadoAsignarPagos(DocumentoPorCobrarPago);
                //es para el boton de regresar 1 es compra, 2 es flete de la compra
                if (DocumentoPorCobrarPago.TipoServicio == 1 || DocumentoPorCobrarPago.TipoServicio == 2)
                    DocumentoPorCobrarPago.Id_compra = DocumentoPorCobrarPago.ListaAsignar[0].Id_2;

                DocumentoPorCobrarPago.ListaFormaPagos = DocCobrarDatos.GetListadoCFDIFormaPago(DocumentoPorCobrarPago);
                DocumentoPorCobrarPago = DocCobrarDatos.GetNombreEmpresaProveedorCliente(DocumentoPorCobrarPago);
                DocumentoPorCobrarPago.TipoCuentaBancaria = 1;
                DocumentoPorCobrarPago.ListaCuentasBancariasEmpresa = DocCobrarDatos.GetListadoCuentasBancarias(DocumentoPorCobrarPago);
                DocumentoPorCobrarPago.TipoCuentaBancaria = 2;
                DocumentoPorCobrarPago.ListaCuentasBancariasProveedor = DocCobrarDatos.GetListadoCuentasBancarias(DocumentoPorCobrarPago);
                DocumentoPorCobrarPago.fecha = DateTime.Now;
                DocumentoPorCobrarPago.Bancarizado = false;

                DocumentoPorCobrarPago.ImagenMostrar = Auxiliar.SetDefaultImage();
                DocumentoPorCobrarPago.ExtensionImagenBase64 = Auxiliar.ObtenerExtensionImagenBase64(DocumentoPorCobrarPago.ImagenMostrar);
                return View(DocumentoPorCobrarPago);
            }
            catch
            {
                return View();
            }
        }
        #endregion

        #region Edit Comprobante
        public ActionResult EditComprobante(DocumentosPorCobrarDetallePagosModels DocumentoPorCobrarPago)
        {
            try
            {
                _DocumentoXCobrar_Datos DocCobrarDatos = new _DocumentoXCobrar_Datos();
                DocumentoPorCobrarPago.Usuario = User.Identity.Name;
                DocumentoPorCobrarPago.Conexion = Conexion;
                DocumentoPorCobrarPago = DocCobrarDatos.GetDetalleDocumentoPago(DocumentoPorCobrarPago);

                DocumentoPorCobrarPago.ListaAsignar = DocCobrarDatos.GetListadoAsignarPagos(DocumentoPorCobrarPago);

                if (DocumentoPorCobrarPago.TipoServicio == 1 || DocumentoPorCobrarPago.TipoServicio == 2)
                    DocumentoPorCobrarPago.Id_compra = DocumentoPorCobrarPago.ListaAsignar[0].Id_2;


                if (string.IsNullOrEmpty(DocumentoPorCobrarPago.ImagenBase64))
                {
                    DocumentoPorCobrarPago.ImagenMostrar = Auxiliar.SetDefaultImage();
                }
                else
                {
                    DocumentoPorCobrarPago.ImagenMostrar = DocumentoPorCobrarPago.ImagenBase64;
                }
                DocumentoPorCobrarPago.ExtensionImagenBase64 = Auxiliar.ObtenerExtensionImagenBase64(DocumentoPorCobrarPago.ImagenBase64);


                DocumentoPorCobrarPago.ListaFormaPagos = DocCobrarDatos.GetListadoCFDIFormaPago(DocumentoPorCobrarPago);
                DocumentoPorCobrarPago = DocCobrarDatos.GetNombreEmpresaProveedorCliente(DocumentoPorCobrarPago);
                DocumentoPorCobrarPago.TipoCuentaBancaria = 1;
                DocumentoPorCobrarPago.ListaCuentasBancariasEmpresa = DocCobrarDatos.GetListadoCuentasBancarias(DocumentoPorCobrarPago);
                DocumentoPorCobrarPago.TipoCuentaBancaria = 2;
                DocumentoPorCobrarPago.ListaCuentasBancariasProveedor = DocCobrarDatos.GetListadoCuentasBancarias(DocumentoPorCobrarPago);

                return View(DocumentoPorCobrarPago);
            }
            catch
            {
                return View();
            }
        }
        #endregion

        #region Del comprobante
        #region Del comprobante compra
        public ActionResult DelComprobante(DocumentosPorCobrarDetallePagosModels DocumentoPorCobrarPago)
        {
            try
            {
                _DocumentoXCobrar_Datos DocCobrarDatos = new _DocumentoXCobrar_Datos();
                DocumentoPorCobrarPago.Usuario = User.Identity.Name;
                DocumentoPorCobrarPago.Conexion = Conexion;
                DocumentoPorCobrarPago.RespuestaAjax = new RespuestaAjax();
                DocumentoPorCobrarPago = DocCobrarDatos.DEL_ComprobanteCompra(DocumentoPorCobrarPago);

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
            catch
            {
                return View();
            }
        }
        #endregion
        #endregion

        #region Modal impuesto
        #region compra
        public ActionResult ModalImpuestosCompra(string IDDetalleDoctoCobrar)
        {
            DocumentosPorCobrarDetalleImpuestoModels Impuesto = new DocumentosPorCobrarDetalleImpuestoModels();
            _DocumentoXCobrar_Datos DocumentoPorCobrarDatos = new _DocumentoXCobrar_Datos();

            Impuesto.Id_detalleDoctoCobrar = IDDetalleDoctoCobrar;
            Impuesto.Conexion = Conexion;

            //FleteImpuesto = FleteImpuestoDatos.GetFleteImpuestoXIDFleteImpuesto(FleteImpuesto);
            //FleteImpuesto.ListaImpuesto = FleteImpuestoDatos.GetListadoImpuesto(FleteImpuesto);
            //FleteImpuesto.ListaTipoImpuesto = FleteImpuestoDatos.GetListadoTipoImpuesto(FleteImpuesto);
            //FleteImpuesto.ListaTipoFactor = FleteImpuestoDatos.GetListadoTipoFactor(FleteImpuesto);

            //Impuesto.ListaImpuesto = DocumentoPorCobrarDatos.GetListadoImpuesto(Impuesto);
            //Impuesto = FleteImpuestoDatos.GetFleteImpuestoXIDFleteImpuesto(FleteImpuesto);

            //Impuesto = DocumentoPorCobrarDatos.GetEventoXIDEvento(Evento);
            //Impuesto.ListaEventos = CompraDatos.GetListaTiposEventos(Evento);


            return PartialView("ModalImpuestoCompra", Impuesto);
        }
        #endregion
        #endregion

        #region Guardar Comprobante compra
        [HttpPost]
        public ActionResult GuardarComprobante(DocumentosPorCobrarDetallePagosModels DocumentoPorCobrarPago)
        {
            try
            {
                _DocumentoXCobrar_Datos DocCobrarDatos = new _DocumentoXCobrar_Datos();
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
                DocumentoPorCobrarPago = DocCobrarDatos.AC_ComprobanteCompra(DocumentoPorCobrarPago);

                if (DocumentoPorCobrarPago.RespuestaAjax.Success)
                {
                    if (DocumentoPorCobrarPago.TipoServicio == 1 || DocumentoPorCobrarPago.TipoServicio == 2) {
                        TempData["typemessage"] = "1";
                        TempData["message"] = "Datos guardados correctamente.";
                    }
                }
                return Content(DocumentoPorCobrarPago.RespuestaAjax.ToJSON(), "application/json");
            }
            catch
            {
                return View();
            }
        }
        #endregion

        #region Combos
        #region Bancos
        [HttpPost]
        public ActionResult GetCuentasBancarias(string Id_documentoPorCobrar, int tipoServicio)
        {
            try
            {
                DocumentosPorCobrarDetallePagosModels DocumentoPorCobrarPagos = new DocumentosPorCobrarDetallePagosModels();
                _DocumentoXCobrar_Datos DocumentoDatos = new _DocumentoXCobrar_Datos();
                DocumentoPorCobrarPagos.Id_documentoPorCobrar = Id_documentoPorCobrar;
                DocumentoPorCobrarPagos.TipoCuentaBancaria = tipoServicio;
                DocumentoPorCobrarPagos.Conexion = Conexion;
                DocumentoPorCobrarPagos.Usuario = User.Identity.Name;
                DocumentoPorCobrarPagos.ListaCuentasBancariasEmpresa = DocumentoDatos.GetListadoCuentasBancarias(DocumentoPorCobrarPagos);

                return Content(DocumentoPorCobrarPagos.ListaCuentasBancariasEmpresa.ToJSON(), "application/json");
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

        #region Detalle producto / servicio
        [HttpGet]
        public ActionResult AddProductoServicio(int TipoServicio, string Id_documentoPorCobrar, string Id_redireccionar)
        {
            try
            {
                DocumentosPorCobrarDetalleModels DocumentoPorCobrarPago = new DocumentosPorCobrarDetalleModels();
                DocumentoPorCobrarPago.TipoServicio = TipoServicio;
                DocumentoPorCobrarPago.Id_documentoCobrar = Id_documentoPorCobrar;
                DocumentoPorCobrarPago.Id_redireccionar = Id_redireccionar;

                _DocumentoXCobrar_Datos DocCobrarDatos = new _DocumentoXCobrar_Datos();
                DocumentoPorCobrarPago.Conexion = Conexion;
                DocumentoPorCobrarPago.ListaTipoClasificacionCobro = DocCobrarDatos.GetListadoTipoClasificacion(DocumentoPorCobrarPago);
                DocumentoPorCobrarPago.ListaProductosServiciosCFDI = DocCobrarDatos.GetListadoCFDIProductosServiciosCompra(DocumentoPorCobrarPago);
                CatAlmacenModels almacen = new CatAlmacenModels();
                almacen.IDAlmacen = "";
                almacen.Descripcion = "-- Seleccione --";
                DocumentoPorCobrarPago.ListaAlmacen = new List<CatAlmacenModels>();
                DocumentoPorCobrarPago.ListaAlmacen.Add(almacen);

                CatProductosAlmacenModels producto = new CatProductosAlmacenModels();
                producto.IDProductoAlmacen = "";
                producto.Nombre = "-- Seleccione --";
                DocumentoPorCobrarPago.ListaProductos = new List<CatProductosAlmacenModels>();
                DocumentoPorCobrarPago.ListaProductos.Add(producto);

                return View(DocumentoPorCobrarPago);
            }
            catch (Exception ex)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Verifique sus datos";
                return RedirectToAction("Index", "Compra");
            }
        }
        [HttpPost]
        public ActionResult AddProductoServicio(DocumentosPorCobrarDetalleModels DocumentoPorCobrarPago)
        {
            try
            {
                _DocumentoXCobrar_Datos DocCobrarDatos = new _DocumentoXCobrar_Datos();
                DocumentoPorCobrarPago.Conexion = Conexion;
                DocumentoPorCobrarPago.Usuario = User.Identity.Name;
                DocumentoPorCobrarPago.RespuestaAjax = new RespuestaAjax();
                DocumentoPorCobrarPago = DocCobrarDatos.AC_ProductoServicio_Compra(DocumentoPorCobrarPago);

                if (DocumentoPorCobrarPago.RespuestaAjax.Success)
                    return RedirectToAction("Transacciones", "Compra", new { IDCompra = DocumentoPorCobrarPago.Id_redireccionar });
                else
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = DocumentoPorCobrarPago.RespuestaAjax.Mensaje;
                    return View(DocumentoPorCobrarPago);
                }
            }
            catch (Exception ex)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Verifique sus datos";
                return RedirectToAction("Index", "Compra");
            }
        }

        [HttpGet]
        public ActionResult EditProductoServicio(string Id_detalleDoctoCobrar, string Id_redireccionar, int TipoServicio)
        {
            try
            {
                DocumentosPorCobrarDetalleModels DocumentoPorCobrarPago = new DocumentosPorCobrarDetalleModels();
                DocumentoPorCobrarPago.Id_detalleDoctoCobrar = Id_detalleDoctoCobrar;
                DocumentoPorCobrarPago.Id_redireccionar = Id_redireccionar;
                DocumentoPorCobrarPago.TipoServicio = TipoServicio;

                _DocumentoXCobrar_Datos DocCobrarDatos = new _DocumentoXCobrar_Datos();
                DocumentoPorCobrarPago.Conexion = Conexion;

                DocumentoPorCobrarPago = DocCobrarDatos.GetDatosProductoServicio(DocumentoPorCobrarPago);

                DocumentoPorCobrarPago.ListaTipoClasificacionCobro = DocCobrarDatos.GetListadoTipoClasificacion(DocumentoPorCobrarPago);
                DocumentoPorCobrarPago.ListaProductosServiciosCFDI = DocCobrarDatos.GetListadoCFDIProductosServiciosCompra(DocumentoPorCobrarPago);

                DocumentoPorCobrarPago.ListaAlmacen = DocCobrarDatos.GetAlmacenesHabilitados(DocumentoPorCobrarPago);
                DocumentoPorCobrarPago.ListaProductos = DocCobrarDatos.GetProductosAlmacen(DocumentoPorCobrarPago, DocumentoPorCobrarPago.Id_almacen);

                return View(DocumentoPorCobrarPago);
            }
            catch (Exception ex)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Verifique sus datos";
                return RedirectToAction("Index", "Compra");
            }
        }

        [HttpPost]
        public ActionResult EditProductoServicio(DocumentosPorCobrarDetalleModels DocumentoPorCobrarPago)
        {
            try
            {
                _DocumentoXCobrar_Datos DocCobrarDatos = new _DocumentoXCobrar_Datos();
                DocumentoPorCobrarPago.Conexion = Conexion;
                DocumentoPorCobrarPago.RespuestaAjax = new RespuestaAjax();
                DocumentoPorCobrarPago = DocCobrarDatos.AC_ProductoServicio_Compra(DocumentoPorCobrarPago);

                if (DocumentoPorCobrarPago.RespuestaAjax.Success)
                    return RedirectToAction("Transacciones", "Compra", new { IDCompra = DocumentoPorCobrarPago.Id_redireccionar });
                else
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = DocumentoPorCobrarPago.RespuestaAjax.Mensaje;
                    return View(DocumentoPorCobrarPago);
                }

                return View(DocumentoPorCobrarPago);
            }
            catch (Exception ex)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Verifique sus datos";
                return RedirectToAction("Index", "Compra");
            }
        }

        [HttpPost]
        public ActionResult Del_ProductoServicio(DocumentosPorCobrarDetalleModels documento)
        {
            try
            {
                _DocumentoXCobrar_Datos DocCobrarDatos = new _DocumentoXCobrar_Datos();
                documento.Conexion = Conexion;
                documento.Usuario = User.Identity.Name;
                documento.RespuestaAjax = new RespuestaAjax();
                documento = DocCobrarDatos.DEL_ProductoServicioCompra(documento);

                if (documento.RespuestaAjax.Success)
                {
                    TempData["typemessage"] = "1";
                    TempData["message"] = documento.RespuestaAjax.Mensaje;
                }
                else
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = documento.RespuestaAjax.Mensaje;
                }
                
                return Content(documento.RespuestaAjax.ToJSON(), "application/json");
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


        public ActionResult GetAlmacenes()
        {
            try
            {
                DocumentosPorCobrarDetalleModels  documento = new DocumentosPorCobrarDetalleModels();
                _DocumentoXCobrar_Datos datos  = new _DocumentoXCobrar_Datos();
                documento.Conexion = Conexion;
                documento.Usuario = User.Identity.Name;
                documento.ListaAlmacen = datos.GetAlmacenesHabilitados(documento);

                return Content(documento.ListaAlmacen.ToJSON(), "application/json");
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }
        public ActionResult GetProductosAlmacen(string almacen)
        {
            try
            {
                DocumentosPorCobrarDetalleModels documento = new DocumentosPorCobrarDetalleModels();
                _DocumentoXCobrar_Datos datos = new _DocumentoXCobrar_Datos();
                documento.Conexion = Conexion;
                documento.Usuario = User.Identity.Name;
                documento.ListaProductos = datos.GetProductosAlmacen(documento, almacen);

                return Content(documento.ListaProductos.ToJSON(), "application/json");
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }
        #endregion
    }
}
