﻿using CreativaSL.Web.Ganados.App_Start;
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
using CreativaSL.Web.Ganados.Models.Datatable;
using CreativaSL.Web.Ganados.Models.Helpers;
using CreativaSL.Web.Ganados.Models.System;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    [Autorizado]
    public class CatProveedorController : Controller
    {
        private TokenProcessor Token = TokenProcessor.GetInstance();
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");

        #region Notas

        [HttpGet]
        public ActionResult Notas(string IdProveedor)
        {
            try
            {
                if (string.IsNullOrEmpty(IdProveedor))
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Verifique sus datos.";
                    return RedirectToAction("Index");
                }
                    

                Token.SaveToken();
                var proveedor = new CatProveedorModels();
                proveedor.IDProveedor = IdProveedor;
                proveedor.Conexion = Conexion;

                var proveedorDatos = new _CatProveedor_Datos();
                proveedorDatos.ObtenerNota(proveedor);
                
                return View(proveedor);
            }
            catch (Exception ex)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Ha ocurrido un error al obtener la nota del proveedor, intentelo de nuevo o contacte con soporte técnico.";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Notas(CatProveedorModels model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.IDProveedor))
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Verifique sus datos.";
                    return RedirectToAction("Index");
                }

                if (Token.IsTokenValid())
                {
                    model.Conexion = Conexion;

                    var proveedorDatos = new _CatProveedor_Datos();
                    proveedorDatos.GuardarNota(model);

                    if (model.RespuestaAjax.Success)
                    {
                        TempData["typemessage"] = "1";
                    }
                    else
                    {
                        TempData["typemessage"] = "2";
                    }
                    
                    TempData["message"] = model.RespuestaAjax.Mensaje;
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Verifique sus datos.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Ha ocurrido un al obtener la nota del proveedor, intentelo de nuevo o contacte con soporte técnico.";
                return RedirectToAction("Index");
            }
        }
        #endregion

        #region Datatable

        public ActionResult JsonIndex(DataTableAjaxPostModel dataTableAjaxPostModel)
        {
            var Proveedor = new CatProveedorModels();
            Proveedor.Conexion = Conexion;
            var ProveedorDatos = new _CatProveedor_Datos();

            var json = ProveedorDatos.ObtenerCatProveedores(Proveedor, dataTableAjaxPostModel);

            return Content(json, "application/json");
        }

        #endregion

        // GET: Admin/CatProveedor
        public ActionResult Index()
        {
            try
            {
                CatProveedorModels Proveedor = new CatProveedorModels();
                _CatProveedorAlmacen_Datos ProveedorDAlmacen = new _CatProveedorAlmacen_Datos();
                _CatProveedorCombustible_Datos ProveedorDCombustible = new _CatProveedorCombustible_Datos();
                _CatProveedorServicio_Datos ProveedorDServicio = new _CatProveedorServicio_Datos();
                _CatProveedorTransporte_Datos ProveedorDTransporte = new _CatProveedorTransporte_Datos();

                Proveedor.Conexion = Conexion;
                ViewBag.LProveedorAlmacen = ProveedorDAlmacen.ObtenerListaProveedorAlmacen(Conexion);
                ViewBag.LProveedorCombistible = ProveedorDCombustible.ObtenerCatProveedores(Conexion);
                ViewBag.LProveddorServicio = ProveedorDServicio.ObtenerCatProveedores(Conexion);
                ViewBag.LProveddorTransporte = ProveedorDTransporte.ObtenerCatProveedoresTransportes(Conexion);

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

        // GET: Admin/CatProveedor/Create
        public ActionResult Create()
        {
            try
            {
                Token.SaveToken();
                CatProveedorModels Proveedor = new CatProveedorModels();
                _CatProveedor_Datos ProveedorDatos = new _CatProveedor_Datos();
                Proveedor.Conexion = Conexion;
                Proveedor.listaSucursal = ProveedorDatos.obtenerListaSucursales(Proveedor);
                Proveedor.listaTipoProveedor = ProveedorDatos.obtenerListaTipoProveedor(Proveedor);
                Proveedor.ListaGeneroCMB = ProveedorDatos.ObteneComboCatGenero(Proveedor);
                Proveedor.ListaPeriodo = ProveedorDatos.ObteneComboCatPeriodo(Proveedor);
                return View(Proveedor);
            }
            catch (Exception ex)
            {
                CatProveedorModels Proveedor = new CatProveedorModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("Index");
            }
        }

        // POST: Admin/CatProveedor/Create
        [HttpPost]
        public ActionResult Create(CatProveedorModels Proveedor)
        {
            _CatProveedor_Datos ProveedorDatos = new _CatProveedor_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        HttpPostedFileBase bannerImage = Request.Files["ImgINE"] as HttpPostedFileBase;
                        if (bannerImage != null && bannerImage.ContentLength > 0)
                        {
                            var baseDir = Server.MapPath("~/Imagenes/Proveedor/INE/");
                            var s = bannerImage.InputStream;

                            if (Path.GetExtension(bannerImage.FileName).ToLower() == ".heic")
                            {

                                Image img = (Image)Auxiliar.ProcessFile(s);
                                Bitmap image = new Bitmap(ComprimirImagen.VaryQualityLevel((Image)img.Clone(), 35L));
                                Proveedor.ImgINE = image.ToBase64String(ImageFormat.Jpeg);
                            }
                            else
                            {
                                Image img = new Bitmap(s);
                                Bitmap IMG3 = ComprimirImagen.SaveJpeg("", img, 50, false);
                                Proveedor.ImgINE = IMG3.ToBase64String(img.RawFormat);
                            }
                        }
                        HttpPostedFileBase bannerImage2 = Request.Files["ImgManifestacionFierro"] as HttpPostedFileBase;
                        if (bannerImage2 != null && bannerImage2.ContentLength > 0)
                        {
                            Stream s = bannerImage2.InputStream;

                            if (Path.GetExtension(bannerImage2.FileName).ToLower() == ".heic")
                            {

                                Image img = (Image)Auxiliar.ProcessFile(s);
                                Bitmap image = new Bitmap(ComprimirImagen.VaryQualityLevel((Image)img.Clone(), 35L));
                                Proveedor.ImgManifestacionFierro = image.ToBase64String(ImageFormat.Jpeg);
                            }
                            else
                            {
                                Image img = new Bitmap(s);
                                Bitmap IMG3 = ComprimirImagen.SaveJpeg("", img, 50, false);
                                Proveedor.ImgManifestacionFierro = IMG3.ToBase64String(img.RawFormat);
                            }
                        }
                        Proveedor.Conexion = Conexion;
                        Proveedor.Usuario = User.Identity.Name;
                        Proveedor.Opcion = 1;
                        Proveedor = ProveedorDatos.AcCatProveedor(Proveedor);

                        if (Proveedor.Completado)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "Los datos se guardaron correctamente.";

                            var fotoPerfilPostedFileBase = Request.Files["FotoPerfil"] as HttpPostedFileBase;
                            if (fotoPerfilPostedFileBase != null && fotoPerfilPostedFileBase.ContentLength > 0)
                            {
                                var uploadImageToserver = new UploadFileToServerModel();
                                uploadImageToserver.FileBase = fotoPerfilPostedFileBase;
                                uploadImageToserver.BaseDir = "/Imagenes/Proveedor/FotoPerfil/";
                                uploadImageToserver.FileName = "fp_" + Proveedor.IDProveedor;
                                CidFaresHelper.UploadFileToServer(uploadImageToserver);

                                if (uploadImageToserver.Success)
                                {
                                    var responseDb = ProveedorDatos.ActualizarFotoPerfil(Proveedor.IDProveedor,
                                        User.Identity.Name, uploadImageToserver.UrlComplete, Proveedor.Conexion);

                                    if (!responseDb.Success)
                                    {
                                        TempData["typemessage"] = "2";
                                        TempData["message"] = "Ha ocurrido un error al guardar la imagen de perfil al proveedor, intenlo subir de nuevo o contacte con soporte técnico.";
                                    }
                                }
                                else
                                {
                                    TempData["typemessage"] = "2";
                                    TempData["message"] = "Ha ocurrido un error al guardar en el servidor la imagen de perfil, intenlo subir de nuevo o contacte con soporte técnico.";

                                }
                            }
                           
                            Token.ResetToken();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            Proveedor.listaSucursal = ProveedorDatos.obtenerListaSucursales(Proveedor);
                            Proveedor.listaTipoProveedor = ProveedorDatos.obtenerListaTipoProveedor(Proveedor);
                            Proveedor.ListaGeneroCMB = ProveedorDatos.ObteneComboCatGenero(Proveedor);
                            Proveedor.ListaPeriodo = ProveedorDatos.ObteneComboCatPeriodo(Proveedor);
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
                        Proveedor.ListaPeriodo = ProveedorDatos.ObteneComboCatPeriodo(Proveedor);
                        return View(Proveedor);
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                Proveedor.Conexion = Conexion;
                Proveedor.listaSucursal = ProveedorDatos.obtenerListaSucursales(Proveedor);
                Proveedor.listaTipoProveedor = ProveedorDatos.obtenerListaTipoProveedor(Proveedor);
                Proveedor.ListaGeneroCMB = ProveedorDatos.ObteneComboCatGenero(Proveedor);
                Proveedor.ListaPeriodo = ProveedorDatos.ObteneComboCatPeriodo(Proveedor);
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error al intentar guardar los datos. Contacte a soporte técnico.";
                return View(Proveedor);
            }
        }

        // GET: Admin/CatProveedor/Edit/5
        public ActionResult Edit(string id)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(id))
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "No se puede cargar la vista";
                    return RedirectToAction("Index");
                }

                Token.SaveToken();
                CatProveedorModels Proveedor = new CatProveedorModels();
                _CatProveedor_Datos ProveedorDatos = new _CatProveedor_Datos();
                Proveedor.IDProveedor = id;
                Proveedor.Conexion = Conexion;
                Proveedor = ProveedorDatos.ObtenerDetalleCatProveedor(Proveedor);
                Proveedor.listaSucursal = ProveedorDatos.obtenerListaSucursales(Proveedor);
                Proveedor.listaTipoProveedor = ProveedorDatos.obtenerListaTipoProveedor(Proveedor);
                Proveedor.ListaGeneroCMB = ProveedorDatos.ObteneComboCatGenero(Proveedor);
                Proveedor.ListaPeriodo = ProveedorDatos.ObteneComboCatPeriodo(Proveedor);
                return View(Proveedor);
            }
            catch (Exception ex)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("Index");
            }
        }

        // POST: Admin/CatProveedor/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, CatProveedorModels Proveedor)
        {
            _CatProveedor_Datos ProveedorDatos = new _CatProveedor_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        Proveedor.Conexion = Conexion;
                        Proveedor.Usuario = User.Identity.Name;

                        HttpPostedFileBase bannerImage = Request.Files["ImgINE"] as HttpPostedFileBase;
                        
                        if (bannerImage != null && bannerImage.ContentLength > 0)
                        {
                            Stream s = bannerImage.InputStream;

                            if (Path.GetExtension(bannerImage.FileName).ToLower() == ".heic")
                            {

                                Image img = (Image)Auxiliar.ProcessFile(s);
                                Bitmap image = new Bitmap(ComprimirImagen.VaryQualityLevel((Image)img.Clone(), 35L));
                                Proveedor.ImgINE = image.ToBase64String(ImageFormat.Jpeg);
                            }
                            else
                            {
                                Image img = new Bitmap(s);
                                Bitmap IMG3 = ComprimirImagen.SaveJpeg("", img, 50, false);
                                Proveedor.ImgINE = IMG3.ToBase64String(img.RawFormat);
                            }
                        }
                        else
                        {
                            Proveedor.BandINE = true;
                        }
                        HttpPostedFileBase bannerImage2 = Request.Files["ImgManifestacionFierro"] as HttpPostedFileBase;
                        
                        if (bannerImage2 != null && bannerImage2.ContentLength > 0)
                        {
                            Stream s = bannerImage2.InputStream;

                            if (Path.GetExtension(bannerImage.FileName).ToLower() == ".heic")
                            {

                                Image img = (Image)Auxiliar.ProcessFile(s);
                                Bitmap image = new Bitmap(ComprimirImagen.VaryQualityLevel((Image)img.Clone(), 35L));
                                Proveedor.ImgManifestacionFierro = image.ToBase64String(ImageFormat.Jpeg);
                            }
                            else
                            {
                                Image img = new Bitmap(s);
                                Bitmap IMG3 = ComprimirImagen.SaveJpeg("", img, 50, false);
                                Proveedor.ImgManifestacionFierro = IMG3.ToBase64String(img.RawFormat);
                            }
                        }
                        else
                        {
                            Proveedor.BandMF = true;
                        }

                        var fotoPerfilPostedFileBase = Request.Files["FotoPerfil"] as HttpPostedFileBase;
                        //foto de perfil
                        var uploadImageToserver = new UploadFileToServerModel();
                        uploadImageToserver.FileBase = fotoPerfilPostedFileBase;
                        uploadImageToserver.BaseDir = "/Imagenes/Proveedor/FotoPerfil/";
                        uploadImageToserver.FileName = "fp_" + Proveedor.IDProveedor;

                        if (fotoPerfilPostedFileBase != null && fotoPerfilPostedFileBase.ContentLength > 0)
                        {
                            //borramos la anterior
                            CidFaresHelper.DeleteFilesWithOutExtensionFromServer(uploadImageToserver);
                            //hay foto nueva
                            CidFaresHelper.UploadFileToServer(uploadImageToserver);

                            if (!uploadImageToserver.Success)
                            {
                                Proveedor.listaSucursal = ProveedorDatos.obtenerListaSucursales(Proveedor);
                                Proveedor.listaTipoProveedor = ProveedorDatos.obtenerListaTipoProveedor(Proveedor);
                                Proveedor.ListaGeneroCMB = ProveedorDatos.ObteneComboCatGenero(Proveedor);
                                Proveedor.ListaPeriodo = ProveedorDatos.ObteneComboCatPeriodo(Proveedor);
                                TempData["typemessage"] = "2";
                                TempData["message"] = "Ocurrio un error al intentar guardar la imagen de perfil, intentelo más tarde.";
                                return View(Proveedor);
                            }
                            
                            var responseDb = ProveedorDatos.ActualizarFotoPerfil(Proveedor.IDProveedor,
                                User.Identity.Name, uploadImageToserver.UrlComplete, Proveedor.Conexion);
                        }
                        else
                        {
                            if (Proveedor.DeleteFotoPerfilFromServer)
                            {
                                CidFaresHelper.DeleteFilesWithOutExtensionFromServer(uploadImageToserver);
                                //eliminar la imagen de perfil, lo borramos de la bd
                                var responseDb = ProveedorDatos.ActualizarFotoPerfil(Proveedor.IDProveedor,
                                    User.Identity.Name, "", Proveedor.Conexion);
                            }
                        }
                        

                        
                        Proveedor.Opcion = 2;
                        Proveedor = ProveedorDatos.AcCatProveedor(Proveedor);
                        if (Proveedor.Completado)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "Los datos se guardaron correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            Proveedor.listaSucursal = ProveedorDatos.obtenerListaSucursales(Proveedor);
                            Proveedor.listaTipoProveedor = ProveedorDatos.obtenerListaTipoProveedor(Proveedor);
                            Proveedor.ListaGeneroCMB = ProveedorDatos.ObteneComboCatGenero(Proveedor);
                            Proveedor.ListaPeriodo = ProveedorDatos.ObteneComboCatPeriodo(Proveedor);
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
                        Proveedor.ListaPeriodo = ProveedorDatos.ObteneComboCatPeriodo(Proveedor);
                        return View(Proveedor);
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                Proveedor.Conexion = Conexion;
                Proveedor.listaSucursal = ProveedorDatos.obtenerListaSucursales(Proveedor);
                Proveedor.listaTipoProveedor = ProveedorDatos.obtenerListaTipoProveedor(Proveedor);
                Proveedor.ListaGeneroCMB = ProveedorDatos.ObteneComboCatGenero(Proveedor);
                Proveedor.ListaPeriodo = ProveedorDatos.ObteneComboCatPeriodo(Proveedor);
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error al intentar guardar los datos. Contacte a soporte técnico.";
                return View(Proveedor);
            }
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

                var uploadImageToserver = new UploadFileToServerModel();
                uploadImageToserver.BaseDir = "/Imagenes/Proveedor/FotoPerfil/";
                uploadImageToserver.FileName = "fp_" + Proveedor.IDProveedor;
                CidFaresHelper.DeleteFilesWithOutExtensionFromServer(uploadImageToserver);

                if (Proveedor.Completado)
                {
                    TempData["typemessage"] = "1";
                    TempData["message"] = "Registro eliminado correctamente.";
                }
                else
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "No se pudo eliminar el registro contacte con soporte técnico.";
                }

                return Json("");
            }
            catch
            {
                CatProductosModels Producto = new CatProductosModels();

                TempData["typemessage"] = "2";
                TempData["message"] = "No se pudo borrar los datos. Por favor contacte a soporte técnico";
                return Json("");

            }
        }

        // GET: Admin/CatProveedor/Cuentas/5
        [HttpGet]
        public ActionResult Cuentas(string id)
        {
            try
            {
                CuentaBancariaProveedorModels Cuenta = new CuentaBancariaProveedorModels();
                _CatProveedor_Datos ProveedorDatos = new _CatProveedor_Datos();
                Cuenta.Conexion = Conexion;
                Cuenta.IDProveedor = id;
                Cuenta.ListaCuentaBancaria = ProveedorDatos.ObtenerCuentasBancarias(Cuenta);
                return View(Cuenta);
            }
            catch (Exception)
            {
                CuentaBancariaProveedorModels Cuenta = new CuentaBancariaProveedorModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("Index");
            }
        }

        //GET: Admin/CatProveedor/CreateCuenta/3
        [HttpGet]
        public ActionResult CreateCuenta(string id)
        {
            try
            {
                Token.SaveToken();
                CuentaBancariaProveedorModels Cuenta = new CuentaBancariaProveedorModels();
                _CatProveedor_Datos ProveedorDatos = new _CatProveedor_Datos();
                Cuenta.IDProveedor = id;
                Cuenta.Conexion = Conexion;
                Cuenta.ListaCmbBancos = ProveedorDatos.ObteneComboCatBancos(Cuenta);
                return View(Cuenta);
            }
            catch (Exception)
            {
                CuentaBancariaProveedorModels Cuenta = new CuentaBancariaProveedorModels();
                Cuenta.IDProveedor = id;
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("Cuentas", new { id = Cuenta.IDProveedor });
            }
        }

        //POST:Admin/CatProveedor/createCuenta/3
        [HttpPost]
        public ActionResult CreateCuenta(string id, CuentaBancariaProveedorModels IDCuentaBancoP)
        {
            _CatProveedor_Datos ProveedorDatos = new _CatProveedor_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        IDCuentaBancoP.Conexion = Conexion;
                        IDCuentaBancoP.Usuario = User.Identity.Name;
                        IDCuentaBancoP.Opcion = 1;
                        ProveedorDatos.ACDatosBancariosProveedor(IDCuentaBancoP);
                        if (IDCuentaBancoP.Completado == true)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "Los datos se guardaron correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("Cuentas", new { id = IDCuentaBancoP.IDProveedor });
                        }
                        else
                        {
                            IDCuentaBancoP.ListaCmbBancos = ProveedorDatos.ObteneComboCatBancos(IDCuentaBancoP);
                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrió un error al intentar guardar los datos. Intente más tarde.";
                            return View(IDCuentaBancoP);
                        }
                    }
                    else
                    {
                        IDCuentaBancoP.Conexion = Conexion;
                        IDCuentaBancoP.ListaCmbBancos = ProveedorDatos.ObteneComboCatBancos(IDCuentaBancoP);
                        return View(IDCuentaBancoP);
                    }

                }
                else
                {
                    return RedirectToAction("Cuentas", new { id = IDCuentaBancoP.IDProveedor });
                }
            }
            catch (Exception)
            {
                IDCuentaBancoP.Conexion = Conexion;
                IDCuentaBancoP.ListaCmbBancos = ProveedorDatos.ObteneComboCatBancos(IDCuentaBancoP);
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error al intentar guardar los datos. Contacte a soporte técnico.";
                return View(IDCuentaBancoP);
            }
        }

        //GET: Admin/CatProveedor/EditarCuenta/3
        [HttpGet]
        public ActionResult EditCuenta(string id, string id2)
        {
            try
            {
                Token.SaveToken();
                CuentaBancariaProveedorModels Cuenta = new CuentaBancariaProveedorModels();
                _CatProveedor_Datos ProveedorDatos = new _CatProveedor_Datos();
                Cuenta.IDDatosBancarios = id;
                Cuenta.IDProveedor = id2;
                Cuenta.Conexion = Conexion;
                Cuenta = ProveedorDatos.ObtenerDetalleCuentaBancaria(Cuenta);
                Cuenta.ListaCmbBancos = ProveedorDatos.ObteneComboCatBancos(Cuenta);
                return View(Cuenta);
            }
            catch (Exception)
            {
                CuentaBancariaProveedorModels Cuenta = new CuentaBancariaProveedorModels();
                Cuenta.IDProveedor = id2;
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("Cuentas", new { id = Cuenta.IDProveedor });
            }
        }

        //POST: Admin/CatProveedor/EditarCuenta/3
        [HttpPost]
        public ActionResult EditCuenta(string id, string id2, CuentaBancariaProveedorModels IDCuentaBancoP)
        {
            _CatProveedor_Datos ProveedorDatos = new _CatProveedor_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        IDCuentaBancoP.Conexion = Conexion;
                        IDCuentaBancoP.Usuario = User.Identity.Name;
                        IDCuentaBancoP.Opcion = 2;
                        ProveedorDatos.ACDatosBancariosProveedor(IDCuentaBancoP);
                        if (IDCuentaBancoP.Completado == true)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "Los datos se guardaron correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("Cuentas", new { id = IDCuentaBancoP.IDProveedor });
                        }
                        else
                        {
                            IDCuentaBancoP.ListaCmbBancos = ProveedorDatos.ObteneComboCatBancos(IDCuentaBancoP);
                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrió un error al intentar guardar los datos. Intente más tarde.";
                            return View(IDCuentaBancoP);
                        }
                    }
                    else
                    {
                        IDCuentaBancoP.Conexion = Conexion;
                        IDCuentaBancoP.ListaCmbBancos = ProveedorDatos.ObteneComboCatBancos(IDCuentaBancoP);
                        return View(IDCuentaBancoP);
                    }
                }
                else
                {
                    return RedirectToAction("Cuentas", new { id = IDCuentaBancoP.IDProveedor });
                }
            }
            catch (Exception)
            {
                IDCuentaBancoP.Conexion = Conexion;
                IDCuentaBancoP.ListaCmbBancos = ProveedorDatos.ObteneComboCatBancos(IDCuentaBancoP);
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error al intentar guardar los datos. Contacte a soporte técnico.";
                return View(IDCuentaBancoP);
            }
        }

        // POST: Admin/CatProvedor/Delete/5
        [HttpPost]
        public ActionResult DeleteCuenta(string id, string id2)
        {
            try
            {
                CuentaBancariaProveedorModels Datos = new CuentaBancariaProveedorModels
                {
                    IDProveedor = id2,
                    IDDatosBancarios = id,
                    Conexion = Conexion,
                    Usuario = User.Identity.Name
                };
                _CatProveedor_Datos ProveedorDatos = new _CatProveedor_Datos();
                ProveedorDatos.EliminarDatosBancariosProveedor(Datos);
                if (Datos.Completado)
                {
                    TempData["typemessage"] = "1";
                    TempData["message"] = "El registro se ha eliminado correctamente";
                    return Json("");
                }
                else
                { return Json(""); }
            }
            catch
            {
                return View();
            }
        }

        //GET: Admin/CatProveedor/LugarProveedor/4
        [HttpGet]
        public ActionResult LugarProveedor(string id, string id2)
        {
            try
            {
                ProveedorLugarModels ProveedorL = new ProveedorLugarModels();
                _CatProveedor_Datos ProveedorDatos = new _CatProveedor_Datos();
                ProveedorL.Conexion = Conexion;
                ProveedorL.IDProveedor = id;
                ProveedorL.IDSucursal = id2;
                ProveedorL.ListaProveedorLugar = ProveedorDatos.ObtenerLugaresProveedor(ProveedorL);
                return View(ProveedorL);
            }
            catch (Exception)
            {
                ProveedorLugarModels Proveedor = new ProveedorLugarModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Proveedor);
            }
        }

        //GET:Admin/CatProveedor/CreateLugar/3
        [HttpGet]
        public ActionResult CreateLugar(string id, string id2)
        {
            try
            {
                Token.SaveToken();
                ProveedorLugarModels ProveedorLugar = new ProveedorLugarModels();
                _CatProveedor_Datos ProveedorDatos = new _CatProveedor_Datos();
                ProveedorLugar.Conexion = Conexion;
                ProveedorLugar.IDProveedor = id;
                ProveedorLugar.IDSucursal = id2;
                ProveedorLugar.ListaLugares = ProveedorDatos.obtenerComboLugares(ProveedorLugar);
                return View(ProveedorLugar);
            }
            catch (Exception)
            {
                ProveedorLugarModels Proveedor = new ProveedorLugarModels();
                Proveedor.IDProveedor = id;
                Proveedor.IDSucursal = id2;
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("LugarProveedor", "CatProveedor", new { id = Proveedor.IDProveedor, id2 = Proveedor.IDSucursal });
            }
        }

        //POST: Admin/CatProvedor/CreateLugar/3
        [HttpPost]
        public ActionResult CreateLugar(ProveedorLugarModels Proveedor)
        {
            _CatProveedor_Datos ProveedorDatos = new _CatProveedor_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        Proveedor.Conexion = Conexion;
                        Proveedor.Usuario = User.Identity.Name;
                        ProveedorDatos.ACProveedorLugares(Proveedor);
                        if (Proveedor.Completado)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "Los datos se guardaron correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("LugarProveedor", "CatProveedor", new { id = Proveedor.IDProveedor, id2 = Proveedor.IDSucursal });
                        }
                        else
                        {
                            Proveedor.ListaLugares = ProveedorDatos.obtenerComboLugares(Proveedor);
                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrió un error al intentar guardar los datos. Intente más tarde.";
                            return RedirectToAction("CreateLugar", "CatProveedor", new { id = Proveedor.IDProveedor, id2 = Proveedor.IDSucursal });
                        }
                    }
                    else
                    {
                        Proveedor.Conexion = Conexion;
                        Proveedor.ListaLugares = ProveedorDatos.obtenerComboLugares(Proveedor);
                        return RedirectToAction("CreateLugar", "CatProveedor", new { id = Proveedor.IDProveedor, id2 = Proveedor.IDSucursal });
                    }
                }
                else
                {
                    return RedirectToAction("LugarProveedor", "CatProveedor", new { id = Proveedor.IDProveedor, id2 = Proveedor.IDSucursal });
                }
            }
            catch (Exception)
            {
                Proveedor.Conexion = Conexion;
                Proveedor.ListaLugares = ProveedorDatos.obtenerComboLugares(Proveedor);
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error al intentar guardar los datos. Contacte a soporte técnico.";
                return RedirectToAction("CreateLugar", "CatProveedor", new { id = Proveedor.IDProveedor, id2 = Proveedor.IDSucursal });
            }
        }

        // POST: Admin/CatProvedor/Delete/5
        [HttpPost]
        public ActionResult DeleteLugar(string id)
        {
            try
            {
                ProveedorLugarModels Datos = new ProveedorLugarModels
                {
                    IDProveedorLugar = id,
                    Conexion = Conexion,
                    Usuario = User.Identity.Name
                };
                _CatProveedor_Datos ProveedorDatos = new _CatProveedor_Datos();
                ProveedorDatos.EliminarProveedorLugar(Datos);
                if (Datos.Completado)
                {
                    TempData["typemessage"] = "1";
                    TempData["message"] = "El registro se ha eliminado correctamente";
                    return Json("");
                }
                else
                { return Json(""); }
            }
            catch
            {
                return View();
            }
        }

        //GET: Admin/CatProveedor/PrecioProveedor/3
        [HttpGet]
        public ActionResult PrecioProveedor(string id)
        {
            try
            {
                RangoPrecioProveedorModels RangoProveedor = new RangoPrecioProveedorModels();
                _CatProveedor_Datos ProveedorDato = new _CatProveedor_Datos();
                RangoProveedor.Conexion = Conexion;
                RangoProveedor.IDProveedor = id;
                RangoProveedor.ListaRangoPrecioProveedor = ProveedorDato.ObtenerPrecioXRangoPesoProveedor(RangoProveedor);
                return View(RangoProveedor);
            }
            catch (Exception)
            {
                RangoPrecioProveedorModels Proveedor = new RangoPrecioProveedorModels();
                TempData["typrmessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Proveedor);
            }
        }

        //GET: Admin/CatProveedor/EditPrecio/3
        [HttpGet]
        public ActionResult EditPrecio(int id, string id2)
        {
            try
            {
                Token.SaveToken();
                RangoPrecioProveedorModels RangoProveedor = new RangoPrecioProveedorModels();
                _CatProveedor_Datos ProvedorDatos = new _CatProveedor_Datos();
                RangoProveedor.Conexion = Conexion;
                RangoProveedor.IDProveedor = id2;
                RangoProveedor.IDRango = id;
                RangoProveedor = ProvedorDatos.ObtenerDetallePrecioXProveedor(RangoProveedor);
                return View(RangoProveedor);
            }
            catch (Exception)
            {
                RangoPrecioProveedorModels Proveedor = new RangoPrecioProveedorModels();
                Proveedor.IDProveedor = id2;
                TempData["typrmessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("PrecioProveedor", "CatProveedor", new { id = Proveedor.IDProveedor});
            }
        }

        //POST: Admin/CatProveedor/EditPrecio/5
        [HttpPost]
        public ActionResult EditPrecio(RangoPrecioProveedorModels RangoPrecio)
        {
            _CatProveedor_Datos ProveedorDatos = new _CatProveedor_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        RangoPrecio.Conexion = Conexion;
                        RangoPrecio.Usuario = User.Identity.Name;
                        ProveedorDatos.ACPrecioPoRangoPesoProveedor(RangoPrecio);
                        if (RangoPrecio.Completado)
                        {
                            if (RangoPrecio.Resultado == 1)
                            {
                                TempData["typemessage"] = "1";
                                TempData["message"] = "Los datos se guardaron correctamente.";
                                Token.ResetToken();
                                return RedirectToAction("PrecioProveedor", "CatProveedor", new { id = RangoPrecio.IDProveedor });
                            }
                            else if (RangoPrecio.Resultado == 2)
                            {
                                TempData["typemessage"] = "1";
                                TempData["message"] = "Los datos actualizado correctamente.";
                                Token.ResetToken();
                                return RedirectToAction("PrecioProveedor", "CatProveedor", new { id = RangoPrecio.IDProveedor });
                            }
                            else
                            {
                                TempData["typemessage"] = "2";
                                TempData["message"] = "Ocurrió un error al intentar guardar los datos. Intente más tarde.";
                                return View(RangoPrecio);
                            }
                        }
                        else
                        {
                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrió un error al intentar guardar los datos. Intente más tarde.";
                            return View(RangoPrecio);
                        }
                    }
                    else
                    {
                        return View(RangoPrecio);//return RedirectToAction("EditPrecio", "CatProveedor", new { id = RangoPrecio.IDRango, id2 = RangoPrecio.IDProveedor });
                    }
                }
                else
                {
                    return RedirectToAction("PrecioProveedor", "CatProveedor", new { id = RangoPrecio.IDProveedor });
                }
            }
            catch (Exception)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error al intentar guardar los datos. Contacte a soporte técnico.";
                return View(RangoPrecio);
            }
        }
        [HttpGet]
        public ActionResult DatosContacto(string id,string id2) {

            try
            {
                Token.SaveToken();
                CatProveedorModels Proveedor = new CatProveedorModels();
                _CatProveedor_Datos ProveedorDatos = new _CatProveedor_Datos();
                Proveedor.IDProveedor = id;
                Proveedor.IDSucursal = id2;
                Proveedor.Conexion = Conexion;
                Proveedor.listaDatosContactos = ProveedorDatos.obtenerDatosContacto(Proveedor);

                return View(Proveedor);
            }
            catch(Exception ex)
            {
                CatProveedorModels Proveedor = new CatProveedorModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error al intentar cargar los datos. Contacte a soporte técnico.";
                return View(Proveedor);

            }
        }
        [HttpGet]
        public ActionResult NuevoDatosContactoEdit(string id, string id2,string id3)
        {
            try
            {
                Token.SaveToken();
                CatContactosModels Contactos = new CatContactosModels();
                _CatProveedor_Datos ProveedorDatos = new _CatProveedor_Datos();
                Contactos.IDProveedor = id;
                Contactos.IDSucursal = id2;
                Contactos.IDContacto = id3;
                Contactos.Conexion = Conexion;
                Contactos = ProveedorDatos.ObtenerDetalleCatDatosXProveedor(Contactos);
                return View(Contactos);
            }
            catch (Exception ex)
            {
                CatProveedorModels Proveedor = new CatProveedorModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error al intentar cargar los datos. Contacte a soporte técnico.";
                return View(Proveedor);

            }
        }
        [HttpPost]
        public ActionResult NuevoDatosContactoEdit(CatContactosModels Contactos)
        {
            try
            {

                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        _CatProveedor_Datos ProveedorDatos = new _CatProveedor_Datos();

                        Contactos.Conexion = Conexion;
                        Contactos.Usuario = User.Identity.Name;
                        Contactos.Opcion = 2;
                        Contactos = ProveedorDatos.AcContactoProveedor(Contactos);
                        if (Contactos.Completado)
                        {

                            TempData["typemessage"] = "1";
                            TempData["message"] = "Los datos se guardaron correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("DatosContacto", "CatProveedor", new { id = Contactos.IDProveedor, id2 = Contactos.IDSucursal });
                        }
                        else
                        {

                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrió un error al intentar guardar los datos. Intente más tarde.";
                            return RedirectToAction("NuevoDatosContacto", "CatProveedor", new { id = Contactos.IDProveedor, id2 = Contactos.IDSucursal });
                        }
                    }
                    else
                    {
                        TempData["typemessage"] = "2";
                        TempData["message"] = "Ocurrió un error al intentar guardar los datos. Intente más tarde.";
                        return View(Contactos);
                    }
                }
                else
                {
                    return RedirectToAction("NuevoDatosContacto", "CatProveedor", new { id = Contactos.IDProveedor, id2 = Contactos.IDSucursal });
                }



            }
            catch (Exception ex)
            {
                CatProveedorModels Proveedor = new CatProveedorModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error al intentar cargar los datos. Contacte a soporte técnico.";
                return View(Proveedor);

            }
        }

        [HttpGet]
        public ActionResult NuevoDatosContacto(string id, string id2) {
            try
            {
                Token.SaveToken();
                CatContactosModels Contactos = new CatContactosModels();
                _CatProveedor_Datos ProveedorDatos = new _CatProveedor_Datos();
                Contactos.IDProveedor = id;
                Contactos.IDSucursal = id2;
                Contactos.Conexion = Conexion;
               
                return View(Contactos);
            }
            catch (Exception ex)
            {
                CatProveedorModels Proveedor = new CatProveedorModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error al intentar cargar los datos. Contacte a soporte técnico.";
                return View(Proveedor);

            }
        }
        [HttpPost]
        public ActionResult NuevoDatosContacto(CatContactosModels Contactos)
        {
            try
            {

                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        _CatProveedor_Datos ProveedorDatos = new _CatProveedor_Datos();

                        Contactos.Conexion = Conexion;
                        Contactos.Usuario = User.Identity.Name;
                        Contactos.Opcion = 1;
                        Contactos = ProveedorDatos.AcContactoProveedor(Contactos);
                        if (Contactos.Completado)
                        {

                            TempData["typemessage"] = "1";
                            TempData["message"] = "Los datos se guardaron correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("DatosContacto", "CatProveedor", new { id = Contactos.IDProveedor, id2 = Contactos.IDSucursal });
                        }
                        else
                        {

                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrió un error al intentar guardar los datos. Intente más tarde.";
                            return RedirectToAction("NuevoDatosContacto", "CatProveedor", new { id = Contactos.IDProveedor, id2 = Contactos.IDSucursal });
                        }
                    }
                    else
                    {
                        TempData["typemessage"] = "2";
                        TempData["message"] = "Ocurrió un error al intentar guardar los datos. Intente más tarde.";
                        return View(Contactos);
                    }
                }
                else {
                    return RedirectToAction("NuevoDatosContacto", "CatProveedor", new { id = Contactos.IDProveedor, id2 = Contactos.IDSucursal });
                }



            }
            catch (Exception ex)
            {
                CatProveedorModels Proveedor = new CatProveedorModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error al intentar cargar los datos. Contacte a soporte técnico.";
                return View(Proveedor);

            }
        }
        // POST: Admin/CatProvedor/Delete/5
        [HttpPost]
        public ActionResult DeleteDatosContacto(string id, string id2, string id3)
        {
            try
            {
                CatContactosModels Datos = new CatContactosModels
                {
                    IDProveedor = id,
                    IDContacto = id3,
                    Conexion = Conexion,
                    Usuario = User.Identity.Name
                };
                _CatProveedor_Datos ProveedorDatos = new _CatProveedor_Datos();
                ProveedorDatos.EliminarProveedorDatosContacto(Datos);
                if (Datos.Completado)
                {
                    TempData["typemessage"] = "1";
                    TempData["message"] = "El registro se ha eliminado correctamente";
                    return Json("");
                }
                else
                { return Json(""); }
            }
            catch
            {
                return View();
            }
        }
        [HttpGet]
        public ActionResult UPPProveedor(string id)
        {
            try
            {
                Token.SaveToken();
                UPPProvedorModels uPPProvedor = new UPPProvedorModels();
                _CatProveedor_Datos ProveedorDatos = new _CatProveedor_Datos();
                uPPProvedor.Conexion = Conexion;
                uPPProvedor.id_proveedor = id;
                uPPProvedor = ProveedorDatos.ObtenerUPPProveedor(uPPProvedor);
                uPPProvedor.listaPaises = ProveedorDatos.obtenerListaPaises(uPPProvedor);
                uPPProvedor.listaEstado = ProveedorDatos.obtenerListaEstados(uPPProvedor);
                uPPProvedor.listaMunicipio = ProveedorDatos.obtenerListaMunicipios(uPPProvedor);

                if (string.IsNullOrEmpty(uPPProvedor.Imagen))
                {
                    uPPProvedor.ImagenMostrar = Auxiliar.SetDefaultImage();
                }
                else
                {
                    uPPProvedor.ImagenMostrar = uPPProvedor.Imagen;
                }

                return View(uPPProvedor);
            }
            catch (Exception)
            {
                UPPProvedorModels uPPProvedor = new UPPProvedorModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("Index");
            }
        }
        public ActionResult UPPProveedor(string id, UPPProvedorModels uPPProvedor)
        {
            _CatProveedor_Datos ProveedorDatos = new _CatProveedor_Datos();
            try
            {
                if(Token.IsTokenValid())
                {
                    if (uPPProvedor.ImagenHttp != null)
                    {
                        //uPPProvedor.Imagen = Auxiliar.ImageToBase64(uPPProvedor.ImagenHttp);

                        Stream s = uPPProvedor.ImagenHttp.InputStream;

                        if (Path.GetExtension(uPPProvedor.ImagenHttp.FileName).ToLower() == ".heic")
                        {
                            Image img = (Image)Auxiliar.ProcessFile(s);
                            Bitmap image = new Bitmap(ComprimirImagen.VaryQualityLevel((Image)img.Clone(), 35L));
                            uPPProvedor.Imagen = image.ToBase64String(ImageFormat.Jpeg);
                        }
                        else
                        {
                            Image img = new Bitmap(s);
                            Bitmap image = new Bitmap(ComprimirImagen.VaryQualityLevel((Image)img.Clone(), 35L));
                            uPPProvedor.Imagen = image.ToBase64String(img.RawFormat);
                        }
                    }

                    if (ModelState.IsValid)
                    {
                        uPPProvedor.Conexion = Conexion;
                        uPPProvedor.id_proveedor = id;
                        uPPProvedor.Usuario = User.Identity.Name;

                        if(uPPProvedor.id_municipio == 0)
                            uPPProvedor.id_municipio = 102; //Reforma

                        if (uPPProvedor.id_estado == 0)
                            uPPProvedor.id_municipio = 7; //Chiapas
                        
                        uPPProvedor = ProveedorDatos.CUPPProveedor(uPPProvedor);
                        if (uPPProvedor.Completado)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "Los datos se guardaron correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            uPPProvedor.Conexion = Conexion;
                            uPPProvedor.id_proveedor = id;
                            uPPProvedor.listaPaises = ProveedorDatos.obtenerListaPaises(uPPProvedor);
                            uPPProvedor.listaEstado = ProveedorDatos.obtenerListaEstados(uPPProvedor);
                            uPPProvedor.listaMunicipio = ProveedorDatos.obtenerListaMunicipios(uPPProvedor);
                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrio un error al intentar guardar los datos. Intente más tarde.";
                            return View(uPPProvedor);
                        }
                    }
                    else
                    {
                        uPPProvedor.Conexion = Conexion;
                        uPPProvedor.id_proveedor = id;
                        uPPProvedor.listaPaises = ProveedorDatos.obtenerListaPaises(uPPProvedor);
                        uPPProvedor.listaEstado = ProveedorDatos.obtenerListaEstados(uPPProvedor);
                        uPPProvedor.listaMunicipio = ProveedorDatos.obtenerListaMunicipios(uPPProvedor);
                        TempData["typemessage"] = "2";
                        TempData["message"] = "Ocurrio un error al intentar guardar los datos. Intente más tarde.";
                        return View(uPPProvedor);
                    }
                   
                }
                else
                {
                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {
                // UPPProvedorModels uPPProvedor = new UPPProvedorModels();
                uPPProvedor.Conexion = Conexion;
                uPPProvedor.id_proveedor = id;
                uPPProvedor.listaPaises = ProveedorDatos.obtenerListaPaises(uPPProvedor);
                uPPProvedor.listaEstado = ProveedorDatos.obtenerListaEstados(uPPProvedor);
                uPPProvedor.listaMunicipio = ProveedorDatos.obtenerListaMunicipios(uPPProvedor);
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error al intentar guardar los datos. Intente más tarde.";
                return View(uPPProvedor);
            }
        }
        //AJAX OBTIENE TODOS LOS COMBOS MEDIANTE JAVASCRIPT
        [HttpPost]
        public ActionResult Estado(string id)
        {
            try
            {
                UPPProvedorModels uPPProvedor = new UPPProvedorModels();
                _CatProveedor_Datos LugarDatos = new _CatProveedor_Datos();
                uPPProvedor.Conexion = Conexion;
                uPPProvedor.id_pais = id;
                uPPProvedor.listaEstado = LugarDatos.obtenerListaEstados(uPPProvedor);

                return Json(uPPProvedor.listaEstado, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]

        public ActionResult Municipio(string idPais, string id)
        {
            try
            {
                UPPProvedorModels uPPProvedor = new UPPProvedorModels();
                _CatProveedor_Datos LugarDatos = new _CatProveedor_Datos();


                uPPProvedor.id_estadoCodigo = id;
                uPPProvedor.id_pais = idPais;
                uPPProvedor.Conexion = Conexion;
                uPPProvedor.listaMunicipio = LugarDatos.obtenerListaMunicipios(uPPProvedor);
                return Json(uPPProvedor.listaMunicipio, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        #region Documentos Extras
        [HttpPost]
        public ActionResult JsonIndexDocumentosExtras(DataTableAjaxPostModel dataTableAjaxPostModel)
        {
            var ProveedorDatos = new _CatProveedor_Datos();

            var json = ProveedorDatos.DocumentosExtras_ObtenerIndex(dataTableAjaxPostModel);

            return Content(json, "application/json");
        }

        [HttpGet]
        public ActionResult DocumentosExtras(string IdProveedor)
        {
            if (string.IsNullOrEmpty(IdProveedor))
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Verifique sus datos.";
                return RedirectToAction("Index");
            }

            ViewBag.IdProveedorGanado = IdProveedor;

        return View();
        }

        [HttpGet]
        public ActionResult CreateDocumentoExtra(string idProveedor)
        {
            if (string.IsNullOrWhiteSpace(idProveedor))
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Verifique sus datos.";
                return RedirectToAction("Index");
            }
            Token.SaveToken();
            var model = new DocumentacionExtra_CatProveedorModel();
            var CatTipoDocumentacionExtraDatos = new _CatTipoDocumentacionExtra_Datos();

            var modulo = ProjectSettings.ModuloProveedor;
            ViewBag.ListaCatTipoDocumentacionExtra = CatTipoDocumentacionExtraDatos.ObtenerComboCatTiposDocumentacionExtraXIdModulo(modulo);
            model.IdProveedor = idProveedor;

            return View(model);
        }

        [HttpPost]
        public ActionResult CreateDocumentoExtra(DocumentacionExtra_CatProveedorModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (Token.IsTokenValid())
                    {
                        var fotoPerfilPostedFileBase = Request.Files["Archivo"] as HttpPostedFileBase;
                        var uploadImageToserver = new UploadFileToServerModel();
                        uploadImageToserver.FileBase = fotoPerfilPostedFileBase;
                        uploadImageToserver.BaseDir = "/Imagenes/Proveedor/DocumentacionExtra/";
                        uploadImageToserver.FileName = DateTime.Now.ToString("MM_dd_yyyy_hh_mm_ss");

                        if (fotoPerfilPostedFileBase != null && fotoPerfilPostedFileBase.ContentLength > 0)
                        {
                            //hay foto nueva
                            CidFaresHelper.UploadFileToServer(uploadImageToserver);

                            if (!uploadImageToserver.Success)
                            {
                                var CatTipoDocumentacionExtraDatos = new _CatTipoDocumentacionExtra_Datos();
                                var modulo = ProjectSettings.ModuloProveedor;
                                ViewBag.ListaCatTipoDocumentacionExtra = CatTipoDocumentacionExtraDatos.ObtenerComboCatTiposDocumentacionExtraXIdModulo(modulo);

                                TempData["typemessage"] = "2";
                                TempData["message"] = "Ocurrio un error al intentar guardar la imagen de perfil, intentelo más tarde.";
                                return View(model);
                            }



                            TempData["typemessage"] = "1";
                            TempData["message"] = "OK";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            TempData["typemessage"] = "2";
                            TempData["message"] = "Verifique sus datos.";
                            return RedirectToAction("Index");
                        }
                    }
                    else
                    {
                        TempData["typemessage"] = "2";
                        TempData["message"] = "Verifique sus datos.";
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Verifique sus datos.";
                    return RedirectToAction("Index");
                }

            }
            catch (Exception e)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Verifique sus datos.";
                return RedirectToAction("Index");
            }
        }
        #endregion
    }
}
