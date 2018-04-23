using CreativaSL.Web.Ganados.Filters;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CreativaSL.Web.Ganados.Models;
using CreativaSL.Web.Ganados.App_Start;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    [Autorizado]
    public class CatUnidadMedidaController : Controller
    {
        private TokenProcessor Token = TokenProcessor.GetInstance();
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
        // GET: Admin/CatUnidadMedida
        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                CatUnidadMedidaModels Unidad = new CatUnidadMedidaModels();
                _CatUnidadMedida_Datos UnidadDatos = new _CatUnidadMedida_Datos();
                Unidad.Conexion = Conexion;
                Unidad = UnidadDatos.ObtenerListaUnidad(Unidad);
                return View(Unidad);
            }
            catch (Exception)
            {
                CatUnidadMedidaModels Unidad = new CatUnidadMedidaModels();
                TempData["typemessage"]= "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Unidad);
            }
            
        }

        // GET: Admin/CatUnidadMedida/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/CatUnidadMedida/Create
        [HttpGet]
        public ActionResult Create()
        {
            try
            {
                Token.SaveToken();
                CatUnidadMedidaModels Unidad = new CatUnidadMedidaModels();
                return View(Unidad);
            }
            catch (Exception)
            {
                CatUnidadMedidaModels Unidad = new CatUnidadMedidaModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Unidad);
            }
            
        }

        // POST: Admin/CatUnidadMedida/Create
        [HttpPost]
        public ActionResult Create(CatUnidadMedidaModels Unidad)
        {
            _CatUnidadMedida_Datos UnidadDatos = new _CatUnidadMedida_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        Unidad.Conexion = Conexion;
                        Unidad.Opcion = 1;
                        Unidad.Usuario = User.Identity.Name;
                        Unidad = UnidadDatos.AbcCatUnidadMedida(Unidad);
                        if (Unidad.Completado == true)
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
                            return View(Unidad);
                        }
                    }
                    else
                    {
                        return View(Unidad);
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                Unidad.Conexion = Conexion;
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrió un error el intentar guardar. Contacte a soporte técnico";
                return View(Unidad);
            }
        }

        // GET: Admin/CatUnidadMedida/Edit/5
        public ActionResult Edit(int id)
        {
            
            try
            {
                Token.SaveToken();
                CatUnidadMedidaModels Unidad = new CatUnidadMedidaModels();
                _CatUnidadMedida_Datos UnidadDatos = new _CatUnidadMedida_Datos();
                Unidad.Conexion = Conexion;
                Unidad.IDUnidadMedida = id;
                Unidad = UnidadDatos.ObtenerDetalleUnidadxID(Unidad);
                return View(Unidad);
            }
            catch (Exception)
            {
                CatUnidadMedidaModels Unidad = new CatUnidadMedidaModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Unidad);
            }
        }

        // POST: Admin/CatUnidadMedida/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, CatUnidadMedidaModels Unidad)
        {
            _CatUnidadMedida_Datos UnidadDatos = new _CatUnidadMedida_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        Unidad.Conexion = Conexion;
                        Unidad.Opcion = 2;
                        Unidad.Usuario = User.Identity.Name;
                        Unidad = UnidadDatos.AbcCatUnidadMedida(Unidad);
                        if (Unidad.Completado == true)
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
                            return View(Unidad);
                        }
                    }
                    else
                    {
                        return View(Unidad);
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
                return View(Unidad);
            }
        }

        // GET: Admin/CatUnidadMedida/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/CatUnidadMedida/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                CatUnidadMedidaModels unidad = new CatUnidadMedidaModels();
                _CatUnidadMedida_Datos unidadDatos = new _CatUnidadMedida_Datos();
                unidad.Conexion = Conexion;
                unidad.IDUnidadMedida = id;
                unidad.Opcion = 3;
                unidad.Usuario = User.Identity.Name;
                unidad = unidadDatos.EliminarUnidad(unidad);
                TempData["typemessage"] = "1";
                TempData["message"] = "El registro se elimino correctamente.";
                return Json("");
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
