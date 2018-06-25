using CreativaSL.Web.Ganados.App_Start;
using CreativaSL.Web.Ganados.Models;
using CreativaSL.Web.Ganados.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    public class MantenimientoController : Controller
    {
        private TokenProcessor Token = TokenProcessor.GetInstance();
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");

        // GET: Admin/Mantenimiento
        public ActionResult Index()
        {
            try
            {
                ServiciosMantenimientoModels Model = new ServiciosMantenimientoModels();
                _ServicioMantenimiento_Datos Datos = new _ServicioMantenimiento_Datos();
                Model = Datos.ObtenerDatosIndex(Conexion, string.Empty);
                return View(Model);
            }
            catch(Exception)
            {
                return View(new ServiciosMantenimientoModels());
            }
        }

        // GET: Admin/Mantenimiento/Create
        public ActionResult Create(string id)
        {
            try
            {
                Token.SaveToken();
                ServiciosMantenimientoViewModels Servicio = new ServiciosMantenimientoViewModels();
                _Combos_Datos Datos = new _Combos_Datos();
                Servicio.ListaSucursales = Datos.ObtenerComboSucursales(Conexion);
                Servicio.ListaProveedores = Datos.ObtenerComboProveedoresMantenimiento(Conexion, string.Empty);
                Servicio.ID = id;
                return View(Servicio);
            }
            catch (Exception)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("Servicios", new { id = id });
            }
        }

        // POST: Admin/Mantenimiento/Create
        [HttpPost]
        public ActionResult Create(ServiciosMantenimientoViewModels Model)
        {
            _ServicioMantenimiento_Datos Datos = new _ServicioMantenimiento_Datos();
            _Combos_Datos CDatos = new _Combos_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        ServiciosMantenimientoModels ModelP = new ServiciosMantenimientoModels
                        {
                            NuevoRegistro = true,
                            IDServicio = string.Empty,
                            Sucursal = new CatSucursalesModels { IDSucursal = Model.IDSucursal },
                            Proveedor = new CatProveedorModels { IDProveedor = Model.IDProveedor },
                            Fecha = Model.Fecha,
                            Vehiculo = new CatVehiculoModels { IDVehiculo = Model.ID },
                            Conexion = Conexion,
                            Usuario = User.Identity.Name
                        };
                        Datos.ACServicio(ModelP);
                        if (ModelP.Completado == true)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "Los datos se guardaron correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("CreateDetail", new { id = ModelP.IDServicio });
                        }
                        else
                        {
                            Model.ListaSucursales = CDatos.ObtenerComboSucursales(Conexion);
                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrio un error al intentar guardar los datos. Intente más tarde.";
                            return View(Model);
                        }
                    }
                    else
                    {
                        Model.ListaSucursales = CDatos.ObtenerComboSucursales(Conexion);
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
                Model.ListaSucursales = CDatos.ObtenerComboSucursales(Conexion);
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error al intentar guardar los datos. Contacte a soporte técnico.";
                return View(Model);
            }
        }

        // GET: Admin/Mantenimiento/Create
        public ActionResult Edit(string id)
        {
            try
            {
                Token.SaveToken();
                ServiciosMantenimientoViewModels Servicio = new ServiciosMantenimientoViewModels();
                _Combos_Datos CDatos = new _Combos_Datos();
                _ServicioMantenimiento_Datos Datos = new _ServicioMantenimiento_Datos();
                Servicio = Datos.ObtenerDatosServicio(Conexion, id);
                Servicio.ListaSucursales = CDatos.ObtenerComboSucursales(Conexion);
                Servicio.ListaProveedores = CDatos.ObtenerComboProveedoresMantenimiento(Conexion, Servicio.IDSucursal);
                return View(Servicio);
            }
            catch (Exception)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("Servicios", new {id=id });
            }
        }

        // POST: Admin/Mantenimiento/Create
        [HttpPost]
        public ActionResult Edit(ServiciosMantenimientoViewModels Model)
        {
            _ServicioMantenimiento_Datos Datos = new _ServicioMantenimiento_Datos();
            _Combos_Datos CDatos = new _Combos_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        ServiciosMantenimientoModels ModelP = new ServiciosMantenimientoModels
                        {
                            NuevoRegistro = false,
                            IDServicio = Model.IDServicio,
                            Sucursal = new CatSucursalesModels { IDSucursal = Model.IDSucursal },
                            Proveedor = new CatProveedorModels { IDProveedor = Model.IDProveedor },
                            Fecha = Model.Fecha,
                            Vehiculo = new CatVehiculoModels(),
                            Conexion = Conexion,
                            Usuario = User.Identity.Name
                        };
                        Datos.ACServicio(ModelP);
                        if (ModelP.Completado == true)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "Los datos se guardaron correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("Details", new { id = ModelP.IDServicio });
                        }
                        else
                        {
                            Model.ListaSucursales = CDatos.ObtenerComboSucursales(Conexion);
                            Model.ListaProveedores = CDatos.ObtenerComboProveedoresMantenimiento(Conexion, Model.IDSucursal);
                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrio un error al intentar guardar los datos. Intente más tarde.";
                            return View(Model);
                        }
                    }
                    else
                    {
                        Model.ListaSucursales = CDatos.ObtenerComboSucursales(Conexion);
                        Model.ListaProveedores = CDatos.ObtenerComboProveedoresMantenimiento(Conexion, Model.IDSucursal);
                        return View(Model);
                    }
                }
                else
                {
                    return RedirectToAction("Servicios", new {id = Model.ID });
                }
            }
            catch
            {
                Model.ListaSucursales = CDatos.ObtenerComboSucursales(Conexion);
                Model.ListaProveedores = CDatos.ObtenerComboProveedoresMantenimiento(Conexion, Model.IDSucursal);
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error al intentar guardar los datos. Contacte a soporte técnico.";
                return View(Model);
            }
        }
        
        // GET: Admin/Mantenimiento/Servicios/5
        public ActionResult Servicios(string id)
        {
            try
            {
                ViewBag.IDVehiculo = id;
                _ServicioMantenimiento_Datos Datos = new _ServicioMantenimiento_Datos();
                return View(Datos.ObtenerServiciosXIDVehiculo(Conexion, id));
            }
            catch (Exception)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("Index");
            }
        }
                
        // POST: Admin/Mantenimiento/Delete/5
        [HttpPost]
        public ActionResult Delete(string id)
        {
            try
            {
                ServiciosMantenimientoModels Model = new ServiciosMantenimientoModels();
                _ServicioMantenimiento_Datos Datos = new _ServicioMantenimiento_Datos();
                Model.Conexion = Conexion;
                Model.Usuario = User.Identity.Name;
                Model.IDServicio = id;
                Datos.EliminarServicio(Model);
                if (Model.Completado)
                    return Json("true");
                else
                {
                    throw new Exception("Error");
                }
            }
            catch
            {
                return Json("false");
            }
        }

        // POST: Admin/Mantenimiento/Delete/5
        [HttpPost]
        public ActionResult Process(string id)
        {
            try
            {
                ServiciosMantenimientoModels Model = new ServiciosMantenimientoModels();
                _ServicioMantenimiento_Datos Datos = new _ServicioMantenimiento_Datos();
                Model.Conexion = Conexion;
                Model.Usuario = User.Identity.Name;
                Model.IDServicio = id;
                Datos.ProcesarServicio(Model);
                if (Model.Completado)
                    return Json("true");
                else
                    return Json("");
            }
            catch
            {
                return Json("");
            }
        }
        
        // GET: Admin/Mantenimiento/Details/5
        public ActionResult Details(string id)
        {
            try
            {
                ViewBag.IDServicio = id;
                _ServicioMantenimiento_Datos Datos = new _ServicioMantenimiento_Datos();
                ServiciosMantenimientoModels Resultado = Datos.ObtenerDetalleServicioXID(Conexion, id);
                ViewBag.IDVehiculo = Resultado.Vehiculo.IDVehiculo;
                return View(Resultado.ListaDetalle);
            }
            catch (Exception)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("Index");
            }
        }

        // GET: Admin/Mantenimiento/Create
        public ActionResult CreateDetail(string id)
        {
            try
            {
                Token.SaveToken();
                ServiciosMantenimientoDetalleViewModels Servicio = new ServiciosMantenimientoDetalleViewModels();
                _Combos_Datos Datos = new _Combos_Datos();
                Servicio.ListaTipoServicios = Datos.ObtenerComboTipoServicio(Conexion);
                Servicio.IDServicio = id;
                return View(Servicio);
            }
            catch (Exception)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("Servicios", new { id = id });
            }
        }

        // POST: Admin/Mantenimiento/Create
        [HttpPost]
        public ActionResult CreateDetail(ServiciosMantenimientoDetalleViewModels Model)
        {
            _ServicioMantenimiento_Datos Datos = new _ServicioMantenimiento_Datos();
            _Combos_Datos CDatos = new _Combos_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        ServiciosMantenimientoDetalleModels ModelP = new ServiciosMantenimientoDetalleModels
                        {
                            NuevoRegistro = true,
                            IDServicioDetalle = string.Empty,
                            IDServicio = Model.IDServicio,
                            TipoServicio = new CatTipoServicioModels { IDTipoServicio = Model.IDTipoServicio },
                            Encargado = Model.Encargado,
                            Importe = Model.Importe,
                            ImporteRefacciones = Model.ImporteRefacciones,
                            Observaciones = Model.Observaciones,
                            Conexion = Conexion,
                            Usuario = User.Identity.Name
                        };
                        Datos.ACServicioDetalle(ModelP);
                        if (ModelP.Completado == true)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "Los datos se guardaron correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("Details", new { id = ModelP.IDServicio });
                        }
                        else
                        {
                            Model.ListaTipoServicios = CDatos.ObtenerComboTipoServicio(Conexion);
                            if (ModelP.Resultado == -1)
                            {
                                ModelState.AddModelError("", "El estatus del servicio no permite su modificación.");
                            }
                            else
                            {
                                TempData["typemessage"] = "2";
                                TempData["message"] = "Ocurrió un error al intentar guardar los datos. Intente más tarde.";
                            }
                            return View(Model);
                        }
                    }
                    else
                    {
                        Model.ListaTipoServicios = CDatos.ObtenerComboTipoServicio(Conexion);
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
                Model.ListaTipoServicios = CDatos.ObtenerComboTipoServicio(Conexion);
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrió un error al intentar guardar los datos. Contacte a soporte técnico.";
                return View(Model);
            }
        }

        // GET: Admin/Mantenimiento/Create
        public ActionResult EditDetail(string id)
        {
            try
            {
                Token.SaveToken();
                ServiciosMantenimientoDetalleViewModels Servicio = new ServiciosMantenimientoDetalleViewModels();
                _Combos_Datos CDatos = new _Combos_Datos();
                _ServicioMantenimiento_Datos Datos = new _ServicioMantenimiento_Datos();
                Servicio = Datos.ObtenerDatosServicioDetalle(Conexion, id);
                Servicio.ListaTipoServicios = CDatos.ObtenerComboTipoServicio(Conexion);
                Servicio.IDServicioDetalle = id;
                return View(Servicio);
            }
            catch (Exception)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("Index");
            }
        }

        // POST: Admin/Mantenimiento/Create
        [HttpPost]
        public ActionResult EditDetail(ServiciosMantenimientoDetalleViewModels Model)
        {
            _ServicioMantenimiento_Datos Datos = new _ServicioMantenimiento_Datos();
            _Combos_Datos CDatos = new _Combos_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        ServiciosMantenimientoDetalleModels ModelP = new ServiciosMantenimientoDetalleModels
                        {
                            NuevoRegistro = false,
                            IDServicioDetalle = Model.IDServicioDetalle,
                            IDServicio = Model.IDServicio,
                            TipoServicio = new CatTipoServicioModels { IDTipoServicio = Model.IDTipoServicio },
                            Encargado = Model.Encargado,
                            Importe = Model.Importe,
                            ImporteRefacciones = Model.ImporteRefacciones,
                            Observaciones = Model.Observaciones,
                            Conexion = Conexion,
                            Usuario = User.Identity.Name
                        };
                        Datos.ACServicioDetalle(ModelP);
                        if (ModelP.Completado == true)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "Los datos se guardaron correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("Details", new { id = ModelP.IDServicio });
                        }
                        else
                        {
                            Model.ListaTipoServicios = CDatos.ObtenerComboTipoServicio(Conexion);
                            if (ModelP.Resultado == -1)
                            {
                                ModelState.AddModelError("", "El estatus del servicio no permite su modificación.");
                            }
                            else
                            {
                                TempData["typemessage"] = "2";
                                TempData["message"] = "Ocurrió un error al intentar guardar los datos. Intente más tarde.";
                            }
                            return View(Model);
                        }
                    }
                    else
                    {
                        Model.ListaTipoServicios = CDatos.ObtenerComboTipoServicio(Conexion);
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
                Model.ListaTipoServicios = CDatos.ObtenerComboTipoServicio(Conexion);
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrió un error al intentar guardar los datos. Contacte a soporte técnico.";
                return View(Model);
            }
        }

        // POST: Admin/Mantenimiento/Delete/5
        [HttpPost]
        public ActionResult DeleteDetail(string id)
        {
            try
            {
                ServiciosMantenimientoDetalleModels Model = new ServiciosMantenimientoDetalleModels();
                _ServicioMantenimiento_Datos Datos = new _ServicioMantenimiento_Datos();
                Model.Conexion = Conexion;
                Model.Usuario = User.Identity.Name;
                Model.IDServicioDetalle = id;
                Datos.EliminarServicioDetalle(Model);
                if (Model.Completado)
                    return Json("true");
                else
                    return Json("");
            }
            catch
            {
                return Json("");
            }
        }


        // POST: Admin/Mantenimiento/ObtenerComboProveedores
        [HttpPost]
        public ActionResult ObtenerComboProveedores(string IDSucursal)
        {
            try
            {
                _Combos_Datos Datos = new _Combos_Datos();
                List<CatProveedorModels> Lista = Datos.ObtenerComboProveedoresMantenimiento(Conexion, IDSucursal);
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
