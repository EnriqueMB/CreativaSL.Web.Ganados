using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CreativaSL.Web.Ganados.Models;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    public class CompraController : Controller
    {
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
        // GET: Admin/Compra
        public ActionResult Index()
        {
            try
            {
                CompraModels Compra = new CompraModels();
                _Compra_Datos CompraDatos = new _Compra_Datos();
                Compra.Conexion = Conexion;
                Compra = CompraDatos.ObtenerCompraIndex(Compra);
                return View(Compra);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET: Admin/Compra/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Compra/Create
        public ActionResult Create()
        {
            try
            {
                CompraModels Compra = new CompraModels();
                _Compra_Datos CompraDatos = new _Compra_Datos();
                Compra.Conexion = Conexion;
                Compra.TablaProveedoresCmb = CompraDatos.ObtenerListadoProveedores(Compra);
                var ListProveedores = new SelectList(Compra.TablaProveedoresCmb, "IDProveedor", "NombreRazonSocial");
                ViewData["cmbProveedores"] = ListProveedores;
                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // POST: Admin/Compra/Create
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

        // GET: Admin/Compra/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Compra/Edit/5
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

        // GET: Admin/Compra/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Compra/Delete/5
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
