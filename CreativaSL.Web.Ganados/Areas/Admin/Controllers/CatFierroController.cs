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
using CreativaSL.Web.Ganados.Models.Datatable;
using CreativaSL.Web.Ganados.Models.Helpers;
using CreativaSL.Web.Ganados.Models.System;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    [Autorizado]
    public class CatFierroController : Controller
    {
        private TokenProcessor Token = TokenProcessor.GetInstance();
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");

        public ActionResult DatatableIndex(DataTableAjaxPostModel dataTableAjaxPostModel)
        {
            try
            {
                var fierro = new CatFierroModels();
                var fierroDatos = new CatFierro_Datos();

                fierro.Conexion = Conexion;
                fierro.RespuestaAjax = new RespuestaAjax();
                fierro.RespuestaAjax.Mensaje = fierroDatos.DatatableIndex(dataTableAjaxPostModel);
                fierro.RespuestaAjax.Success = true;

                return Content(fierro.RespuestaAjax.Mensaje, "application/json");

            }
            catch (Exception ex)
            {
                var fierro = new CatFierroModels();
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
                var Fierro = new CatFierroModels();
                Fierro.Id_servicio = Id_servicio;
                return View(Fierro);
            }
            catch (Exception)
            {
                var Fierro = new CatFierroModels();
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
                        
                        //string baseDir = Server.MapPath("~/Imagenes/Fierro/");
                        //Image Img = Comun.Base64StringToBitmap(Fierro.ImgFierro);
                        //Bitmap image = new Bitmap(ComprimirImagen.VaryQualityLevel((Image)Img.Clone(), 32L));
                        //ImageCodecInfo jpgEncoder = ComprimirImagen.GetEncoder(ComprimirImagen.GetImageFormat(Img));
                        //string fileName = Fierro.IDFierro + ".png";
                        //Fierro.ImgFierro = image.ToBase64String(ImageFormat.Png);
                        //string newImagePath = baseDir + fileName;
                        //image.Save(newImagePath, ImageFormat.Png);
                        

                        var uploadBase64ToServerModel = CidFaresHelper.UploadBase64ToServer(Fierro.ImgFierro,
                            ProjectSettings.BaseDirCatFierro);

                        Fierro.NombreArchivo = uploadBase64ToServerModel.FileName;
                        Fierro.ImgFierro = uploadBase64ToServerModel.FileName;
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
                    var bannerImage = Request.Files[0] as HttpPostedFileBase;
                    var fileName = Guid.NewGuid().ToString().ToUpper();
                    var uploadImageToserver = new UploadFileToServerModel();
                    uploadImageToserver.FileBase = bannerImage;
                    uploadImageToserver.BaseDir = ProjectSettings.BaseDirCatFierro;
                    uploadImageToserver.FileName = fileName;

                    CidFaresHelper.UploadFileToServer(uploadImageToserver);

                    if(!uploadImageToserver.Success)
                    { 
                        ModelState.AddModelError(string.Empty, "Cargar imagen Fierro");
                    }

                    Fierro.ImgFierro = uploadImageToserver.FileName;

                    var FierroDatos = new CatFierro_Datos();
                    Fierro.Conexion = Conexion;
                    Fierro.Opcion = 1;
                    Fierro.Usuario = User.Identity.Name;
                    
                    Fierro = FierroDatos.AbcCatFierro(Fierro);
                    if (!Fierro.Completado)
                    {
                        CidFaresHelper.DeleteFileFromServer(uploadImageToserver);
                        TempData["typemessage"] = "2";
                        TempData["message"] = "Verifique sus datos";
                        return View(Fierro);
                    }

                    TempData["typemessage"] = "1";
                    TempData["message"] = "Fierro creado satisfactoriamente.";
                    return RedirectToAction("Index");
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

                    var uploadImageToserver = new UploadFileToServerModel();
                    uploadImageToserver.BaseDir = ProjectSettings.BaseDirCatFierro;
                    uploadImageToserver.FileName = Fierro.ImgFierro;

                    CidFaresHelper.DeleteFileFromServer(uploadImageToserver);

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
