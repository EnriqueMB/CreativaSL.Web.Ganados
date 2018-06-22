using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CreativaSL.Web.Ganados.Models;
using System.Configuration;
using CreativaSL.Web.Ganados.App_Start;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    public class CatEventoController : Controller
    {
        private TokenProcessor Token = TokenProcessor.GetInstance();
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
        // GET: Admin/CatEvento
        public ActionResult Index()
        {
            return View();
        }

        // GET: Admin/CatEvento/Create
        public ActionResult Create()
        {
            try
            {
                Token.SaveToken();
                return View();
            }
            catch (Exception)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("Index");
            }
        }

        // POST: Admin/CatEvento/Create
        [HttpPost]
        public ActionResult Create(CatTipoEventoEnvioModels Evento)
        {
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        Evento.Conexion = Conexion;
                        Evento.Usuario = User.Identity.Name;
                        _CatEvento_Datos EventoDatos = new _CatEvento_Datos();

                        Evento.RespuestaAjax = EventoDatos.AC_Evento(Evento);

                        if (Evento.RespuestaAjax.Success == true)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "Los datos se guardaron correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrió un error al intentar guardar los datos. Intente más tarde.";
                            return View(Evento);
                        }
                    }
                    else
                    {
                        TempData["typemessage"] = "2";
                        TempData["message"] = "Revise su formulario.";
                        return View(Evento);
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrió un error al intentar guardar los datos. Contacte a soporte técnico.";
                return View(Evento);
            }
        }

        // GET: Admin/CatEvento/Edit/5
        public ActionResult Edit(int IDTipoEventoEnvio)
        {
            try
            {
                CatTipoEventoEnvioModels Evento = new CatTipoEventoEnvioModels();
                Evento.Conexion = Conexion;
                Evento.Usuario = User.Identity.Name;
                _CatEvento_Datos EventoDatos = new _CatEvento_Datos();
                Evento.IDTipoEventoEnvio = IDTipoEventoEnvio;

                Evento = EventoDatos.GET_EventoXIDEvento(Evento);
                if (Evento.RespuestaAjax.Success)
                {
                    Token.SaveToken();
                    return View(Evento);
                }
                else
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = Evento.RespuestaAjax.Mensaje;
                    return View("Index");
                }
            }
            catch (Exception ex)
            {
                CatTipoEventoEnvioModels Evento = new CatTipoEventoEnvioModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrió un error al intentar desplegar los datos. Contacte a soporte técnico.";
                return View("Index");
            }
        }

        // POST: Admin/CatEvento/Edit/5
        [HttpPost]
        public ActionResult Edit(CatTipoEventoEnvioModels Evento)
        {
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        Evento.Conexion = Conexion;
                        Evento.Usuario = User.Identity.Name;
                        _CatEvento_Datos EventoDatos = new _CatEvento_Datos();

                        Evento.RespuestaAjax = EventoDatos.AC_Evento(Evento);

                        if (Evento.RespuestaAjax.Success == true)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "Los datos se guardaron correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrió un error al intentar guardar los datos. Intente más tarde.";
                            return View(Evento);
                        }
                    }
                    else
                    {
                        TempData["typemessage"] = "2";
                        TempData["message"] = "Revise su formulario.";
                        return View(Evento);
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrió un error al intentar guardar los datos. Contacte a soporte técnico.";
                return View(Evento);
            }
        }

        // POST: Admin/CatEvento/Delete/5
        [HttpGet]
        public ActionResult Delete(int IDTipoEventoEnvio)
        {
            try
            {
                CatTipoEventoEnvioModels Evento = new CatTipoEventoEnvioModels();
                Evento.Conexion = Conexion;
                Evento.Usuario = User.Identity.Name;
                _CatEvento_Datos EventoDatos = new _CatEvento_Datos();
                Evento.IDTipoEventoEnvio = IDTipoEventoEnvio;

                Evento = EventoDatos.DEL_EventoXIDEvento(Evento);
                if (Evento.RespuestaAjax.Success)
                {
                    TempData["typemessage"] = "1";
                }
                else
                {
                    TempData["typemessage"] = "2";
                }
                TempData["message"] = Evento.RespuestaAjax.Mensaje;
                return View("Index");
            }
            catch (Exception ex)
            {
                CatTipoEventoEnvioModels Evento = new CatTipoEventoEnvioModels();
                Evento.RespuestaAjax = new RespuestaAjax();
                Evento.RespuestaAjax.Success = false;
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrió un error al intentar desplegar los datos. Contacte a soporte técnico.";

                return View("Index");
            }
        }

        [HttpPost]
        public ActionResult DatatableIndex()
        {
            try
            {
                _CatEvento_Datos EventoDatos = new _CatEvento_Datos();
                CatTipoEventoEnvioModels Evento = new CatTipoEventoEnvioModels();

                Evento.Conexion = Conexion;
                Evento.RespuestaAjax = new RespuestaAjax();

                Evento.RespuestaAjax.Mensaje = EventoDatos.DatatableIndex(Evento);
                Evento.RespuestaAjax.Success = true;

                return Content(Evento.RespuestaAjax.Mensaje, "application/json");

            }
            catch (Exception ex)
            {
                CatTipoEventoEnvioModels Evento = new CatTipoEventoEnvioModels();
                Evento.RespuestaAjax = new RespuestaAjax();
                TempData["message"] = ex.ToString();
                TempData["typemessage"] = "2";
                Evento.RespuestaAjax.Success = false;
                return Content(Evento.RespuestaAjax.ToJSON(), "application/json");
            }
        }
    }
}
