using CreativaSL.Web.Ganados.Filters;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CreativaSL.Web.Ganados.Models;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    [Autorizado]
    public class DocumentoXPagarController : Controller
    {
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
                DocumentoPorPagarModels Documentos = new DocumentoPorPagarModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Documentos);
            }
        }
        public ActionResult DetalleDocumento(string id, int id2)
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
                DocumentoPorPagarModels Documentos = new DocumentoPorPagarModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Documentos);
            }
        }

        // GET: Admin/DocumentoXPagar/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/DocumentoXPagar/Create
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
    }
}
