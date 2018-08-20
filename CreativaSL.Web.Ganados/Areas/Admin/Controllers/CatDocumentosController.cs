using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CreativaSL.Web.Ganados.Models;
using System.Configuration;
using CreativaSL.Web.Ganados.App_Start;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    public class CatDocumentosController : Controller
    {
        private TokenProcessor Token = TokenProcessor.GetInstance();
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
        // GET: Admin/CatEvento
        public ActionResult Index()
        {
            return View();
        }

        // GET: Admin/CatEvento/Create
        public ActionResult Create()
        {
            try
            {
                Token.SaveToken();
                return View();
            }
            catch (Exception)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("Index");
            }
        }

        // POST: Admin/CatEvento/Create
        [HttpPost]
        public ActionResult Create(CatTipoDocumentoModels Documento)
        {
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        Documento.Conexion = Conexion;
                        Documento.Usuario = User.Identity.Name;
                        _CatDocumentos_Datos Datos = new _CatDocumentos_Datos();
                        Documento.RespuestaAjax = new RespuestaAjax();
                        Documento.RespuestaAjax = Datos.AC_Documentos(Documento);

                        if (Documento.RespuestaAjax.Success == true)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = Documento.RespuestaAjax.Mensaje;
                            Token.ResetToken();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            TempData["typemessage"] = "2";
                            TempData["message"] = Documento.RespuestaAjax.Mensaje;
                            return View(Documento);
                        }
                    }
                    else
                    {
                        TempData["typemessage"] = "2";
                        TempData["message"] = "Revise su formulario.";
                        return View(Documento);
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrió un error al intentar guardar los datos. Contacte a soporte técnico. Error: " + Mensaje;
                return View(Documento);
            }
        }

        // GET: Admin/CatEvento/Edit/5
        public ActionResult Edit(int IDTipoDocumento)
        {
            try
            {
                CatTipoDocumentoModels Documento = new CatTipoDocumentoModels();
                Documento.Conexion = Conexion;
                Documento.Usuario = User.Identity.Name;
                _CatDocumentos_Datos Datos = new _CatDocumentos_Datos();
                Documento.IDTipoDocumento = IDTipoDocumento;

                Documento = Datos.GET_DocumentoXID(Documento);
                if (Documento.RespuestaAjax.Success)
                {
                    Token.SaveToken();
                    return View(Documento);
                }
                else
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = Documento.RespuestaAjax.Mensaje;
                    return View("Index");
                }
            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrió un error al intentar desplegar los datos. Contacte a soporte técnico. Error: " + Mensaje;
                return View("Index");
            }
        }

        // POST: Admin/CatEvento/Edit/5
        [HttpPost]
        public ActionResult Edit(CatTipoDocumentoModels Documento)
        {
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        Documento.Conexion = Conexion;
                        Documento.Usuario = User.Identity.Name;
                        _CatDocumentos_Datos Datos = new _CatDocumentos_Datos();
                        Documento.RespuestaAjax = new RespuestaAjax();
                        Documento.RespuestaAjax = Datos.AC_Documentos(Documento);

                        if (Documento.RespuestaAjax.Success == true)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = Documento.RespuestaAjax.Mensaje;
                            Token.ResetToken();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            TempData["typemessage"] = "2";
                            TempData["message"] = Documento.RespuestaAjax.Mensaje;
                            return View(Documento);
                        }
                    }
                    else
                    {
                        TempData["typemessage"] = "2";
                        TempData["message"] = "Revise su formulario.";
                        return View(Documento);
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrió un error al intentar guardar los datos. Contacte a soporte técnico. Error: " + Mensaje;
                return View(Documento);
            }
        }

        // POST: Admin/CatEvento/Delete/5
        [HttpGet]
        public ActionResult Delete(int IDTipoDocumento)
        {
            try
            {
                CatTipoDocumentoModels Documento = new CatTipoDocumentoModels();
                Documento.Conexion = Conexion;
                Documento.Usuario = User.Identity.Name;
                _CatDocumentos_Datos Datos = new _CatDocumentos_Datos();
                Documento.IDTipoDocumento = IDTipoDocumento;

                Documento = Datos.DEL_DocumentoXID(Documento);
                if (Documento.RespuestaAjax.Success)
                {
                    TempData["typemessage"] = "1";
                }
                else
                {
                    TempData["typemessage"] = "2";
                }
                TempData["message"] = Documento.RespuestaAjax.Mensaje;
                return View("Index");
            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                CatTipoDocumentoModels Documento = new CatTipoDocumentoModels();
                Documento.RespuestaAjax = new RespuestaAjax();
                Documento.RespuestaAjax.Success = false;
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrió un error al intentar desplegar los datos. Contacte a soporte técnico. Error: " + Mensaje;

                return View("Index");
            }
        }

        [HttpPost]
        public ActionResult DatatableIndex()
        {
            try
            {
                _CatDocumentos_Datos Datos = new _CatDocumentos_Datos();
                CatTipoDocumentoModels Documento = new CatTipoDocumentoModels();

                Documento.Conexion = Conexion;
                Documento.RespuestaAjax = new RespuestaAjax();

                Documento.RespuestaAjax.Mensaje = Datos.DatatableIndex(Documento);
                Documento.RespuestaAjax.Success = true;

                return Content(Documento.RespuestaAjax.Mensaje, "application/json");

            }
            catch (Exception ex)
            {
                CatTipoDocumentoModels Documento = new CatTipoDocumentoModels();
                Documento.RespuestaAjax = new RespuestaAjax();
                TempData["message"] = ex.Message;
                TempData["typemessage"] = "2";
                Documento.RespuestaAjax.Success = false;
                return Content(Documento.RespuestaAjax.ToJSON(), "application/json");
            }
        }
    }
}
