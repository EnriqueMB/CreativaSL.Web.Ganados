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
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
        // GET: Admin/EntregaCombustible
        public ActionResult Index()
        {
            try
            {
                EntregaCombistibleModels Entregas = new EntregaCombistibleModels();
                _EntregaCombustible_Datos Datos = new _EntregaCombustible_Datos();
                Entregas.Conexion = Conexion;
                Entregas.ListaEntregas = Datos.ObtenerEntregasCombustible(Entregas);
                return View(Entregas);
            }
            catch (Exception)
            {
                EntregaCombistibleModels Entregas = new EntregaCombistibleModels();
                Entregas.ListaEntregas = new List<EntregaCombistibleModels>();
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
                EntregaCombustibleViewModels Entrega = new EntregaCombustibleViewModels();
                _Combos_Datos Datos = new _Combos_Datos();
                Entrega.ListaSucursales = Datos.ObtenerComboSucursales(Conexion);
                Entrega.ListaVehiculos = Datos.ObtenerComboVehiculos(Conexion, string.Empty);
                Entrega.ListaTipoCombustible = Datos.ObtenerComboTiposCombustible(Conexion);
                
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
        public ActionResult Create(EntregaCombustibleViewModels Entrega)
        {
            _EntregaCombustible_Datos EntregaCombustibleDatos = new _EntregaCombustible_Datos();
            _Combos_Datos Datos = new _Combos_Datos();
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    HttpPostedFileBase bannerImage = Request.Files[0] as HttpPostedFileBase;
                    if (bannerImage != null && bannerImage.ContentLength > 0)
                    {
                        Stream s = bannerImage.InputStream;
                        Bitmap img = new Bitmap(s);
                        Entrega.UrlImagen64 = img.ToBase64String(ImageFormat.Png);
                    }

                    Entrega.Conexion = Conexion;
                    Entrega.Opcion = 1;
                    Entrega.IDEntregaCombustible = "0";


                    //Entrega = EntregaCombustibleDatos.AcCatProductosAlmacen(Producto);
                    if (Entrega.Completado == true)
                    {
                        TempData["typemessage"] = "1";
                        TempData["message"] = "El registro se guardo correctamente.";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["typemessage"] = "2";
                        TempData["message"] = "Ocurrió un error al guardar el registro.";
                        return View(Entrega);
                    }
                }
                else
                {
                    Entrega.ListaSucursales = Datos.ObtenerComboSucursales(Conexion);
                    Entrega.ListaVehiculos = Datos.ObtenerComboVehiculos(Conexion, string.Empty);
                    Entrega.ListaTipoCombustible = Datos.ObtenerComboTiposCombustible(Conexion);
                    return View(Entrega);
                }


            }
            catch
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "No se pudo guardar los datos. Por favor contacte a soporte técnico";
                return View(Entrega);
            }
        }

        // GET: Admin/EntregaCombustible/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/EntregaCombustible/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/EntregaCombustible/Rendimiento/5
        public ActionResult Rendimiento(string id)
        {
            try
            {
                RendimientoCombustibleViewModels Rendimiento = new RendimientoCombustibleViewModels();
                Rendimiento.Fecha = DateTime.Today.ToShortDateString();
                Rendimiento.NoTicket = "2998389028e67492";
                Rendimiento.Litros = 10;
                Rendimiento.KMInicial = 12039;
                Rendimiento.Rendimiento = 0;
                return View(Rendimiento);
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
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
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


    }
}
