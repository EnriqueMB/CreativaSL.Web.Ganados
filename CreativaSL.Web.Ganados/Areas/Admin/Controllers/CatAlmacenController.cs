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
    public class CatAlmacenController : Controller
    {
        private TokenProcessor Token = TokenProcessor.GetInstance();
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
        // GET: Admin/CatAlmacen
        public ActionResult Index()
        {
            try
            {
                CatAlmacenModels Almacen = new CatAlmacenModels();
                _CatAlmacen_Datos AlmacenDatos = new _CatAlmacen_Datos();
                Almacen.Conexion = Conexion;
                Almacen.ListaAlmacen = AlmacenDatos.ObtenerCatAlmacen(Almacen);
                return View(Almacen);
            }
            catch (Exception ex)
            {
                CatLugarModels Lugar = new CatLugarModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Lugar);
            }
        }

        // GET: Admin/CatAlmacen/Details/5 
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/CatAlmacen/Create
        public ActionResult Create()
        {
            try
            {
                Token.SaveToken();
                CatAlmacenModels Almacen = new CatAlmacenModels();
                _CatAlmacen_Datos AlmacenDatos = new _CatAlmacen_Datos();
                Almacen.Conexion = Conexion;
                Almacen.ListaSucursales = AlmacenDatos.obtenerListaSucursales(Almacen);
                var listaSucursal = new SelectList(Almacen.ListaSucursales, "IDSucursal", "NombreSucursal");
                ViewData["cmbSucursal"] = listaSucursal;
                return View(Almacen);
            }
            catch (Exception ex)
            {
                CatAlmacenModels Almacen = new CatAlmacenModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Almacen);
            }
        }

        // POST: Admin/CatAlmacen/Create
        [HttpPost]
        public ActionResult Create(CatAlmacenModels Almacen)
        {
            
            _CatAlmacen_Datos AlmacenDatos = new _CatAlmacen_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        Almacen.Conexion = Conexion;

                        Almacen.Usuario = User.Identity.Name;
                        Almacen.Opcion = 1;
                        Almacen.IDAlmacen = string.Empty;
                        Almacen = AlmacenDatos.AcCatAlmacen(Almacen);
                        //Si abc fue completado correctamente
                        if (Almacen.Completado == true)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "El registro se guardo correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrió un error al guardar el registro.";
                            Almacen.ListaSucursales = AlmacenDatos.obtenerListaSucursales(Almacen);
                            var listaSucursal = new SelectList(Almacen.ListaSucursales, "IDSucursal", "NombreSucursal");
                            ViewData["cmbSucursal"] = listaSucursal;
                            return View(Almacen);
                        }
                    }
                    else {
                        Almacen.Conexion = Conexion;
                        Almacen.ListaSucursales = AlmacenDatos.obtenerListaSucursales(Almacen);
                        return View(Almacen);
                    }                   
                }
                else
                {
                    Almacen.Conexion = Conexion;
                    Almacen.ListaSucursales = AlmacenDatos.obtenerListaSucursales(Almacen);
                    return View(Almacen);
                }
            }
            catch (Exception)
            {
                Almacen.ListaSucursales = AlmacenDatos.obtenerListaSucursales(Almacen);
                var listaSucursal = new SelectList(Almacen.ListaSucursales, "IDSucursal", "NombreSucursal");
                ViewData["cmbSucursal"] = listaSucursal;
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Almacen);
            }
        }

        // GET: Admin/CatAlmacen/Edit/5
        public ActionResult Edit(string id)
        {
            try
            {
                Token.SaveToken();
                CatAlmacenModels Almacen = new CatAlmacenModels();
                _CatAlmacen_Datos AlmacenDatos = new _CatAlmacen_Datos();
                Almacen.Conexion = Conexion;
                Almacen.IDAlmacen = id;
                Almacen.ListaSucursales = AlmacenDatos.obtenerListaSucursales(Almacen);
                var listaSucursal = new SelectList(Almacen.ListaSucursales, "IDSucursal", "NombreSucursal");
                ViewData["cmbSucursal"] = listaSucursal;
                Almacen = AlmacenDatos.ObtenerDetalleCatAlmacen(Almacen);

                return View(Almacen);
            }
            catch (Exception ex)
            {
                CatAlmacenModels Almacen = new CatAlmacenModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Almacen);
            }
        }

        // POST: Admin/CatAlmacen/Edit/5
        [HttpPost]
        public ActionResult Edit(CatAlmacenModels Almacen)
        {
            _CatAlmacen_Datos AlmacenDatos = new _CatAlmacen_Datos();
            try
            {

                if(Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        Almacen.Conexion = Conexion;

                        Almacen.Usuario = User.Identity.Name;
                        Almacen.Opcion = 2;
                        Almacen = AlmacenDatos.AcCatAlmacen(Almacen);
                        //Si abc fue completado correctamente
                        if (Almacen.Completado == true)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "El registro se guardo correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrió un error al guardar el registro.";
                            return View(Almacen);
                        }
                    }
                    else
                    {
                        Almacen.Conexion = Conexion;
                        Almacen.ListaSucursales = AlmacenDatos.obtenerListaSucursales(Almacen);
                        return View(Almacen);
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                CatProductosModels Producto = new CatProductosModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Producto);
            }
        }

        // GET: Admin/CatAlmacen/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/CatAlmacen/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {

                CatAlmacenModels Almacen = new CatAlmacenModels();
                _CatAlmacen_Datos AlmacenDatos = new _CatAlmacen_Datos();
                Almacen.Conexion = Conexion;
                Almacen.IDAlmacen = id;
                Almacen.Usuario = User.Identity.Name;
                Almacen = AlmacenDatos.EliminarAlmacen(Almacen);
                //TempData["typemessage"] = "1";
                //TempData["message"] = "El registro se ha eliminado correctamente";
                return Json("");
                // TODO: Add delete logic here
            }
            catch
            {
                CatAlmacenModels Almacen = new CatAlmacenModels();

                //TempData["typemessage"] = "2";
                //TempData["message"] = "No se pudo borrar los datos. Por favor contacte a soporte técnico";
                return Json("");

            }
        }
    }
}
