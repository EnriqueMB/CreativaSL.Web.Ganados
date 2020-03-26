using System.Configuration;
using System;
using CreativaSL.Web.Ganados.Models;
using System.Web;
using System.Web.Mvc;
using CreativaSL.Web.Ganados.Filters;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using CreativaSL.Web.Ganados.App_Start;
using CreativaSL.Web.Ganados.Models.Helpers;
using CreativaSL.Web.Ganados.Models.System;

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
                    if (bannerImage != null && bannerImage.ContentLength > 0)
                    {
                        var uploadImageImagenBancoToserver = new UploadFileToServerModel();
                        uploadImageImagenBancoToserver.FileBase = bannerImage;
                        uploadImageImagenBancoToserver.BaseDir = ProjectSettings.BaseDirCatBanco;
                        uploadImageImagenBancoToserver.FileName =
                            Banco.Imagen?.Replace(ProjectSettings.BaseDirCatBanco, string.Empty);

                        CidFaresHelper.DeleteFileFromServer(uploadImageImagenBancoToserver);

                        uploadImageImagenBancoToserver.FileName = Guid.NewGuid().ToString().ToUpper();
                        CidFaresHelper.UploadFileToServer(uploadImageImagenBancoToserver);
                        Banco.Imagen = uploadImageImagenBancoToserver.FileName;

                        if (!uploadImageImagenBancoToserver.Success)
                        {
                            //borramos en caso que se halla subido y marque error
                            CidFaresHelper.DeleteFilesWithOutExtensionFromServer(uploadImageImagenBancoToserver);
                            TempData["typemessage"] = "2";
                            TempData["message"] =
                                "Ha ocurrido un error al guardar la imagen del banco, intenlo subir de nuevo o contacte con soporte técnico.";
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

                        var  bannerImage = Request.Files[0] as HttpPostedFileBase;

                        if (bannerImage != null && bannerImage.ContentLength > 0)
                        {
                            var uploadImageImagenBancoToserver = new UploadFileToServerModel();
                            uploadImageImagenBancoToserver.FileBase = bannerImage;
                            uploadImageImagenBancoToserver.BaseDir = ProjectSettings.BaseDirCatBanco;
                            uploadImageImagenBancoToserver.FileName =
                                Banco.Imagen?.Replace(ProjectSettings.BaseDirCatBanco, string.Empty);

                            CidFaresHelper.DeleteFileFromServer(uploadImageImagenBancoToserver);

                            uploadImageImagenBancoToserver.FileName = Guid.NewGuid().ToString().ToUpper();
                            CidFaresHelper.UploadFileToServer(uploadImageImagenBancoToserver);
                            Banco.Imagen = uploadImageImagenBancoToserver.FileName;

                            if (!uploadImageImagenBancoToserver.Success)
                            {
                                //borramos en caso que se halla subido y marque error
                                CidFaresHelper.DeleteFilesWithOutExtensionFromServer(uploadImageImagenBancoToserver);
                                TempData["typemessage"] = "2";
                                TempData["message"] =
                                    "Ha ocurrido un error al guardar la imagen del banco, intenlo subir de nuevo o contacte con soporte técnico.";
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
                
                if (Banco.Completado)
                {
                    var uploadImageImagenBancoToserver = new UploadFileToServerModel();
                    uploadImageImagenBancoToserver.BaseDir = ProjectSettings.BaseDirCatBanco;
                    uploadImageImagenBancoToserver.FileName = Banco.Imagen;

                    CidFaresHelper.DeleteFileFromServer(uploadImageImagenBancoToserver);
                }

                return Json("");
            }
            catch
            {
                CatBancoModels Bancos = new CatBancoModels();

                //TempData["typemessage"] = "2";
                //TempData["message"] = "No se pudo borrar los datos. Por favor contacte a soporte técnico";
                return Json("");

            }
        }
    }
}