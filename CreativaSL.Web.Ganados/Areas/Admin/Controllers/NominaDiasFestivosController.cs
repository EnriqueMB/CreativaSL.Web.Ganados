using CreativaSL.Web.Ganados.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CreativaSL.Web.Ganados.Filters;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    [Autorizado]
    public class NominaDiasFestivosController : Controller
    {
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
        // GET: Admin/NominaDiasFestivos
        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                NominaDiasFestivosModels nomina = new NominaDiasFestivosModels();
                _NominaDiasFestivos_Datos nominaDatos = new _NominaDiasFestivos_Datos();
                nomina.Conexion = Conexion;
                nomina = nominaDatos.ObtenerListaNominaDias(nomina);
                return View(nomina);
            }
            catch (Exception)
            {
                NominaDiasFestivosModels nomina = new NominaDiasFestivosModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(nomina);
            }
        }

        // GET: Admin/NominaDiasFestivos/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/NominaDiasFestivos/Create
        public ActionResult Create()
        {
            
            try
            {
                NominaDiasFestivosModels nomina = new NominaDiasFestivosModels();
                return View(nomina);
            }
            catch (Exception)
            {
                NominaDiasFestivosModels nomina = new NominaDiasFestivosModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(nomina);
            }
        }

        // POST: Admin/NominaDiasFestivos/Create
        [HttpPost]
        public ActionResult Create(NominaDiasFestivosModels nomina)
        {
            _NominaDiasFestivos_Datos nominaDatos = new _NominaDiasFestivos_Datos();
            try
            {
                if (ModelState.IsValid)
                {
                    nomina.Conexion = Conexion;
                    nomina.Opcion = 1;
                    nomina.Usuario = User.Identity.Name;
                    nomina = nominaDatos.AbcCatNominaDiasFestivos(nomina);
                    if (nomina.Completado == true)
                    {
                        TempData["typemessage"] = "1";
                        TempData["message"] = "Los datos se guardaron correctamente.";
                        return RedirectToAction("Index");
                    }
                    else
                    {

                        TempData["typemessage"] = "2";
                        TempData["message"] = "Ocurrió un error al intentar guardar.";
                        return View(nomina);
                    }
                }
                else
                {
                    nomina.Conexion = Conexion;
                    return View(nomina);
                }
            }
            catch
            {
                nomina.Conexion = Conexion;
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrió un error el intentar guardar. Contacte a soporte técnico";
                return View(nomina);
            }
        }

        // GET: Admin/NominaDiasFestivos/Edit/5
        public ActionResult Edit(string id)
        {
            try
            {
                NominaDiasFestivosModels nomina = new NominaDiasFestivosModels();
                _NominaDiasFestivos_Datos nominaDatos = new _NominaDiasFestivos_Datos();
                nomina.Conexion = Conexion;
                nomina.IdDiasFestivos = id;
                nomina = nominaDatos.ObtenerDetalleNomDiasFestivosxID(nomina);
                return View(nomina);
            }
            catch (Exception)
            {
                NominaDiasFestivosModels nomina = new NominaDiasFestivosModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(nomina);
            }
        }

        // POST: Admin/NominaDiasFestivos/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, NominaDiasFestivosModels nomina)
        {
            _NominaDiasFestivos_Datos nominaDatos = new _NominaDiasFestivos_Datos();
            try
            {
                if (ModelState.IsValid)
                {
                    nomina.Conexion = Conexion;
                    nomina.Opcion = 2;
                    nomina.Usuario = User.Identity.Name;
                    nomina = nominaDatos.AbcCatNominaDiasFestivos(nomina);
                    if (nomina.Completado == true)
                    {
                        TempData["typemessage"] = "1";
                        TempData["message"] = "Los datos se guardaron correctamente.";
                        return RedirectToAction("Index");
                    }
                    else
                    {

                        TempData["typemessage"] = "2";
                        TempData["message"] = "Ocurrió un error al intentar guardar.";
                        return RedirectToAction("Edit", "NominaDiasFestivos");
                    }
                }
                else
                {
                    nomina.Conexion = Conexion;
                    return View(nomina);
                }
            }
            catch
            {
                nomina.Conexion = Conexion;
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrió un error el intentar guardar. Contacte a soporte técnico";
                return View(nomina);
            }
        }

        // GET: Admin/NominaDiasFestivos/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/NominaDiasFestivos/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                NominaDiasFestivosModels nomina = new NominaDiasFestivosModels();
                _NominaDiasFestivos_Datos nominaDatos = new _NominaDiasFestivos_Datos();
                nomina.Conexion = Conexion;
                nomina.IdDiasFestivos = id;
                nomina.Opcion = 3;
                nomina.Usuario = User.Identity.Name;
                nomina = nominaDatos.EliminarNomDiasFestivos(nomina);
                if (nomina.Completado == true)
                {
                    TempData["typemessage"] = "1";
                    TempData["message"] = "El registro se elimino correctamente.";
                    return Json("");
                }
                else
                {

                    TempData["typemessage"] = "2";
                    TempData["message"] = "Ocurrió un error al intentar eliminar.";
                    return View(nomina);
                }
            }
            catch
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrió un error el intentar guardar. Contacte a soporte técnico";
                return View();
            }
        }
    }
}
