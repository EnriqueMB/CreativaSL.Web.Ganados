using CreativaSL.Web.Ganados.Filters;
using System;
using System.Collections.Generic;
using System.Configuration;
using CreativaSL.Web.Ganados.Models;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CreativaSL.Web.Ganados.App_Start;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    [Autorizado]
    public class CatConceptoDocumentoController : Controller
    {
        private TokenProcessor Token = TokenProcessor.GetInstance();
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
                Token.SaveToken();
                CatConceptoDetalleDocumentosModels ConceptoDetalle = new CatConceptoDetalleDocumentosModels();
                _CatConceptoDetalleDocumento_Datos ConceptoDetalleDatos = new _CatConceptoDetalleDocumento_Datos();
                ConceptoDetalle.Conexion = Conexion;
                ConceptoDetalle.listTipoConciliacion = ConceptoDetalleDatos.ObtenerComboTipoConciliacion(ConceptoDetalle);
                return View(ConceptoDetalle);
            }
            catch (Exception ex)
            {
                CatConceptoDetalleDocumentosModels ConceptoDetalle = new CatConceptoDetalleDocumentosModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Create(CatConceptoDetalleDocumentosModels Concepto)
        {
            _CatConceptoDetalleDocumento_Datos ConceptoDatos = new _CatConceptoDetalleDocumento_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
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
                            Token.ResetToken();
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
                        return View(Concepto);
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error al intentar guardar los datos. Contacte a soporte técnico.";
                return View(Concepto);
            }
        }
        // GET: Admin/CatConceptoDocumento/Edit
        //[HttpGet]
        public ActionResult Edit(string id)
        {
            try
            {
                Token.SaveToken();
                CatConceptoDetalleDocumentosModels Concepto = new CatConceptoDetalleDocumentosModels();
                _CatConceptoDetalleDocumento_Datos ConceptoDatos = new _CatConceptoDetalleDocumento_Datos();
                Concepto.IDConceptosDocumento = id;
                Concepto.Conexion = Conexion;
                Concepto = ConceptoDatos.ObternerCatConceptoDocumento(Concepto);
                Concepto.listTipoConciliacion = ConceptoDatos.ObtenerComboTipoConciliacion(Concepto);
                return View(Concepto);
            }
            catch (Exception ex)
            {
                CatConceptoDetalleDocumentosModels Concepto = new CatConceptoDetalleDocumentosModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("Index");
            }
        }
        // POST: Admin/CatConceptoDocumento/Edit
        [HttpPost]
        public ActionResult Edit(CatConceptoDetalleDocumentosModels Concepto)
        {
            _CatConceptoDetalleDocumento_Datos ConceptoDatos = new _CatConceptoDetalleDocumento_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        Concepto.Conexion = Conexion;
                        Concepto.Opcion = 2;
                        Concepto.Usuario = User.Identity.Name;

                        Concepto = ConceptoDatos.DACatConceptoDocumento(Concepto);
                        if (Concepto.Completado == true)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "Los datos se guardarón correctamente.";
                            Token.ResetToken();
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
                        return View(Concepto);
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error al intentar guardar los datos. Contácte a soporte técnico.";
                return View(Concepto);
            }

        }
        public ActionResult Delete(string id)
        {
            return View();
        }

        // POST: Admin/CatBanco/Delete
        [HttpPost]
        public ActionResult Delete(string id, CatConceptoDetalleDocumentosModels Concepto)
        {
            try
            {
                _CatConceptoDetalleDocumento_Datos ConceptoDatos = new _CatConceptoDetalleDocumento_Datos();
                Concepto.Conexion = Conexion;
                Concepto.Opcion = 3;
                Concepto.IDConceptosDocumento = id;
                Concepto.Usuario = User.Identity.Name;
                Concepto = ConceptoDatos.EliminarCatConceptoDocumento(Concepto);
                //TempData["typemessage"] = "1";
                //TempData["message"] = "El registro se ha eliminado correctamente";
                return Json("");
            }
            catch
            {
                CatConceptoDetalleDocumentosModels Conceptos = new CatConceptoDetalleDocumentosModels();
                //TempData["typemessage"] = "2";
                //TempData["message"] = "No se pudo borrar los datos. Por favor contacte a soporte técnico";
                return Json("");

            }
        }
    }
}