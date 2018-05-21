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
                combustibleModels.listaProveedoresCombustible = datos.ObtenerCatProveedores(combustibleModels);
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
            try
            {
                Token.SaveToken();
                _Combos_Datos cmb = new _Combos_Datos();
                CatProveedorCombustibleModels combustibleModels = new CatProveedorCombustibleModels();
                _CatProveedorCombustible_Datos datos = new _CatProveedorCombustible_Datos();
                combustibleModels.Conexion = Conexion;
                combustibleModels.listaSucursal = cmb.ObtenerComboSucursales(combustibleModels.Conexion);
               
                return View(combustibleModels);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // POST: Admin/CatProveedorCombustible/Create
        [HttpPost]
        public ActionResult Create(CatProveedorCombustibleModels combustibleModels)
        {
            _CatProveedorCombustible_Datos Proovedordatos = new _CatProveedorCombustible_Datos();
            _Combos_Datos cmb = new _Combos_Datos();
            combustibleModels.Conexion = Conexion;
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {

                        
                        combustibleModels.Usuario = User.Identity.Name;
                        combustibleModels.Opcion = 1;
                        combustibleModels = Proovedordatos.acCatProveedorCombustible(combustibleModels);
                        if (combustibleModels.Completado)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "Los datos se guardaron correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            
                            combustibleModels.listaSucursal = cmb.ObtenerComboSucursales(combustibleModels.Conexion);

                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrio un error al intentar guardar los datos. Intente más tarde.";
                            return View(combustibleModels);
                        }
                    }
                    else
                    {
                        
                        combustibleModels.listaSucursal = cmb.ObtenerComboSucursales(combustibleModels.Conexion);

                        return View(combustibleModels);
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
              
                combustibleModels.listaSucursal = cmb.ObtenerComboSucursales(combustibleModels.Conexion);
                
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error al intentar guardar los datos. Contacte a soporte técnico.";
                return View(combustibleModels);
            }
        }

        // GET: Admin/CatProveedorCombustible/Edit/5
        public ActionResult Edit(string id)
        {
            try
            {
                Token.SaveToken();
                _Combos_Datos cmb = new _Combos_Datos();
                CatProveedorCombustibleModels combustibleModels = new CatProveedorCombustibleModels();
                _CatProveedorCombustible_Datos datos = new _CatProveedorCombustible_Datos();
                combustibleModels.Conexion = Conexion;
                combustibleModels.IDProveedor = id;
                combustibleModels.listaSucursal = cmb.ObtenerComboSucursales(combustibleModels.Conexion);
                combustibleModels = datos.ObtenerDetalleCatProveedor(combustibleModels);

                return View(combustibleModels);
            }
            catch (Exception ex)
            {
                CatProveedorModels Proveedor = new CatProveedorModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("Index");
            }
        }

        // POST: Admin/CatProveedorCombustible/Edit/5
        [HttpPost]
        public ActionResult Edit(CatProveedorCombustibleModels combustibleModels)
        {
            _CatProveedorCombustible_Datos Proovedordatos = new _CatProveedorCombustible_Datos();
            _Combos_Datos cmb = new _Combos_Datos();
            combustibleModels.Conexion = Conexion;
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {


                        combustibleModels.Usuario = User.Identity.Name;
                        combustibleModels.Opcion = 2;
                        combustibleModels = Proovedordatos.acCatProveedorCombustible(combustibleModels);
                        if (combustibleModels.Completado)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "Los datos se guardaron correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("Index");
                        }
                        else
                        {

                            combustibleModels.listaSucursal = cmb.ObtenerComboSucursales(combustibleModels.Conexion);

                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrio un error al intentar guardar los datos. Intente más tarde.";
                            return View(combustibleModels);
                        }
                    }
                    else
                    {

                        combustibleModels.listaSucursal = cmb.ObtenerComboSucursales(combustibleModels.Conexion);

                        return View(combustibleModels);
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {

                combustibleModels.listaSucursal = cmb.ObtenerComboSucursales(combustibleModels.Conexion);

                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error al intentar guardar los datos. Contacte a soporte técnico.";
                return View(combustibleModels);
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
