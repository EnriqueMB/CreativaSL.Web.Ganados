using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CreativaSL.Web.Ganados.Models;
using System.Data;
using CreativaSL.Web.Ganados.ViewModels;
using CreativaSL.Web.Ganados.App_Start;
using CreativaSL.Web.Ganados.Filters;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    [Autorizado]
    public class CatConceptosNominaController : Controller
    {
        private TokenProcessor Token = TokenProcessor.GetInstance();
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
        // GET: Admin/CatConceptosNomina
        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                CatConceptosNominaModels concepto = new CatConceptosNominaModels();
                _CatConceptoNomina_Datos conceptoD = new _CatConceptoNomina_Datos();
                concepto.Conexion = Conexion;
                concepto = conceptoD.ObtenerConceptosNomina(concepto);
                return View(concepto);
            }
            catch (Exception)
            {
                CatConceptosNominaModels concepto = new CatConceptosNominaModels();
                concepto.LIstaConceptoNomina = new List<CatConceptosNominaModels>();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(concepto);
            }
        }

        // GET: Admin/CatConceptosNomina/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/CatConceptosNomina/Create
        public ActionResult Create()
        {
            try
            {
                Token.IsTokenValid();
                CatConceptosNominaModels catConceptos = new CatConceptosNominaModels();
                catConceptos.Calculado = false;
                catConceptos.SumaResta = false;
                catConceptos.SoloLectura = false;
                return View(catConceptos);
            }
            catch (Exception)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("Index");
            }
        }

        // POST: Admin/CatConceptosNomina/Create
        [HttpPost]
        public ActionResult Create(CatConceptosNominaModels conceptos)
        {
            _CatConceptoNomina_Datos conceptoD = new _CatConceptoNomina_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        conceptos.Conexion = Conexion;
                        conceptos.Opcion = 1;
                        conceptos.Calculado = false;
                        conceptos.SoloLectura = false;
                        conceptos.Usuario = User.Identity.Name;
                        conceptos = conceptoD.AbcCatConceptosNomina(conceptos);
                        if (conceptos.Completado == true)
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
                            return View(conceptos);
                        }
                    }
                    else
                    {
                        return View(conceptos);
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
                return View(conceptos);
            }
        }

        // GET: Admin/CatConceptosNomina/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                Token.SaveToken();
                CatConceptosNominaModels conceptos = new CatConceptosNominaModels();
                _CatConceptoNomina_Datos conceptosD = new _CatConceptoNomina_Datos();
                conceptos.Conexion = Conexion;
                conceptos.IDConceptoNomina = id;
                conceptos = conceptosD.ObtenerDetalleConceptoNomina(conceptos);
                return View(conceptos);
            }
            catch (Exception)
            {
                CatConceptosNominaModels conceptos = new CatConceptosNominaModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(conceptos);
            }
        }

        // POST: Admin/CatConceptosNomina/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, CatConceptosNominaModels conceptos)
        {
            _CatConceptoNomina_Datos conceptoD = new _CatConceptoNomina_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        conceptos.Conexion = Conexion;
                        conceptos.IDConceptoNomina = id;
                        conceptos.Opcion = 2;
                        conceptos.Usuario = User.Identity.Name;
                        conceptos = conceptoD.AbcCatConceptosNomina(conceptos);
                        if (conceptos.Completado == true)
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
                            return View(conceptos);
                        }
                    }
                    else
                    {
                        return View(conceptos);
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
                return View(conceptos);
            }
        }

        //// GET: Admin/CatConceptosNomina/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: Admin/CatConceptosNomina/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                CatConceptosNominaModels conceptos = new CatConceptosNominaModels();
                _CatConceptoNomina_Datos conceptosD = new _CatConceptoNomina_Datos();
                conceptos.Conexion = Conexion;
                conceptos.IDConceptoNomina = id;
                conceptos.Usuario = User.Identity.Name;
                conceptosD.EliminarConcetoNomina(conceptos);
                TempData["typemessage"] = "1";
                TempData["message"] = "El registro se ha eliminado correctamente";
                return Json("");
            }
            catch
            {
                return View();
            }
        }
    }
}
