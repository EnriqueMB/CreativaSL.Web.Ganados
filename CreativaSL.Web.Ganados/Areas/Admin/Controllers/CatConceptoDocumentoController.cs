using CreativaSL.Web.Ganados.Filters;
using System;
using System.Collections.Generic;
using System.Configuration;
using CreativaSL.Web.Ganados.Models;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    [Autorizado]
    public class CatConceptoDocumentoController : Controller
    {
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
        // GET: Admin/CatConceptoDocumento
        public ActionResult Index()
        {
            try
            {
                CatConceptoDetalleDocumentosModels ConceptoDetalle = new CatConceptoDetalleDocumentosModels();
                _CatConceptoDetalleDocumento_Datos ConceptoDetalleDatos = new _CatConceptoDetalleDocumento_Datos();
                ConceptoDetalle.Conexion = Conexion;
                ConceptoDetalle.listaConceptosDetalles = ConceptoDetalleDatos.ObtenerlistConceptosDocumentos(ConceptoDetalle);
                return View(ConceptoDetalle);
            }
            catch (Exception)
            {
                CatConceptoDetalleDocumentosModels ConceptoDetalle = new CatConceptoDetalleDocumentosModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(ConceptoDetalle);
            }
        }

        // GET: Admin/CatConceptoDocumento/Create
        [HttpGet]
        public ActionResult Create()
        {
            try
            {
                CatConceptoDetalleDocumentosModels ConceptoDetalle = new CatConceptoDetalleDocumentosModels();
                _CatConceptoDetalleDocumento_Datos ConceptoDetalleDatos = new _CatConceptoDetalleDocumento_Datos();
                ConceptoDetalle.Conexion = Conexion;
                ConceptoDetalle.listTipoConciliacion = ConceptoDetalleDatos.ObtenerComboTipoConciliacion(ConceptoDetalle);
                return View(ConceptoDetalle);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpPost]
        public ActionResult Create(CatConceptoDetalleDocumentosModels Concepto)
        {
            
            try
            {
                _CatConceptoDetalleDocumento_Datos ConceptoDatos = new _CatConceptoDetalleDocumento_Datos();
                if (ModelState.IsValid)
                {
                    Concepto.Conexion = Conexion;
                    Concepto.Usuario = User.Identity.Name;
                    Concepto.Opcion = 1;
                    Concepto = ConceptoDatos.DACatConceptoDocumento(Concepto);
                    if (Concepto.Completado)
                    {
                        TempData["typemessage"] = "1";
                        TempData["message"] = "Los datos se guardaron correctamente.";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["typemessage"] = "2";
                        TempData["message"] = "Ocurrio un error al intentar guardar los datos. Intente más tarde.";
                        return View(Concepto);
                    }
                }
                else
                {
                    Concepto.Conexion = Conexion;
                    Concepto.listTipoConciliacion = ConceptoDatos.ObtenerComboTipoConciliacion(Concepto);
                    return View(Concepto);
                }
            }
            catch (Exception ex)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error al intentar guardar los datos. Contacte a soporte técnico.";
                return View(Concepto);
            }
        }
    }
}