using CreativaSL.Web.Ganados.App_Start;
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
        private TokenProcessor Token = TokenProcessor.GetInstance();
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
        // GET: Admin/CatProducto
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
                Token.SaveToken();
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
        public ActionResult Create(CatProductosModels Producto)
        {
            _CatProductos_Datos ProductoDatos = new _CatProductos_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        Producto.Conexion = Conexion;
                        Producto.Usuario = User.Identity.Name;
                        Producto.Opcion = 1;
                        Producto = ProductoDatos.AcCatProductos(Producto);
                        //Si abc fue completado correctamente
                        if (Producto.Completado == true)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "El registro se guardo correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrió un error al guardar el registro.";
                            return View(Producto);
                        }
                    }
                    else
                    {
                        Producto.Conexion = Conexion;
                        return View(Producto);
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }               
            }
            catch (Exception)
            {
               
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
                Token.SaveToken();
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
        public ActionResult Edit(string id, CatProductosModels Producto)
        {
            _CatProductos_Datos ProductoDatos = new _CatProductos_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        Producto.IDProducto = id;
                        Producto.Conexion = Conexion;
                        Producto.Usuario = User.Identity.Name;
                        Producto.Opcion = 2;
                        Producto = ProductoDatos.AcCatProductos(Producto);
                        //Si abc fue completado correctamente
                        if (Producto.Completado == true)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "El registro se guardo correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrió un error al guardar el registro.";
                            return View(Producto);
                        }
                    }
                    else
                    {
                        return View(Producto);

                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
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
