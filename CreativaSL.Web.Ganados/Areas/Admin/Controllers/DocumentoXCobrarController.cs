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
        #endregion

        #region Modal
        #region ModalArticuloServicio
        [HttpPost]
        public ActionResult ModalArticuloServicio(DocumentosPorCobrarDetalleModels DocumentoPorCobrarDetalle)
        {
            {
                _DocumentoXCobrar_Datos DocumentoDatos = new _DocumentoXCobrar_Datos();
                DocumentoPorCobrarDetalle.Conexion = Conexion;
                DocumentoPorCobrarDetalle.Usuario = User.Identity.Name;

                DocumentoPorCobrarDetalle.ListaAsignar = DocumentoDatos.GetListadoAsignar(DocumentoPorCobrarDetalle);
                DocumentoPorCobrarDetalle.ListaProductosServiciosCFDI = DocumentoDatos.GetListadoCFDIProductosServiciosCompra(DocumentoPorCobrarDetalle);
                DocumentoPorCobrarDetalle.ListaTipoClasificacionCobro = DocumentoDatos.GetListadoTipoClasificacion(DocumentoPorCobrarDetalle);

                //registrarPago.DocumentoPorCobrarDetallePagosBancarizado.ListaCuentasBancarias;

                return PartialView("ModalServicioProducto", DocumentoPorCobrarDetalle);
            }
        }
        #endregion
        #region ModalComprobante
        [HttpPost]
        public ActionResult ModalRegistrarComprobantePago(DocumentosPorCobrarDetallePagosModels DocumentoPorCobrarPago)
        {
            {
                _DocumentoXCobrar_Datos DocCobrarDatos = new _DocumentoXCobrar_Datos();
                DocumentoPorCobrarPago.Usuario = User.Identity.Name;
                DocumentoPorCobrarPago.Conexion = Conexion;
                DocumentoPorCobrarPago.ListaAsignar = DocCobrarDatos.GetListadoAsignar(DocumentoPorCobrarPago);
                DocumentoPorCobrarPago.ListaFormaPagos = DocCobrarDatos.GetListadoCFDIFormaPago(DocumentoPorCobrarPago);
                //registrarPago.DocumentoPorCobrarDetallePagosBancarizado.ListaCuentasBancarias;

                return PartialView("ModalRegistrarComprobantePago", DocumentoPorCobrarPago);
            }
        }
        #endregion
        #endregion
    }
}
