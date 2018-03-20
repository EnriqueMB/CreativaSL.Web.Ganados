using CreativaSL.Web.Ganados.Filters;

using CreativaSL.Web.Ganados.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    [Autorizado]
    public class CatVehiculoController : Controller
    {
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

                CatVehiculoModels Vehiculo = new CatVehiculoModels();
                _CatVehiculo_Datos VehiculoDatos = new _CatVehiculo_Datos();
                Vehiculo.Conexion = Conexion;

                Vehiculo.listaTipoVehiculos = VehiculoDatos.obtenerListaTipoVehiculo(Vehiculo);
                var List = new SelectList(Vehiculo.listaTipoVehiculos, "IDTipoVehiculo", "Descripcion");
                ViewData["cmbTipoVehiculo"] = List;

                Vehiculo.listaSucursal = VehiculoDatos.obtenerListaSucursales(Vehiculo);
                var ListaSucursal = new SelectList(Vehiculo.listaSucursal, "IDSucursal", "NombreSucursal");
                ViewData["cmbSucursal"] = ListaSucursal;

                Vehiculo.listaMarcas = VehiculoDatos.obtenerListaMarcas(Vehiculo);
                var ListaMarcas = new SelectList(Vehiculo.listaMarcas, "IDMarca", "Descripcion");
                ViewData["cmbMarcas"] = ListaMarcas;
                Vehiculo.Estatus = Convert.ToBoolean("true");
                Vehiculo.EsPropio = Convert.ToBoolean("true");
                return View(Vehiculo);
            }
            catch (Exception ex)
            {
                CatLugarModels Lugar = new CatLugarModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Lugar);
            }
        }

        // POST: Admin/CatVehiculo/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                CatVehiculoModels Vehiculo = new CatVehiculoModels();
                _CatVehiculo_Datos VehiculoDatos = new _CatVehiculo_Datos();
                Vehiculo.Conexion = Conexion;
                Vehiculo.Opcion = 1;
                Vehiculo.Usuario = User.Identity.Name;
                Vehiculo.IDMarca = Convert.ToInt32(collection["listaMarcas"]);
                Vehiculo.IDTipoVehiculo = Convert.ToInt32(collection["listaTipoVehiculos"]);
                Vehiculo.IDSucursal = collection["listaSucursal"];
                Vehiculo.Placas = collection["Placas"];
                Vehiculo.Modelo = collection["Modelo"];
                Vehiculo.NoSerie = collection["NoSerie"];
                Vehiculo.Remolque = collection["Remolque"];
                Vehiculo.Color = collection["Color"];
                Vehiculo.Capacidad = collection["Capacidad"];
                Vehiculo.EsPropio = collection["EsPropio"].StartsWith("true");
                Vehiculo.Estatus = true;
                Vehiculo = VehiculoDatos.AcCatVehiculo(Vehiculo);
                if (Vehiculo.Completado == true)
                {
                    TempData["typemessage"] = "1";
                    TempData["message"] = "El registro se guardo correctamente.";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Ocurrió un error al guardar el registro.";
                    return View(Vehiculo);
                }
               
            }
            catch
            {
                CatVehiculoModels Vehiculo = new CatVehiculoModels();

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

                CatVehiculoModels Vehiculo = new CatVehiculoModels();
                _CatVehiculo_Datos VehiculoDatos = new _CatVehiculo_Datos();
                Vehiculo.Conexion = Conexion;

                Vehiculo.listaTipoVehiculos = VehiculoDatos.obtenerListaTipoVehiculo(Vehiculo);
                var List = new SelectList(Vehiculo.listaTipoVehiculos, "IDTipoVehiculo", "Descripcion");
                ViewData["cmbTipoVehiculo"] = List;

                Vehiculo.listaSucursal = VehiculoDatos.obtenerListaSucursales(Vehiculo);
                var ListaSucursal = new SelectList(Vehiculo.listaSucursal, "IDSucursal", "NombreSucursal");
                ViewData["cmbSucursal"] = ListaSucursal;

                Vehiculo.listaMarcas = VehiculoDatos.obtenerListaMarcas(Vehiculo);
                var ListaMarcas = new SelectList(Vehiculo.listaMarcas, "IDMarca", "Descripcion");
                ViewData["cmbMarcas"] = ListaMarcas;
                Vehiculo.IDVehiculo = id;
                Vehiculo = VehiculoDatos.ObtenerDetalleCatVehiculo(Vehiculo);

                return View(Vehiculo);
            }
            catch (Exception ex)
            {
                CatLugarModels Lugar = new CatLugarModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Lugar);
            }
        }

        // POST: Admin/CatVehiculo/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, FormCollection collection)
        {
            try
            {
                CatVehiculoModels Vehiculo = new CatVehiculoModels();
                _CatVehiculo_Datos VehiculoDatos = new _CatVehiculo_Datos();
                Vehiculo.Conexion = Conexion;
                Vehiculo.IDVehiculo = id;
                Vehiculo.Opcion = 2;
                Vehiculo.Usuario = User.Identity.Name;
                Vehiculo.IDMarca = Convert.ToInt32(collection["listaMarcas"]);
                Vehiculo.IDTipoVehiculo = Convert.ToInt32(collection["listaTipoVehiculos"]);
                Vehiculo.IDSucursal = collection["listaSucursal"];
                Vehiculo.Placas = collection["Placas"];
                Vehiculo.Modelo = collection["Modelo"];
                Vehiculo.NoSerie = collection["NoSerie"];
                Vehiculo.Remolque = collection["Remolque"];
                Vehiculo.Color = collection["Color"];
                Vehiculo.Capacidad = collection["Capacidad"];
                Vehiculo.EsPropio = collection["EsPropio"].StartsWith("true");
                Vehiculo.Estatus = true;
                Vehiculo = VehiculoDatos.AcCatVehiculo(Vehiculo);
                if (Vehiculo.Completado == true)
                {
                    TempData["typemessage"] = "1";
                    TempData["message"] = "El registro se guardo correctamente.";

                }
                else
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Ocurrió un error al guardar el registro.";
                }
                return RedirectToAction("Index");
            }
            catch
            {
                CatVehiculoModels Vehiculo = new CatVehiculoModels();

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
        public ActionResult Delete(string id, FormCollection collection)
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
                TempData["typemessage"] = "1";
                TempData["message"] = "El registro se ha eliminado correctamente";
                return Json("");
            }
            catch
            {
                return View();
            }
        }
    }
}
