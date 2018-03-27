﻿using CreativaSL.Web.Ganados.Filters;
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
    public class CatTipoProveedorController : Controller
    {
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
        // GET: Admin/CatTipoProveedor
        public ActionResult Index()
        {
            try
            {
                CatTipoProveedorModels TipoProveedor = new CatTipoProveedorModels();
                _CatTipoProveedor_Datos TipoProveedorDatos = new _CatTipoProveedor_Datos();
                TipoProveedor.Conexion = Conexion;
                TipoProveedor.listaTipoProveedores = TipoProveedorDatos.ObtenerCatProveedores(TipoProveedor);
                return View(TipoProveedor);
            }
            catch (Exception ex)
            {
                CatProveedorModels Proveedor = new CatProveedorModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Proveedor);
            }
        }

        // GET: Admin/CatTipoProveedor/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/CatTipoProveedor/Create
        public ActionResult Create()
        {
            try
            {
                CatTipoProveedorModels TipoProveedor = new CatTipoProveedorModels();
                _CatTipoProveedor_Datos TipoProveedorDatos = new _CatTipoProveedor_Datos();
                TipoProveedor.Conexion = Conexion;
               

                return View(TipoProveedor);
            }
            catch (Exception ex)
            {
                CatTipoProveedorModels TipoProveedor = new CatTipoProveedorModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(TipoProveedor);
            }
        }

        // POST: Admin/CatTipoProveedor/Create
        [HttpPost]
        public ActionResult Create(CatTipoProveedorModels TipoProveedor)
        {
            try
            {
                // TODO: Add insert logic here
                _CatTipoProveedor_Datos TipoProveedorDatos = new _CatTipoProveedor_Datos();
                TipoProveedor.Conexion = Conexion;
                TipoProveedor.IDTipoProveedor = 0;
                TipoProveedor.Opcion = 1;
                TipoProveedor.Usuario = User.Identity.Name;
                TipoProveedor = TipoProveedorDatos.AcCatProveedor(TipoProveedor);
                if (TipoProveedor.Completado == true)
                {
                    TempData["typemessage"] = "1";
                    TempData["message"] = "El registro se guardo correctamente.";
                    return RedirectToAction("Index");
                }
                else
                {
                    
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Ocurrió un error al guardar el registro.";
                    return View(TipoProveedor);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/CatTipoProveedor/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                CatTipoProveedorModels TipoProveedor = new CatTipoProveedorModels();
                _CatTipoProveedor_Datos TipoProveedorDatos = new _CatTipoProveedor_Datos();
                TipoProveedor.Conexion = Conexion;
                TipoProveedor.IDTipoProveedor = id;
                TipoProveedor = TipoProveedorDatos.ObtenerDetalleCatProveedor(TipoProveedor);
                return View(TipoProveedor);
            }
            catch (Exception ex)
            {
                CatTipoProveedorModels TipoProveedor = new CatTipoProveedorModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(TipoProveedor);
            }
        }

        // POST: Admin/CatTipoProveedor/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, CatTipoProveedorModels TipoProveedor)
        {
            try
            {
                // TODO: Add insert logic here
                _CatTipoProveedor_Datos TipoProveedorDatos = new _CatTipoProveedor_Datos();
                TipoProveedor.Conexion = Conexion;
               
                TipoProveedor.IDTipoProveedor = id;
                TipoProveedor.Opcion = 2;
                TipoProveedor.Usuario = User.Identity.Name;
                TipoProveedor = TipoProveedorDatos.AcCatProveedor(TipoProveedor);
                if (TipoProveedor.Completado == true)
                {
                    TempData["typemessage"] = "1";
                    TempData["message"] = "El registro se guardo correctamente.";
                    return RedirectToAction("Index");
                }
                else
                {

                    TempData["typemessage"] = "2";
                    TempData["message"] = "Ocurrió un error al guardar el registro.";
                    return View(TipoProveedor);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/CatTipoProveedor/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/CatTipoProveedor/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                CatTipoProveedorModels Proveedor = new CatTipoProveedorModels();
                _CatTipoProveedor_Datos TipoProveedorDatos = new _CatTipoProveedor_Datos();
                Proveedor.Conexion = Conexion;
                Proveedor.IDTipoProveedor= id;
                Proveedor.Usuario = User.Identity.Name;
                Proveedor = TipoProveedorDatos.EliminarProveedor(Proveedor);

                return Json("");
                // TODO: Add delete logic here


            }
            catch
            {
                CatProductosModels Producto = new CatProductosModels();

                TempData["typemessage"] = "2";
                TempData["message"] = "No se pudo borrar los datos. Por favor contacte a soporte técnico";
                return Json("");

            }
        }
    }
}
