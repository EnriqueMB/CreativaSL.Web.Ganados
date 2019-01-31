using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    public class CatTipoClienteController : Controller
    {
        // GET: Admin/CatTipoCliente
        public ActionResult Index()
        {
            return View();
        }

        // GET: Admin/CatTipoCliente/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/CatTipoCliente/Create
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

        // GET: Admin/CatTipoCliente/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/CatTipoCliente/Edit/5
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

        // GET: Admin/CatTipoCliente/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/CatTipoCliente/Delete/5
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
    }
}
