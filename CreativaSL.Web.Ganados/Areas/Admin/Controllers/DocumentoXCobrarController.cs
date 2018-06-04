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
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
        // GET: Admin/DocumentosXCobrar
        public ActionResult Index()
        {
            return View();
        }

        // GET: Admin/DocumentosXCobrar/Details/5
        public ActionResult Details(int id)
        {
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

                Documento.RespuestaAjax.Mensaje = Auxiliar.SqlReaderToJson(DocumentoDatos.GetDocumentosDetallesCompra(Documento));
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

                if(DocumentoPorCobrarPago.RespuestaAjax.Success)
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
                    if(DocumentoPorCobrarPago.HttpImagen == null)
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
                    if(DocumentoPorCobrarPago.TipoServicio == 1 || DocumentoPorCobrarPago.TipoServicio == 2) {
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



    }
}
