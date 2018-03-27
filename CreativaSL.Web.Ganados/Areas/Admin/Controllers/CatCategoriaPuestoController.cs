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
    public class CatCategoriaPuestoController : Controller
    {
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
        // GET: Admin/CategoriaPuesto
        public ActionResult Index()
        {
            try
            {
                CatCategoriaPuestoModels CategoriaPuestos = new CatCategoriaPuestoModels();
                _CatCategoriaPuesto_Datos CategoriaPuestosDatos = new _CatCategoriaPuesto_Datos();
                CategoriaPuestos.Conexion = Conexion;
                CategoriaPuestos.listaCategoriaPuesto = CategoriaPuestosDatos.obtenerCatCategoriaPuesto(CategoriaPuestos);
                return View(CategoriaPuestos);
            }
            catch (Exception ex)
            {
                CatCategoriaPuestoModels CategoriaPuestos = new CatCategoriaPuestoModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(CategoriaPuestos);
            }
        }

        // GET: Admin/CategoriaPuesto/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/CategoriaPuesto/Create
        public ActionResult Create()
        {
            try
            {
                CatCategoriaPuestoModels CategoriaPuestos = new CatCategoriaPuestoModels();
                _CatCategoriaPuesto_Datos CategoriaPuestosDatos = new _CatCategoriaPuesto_Datos();
                CategoriaPuestos.Conexion = Conexion;

                CategoriaPuestos.listaPuestos = CategoriaPuestosDatos.obtenerListaCategoriaPuesto(CategoriaPuestos);
                var List = new SelectList(CategoriaPuestos.listaPuestos, "IDPuesto", "Descripcion");
                ViewData["cmbCategoriaPuestos"] = List;

                
                return View(CategoriaPuestos);
            }
            catch (Exception ex)
            {
                CatLugarModels Lugar = new CatLugarModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Lugar);
            }
        }

        // POST: Admin/CategoriaPuesto/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {

                CatCategoriaPuestoModels CategoriaPuestos = new CatCategoriaPuestoModels();
                _CatCategoriaPuesto_Datos CategoriaPuestosDatos = new _CatCategoriaPuesto_Datos();
                CategoriaPuestos.Conexion = Conexion;
                CategoriaPuestos.Opcion = 1;
                CategoriaPuestos.id_puesto = Convert.ToInt32(collection["listaPuestos"]);
                CategoriaPuestos.descripcion = collection["descripcion"];
                CategoriaPuestos.sueldoBase = Convert.ToDecimal(collection["sueldoBase"]);

                CategoriaPuestos.Usuario = User.Identity.Name;
                CategoriaPuestos = CategoriaPuestosDatos.AcCatCategoriaPuestos(CategoriaPuestos);
                if (CategoriaPuestos.Completado == true)
                {
                    TempData["typemessage"] = "1";
                    TempData["message"] = "El registro se guardo correctamente.";
                    return RedirectToAction("Index");

                }
                else
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Ocurrió un error al guardar el registro.";
                    return View(CategoriaPuestos);
                }

            }
            catch (Exception ex)
            {
                CatCategoriaPuestoModels CategoriaPuestos = new CatCategoriaPuestoModels();

                TempData["typemessage"] = "2";
                TempData["message"] = "No se pudo guardar los datos. Por favor contacte a soporte técnico";
                return View(CategoriaPuestos);
            }
        }

        // GET: Admin/CategoriaPuesto/Edit/5
        public ActionResult Edit(string id)
        {
            try
            {
                CatCategoriaPuestoModels CategoriaPuestos = new CatCategoriaPuestoModels();
                _CatCategoriaPuesto_Datos CategoriaPuestosDatos = new _CatCategoriaPuesto_Datos();
                CategoriaPuestos.Conexion = Conexion;

                CategoriaPuestos.listaPuestos = CategoriaPuestosDatos.obtenerListaCategoriaPuesto(CategoriaPuestos);
                var List = new SelectList(CategoriaPuestos.listaPuestos, "IDPuesto", "Descripcion");
                ViewData["cmbCategoriaPuestos"] = List;
                CategoriaPuestos.id_categoria = id;
                CategoriaPuestos = CategoriaPuestosDatos.ObtenerDetalleCatCategoriaPuesto(CategoriaPuestos);

                return View(CategoriaPuestos);
            }
            catch (Exception ex)
            {
                CatLugarModels Lugar = new CatLugarModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Lugar);
            }
        }

        // POST: Admin/CategoriaPuesto/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, FormCollection collection)
        {
            try
            {

                CatCategoriaPuestoModels CategoriaPuestos = new CatCategoriaPuestoModels();
                _CatCategoriaPuesto_Datos CategoriaPuestosDatos = new _CatCategoriaPuesto_Datos();
                CategoriaPuestos.Conexion = Conexion;
                CategoriaPuestos.Opcion = 2;
                CategoriaPuestos.id_categoria = id;
                CategoriaPuestos.id_puesto = Convert.ToInt32(collection["listaPuestos"]);
                CategoriaPuestos.descripcion = collection["descripcion"];
                CategoriaPuestos.sueldoBase = Convert.ToDecimal(collection["sueldoBase"]);

                CategoriaPuestos.Usuario = User.Identity.Name;
                CategoriaPuestos = CategoriaPuestosDatos.AcCatCategoriaPuestos(CategoriaPuestos);
                if (CategoriaPuestos.Completado == true)
                {
                    TempData["typemessage"] = "1";
                    TempData["message"] = "El registro se guardo correctamente.";
                    return RedirectToAction("Index");

                }
                else
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Ocurrió un error al guardar el registro.";
                    return View(CategoriaPuestos);
                }

            }
            catch (Exception ex)
            {
                CatCategoriaPuestoModels CategoriaPuestos = new CatCategoriaPuestoModels();

                TempData["typemessage"] = "2";
                TempData["message"] = "No se pudo guardar los datos. Por favor contacte a soporte técnico";
                return View(CategoriaPuestos);
            }
        }

        // GET: Admin/CategoriaPuesto/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/CategoriaPuesto/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                CatCategoriaPuestoModels CategoriaPuestos = new CatCategoriaPuestoModels();
                _CatCategoriaPuesto_Datos CategoriaPuestosDatos = new _CatCategoriaPuesto_Datos();
                CategoriaPuestos.Conexion = Conexion;
                CategoriaPuestos.id_categoria = id;
                CategoriaPuestos.Usuario = User.Identity.Name;
                CategoriaPuestos = CategoriaPuestosDatos.EliminarCategoriaPuesto(CategoriaPuestos);
                TempData["typemessage"] = "1";
                TempData["message"] = "El registro se ha eliminado correctamente";
                return Json("");
                // TODO: Add delete logic here


            }
            catch
            {
                CatChoferModels Chofer = new CatChoferModels();

                TempData["typemessage"] = "2";
                TempData["message"] = "No se pudo borrar los datos. Por favor contacte a soporte técnico";
                return Json("");

            }
        }
    }
}