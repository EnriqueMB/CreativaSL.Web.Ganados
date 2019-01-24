using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using CreativaSL.Web.Ganados.Models;
using CreativaSL.Web.Ganados.Filters;
using CreativaSL.Web.Ganados.App_Start;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{


    public class CatCorralController : Controller
    {
        private TokenProcessor Token = TokenProcessor.GetInstance();
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");

        // GET: Admin/CatCorral
        public ActionResult Index()
        {
            try
            {
               CatCorralModels Corral = new CatCorralModels();
                _CatCorral_Datos Corrals = new _CatCorral_Datos();
                Corral.conexion= Conexion;
                Corral.ListaCorral = Corrals.ObtenerListaCorral(Corral);
                return View(Corral);
            }
            catch (Exception)
            {
                CatCorralModels Puesto = new CatCorralModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Puesto);
            }
            
        }

        // GET: Admin/CatCorral/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/CatCorral/Create
        public ActionResult Create()
        {
            try
            {
                CatCorralModels Corral = new CatCorralModels();
                Token.SaveToken();
                return View(Corral);
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        // POST: Admin/CatCorral/Create
        [HttpPost]
        public ActionResult Create(CatCorralModels Corral)
        {
            _CatCorral_Datos CorralDatos = new _CatCorral_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        Corral.conexion = Conexion;
                        Corral.Usuario = User.Identity.Name;
                        Corral.Opcion = 1;
                        //Puesto.Descripcion = collection["Descripcion"];
                        //Puesto.EsGerente = collection["EsGerente"].StartsWith("true");
                        Corral = CorralDatos.insertCorral(Corral);
                        if (Corral.Completado == true)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "Los datos se guardarón correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrio un error al intentar guardar los datos. Intente más tarde.";
                            return View(Corral);
                        }
                    }
                    else
                    {
                        return View(Corral);
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
                TempData["message"] = "Ocurrio un error al intentar guardar los datos. Contacte a soporte técnico.";
                return RedirectToAction("Index");
            }
        }

        // GET: Admin/CatCorral/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                Token.SaveToken();
                CatCorralModels Corral = new CatCorralModels();
                _CatCorral_Datos corralDatos = new _CatCorral_Datos();
                Corral.Id_corral = id;
                Corral.conexion = Conexion;
                Corral = corralDatos.ObtenerDetalleCatCorral(Corral);
                return View(Corral);
            }
            catch (Exception)
            {
                CatCorralModels Corral = new CatCorralModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Corral);
            }
        }

        // POST: Admin/CatCorral/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, CatCorralModels Corral)
        {
            //CatPuestoModels Puesto = new CatPuestoModels();
            _CatCorral_Datos CorralDatos = new _CatCorral_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        Corral.conexion = Conexion;
                        Corral.Usuario = User.Identity.Name;
                        Corral.Opcion = 2;
                        
                        Corral = CorralDatos.insertCorral(Corral);
                        if (Corral.Completado == true)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "Los datos se guardarón correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrio un error al intentar guardar los datos. Intente más tarde.";
                            return View(Corral);
                        }
                    }
                    else
                    {
                        return View(Corral);
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
                TempData["message"] = "Ocurrio un error al intentar guardar los datos. Contacte a soporte técnico.";
                return RedirectToAction("Index");
            }
        }

        // GET: Admin/CatCorral/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/CatCorral/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                CatCorralModels Corral = new CatCorralModels();
                _CatCorral_Datos CorralDatos = new _CatCorral_Datos();
                Corral.conexion = Conexion;
                Corral.Id_corral = id;
                Corral.Usuario = User.Identity.Name;
                Corral = CorralDatos.EliminarCorral(Corral);
                if (Corral.Resultado==1)
                {
                    TempData["typemessage"] = "1";
                    TempData["message"] = "El registro se ha eliminado correctamente";
                }
                else
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "No se pudo eliminar el corral, ya que tiene ganados activos en este.";
                }
                return Json("");
            }
            catch
            {
                CatCorralModels corral = new CatCorralModels();
                return View();
            }
        }
    }
}
