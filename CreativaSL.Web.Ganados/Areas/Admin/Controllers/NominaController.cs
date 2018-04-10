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
    public class NominaController : Controller
    {
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");

        // GET: Admin/Nomina
        public ActionResult Index()
        {
            try
            {
                NominaModels Nomina = new NominaModels();
                Nomina_Datos NominaDatos = new Nomina_Datos();
                Nomina.Conexion = Conexion;
                Nomina.ListaNomina = NominaDatos.ObtenerListaNomina(Nomina);
                return View(Nomina);
            }
            catch (Exception)
            {
                NominaModels Nomina = new NominaModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Nomina);
            }
        }

        // GET: Admin/Nomina/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Nomina/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Nomina/Create
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

        // GET: Admin/Nomina/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Nomina/Edit/5
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

        // GET: Admin/Nomina/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Nomina/Delete/5
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
