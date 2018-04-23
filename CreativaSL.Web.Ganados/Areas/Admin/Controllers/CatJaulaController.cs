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
    public class CatJaulaController : Controller
    {
        private TokenProcessor Token = TokenProcessor.GetInstance();
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
        // GET: Admin/CatJaula
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
                CatJaulaModels Jaula = new CatJaulaModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Jaula);
            }
        }

        // GET: Admin/CatJaula/Details/5
        public ActionResult Details(int id)
        {

            return View();
        }

        // GET: Admin/CatJaula/Create
        [HttpGet]
        public ActionResult Create()
        {
            try
            {
                Token.SaveToken();
                CatJaulaModels Jaula = new CatJaulaModels();
                _CatJaula_Datos JaulaDatos = new _CatJaula_Datos();
                Jaula.conexion = Conexion; 
                Jaula.listaSucursales = JaulaDatos.obtenerListaSucursales(Jaula);
                Jaula.ListaEmpresas = JaulaDatos.ObteneComboCatEmpresa(Jaula);
                return View(Jaula);
            }
            catch (Exception ex)
            {
                CatJaulaModels Jaula = new CatJaulaModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Jaula);
            }
        }

        // POST: Admin/CatJaula/Create
        [HttpPost]
        public ActionResult Create(CatJaulaModels Jaula)
        {
            _CatJaula_Datos JaulaDatos = new _CatJaula_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        Jaula.conexion = Conexion;
                        Jaula.opcion = 1;
                        Jaula.user = User.Identity.Name;
                        Jaula = JaulaDatos.AbcCatJaula(Jaula);
                        if (Jaula.Completado == true)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "El registro se guardo correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            Jaula.Estatus = true;
                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrió un error al guardar el registro. Intente más tarde.";
                            return View(Jaula);
                        }
                    }
                    else
                    {
                        return View(Jaula);
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
                return View(Jaula);
            }
        }

        // GET: Admin/CatJaula/Edit/5
        [HttpGet]
        public ActionResult Edit(string id)
        {
            try
            {
                Token.SaveToken();
                CatJaulaModels Jaula = new CatJaulaModels();
                _CatJaula_Datos JaulaDatos = new _CatJaula_Datos();
                Jaula.conexion = Conexion;
                Jaula.IDJaula = id;
                Jaula.listaSucursales = JaulaDatos.obtenerListaSucursales(Jaula);
                Jaula = JaulaDatos.ObtenerDetalleCatJaula(Jaula);
                Jaula.ListaEmpresas = JaulaDatos.ObteneComboCatEmpresa(Jaula);
                return View(Jaula);
            }
            catch (Exception ex)
            {
                CatJaulaModels Jaula = new CatJaulaModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Jaula);
            }
        }

        // POST: Admin/CatJaula/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, CatJaulaModels Jaula)
        {
            _CatJaula_Datos JaulaDatos = new _CatJaula_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        //CatJaulaModels Jaula = new CatJaulaModels();
                        Jaula.conexion = Conexion;
                        Jaula.IDJaula = id;
                        Jaula.opcion = 2;
                        //Jaula.Estatus = collection["Estatus"].StartsWith("true");
                        //Jaula.IDSucursal = collection["IDSucursal"];
                        //Jaula.Matricula = collection["Matricula"];
                        //Jaula.IDEmpresa = collection["IDEmpresa"];
                        Jaula.user = User.Identity.Name;
                        Jaula = JaulaDatos.AbcCatJaula(Jaula);
                        if (Jaula.Completado == true)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "El registro se guardo correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            Jaula.Estatus = true;
                            Jaula.listaSucursales = JaulaDatos.obtenerListaSucursales(Jaula);
                            Jaula.ListaEmpresas = JaulaDatos.ObteneComboCatEmpresa(Jaula);
                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrió un error al guardar el registro. Intente más tarde.";
                            return View(Jaula);
                        }
                    }
                    else
                    {
                        Jaula.conexion = Conexion;
                        Jaula.listaSucursales = JaulaDatos.obtenerListaSucursales(Jaula);
                        Jaula.ListaEmpresas = JaulaDatos.ObteneComboCatEmpresa(Jaula);
                        return View(Jaula);
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
        [HttpPost]
        public ActionResult ObtenerSucursalesXIDEmpresa(string IDEmpresa)
        {
            try
            {
                CatJaulaModels Jaula = new CatJaulaModels();
                _CatJaula_Datos JaulaDatos = new _CatJaula_Datos();
                Jaula.conexion = Conexion;
                Jaula.IDEmpresa = IDEmpresa;
                Jaula.user = User.Identity.Name;
                Jaula.listaSucursales = JaulaDatos.ObtenerSucursalesXIDEmpresa(Jaula);
                return Content(Jaula.listaSucursales.ToJSON(), "application/json");
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
