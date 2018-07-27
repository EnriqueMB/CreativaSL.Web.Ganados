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
    public class CatTipoServicioController : Controller
    {
        private TokenProcessor Token = TokenProcessor.GetInstance();
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
        // GET: Admin/CatTipoServicio
        public ActionResult Index()
        {
            try
            {
                CatTipoServicioModels TipoServicio = new CatTipoServicioModels();
                _CatTipoServicio_Datos TipoServicioDatos = new _CatTipoServicio_Datos();
                TipoServicio.Conexion = Conexion;
                TipoServicio = TipoServicioDatos.ObtenerListaTipoServicio(TipoServicio);
                return View(TipoServicio);
            }
            catch (Exception)
            {
                CatTipoServicioModels TipoServicio = new CatTipoServicioModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(TipoServicio);
            }
        }

        // GET: Admin/CatTipoServicio/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/CatTipoServicio/Create
        public ActionResult Create()
        {
            try
            {
                Token.SaveToken();
                CatTipoServicioModels TipoServicio = new CatTipoServicioModels();
                return View(TipoServicio);
            }
            catch (Exception)
            {
                CatTipoServicioModels TipoServicio = new CatTipoServicioModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(TipoServicio);
            }
        }

        // POST: Admin/CatTipoVehiculo/Create
        [HttpPost]
        public ActionResult Create(CatTipoServicioModels TipoServicio)
        {
            _CatTipoServicio_Datos TipoServicioDatos = new _CatTipoServicio_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        TipoServicio.Conexion = Conexion;
                        TipoServicio.Usuario = User.Identity.Name;
                        TipoServicio.Opcion = 1;

                        TipoServicio = TipoServicioDatos.AcCatTipoServicio(TipoServicio);
                        if (TipoServicio.Completado == true)
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
                            return View(TipoServicio);
                        }
                    }
                    else
                    {
                        return View(TipoServicio);
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

        // GET: Admin/CatTipoVehiculo/Edit/5
        public ActionResult Edit(string id)
        {
            try
            {
                Token.SaveToken();
                CatTipoServicioModels TipoServicio = new CatTipoServicioModels();
                _CatTipoServicio_Datos TipoServicioDatos = new _CatTipoServicio_Datos();
                TipoServicio.Conexion = Conexion;
                TipoServicio.IDTipoServicio = id;
                TipoServicio = TipoServicioDatos.ObtenerDetalleCatTipoServicio(TipoServicio);
                return View(TipoServicio);
            }
            catch (Exception)
            {
                CatTipoServicioModels TipoServicio = new CatTipoServicioModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(TipoServicio);
            }
        }

        // POST: Admin/CatTipoVehiculo/Edit/5
        [HttpPost]
        public ActionResult Edit(CatTipoServicioModels TipoServicio)
        {
            _CatTipoServicio_Datos TipoServicioDatos = new _CatTipoServicio_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        TipoServicio.Conexion = Conexion;
                        TipoServicio.Usuario = User.Identity.Name;
                        TipoServicio.Opcion = 2;

                        TipoServicio = TipoServicioDatos.AcCatTipoServicio(TipoServicio);
                        if (TipoServicio.Completado == true)
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
                            return View(TipoServicio);
                        }
                    }
                    else
                    {
                        return View(TipoServicio);
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

        // GET: Admin/CatTipoServicio/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/CatTipoServicio/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                CatTipoServicioModels TipoServicio = new CatTipoServicioModels();
                _CatTipoServicio_Datos TipoServicioDatos = new _CatTipoServicio_Datos();
                TipoServicio.Conexion = Conexion;
                TipoServicio.Usuario = User.Identity.Name;
                TipoServicio.IDTipoServicio = id;
                TipoServicio = TipoServicioDatos.EliminarTipoServicio(TipoServicio);
                if (TipoServicio.Completado == true)
                {
                    //TempData["typemessage"] = "1";
                    //TempData["message"] = "El registro se ha eliminado correctamente";
                }
                return Json("");
            }
            catch
            {
                return View();
            }
        }
    }
}
