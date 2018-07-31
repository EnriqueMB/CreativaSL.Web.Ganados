﻿using CreativaSL.Web.Ganados.Filters;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CreativaSL.Web.Ganados.Models;
using System.Data;
using System.Globalization;
using CreativaSL.Web.Ganados.App_Start;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    [Autorizado]
    public class CatChoferController : Controller
    {
        private TokenProcessor Token = TokenProcessor.GetInstance();
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
                Token.SaveToken();
                CatChoferModels Chofer = new CatChoferModels();
                CatChofer_Datos ChoferDatos = new CatChofer_Datos();
                Chofer.Conexion = Conexion;
                Chofer.ListaGeneroCMB = ChoferDatos.ObteneComboCatGenero(Chofer);
                Chofer.listaGrupoSanguineo = ChoferDatos.ObteneComboCatGrupoSanguineo(Chofer);
                Chofer.listaSucursales = ChoferDatos.ObteneComboCatSucursal(Chofer);
                Chofer.Licencia = Convert.ToBoolean("true");
                Chofer.ListaEmpresas = ChoferDatos.ObteneComboCatEmpresa(Chofer);
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
      
        public ActionResult Create(CatChoferModels Chofer)
        {
            CatChofer_Datos ChoferDatos = new CatChofer_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        Chofer.IDChofer = "-";
                        Chofer.Conexion = Conexion;
                        Chofer.Usuario = User.Identity.Name;
                        Chofer.Opcion = 1;
                        Chofer = ChoferDatos.AbcCatChofer(Chofer);

                        //Si abc fue completado correctamente
                        if (Chofer.Completado == true)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "El registro se guardo correctamente.";
                            Token.ResetToken();
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
                    else
                    {
                        Chofer.Licencia = true;
                        Chofer.listaGrupoSanguineo = ChoferDatos.ObteneComboCatGrupoSanguineo(Chofer);
                        Chofer.ListaGeneroCMB = ChoferDatos.ObteneComboCatGenero(Chofer);
                        Chofer.listaSucursales = ChoferDatos.ObteneComboCatSucursal(Chofer);
                        return View(Chofer);
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
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
                Token.SaveToken();
                CatChoferModels Chofer = new CatChoferModels();
                CatChofer_Datos ChoferDatos = new CatChofer_Datos();
                Chofer.IDChofer = id;
                Chofer.Conexion = Conexion;
                Chofer = ChoferDatos.ObtenerDetalleCatChofer(Chofer);
                Chofer.ListaGeneroCMB = ChoferDatos.ObteneComboCatGenero(Chofer);
                Chofer.listaSucursales = ChoferDatos.ObteneComboCatSucursal(Chofer);
                Chofer.listaGrupoSanguineo = ChoferDatos.ObteneComboCatGrupoSanguineo(Chofer);
                Chofer.ListaEmpresas = ChoferDatos.ObteneComboCatEmpresa(Chofer);
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
        public ActionResult Edit(string id, CatChoferModels Chofer)
        {
            CatChofer_Datos ChoferDatos = new CatChofer_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        Chofer.Conexion = Conexion;
                        Chofer.IDChofer = id;
                        Chofer.Usuario = User.Identity.Name;
                        Chofer.Opcion = 2;
                        Chofer = ChoferDatos.AbcCatChofer(Chofer);
                        //Si abc fue completado correctamente
                        if (Chofer.Completado == true)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "El registro se guardo correctamente.";
                            Token.ResetToken();
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
                    else
                    {
                        Chofer.Conexion = Conexion;
                        Chofer.listaGrupoSanguineo = ChoferDatos.ObteneComboCatGrupoSanguineo(Chofer);
                        Chofer.ListaGeneroCMB = ChoferDatos.ObteneComboCatGenero(Chofer);
                        Chofer.listaSucursales = ChoferDatos.ObteneComboCatSucursal(Chofer);
                        return View(Chofer);
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                Chofer.Conexion = Conexion;
                Chofer.listaGrupoSanguineo = ChoferDatos.ObteneComboCatGrupoSanguineo(Chofer);
                Chofer.ListaGeneroCMB = ChoferDatos.ObteneComboCatGenero(Chofer);
                Chofer.listaSucursales = ChoferDatos.ObteneComboCatSucursal(Chofer);
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
        public ActionResult Delete(string id)
        {
            try
            {
                CatChoferModels Chofer = new CatChoferModels();
                CatChofer_Datos ChoferDatos = new CatChofer_Datos();
                Chofer.Conexion = Conexion;
                Chofer.IDChofer = id;
                Chofer.Usuario = User.Identity.Name;
                Chofer = ChoferDatos.EliminarChofer(Chofer);
                //TempData["typemessage"] = "1";
                //TempData["message"] = "El registro se ha eliminado correctamente";
                return Json("");
            }
            catch
            {
                CatChoferModels Chofer = new CatChoferModels();
                Chofer.TablaDatos = new DataTable();
                //TempData["typemessage"] = "2";
                //TempData["message"] = "No se pudo borrar los datos. Por favor contacte a soporte técnico";
                return Json("");
               
            }
        }

        [HttpPost]
        public ActionResult ObtenerSucursalesXIDEmpresa(string IDEmpresa)
        {
            try
            {
                CatChoferModels Chofer = new CatChoferModels();
                CatChofer_Datos ChoferDatos = new CatChofer_Datos();
                Chofer.Conexion = Conexion;
                Chofer.IDEmpresa = IDEmpresa;
                Chofer.Usuario = User.Identity.Name;
                Chofer.listaSucursales = ChoferDatos.ObtenerSucursalesXIDEmpresa(Chofer);
                return Content(Chofer.listaSucursales.ToJSON(), "application/json");                
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
