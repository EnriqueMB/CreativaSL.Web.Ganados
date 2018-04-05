using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CreativaSL.Web.Ganados.Models;
using System.Data;
using CreativaSL.Web.Ganados.ViewModels;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    public class CatConceptosNominaController : Controller
    {
        // GET: Admin/CatConceptosNomina
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
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
            return View();
        }

        // POST: Admin/CatConceptosNomina/Create
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

        // GET: Admin/CatConceptosNomina/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/CatConceptosNomina/Edit/5
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

        // GET: Admin/CatConceptosNomina/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/CatConceptosNomina/Delete/5
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
