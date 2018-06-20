using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CreativaSL.Web.Ganados.Models;
using System.Configuration;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    public class CatEventoController : Controller
    {
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
        // GET: Admin/CatEvento
        public ActionResult Index()
        {
            return View();
        }

        // GET: Admin/CatEvento/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/CatEvento/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/CatEvento/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/CatEvento/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/CatEvento/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/CatEvento/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
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
