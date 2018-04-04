using CreativaSL.Web.Ganados.Models;
using CreativaSL.Web.Ganados.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    public class EntregaCombustibleController : Controller
    {
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
        // GET: Admin/EntregaCombustible
        public ActionResult Index()
        {
            return View();
        }

        // GET: Admin/EntregaCombustible/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/EntregaCombustible/Create
        public ActionResult Create()
        {
            try
            {
                EntregaCombustibleViewModels Entrega = new EntregaCombustibleViewModels();
                _Combos_Datos Datos = new _Combos_Datos();
                Entrega.ListaSucursales = Datos.ObtenerComboSucursales(Conexion);
                Entrega.ListaVehiculos = Datos.ObtenerComboVehiculos(Conexion, string.Empty);
                Entrega.ListaTipoCombustible = Datos.ObtenerComboTiposCombustible(Conexion);
                
                return View(Entrega);
            }
            catch (Exception)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("Index");
            }
        }

        // POST: Admin/EntregaCombustible/Create
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

        // GET: Admin/EntregaCombustible/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/EntregaCombustible/Edit/5
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

        // GET: Admin/EntregaCombustible/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/EntregaCombustible/Delete/5
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
