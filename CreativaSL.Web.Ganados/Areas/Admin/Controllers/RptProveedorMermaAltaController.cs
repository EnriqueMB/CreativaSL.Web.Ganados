using CreativaSL.Web.Ganados.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    public class RptProveedorMermaAltaController : Controller
    {
         string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
        // GET: Admin/RptProveedorMermaAlta
        public ActionResult Index()
        {

            try
            {
                RptProveedorMermaAltaModels reporte = new RptProveedorMermaAltaModels();
                _RptProveedorMermaAlta_Datos reporteDatos = new _RptProveedorMermaAlta_Datos();
                reporte.Conexion = Conexion;
                reporte.listaRptProveedorMerma = reporteDatos.obtenerListaProveedoresMermaAlta(reporte);
                return View(reporte);
            }
            catch (Exception ex)
            {
                RptProveedorMermaAltaModels Proveedor = new RptProveedorMermaAltaModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Proveedor);
            }
        }

        // GET: Admin/RptProveedorMermaAlta/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/RptProveedorMermaAlta/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/RptProveedorMermaAlta/Create
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

        // GET: Admin/RptProveedorMermaAlta/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/RptProveedorMermaAlta/Edit/5
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

        // GET: Admin/RptProveedorMermaAlta/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/RptProveedorMermaAlta/Delete/5
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
