﻿using CreativaSL.Web.Ganados.App_Start;
using CreativaSL.Web.Ganados.Models;
using CreativaSL.Web.Ganados.ViewModels;
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
    public class EntregaCombustibleController : Controller
    {
        private TokenProcessor Token = TokenProcessor.GetInstance();
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
        // GET: Admin/EntregaCombustible
        public ActionResult Index()
        {
            try
            {
                EntregaCombustibleModels Entregas = new EntregaCombustibleModels();
                _EntregaCombustible_Datos Datos = new _EntregaCombustible_Datos();
                _Combos_Datos Combo = new _Combos_Datos();
                Entregas.ListaSucursales = Combo.ObtenerComboSucursales(Conexion);
                Entregas.ListaVehiculos = Combo.ObtenerComboVehiculosPrincp(Conexion);
                Entregas.BandFechaEntrega = Convert.ToBoolean("false");
                Entregas.BandIDSucursal = Convert.ToBoolean("false");
                Entregas.BandIDVehiuculo = Convert.ToBoolean("false");
                Entregas.Conexion = Conexion;
                Entregas.listaEntregaCombustible = Datos.ObtenerEntregasCombustible(Entregas);
                return View(Entregas);
            }
            catch (Exception)
            {
                EntregaCombustibleModels Entregas = new EntregaCombustibleModels();
                _Combos_Datos Combo = new _Combos_Datos();
                Entregas.ListaSucursales = Combo.ObtenerComboSucursales(Conexion);
                Entregas.ListaVehiculos = Combo.ObtenerComboVehiculosPrincp(Conexion);
                Entregas.listaEntregaCombustible = new List<EntregaCombustibleModels>();

                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Entregas);
            }
        }

        [HttpPost]
        public ActionResult Index(EntregaCombustibleModels Entrega)
        {
            try
            {
               
                _EntregaCombustible_Datos Datos = new _EntregaCombustible_Datos();
                _Combos_Datos Combo = new _Combos_Datos();
                Entrega.ListaSucursales = Combo.ObtenerComboSucursales(Conexion);
                Entrega.ListaVehiculos = Combo.ObtenerComboVehiculosPrincp(Conexion);
                if (!Entrega.BandIDVehiuculo)
                {
                    Entrega.IDVehiculo = string.Empty ;
                }
                 if (!Entrega.BandIDSucursal) {
                    Entrega.IDSucursal = string.Empty;
                }
                if (!Entrega.BandFechaEntrega) {
                    Entrega.Fecha = DateTime.MinValue;
                }
               
                Entrega.Conexion = Conexion;
                Entrega.listaEntregaCombustible = Datos.ObtenerEntregasCombustible(Entrega);
                return View(Entrega);
            }
            catch (Exception)
            {
                EntregaCombustibleModels Entregas = new EntregaCombustibleModels();
                _Combos_Datos Combo = new _Combos_Datos();
                Entregas.ListaSucursales = Combo.ObtenerComboSucursales(Conexion);
                Entregas.ListaVehiculos = Combo.ObtenerComboVehiculosPrincp(Conexion);
                Entregas.listaEntregaCombustible = new List<EntregaCombustibleModels>();

                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Entregas);
            }
        }
        // GET: Admin/EntregaCombustible/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/EntregaCombustible/Create
        public ActionResult Create()
        {
            try
            {
                Token.SaveToken();
                EntregaCombustibleViewModels Entrega = new EntregaCombustibleViewModels();
                _Combos_Datos Datos = new _Combos_Datos();
                Entrega.ListaSucursales = Datos.ObtenerComboSucursales(Conexion);
                Entrega.ListaVehiculos = Datos.ObtenerComboVehiculos(Conexion, string.Empty);
                Entrega.ListaTipoCombustible = Datos.ObtenerComboTiposCombustible(Conexion);
                Entrega.ListaProveedores = Datos.ObtenerComboProveedoresCombustible(Conexion, string.Empty);
              //  Entrega.listaChofer = new List<EntregaCombustibleModels>();
                Entrega.listaChofer = Datos.obtenerChofer(Conexion,string.Empty);
                
                return View(Entrega);
            }
            catch (Exception)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("Index");
            }
        }

        // POST: Admin/EntregaCombustible/Create
        [HttpPost]
        public ActionResult Create(EntregaCombustibleViewModels Model)
        {
            _EntregaCombustible_Datos EntregaCombustibleDatos = new _EntregaCombustible_Datos();
            _Combos_Datos Datos = new _Combos_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    ModelState.Remove("ImgTicket");
                    if (ModelState.IsValid)
                    {
                        EntregaCombustibleModels Entrega = Model.ObtenerModeloPersistencia();
                        HttpPostedFileBase bannerImage = Request.Files[0] as HttpPostedFileBase;
                        if (bannerImage != null && bannerImage.ContentLength > 0)
                        {
                            //Stream s = bannerImage.InputStream;
                            //Bitmap img = new Bitmap(s);
                            //Entrega.UrlImagen64 = img.ToBase64String(ImageFormat.Png);

                            Stream s = bannerImage.InputStream;

                            if (Path.GetExtension(bannerImage.FileName).ToLower() == ".heic")
                            {
                                Image img = (Image)Auxiliar.ProcessFile(s);
                                Bitmap image = new Bitmap(ComprimirImagen.VaryQualityLevel((Image)img.Clone(), 35L));
                                Entrega.UrlImagen64 = image.ToBase64String(ImageFormat.Jpeg);
                            }
                            else
                            {
                                Image img = new Bitmap(s);
                                Bitmap image = new Bitmap(ComprimirImagen.VaryQualityLevel((Image)img.Clone(), 35L));
                                Entrega.UrlImagen64 = image.ToBase64String(img.RawFormat);
                            }

                        }
                        else
                        {
                            ModelState.AddModelError("ImgTicket", "Debe seleccionar una imagen en formato png.");
                            Model.ListaSucursales = Datos.ObtenerComboSucursales(Conexion);
                            Model.ListaVehiculos = Datos.ObtenerComboVehiculos(Conexion, Model.IDSucursal);
                            Model.ListaTipoCombustible = Datos.ObtenerComboTiposCombustible(Conexion);
                            Model.ListaProveedores = Datos.ObtenerComboProveedoresCombustible(Conexion, Model.IDSucursal);
                            Model.listaChofer = Datos.obtenerChofer(Conexion, Model.IDSucursal);//--------------------------------
                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrió un error al guardar el registro.";
                            return View(Model);
                        }
                        Entrega.Conexion = Conexion;
                        Entrega.NuevoRegistro = true;
                        Entrega.Precio = Entrega.Total / Entrega.Litros;
                        Entrega.Usuario = User.Identity.Name;
                        Entrega = EntregaCombustibleDatos.AcEntregaCombustible(Entrega);
                        if (Entrega.Completado == true)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "El registro se guardo correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            Model.ListaSucursales = Datos.ObtenerComboSucursales(Conexion);
                            Model.ListaVehiculos = Datos.ObtenerComboVehiculos(Conexion, Model.IDSucursal);
                            Model.ListaTipoCombustible = Datos.ObtenerComboTiposCombustible(Conexion);
                            Model.ListaProveedores = Datos.ObtenerComboProveedoresCombustible(Conexion, Model.IDSucursal);
                            Model.listaChofer = Datos.obtenerChofer(Conexion, Model.IDSucursal);
                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrió un error al guardar el registro.";
                            return View(Model);
                        }
                    }
                    else
                    {
                        
                        Model.ListaSucursales = Datos.ObtenerComboSucursales(Conexion);
                        Model.ListaVehiculos = Datos.ObtenerComboVehiculos(Conexion, Model.IDSucursal);
                        Model.ListaTipoCombustible = Datos.ObtenerComboTiposCombustible(Conexion);
                        Model.ListaProveedores = Datos.ObtenerComboProveedoresCombustible(Conexion, Model.IDSucursal);
                        Model.listaChofer = Datos.obtenerChofer(Conexion, Model.IDSucursal);
                        return View(Model);
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                
                Model.ListaSucursales = Datos.ObtenerComboSucursales(Conexion);
                Model.ListaVehiculos = Datos.ObtenerComboVehiculos(Conexion, Model.IDSucursal);
                Model.ListaTipoCombustible = Datos.ObtenerComboTiposCombustible(Conexion);
                Model.ListaProveedores = Datos.ObtenerComboProveedoresCombustible(Conexion, Model.IDSucursal);
                Model.listaChofer = Datos.obtenerChofer(Conexion, Model.IDSucursal);
                TempData["typemessage"] = "2";
                TempData["message"] = "No se pudo guardar los datos. Por favor contacte a soporte técnico";
                return View(Model);
            }
        }

        // GET: Admin/EntregaCombustible/Edit/5
        public ActionResult Edit(string id)
        {
            try
            {
                Token.SaveToken();
                EntregaCombustibleModels Entrega = new EntregaCombustibleModels();
                _EntregaCombustible_Datos EntregaCombustibleDatos = new _EntregaCombustible_Datos();
                _Combos_Datos Datos = new _Combos_Datos();
               
                Entrega.IDEntregaCombustible = id;
                Entrega.Conexion = Conexion;
                Entrega = EntregaCombustibleDatos.ObtenerDetalleEntregaCombustible(Entrega);

                EntregaCombustibleViewModels Model = new EntregaCombustibleViewModels
                {
                    IDEntregaCombustible = Entrega.IDEntregaCombustible,
                    IDSucursal = Entrega.IDSucursal,
                    IDProveedor = Entrega.IDProveedor,
                    IDVehiculo = Entrega.IDVehiculo,
                    IDTipoCombustible = Entrega.IDTipoCombustible,
                    Fecha = Entrega.Fecha,
                    NoTicket = Entrega.NoTicket,
                    KMInicial = Entrega.KMInicial,
                    Litros = Entrega.Litros,
                    Total = Entrega.Total,
                    UrlImagen64 = Entrega.UrlImagen64,
                    IDChofer = Entrega.IDChofer
                };
                Model.ImgTicketBand = true;
                Model.ListaSucursales = Datos.ObtenerComboSucursales(Conexion);
                Model.ListaVehiculos = Datos.ObtenerComboVehiculos(Conexion, Model.IDSucursal);
                Model.ListaTipoCombustible = Datos.ObtenerComboTiposCombustible(Conexion);
                Model.ListaProveedores = Datos.ObtenerComboProveedoresCombustible(Conexion, Model.IDSucursal);
                Model.listaChofer = Datos.obtenerChofer(Conexion, Model.IDSucursal);
                return View(Model);
            }
            catch (Exception)
            {

                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("Index");
            }
        }

        // POST: Admin/EntregaCombustible/Edit/5
        [HttpPost]
        public ActionResult Edit(EntregaCombustibleViewModels Model)
        {
            _EntregaCombustible_Datos EntregaCombustibleDatos = new _EntregaCombustible_Datos();
            _Combos_Datos Datos = new _Combos_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    // TODO: Add insert logic here
                    ModelState.Remove("ImgTicket");
                    if (ModelState.IsValid)
                    {
                        EntregaCombustibleModels Entrega = Model.ObtenerModeloPersistencia();
                        HttpPostedFileBase bannerImage = Request.Files[0] as HttpPostedFileBase;
                        if (!string.IsNullOrEmpty(bannerImage.FileName))
                        {
                            if (bannerImage != null && bannerImage.ContentLength > 0)
                            {
                                //Stream s = bannerImage.InputStream;
                                //Bitmap img = new Bitmap(s);
                                //Entrega.UrlImagen64 = img.ToBase64String(ImageFormat.Png);

                                Stream s = bannerImage.InputStream;
                                
                                if (Path.GetExtension(bannerImage.FileName).ToLower() == ".heic")
                                {
                                    Image img = (Image)Auxiliar.ProcessFile(s);
                                    Bitmap image = new Bitmap(ComprimirImagen.VaryQualityLevel((Image)img.Clone(), 35L));
                                    Entrega.UrlImagen64 = image.ToBase64String(ImageFormat.Jpeg);
                                }
                                else
                                {
                                    Image img = new Bitmap(s);
                                    Bitmap image = new Bitmap(ComprimirImagen.VaryQualityLevel((Image)img.Clone(), 35L));
                                    Entrega.UrlImagen64 = image.ToBase64String(img.RawFormat);
                                }

                            }
                        }
                        else
                        {
                            Entrega.BandImg = true;
                        }
                        Entrega.Conexion = Conexion;
                        Entrega.Precio = Entrega.Total / Entrega.Litros;
                        Entrega.NuevoRegistro = false;
                        Entrega.Usuario = User.Identity.Name;
                        Entrega = EntregaCombustibleDatos.AcEntregaCombustible(Entrega);
                        if (Entrega.Completado == true)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "El registro se guardo correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            if (Entrega.Resultado == -1)
                            {
                                TempData["typemessage"] = "2";
                                TempData["message"] = "El estatus no permite su modificación.";
                                Model.ListaSucursales = Datos.ObtenerComboSucursales(Conexion);
                                Model.ListaVehiculos = Datos.ObtenerComboVehiculos(Conexion, Model.IDSucursal);
                                Model.ListaTipoCombustible = Datos.ObtenerComboTiposCombustible(Conexion);
                                Model.ListaProveedores = Datos.ObtenerComboProveedoresCombustible(Conexion, Model.IDSucursal);
                                Model.listaChofer = Datos.obtenerChofer(Conexion, Model.IDSucursal);
                                return View(Model);
                            }
                            else
                            {
                                TempData["typemessage"] = "2";
                                TempData["message"] = "Ocurrió un error al guardar el registro.";
                                Model.ListaSucursales = Datos.ObtenerComboSucursales(Conexion);
                                Model.ListaVehiculos = Datos.ObtenerComboVehiculos(Conexion, Model.IDSucursal);
                                Model.ListaTipoCombustible = Datos.ObtenerComboTiposCombustible(Conexion);
                                Model.ListaProveedores = Datos.ObtenerComboProveedoresCombustible(Conexion, Model.IDSucursal);
                                Model.listaChofer = Datos.obtenerChofer(Conexion, Model.IDSucursal);
                                return View(Model);
                            }
                        }
                    }
                    else
                    {
                        Model.ListaSucursales = Datos.ObtenerComboSucursales(Conexion);
                        Model.ListaVehiculos = Datos.ObtenerComboVehiculos(Conexion, Model.IDSucursal);
                        Model.ListaTipoCombustible = Datos.ObtenerComboTiposCombustible(Conexion);
                        Model.ListaProveedores = Datos.ObtenerComboProveedoresCombustible(Conexion, Model.IDSucursal);
                        Model.listaChofer = Datos.obtenerChofer(Conexion, Model.IDSucursal);
                        return View(Model);
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                Model.ListaSucursales = Datos.ObtenerComboSucursales(Conexion);
                Model.ListaVehiculos = Datos.ObtenerComboVehiculos(Conexion, Model.IDSucursal);
                Model.ListaTipoCombustible = Datos.ObtenerComboTiposCombustible(Conexion);
                Model.ListaProveedores = Datos.ObtenerComboProveedoresCombustible(Conexion, Model.IDSucursal);
                Model.listaChofer = Datos.obtenerChofer(Conexion, Model.IDSucursal);
                TempData["typemessage"] = "2";
                TempData["message"] = "No se pudo guardar los datos. Por favor contacte a soporte técnico";
                return View(Model);
            }
        }

        // GET: Admin/EntregaCombustible/Rendimiento/5
        public ActionResult Rendimiento(string id)
        {
            try
            {
                Token.SaveToken();
                RendimientoCombustibleViewModels Rendimiento = new RendimientoCombustibleViewModels();
                _EntregaCombustible_Datos EntregaCombustibleDatos = new _EntregaCombustible_Datos();
                Rendimiento.IDEntregaCombustible = id;
                Rendimiento.Conexion = Conexion;
                Rendimiento = EntregaCombustibleDatos.ObtenerDatosRendimiento(Rendimiento);
                return View(Rendimiento);
            }
            catch (Exception)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public ActionResult Rendimiento(RendimientoCombustibleViewModels Rendimientoactual, FormCollection COLL)
        {
            //RendimientoCombustibleViewModels Rendimiento = new RendimientoCombustibleViewModels();
            _EntregaCombustible_Datos EntregaCombustibleDatos = new _EntregaCombustible_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        Rendimientoactual.Usuario = User.Identity.Name;
                        Rendimientoactual.Conexion = Conexion;
                        //Rendimiento.IDEntregaCombustible = collection["IDEntregaCombustible"];
                        //Rendimiento.KMFinal = Convert.ToInt32(collection["KMFinal"]);
                        //Rendimiento.Rendimiento = Convert.ToDecimal(collection["Rendimiento"]);
                        Rendimientoactual = EntregaCombustibleDatos.setRendimiento(Rendimientoactual);

                        if (Rendimientoactual.Completado == true)
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
                            return View(Rendimientoactual);
                        }
                    }
                    else
                    {
                        return View(Rendimientoactual);
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
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("Index");
            }
        }

        // GET: Admin/EntregaCombustible/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/EntregaCombustible/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                EntregaCombustibleModels Entrega = new EntregaCombustibleModels();
                _EntregaCombustible_Datos EntregaCombustibleDatos = new _EntregaCombustible_Datos();
                Entrega.Conexion = Conexion;
                Entrega.IDEntregaCombustible = id;
                Entrega.Usuario = User.Identity.Name;
                Entrega = EntregaCombustibleDatos.EliminarEntregaCombustible(Entrega);
                if (Entrega.Completado)
                    return Json("true");
                else
                    return Json("false");
                // TODO: Add delete logic here
            }
            catch
            {
                EntregaCombustibleModels Entrega = new EntregaCombustibleModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se pudo borrar los datos. Por favor contacte a soporte técnico";
                return Json("");

            }
        }

        // POST: Admin/EntregaCombustible/Delete/5
        [HttpPost]
        public ActionResult Procesar(string id)
        {
            try
            {
                EntregaCombustibleModels Entrega = new EntregaCombustibleModels();
                _EntregaCombustible_Datos EntregaCombustibleDatos = new _EntregaCombustible_Datos();
                Entrega.Conexion = Conexion;
                Entrega.IDEntregaCombustible = id;
                Entrega.Usuario = User.Identity.Name;
                Entrega = EntregaCombustibleDatos.ProcesarEntregaCombustible(Entrega);
                if (Entrega.Completado)
                    return Json("true");
                else
                    return Json("false");
            }
            catch
            {
                return Json("false");
            }
        }

        // POST: Admin/EntregaCombustible/ObtenerComboVehiculos
        [HttpPost]
        public ActionResult ObtenerComboVehiculos(string IDSucursal)
        {
            try
            {
                _Combos_Datos Datos = new _Combos_Datos();
                List<CatVehiculoModels> Lista = Datos.ObtenerComboVehiculos(Conexion, IDSucursal);
                return Json(Lista, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        // POST: Admin/EntregaCombustible/ObtenerComboProveedores
        [HttpPost]
        public ActionResult ObtenerComboProveedores(string IDSucursal)
        {
            try
            {
                _Combos_Datos Datos = new _Combos_Datos();
                List<CatProveedorModels> Lista = Datos.ObtenerComboProveedoresCombustible(Conexion, IDSucursal);
                return Json(Lista, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        // POST: Admin/EntregaCombustible/ObtenerComboProveedores
        [HttpPost]
        public ActionResult ObtenerComboChoferes(string IDSucursal)
        {
            try
            {
                _Combos_Datos Datos = new _Combos_Datos();
                List<CatChoferModels> Lista = Datos.obtenerChofer(Conexion, IDSucursal);
                return Json(Lista, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ModalTicket(string ID)
        {

            EntregaCombustibleModels Entrega = new EntregaCombustibleModels();
            _EntregaCombustible_Datos Datos = new _EntregaCombustible_Datos();
            Entrega.Conexion = Conexion;
            Entrega.IDEntregaCombustible = ID;
            Datos.MostrarTickets(Entrega);

           
            return PartialView("ModalTicket", Entrega);
        }

    }
}
