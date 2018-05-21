using CreativaSL.Web.Ganados.App_Start;
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
    public class CatProveedorCombustibleController : Controller
    {
        private TokenProcessor Token = TokenProcessor.GetInstance();
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
        // GET: Admin/CatProveedorCombustible
        public ActionResult Index()
        {
            try
            {
                _Combos_Datos cmb = new _Combos_Datos();
                CatProveedorCombustibleModels combustibleModels = new CatProveedorCombustibleModels();
                _CatProveedorCombustible_Datos datos = new _CatProveedorCombustible_Datos();
                combustibleModels.Conexion = Conexion;
                //combustibleModels = 
                return View(combustibleModels);
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        // GET: Admin/CatProveedorCombustible/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/CatProveedorCombustible/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/CatProveedorCombustible/Create
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

        // GET: Admin/CatProveedorCombustible/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/CatProveedorCombustible/Edit/5
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

        // GET: Admin/CatProveedorCombustible/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/CatProveedorCombustible/Delete/5
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
