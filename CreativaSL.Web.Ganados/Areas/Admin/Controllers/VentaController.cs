using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    public class VentaController : Controller
    {
        // GET: Admin/Venta
        public ActionResult Index()
        {
            return View();
        }

        // GET: Admin/Venta/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Venta/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Venta/Create
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

        // GET: Admin/Venta/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Venta/Edit/5
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

        // GET: Admin/Venta/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Venta/Delete/5
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

        #region Ganado
        [HttpPost]
        public ActionResult ModalEvento()
        {
            return PartialView("ModalEvento");
        }
        #endregion
    }
}
