using CreativaSL.Web.Ganados.Filters;
using CreativaSL.Web.Ganados.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    [Autorizado]
    public class CatProductosAlmacenController : Controller
    {
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
        // GET: Admin/CatProductosAlmacen
        public ActionResult Index()
        {
            try
            {
                CatProductosAlmacenModels Producto = new CatProductosAlmacenModels();
                _CatProductosAlmacen_Datos ProductoDatos = new _CatProductosAlmacen_Datos();
                Producto.Conexion = Conexion;
                Producto.listaPrdocutosAlmacen = ProductoDatos.ObtenerCatProductosAlmacen(Producto);
                return View(Producto);
            }
            catch (Exception ex) {
                CatProductosAlmacenModels Producto = new CatProductosAlmacenModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista" + ex;
                return View(Producto);
            }
            
        }

        // GET: Admin/CatProductosAlmacen/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/CatProductosAlmacen/Create
        public ActionResult Create()
        {
            try
            {
                CatProductosAlmacenModels Producto = new CatProductosAlmacenModels();
                _CatProductosAlmacen_Datos ProductoDatos = new _CatProductosAlmacen_Datos();
                Producto.Conexion = Conexion;
                Producto.listaTipoCodigoProducto = ProductoDatos.obtenerComboCatTipoCodigo(Producto);
                Producto.listaUnidadMedida = ProductoDatos.obtenerComboCatUnidadMedida(Producto);
                return View(Producto);
            }
            catch (Exception ex) {
                CatProductosAlmacenModels Producto = new CatProductosAlmacenModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Producto);
            }
        }

        // POST: Admin/CatProductosAlmacen/Create
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

        // GET: Admin/CatProductosAlmacen/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/CatProductosAlmacen/Edit/5
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

        // GET: Admin/CatProductosAlmacen/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/CatProductosAlmacen/Delete/5
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
