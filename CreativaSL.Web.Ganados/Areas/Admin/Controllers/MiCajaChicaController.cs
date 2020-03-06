using CreativaSL.Web.Ganados.App_Start;
using CreativaSL.Web.Ganados.Models;
using CreativaSL.Web.Ganados.Models.Helpers;
using CreativaSL.Web.Ganados.Models.System;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    public class MiCajaChicaController : Controller
    {
        private TokenProcessor Token = TokenProcessor.GetInstance();
     //   string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
        // GET: Admin/MiCajaChica
        public ActionResult Index()
        {
            try
            {
                _CajaChica_Datos regionDatos = new _CajaChica_Datos();
                string idCaja = regionDatos.ObtenerIdCajaChica(User.Identity.Name);
                //idCaja = 4;
                if (string.IsNullOrEmpty(idCaja) || idCaja.Trim() != "0")
                {
                    List<MovimientosCajaChicaModels> Lista = regionDatos.ObtenerDetalleXIdCaja(idCaja);
                    ViewBag.IdCaja = idCaja;
                    return View(Lista);
                }
                else
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "El usuario no tiene caja activa.";
                    return RedirectToAction("Index", "HomeAdmin");
                }
            }
            catch (Exception)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la información";
                return RedirectToAction("Index", "HomeAdmin");
            }
        }


        // GET: Admin/MiCajaChica/Create
        public ActionResult Create(Int64 id)
        {
            try
            {
                Token.SaveToken();
                _CajaChica_Datos datos = new _CajaChica_Datos();
                ViewBag.ListaConceptos = datos.LlenarComboConceptos(false);
                ViewBag.ListaFormasPago = datos.LlenarComboFormaPagos(false);
                return View(new MovimientosCajaChicaModels { IdCaja = id });
            }
            catch (Exception)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                //ViewBag.ListaConceptos = new List<ConceptosCajaChicaModels>();
                //ViewBag.ListaFormasPago = new List<FormaPagoCajaChicaModels>();
                //return View(new MovimientosCajaChicaModels { IdCaja = id });
                return RedirectToAction("Index");
            }
        }

        // POST: Admin/CajaChica/Create
        [HttpPost]
        public ActionResult Create(MovimientosCajaChicaModels model)
        {
            _CajaChica_Datos datos = new _CajaChica_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {

                        var chequeComprobantePostedFileBase = Request.Files["FotoCheque"] as HttpPostedFileBase;
                        var fileName = Guid.NewGuid().ToString().ToUpper();

                        if (chequeComprobantePostedFileBase != null && chequeComprobantePostedFileBase.ContentLength > 0)
                        {
                            var uploadImageChequeComprobanteToserver = new UploadFileToServerModel();
                            uploadImageChequeComprobanteToserver.FileBase = chequeComprobantePostedFileBase;
                            uploadImageChequeComprobanteToserver.BaseDir = ProjectSettings.BaseDirCajaChicaChequeComprobante;
                            uploadImageChequeComprobanteToserver.FileName = fileName;
                            CidFaresHelper.UploadFileToServer(uploadImageChequeComprobanteToserver);
                            model.FotoCheque = uploadImageChequeComprobanteToserver.FileName;

                            if (!uploadImageChequeComprobanteToserver.Success)
                            {
                                CidFaresHelper.DeleteFilesWithOutExtensionFromServer(uploadImageChequeComprobanteToserver);
                                TempData["typemessage"] = "2";
                                TempData["message"] =
                                    "Ha ocurrido un error al guardar el comprobante o cheque, intentlo más tarde o contacte con sopoerte técnico.";
                                ViewBag.ListaConceptos = datos.LlenarComboConceptos(false);
                                ViewBag.ListaFormasPago = datos.LlenarComboFormaPagos(false);

                                return View(model);
                            }
                        }
                        
                        var Resultado = datos.GuardarMovimiento(model, User.Identity.Name);
                        if (Resultado == 1)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "Datos guardados correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            string mensajeError = "Ocurrió un error al intentar guardar los datos. Intente más tarde.";
                            TempData["typemessage"] = "2";
                            TempData["message"] = mensajeError;
                            ViewBag.ListaConceptos = datos.LlenarComboConceptos(false);
                            ViewBag.ListaFormasPago = datos.LlenarComboFormaPagos(false);
                            return View(model);
                        }
                    }
                    else
                    {
                        ViewBag.ListaConceptos = datos.LlenarComboConceptos(false);
                        ViewBag.ListaFormasPago = datos.LlenarComboFormaPagos(false);
                        return View(model);
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Error al procesar los datos";
                ViewBag.ListaConceptos = datos.LlenarComboConceptos(false);
                ViewBag.ListaFormasPago = datos.LlenarComboFormaPagos(false);
                return View(model);
            }
        }

        // GET: Admin/MiCajaChica/Edit
        public ActionResult Edit(string id)
        {
            try
            {
                Token.SaveToken();
                var datos = new _CajaChica_Datos();
                var model = datos.ObtenerDetalleMovimientoXId(id);

                var uploadBase64ToServerModel = CidFaresHelper.UploadBase64ToServer(model.FotoCheque,
                    ProjectSettings.BaseDirCajaChicaChequeComprobante);
                //Si hizo una conversion y subio el archivo al server, por lo que hay que guardar el nombre del archivo al registro
                if (uploadBase64ToServerModel.Success)
                {
                    var responseDb = datos.ActualizarFotoComprobate(model.IdMovimiento, uploadBase64ToServerModel.FileName);
                    if (!responseDb.Success)
                    {
                        TempData["typemessage"] = "2";
                        TempData["message"] = responseDb.Mensaje;
                        return RedirectToAction("Index");
                    }

                    model.FotoCheque = uploadBase64ToServerModel.UrlRelative;
                }

                ViewBag.ListaConceptos = datos.LlenarComboConceptos(false);
                ViewBag.ListaFormasPago = datos.LlenarComboFormaPagos(false);
                return View(model);
            }
            catch (Exception)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("Index");
            }
        }

        // POST: Admin/CajaChica/Create
        [HttpPost]
        public ActionResult Edit(MovimientosCajaChicaModels model)
        {
            _CajaChica_Datos datos = new _CajaChica_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        HttpPostedFileBase bannerImage = Request.Files[0] as HttpPostedFileBase;
                        if (!string.IsNullOrEmpty(bannerImage.FileName))
                        {
                            if (bannerImage != null && bannerImage.ContentLength > 0)
                            {
                                Stream s = bannerImage.InputStream;
                                if (Path.GetExtension(bannerImage.FileName).ToLower() == ".heic")
                                {
                                    Image img = (Image)Auxiliar.ProcessFile(s);
                                    Bitmap image = new Bitmap(ComprimirImagen.VaryQualityLevel((Image)img.Clone(), 35L));
                                    model.FotoCheque = image.ToBase64String(ImageFormat.Jpeg);
                                }
                                else
                                {
                                    Image img = new Bitmap(s);
                                    Bitmap IMG3 = ComprimirImagen.SaveJpeg("", img, 50, false);
                                    model.FotoCheque = IMG3.ToBase64String(img.RawFormat);
                                }
                            }
                        }
                        else
                        {
                            model.Estatus = true;
                        }
                        int Resultado = datos.ModificarMovimiento(model, User.Identity.Name);
                        if (Resultado == 1)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "Datos guardados correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            string mensajeError = "Ocurrió un error al intentar guardar los datos. Intente más tarde.";
                            TempData["typemessage"] = "2";
                            TempData["message"] = mensajeError;
                            ViewBag.ListaConceptos = datos.LlenarComboConceptos(false);
                            ViewBag.ListaFormasPago = datos.LlenarComboFormaPagos(false);
                            return View(model);
                        }
                    }
                    else
                    {
                        ViewBag.ListaConceptos = datos.LlenarComboConceptos(false);
                        ViewBag.ListaFormasPago = datos.LlenarComboFormaPagos(false);
                        return View(model);
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Error al procesar los datos";
                ViewBag.ListaConceptos = datos.LlenarComboConceptos(false);
                ViewBag.ListaFormasPago = datos.LlenarComboFormaPagos(false);
                return View(model);
            }
        }

        // GET: Admin/MiCajaChica
        public ActionResult Entrega()
        {
            return View();
        }

        // POST: Admin/MiCajaChica/Delete
        [HttpPost]
        public ActionResult Delete(string id)
        {
            _CajaChica_Datos datos = new _CajaChica_Datos();
            try
            {
                int Resultado = datos.EliminarMovimiento(id, User.Identity.Name);
                if (Resultado == 1)
                {

                    var cajaChica = new CajaChicaModels();
                    cajaChica.IdCaja = id;

                    datos.ObtenerImagenCajaChica(cajaChica);
                    var uploadFileToServer = new UploadFileToServerModel();
                    uploadFileToServer.BaseDir = ProjectSettings.BaseDirCajaChicaChequeComprobante;
                    uploadFileToServer.FileName =
                        cajaChica.ImagenCajaChica.Replace(ProjectSettings.BaseDirCajaChicaChequeComprobante,
                            string.Empty);

                    CidFaresHelper.DeleteFileromServer(uploadFileToServer);

                    return Json("true");
                }
                return Json("false");
            }
            catch
            {
                return Json("false");
            }
        }

        //POST: Admin/MiCajaChica/estatusCheque/3
        [HttpPost]
        public ActionResult EstatusCheque(Int64 id)
        {
            _CajaChica_Datos datos = new _CajaChica_Datos();
            try
            {
                int Resultado = datos.EstatusChequeCobrado(id, User.Identity.Name);
                if (Resultado == 1)
                {
                    return Json("true");
                }
                return Json("false");
            }
            catch
            {
                return Json("false");
            }
        }
        public ActionResult ModalTicket2(string ID)
        {
            var Imagen = new CajaChicaModels();
            Imagen.IdCaja = ID;

            var datos = new _CajaChica_Datos();
            datos.ObtenerImagenCajaChica(Imagen);

            var uploadBase64ToServerModel = CidFaresHelper.UploadBase64ToServer(Imagen.ImagenCajaChica,
                ProjectSettings.BaseDirCajaChicaChequeComprobante);

            if (uploadBase64ToServerModel.Success)
            {
                var responseDb = datos.ActualizarFotoComprobate(Imagen.IdCaja, uploadBase64ToServerModel.FileName);
                Imagen.ImagenCajaChica = uploadBase64ToServerModel.UrlRelative;
            }
            
            return PartialView("ModalTicket2", Imagen);
        }

    }
}
