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
    public class MantenimientoController : Controller
    {
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
        // GET: Admin/Mantenimiento
        public ActionResult Index()
        {
            try
            {
                ServiciosMantenimientoModels Model = new ServiciosMantenimientoModels();
                _ServicioMantenimiento_Datos Datos = new _ServicioMantenimiento_Datos();
                Model = Datos.ObtenerDatosIndex(Conexion, string.Empty);
                return View(Model);
            }
            catch(Exception)
            {
                return View(new ServiciosMantenimientoModels());
            }
        }

        // GET: Admin/Mantenimiento/Servicios/5
        public ActionResult ServiciosV(string id)
        {
            try
            {
                ViewBag.IDVehiculo = id;
                _ServicioMantenimiento_Datos Datos = new _ServicioMantenimiento_Datos();
                return View(Datos.ObtenerServiciosXIDVehiculo(Conexion, id));
            }
            catch (Exception)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("Index");
            }
        }

        // GET: Admin/Mantenimiento/Servicios/5
        public ActionResult ServiciosR(string id)
        {
            return View();
        }


        // GET: Admin/Mantenimiento/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Mantenimiento/Create
        public ActionResult Create(string id, int tipo)
        {
            try
            {
                ServiciosMantenimientoViewModels Servicio = new ServiciosMantenimientoViewModels();
                _Combos_Datos Datos = new _Combos_Datos();
                Servicio.ListaSucursales = Datos.ObtenerComboSucursales(Conexion);
                return View(Servicio);
            }
            catch (Exception)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("Servicios", new { id = id, tipo = tipo } );
            }
        }

        // POST: Admin/Mantenimiento/Create
        [HttpPost]
        public ActionResult Create(ServiciosMantenimientoViewModels Model)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("");
            }
            catch
            {
                return View();
            }
        }
        // GET: Admin/Mantenimiento/Create
        public ActionResult CreateV(string id)
        {
            try
            {
                ServiciosMantenimientoViewModels Servicio = new ServiciosMantenimientoViewModels();
                _Combos_Datos Datos = new _Combos_Datos();
                Servicio.ListaSucursales = Datos.ObtenerComboSucursales(Conexion);
                return View(Servicio);
            }
            catch (Exception)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("Servicios", new {id = id });
            }
        }

        // POST: Admin/Mantenimiento/Create
        [HttpPost]
        public ActionResult CreateV(ServiciosMantenimientoViewModels Model)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Mantenimiento/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Mantenimiento/Edit/5
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

        // GET: Admin/Mantenimiento/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Mantenimiento/Delete/5
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

        // GET: Admin/Mantenimiento/Servicios/5
        public ActionResult Servicios(string id, int opcion)
        {
            return View();
        }

       





        // GET: Admin/Mantenimiento/Delete/5
        public ActionResult Test()
        {
            ServiciosMantenimientoViewModels Model = new ServiciosMantenimientoViewModels();
            _Combos_Datos Datos = new _Combos_Datos();
            Model.ListaSucursales = Datos.ObtenerComboSucursales(Conexion);
            return View(Model);
        }

        // POST: Admin/Mantenimiento/Delete/5
        [HttpPost]
        public ActionResult Test(ServiciosMantenimientoViewModels Model) //string IDSucursal, List<CatEmpleadoModels> Lista)
        {
            try
            {
                _Combos_Datos Datos = new _Combos_Datos();
                Model.ListaSucursales = Datos.ObtenerComboSucursales(Conexion);
                return View(Model);
            }
            catch
            {
                return View();
            }
        }
    }
}
