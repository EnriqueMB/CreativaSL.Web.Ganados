using CreativaSL.Web.Ganados.Filters;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CreativaSL.Web.Ganados.Models;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using CreativaSL.Web.Ganados.App_Start;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    [Autorizado]
    public class CatFierroController : Controller
    {
        private TokenProcessor Token = TokenProcessor.GetInstance();
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");

        public ActionResult DatatableIndex()
        {
            try
            {
                CatFierroModels fierro = new CatFierroModels();
                CatFierro_Datos fierroDatos = new CatFierro_Datos();

                fierro.Conexion = Conexion;
                fierro.RespuestaAjax = new RespuestaAjax();
                fierro.RespuestaAjax.Mensaje = fierroDatos.DatatableIndex(fierro);
                fierro.RespuestaAjax.Success = true;

                return Content(fierro.RespuestaAjax.Mensaje, "application/json");

            }
            catch (Exception ex)
            {
                CatFierroModels fierro = new CatFierroModels();
                fierro.RespuestaAjax = new RespuestaAjax();
                fierro.RespuestaAjax.Mensaje = ex.ToString();
                fierro.RespuestaAjax.Success = false;
                return Content(fierro.RespuestaAjax.ToJSON(), "application/json");
            }
        }

        // GET: Admin/CatFierro
        public ActionResult Index()
        {
            try
            {
                CatFierroModels fierro = new CatFierroModels();
                return View(fierro);
            }
            catch (Exception)
            {
                CatFierroModels Fierro = new CatFierroModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Fierro);
            }
        }

        // GET: Admin/CatFierro/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/CatFierro/Create
        [HttpGet]
        public ActionResult Create(string Id_servicio)
        {
            try
            {

                Token.SaveToken();
                CatFierroModels Fierro = new CatFierroModels();
                Fierro.Id_servicio = Id_servicio;
                return View(Fierro);
            }
            catch (Exception)
            {
                CatFierroModels Fierro = new CatFierroModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Fierro);
            }
        }

       // POST: Admin/CatFierro/Create
       [HttpPost]
        public ActionResult Create(CatFierroModels Fierro)
        {
            try
            {
                if (Token.IsTokenValid())
                {
                    CatFierro_Datos FierroDatos = new CatFierro_Datos();
                    Fierro.Conexion = Conexion;
                    Fierro.Opcion = 1;
                    Fierro.Usuario = User.Identity.Name;
                    string[] tmp = Fierro.ImgFierro.Split(',');
                    Fierro.ImgFierro = tmp[1];
                    Fierro = FierroDatos.AbcCatFierro(Fierro);
                    if (!string.IsNullOrEmpty(Fierro.IDFierro))
                    {
                        
                        string baseDir = Server.MapPath("~/Imagenes/Fierro/");
                        Image Img = Comun.Base64StringToBitmap(Fierro.ImgFierro);
                        Bitmap image = new Bitmap(ComprimirImagen.VaryQualityLevel((Image)Img.Clone(), 32L));
                        ImageCodecInfo jpgEncoder = ComprimirImagen.GetEncoder(ComprimirImagen.GetImageFormat(Img));
                        string fileName = Fierro.IDFierro + ".png";
                        Fierro.ImgFierro = image.ToBase64String(ImageFormat.Png);
                        string newImagePath = baseDir + fileName;
                        image.Save(newImagePath, ImageFormat.Png);
                        Fierro.NombreArchivo = fileName;
                        Fierro = FierroDatos.ActualizarImagen(Fierro);
                        if (Fierro.Completado == true)
                        {
                            if (!string.IsNullOrEmpty(Fierro.Id_servicio))
                            {
                                TempData["typemessage"] = "1";
                                TempData["message"] = "El fierro se registro correctamente a la compra.";
                                Token.ResetToken();
                                Fierro.RespuestaAjax = new RespuestaAjax();
                                Fierro.RespuestaAjax.Success = true;
                                Fierro.RespuestaAjax.Href = Fierro.Id_servicio;
                                return Content(Fierro.RespuestaAjax.ToJSON(), "application/json");
                                //return RedirectToAction("DocumentosCompra", "Compra", new { Id_1 = Fierro.Id_servicio });
                            }
                            else
                            {
                                TempData["typemessage"] = "1";
                                TempData["message"] = "Los datos se guardarón correctamente.";
                                Token.ResetToken();
                                Fierro.RespuestaAjax = new RespuestaAjax();
                                Fierro.RespuestaAjax.Success = true;
                                return Content(Fierro.RespuestaAjax.ToJSON(), "application/json");
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(Fierro.Id_servicio))
                            {
                                TempData["typemessage"] = "2";
                                TempData["message"] = "Ocurrio un error al intentar guardar el fierro a la compra. Intente más tarde.";
                                Token.ResetToken();
                                Fierro.RespuestaAjax = new RespuestaAjax();
                                Fierro.RespuestaAjax.Success = false;
                                Fierro.RespuestaAjax.Href = Fierro.Id_servicio;
                                Fierro.RespuestaAjax.Mensaje = "Ocurrio un error al intentar guardar el fierro a la compra. Intente más tarde.";
                                return Content(Fierro.RespuestaAjax.ToJSON(), "application/json");
                            }
                            else
                            {
                                TempData["typemessage"] = "2";
                                TempData["message"] = "Ocurrio un error al intentar guardar los datos. Intente más tarde.";
                                Fierro.RespuestaAjax = new RespuestaAjax();
                                Fierro.RespuestaAjax.Success = false;

                                return Content(Fierro.RespuestaAjax.ToJSON(), "application/json");
                            }
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(Fierro.Id_servicio))
                        {
                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrio un error al intentar guardar el fierro a la compra. Intente más tarde.";
                            Token.ResetToken();
                            Fierro.RespuestaAjax = new RespuestaAjax();
                            Fierro.RespuestaAjax.Success = false;
                            Fierro.RespuestaAjax.Href = Fierro.Id_servicio;
                            Fierro.RespuestaAjax.Mensaje = "Ocurrio un error al intentar guardar el fierro a la compra. Intente más tarde.";
                            return Content(Fierro.RespuestaAjax.ToJSON(), "application/json");
                        }
                        else
                        {
                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrio un error al intentar guardar los datos. Intente más tarde.";
                            Fierro.RespuestaAjax = new RespuestaAjax();
                            Fierro.RespuestaAjax.Success = false;

                            return Content(Fierro.RespuestaAjax.ToJSON(), "application/json");
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(Fierro.Id_servicio))
                    {
                        TempData["typemessage"] = "2";
                        TempData["message"] = "Ocurrio un error al intentar guardar el fierro a la compra. Intente más tarde.";
                        Token.ResetToken();
                        Fierro.RespuestaAjax = new RespuestaAjax();
                        Fierro.RespuestaAjax.Success = false;
                        Fierro.RespuestaAjax.Href = Fierro.Id_servicio;
                        Fierro.RespuestaAjax.Mensaje = "Ocurrio un error al intentar guardar el fierro a la compra. Intente más tarde.";
                        return Content(Fierro.RespuestaAjax.ToJSON(), "application/json");
                    }
                    else
                    {
                        TempData["typemessage"] = "2";
                        TempData["message"] = "Ocurrio un error al intentar guardar los datos. Intente más tarde.";
                        Fierro.RespuestaAjax = new RespuestaAjax();
                        Fierro.RespuestaAjax.Success = false;

                        return Content(Fierro.RespuestaAjax.ToJSON(), "application/json");
                    }
                }
               
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(Fierro.Id_servicio))
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Ocurrio un error al intentar guardar el fierro a la compra. Intente más tarde.";
                    Token.ResetToken();
                    Fierro.RespuestaAjax = new RespuestaAjax();
                    Fierro.RespuestaAjax.Success = false;
                    Fierro.RespuestaAjax.Href = Fierro.Id_servicio;
                    Fierro.RespuestaAjax.Mensaje = "Ocurrio un error al intentar guardar el fierro a la compra. Intente más tarde.";
                    return Content(Fierro.RespuestaAjax.ToJSON(), "application/json");
                }
                else
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Ocurrio un error al intentar guardar los datos. Intente más tarde.";
                    Fierro.RespuestaAjax = new RespuestaAjax();
                    Fierro.RespuestaAjax.Success = false;
                    
                    return Content(Fierro.RespuestaAjax.ToJSON(), "application/json");
                }
            }
        }
        public ActionResult UploadImagen(string Id_servicio)
        {
            try
            {
                Token.SaveToken();
                CatFierroModels Fierro = new CatFierroModels();
                Fierro.Id_servicio = Id_servicio;
                return View(Fierro);
            }
            catch (Exception)
            {
                CatFierroModels Fierro = new CatFierroModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Fierro);
            }
        }

        // POST: Admin/CatFierro/Create
        [HttpPost]
        public ActionResult UploadImagen(CatFierroModels Fierro)
        {
            try
            {
                if (Token.IsTokenValid())
                {
                    HttpPostedFileBase bannerImage = Request.Files[0] as HttpPostedFileBase;

                    MemoryStream ms = new MemoryStream();
                    bannerImage.InputStream.CopyTo(ms);
                    bannerImage.InputStream.Position = ms.Position = 0;
                    Stream s2 = ms;

                    if (!string.IsNullOrEmpty(bannerImage.FileName))
                    {
                        if (bannerImage != null && bannerImage.ContentLength > 0)
                        {
                            Stream s = bannerImage.InputStream;

                            if (Path.GetExtension(bannerImage.FileName).ToLower() == ".heic")
                            {

                                Image img = (Image)Auxiliar.ProcessFile(s);
                                Bitmap image = new Bitmap(ComprimirImagen.VaryQualityLevel((Image)img.Clone(), 35L));
                                Fierro.ImgFierro = image.ToBase64String(ImageFormat.Jpeg);
                            }
                            else
                            {
                                Bitmap img = new Bitmap(s);
                                Fierro.ImgFierro = img.ToBase64String(img.RawFormat);
                            }
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Cargar imagen Fierro");
                    }
                        CatFierro_Datos FierroDatos = new CatFierro_Datos();
                        Fierro.Conexion = Conexion;
                        Fierro.Opcion = 1;
                        Fierro.Usuario = User.Identity.Name;

                        Fierro = FierroDatos.AbcCatFierro(Fierro);
                    if (!string.IsNullOrEmpty(Fierro.IDFierro))
                    {
                        if (!string.IsNullOrEmpty(bannerImage.FileName))
                        {
                            string baseDir = Server.MapPath("~/Imagenes/Fierro/");
                            string fileExtension = Path.GetExtension(bannerImage.FileName);
                            fileExtension = fileExtension == (".heic") ? ".png" : fileExtension;
                            
                            string fileName = Fierro.IDFierro + fileExtension;
                            Bitmap IMG3 = null;

                            if (Path.GetExtension(bannerImage.FileName) == ".heic")
                            {
                                Image img = (Image)Auxiliar.ProcessFile(s2);
                                Bitmap image = new Bitmap(ComprimirImagen.VaryQualityLevel((Image)img.Clone(), 35L));
                                IMG3 = ComprimirImagen.SaveJpeg(baseDir + fileName, image, 50, true);
                            }
                            else
                            {
                                Image Img2 = new Bitmap(s2);
                                IMG3 = ComprimirImagen.SaveJpeg(baseDir + fileName, Img2, 50, true);
                            }

                            Fierro.ImgFierro = IMG3.ToBase64String(ImageFormat.Jpeg);

                            Fierro.NombreArchivo = fileName;
                            Fierro = FierroDatos.ActualizarImagen(Fierro);
                            if (Fierro.Completado == true)
                            {
                                if (!string.IsNullOrEmpty(Fierro.Id_servicio))
                                {
                                    TempData["typemessage"] = "1";
                                    TempData["message"] = "El fierro se registro correctamente a la compra.";
                                    Token.ResetToken();
                                    return RedirectToAction("DocumentosCompra", "Compra", new { Id_1 = Fierro.Id_servicio });
                                }
                                else
                                {
                                    TempData["typemessage"] = "1";
                                    TempData["message"] = "Los datos se guardaron correctamente.";
                                    Token.ResetToken();
                                    return RedirectToAction("Index");
                                }
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(Fierro.Id_servicio))
                                {
                                    TempData["typemessage"] = "2";
                                    TempData["message"] = "Ocurrio un error al intentar guardar la imagen de fierro. Intente más tarde.";
                                    return RedirectToAction("DocumentosCompra", "Compra", new { Id_1 = Fierro.Id_servicio });
                                }
                                else
                                {
                                    TempData["typemessage"] = "2";
                                    TempData["message"] = "Ocurrio un error al intentar guardar los datos. Intente más tarde.";
                                    return View(Fierro);
                                }
                            }
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(Fierro.Id_servicio))
                        {
                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrio un error al intentar guardar la imagen del fierro. Intente más tarde.";
                            return RedirectToAction("DocumentosCompra", "Compra", new { Id_1 = Fierro.Id_servicio });
                        }
                        else
                        {
                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrio un error al intentar guardar los datos. Intente más tarde.";
                            return View(Fierro);
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(Fierro.Id_servicio))
                    {
                        TempData["typemessage"] = "2";
                        TempData["message"] = "Verifique sus datos";
                        return RedirectToAction("DocumentosCompra", "Compra", new { Id_1 = Fierro.Id_servicio });
                    }
                    else
                    {
                        TempData["typemessage"] = "2";
                        TempData["message"] = "Verifique sus datos";
                        return View(Fierro);
                    }
                }
                return View(Fierro);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(Fierro.Id_servicio))
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Verifique sus datos";
                    return RedirectToAction("DocumentosCompra", "Compra", new { Id_1 = Fierro.Id_servicio });
                }
                else
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Verifique sus datos";
                    return View(Fierro);
                }
            }
        }
        // GET: Admin/CatFierro/Edit/5
        [HttpGet]
        public ActionResult Edit(string IDFierro)
        {
            try
            {
                Token.SaveToken();
                CatFierroModels Fierro = new CatFierroModels();
                CatFierro_Datos FierroDatos = new CatFierro_Datos();
                Fierro.IDFierro = IDFierro;
                Fierro.Conexion = Conexion;
                Fierro = FierroDatos.ObtenerDetalleCatFierro(Fierro);
                return View(Fierro);
            }
            catch (Exception)
            {
                CatFierroModels Fierro = new CatFierroModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Fierro);
            }
        }

        // POST: Admin/CatFierro/Edit/5
        [HttpPost]
        public ActionResult Edit(CatFierroModels Fierro)
        {
            try
            {
                if(Token.IsTokenValid())
                {
                    CatFierro_Datos FierroDatos = new CatFierro_Datos();
                    Fierro.Conexion = Conexion;
                    Fierro.Opcion = 2;
                    Fierro.Usuario = User.Identity.Name;
                    string[] tmp = Fierro.ImgFierro.Split(',');
                    Fierro.ImgFierro = tmp[1];
                    Fierro = FierroDatos.AbcCatFierro(Fierro);

                    if (Fierro.Completado == true)
                    {
                        TempData["typemessage"] = "1";
                        TempData["message"] = "Los datos se guardarón correctamente.";
                        Token.ResetToken();
                        Fierro.RespuestaAjax = new RespuestaAjax();
                        Fierro.RespuestaAjax.Success = true;

                        return Content(Fierro.RespuestaAjax.ToJSON(), "application/json");
                    }
                    else
                    {
                        TempData["typemessage"] = "2";
                        TempData["message"] = "Ocurrio un error al intentar guardar los datos. Intente más tarde.";
                        Fierro.RespuestaAjax = new RespuestaAjax();
                        Fierro.RespuestaAjax.Success = false;

                        return Content(Fierro.RespuestaAjax.ToJSON(), "application/json");
                    }
                }
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Verifique sus datos";

                    Fierro.RespuestaAjax = new RespuestaAjax();
                    Fierro.RespuestaAjax.Success = false;

                    return Content(Fierro.RespuestaAjax.Mensaje, "application/json");
                }
            }
            catch (Exception ex)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error al intentar guardar los datos. Contacte a soporte técnico.";
                Fierro.RespuestaAjax = new RespuestaAjax();
                Fierro.RespuestaAjax.Success = false;

                return Content(Fierro.RespuestaAjax.Mensaje, "application/json");
            }
        }

        // GET: Admin/CatFierro/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/CatFierro/Delete/5
        [HttpPost]
        public ActionResult Delete(string IDFierro)
        {
            try
            {
                CatFierro_Datos FierroDatos = new CatFierro_Datos();
                CatFierroModels Fierro = new CatFierroModels();
                Fierro.Conexion = Conexion;
                Fierro.IDFierro = IDFierro;
                Fierro.Usuario = User.Identity.Name;

                Fierro = FierroDatos.EliminarFierro(Fierro);
                if (Fierro.Completado == true)
                {
                    TempData["typemessage"] = "1";
                    TempData["message"] = "El fierro se ha eliminado correctamente.";
                    Token.ResetToken();
                    Fierro.RespuestaAjax = new RespuestaAjax();
                    Fierro.RespuestaAjax.Success = true;

                    return Content(Fierro.RespuestaAjax.ToJSON(), "application/json");
                }
                else
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Ocurrio un error al intentar eliminar el fierro. Intente más tarde.";
                    Fierro.RespuestaAjax = new RespuestaAjax();
                    Fierro.RespuestaAjax.Success = false;

                    return Content(Fierro.RespuestaAjax.ToJSON(), "application/json");
                }
            }
            catch (Exception ex)
            {
                CatFierroModels Fierro = new CatFierroModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error al intentar guardar los datos. Contacte a soporte técnico.";
                Fierro.RespuestaAjax = new RespuestaAjax();
                Fierro.RespuestaAjax.Success = false;

                return Content(Fierro.RespuestaAjax.Mensaje, "application/json");
            }
        }
    }
}
