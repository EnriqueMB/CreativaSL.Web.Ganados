using CreativaSL.Web.Ganados.App_Start;
using CreativaSL.Web.Ganados.Filters;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CreativaSL.Web.Ganados.Models;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    [Autorizado]
    public class CatSociosController : Controller
    {
        private TokenProcessor Token = TokenProcessor.GetInstance();
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
        // GET: Admin/CatSocios
        public ActionResult Index()
        {
            try
            {
                CatSociosModels Socio = new CatSociosModels();
                _CatSocio_Datos DBSocio = new _CatSocio_Datos();
                Socio.Conexion = Conexion;
                Socio.ListaSocios = DBSocio.ObtenerListaSocios(Socio);
                return View(Socio);
            }
            catch (Exception)
            {
                CatSociosModels Socio = new CatSociosModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Socio);
            }
        }

        // GET: Admin/CatSocios/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/CatSocios/Create
        public ActionResult Create()
        {
            try
            {
                Token.SaveToken();
                CatSociosModels Socio = new CatSociosModels();
                return View(Socio);
            }
            catch (Exception)
            {
                CatSociosModels Socio = new CatSociosModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("Index");
            }
        }

        // POST: Admin/CatSocios/Create
        [HttpPost]
        public ActionResult Create(CatSociosModels DatosSocio)
        {
            _CatSocio_Datos DBSocio = new _CatSocio_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        DatosSocio.Conexion = Conexion;
                        DatosSocio.Opcion = 1;
                        DatosSocio.Usuario = User.Identity.Name;
                        DatosSocio = DBSocio.ABCatSocios(DatosSocio);
                        if (DatosSocio.Completado == true)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "Los datos se guardaron correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrio un error al intentar guardar los datos. Intente más tarde.";
                            return View(DatosSocio);
                        }
                    }
                    else
                    {
                        return View(DatosSocio);
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
                return View(DatosSocio);
            }
        }

        // GET: Admin/CatSocios/Edit/5
        public ActionResult Edit(string id)
        {
            try
            {
                Token.SaveToken();
                CatSociosModels Socio = new CatSociosModels();
                _CatSocio_Datos BDSocio = new _CatSocio_Datos();
                Socio.Conexion = Conexion;
                Socio.IDSocio = id;
                Socio = BDSocio.ObternerDetalleCatSocio(Socio);
                return View(Socio);
            }
            catch (Exception)
            {
                CatSociosModels Socio = new CatSociosModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Socio);
            }
        }

        // POST: Admin/CatSocios/Edit/5
        [HttpPost]
        public ActionResult Edit(CatSociosModels Socios)
        {
            _CatSocio_Datos DBSocio = new _CatSocio_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        Socios.Conexion = Conexion;
                        Socios.Opcion = 2;
                        Socios.Usuario = User.Identity.Name;
                        Socios = DBSocio.ABCatSocios(Socios);
                        if (Socios.Completado == true)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "Los datos se guardaron correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrio un error al intentar guardar los datos. Intente más tarde.";
                            return View(Socios);
                        }
                    }
                    else
                    {
                        return View(Socios);
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
                return View(Socios);
            }
        }

        // GET: Admin/CatSocios/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/CatSocios/Delete/5
        [HttpPost]
        public ActionResult Delete(string id)
        {
            try
            {
                CatSociosModels Socio = new CatSociosModels();
                _CatSocio_Datos DBSocio = new _CatSocio_Datos();
                Socio.IDSocio = id;
                Socio.Conexion = Conexion;
                Socio.Usuario = User.Identity.Name;
                Socio = DBSocio.EliminarSocio(Socio);
                TempData["typemessage"] = "1";
                TempData["message"] = "El registro se ha eliminado correctamente";
                return Json("");
            }
            catch
            {
                return View();
            }
        }
    }
}
