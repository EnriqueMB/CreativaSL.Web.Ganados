using CreativaSL.Web.Ganados.Filters;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CreativaSL.Web.Ganados.Models;
using System.Data;
using System.Globalization;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    [Autorizado]
    public class CatChoferController : Controller
    {
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
        // GET: Admin/CatChofer
        public ActionResult Index()
        {
            try
            {
                CatChoferModels Chofer = new CatChoferModels();
                CatChofer_Datos ChoferDatos = new CatChofer_Datos();
                Chofer.Conexion = Conexion;

                Chofer.ListaChoferes = ChoferDatos.ObtenerCatChofer(Chofer);
                return View(Chofer);
            }
            catch (Exception)
            {
                CatChoferModels Chofer = new CatChoferModels();
                Chofer.TablaDatos = new DataTable();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Chofer);
            }
        }

        // GET: Admin/CatChofer/Create
        public ActionResult Create()
        {
            try
            {
                CatChoferModels Chofer = new CatChoferModels();
                CatChofer_Datos ChoferDatos = new CatChofer_Datos();
              
                Chofer.Licencia = Convert.ToBoolean("true");
                return View(Chofer);
            }
            catch (Exception)
            {
                CatChoferModels Chofer = new CatChoferModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Chofer);
            }            
        }

        // POST: Admin/CatChofer/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
                {
                CatChoferModels Chofer = new CatChoferModels();
                CatChofer_Datos ChoferDatos = new CatChofer_Datos();

                
                Chofer.Conexion = Conexion;
                Chofer.Licencia = collection["Licencia"].StartsWith("true");
                Chofer.numLicencia = collection["numLicencia"];
                //Chofer.vigencia = DateTime.ParseExact(collection["vigencia"], "yyyy/MM/dd", CultureInfo.InvariantCulture);
                DateTime Fecha = DateTime.Now;
                DateTime.TryParse(collection["vigencia"], out Fecha);
                Chofer.vigencia = Fecha;
                Chofer.Nombre = collection["nombre"];
                Chofer.ApPaterno = collection["ApPaterno"];
                Chofer.ApMaterno = collection["ApMaterno"];
                Chofer.Usuario = User.Identity.Name;
                Chofer.Opcion = 1;
                Chofer = ChoferDatos.AbcCatChofer(Chofer);

                //Si abc fue completado correctamente
                if (Chofer.Completado == true)
                {
                    TempData["typemessage"] = "1";
                    TempData["message"] = "El registro se guardo correctamente.";
                    return RedirectToAction("Index");
                }
                else
                {
                    Chofer.Licencia = true;
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Ocurrió un error al guardar el registro.";
                    return View(Chofer);
                }
            }
            catch (Exception ex)
            {
                CatChoferModels Chofer = new CatChoferModels();
                Chofer.TablaDatos = new DataTable();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se pudo guardar los datos. Por favor contacte a soporte técnico";
                return View(Chofer);
            }
        }

        // GET: Admin/CatChofer/Edit/5
        //======================== EDITAR ===========================
        //Obtiene el detalle del registro del chofer para ser editado
        //===========================================================
        public ActionResult Edit(string id)
        {
            try
            {
                CatChoferModels Chofer = new CatChoferModels();
                CatChofer_Datos ChoferDatos = new CatChofer_Datos();
                Chofer.IDChofer = id;
                Chofer.Conexion = Conexion;
                Chofer = ChoferDatos.ObtenerDetalleCatChofer(Chofer);
                return View(Chofer);
            }
            catch (Exception)
            {
                CatChoferModels Chofer = new CatChoferModels();
                Chofer.TablaDatos = new DataTable();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Chofer);
            }
        }

        // POST: Admin/CatChofer/Edit/5
        //Obtiene los datos del registro del chofer para ser editado
        [HttpPost]
        public ActionResult Edit(string id, FormCollection collection)
        {
            try
            {
                CatChoferModels Chofer = new CatChoferModels();
                CatChofer_Datos ChoferDatos = new CatChofer_Datos();
                Chofer.Conexion = Conexion;
                Chofer.IDChofer = id;
                Chofer.Nombre = collection["nombre"];
                Chofer.ApPaterno = collection["ApPaterno"];
                Chofer.ApMaterno = collection["ApMaterno"];
                Chofer.Licencia = collection["Licencia"].StartsWith("true");
                Chofer.numLicencia = collection["numLicencia"];
                //Chofer.vigencia = DateTime.ParseExact(collection["vigencia"], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime Fecha = DateTime.Now;
                DateTime.TryParse(collection["vigencia"], out Fecha);
                Chofer.vigencia = Fecha;
                Chofer.Usuario = User.Identity.Name;
                Chofer.Opcion = 2;
                Chofer = ChoferDatos.AbcCatChofer(Chofer);
                //Si abc fue completado correctamente
                if (Chofer.Completado == true)
                {
                    TempData["typemessage"] = "1";
                    TempData["message"] = "El registro se guardo correctamente.";
                    return RedirectToAction("Index");
                }
                else
                {
                    Chofer.Licencia = true;
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Ocurrió un error al guardar el registro.";
                    return View(Chofer);
                }
            }
            catch (Exception ex)
            {
                CatChoferModels Chofer = new CatChoferModels();
                Chofer.TablaDatos = new DataTable();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se pudo guardar los datos. Por favor contacte a soporte técnico";
                return View(Chofer);
            }
        }
        //==============================ELIMINAR CHOFER =================================
        // GET: Admin/CatChofer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/CatChofer/Delete/5
        //recibe el id del chofer para ser eliminado
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                CatChoferModels Chofer = new CatChoferModels();
                CatChofer_Datos ChoferDatos = new CatChofer_Datos();
                Chofer.Conexion = Conexion;
                Chofer.IDChofer = id;
                Chofer.Usuario = User.Identity.Name;
                Chofer = ChoferDatos.EliminarChofer(Chofer);
                TempData["typemessage"] = "1";
                TempData["message"] = "El registro se ha eliminado correctamente";
                return Json("");
            }
            catch
            {
                CatChoferModels Chofer = new CatChoferModels();
                Chofer.TablaDatos = new DataTable();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se pudo borrar los datos. Por favor contacte a soporte técnico";
                return Json("");
               
            }
        }
    }
}
