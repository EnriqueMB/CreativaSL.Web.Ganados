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
    public class DocumentoXCobrarController : Controller
    {
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
        // GET: Admin/DocumentosXCobrar
        public ActionResult Index()
        {
            return View();
        }

        // GET: Admin/DocumentosXCobrar/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/DocumentosXCobrar/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/DocumentosXCobrar/Create
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

        // GET: Admin/DocumentosXCobrar/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/DocumentosXCobrar/Edit/5
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

        // GET: Admin/DocumentosXCobrar/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/DocumentosXCobrar/Delete/5
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
        public ActionResult ModalRegistrarComprobantePago(int opcion, string id)
        {
            {
                DocumentosPorCobrarDetallePagosModels registrarPago = new DocumentosPorCobrarDetallePagosModels();
                _DocumentoXCobrar_Datos DocCobrarDatos = new _DocumentoXCobrar_Datos();
                registrarPago.Id_documentoPorCobrar = id;
                registrarPago.Usuario = User.Identity.Name;
                registrarPago.Conexion = Conexion;
                registrarPago.ListaAsignar = DocCobrarDatos.GetListadoAsignar(registrarPago);
                registrarPago.ListaFormaPagos = DocCobrarDatos.GetListadoCFDIFormaPago(registrarPago);
                //registrarPago.DocumentoPorCobrarDetallePagosBancarizado.ListaCuentasBancarias;
                
                return PartialView("ModalRegistrarComprobantePago", registrarPago);
            }
        }






    }
}
