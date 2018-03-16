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
    public class CatProductosController : Controller
    {
        // GET: Admin/CatProducto
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
        public ActionResult Index()
        {
            try
            {
                CatProductosModels Producto = new CatProductosModels();
                _CatProductos_Datos ProductoDatos = new _CatProductos_Datos();
                Producto.Conexion = Conexion;
                Producto.listaProductos = ProductoDatos.ObtenerCatProductos(Producto);
                return View(Producto);
            }
            catch (Exception ex)
            {

                CatProductosModels Producto = new CatProductosModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista" + ex;
                return View(Producto);
            }
        }

        // GET: Admin/CatProducto/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/CatProducto/Create
        public ActionResult Create()
        {
            try
            {
                CatProductosModels Producto = new CatProductosModels();
                _CatProductos_Datos ProductoDatos = new _CatProductos_Datos();

              
                return View(Producto);
            }
            catch (Exception)
            {
                CatProductosModels Producto = new CatProductosModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Producto);
            }
        }

        // POST: Admin/CatProducto/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                CatProductosModels Producto = new CatProductosModels();
                _CatProductos_Datos ProductoDatos = new _CatProductos_Datos();
                Producto.Conexion = Conexion;
                Producto.Clave = collection["Clave"];
                Producto.Descripcion = collection["Descripcion"];
                Producto.Clave_cfdi = collection["Clave_cfdi"];
                Producto.Usuario = User.Identity.Name;
                Producto.Opcion = 1;
                Producto = ProductoDatos.AcCatProductos(Producto);
                //Si abc fue completado correctamente
                if (Producto.Completado == true)
                {
                    TempData["typemessage"] = "1";
                    TempData["message"] = "El registro se guardo correctamente.";
                      return RedirectToAction("Index");
                }
                else
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Ocurrió un error al guardar el registro.";
                    return View(Producto);
                }
               

               
            }
            catch (Exception)
            {
                CatProductosModels Producto = new CatProductosModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Producto);
            }
           
        }

        // GET: Admin/CatProducto/Edit/5
        public ActionResult Edit(string id)
        {

            try
            {
                CatProductosModels Producto = new CatProductosModels();
                _CatProductos_Datos ProductoDatos = new _CatProductos_Datos();
                Producto.Conexion = Conexion;
                Producto.IDProducto = id;
                Producto = ProductoDatos.ObtenerDetalleCatProducto(Producto);

                return View(Producto);
            }
            catch (Exception)
            {
                CatProductosModels Producto = new CatProductosModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Producto);
            }
        }

        // POST: Admin/CatProducto/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, FormCollection collection)
        {
            try
            {
                CatProductosModels Producto = new CatProductosModels();
                _CatProductos_Datos ProductoDatos = new _CatProductos_Datos();
                Producto.IDProducto = id;
                Producto.Conexion = Conexion;
                Producto.Clave = collection["Clave"];
                Producto.Descripcion = collection["Descripcion"];
                Producto.Clave_cfdi = collection["Clave_cfdi"];
                Producto.Usuario = User.Identity.Name;
                Producto.Opcion = 2;
                Producto = ProductoDatos.AcCatProductos(Producto);
                //Si abc fue completado correctamente
                if (Producto.Completado == true)
                {
                    TempData["typemessage"] = "1";
                    TempData["message"] = "El registro se guardo correctamente.";
                    return RedirectToAction("Index");

                }
                else
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Ocurrió un error al guardar el registro.";
                    return View(Producto);
                }
               


            }
            catch (Exception)
            {
                CatProductosModels Producto = new CatProductosModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Producto);
            }
        }

        // GET: Admin/CatProducto/Delete/5
        public ActionResult Delete(string id)
        {
            return View();
        }

        // POST: Admin/CatProducto/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                CatProductosModels Producto = new CatProductosModels();
                _CatProductos_Datos ProductoDatos = new _CatProductos_Datos();
                Producto.Conexion = Conexion;
                Producto.IDProducto = id;
                Producto.Usuario = User.Identity.Name;
                Producto = ProductoDatos.EliminarProducto(Producto);
                TempData["typemessage"] = "1";
                TempData["message"] = "El registro se ha eliminado correctamente";
                return Json("");
                // TODO: Add delete logic here


            }
            catch
            {
                CatProductosModels Producto = new CatProductosModels();

                TempData["typemessage"] = "2";
                TempData["message"] = "No se pudo borrar los datos. Por favor contacte a soporte técnico";
                return Json("");

            }
        }
    }
}
