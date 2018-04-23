using System.Configuration;
using System;
using System.Collections.Generic;
using CreativaSL.Web.Ganados.Models;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CreativaSL.Web.Ganados.Filters;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using CreativaSL.Web.Ganados.App_Start;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    [Autorizado]
    public class CatBancoController : Controller
    {
        private TokenProcessor Token = TokenProcessor.GetInstance();
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
        // GET: Admin/CatBanco
        public ActionResult Index()
        {
            try
            {
                CatBancoModels Banco = new CatBancoModels();
                _CatBanco_Datos BancoDatos = new _CatBanco_Datos();
                Banco.Conexion = Conexion;
                Banco.listaBancos = BancoDatos.ObtenerlistaBancos(Banco);
                return View(Banco);
            }
            catch (Exception)
            {
                CatBancoModels Bancos = new CatBancoModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Bancos);
            }
        }
        // GET: Admin/CatBanco/Create
        public ActionResult Create()
        {
            try
            {
                Token.SaveToken();
                CatBancoModels Banco = new CatBancoModels();
                //_CatBanco_Datos BancoDatos = new _CatBanco_Datos();
                //Banco.Conexion = Conexion;
                return View(Banco);
            }
            catch (Exception)
            {
                CatBancoModels Bancos = new CatBancoModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Bancos);
            }
        }
        // POST: Admin/CatBanco/Create
        [HttpPost]
        public ActionResult Create(CatBancoModels Banco)
        {
            _CatBanco_Datos BancoDatos = new _CatBanco_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    HttpPostedFileBase bannerImage = Request.Files[0] as HttpPostedFileBase;
                    if (!string.IsNullOrEmpty(bannerImage.FileName))
                    {
                        if (bannerImage != null && bannerImage.ContentLength > 0)
                        {

                            Stream s = bannerImage.InputStream;
                            Bitmap img = new Bitmap(s);
                            Banco.Imagen = img.ToBase64String(ImageFormat.Png);

                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Cargar imagen Banco");
                    }
                    if (ModelState.IsValid)
                    {
                        Banco.Conexion = Conexion;
                        Banco.Usuario = User.Identity.Name;
                        Banco.Opcion = 1;
                        Banco = BancoDatos.DaCatBancos(Banco);
                        if (Banco.Completado)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "Los datos se guardaron correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrio un error al intentar guardar los datos. Intente más tarde.";
                            return View(Banco);
                        }
                    }
                    else
                    {
                        return View(Banco);
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error al intentar guardar los datos. Contacte a soporte técnico.";
                return View(Banco);
            }
        }
        // GET: Admin/CatBanco/Edit
        //[HttpGet]
        public ActionResult Edit(int id)
        {
            try
            {
                Token.SaveToken();
                CatBancoModels Banco = new CatBancoModels();
                _CatBanco_Datos BancoDatos = new _CatBanco_Datos();
                Banco.IDBanco = id;
                Banco.Conexion = Conexion;
                Banco = BancoDatos.ObternerDetalleCatBanco(Banco);
                return View(Banco);
            }
            catch (Exception ex)
            {
                CatProveedorModels Proveedor = new CatProveedorModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Proveedor);
            }
        }
        // POST: Admin/CatBanco/Edit
        [HttpPost]
        public ActionResult Edit(int id, CatBancoModels Banco)
        {
            try
            {
                if (Token.IsTokenValid())
                {
                    ModelState.Remove("ImagenB");
                    if (ModelState.IsValid)
                    {
                        //CatBancoModels Banco = new CatBancoModels();
                        _CatBanco_Datos BancoDatos = new _CatBanco_Datos();
                        Banco.Conexion = Conexion;
                        Banco.IDBanco = id;
                        Banco.Opcion = 2;
                        Banco.Usuario = User.Identity.Name;
                        // Banco.Descripcion = collection["Descripcion"];

                        HttpPostedFileBase bannerImage = Request.Files[0] as HttpPostedFileBase;
                        if (!string.IsNullOrEmpty(bannerImage.FileName))
                        {
                            if (bannerImage != null && bannerImage.ContentLength > 0)
                            {
                                Stream s = bannerImage.InputStream;
                                Bitmap img = new Bitmap(s);
                                Banco.Imagen = img.ToBase64String(ImageFormat.Png);
                            }
                        }
                        else
                        {
                            Banco.BandImg = true;
                        }
                        Banco = BancoDatos.DaCatBancos(Banco);
                        if (Banco.Completado == true)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "Los datos se guardarón correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrio un error al intentar guardar los datos. Intente más tarde.";
                            return View("");
                        }
                    }
                    else
                    {
                        return View(Banco);
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error al intentar guardar los datos. Contacte a soporte técnico.";
                return View(Banco);
            }

        }
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/CatBanco/Delete
        [HttpPost]
        public ActionResult Delete(int id, CatBancoModels Banco)
        {
            try
            {
                //CatBancoModels Banco = new CatBancoModels();
                _CatBanco_Datos BancoDatos = new _CatBanco_Datos();
                Banco.Conexion = Conexion;
                Banco.Opcion = 3;
                Banco.IDBanco = id;
                Banco.Usuario = User.Identity.Name;
                Banco = BancoDatos.EliminarCatBanco(Banco);
                if (Banco.Completado == true)
                {
                    TempData["typemessage"] = "1";
                    TempData["message"] = "El registro se ha eliminado correctamente";
                }
                return Json("");


            }
            catch
            {
                CatBancoModels Bancos = new CatBancoModels();

                TempData["typemessage"] = "2";
                TempData["message"] = "No se pudo borrar los datos. Por favor contacte a soporte técnico";
                return Json("");

            }
        }
    }
}