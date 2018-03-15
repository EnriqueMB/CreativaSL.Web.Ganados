using CreativaSL.Web.Ganados.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    public class AlmacenController : Controller
    {
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
                string IdSucursal = string.Empty;
                Almacen.ListaAlmacen = this.ObtenerGridAlmacen(IdSucursal);                
                return View(Almacen);
            }
            catch(Exception)
            {
                CatAlmacenModels Almacen = new CatAlmacenModels();
                return View(Almacen);
            }
        }
                
        // GET: Admin/Almacen/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

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
        /// <summary>
        /// Obtener el listado de almacenes de una sucursal
        /// </summary>
        /// <param name="IdSucursal"></param>
        /// <returns></returns>
        private List<CatAlmacenModels> ObtenerGridAlmacen(string IdSucursal)
        {
            try
            {
                List<CatAlmacenModels> Lista = new List<CatAlmacenModels>();
                Lista.Add(new CatAlmacenModels { ClaveAlmacen = "ALM-01-001", IDAlmacen = "0001", Sucursal = new CatSucursalesModels { NombreSucursal = "MAtriz Grupo Ocampo" }, Descripcion = "Almacén Norte" });
                Lista.Add(new CatAlmacenModels { ClaveAlmacen = "ALM-01-002", IDAlmacen = "0002", Sucursal = new CatSucursalesModels { NombreSucursal = "MAtriz Grupo Ocampo" }, Descripcion = "Almacén Sur" });
                return Lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtener el listado de sucursales para mostrarlo en una lista desplegable
        /// </summary>
        /// <returns></returns>
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
