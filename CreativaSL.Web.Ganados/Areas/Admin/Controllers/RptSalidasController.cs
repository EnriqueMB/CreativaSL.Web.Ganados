using CreativaSL.Web.Ganados.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    public class RptSalidasController : Controller
    {
        // GET: Admin/RptSalidas
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
        public ActionResult Index()
        {
            try
            {
                RptSalidasModels reporte = new RptSalidasModels();
                _RptSalidas_Datos reporteDatos = new _RptSalidas_Datos();
                reporte.conexion = Conexion;
                reporte.DatosEmpresa = reporteDatos.ObtenerDatosEmpresaTipo1(reporte);

                reporte.listaSalidas = reporteDatos.obtenerListaProveedoresFecha(reporte);
                reporte = reporteDatos.obtenerListaProveedoresFecha2(reporte);
                return View(reporte);
            }
            catch (Exception ex)
            {
                RptSalidasModels Proveedor = new RptSalidasModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Proveedor);
            }
        }
        [HttpPost]
        public ActionResult Index(RptSalidasModels reporte)
        {
            try
            {

                _RptSalidas_Datos reporteDatos = new _RptSalidas_Datos();


                reporte.conexion = Conexion;
               
                reporte.DatosEmpresa = reporteDatos.ObtenerDatosEmpresaTipo1(reporte);
                reporte.listaSalidas = reporteDatos.obtenerListaProveedoresFecha(reporte);
                reporte = reporteDatos.obtenerListaProveedoresFecha2(reporte);

                return View(reporte);
            }
            catch (Exception ex)
            {
                RptSalidasModels Proveedor = new RptSalidasModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Proveedor);
            }

        }
        // GET: Admin/RptSalidas/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/RptSalidas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/RptSalidas/Create
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

        // GET: Admin/RptSalidas/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/RptSalidas/Edit/5
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

        // GET: Admin/RptSalidas/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/RptSalidas/Delete/5
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
