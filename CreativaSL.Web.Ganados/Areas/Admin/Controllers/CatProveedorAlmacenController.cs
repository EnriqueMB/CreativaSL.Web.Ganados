using CreativaSL.Web.Ganados.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CreativaSL.Web.Ganados.Filters;
using CreativaSL.Web.Ganados.App_Start;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    [Autorizado]
    public class CatProveedorAlmacenController : Controller
    {
        private TokenProcessor Token = TokenProcessor.GetInstance();
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
        // GET: Admin/CatProveedorAlmacen
        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                CatProveedorAlmacenModels proveedor = new CatProveedorAlmacenModels();
                _CatProveedorAlmacen_Datos proveedorDatos = new _CatProveedorAlmacen_Datos();
                proveedor.Conexion = Conexion;
                proveedor = proveedorDatos.ObtenerListaProveedorAlmacen(proveedor);
                return View(proveedor);
            }
            catch (Exception)
            {

                CatProveedorAlmacenModels proveedor = new CatProveedorAlmacenModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(proveedor);
            }
        }

        // GET: Admin/CatProveedorAlmacen/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/CatProveedorAlmacen/Create
        [HttpGet]
        public ActionResult Create()
        {
            try
            {
                Token.SaveToken();
                CatProveedorAlmacenModels proveedor = new CatProveedorAlmacenModels();
                return View(proveedor);
            }
            catch (Exception)
            {
                CatProveedorAlmacenModels proveedor = new CatProveedorAlmacenModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(proveedor);
            }
        }

        // POST: Admin/CatProveedorAlmacen/Create
        [HttpPost]
        public ActionResult Create(CatProveedorAlmacenModels proveedor)
        {
            _CatProveedorAlmacen_Datos proveedorDatos = new _CatProveedorAlmacen_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        proveedor.Conexion = Conexion;
                        proveedor.Opcion = 1;
                        proveedor.Usuario = User.Identity.Name;
                        proveedor = proveedorDatos.AbcCatProveedorAlmacen(proveedor);
                        if (proveedor.Completado == true)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "Los datos se guardaron correctamente.";
                            Token.SaveToken();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrió un error al intentar guardar.";
                            return View(proveedor);
                        }
                    }
                    else
                    {
                        proveedor.Conexion = Conexion;
                        return View(proveedor);
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                proveedor.Conexion = Conexion;
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrió un error el intentar guardar. Contacte a soporte técnico";
                return View(proveedor);
            }
        }

        // GET: Admin/CatProveedorAlmacen/Edit/5
        public ActionResult Edit(string id)
        {
            try
            {
                Token.SaveToken();
                CatProveedorAlmacenModels proveedor = new CatProveedorAlmacenModels();
                _CatProveedorAlmacen_Datos proveedorDatos = new _CatProveedorAlmacen_Datos();
                proveedor.Conexion = Conexion;
                proveedor.IDProveedorAlmacen = id;
                proveedor = proveedorDatos.ObtenerDetalleProveedorAlmacenxID(proveedor);
                return View(proveedor);
            }
            catch (Exception)
            {
                CatProveedorAlmacenModels proveedor = new CatProveedorAlmacenModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(proveedor);
            }
        }

        // POST: Admin/CatProveedorAlmacen/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, CatProveedorAlmacenModels proveedor)
        {
            _CatProveedorAlmacen_Datos proveedorDatos = new _CatProveedorAlmacen_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        proveedor.Conexion = Conexion;
                        proveedor.Opcion = 2;
                        proveedor.Usuario = User.Identity.Name;
                        proveedor = proveedorDatos.AbcCatProveedorAlmacen(proveedor);
                        if (proveedor.Completado == true)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "Los datos se guardaron correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrió un error al intentar guardar.";
                            return RedirectToAction("Edit", "CatProveedorAlmacen");
                        }
                    }
                    else
                    {
                        proveedor.Conexion = Conexion;
                        return View(proveedor);
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
                TempData["message"] = "Ocurrió un error el intentar guardar. Contacte a soporte técnico";
                return View(proveedor);
            }
        }

        // GET: Admin/CatProveedorAlmacen/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/CatProveedorAlmacen/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                CatProveedorAlmacenModels proveedor = new CatProveedorAlmacenModels();
                _CatProveedorAlmacen_Datos proveedorDatos = new _CatProveedorAlmacen_Datos();
                proveedor.Conexion = Conexion;
                proveedor.IDProveedorAlmacen = id;
                proveedor.Opcion = 3;
                proveedor.Usuario= User.Identity.Name;
                proveedor = proveedorDatos.EliminarProveedorAlmacen(proveedor);
                if (proveedor.Completado == true)
                {
                    TempData["typemessage"] = "1";
                    TempData["message"] = "El registro se elimino correctamente.";
                    return Json("");
                }
                else
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Ocurrió un error al intentar eliminar.";
                    return View(proveedor);
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
