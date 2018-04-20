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
    public class CatRemolqueController : Controller
    {
        private TokenProcessor Token = TokenProcessor.GetInstance();
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
                Token.SaveToken();
                CatRemolqueModels Remolque = new CatRemolqueModels();
                _CatRemolque_Datos RemolqueDatos = new _CatRemolque_Datos();
                Remolque.Conexion = Conexion;
                Remolque.listaSucursales = RemolqueDatos.obtenerListaSucursales(Remolque);
                Remolque.ListaEmpresas = RemolqueDatos.ObtenerListaEmpresas(Remolque);
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
            _CatRemolque_Datos RemolqueDatos = new _CatRemolque_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        Remolque.Conexion = Conexion;
                        Remolque.Opcion = 1;
                        Remolque.Usuario = User.Identity.Name;
                        Remolque = RemolqueDatos.AcCatRemolque(Remolque);

                        if (Remolque.Completado == true)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "El registro se guardo correctamente.";
                            Token.ResetToken();
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
                    else
                    {
                        Remolque.Conexion = Conexion;
                        Remolque.listaSucursales = RemolqueDatos.obtenerListaSucursales(Remolque);
                        return View(Remolque);
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                Remolque.Conexion = Conexion;
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
                Token.IsTokenValid();
                CatRemolqueModels Remolque = new CatRemolqueModels();
                _CatRemolque_Datos RemolqueDatos = new _CatRemolque_Datos();
                Remolque.Conexion = Conexion;
                Remolque.IDRemolque = id;
                Remolque.listaSucursales = RemolqueDatos.obtenerListaSucursales(Remolque);
                var listaSucursal = new SelectList(Remolque.listaSucursales, "IDSucursal", "NombreSucursal");
                Remolque = RemolqueDatos.ObtenerDetalleCatRemolque(Remolque);
                Remolque.ListaEmpresas = RemolqueDatos.ObtenerListaEmpresas(Remolque);
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
            _CatRemolque_Datos RemolqueDatos = new _CatRemolque_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        Remolque.Conexion = Conexion;
                        Remolque.Opcion = 2;
                        Remolque.Usuario = User.Identity.Name;
                        Remolque = RemolqueDatos.AcCatRemolque(Remolque);
                        if (Remolque.Completado == true)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "El registro se guardo correctamente.";
                            Token.ResetToken();
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
                    else
                    {
                        Remolque.Conexion = Conexion;
                        Remolque.listaSucursales = RemolqueDatos.obtenerListaSucursales(Remolque);
                        return View(Remolque);
                    }
                }
                else
                {
                    return RedirectToAction("Index");
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
        [HttpPost]
        public ActionResult ObtenerSucursalesXIDEmpresa(string IDEmpresa)
        {
            try
            {
                CatRemolqueModels Remolque = new CatRemolqueModels();
                _CatRemolque_Datos RemolqueDatos = new _CatRemolque_Datos();
                Remolque.Conexion = Conexion;
                Remolque.IDEmpresa = IDEmpresa;
                Remolque.Usuario = User.Identity.Name;
                Remolque.listaSucursales = RemolqueDatos.ObtenerSucursalesXIDEmpresa(Remolque);
                return Content(Remolque.listaSucursales.ToJSON(), "application/json");
            }
            catch
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error. Por favor contacte a soporte técnico";
                return Json("");
            }
        }
    }
}
