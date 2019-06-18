using CreativaSL.Web.Ganados.App_Start;
using CreativaSL.Web.Ganados.Filters;

using CreativaSL.Web.Ganados.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    [Autorizado]
    public class CatVehiculoController : Controller
    {
        private TokenProcessor Token = TokenProcessor.GetInstance();
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
        // GET: Admin/CatVehiculo
        public ActionResult Index()
        {
            try
            {
                CatVehiculoModels Vehiculo = new CatVehiculoModels();
                _CatVehiculo_Datos VehiculoDatos = new _CatVehiculo_Datos();
                Vehiculo.Conexion = Conexion;
                Vehiculo = VehiculoDatos.ObtenerListaVehiculos(Vehiculo);
                return View(Vehiculo);
            }
            catch (Exception)
            {
                CatMarcaVehiculoModels Marca = new CatMarcaVehiculoModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Marca);
            }
        }

        // GET: Admin/CatVehiculo/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/CatVehiculo/Create
        public ActionResult Create()
        {
            try
            {
                Token.SaveToken();
                CatVehiculoModels Vehiculo = new CatVehiculoModels();
                _CatVehiculo_Datos VehiculoDatos = new _CatVehiculo_Datos();
                Vehiculo.Conexion = Conexion;
                Vehiculo.listaTipoVehiculos = VehiculoDatos.obtenerListaTipoVehiculo(Vehiculo);
                Vehiculo.listaSucursal = VehiculoDatos.obtenerListaSucursales(Vehiculo);
                Vehiculo.listaMarcas = VehiculoDatos.obtenerListaMarcas(Vehiculo);
                Vehiculo.ListaEmpresas = VehiculoDatos.obtenerListaEmpresas(Vehiculo);
                Vehiculo.Estatus = Convert.ToBoolean("true");
                Vehiculo.EsPropio = Convert.ToBoolean("true");

                return View(Vehiculo);
            }
            catch (Exception ex)
            {
                CatVehiculoModels Vehiculo = new CatVehiculoModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Vehiculo);
            }
        }

        // POST: Admin/CatVehiculo/Create
        [HttpPost]
      
        public ActionResult Create(CatVehiculoModels Vehiculo)
        {
            _CatVehiculo_Datos VehiculoDatos = new _CatVehiculo_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        Vehiculo.Conexion = Conexion;
                        Vehiculo.Opcion = 1;
                        Vehiculo.IDVehiculo = "0";
                        Vehiculo.Usuario = User.Identity.Name;
                        HttpPostedFileBase bannerImage = Request.Files[0] as HttpPostedFileBase;
                        if (bannerImage != null && bannerImage.ContentLength > 0)
                        {
                            Stream s = bannerImage.InputStream;
                            //Bitmap img = new Bitmap(s);
                            //Vehiculo.img64 = img.ToBase64String(ImageFormat.Png);

                            if (Path.GetExtension(bannerImage.FileName).ToLower() == ".heic")
                            {
                                Image img = (Image)Auxiliar.ProcessFile(s);
                                Bitmap image = new Bitmap(ComprimirImagen.VaryQualityLevel((Image)img.Clone(), 35L));
                                Vehiculo.img64 = image.ToBase64String(ImageFormat.Jpeg);
                            }
                            else
                            {
                                Image img = new Bitmap(s);
                                Bitmap image = new Bitmap(ComprimirImagen.VaryQualityLevel((Image)img.Clone(), 35L));
                                Vehiculo.img64 = image.ToBase64String(img.RawFormat);
                            }

                        }
                        Vehiculo.Estatus = true;
                        Vehiculo = VehiculoDatos.AcCatVehiculo(Vehiculo);
                        if (Vehiculo.Completado == true)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "El registro se guardo correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            Vehiculo.ListaEmpresas = VehiculoDatos.obtenerListaEmpresas(Vehiculo);
                            Vehiculo.listaTipoVehiculos = VehiculoDatos.obtenerListaTipoVehiculo(Vehiculo);
                            Vehiculo.listaSucursal = VehiculoDatos.obtenerListaSucursales(Vehiculo);
                            Vehiculo.listaMarcas = VehiculoDatos.obtenerListaMarcas(Vehiculo);
                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrió un error al guardar el registro.";
                            return View(Vehiculo);
                        }
                    }
                    else
                    {
                        Vehiculo.Conexion = Conexion;
                        Vehiculo.listaTipoVehiculos = VehiculoDatos.obtenerListaTipoVehiculo(Vehiculo);
                        Vehiculo.listaSucursal = VehiculoDatos.obtenerListaSucursales(Vehiculo);
                        Vehiculo.listaMarcas = VehiculoDatos.obtenerListaMarcas(Vehiculo);
                        Vehiculo.ListaEmpresas = VehiculoDatos.obtenerListaEmpresas(Vehiculo);
                        return View(Vehiculo);

                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                Vehiculo.Conexion = Conexion;
                Vehiculo.listaTipoVehiculos = VehiculoDatos.obtenerListaTipoVehiculo(Vehiculo);
                Vehiculo.listaSucursal = VehiculoDatos.obtenerListaSucursales(Vehiculo);
                Vehiculo.listaMarcas = VehiculoDatos.obtenerListaMarcas(Vehiculo);
                Vehiculo.ListaEmpresas = VehiculoDatos.obtenerListaEmpresas(Vehiculo);
                TempData["typemessage"] = "2";
                TempData["message"] = "No se pudo guardar los datos. Por favor contacte a soporte técnico";
                return View(Vehiculo);
            }
        }
        #region Archivos
        [HttpPost]
        public ActionResult LoadTableArchivos(string id_vehiculo)
        {
            try
            {
                _CatVehiculo_Datos Datos = new _CatVehiculo_Datos();
                string datatable = Datos.VEHICULO_index_Archivo(Conexion, id_vehiculo);

                return Content(datatable, "application/json");
            }
            catch (Exception ex)
            {
                return Content("", "application/json");
            }
        }

        [HttpGet]
        public ActionResult Archivos(string id, string nombreVehiculo)
        {
            try
            {
                if (string.IsNullOrEmpty(id.Trim()) || string.IsNullOrEmpty(nombreVehiculo))
                {
                    return RedirectToAction("Index");
                }

                ViewBag.NombreVehiculo = nombreVehiculo;
                ViewBag.Id_vehiculo = id;

                return View();
            }
            catch (Exception)
            {
                CatVehiculoModels Vehiculo = new CatVehiculoModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public ActionResult AgregarArchivo(string id_vehiculo)
        {
            try
            {
                if (string.IsNullOrEmpty(id_vehiculo.Trim()))
                {
                    return RedirectToAction("Index");
                }

                ArchivoVehiculoModels Archivo = new ArchivoVehiculoModels();

                Archivo.Id_vehiculo = id_vehiculo;

                return View(Archivo);
            }
            catch (Exception ex)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Verifique sus datos.";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult AgregarArchivo(ArchivoVehiculoModels ArchivoModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(ArchivoModel);

                }

                _CatVehiculo_Datos Datos = new _CatVehiculo_Datos();

                if (Path.GetExtension(ArchivoModel.Archivo.FileName).ToLower() == ".heic")
                {
                    ArchivoModel.UrlArchivo = Guid.NewGuid().ToString() + ".png";
                    ArchivoModel.NombreArchivo = ArchivoModel.Archivo.FileName.Replace(Path.GetExtension(ArchivoModel.Archivo.FileName), ".png");
                }
                else
                {
                    ArchivoModel.UrlArchivo = Guid.NewGuid().ToString() + Path.GetExtension(ArchivoModel.Archivo.FileName);
                    ArchivoModel.NombreArchivo = ArchivoModel.Archivo.FileName;
                }
                RespuestaAjax respuesta = Datos.VEHICULO_ac_Archivo(ArchivoModel, Conexion, User.Identity.Name, 1);

                if (respuesta.Success)
                {
                    if (Path.GetExtension(ArchivoModel.Archivo.FileName).ToLower() == ".heic")
                    {
                        Stream oStream = ArchivoModel.Archivo.InputStream;
                        Bitmap bmp = Auxiliar.ProcessFile(oStream);
                        bmp.Save(Server.MapPath("~/ArchivosVehiculo/" + ArchivoModel.UrlArchivo));
                    }
                    else
                    {
                        ArchivoModel.Archivo.SaveAs(Server.MapPath("~/ArchivosVehiculo/" + ArchivoModel.UrlArchivo));
                    }

                    TempData["typemessage"] = "1";
                }
                else
                {
                    TempData["typemessage"] = "2";
                }

                TempData["message"] = respuesta.Mensaje;

                return RedirectToAction("Archivos", "CatVehiculo", new { id = ArchivoModel.Id_vehiculo, nombreVehiculo = respuesta.Href });
            }
            catch (Exception ex)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Verifique sus datos.";
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult DescargarArchivo(string nombreArchivoServer, string nombreArchivo)
        {
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + "ArchivosVehiculo/";
                byte[] fileBytes = System.IO.File.ReadAllBytes(path + nombreArchivoServer);
                string fileName = nombreArchivo;
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult EliminarArchivo(string nombreArchivoServer, int? id)
        {
            try
            {
                RespuestaAjax respuesta = new RespuestaAjax();

                if ((string.IsNullOrEmpty(nombreArchivoServer.Trim())) || (id == null || id == 0))
                {
                    respuesta.Success = false;
                    respuesta.Mensaje = "Verifique sus datos";
                    return Content(respuesta.ToJSON(), "application/json");
                }

                //Borramos el archivo del servidor para no acumular basura
                string pathRoot = Server.MapPath("~/ArchivosVehiculo");
                string filePath = pathRoot + "\\" + nombreArchivoServer;

                if ((System.IO.File.Exists(filePath)))
                {
                    System.IO.File.Delete(filePath);
                    //Ponemos en activo 0 el archivo

                    _CatVehiculo_Datos Datos = new _CatVehiculo_Datos();
                    respuesta = Datos.VEHICULO_del_Archivo(Conexion, id.Value);

                    respuesta.Success = respuesta.Success;
                    respuesta.Mensaje = respuesta.Mensaje;
                    return Content(respuesta.ToJSON(), "application/json");
                }
                else
                {
                    respuesta.Success = false;
                    respuesta.Mensaje = "Verifique sus datos";
                    return Content(respuesta.ToJSON(), "application/json");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        // GET: Admin/CatVehiculo/Edit/5
        public ActionResult Edit(string id)
        {
            try
            {
                Token.SaveToken();
                CatVehiculoModels Vehiculo = new CatVehiculoModels();
                _CatVehiculo_Datos VehiculoDatos = new _CatVehiculo_Datos();
                Vehiculo.Conexion = Conexion;
                Vehiculo.IDVehiculo = id;
                Vehiculo = VehiculoDatos.ObtenerDetalleCatVehiculo(Vehiculo);
                Vehiculo.listaTipoVehiculos = VehiculoDatos.obtenerListaTipoVehiculo(Vehiculo);
                Vehiculo.listaSucursal = VehiculoDatos.obtenerListaSucursales(Vehiculo);
                Vehiculo.listaMarcas = VehiculoDatos.obtenerListaMarcas(Vehiculo);
                Vehiculo.ListaEmpresas = VehiculoDatos.obtenerListaEmpresas(Vehiculo);
                return View(Vehiculo);
            }
            catch (Exception ex)
            {
                CatVehiculoModels Vehiculo = new CatVehiculoModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Vehiculo);
            }
        }

        // POST: Admin/CatVehiculo/Edit/5
        [HttpPost]

        public ActionResult Edit(string id, CatVehiculoModels Vehiculo)
        {
            _CatVehiculo_Datos VehiculoDatos = new _CatVehiculo_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    ModelState.Remove("Img");
                    if (ModelState.IsValid)
                    {

                        HttpPostedFileBase bannerImage = Request.Files[0] as HttpPostedFileBase;
                        if (!string.IsNullOrEmpty(bannerImage.FileName))
                        {
                            if (bannerImage != null && bannerImage.ContentLength > 0)
                            {
                                Stream s = bannerImage.InputStream;
                                //Bitmap img = new Bitmap(s);
                                //Vehiculo.img64 = img.ToBase64String(ImageFormat.Png);

                                if (Path.GetExtension(bannerImage.FileName).ToLower() == ".heic")
                                {
                                    Image img = (Image)Auxiliar.ProcessFile(s);
                                    Bitmap image = new Bitmap(ComprimirImagen.VaryQualityLevel((Image)img.Clone(), 35L));
                                    Vehiculo.img64 = image.ToBase64String(ImageFormat.Jpeg);
                                }
                                else
                                {
                                    Image img = new Bitmap(s);
                                    Bitmap image = new Bitmap(ComprimirImagen.VaryQualityLevel((Image)img.Clone(), 35L));
                                    Vehiculo.img64 = image.ToBase64String(img.RawFormat);
                                }
                            }
                        }
                        else
                        {
                            Vehiculo.BandImg = true;
                        }
                        Vehiculo.Conexion = Conexion;
                        Vehiculo.Usuario = User.Identity.Name;
                        Vehiculo.Opcion = 2;
                        Vehiculo.IDVehiculo = id;

                        Vehiculo.Estatus = true;
                        Vehiculo = VehiculoDatos.AcCatVehiculo(Vehiculo);
                        if (Vehiculo.Completado == true)
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
                            return View(Vehiculo);
                        }
                    }
                    else
                    {
                        Vehiculo.Conexion = Conexion;
                        Vehiculo.listaTipoVehiculos = VehiculoDatos.obtenerListaTipoVehiculo(Vehiculo);
                        Vehiculo.listaSucursal = VehiculoDatos.obtenerListaSucursales(Vehiculo);
                        Vehiculo.listaMarcas = VehiculoDatos.obtenerListaMarcas(Vehiculo);
                        Vehiculo.ListaEmpresas = VehiculoDatos.obtenerListaEmpresas(Vehiculo);
                        return View(Vehiculo);

                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                Vehiculo.Conexion = Conexion;
                Vehiculo.listaTipoVehiculos = VehiculoDatos.obtenerListaTipoVehiculo(Vehiculo);
                Vehiculo.listaSucursal = VehiculoDatos.obtenerListaSucursales(Vehiculo);
                Vehiculo.listaMarcas = VehiculoDatos.obtenerListaMarcas(Vehiculo);
                Vehiculo.ListaEmpresas = VehiculoDatos.obtenerListaEmpresas(Vehiculo);
                TempData["typemessage"] = "2";
                TempData["message"] = "No se pudo guardar los datos. Por favor contacte a soporte técnico";
                return View(Vehiculo);
            }
        }

        // GET: Admin/CatVehiculo/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/CatVehiculo/Delete/5
        [HttpPost]
        public ActionResult Delete(string id)
        {
            try
            {
                CatVehiculoModels Vehiculo = new CatVehiculoModels();
                _CatVehiculo_Datos VehiculoDatos = new _CatVehiculo_Datos();
                Vehiculo.Conexion = Conexion;
                Vehiculo.IDVehiculo = id;
                Vehiculo.Usuario = User.Identity.Name;
                // TODO: Add delete logic here
                Vehiculo = VehiculoDatos.EliminarVehiculo(Vehiculo);
                //TempData["typemessage"] = "1";
                //TempData["message"] = "El registro se ha eliminado correctamente";
                return Json("");
            }
            catch
            {
                CatVehiculoModels Vehiculo = new CatVehiculoModels();

                return View();
            }
        }

        [HttpPost]
        public ActionResult ObtenerSucursalesXIDEmpresa(string IDEmpresa)
        {
            try
            {
                CatVehiculoModels Vehiculo = new CatVehiculoModels();
                _CatVehiculo_Datos VehiculoDatos = new _CatVehiculo_Datos();
                Vehiculo.Conexion = Conexion;
                Vehiculo.IDEmpresa = IDEmpresa;
                Vehiculo.Usuario = User.Identity.Name;
                Vehiculo.listaSucursal = VehiculoDatos.ObtenerSucursalesXIDEmpresa(Vehiculo);
                return Content(Vehiculo.listaSucursal.ToJSON(), "application/json");
            }
            catch
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error. Por favor contacte a soporte técnico";
                return Json("");
            }
        }
    }
}
