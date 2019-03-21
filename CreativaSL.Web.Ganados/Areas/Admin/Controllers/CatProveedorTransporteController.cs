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
    public class CatProveedorTransporteController : Controller
    {
        private TokenProcessor Token = TokenProcessor.GetInstance();
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");

        [HttpGet]
        public ActionResult Create()
        {
            try
            {
                Token.SaveToken();
                CatProveedorTransporteModels proveedor = new CatProveedorTransporteModels();
                return View(proveedor);
            }
            catch (Exception)
            {
                CatProveedorTransporteModels proveedor = new CatProveedorTransporteModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(proveedor);
            }
        }

        [HttpPost]
        public ActionResult Create(CatProveedorTransporteModels oProveedor)
        {
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        string usuario = User.Identity.Name;
                        _CatProveedorTransporte_Datos datos = new _CatProveedorTransporte_Datos();
                        RespuestaAjax respuesta = datos.Catalogo_ac_CatProveedorTransporte(Conexion, 1, usuario, oProveedor);
                        TempData["message"] = respuesta.Mensaje;

                        if (respuesta.Success)
                        {
                            TempData["typemessage"] = "1";
   
                            Token.ResetToken();
                            return RedirectToAction("Index", "CatProveedor");
                        }
                        else
                        {
                            TempData["typemessage"] = "2";
                            return View(oProveedor);
                        }
                    }
                    else
                    {
                        TempData["typemessage"] = "2";
                        TempData["message"] = "Ocurrió un error el intentar guardar. Contacte a soporte técnico";
                        return View(oProveedor);
                    }
                }
                else
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Ocurrió un error. Contacte a soporte técnico";
                    return RedirectToAction("Index", "CatProveedor");
                }
            }
            catch
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrió un error el intentar guardar. Contacte a soporte técnico";
                return View(oProveedor);
            }
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "No se puede cargar la vista";
                    return RedirectToAction("Index", "CatProveedor");
                }

                Token.SaveToken();
                CatProveedorTransporteModels oProveedor = new CatProveedorTransporteModels();
                _CatProveedorTransporte_Datos oDatos = new _CatProveedorTransporte_Datos();

                oProveedor = oDatos.Catalogo_get_CatProveedorTransporteXId(Conexion, id);
                return View(oProveedor);
            }
            catch (Exception ex)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("Index", "CatProveedor");
            }
        }

        [HttpPost]
        public ActionResult Edit(CatProveedorTransporteModels oProveedor)
        {
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        string usuario = User.Identity.Name;
                        _CatProveedorTransporte_Datos oDatos = new _CatProveedorTransporte_Datos();

                        RespuestaAjax respuesta  = oDatos.Catalogo_ac_CatProveedorTransporte(Conexion, 2, usuario, oProveedor);
                        TempData["message"] = respuesta.Mensaje;

                        if (respuesta.Success)
                        {
                            TempData["typemessage"] = "1";
                            
                            Token.ResetToken();
                            return RedirectToAction("Index", "CatProveedor");
                        }
                        else
                        {
                            TempData["typemessage"] = "2";
                            return View(oProveedor);
                        }
                    }
                    else
                    {
                        TempData["typemessage"] = "2";
                        TempData["message"] = "Ocurrio un error al intentar actualizar los datos. Contacte a soporte técnico.";
                        return RedirectToAction("Index", "CatProveedor");
                    }
                }
                else
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Ocurrio un error. Contacte a soporte técnico.";
                    return RedirectToAction("Index", "CatProveedor");
                }
            }
            catch (Exception ex)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error. Contacte a soporte técnico.";
                return RedirectToAction("Index", "CatProveedor");
            }
        }

        [HttpPost]
        public ActionResult Delete(string id)
        {
            try
            {
                _CatProveedorTransporte_Datos oDatos = new _CatProveedorTransporte_Datos();
                string usuario = User.Identity.Name;
                RespuestaAjax respuesta = oDatos.Catalogo_del_CatProveedorTransporte(Conexion, id, usuario);

                if(respuesta.Success)
                    TempData["typemessage"] = "1";
                else
                    TempData["typemessage"] = "2";

                TempData["message"] = respuesta.Mensaje;

                //return RedirectToAction("Index", "CatProveedor");
                return Json("");
            }
            catch
            {
                RespuestaAjax respuesta = new RespuestaAjax();
                respuesta.Success = false;
                respuesta.Mensaje = "No se pudo borrar los datos. Por favor contacte a soporte técnico";
                TempData["typemessage"] = "2";
                TempData["message"] = respuesta.Mensaje;

                return Json("");
                //return RedirectToAction("Index", "CatProveedor");
            }
        }
    }
}