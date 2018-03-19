using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CreativaSL.Web.Ganados.Filters;
using CreativaSL.Web.Ganados.Models;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    [Autorizado]
    public class CompraController : Controller
    {
        private string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
        private CompraModels Compra;
        private _Compra_Datos CompraDatos;

        // GET: Admin/Compra
        public ActionResult Index()
        {
            try
            {
                Compra = new CompraModels();
                CompraDatos = new _Compra_Datos();
                Compra.Conexion = Conexion;
                Compra = CompraDatos.ObtenerCompraIndex(Compra);
                return View(Compra);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult Create()
        {

            Compra = new CompraModels();
            CompraDatos = new _Compra_Datos();
            Compra.Conexion = Conexion;
            Compra = CompraDatos.ObtenerInfoTab1(Compra);
            Compra.ListaProveedores = CompraDatos.ObtenerListadoProveedores(Compra);

            return View(Compra);
        }

        // GET: Admin/Compra/Edit/5
        public ActionResult Fierros(string id)
        {
            Compra = new CompraModels();
            CompraDatos = new _Compra_Datos();
            Compra.Conexion = Conexion;
            Compra.IDCompra = id;
            Compra = CompraDatos.ObtenerInfoTab2(Compra);

            return View(Compra);
        }

        // GET: Admin/Compra/Edit/5
        public ActionResult Flete(string id)
        {
            Compra = new CompraModels();
            CompraDatos = new _Compra_Datos();
            Compra.Conexion = Conexion;
            Compra.IDFlete = id;
            //Compra = CompraDatos.ObtenerInfoTab3(Compra);

            return View(Compra);
        }

        // GET: Admin/Compra/Details/5
        public ActionResult Details(int id)
        {


            return View();
        }
        // GET: Admin/Compra/Edit/5
        public ActionResult Edit(string id)
        {
            Compra = new CompraModels();
            _Compra_Datos CompraDatos = new _Compra_Datos();
            Compra.Conexion = Conexion;
            Compra.IDProveedor = id;
            Compra = CompraDatos.ObtenerInfoTab1(Compra);
            Compra.ListaProveedores = CompraDatos.ObtenerListadoProveedores(Compra);

            return View(Compra);
        }

        // GET: Admin/Compra/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Compra/Delete/5
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
        public JsonResult Guardar(CompraModels Compra)
        {
            var rm = new ResponseModel();
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);

            Compra.GanadosPactadoTotal = Compra.GanadosPactadoMachos + Compra.GanadosPactadoHembras;
            Compra.IDUsuario = ticket.Name;

            return Json(rm);
        }
    }
}
