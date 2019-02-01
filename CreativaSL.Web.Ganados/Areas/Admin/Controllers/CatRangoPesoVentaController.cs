using CreativaSL.Web.Ganados.App_Start;
using CreativaSL.Web.Ganados.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    public class CatRangoPesoVentaController : Controller
    {
        private TokenProcessor Token = TokenProcessor.GetInstance();
        private string Conexion = ConfigurationManager.AppSettings.Get("strConnection");

        // GET: Admin/CatRangoPrecioCliente
        public ActionResult Index()
        {
            return View();
        }

        // GET: Admin/CatRangoPrecioCliente/Create
        public ActionResult Create()
        {
            try
            {
                Token.SaveToken();
                CatRangoPesoVentaModels Rango = new CatRangoPesoVentaModels();
                _CatRangoPesoVenta_Datos RangoDatos = new _CatRangoPesoVenta_Datos();
                //ViewBag.ListaTipoClientes = RangoDatos.ObetenerListaTipoProveedor(Rango, Conexion);
                Rango.EsMacho = true;
                return View(Rango);
            }
            catch (Exception)
            {
                CatRangoPesoCompraModels RangoPeso = new CatRangoPesoCompraModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("Index");
            }
        }

        // POST: Admin/CatRangoPrecioCliente/Create
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

        // GET: Admin/CatRangoPrecioCliente/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/CatRangoPrecioCliente/Edit/5
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

        // GET: Admin/CatRangoPrecioCliente/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/CatRangoPrecioCliente/Delete/5
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

        #region DataTable
        [HttpPost]
        public ActionResult LoadTblRangoPesoVenta()
        {
            try
            {
                _CatRangoPesoVenta_Datos Datos = new _CatRangoPesoVenta_Datos();
                string datatable = Datos.RangoPesoVenta_index_RangoPesoVenta(Conexion);

                return Content(datatable, "application/json");
            }
            catch (Exception ex)
            {
                return Content("", "application/json");
            }
        }
        #endregion
    }
}
