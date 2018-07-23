using CreativaSL.Web.Ganados.Filters;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CreativaSL.Web.Ganados.Models;
using CreativaSL.Web.Ganados.App_Start;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    [Autorizado]
    public class DocumentoXPagarController : Controller
    {
        private TokenProcessor Token = TokenProcessor.GetInstance();
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
        // GET: Admin/DocumentoXPagar
        public ActionResult Index()
        {
            try
            {
                DocumentoPorPagarModels Documentos = new DocumentoPorPagarModels();
                DocumentoXPagar_Datos DocumentosDatos = new DocumentoXPagar_Datos();
                Documentos.Conexion = Conexion;
                Documentos.ListaDocumentos = DocumentosDatos.ObtenerListaDocumentosPagar(Documentos);
                return View(Documentos);
            }
            catch (Exception)
            {
                DocumentoPorPagarModels Documentos = new DocumentoPorPagarModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Documentos);
            }
        }

        // GET: Admin/DocumentoXPagar/Details/5
        public ActionResult Details(string id, int id2)
        {
            try
            {
                DocumentoPorPagarDetalleModels Documentos = new DocumentoPorPagarDetalleModels();
                DocumentoXPagar_Datos DocumentosDatos = new DocumentoXPagar_Datos();
                Documentos.Conexion = Conexion;
                Documentos.IDDocumentoPagar = id;
                Documentos.IDTipoDocumento = id2;
                Documentos.listaDocumentosDetalle = DocumentosDatos.ObtenerDetalleListaDocumentosPagar(Documentos);
                return View(Documentos);
            }
            catch (Exception)
            {
                DocumentoPorPagarDetalleModels Documentos = new DocumentoPorPagarDetalleModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Documentos);
            }
        }
        public ActionResult DetalleDocumento(string id, string id1, int id2)
        {
            try
            {
                DocumentoPorPagarDetalleModels Documentos = new DocumentoPorPagarDetalleModels();
                DocumentoXPagar_Datos DocumentosDatos = new DocumentoXPagar_Datos();
                Documentos.Conexion = Conexion;
                Documentos.IDDocumentoPagar = id;
                Documentos.IDTipoDocumento = id2;
                Documentos.IDDetalleDoctoPagar = id1;
                Documentos.listaDocumentosDetalle = DocumentosDatos.ObtenerDetalleListaDocumentosPagarDetalle(Documentos);
                return View(Documentos);
            }
            catch (Exception)
            {
                DocumentoPorPagarDetalleModels Documentos = new DocumentoPorPagarDetalleModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Documentos);
            }
        }

        // GET: Admin/DocumentoXPagar/Create
        public ActionResult Create()
        {
            try
            {
                Token.SaveToken();
                _Combos_Datos CMB = new _Combos_Datos();
                DocumentoXPagar_Datos datos = new DocumentoXPagar_Datos();
                DocumentoPorPagarModels documentos = new DocumentoPorPagarModels();
                documentos.Fecha = DateTime.Now;
                documentos.ListaSucursal = CMB.ObtenerComboSucursales(Conexion);
                documentos.ListaCDocumento = datos.ObtenerConceptosDocumento(Conexion);
                documentos.LisTipoProveedor = datos.ObteneComboCatTipoProveedor(Conexion);
                documentos.IDTProveedor = 0;
                documentos.Conexion = Conexion;
                documentos.LisProveedor = datos.ObteneComboProveedoresXID(documentos);
                return View(documentos);
            }
            catch (Exception)
            {
                DocumentoPorPagarModels documentos = new DocumentoPorPagarModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(documentos);
            }
        }

        // POST: Admin/DocumentoXPagar/Create
        [HttpPost]
        public ActionResult Create(DocumentoPorPagarModels documentos)
        {
            _Combos_Datos CMB = new _Combos_Datos();
            DocumentoXPagar_Datos documentoDatos = new DocumentoXPagar_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        documentos.Conexion = Conexion;
                        documentos.Opcion = 1;
                        documentos.Usuario = User.Identity.Name;
                        documentos = documentoDatos.AbcDocumentoXPagar(documentos);
                        if (documentos.Completado == true)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "Los datos se guardaron correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            documentos.ListaSucursal = CMB.ObtenerComboSucursales(Conexion);
                            documentos.ListaCDocumento = documentoDatos.ObtenerConceptosDocumento(Conexion);
                            documentos.LisTipoProveedor = documentoDatos.ObteneComboCatTipoProveedor(Conexion);
                            documentos.IDTProveedor = 0;
                            documentos.Conexion = Conexion;
                            documentos.LisProveedor = documentoDatos.ObteneComboProveedoresXID(documentos);
                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrió un error al intentar guardar.";
                            return View(documentos);
                        }
                    }
                    else
                    {
                        documentos.Conexion = Conexion;
                        documentos.ListaSucursal = CMB.ObtenerComboSucursales(Conexion);
                        documentos.ListaCDocumento = documentoDatos.ObtenerConceptosDocumento(Conexion);
                        documentos.LisTipoProveedor = documentoDatos.ObteneComboCatTipoProveedor(Conexion);
                        documentos.IDTProveedor = 0;
                        documentos.LisProveedor = documentoDatos.ObteneComboProveedoresXID(documentos);
                        return View(documentos);
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                documentos.ListaSucursal = CMB.ObtenerComboSucursales(Conexion);
                documentos.ListaCDocumento = documentoDatos.ObtenerConceptosDocumento(Conexion);
                documentos.LisTipoProveedor = documentoDatos.ObteneComboCatTipoProveedor(Conexion);
                documentos.IDTProveedor = 0;
                documentos.Conexion = Conexion;
                documentos.LisProveedor = documentoDatos.ObteneComboProveedoresXID(documentos);
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrió un error el intentar guardar. Contacte a soporte técnico";
                return View(documentos);
            }
        }
        [HttpPost]
        public ActionResult ObtenerProveedoresXID(int IDP)
        {
            try
            {
                DocumentoPorPagarModels Documento = new DocumentoPorPagarModels();
                DocumentoXPagar_Datos DocumentoDatos = new DocumentoXPagar_Datos();
                Documento.IDTProveedor = IDP;
                Documento.Conexion = Conexion;
                List<CatProveedorModels> Lista = DocumentoDatos.ObteneComboProveedoresXID(Documento);
                return Json(Lista, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }
        // GET: Admin/DocumentoXPagar/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/DocumentoXPagar/Edit/5
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

        // GET: Admin/DocumentoXPagar/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/DocumentoXPagar/Delete/5
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

        public ActionResult DetallePagos(string id)
        {
            try
            {
                DocumentoPorPagarDetallePagosModels pago = new DocumentoPorPagarDetallePagosModels();
                DocumentoXPagar_Datos documentoDatos = new DocumentoXPagar_Datos();
                pago.Conexion = Conexion;
                pago.Id_documentoPorPagar = id;
                pago.ListaPagosDocumento = documentoDatos.ObtenerListaDetallePagos(pago);
                return View(pago);
            }
            catch (Exception)
            {
                DocumentoPorPagarModels docu = new DocumentoPorPagarModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("Index");
            }
        }
        // GET: Admin/DocumentoXPagar/PagosCreate/5
        public ActionResult PagosCreate(string id)
        {

            try
            {
                Token.SaveToken();
                DocumentoPorPagarDetallePagosModels documentoPago = new DocumentoPorPagarDetallePagosModels();
                DocumentoXPagar_Datos DocPagarDatos = new DocumentoXPagar_Datos();
                documentoPago.Id_documentoPorPagar = id;
                documentoPago.Usuario = User.Identity.Name;
                documentoPago.Conexion = Conexion;
                documentoPago.TipoServicio = 1;
                documentoPago.ListaAsignar = DocPagarDatos.GetListadoAsignarPagos(documentoPago);
                //es para el boton de regresar 1 es compra, 2 es flete de la compra
                if (documentoPago.TipoServicio == 1 || documentoPago.TipoServicio == 2)
                    documentoPago.Id_compra = documentoPago.ListaAsignar[0].Id_2;

                documentoPago.ListaFormaPagos = DocPagarDatos.GetListadoCFDIFormaPago(documentoPago);
                documentoPago = DocPagarDatos.GetNombreEmpresaProveedorCliente(documentoPago);
                documentoPago.TipoCuentaBancaria = 1;
                documentoPago.ListaCuentasBancariasEmpresa = DocPagarDatos.GetListadoCuentasBancarias(documentoPago);
                documentoPago.TipoCuentaBancaria = 2;
                documentoPago.ListaCuentasBancariasProveedor = DocPagarDatos.GetListadoCuentasBancarias(documentoPago);
                documentoPago.fecha = DateTime.Now;
                documentoPago.Bancarizado = false;

                documentoPago.ImagenMostrar = Auxiliar.SetDefaultImage();
                documentoPago.ExtensionImagenBase64 = Auxiliar.ObtenerExtensionImagenBase64(documentoPago.ImagenMostrar);
                return View(documentoPago);
            }
            catch
            {
                DocumentoPorPagarDetallePagosModels documentoPago = new DocumentoPorPagarDetallePagosModels();
                DocumentoXPagar_Datos DocPagarDatos = new DocumentoXPagar_Datos();
                documentoPago.Id_documentoPorPagar = id;
                documentoPago.Usuario = User.Identity.Name;
                documentoPago.Conexion = Conexion;
                documentoPago.TipoServicio = 1;
                documentoPago.ListaAsignar = DocPagarDatos.GetListadoAsignarPagos(documentoPago);
                //es para el boton de regresar 1 es compra, 2 es flete de la compra
                if (documentoPago.TipoServicio == 1 || documentoPago.TipoServicio == 2)
                    documentoPago.Id_compra = documentoPago.ListaAsignar[0].Id_2;

                documentoPago.ListaFormaPagos = DocPagarDatos.GetListadoCFDIFormaPago(documentoPago);
                documentoPago = DocPagarDatos.GetNombreEmpresaProveedorCliente(documentoPago);
                documentoPago.TipoCuentaBancaria = 1;
                documentoPago.ListaCuentasBancariasEmpresa = DocPagarDatos.GetListadoCuentasBancarias(documentoPago);
                documentoPago.TipoCuentaBancaria = 2;
                documentoPago.ListaCuentasBancariasProveedor = DocPagarDatos.GetListadoCuentasBancarias(documentoPago);
                documentoPago.fecha = DateTime.Now;
                documentoPago.Bancarizado = false;

                documentoPago.ImagenMostrar = Auxiliar.SetDefaultImage();
                documentoPago.ExtensionImagenBase64 = Auxiliar.ObtenerExtensionImagenBase64(documentoPago.ImagenMostrar);
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(documentoPago);
            }
        }
        [HttpPost]
        public ActionResult PagosCreate(DocumentoPorPagarDetallePagosModels DocumentoPorPagarPago)
        {
            DocumentoXPagar_Datos DocumentoDatos = new DocumentoXPagar_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        DocumentoPorPagarPago.Conexion = Conexion;
                        DocumentoPorPagarPago.Usuario = User.Identity.Name;
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
                        DocumentoPorPagarPago = DocumentoDatos.AC_ComprobanteCompra(DocumentoPorPagarPago);
                        if (DocumentoPorPagarPago.Completado == true)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "Los datos se guardaron correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("DetallePagos", new { id = DocumentoPorPagarPago.Id_documentoPorPagar });
                        }
                        else
                        {

                            DocumentoPorPagarDetallePagosModels documentoPago = new DocumentoPorPagarDetallePagosModels();
                            DocumentoXPagar_Datos DocPagarDatos = new DocumentoXPagar_Datos();
                            documentoPago.Id_documentoPorPagar = DocumentoPorPagarPago.Id_documentoPorPagar;
                            documentoPago.Usuario = User.Identity.Name;
                            documentoPago.Conexion = Conexion;
                            documentoPago.TipoServicio = 1;
                            documentoPago.ListaAsignar = DocPagarDatos.GetListadoAsignarPagos(documentoPago);
                            //es para el boton de regresar 1 es compra, 2 es flete de la compra
                            if (documentoPago.TipoServicio == 1 || documentoPago.TipoServicio == 2)
                                documentoPago.Id_compra = documentoPago.ListaAsignar[0].Id_2;

                            documentoPago.ListaFormaPagos = DocPagarDatos.GetListadoCFDIFormaPago(documentoPago);
                            documentoPago = DocPagarDatos.GetNombreEmpresaProveedorCliente(documentoPago);
                            documentoPago.TipoCuentaBancaria = 1;
                            documentoPago.ListaCuentasBancariasEmpresa = DocPagarDatos.GetListadoCuentasBancarias(documentoPago);
                            documentoPago.TipoCuentaBancaria = 2;
                            documentoPago.ListaCuentasBancariasProveedor = DocPagarDatos.GetListadoCuentasBancarias(documentoPago);
                            documentoPago.fecha = DateTime.Now;
                            documentoPago.Bancarizado = false;

                            documentoPago.ImagenMostrar = Auxiliar.SetDefaultImage();
                            documentoPago.ExtensionImagenBase64 = Auxiliar.ObtenerExtensionImagenBase64(documentoPago.ImagenMostrar);
                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrio un error al intentar guardar";
                            return View(documentoPago);
                        }
                    }
                    else
                    {
                        DocumentoPorPagarDetallePagosModels documentoPago = new DocumentoPorPagarDetallePagosModels();
                        DocumentoXPagar_Datos DocPagarDatos = new DocumentoXPagar_Datos();
                        documentoPago.Id_documentoPorPagar = DocumentoPorPagarPago.Id_documentoPorPagar;
                        documentoPago.Usuario = User.Identity.Name;
                        documentoPago.Conexion = Conexion;
                        documentoPago.TipoServicio = 1;
                        documentoPago.ListaAsignar = DocPagarDatos.GetListadoAsignarPagos(documentoPago);
                        //es para el boton de regresar 1 es compra, 2 es flete de la compra
                        if (documentoPago.TipoServicio == 1 || documentoPago.TipoServicio == 2)
                            documentoPago.Id_compra = documentoPago.ListaAsignar[0].Id_2;

                        documentoPago.ListaFormaPagos = DocPagarDatos.GetListadoCFDIFormaPago(documentoPago);
                        documentoPago = DocPagarDatos.GetNombreEmpresaProveedorCliente(documentoPago);
                        documentoPago.TipoCuentaBancaria = 1;
                        documentoPago.ListaCuentasBancariasEmpresa = DocPagarDatos.GetListadoCuentasBancarias(documentoPago);
                        documentoPago.TipoCuentaBancaria = 2;
                        documentoPago.ListaCuentasBancariasProveedor = DocPagarDatos.GetListadoCuentasBancarias(documentoPago);
                        documentoPago.fecha = DateTime.Now;
                        documentoPago.Bancarizado = false;

                        documentoPago.ImagenMostrar = Auxiliar.SetDefaultImage();
                        documentoPago.ExtensionImagenBase64 = Auxiliar.ObtenerExtensionImagenBase64(documentoPago.ImagenMostrar);
                        return View(documentoPago);
                    }
                }
                else
                {
                    return RedirectToAction("DetallePagos", new { id = DocumentoPorPagarPago.Id_documentoPorPagar });
                }
            }
            catch (Exception)
            {
                DocumentoPorPagarDetallePagosModels documentoPago = new DocumentoPorPagarDetallePagosModels();
                DocumentoXPagar_Datos DocPagarDatos = new DocumentoXPagar_Datos();
                documentoPago.Id_documentoPorPagar = DocumentoPorPagarPago.Id_documentoPorPagar;
                documentoPago.Usuario = User.Identity.Name;
                documentoPago.Conexion = Conexion;
                documentoPago.TipoServicio = 1;
                documentoPago.ListaAsignar = DocPagarDatos.GetListadoAsignarPagos(documentoPago);
                //es para el boton de regresar 1 es compra, 2 es flete de la compra
                if (documentoPago.TipoServicio == 1 || documentoPago.TipoServicio == 2)
                    documentoPago.Id_compra = documentoPago.ListaAsignar[0].Id_2;

                documentoPago.ListaFormaPagos = DocPagarDatos.GetListadoCFDIFormaPago(documentoPago);
                documentoPago = DocPagarDatos.GetNombreEmpresaProveedorCliente(documentoPago);
                documentoPago.TipoCuentaBancaria = 1;
                documentoPago.ListaCuentasBancariasEmpresa = DocPagarDatos.GetListadoCuentasBancarias(documentoPago);
                documentoPago.TipoCuentaBancaria = 2;
                documentoPago.ListaCuentasBancariasProveedor = DocPagarDatos.GetListadoCuentasBancarias(documentoPago);
                documentoPago.fecha = DateTime.Now;
                documentoPago.Bancarizado = false;

                documentoPago.ImagenMostrar = Auxiliar.SetDefaultImage();
                documentoPago.ExtensionImagenBase64 = Auxiliar.ObtenerExtensionImagenBase64(documentoPago.ImagenMostrar);
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error contacte a soporte tecnico";
                return RedirectToAction("DetallePagos", new { id = DocumentoPorPagarPago.Id_documentoPorPagar });

            }
        }

        // GET: Admin/DocumentoXPagar/PagosEdit/5
        public ActionResult PagosEdit(string id, string id2)
        {
            try
            {
                Token.SaveToken();
                DocumentoPorPagarDetallePagosModels DocumentoPagarDetPago = new DocumentoPorPagarDetallePagosModels();
                DocumentoXPagar_Datos DocCobrarDatos = new DocumentoXPagar_Datos();
                DocumentoPagarDetPago.Usuario = User.Identity.Name;
                DocumentoPagarDetPago.Conexion = Conexion;
                DocumentoPagarDetPago.Id_documentoPorPagarDetallePagos = id;
                DocumentoPagarDetPago = DocCobrarDatos.GetDetalleDocumentoPago(DocumentoPagarDetPago);

                DocumentoPagarDetPago.ListaAsignar = DocCobrarDatos.GetListadoAsignarPagos(DocumentoPagarDetPago);

                if (DocumentoPagarDetPago.TipoServicio == 1 || DocumentoPagarDetPago.TipoServicio == 2)
                    DocumentoPagarDetPago.Id_compra = DocumentoPagarDetPago.ListaAsignar[0].Id_2;


                if (string.IsNullOrEmpty(DocumentoPagarDetPago.ImagenBase64))
                {
                    DocumentoPagarDetPago.ImagenMostrar = Auxiliar.SetDefaultImage();
                }
                else
                {
                    DocumentoPagarDetPago.ImagenMostrar = DocumentoPagarDetPago.ImagenBase64;
                }
                DocumentoPagarDetPago.ExtensionImagenBase64 = Auxiliar.ObtenerExtensionImagenBase64(DocumentoPagarDetPago.ImagenBase64);


                DocumentoPagarDetPago.ListaFormaPagos = DocCobrarDatos.GetListadoCFDIFormaPago(DocumentoPagarDetPago);
                DocumentoPagarDetPago = DocCobrarDatos.GetNombreEmpresaProveedorCliente(DocumentoPagarDetPago);
                DocumentoPagarDetPago.TipoCuentaBancaria = 1;
                DocumentoPagarDetPago.ListaCuentasBancariasEmpresa = DocCobrarDatos.GetListadoCuentasBancarias(DocumentoPagarDetPago);
                DocumentoPagarDetPago.TipoCuentaBancaria = 2;
                DocumentoPagarDetPago.ListaCuentasBancariasProveedor = DocCobrarDatos.GetListadoCuentasBancarias(DocumentoPagarDetPago);

                return View(DocumentoPagarDetPago);
            }
            catch
            {
                return View();
            }
        }


        #region Guardar Comprobante compra
        [HttpPost]
        public ActionResult PagosEdit(DocumentoPorPagarDetallePagosModels DocumentoPorPagarPago)
        {
            DocumentoXPagar_Datos DocumentoDatos = new DocumentoXPagar_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        DocumentoPorPagarPago.Conexion = Conexion;
                        DocumentoPorPagarPago.Usuario = User.Identity.Name;
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
                        DocumentoPorPagarPago = DocumentoDatos.AC_ComprobanteCompra(DocumentoPorPagarPago);
                        if (DocumentoPorPagarPago.Completado == true)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "Los datos se guardaron correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("DetallePagos", new { id = DocumentoPorPagarPago.Id_documentoPorPagar });
                        }
                        else
                        {

                            DocumentoPorPagarDetallePagosModels documentoPago = new DocumentoPorPagarDetallePagosModels();
                            DocumentoXPagar_Datos DocPagarDatos = new DocumentoXPagar_Datos();
                            documentoPago.Id_documentoPorPagar = DocumentoPorPagarPago.Id_documentoPorPagar;
                            documentoPago.Usuario = User.Identity.Name;
                            documentoPago.Conexion = Conexion;
                            documentoPago.TipoServicio = 1;
                            documentoPago.ListaAsignar = DocPagarDatos.GetListadoAsignarPagos(documentoPago);
                            //es para el boton de regresar 1 es compra, 2 es flete de la compra
                            if (documentoPago.TipoServicio == 1 || documentoPago.TipoServicio == 2)
                                documentoPago.Id_compra = documentoPago.ListaAsignar[0].Id_2;

                            documentoPago.ListaFormaPagos = DocPagarDatos.GetListadoCFDIFormaPago(documentoPago);
                            documentoPago = DocPagarDatos.GetNombreEmpresaProveedorCliente(documentoPago);
                            documentoPago.TipoCuentaBancaria = 1;
                            documentoPago.ListaCuentasBancariasEmpresa = DocPagarDatos.GetListadoCuentasBancarias(documentoPago);
                            documentoPago.TipoCuentaBancaria = 2;
                            documentoPago.ListaCuentasBancariasProveedor = DocPagarDatos.GetListadoCuentasBancarias(documentoPago);
                            documentoPago.fecha = DateTime.Now;
                            documentoPago.Bancarizado = false;

                            documentoPago.ImagenMostrar = Auxiliar.SetDefaultImage();
                            documentoPago.ExtensionImagenBase64 = Auxiliar.ObtenerExtensionImagenBase64(documentoPago.ImagenMostrar);
                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrio un error al intentar guardar";
                            return View(documentoPago);
                        }
                    }
                    else
                    {
                        DocumentoPorPagarDetallePagosModels documentoPago = new DocumentoPorPagarDetallePagosModels();
                        DocumentoXPagar_Datos DocPagarDatos = new DocumentoXPagar_Datos();
                        documentoPago.Id_documentoPorPagar = DocumentoPorPagarPago.Id_documentoPorPagar;
                        documentoPago.Usuario = User.Identity.Name;
                        documentoPago.Conexion = Conexion;
                        documentoPago.TipoServicio = 1;
                        documentoPago.ListaAsignar = DocPagarDatos.GetListadoAsignarPagos(documentoPago);
                        //es para el boton de regresar 1 es compra, 2 es flete de la compra
                        if (documentoPago.TipoServicio == 1 || documentoPago.TipoServicio == 2)
                            documentoPago.Id_compra = documentoPago.ListaAsignar[0].Id_2;

                        documentoPago.ListaFormaPagos = DocPagarDatos.GetListadoCFDIFormaPago(documentoPago);
                        documentoPago = DocPagarDatos.GetNombreEmpresaProveedorCliente(documentoPago);
                        documentoPago.TipoCuentaBancaria = 1;
                        documentoPago.ListaCuentasBancariasEmpresa = DocPagarDatos.GetListadoCuentasBancarias(documentoPago);
                        documentoPago.TipoCuentaBancaria = 2;
                        documentoPago.ListaCuentasBancariasProveedor = DocPagarDatos.GetListadoCuentasBancarias(documentoPago);
                        documentoPago.fecha = DateTime.Now;
                        documentoPago.Bancarizado = false;

                        documentoPago.ImagenMostrar = Auxiliar.SetDefaultImage();
                        documentoPago.ExtensionImagenBase64 = Auxiliar.ObtenerExtensionImagenBase64(documentoPago.ImagenMostrar);
                        return View(documentoPago);
                    }
                }
                else
                {
                    return RedirectToAction("DetallePagos", new { id = DocumentoPorPagarPago.Id_documentoPorPagar });
                }
            }
            catch (Exception)
            {
                DocumentoPorPagarDetallePagosModels documentoPago = new DocumentoPorPagarDetallePagosModels();
                DocumentoXPagar_Datos DocPagarDatos = new DocumentoXPagar_Datos();
                documentoPago.Id_documentoPorPagar = DocumentoPorPagarPago.Id_documentoPorPagar;
                documentoPago.Usuario = User.Identity.Name;
                documentoPago.Conexion = Conexion;
                documentoPago.TipoServicio = 1;
                documentoPago.ListaAsignar = DocPagarDatos.GetListadoAsignarPagos(documentoPago);
                //es para el boton de regresar 1 es compra, 2 es flete de la compra
                if (documentoPago.TipoServicio == 1 || documentoPago.TipoServicio == 2)
                    documentoPago.Id_compra = documentoPago.ListaAsignar[0].Id_2;

                documentoPago.ListaFormaPagos = DocPagarDatos.GetListadoCFDIFormaPago(documentoPago);
                documentoPago = DocPagarDatos.GetNombreEmpresaProveedorCliente(documentoPago);
                documentoPago.TipoCuentaBancaria = 1;
                documentoPago.ListaCuentasBancariasEmpresa = DocPagarDatos.GetListadoCuentasBancarias(documentoPago);
                documentoPago.TipoCuentaBancaria = 2;
                documentoPago.ListaCuentasBancariasProveedor = DocPagarDatos.GetListadoCuentasBancarias(documentoPago);
                documentoPago.fecha = DateTime.Now;
                documentoPago.Bancarizado = false;

                documentoPago.ImagenMostrar = Auxiliar.SetDefaultImage();
                documentoPago.ExtensionImagenBase64 = Auxiliar.ObtenerExtensionImagenBase64(documentoPago.ImagenMostrar);
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error contacte a soporte tecnico";
                return RedirectToAction("DetallePagos", new { id = DocumentoPorPagarPago.Id_documentoPorPagar });

            }
        }

        [HttpPost]
        public ActionResult PagosDelete(string id, string id2)
        {
            try
            {
                DocumentoPorPagarDetallePagosModels Datos = new DocumentoPorPagarDetallePagosModels
                {
                    Id_documentoPorPagarDetallePagos = id,
                    Id_documentoPorPagar = id2,
                    Conexion = Conexion,
                    Usuario = User.Identity.Name
                };
                DocumentoXPagar_Datos DocDatos = new DocumentoXPagar_Datos();
                DocDatos.EliminarPagoDocumentoPorPagar(Datos);
                if (Datos.Completado)
                {
                    TempData["typemessage"] = "1";
                    TempData["message"] = "El registro se ha eliminado correctamente";
                    return Json("");
                }
                else
                { return Json(""); }
            }
            catch
            {
                return View();
            }
        }
#endregion
        #region Bancos
        [HttpPost]
        public ActionResult GetCuentasBancarias(string Id_documentoPorCobrar, int tipoServicio)
        {
            try
            {
                DocumentoPorPagarDetallePagosModels DocumentoPorPagarPagos = new DocumentoPorPagarDetallePagosModels();
                DocumentoXPagar_Datos DocumentoDatos = new DocumentoXPagar_Datos();
                DocumentoPorPagarPagos.Id_documentoPorPagar = Id_documentoPorCobrar;
                DocumentoPorPagarPagos.TipoCuentaBancaria = tipoServicio;
                DocumentoPorPagarPagos.Conexion = Conexion;
                DocumentoPorPagarPagos.Usuario = User.Identity.Name;
                DocumentoPorPagarPagos.ListaCuentasBancariasEmpresa = DocumentoDatos.GetListadoCuentasBancarias(DocumentoPorPagarPagos);

                return Content(DocumentoPorPagarPagos.ListaCuentasBancariasEmpresa.ToJSON(), "application/json");
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
