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
    public class AlmacenController : Controller
    {
       
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
        // GET: Admin/Almacen
        /// <summary>
        /// Se mostrará el catálogo de almacenes de la sucursal actual (Dada por el usuario
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            try
            {
                CatAlmacenModels Almacen = new CatAlmacenModels();
                _Almacen_Datos AlmancenDatos = new _Almacen_Datos();
                Almacen.Conexion = Conexion;
                Almacen.ListaAlmacen = AlmancenDatos.ObtenerGridAlmacen(Almacen);                
                return View(Almacen);
            }
            catch(Exception)
            {
                CatAlmacenModels Almacen = new CatAlmacenModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Almacen);
            }
        }
                
        // GET: Admin/Almacen/Details/5
        [HttpGet]
        public ActionResult Ver(string id)
        {
            try
            {
                CatAlmacenModels Bodega = new CatAlmacenModels();
                _Almacen_Datos BodegaDatos = new _Almacen_Datos();
                Bodega.Conexion = Conexion;
                Bodega.IDAlmacen = id;
                Bodega.ListaInventario = BodegaDatos.ObtenerListaInventario(Bodega);
                return View(Bodega);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
        //ELIMINAR EL CODIGO DE ABAJO O NO
        // GET: Admin/Almacen/Create
        public ActionResult Create()
        {
            try
            {
                CatAlmacenModels Almacen = new CatAlmacenModels();
                Almacen.ListaSucursales = this.ObtenerComboSucursales();
                var list = new SelectList(Almacen.ListaSucursales, "IDSucursal", "NombreSucursal");
                ViewData["cmbSucursal"] = list;
                return View(Almacen);
            }
            catch (Exception)
            {
                //Mensajes de error al cargar vista
                CatAlmacenModels Almacen = new CatAlmacenModels();
                return View(Almacen);
            }
        }

        // POST: Admin/Almacen/Create
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

        // GET: Admin/Almacen/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                CatAlmacenModels Almacen = new CatAlmacenModels();
                Almacen.Sucursal.IDSucursal = "SUC02";
                Almacen.ListaSucursales = this.ObtenerComboSucursales();
                var list = new SelectList(Almacen.ListaSucursales, "IDSucursal", "NombreSucursal");
                ViewData["cmbSucursal"] = list;
                //CatChofer_Datos ChoferDatos = new CatChofer_Datos();
                //Chofer.IDChofer = id;
                //Chofer.Conexion = Conexion;
                //Chofer = ChoferDatos.ObtenerDetalleCatChofer(Chofer);
                return View(Almacen);
            }
            catch (Exception)
            {
                CatAlmacenModels Almacen = new CatAlmacenModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Almacen);
            }
        }

        // POST: Admin/Almacen/Edit/5
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

        // GET: Admin/Almacen/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Almacen/Delete/5
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


        #region Métodos
        
        private List<CatSucursalesModels> ObtenerComboSucursales()
        {
            try
            {
                List<CatSucursalesModels> Lista = new List<CatSucursalesModels>();
                Lista.Add(new CatSucursalesModels { IDSucursal = "SUC01", NombreSucursal = "Matriz Grupo Ocampo" });
                Lista.Add(new CatSucursalesModels { IDSucursal = "SUC02", NombreSucursal = "Sucursal Frailesca" });
                return Lista;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        #endregion


    }
}
