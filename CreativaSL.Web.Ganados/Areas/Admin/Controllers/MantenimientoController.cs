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

        // GET: Admin/Mantenimiento/Servicios/5
        public ActionResult ServiciosV(string id)
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

        // GET: Admin/Mantenimiento/Servicios/5
        public ActionResult ServiciosR(string id)
        {
            try
            {
                ViewBag.IDRemolque = id;
                _ServicioMantenimiento_Datos Datos = new _ServicioMantenimiento_Datos();
                return View(Datos.ObtenerServiciosXIDRemolque(Conexion, id));
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

        // GET: Admin/Mantenimiento/Create
        public ActionResult CreateV(string id)
        {
            try
            {
                Token.SaveToken();
                ServiciosMantenimientoViewModels Servicio = new ServiciosMantenimientoViewModels();
                _Combos_Datos Datos = new _Combos_Datos();
                Servicio.ListaSucursales = Datos.ObtenerComboSucursales(Conexion);
                Servicio.ID = id;
                return View(Servicio);
            }
            catch (Exception)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("Servicios", new { id = id});
            }
        }

        // POST: Admin/Mantenimiento/Create
        [HttpPost]
        public ActionResult CreateV(ServiciosMantenimientoViewModels Model)
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
                            Fecha = Model.Fecha,
                            Vehiculo = new CatVehiculoModels { IDVehiculo = Model.ID },
                            Opcion = 1,
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
        public ActionResult CreateR(string id)
        {
            try
            {
                Token.SaveToken();
                ServiciosMantenimientoViewModels Servicio = new ServiciosMantenimientoViewModels();
                _Combos_Datos Datos = new _Combos_Datos();
                Servicio.ListaSucursales = Datos.ObtenerComboSucursales(Conexion);
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
        public ActionResult CreateR(ServiciosMantenimientoViewModels Model)
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
                            Fecha = Model.Fecha,
                            //Este objeto es genérico(para solo crear un sp)
                            Vehiculo = new CatVehiculoModels { IDVehiculo = Model.ID },
                            //La opción determina la tabla en la que se guardará
                            Opcion = 2,
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
        public ActionResult EditV(string id)
        {
            try
            {
                Token.SaveToken();
                ServiciosMantenimientoViewModels Servicio = new ServiciosMantenimientoViewModels();
                _Combos_Datos CDatos = new _Combos_Datos();
                _ServicioMantenimiento_Datos Datos = new _ServicioMantenimiento_Datos();                
                Servicio = Datos.ObtenerDatosServicio(Conexion, id, 1);
                Servicio.ListaSucursales = CDatos.ObtenerComboSucursales(Conexion);
                return View(Servicio);
            }
            catch (Exception)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("Servicios");
            }
        }

        // POST: Admin/Mantenimiento/Create
        [HttpPost]
        public ActionResult EditV(ServiciosMantenimientoViewModels Model)
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
                            Fecha = Model.Fecha,
                            Vehiculo = new CatVehiculoModels(),
                            Opcion = 1,
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
        public ActionResult EditR(string id)
        {
            try
            {
                Token.SaveToken();
                ServiciosMantenimientoViewModels Servicio = new ServiciosMantenimientoViewModels();
                _Combos_Datos CDatos = new _Combos_Datos();
                _ServicioMantenimiento_Datos Datos = new _ServicioMantenimiento_Datos();
                Servicio = Datos.ObtenerDatosServicio(Conexion, id, 2);
                Servicio.ListaSucursales = CDatos.ObtenerComboSucursales(Conexion);
                return View(Servicio);
            }
            catch (Exception)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("Servicios");
            }
        }

        // POST: Admin/Mantenimiento/Create
        [HttpPost]
        public ActionResult EditR(ServiciosMantenimientoViewModels Model)
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
                            Fecha = Model.Fecha,
                            Vehiculo = new CatVehiculoModels(),
                            Opcion = 2,
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

        // GET: Admin/Mantenimiento/Details/5
        public ActionResult Details(string id)
        {
            try
            {
                ViewBag.IDServicio = id;                
                _ServicioMantenimiento_Datos Datos = new _ServicioMantenimiento_Datos();
                return View(Datos.ObtenerDetalleServicioXID(Conexion, id));
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
        
                
    }
}
