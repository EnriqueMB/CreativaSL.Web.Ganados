using CreativaSL.Web.Ganados.App_Start;
using CreativaSL.Web.Ganados.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    public class CatDeduccionController : Controller
    {
        private string conexion = ConfigurationManager.AppSettings.Get("strConnection");
        private TokenProcessor Token = TokenProcessor.GetInstance();

        // GET: Admin/CatDeduccion
        public ActionResult Index()
        {
            _CatDeduccion_Datos datos = new _CatDeduccion_Datos();
            ViewBag.ListaDeducciones = datos.SpCIDDB_CatDeduccion_get_index(conexion);
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            try
            {
                Token.SaveToken();
                CatDeduccionModels catDeduccion = new CatDeduccionModels();

                return View(catDeduccion);
            }
            catch (Exception)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Create(CatDeduccionModels catDeduccion)
        {
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        _CatDeduccion_Datos datos = new _CatDeduccion_Datos();
                        string usuario = User.Identity.Name;
                        RespuestaAjax respuesta = datos.SpCIDDB_Catalogo_ac_CatDeduccion(conexion, 1, usuario, catDeduccion);
                        TempData["message"] = respuesta.Mensaje;

                        if (respuesta.Success)
                        {
                            TempData["typemessage"] = "1";
                            Token.ResetToken();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            TempData["typemessage"] = "2";
                            return View(catDeduccion);
                        }
                    }
                    else
                    {
                        return View(catDeduccion);
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error al intentar guardar los datos. Contacte a soporte técnico.";
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null || id == 0)
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Verifique sus datos.";
                    return RedirectToAction("Index");
                }

                Token.SaveToken();
                _CatDeduccion_Datos datos = new _CatDeduccion_Datos();
                CatDeduccionModels catDeduccion = datos.SpCIDDB_CatDeduccion_get_deduccionXId(conexion, id.Value);
                
                return View(catDeduccion);
            }
            catch (Exception)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Edit(CatDeduccionModels catDeduccion)
        {
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        _CatDeduccion_Datos datos = new _CatDeduccion_Datos();
                        string usuario = User.Identity.Name;
                        RespuestaAjax respuesta = datos.SpCIDDB_Catalogo_ac_CatDeduccion(conexion, 2, usuario, catDeduccion);
                        TempData["message"] = respuesta.Mensaje;

                        if (respuesta.Success)
                        {
                            TempData["typemessage"] = "1";
                            Token.ResetToken();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            TempData["typemessage"] = "2";
                            return View(catDeduccion);
                        }
                    }
                    else
                    {
                        return View(catDeduccion);
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

        [HttpPost]
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null || id == 0)
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Verifique sus datos.";
                    return RedirectToAction("Index");
                }
                _CatDeduccion_Datos datos = new _CatDeduccion_Datos();
                RespuestaAjax respuesta = new RespuestaAjax();
                string usuario = User.Identity.Name;
                respuesta = datos.SpCIDDB_CatDeduccion_del_deduccionXId(conexion, id.Value, usuario);

                return Content(respuesta.ToJSON(), "application/json");
            }
            catch
            {
                return View();
            }
        }

    }
}