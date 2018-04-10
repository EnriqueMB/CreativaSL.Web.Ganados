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
                _Combos_Datos Combos = new _Combos_Datos();
                Nomina.Conexion = Conexion;
                Nomina.ListaSucursales = Combos.ObtenerComboSucursales(Conexion);
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

        //POST: Admin/Nomina/3
        [HttpPost]
        public ActionResult Index(NominaModels Nomina)
        {
            try
            {
                Nomina_Datos NominaDatos = new Nomina_Datos();
                _Combos_Datos Combos = new _Combos_Datos();
                Nomina.Conexion = Conexion;
                Nomina.ListaSucursales = Combos.ObtenerComboSucursales(Conexion);
                Nomina.EsBusqueda = true;
                if (!Nomina.BandBusqClave)
                {
                    Nomina.ClaveNomina = string.Empty;
                }
                if (!Nomina.BandIDSucursal)
                {
                    Nomina.IDSucursal = string.Empty;
                }
                if (!Nomina.BandBusqFechas)
                {
                    Nomina.FechaInicio = DateTime.Today;
                    Nomina.FechaFin = DateTime.Today;
                }
                if (string.IsNullOrEmpty(Nomina.IDSucursal))
                {
                    Nomina.BandIDSucursal = false;
                }
                if (!Nomina.BandBusqClave && !Nomina.BandIDSucursal && !Nomina.BandBusqFechas)
                {
                    Nomina.EsBusqueda = false;
                }
                Nomina.ListaNomina = NominaDatos.ObtenerListaNomina(Nomina);
                return View(Nomina);
            }
            catch (Exception)
            {

                throw;
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
            try
            {
                NominaModels Nomina = new NominaModels();
                Nomina_Datos NominaDatos = new Nomina_Datos();
                _Combos_Datos Combos = new _Combos_Datos();
                Nomina.Conexion = Conexion;
                Nomina.ListaSucursales = Combos.ObtenerComboSucursales(Conexion);
                return View(Nomina);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // POST: Admin/Nomina/Create
        [HttpPost]
        public ActionResult Create(NominaModels Nomina)
        {
            try
            {
                
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

        // POST: Admin/Nomina/getDatostablaEmpleado/3
        [HttpPost]
        public ActionResult DatostablaEmpleado(string IDS)
        {
            try
            {
                NominaModels Nomina = new NominaModels();
                Nomina_Datos NominaDatos = new Nomina_Datos();
                Nomina.IDSucursal = IDS;
                Nomina.Conexion = Conexion;
                Nomina.ListaNomina = NominaDatos.ObtenerListaNominaEmpleado(Nomina);
                return Json(Nomina.ListaNomina, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }
    }
}
