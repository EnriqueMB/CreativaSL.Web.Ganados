using CreativaSL.Web.Ganados.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    public class RptProveedorMunicipioController : Controller
    {
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
        // GET: Admin/RptProveedorMunicipio
        public ActionResult Index()
        {

            try
            {
                RptProveedorMunicipioModels reporte = new RptProveedorMunicipioModels();
                _RptProveedorMunicipio_Datos reporteDatos = new _RptProveedorMunicipio_Datos();
                reporte.conexion = Conexion;
                reporte.listaEstado = reporteDatos.obtenerListaEstados(reporte);
               
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
        [HttpPost]
        public ActionResult Index(RptProveedorMunicipioModels reporte)
        {
            try
            {

                _RptProveedorMunicipio_Datos reporteDatos = new _RptProveedorMunicipio_Datos();
                

                reporte.conexion = Conexion;
                reporte.listaEstado = reporteDatos.obtenerListaEstados(reporte);
                reporte.listaMunicipio = reporteDatos.obtenerListaMunicipios(reporte);
                reporte.listaProveedor = reporteDatos.obtenerListaProveedoresFecha(reporte);

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

        // GET: Admin/RptProveedorMunicipio/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/RptProveedorMunicipio/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/RptProveedorMunicipio/Create
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

        // GET: Admin/RptProveedorMunicipio/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/RptProveedorMunicipio/Edit/5
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

        // GET: Admin/RptProveedorMunicipio/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/RptProveedorMunicipio/Delete/5
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
        [HttpPost]
        public ActionResult Municipio(string idPais, int id)
        {
            try
            {
                RptProveedorMunicipioModels reporte = new RptProveedorMunicipioModels();
                _RptProveedorMunicipio_Datos reporteDatos = new _RptProveedorMunicipio_Datos();
                reporte.conexion = Conexion;
                reporte.IDEstado = id;
                reporte.listaMunicipio = reporteDatos.obtenerListaMunicipios(reporte);
                
                return Json(reporte.listaMunicipio, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }
    }
}
