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
                        HttpPostedFileBase bannerImage = Request.Files[0] as HttpPostedFileBase;
                        if (bannerImage != null && bannerImage.ContentLength > 0)
                        {
                            Stream s = bannerImage.InputStream;
                            Bitmap img = new Bitmap(s);
                            Vehiculo.img64 = img.ToBase64String(ImageFormat.Png);
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
                                Bitmap img = new Bitmap(s);
                                Vehiculo.img64 = img.ToBase64String(ImageFormat.Png);
                            }
                        }
                        else
                        {
                            Vehiculo.BandImg = true;
                        }
                        Vehiculo.Conexion = Conexion;
                        Vehiculo.Opcion = 2;
                        Vehiculo.IDVehiculo = id;
                        //Vehiculo.IDVehiculo = "0";

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
