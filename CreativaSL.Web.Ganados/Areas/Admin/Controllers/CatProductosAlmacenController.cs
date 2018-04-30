using CreativaSL.Web.Ganados.Filters;
using CreativaSL.Web.Ganados.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    [Autorizado]
    public class CatProductosAlmacenController : Controller
    {
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
        // GET: Admin/CatProductosAlmacen
        public ActionResult Index()
        {
            try
            {
                CatProductosAlmacenModels Producto = new CatProductosAlmacenModels();
                _CatProductosAlmacen_Datos ProductoDatos = new _CatProductosAlmacen_Datos();
                Producto.Conexion = Conexion;
                Producto.listaPrdocutosAlmacen = ProductoDatos.ObtenerCatProductosAlmacen(Producto);
                return View(Producto);
            }
            catch (Exception ex) {
                CatProductosAlmacenModels Producto = new CatProductosAlmacenModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista" + ex;
                return View(Producto);
            }
            
        }

        // GET: Admin/CatProductosAlmacen/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/CatProductosAlmacen/Create
        public ActionResult Create()
        {
            try
            {
                CatProductosAlmacenModels Producto = new CatProductosAlmacenModels();
                _CatProductosAlmacen_Datos ProductoDatos = new _CatProductosAlmacen_Datos();
                Producto.Conexion = Conexion;
                Producto.listaTipoCodigoProducto = ProductoDatos.obtenerComboCatTipoCodigo(Producto);
                Producto.listaUnidadMedida = ProductoDatos.obtenerComboCatUnidadMedida(Producto);
                return View(Producto);
            }
            catch (Exception ex) {
                CatProductosAlmacenModels Producto = new CatProductosAlmacenModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Producto);
            }
        }

        // POST: Admin/CatProductosAlmacen/Create
        [HttpPost]
        public ActionResult Create(CatProductosAlmacenModels Producto)
        {
            _CatProductosAlmacen_Datos ProductoDatos = new _CatProductosAlmacen_Datos();
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    HttpPostedFileBase bannerImage = Request.Files[0] as HttpPostedFileBase;
                    if (bannerImage != null && bannerImage.ContentLength > 0)
                    {
                        Stream s = bannerImage.InputStream;
                        Bitmap img = new Bitmap(s);
                        Producto.Imagen = img.ToBase64String(ImageFormat.Png);
                    }
                   
                    Producto.Conexion = Conexion;
                    Producto.Opcion = 1;
                    Producto.IDProductoAlmacen = "0";
                    Producto.Usuario= User.Identity.Name;
                    Producto.Almacen = true;
                    Producto = ProductoDatos.AcCatProductosAlmacen(Producto);
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
                else {
                    Producto.Conexion = Conexion;
                    Producto.listaTipoCodigoProducto = ProductoDatos.obtenerComboCatTipoCodigo(Producto);
                    Producto.listaUnidadMedida = ProductoDatos.obtenerComboCatUnidadMedida(Producto);
                    return View(Producto);
                }

                   
            }
            catch
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "No se pudo guardar los datos. Por favor contacte a soporte técnico";
                return View(Producto);
            }
        }

        // GET: Admin/CatProductosAlmacen/Edit/5
        public ActionResult Edit(string id)
        {
            try
            {
                CatProductosAlmacenModels Producto = new CatProductosAlmacenModels();
                _CatProductosAlmacen_Datos ProductoDatos = new _CatProductosAlmacen_Datos();
                Producto.Conexion = Conexion;
                Producto.IDProductoAlmacen = id;
                Producto.listaTipoCodigoProducto = ProductoDatos.obtenerComboCatTipoCodigo(Producto);
                Producto.listaUnidadMedida = ProductoDatos.obtenerComboCatUnidadMedida(Producto);
                Producto= ProductoDatos.ObtenerDetalleCatProductoAlmacen(Producto);
                return View(Producto);
            }
            catch (Exception ex)
            {
                CatProductosAlmacenModels Producto = new CatProductosAlmacenModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Producto);
            }
        }

        // POST: Admin/CatProductosAlmacen/Edit/5
        [HttpPost]
        public ActionResult Edit(CatProductosAlmacenModels Producto)
        {
            _CatProductosAlmacen_Datos ProductoDatos = new _CatProductosAlmacen_Datos();
            try
            {
                // TODO: Add insert logic here
                ModelState.Remove("Imagen2");
                if (ModelState.IsValid)
                {
                   

                    
                        HttpPostedFileBase bannerImage = Request.Files[0] as HttpPostedFileBase;
                        if (!string.IsNullOrEmpty(bannerImage.FileName))
                        {
                            if (bannerImage != null && bannerImage.ContentLength > 0)
                            {
                                Stream s = bannerImage.InputStream;
                                Bitmap img = new Bitmap(s);
                                Producto.Imagen = img.ToBase64String(ImageFormat.Png);
                            }
                        }
                        else
                        {
                            Producto.BandImg = true;
                        }


                        Producto.Conexion = Conexion;
                        Producto.Opcion = 2;
                        

                        Producto.Almacen = true;
                        Producto = ProductoDatos.AcCatProductosAlmacen(Producto);
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
                    else
                    {
                        Producto.Conexion = Conexion;
                        Producto.listaTipoCodigoProducto = ProductoDatos.obtenerComboCatTipoCodigo(Producto);
                        Producto.listaUnidadMedida = ProductoDatos.obtenerComboCatUnidadMedida(Producto);
                        return View(Producto);
                    }


                
            }
            catch
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "No se pudo guardar los datos. Por favor contacte a soporte técnico";
                return View(Producto);
            }
        }

        // GET: Admin/CatProductosAlmacen/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/CatProductosAlmacen/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                CatProductosAlmacenModels Producto = new CatProductosAlmacenModels();
                _CatProductosAlmacen_Datos ProductoDatos = new _CatProductosAlmacen_Datos();
                Producto.Conexion = Conexion;
                Producto.IDProductoAlmacen = id;
                Producto.Usuario = User.Identity.Name;
                Producto = ProductoDatos.EliminarProductoAlmancen(Producto);

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

        #region UnidadProducto
        // GET: Admin/UnidadProductoAlmacen
        [HttpGet]
        public ActionResult UnidadProducto(string id)
        {
            try
            {
                UnidadesProductosAlmacenModels uniprod = new UnidadesProductosAlmacenModels();
                _CatProductosAlmacen_Datos uniprodDatos = new _CatProductosAlmacen_Datos();
                uniprod.Conexion = Conexion;
                uniprod.id_producto = id;
                uniprod = uniprodDatos.ObtenerListaUnidadProducto(uniprod);
                return View(uniprod);
            }
            catch (Exception)
            {

                UnidadesProductosAlmacenModels uniprod = new UnidadesProductosAlmacenModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(uniprod);
            }
        }

        

        // GET: Admin/UnidadProductoAlmacen/Create
        [HttpGet]
        public ActionResult UnidadCreate(string id)
        {

            try
            {
                UnidadesProductosAlmacenModels uniprod = new UnidadesProductosAlmacenModels();
                _CatProductosAlmacen_Datos uniproDatos = new _CatProductosAlmacen_Datos();
                uniprod.Conexion = Conexion;
                uniprod.id_producto = id;
                uniprod = uniproDatos.ObtenerComboUnidadProducto(uniprod);
                return View(uniprod);
            }
            catch (Exception)
            {

                UnidadesProductosAlmacenModels uniprod = new UnidadesProductosAlmacenModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(uniprod);
            }
        }

        // POST: Admin/UnidadProductoAlmacen/Create
        [HttpPost]
        public ActionResult UnidadCreate(UnidadesProductosAlmacenModels uniprod)
        {
            _CatProductosAlmacen_Datos uniprodDatos = new _CatProductosAlmacen_Datos();
            try
            {
                // TODO: Add insert logic here

                if (ModelState.IsValid)
                {
                    uniprod.Conexion = Conexion;
                    uniprod.Opcion = 1;
                    uniprod.Usuario = User.Identity.Name;
                    uniprod = uniprodDatos.AbcUnidadesProductoAlmacen(uniprod);
                    if (uniprod.Completado == true)
                    {
                        TempData["typemessage"] = "1";
                        TempData["message"] = "Los datos se guardaron correctamente.";
                        return RedirectToAction("UnidadProducto", new { id = uniprod.id_producto});
                    }
                    else
                    {

                        TempData["typemessage"] = "2";
                        TempData["message"] = "Ocurrió un error al intentar guardar.";
                        return View(uniprod);
                    }

                }
                else
                {
                    uniprod.Conexion = Conexion;
                    return View(uniprod);
                }
            }
            catch
            {
                uniprod.Conexion = Conexion;
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrió un error el intentar guardar. Contacte a soporte técnico";
                return View(uniprod);
            }
        }

        // GET: Admin/UnidadProductoAlmacen/Edit/5
        public ActionResult UnidadEdit(string id, string id2)
        {

            try
            {
                UnidadesProductosAlmacenModels uniprod = new UnidadesProductosAlmacenModels();
                _CatProductosAlmacen_Datos uniprodDatos = new _CatProductosAlmacen_Datos();
                uniprod.Conexion = Conexion;
                uniprod.id_unidadProducto = id;
                uniprod.id_producto = id2;
                uniprod = uniprodDatos.ObtenerInfoUnidadProductoxID(uniprod);
                uniprod = uniprodDatos.ObtenerComboUnidadProducto(uniprod);
                
                return View(uniprod);
            }
            catch (Exception)
            {
                UnidadesProductosAlmacenModels uniprod = new UnidadesProductosAlmacenModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(uniprod);

            }
        }

        // POST: Admin/UnidadProductoAlmacen/Edit/5
        [HttpPost]
        public ActionResult UnidadEdit(string id, UnidadesProductosAlmacenModels uniprod)
        {
            _CatProductosAlmacen_Datos uniprodDatos = new _CatProductosAlmacen_Datos();

            try
            {
                if (ModelState.IsValid)
                {
                    uniprod.Conexion = Conexion;
                    uniprod.Opcion = 2;
                    uniprod.Usuario = User.Identity.Name;
                    uniprod = uniprodDatos.AbcUnidadesProductoAlmacen(uniprod);
                    if (uniprod.Completado == true)
                    {
                        TempData["typemessage"] = "1";
                        TempData["message"] = "Los datos se guardaron correctamente.";
                        return RedirectToAction("UnidadProducto", new { id = uniprod.id_producto });
                    }
                    else
                    {

                        TempData["typemessage"] = "2";
                        TempData["message"] = "Ocurrió un error al intentar guardar.";
                        return RedirectToAction("Edit", "UnidadProductoAlmacen");
                    }
                }
                else
                {
                    uniprod.Conexion = Conexion;
                    return View(uniprod);
                }
            }
            catch
            {
                uniprod.Conexion = Conexion;
                TempData["message"] = "Ocurrió un error el intentar guardar. Contacte a soporte técnico";
                return View(uniprod);
            }
        }

        

        // POST: Admin/UnidadProductoAlmacen/Delete/5
        [HttpPost]
        public ActionResult UnidadDelete(string id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                UnidadesProductosAlmacenModels uniprod = new UnidadesProductosAlmacenModels();
                _CatProductosAlmacen_Datos uniprodDatos = new _CatProductosAlmacen_Datos();
                uniprod.Conexion = Conexion;
                uniprod.id_unidadProducto = id;
                uniprod.Opcion = 3;
                uniprod.Usuario = User.Identity.Name;
                uniprod = uniprodDatos.EliminarUnidadAlmacen(uniprod);
                if (uniprod.Completado == true)
                {
                    TempData["typemessage"] = "1";
                    TempData["message"] = "El registro se elimino correctamente.";
                    return Json("");
                }
                else
                {

                    TempData["typemessage"] = "2";
                    TempData["message"] = "Ocurrió un error al intentar eliminar.";
                    return View(uniprod);
                }
            }
            catch
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrió un error el intentar guardar. Contacte a soporte técnico";
                return View();
            }
        }
        #endregion

    }
}
