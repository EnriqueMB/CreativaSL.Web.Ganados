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
    public class CatJaulaController : Controller
    {
        // GET: Admin/CatJaula
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
        public ActionResult Index()
        {
            try
            {
                CatJaulaModels Jaula = new CatJaulaModels();
                _CatJaula_Datos JaulaDatos = new _CatJaula_Datos();
                Jaula.conexion = Conexion;
                Jaula.listaJaulas = JaulaDatos.obtenerCatJaula(Jaula);
               

                return View(Jaula);
            }
            catch (Exception ex)
            {
                CatLugarModels Lugar = new CatLugarModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Lugar);
            }
        }

        // GET: Admin/CatJaula/Details/5
        public ActionResult Details(int id)
        {

            return View();
        }

        // GET: Admin/CatJaula/Create
        public ActionResult Create()
        {
            try {
                CatJaulaModels Jaula = new CatJaulaModels();
                _CatJaula_Datos JaulaDatos = new _CatJaula_Datos();
                Jaula.conexion = Conexion; 
                Jaula.listaSucursales = JaulaDatos.obtenerListaSucursales(Jaula);
                var listaSucursal = new SelectList(Jaula.listaSucursales, "IDSucursal", "NombreSucursal");
                ViewData["cmbSucursal"] = listaSucursal;
                Jaula.Estatus = Convert.ToBoolean("true");
                return View(Jaula);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // POST: Admin/CatJaula/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                CatJaulaModels Jaula = new CatJaulaModels();
                _CatJaula_Datos JaulaDatos = new _CatJaula_Datos();
                Jaula.conexion = Conexion;
                Jaula.opcion = 1;
                Jaula.Estatus= collection["Estatus"].StartsWith("true");
                Jaula.IDSucursal = collection["listaSucursales"];
                Jaula.Matricula = collection["Matricula"];
                Jaula.user = User.Identity.Name;
                Jaula = JaulaDatos.AbcCatJaula(Jaula);

                if (Jaula.Completado == true)
                {
                    TempData["typemessage"] = "1";
                    TempData["message"] = "El registro se guardo correctamente.";

                }
                else
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Ocurrió un error al guardar el registro.";
                }
                return RedirectToAction("Index");
            }
            catch
            {
                CatJaulaModels Jaula = new CatJaulaModels();

                TempData["typemessage"] = "2";
                TempData["message"] = "No se pudo guardar los datos. Por favor contacte a soporte técnico";
                return View(Jaula);
            }
        }

        // GET: Admin/CatJaula/Edit/5
        public ActionResult Edit(string id)
        {
            try
            {
                CatJaulaModels Jaula = new CatJaulaModels();
                _CatJaula_Datos JaulaDatos = new _CatJaula_Datos();
                Jaula.conexion = Conexion;
                Jaula.IDJaula = id;
                Jaula.listaSucursales = JaulaDatos.obtenerListaSucursales(Jaula);
                var listaSucursal = new SelectList(Jaula.listaSucursales, "IDSucursal", "NombreSucursal");
                ViewData["cmbSucursal"] = listaSucursal;

                Jaula = JaulaDatos.ObtenerDetalleCatJaula(Jaula);
                return View(Jaula);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // POST: Admin/CatJaula/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, FormCollection collection)
        {
            try
            {
                CatJaulaModels Jaula = new CatJaulaModels();
                _CatJaula_Datos JaulaDatos = new _CatJaula_Datos();
                Jaula.conexion = Conexion;
                Jaula.IDJaula = id;
                Jaula.opcion = 2;
                Jaula.Estatus = collection["Estatus"].StartsWith("true");
                Jaula.IDSucursal = collection["listaSucursales"];
                Jaula.Matricula = collection["Matricula"];
                Jaula.user = User.Identity.Name;
                Jaula = JaulaDatos.AbcCatJaula(Jaula);

                if (Jaula.Completado == true)
                {
                    TempData["typemessage"] = "1";
                    TempData["message"] = "El registro se guardo correctamente.";

                }
                else
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Ocurrió un error al guardar el registro.";
                }
                return RedirectToAction("Index");
            }
            catch
            {
                CatJaulaModels Jaula = new CatJaulaModels();

                TempData["typemessage"] = "2";
                TempData["message"] = "No se pudo guardar los datos. Por favor contacte a soporte técnico";
                return View(Jaula);
            }
        }

        // GET: Admin/CatJaula/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/CatJaula/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                CatJaulaModels Jaula = new CatJaulaModels();
                _CatJaula_Datos JaulaDatos = new _CatJaula_Datos();
                Jaula.conexion = Conexion;
                Jaula.IDJaula = id;
                Jaula.user = User.Identity.Name;
                // TODO: Add delete logic here
                Jaula = JaulaDatos.EliminarJaula(Jaula);
                TempData["typemessage"] = "1";
                TempData["message"] = "El registro se ha eliminado correctamente";
                return Json("");
            }
            catch
            {
                return View();
            }
        }
    }
}
