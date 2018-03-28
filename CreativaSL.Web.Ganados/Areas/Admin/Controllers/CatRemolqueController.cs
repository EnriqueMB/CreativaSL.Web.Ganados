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
    public class CatRemolqueController : Controller
    {
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
        // GET: Admin/CatRemolque
        public ActionResult Index()
        {
            try
            {
                CatRemolqueModels Remolque = new CatRemolqueModels();
                _CatRemolque_Datos RemolqueDatos = new _CatRemolque_Datos();
                Remolque.Conexion = Conexion;
                Remolque.listaRemolque = RemolqueDatos.ObtenerCatRemolque(Remolque);
                return View(Remolque);
            }
            catch (Exception ex)
            {
                CatRemolqueModels Remolque = new CatRemolqueModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Remolque);
            }
        }

        // GET: Admin/CatRemolque/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/CatRemolque/Create
        public ActionResult Create()
        {
            try
            {
                CatRemolqueModels Remolque = new CatRemolqueModels();
                _CatRemolque_Datos RemolqueDatos = new _CatRemolque_Datos();
                Remolque.Conexion = Conexion;
                Remolque.listaSucursales = RemolqueDatos.obtenerListaSucursales(Remolque);
                var listaSucursal = new SelectList(Remolque.listaSucursales, "IDSucursal", "NombreSucursal");

              
                return View(Remolque);
            }
            catch (Exception ex)
            {
                CatRemolqueModels Remolque = new CatRemolqueModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Remolque);
            }
        }

        // POST: Admin/CatRemolque/Create
        [HttpPost]
        public ActionResult Create(CatRemolqueModels Remolque)
        {
            try
            {

                _CatRemolque_Datos RemolqueDatos = new _CatRemolque_Datos();
                Remolque.Conexion = Conexion;
                Remolque.IDRemolque = "-";
                Remolque.Opcion = 1;
                Remolque.Usuario = User.Identity.Name;
                Remolque = RemolqueDatos.AcCatRemolque(Remolque);

                if (Remolque.Completado == true)
                {
                    TempData["typemessage"] = "1";
                    TempData["message"] = "El registro se guardo correctamente.";
                    return RedirectToAction("Index");
                }
                else
                {
                    Remolque.Estatus = true;
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Ocurrió un error al guardar el registro. Intente más tarde.";
                    return View(Remolque);
                }
            }
            catch
            {
                _CatRemolque_Datos RemolqueDatos = new _CatRemolque_Datos();
                Remolque.listaSucursales = RemolqueDatos.obtenerListaSucursales(Remolque);
                var listaSucursal = new SelectList(Remolque.listaSucursales, "IDSucursal", "NombreSucursal");
                TempData["typemessage"] = "2";
                TempData["message"] = "No se pudo guardar los datos. Por favor contacte a soporte técnico.";
                return View(Remolque);
            }
        }

        // GET: Admin/CatRemolque/Edit/5
        public ActionResult Edit(string id)
        {
            try
            {
                CatRemolqueModels Remolque = new CatRemolqueModels();
                _CatRemolque_Datos RemolqueDatos = new _CatRemolque_Datos();
                Remolque.Conexion = Conexion;
                Remolque.IDRemolque = id;
                Remolque.listaSucursales = RemolqueDatos.obtenerListaSucursales(Remolque);
                var listaSucursal = new SelectList(Remolque.listaSucursales, "IDSucursal", "NombreSucursal");
                Remolque = RemolqueDatos.ObtenerDetalleCatRemolque(Remolque);
               
                return View(Remolque);
            }
            catch (Exception ex)
            {
                CatRemolqueModels Remolque = new CatRemolqueModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Remolque);
            }
        }

        // POST: Admin/CatRemolque/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, CatRemolqueModels Remolque)
        {
            try
            {

                _CatRemolque_Datos RemolqueDatos = new _CatRemolque_Datos();
                Remolque.Conexion = Conexion;
                Remolque.Opcion = 2;
                Remolque.IDRemolque = id;
                Remolque.Usuario = User.Identity.Name;
                Remolque = RemolqueDatos.AcCatRemolque(Remolque);

                if (Remolque.Completado == true)
                {
                    TempData["typemessage"] = "1";
                    TempData["message"] = "El registro se guardo correctamente.";
                    return RedirectToAction("Index");
                }
                else
                {
                    Remolque.Estatus = true;
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Ocurrió un error al guardar el registro. Intente más tarde.";
                    return View(Remolque);
                }
            }
            catch
            {

                TempData["typemessage"] = "2";
                TempData["message"] = "No se pudo guardar los datos. Por favor contacte a soporte técnico.";
                return View(Remolque);
            }
        }

        // GET: Admin/CatRemolque/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/CatRemolque/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                CatRemolqueModels Remolque = new CatRemolqueModels();
                _CatRemolque_Datos RemolqueDatos = new _CatRemolque_Datos();
                Remolque.Conexion = Conexion;
                Remolque.IDRemolque = id;
                Remolque.Usuario = User.Identity.Name;
                // TODO: Add delete logic here
                Remolque = RemolqueDatos.EliminarRemolque(Remolque);
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
