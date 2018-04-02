﻿using CreativaSL.Web.Ganados.Filters;
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
    public class CatProveedorController : Controller
    {
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
        // GET: Admin/CatProveedor
        public ActionResult Index()
        {
            try
            {
                CatProveedorModels Proveedor = new CatProveedorModels();
                _CatProveedor_Datos ProveedorDatos = new _CatProveedor_Datos();
                Proveedor.Conexion = Conexion;
                Proveedor.listaProveedores = ProveedorDatos.ObtenerCatProveedores(Proveedor); 
                return View(Proveedor);
            }
            catch (Exception ex)
            {
                CatProveedorModels Proveedor = new CatProveedorModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Proveedor);
            }
        }

        // GET: Admin/CatProveedor/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/CatProveedor/Create
        public ActionResult Create()
        {
            try
            {
                CatProveedorModels Proveedor = new CatProveedorModels();
                _CatProveedor_Datos ProveedorDatos = new _CatProveedor_Datos();
                Proveedor.Conexion = Conexion;
                Proveedor.listaSucursal = ProveedorDatos.obtenerListaSucursales(Proveedor);
                Proveedor.listaTipoProveedor = ProveedorDatos.obtenerListaTipoProveedor(Proveedor);
                Proveedor.ListaGeneroCMB = ProveedorDatos.ObteneComboCatGenero(Proveedor);
                return View(Proveedor);
            }
            catch (Exception ex)
            {
                CatProveedorModels Proveedor = new CatProveedorModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Proveedor);
            }
        }

        // POST: Admin/CatProveedor/Create
        [HttpPost]
        public ActionResult Create(CatProveedorModels Proveedor)
        {
            _CatProveedor_Datos ProveedorDatos = new _CatProveedor_Datos();
            try
            {
                HttpPostedFileBase bannerImage = Request.Files[0] as HttpPostedFileBase;
                if (!string.IsNullOrEmpty(bannerImage.FileName))
                {
                    if (bannerImage != null && bannerImage.ContentLength > 0)
                    {

                        Stream s = bannerImage.InputStream;
                        Bitmap img = new Bitmap(s);
                        Proveedor.ImgINE = img.ToBase64String(ImageFormat.Png);

                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Cargar imagen INE");
                }
                HttpPostedFileBase bannerImage2 = Request.Files[1] as HttpPostedFileBase;
                if (!string.IsNullOrEmpty(bannerImage.FileName))
                {
                    if (bannerImage2 != null && bannerImage2.ContentLength > 0)
                    {

                        Stream s = bannerImage2.InputStream;
                        Bitmap img = new Bitmap(s);
                        Proveedor.ImgManifestacionFierro = img.ToBase64String(ImageFormat.Png);

                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Cargar imagen manifestación fierro");
                }


                if (ModelState.IsValid)
                {
                    Proveedor.Conexion = Conexion;
                    Proveedor.Usuario = User.Identity.Name;
                    Proveedor.Opcion = 2;
                    Proveedor = ProveedorDatos.AcCatProveedor(Proveedor);
                    if (Proveedor.Completado)
                    {
                        TempData["typemessage"] = "1";
                        TempData["message"] = "Los datos se guardaron correctamente.";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        Proveedor.listaSucursal = ProveedorDatos.obtenerListaSucursales(Proveedor);
                        Proveedor.listaTipoProveedor = ProveedorDatos.obtenerListaTipoProveedor(Proveedor);
                        Proveedor.ListaGeneroCMB = ProveedorDatos.ObteneComboCatGenero(Proveedor);
                        TempData["typemessage"] = "2";
                        TempData["message"] = "Ocurrio un error al intentar guardar los datos. Intente más tarde.";
                        return View(Proveedor);
                    }
                }
                else
                {
                    Proveedor.Conexion = Conexion;
                    Proveedor.listaSucursal = ProveedorDatos.obtenerListaSucursales(Proveedor);
                    Proveedor.listaTipoProveedor = ProveedorDatos.obtenerListaTipoProveedor(Proveedor);
                    Proveedor.ListaGeneroCMB = ProveedorDatos.ObteneComboCatGenero(Proveedor);
                    return View(Proveedor);
                }
            }
            catch (Exception ex)
            {
                Proveedor.Conexion = Conexion;
                Proveedor.listaSucursal = ProveedorDatos.obtenerListaSucursales(Proveedor);
                Proveedor.listaTipoProveedor = ProveedorDatos.obtenerListaTipoProveedor(Proveedor);
                Proveedor.ListaGeneroCMB = ProveedorDatos.ObteneComboCatGenero(Proveedor);
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error al intentar guardar los datos. Contacte a soporte técnico.";
                return View(Proveedor);
            }
            //try
            //{
            //    CatProveedorModels Proveedor = new CatProveedorModels();
            //    _CatProveedor_Datos ProveedorDatos = new _CatProveedor_Datos();
            //    Proveedor.Conexion = Conexion;
            //    Proveedor.Opcion = 1;
            //    Proveedor.Usuario = User.Identity.Name;
            //    Proveedor.NombreRazonSocial = collection["NombreRazonSocial"];
            //    Proveedor.RFC = collection["RFC"];
            //    Proveedor.IDSucursal = collection["listaSucursal"];
            //    Proveedor.IDTipoProveedor = Convert.ToInt32(collection["listaTipoProveedor"]);
            //    Proveedor.Direccion = collection["direccion"];
            //    Proveedor.telefonoCelular = collection["telefonoCelular"];
            //    Proveedor.telefonoCasa = collection["telefonoCasa"];
            //    Proveedor.correo = collection["correo"];
            //    Proveedor.sexo = Convert.ToInt32(collection["ListaGeneroCMB"]);
            //    HttpPostedFileBase bannerImage = Request.Files[0] as HttpPostedFileBase;
            //    if (bannerImage != null && bannerImage.ContentLength > 0)
            //    {
            //        Stream s = bannerImage.InputStream;
            //        Bitmap img = new Bitmap(s);
            //        Proveedor.ImgINE = img.ToBase64String(ImageFormat.Png);
            //    }
            //    HttpPostedFileBase bannerImage2 = Request.Files[1] as HttpPostedFileBase;
            //    if (bannerImage2 != null && bannerImage2.ContentLength > 0)
            //    {
            //        Stream s = bannerImage2.InputStream;
            //        Bitmap img = new Bitmap(s);
            //        Proveedor.ImgManifestacionFierro = img.ToBase64String(ImageFormat.Png);
            //    }
            //    Proveedor = ProveedorDatos.AcCatProveedor(Proveedor);
            //    if (Proveedor.Completado == true)
            //    {
            //        TempData["typemessage"] = "1";
            //        TempData["message"] = "Los datos se guardarón correctamente.";
            //        return RedirectToAction("Index");
            //    }
            //    else
            //    {
            //        TempData["typemessage"] = "2";
            //        TempData["message"] = "Ocurrio un error al intentar guardar los datos. Intente más tarde.";
            //        return RedirectToAction("Create");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    TempData["typemessage"] = "2";
            //    TempData["message"] = "Ocurrio un error al intentar guardar los datos. Contacte a soporte técnico.";
            //    return RedirectToAction("Index");
            //}
        }

        // GET: Admin/CatProveedor/Edit/5
        public ActionResult Edit(string id)
        {
            try
            {
                CatProveedorModels Proveedor = new CatProveedorModels();
                _CatProveedor_Datos ProveedorDatos = new _CatProveedor_Datos();
                Proveedor.IDProveedor = id;
                Proveedor.Conexion = Conexion;

                Proveedor.listaSucursal = ProveedorDatos.obtenerListaSucursales(Proveedor);
                var listaSucursal = new SelectList(Proveedor.listaSucursal, "IDSucursal", "NombreSucursal");
                ViewData["cmbSucursal"] = listaSucursal;

                Proveedor.listaTipoProveedor = ProveedorDatos.obtenerListaTipoProveedor(Proveedor);
                var listaTipoProveedores = new SelectList(Proveedor.listaTipoProveedor, "IDTipoProveedor", "Descripcion");
                ViewData["cmbTipoProveedor"] = listaTipoProveedores;

                Proveedor.ListaGeneroCMB = ProveedorDatos.ObteneComboCatGenero(Proveedor);
                var list = new SelectList(Proveedor.ListaGeneroCMB, "IDGenero", "Descripcion");
                ViewData["cmbGenero"] = list;

                Proveedor = ProveedorDatos.ObtenerDetalleCatProveedor(Proveedor);
                return View(Proveedor);
            }
            catch (Exception ex)
            {
                CatProveedorModels Proveedor = new CatProveedorModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Proveedor);
            }
        }

        // POST: Admin/CatProveedor/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, FormCollection collection)
        {
            try
            {
                CatProveedorModels Proveedor = new CatProveedorModels();
                _CatProveedor_Datos ProveedorDatos = new _CatProveedor_Datos();
                Proveedor.Conexion = Conexion;
                Proveedor.IDProveedor = id;
                Proveedor.Opcion = 2;
                Proveedor.Usuario = User.Identity.Name;
                Proveedor.NombreRazonSocial = collection["NombreRazonSocial"];
                Proveedor.RFC = collection["RFC"];
                Proveedor.IDSucursal = collection["listaSucursal"];
                Proveedor.IDTipoProveedor = Convert.ToInt32(collection["listaTipoProveedor"]);
                Proveedor.Direccion = collection["direccion"];
                Proveedor.telefonoCelular = collection["telefonoCelular"];
                Proveedor.telefonoCasa = collection["telefonoCasa"];
                Proveedor.correo = collection["correo"];
                Proveedor.sexo = Convert.ToInt32(collection["ListaGeneroCMB"]);
                HttpPostedFileBase bannerImage = Request.Files[0] as HttpPostedFileBase;
                if (!string.IsNullOrEmpty(bannerImage.FileName))
                {
                    if (bannerImage != null && bannerImage.ContentLength > 0)
                    {
                        Stream s = bannerImage.InputStream;
                        Bitmap img = new Bitmap(s);
                        Proveedor.ImgINE = img.ToBase64String(ImageFormat.Png);
                    }
                }
                else
                {
                    Proveedor.BandINE = true;
                }
                HttpPostedFileBase bannerImage2 = Request.Files[1] as HttpPostedFileBase;
                if (!string.IsNullOrEmpty(bannerImage2.FileName))
                {
                    if (bannerImage2 != null && bannerImage2.ContentLength > 0)
                    {
                        Stream s = bannerImage2.InputStream;
                        Bitmap img = new Bitmap(s);
                        Proveedor.ImgManifestacionFierro = img.ToBase64String(ImageFormat.Png);
                    }
                }
                else
                {
                    Proveedor.BandMF = true;
                }
                Proveedor = ProveedorDatos.AcCatProveedor(Proveedor);
                if (Proveedor.Completado == true)
                {
                    TempData["typemessage"] = "1";
                    TempData["message"] = "Los datos se guardarón correctamente.";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Ocurrio un error al intentar guardar los datos. Intente más tarde.";
                    return RedirectToAction("Create");
                }
            }
            catch (Exception ex)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error al intentar guardar los datos. Contacte a soporte técnico.";
                return RedirectToAction("Index");
            }
        }

        // GET: Admin/CatProveedor/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/CatProveedor/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                CatProveedorModels Proveedor = new CatProveedorModels();
                _CatProveedor_Datos ProveedorDatos = new _CatProveedor_Datos();
                Proveedor.Conexion = Conexion;
                Proveedor.IDProveedor = id;
                Proveedor.Usuario = User.Identity.Name;
                Proveedor = ProveedorDatos.EliminarProveedor(Proveedor);
                
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
