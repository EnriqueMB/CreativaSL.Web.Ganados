using CreativaSL.Web.Ganados.Filters;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CreativaSL.Web.Ganados.Models;
using System.Data;
using System.Globalization;
using CreativaSL.Web.Ganados.App_Start;
using System.IO;
using System.Drawing;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    [Autorizado]
    public class CatChoferController : Controller
    {
        private TokenProcessor Token = TokenProcessor.GetInstance();
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
        // GET: Admin/CatChofer
        public ActionResult Index()
        {
            try
            {
                CatChoferModels Chofer = new CatChoferModels();
                CatChofer_Datos ChoferDatos = new CatChofer_Datos();
                Chofer.Conexion = Conexion;
                Chofer.ListaChoferes = ChoferDatos.ObtenerCatChofer(Chofer);
                return View(Chofer);
            }
            catch (Exception)
            {
                CatChoferModels Chofer = new CatChoferModels();
                Chofer.TablaDatos = new DataTable();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Chofer);
            }
        }

        // GET: Admin/CatChofer/Create
        public ActionResult Create()
        {
            try
            {
                Token.SaveToken();
                CatChoferModels Chofer = new CatChoferModels();
                CatChofer_Datos ChoferDatos = new CatChofer_Datos();
                Chofer.Conexion = Conexion;
                Chofer.ListaGeneroCMB = ChoferDatos.ObteneComboCatGenero(Chofer);
                Chofer.listaGrupoSanguineo = ChoferDatos.ObteneComboCatGrupoSanguineo(Chofer);
                Chofer.listaSucursales = ChoferDatos.ObteneComboCatSucursal(Chofer);
                Chofer.Licencia = Convert.ToBoolean("true");
                Chofer.ListaEmpresas = ChoferDatos.ObteneComboCatEmpresa(Chofer);
                return View(Chofer);
            }
            catch (Exception)
            {
                CatChoferModels Chofer = new CatChoferModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Chofer);
            }            
        }

        // POST: Admin/CatChofer/Create
        [HttpPost]
      
        public ActionResult Create(CatChoferModels Chofer)
        {
            CatChofer_Datos ChoferDatos = new CatChofer_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        Chofer.IDChofer = "-";
                        Chofer.Conexion = Conexion;
                        Chofer.Usuario = User.Identity.Name;
                        Chofer.Opcion = 1;
                        Chofer = ChoferDatos.AbcCatChofer(Chofer);

                        //Si abc fue completado correctamente
                        if (Chofer.Completado == true)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "El registro se guardo correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            Chofer.Licencia = true;
                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrió un error al guardar el registro.";
                            return View(Chofer);
                        }
                    }
                    else
                    {
                        Chofer.Licencia = true;
                        Chofer.listaGrupoSanguineo = ChoferDatos.ObteneComboCatGrupoSanguineo(Chofer);
                        Chofer.ListaGeneroCMB = ChoferDatos.ObteneComboCatGenero(Chofer);
                        Chofer.listaSucursales = ChoferDatos.ObteneComboCatSucursal(Chofer);
                        return View(Chofer);
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                Chofer.TablaDatos = new DataTable();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se pudo guardar los datos. Por favor contacte a soporte técnico";
                return View(Chofer);
            }
        }
        //==================================================================================

        [HttpGet]
        public ActionResult Archivos(string id, string nombreChofer)
        {
            try
            {
                if (string.IsNullOrEmpty(id.Trim()) || string.IsNullOrEmpty(nombreChofer))
                {
                    return RedirectToAction("Index");
                }

                ViewBag.NombreChofer = nombreChofer;
                ViewBag.Id_chofer = id;

                return View();
            }
            catch (Exception)
            {
                CatChoferModels Chofer = new CatChoferModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult LoadTableArchivos(string id_chofer)
        {
            try
            {
                CatChofer_Datos Datos = new CatChofer_Datos();
                string datatable = Datos.CHOFER_index_Archivo(Conexion, id_chofer);

                return Content(datatable, "application/json");
            }
            catch (Exception ex)
            {
                return Content("", "application/json");
            }
        }

        [HttpGet]
        public ActionResult AgregarArchivo(string id_chofer)
        {
            try
            {
                if (string.IsNullOrEmpty(id_chofer.Trim()))
                {
                    return RedirectToAction("Index");
                }

                ArchivoChoferModels Archivo = new ArchivoChoferModels();

                Archivo.Id_chofer = id_chofer;

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
        public ActionResult AgregarArchivo(ArchivoChoferModels ArchivoModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(ArchivoModel);

                }

                CatChofer_Datos Datos = new CatChofer_Datos();

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
                RespuestaAjax respuesta = Datos.CHOFER_ac_Archivo(ArchivoModel, Conexion, User.Identity.Name, 1);

                if (respuesta.Success)
                {
                    if (Path.GetExtension(ArchivoModel.Archivo.FileName).ToLower() == ".heic")
                    {
                        Stream oStream = ArchivoModel.Archivo.InputStream;
                        Bitmap bmp = Auxiliar.ProcessFile(oStream);
                        bmp.Save(Server.MapPath("~/ArchivosChofer/" + ArchivoModel.UrlArchivo));
                    }
                    else
                    {
                        ArchivoModel.Archivo.SaveAs(Server.MapPath("~/ArchivosChofer/" + ArchivoModel.UrlArchivo));
                    }

                    TempData["typemessage"] = "1";
                }
                else
                {
                    TempData["typemessage"] = "2";
                }

                TempData["message"] = respuesta.Mensaje;

                return RedirectToAction("Archivos", "CatChofer", new { id = ArchivoModel.Id_chofer, nombreChofer = respuesta.Href });
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
                string path = AppDomain.CurrentDomain.BaseDirectory + "ArchivosChofer/";
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
                string pathRoot = Server.MapPath("~/ArchivosChofer");
                string filePath = pathRoot + "\\" + nombreArchivoServer;

                if ((System.IO.File.Exists(filePath)))
                {
                    System.IO.File.Delete(filePath);
                    //Ponemos en activo 0 el archivo

                    CatChofer_Datos Datos = new CatChofer_Datos();
                    respuesta = Datos.CHOFER_del_Archivo(Conexion, id.Value);

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

        // GET: Admin/CatChofer/Edit/5
        //======================== EDITAR ===========================
        //Obtiene el detalle del registro del chofer para ser editado
        //===========================================================
        public ActionResult Edit(string id)
        {
            try
            {
                Token.SaveToken();
                CatChoferModels Chofer = new CatChoferModels();
                CatChofer_Datos ChoferDatos = new CatChofer_Datos();
                Chofer.IDChofer = id;
                Chofer.Conexion = Conexion;
                Chofer = ChoferDatos.ObtenerDetalleCatChofer(Chofer);
                Chofer.ListaGeneroCMB = ChoferDatos.ObteneComboCatGenero(Chofer);
                Chofer.listaSucursales = ChoferDatos.ObteneComboCatSucursal(Chofer);
                Chofer.listaGrupoSanguineo = ChoferDatos.ObteneComboCatGrupoSanguineo(Chofer);
                Chofer.ListaEmpresas = ChoferDatos.ObteneComboCatEmpresa(Chofer);
                return View(Chofer);
            }
            catch (Exception)
            {
                CatChoferModels Chofer = new CatChoferModels();
                Chofer.TablaDatos = new DataTable();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Chofer);
            }
        }

        // POST: Admin/CatChofer/Edit/5
        //Obtiene los datos del registro del chofer para ser editado
        [HttpPost]
        public ActionResult Edit(string id, CatChoferModels Chofer)
        {
            CatChofer_Datos ChoferDatos = new CatChofer_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        Chofer.Conexion = Conexion;
                        Chofer.IDChofer = id;
                        Chofer.Usuario = User.Identity.Name;
                        Chofer.Opcion = 2;
                        Chofer = ChoferDatos.AbcCatChofer(Chofer);
                        //Si abc fue completado correctamente
                        if (Chofer.Completado == true)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "El registro se guardo correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            Chofer.Licencia = true;
                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrió un error al guardar el registro.";
                            return View(Chofer);
                        }
                    }
                    else
                    {
                        Chofer.Conexion = Conexion;
                        Chofer.listaGrupoSanguineo = ChoferDatos.ObteneComboCatGrupoSanguineo(Chofer);
                        Chofer.ListaGeneroCMB = ChoferDatos.ObteneComboCatGenero(Chofer);
                        Chofer.listaSucursales = ChoferDatos.ObteneComboCatSucursal(Chofer);
                        return View(Chofer);
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                Chofer.Conexion = Conexion;
                Chofer.listaGrupoSanguineo = ChoferDatos.ObteneComboCatGrupoSanguineo(Chofer);
                Chofer.ListaGeneroCMB = ChoferDatos.ObteneComboCatGenero(Chofer);
                Chofer.listaSucursales = ChoferDatos.ObteneComboCatSucursal(Chofer);
                TempData["typemessage"] = "2";
                TempData["message"] = "No se pudo guardar los datos. Por favor contacte a soporte técnico";
                return View(Chofer);
            }
        }
        //==============================ELIMINAR CHOFER =================================
        // GET: Admin/CatChofer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/CatChofer/Delete/5
        //recibe el id del chofer para ser eliminado
        [HttpPost]
        public ActionResult Delete(string id)
        {
            try
            {
                CatChoferModels Chofer = new CatChoferModels();
                CatChofer_Datos ChoferDatos = new CatChofer_Datos();
                Chofer.Conexion = Conexion;
                Chofer.IDChofer = id;
                Chofer.Usuario = User.Identity.Name;
                Chofer = ChoferDatos.EliminarChofer(Chofer);
                //TempData["typemessage"] = "1";
                //TempData["message"] = "El registro se ha eliminado correctamente";
                return Json("");
            }
            catch
            {
                CatChoferModels Chofer = new CatChoferModels();
                Chofer.TablaDatos = new DataTable();
                //TempData["typemessage"] = "2";
                //TempData["message"] = "No se pudo borrar los datos. Por favor contacte a soporte técnico";
                return Json("");
               
            }
        }

        [HttpPost]
        public ActionResult ObtenerSucursalesXIDEmpresa(string IDEmpresa)
        {
            try
            {
                CatChoferModels Chofer = new CatChoferModels();
                CatChofer_Datos ChoferDatos = new CatChofer_Datos();
                Chofer.Conexion = Conexion;
                Chofer.IDEmpresa = IDEmpresa;
                Chofer.Usuario = User.Identity.Name;
                Chofer.listaSucursales = ChoferDatos.ObtenerSucursalesXIDEmpresa(Chofer);
                return Content(Chofer.listaSucursales.ToJSON(), "application/json");                
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
