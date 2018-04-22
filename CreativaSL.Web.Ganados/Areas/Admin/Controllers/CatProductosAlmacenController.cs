using CreativaSL.Web.Ganados.App_Start;
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
        private TokenProcessor Token = TokenProcessor.GetInstance();
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
            catch (Exception ex)
            {
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
                Token.SaveToken();
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
                if (Token.IsTokenValid())
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

                        Producto.Almacen = true;
                        Producto = ProductoDatos.AcCatProductosAlmacen(Producto);
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
                        Producto.listaTipoCodigoProducto = ProductoDatos.obtenerComboCatTipoCodigo(Producto);
                        Producto.listaUnidadMedida = ProductoDatos.obtenerComboCatUnidadMedida(Producto);
                        return View(Producto);
                    }
                }
                else
                {
                    return RedirectToAction("Index");
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
                Token.SaveToken();
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
                if (Token.IsTokenValid())
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
                        Producto.listaTipoCodigoProducto = ProductoDatos.obtenerComboCatTipoCodigo(Producto);
                        Producto.listaUnidadMedida = ProductoDatos.obtenerComboCatUnidadMedida(Producto);
                        return View(Producto);
                    }
                }
                else
                {
                    return RedirectToAction("Index");
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
                CatProductosAlmacenModels Producto = new CatProductosAlmacenModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se pudo borrar los datos. Por favor contacte a soporte técnico";
                return Json("");

            }
        }

    }
}
