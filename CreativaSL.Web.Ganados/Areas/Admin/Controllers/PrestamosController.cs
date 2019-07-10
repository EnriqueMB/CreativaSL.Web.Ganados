using CreativaSL.Web.Ganados.App_Start;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CreativaSL.Web.Ganados.Models;
using CreativaSL.Web.Ganados.ViewModels;

namespace CreativaSL.Web.Ganados.Areas.Admin
{
    public class PrestamosController : Controller
    {
        private TokenProcessor Token = TokenProcessor.GetInstance();
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
        // GET: Admin/Prestamos
        public ActionResult Index()
        {
            try
            {
                List<PrestamoHerramientaModels> Model = new List<PrestamoHerramientaModels>();
                _PrestamoHerramienta_Datos Datos = new _PrestamoHerramienta_Datos();
                Model = Datos.ObtenerPrestamoHerramienta(Conexion);
                return View(Model);
            }
            catch (Exception)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(new List<SalidaAlmacenModels>());
            }
        }

        // GET: Admin/Prestamos/Create
        public ActionResult Create()
        {
            try
            {
                Token.SaveToken();
                PrestamoHerramientaViewModels Model = new PrestamoHerramientaViewModels();
                _Combos_Datos Datos = new _Combos_Datos();
                Model.ListaSucursales = Datos.ObtenerComboSucursales(Conexion);
                Model.ListaAlmacenes = Datos.ObtenerAlmacenesXIDSucursal(Conexion, string.Empty);
                Model.ListaEmpleados = Datos.ObtenerComboEmpleadosSalidaAlmacen(Conexion, string.Empty);
                Model.FechaPrestamo = DateTime.Today;
                return View(Model);
            }
            catch (Exception)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("Index");
            }
        }

        // POST: Admin/Prestamos/Create
        [HttpPost]
        public ActionResult Create(PrestamoHerramientaViewModels Model)
        {
            _PrestamoHerramienta_Datos Datos = new _PrestamoHerramienta_Datos();
            _Combos_Datos CDatos = new _Combos_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        PrestamoHerramientaModels ModelP = new PrestamoHerramientaModels
                        {
                            NuevoRegistro = true,
                            IDPrestamo = 0,
                            Almacen = new CatAlmacenModels { IDAlmacen = Model.IDAlmacen },
                            Sucursal = new CatSucursalesModels { IDSucursal = Model.IDSucursal },
                            Empleado = new CatEmpleadoModels { IDEmpleado = Model.IDEmpleado },
                            FechaPrestamo = Model.FechaPrestamo,
                            Observacion = Model.Comentario,
                            Conexion = Conexion,
                            Usuario = User.Identity.Name
                        };
                        Datos.ACPrestamosHerramientasAlmacen(ModelP);
                        if (ModelP.Completado == true)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "Los datos se guardaron correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("CreateDetails", new { id = ModelP.IDPrestamo });
                        }
                        else
                        {
                            Model.ListaSucursales = CDatos.ObtenerComboSucursales(Conexion);
                            Model.ListaAlmacenes = CDatos.ObtenerAlmacenesXIDSucursal(Conexion, Model.IDSucursal);
                            Model.ListaEmpleados = CDatos.ObtenerComboEmpleadosSalidaAlmacen(Conexion, Model.IDSucursal);
                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrió un error al intentar guardar los datos. Intente más tarde.";
                            return View(Model);
                        }
                    }
                    else
                    {
                        Model.ListaSucursales = CDatos.ObtenerComboSucursales(Conexion);
                        Model.ListaAlmacenes = CDatos.ObtenerAlmacenesXIDSucursal(Conexion, Model.IDSucursal);
                        Model.ListaEmpleados = CDatos.ObtenerComboEmpleadosSalidaAlmacen(Conexion, Model.IDSucursal);
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
                Model.ListaAlmacenes = CDatos.ObtenerAlmacenesXIDSucursal(Conexion, Model.IDSucursal);
                Model.ListaEmpleados = CDatos.ObtenerComboEmpleadosSalidaAlmacen(Conexion, Model.IDSucursal);
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrió un error al intentar guardar los datos. Contacte a soporte técnico.";
                return View(Model);
            }
        }

        // GET: Admin/Prestamos/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                Token.SaveToken();
                PrestamoHerramientaViewModels Model = new PrestamoHerramientaViewModels();
                _Combos_Datos CDatos = new _Combos_Datos();
                _PrestamoHerramienta_Datos Datos = new _PrestamoHerramienta_Datos();
                Model = Datos.ObtenerDetallePrestamosHerramienta(Conexion, id);
                if (Model.IDPrestamo != 0)
                {
                    Model.ListaSucursales = CDatos.ObtenerComboSucursales(Conexion);
                    Model.ListaAlmacenes = CDatos.ObtenerAlmacenesXIDSucursal(Conexion, Model.IDSucursal);
                    Model.ListaEmpleados = CDatos.ObtenerComboEmpleadosSalidaAlmacen(Conexion, Model.IDSucursal);
                    return View(Model);
                }
                else
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "No se puede cargar la vista";
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

        // POST: Admin/Prestamos/Edit/5
        [HttpPost]
        public ActionResult Edit(PrestamoHerramientaViewModels Model)
        {
            _PrestamoHerramienta_Datos Datos = new _PrestamoHerramienta_Datos();
            _Combos_Datos CDatos = new _Combos_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        PrestamoHerramientaModels ModelP = new PrestamoHerramientaModels
                        {
                            NuevoRegistro = false,
                            IDPrestamo = Model.IDPrestamo,
                            Almacen = new CatAlmacenModels { IDAlmacen = Model.IDAlmacen },
                            Sucursal = new CatSucursalesModels { IDSucursal = Model.IDSucursal },
                            Empleado = new CatEmpleadoModels { IDEmpleado = Model.IDEmpleado },
                            FechaPrestamo = Model.FechaPrestamo,
                            Observacion = Model.Comentario,
                            Conexion = Conexion,
                            Usuario = User.Identity.Name
                        };
                        Datos.ACPrestamosHerramientasAlmacen(ModelP);
                        if (ModelP.Completado == true)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "Los datos se guardaron correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("Details", new { id = ModelP.IDPrestamo });
                        }
                        else
                        {
                            Model.ListaSucursales = CDatos.ObtenerComboSucursales(Conexion);
                            Model.ListaAlmacenes = CDatos.ObtenerAlmacenesXIDSucursal(Conexion, Model.IDSucursal);
                            Model.ListaEmpleados = CDatos.ObtenerComboEmpleadosSalidaAlmacen(Conexion, Model.IDSucursal);
                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrió un error al intentar guardar los datos. Intente más tarde.";
                            return View(Model);
                        }
                    }
                    else
                    {
                        Model.ListaSucursales = CDatos.ObtenerComboSucursales(Conexion);
                        Model.ListaAlmacenes = CDatos.ObtenerAlmacenesXIDSucursal(Conexion, Model.IDSucursal);
                        Model.ListaEmpleados = CDatos.ObtenerComboEmpleadosSalidaAlmacen(Conexion, Model.IDSucursal);
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
                Model.ListaAlmacenes = CDatos.ObtenerAlmacenesXIDSucursal(Conexion, Model.IDSucursal);
                Model.ListaEmpleados = CDatos.ObtenerComboEmpleadosSalidaAlmacen(Conexion, Model.IDSucursal);
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrió un error al intentar guardar los datos. Contacte a soporte técnico.";
                return View(Model);
            }
        }


        // GET: Admin/Prestamos/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                List<PrestamoHerramientaDetalleModels> Model = new List<PrestamoHerramientaDetalleModels>();
                _PrestamoHerramienta_Datos Datos = new _PrestamoHerramienta_Datos();
                ViewBag.IdPrestamos = id;
                Model = Datos.ObtenerListaDetallePrestamosHerramientas(Conexion, id);
                return View(Model);
            }
            catch (Exception)
            {
                return View(new List<SalidaAlmacenDetalleModels>());
            }
        }
        // GET: Admin/Prestamos/Details/5
        [HttpGet]
        public ActionResult Devolucion(int id)
        {
            try
            {
                List<DevolucionHerramientaDetalleModels> Model = new List<DevolucionHerramientaDetalleModels>();
                _PrestamoHerramienta_Datos Datos = new _PrestamoHerramienta_Datos();
                ViewBag.IdPrestamos = id;
                Model = Datos.ObtenerListaDetalleDevolucionHerramientas(Conexion, id);
                if(Model.Count > 0)
                {
                    ViewBag.Estatus = Model[0].Estatus;
                }
                else
                    ViewBag.Estatus = 2;

                return View(Model);
            }
            catch (Exception)
            {
                return View(new List<SalidaAlmacenDetalleModels>());
            }
        }

        //id = IDPrestamo id2 = IDPrestamoDetalle
        [HttpPost]
        public ActionResult DevolverHerramienta(DevolucionHerramientaDetalleModels Models)
        {
            try
            {
                _PrestamoHerramienta_Datos Datos = new _PrestamoHerramienta_Datos();
               
                DevolucionHerramientaDetalleModels ModelD = new DevolucionHerramientaDetalleModels
                {
                    IDPrestamo = Models.IDPrestamo,
                    IDPrestamoDetalle = Models.IDPrestamoDetalle,
                    CantDevolver = Models.CantDevolver,
                    Usuario = User.Identity.Name,
                    Conexion = Conexion
                };
                RespuestaAjax respuesta = new RespuestaAjax();
                string usuario = User.Identity.Name;

                //respuesta = Datos.ACDevolucionHerramientasAlmacenDetalle(ModelD);
                return Content(respuesta.ToJSON(), "application/json");

            }
            catch(Exception ex)
            {
                throw ex;
            }
                    
        }
        [HttpPost]
        public ActionResult Finalizar(int id4)
        {
            try
            {
                _PrestamoHerramienta_Datos Datos = new _PrestamoHerramienta_Datos();
                DevolucionHerramientaDetalleModels Prestamo = Datos.FinalizarDevolucionHerramientasAlmacen(Conexion, id4, User.Identity.Name);
                if (Prestamo.Completado)
                    return Json("true");
                else
                    return Json("");
            }
            catch (Exception)
            {
                return Json("");
            }
        }


        // GET: Admin/Prestamos/CreateDetails
        public ActionResult CreateDetails(int id)
        {
            try
            {
                if (id != 0)
                {
                    Token.SaveToken();
                    PrestamosHerramientasDetalleViewModels Model = new PrestamosHerramientasDetalleViewModels();
                    _Combos_Datos Datos = new _Combos_Datos();
                    Model.IDPrestamo = id;
                    Model.ListaProductos = Datos.ObtenerComboProductosAlmacenXIDPrestamo(Conexion, id);
                    Model.ListaUnidades = new List<UnidadesProductosAlmacenModels>();
                    Model.Existencia = 0;
                    Model.Cantidad = 0;
                    Model.ListaUnidades.Add(new UnidadesProductosAlmacenModels { id_unidadProducto = string.Empty, Descripcion = " -- Seleccione -- " });
                    return View(Model);
                }
                else
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "No se puede cargar la vista";
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

        //POST: Admin/Prestamos/CreateDetails
        [HttpPost]
        public ActionResult CreateDetails(PrestamosHerramientasDetalleViewModels Model)
        {
            _PrestamoHerramienta_Datos Datos = new _PrestamoHerramienta_Datos();
            _Combos_Datos CDatos = new _Combos_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        PrestamoHerramientaDetalleModels ModelP = new PrestamoHerramientaDetalleModels
                        {
                            NuevoRegistro = true,
                            IDPrestamoDetalle = Model.IDPrestamoDetalle,
                            IDPrestamo = Model.IDPrestamo,
                            Producto = new CatProductosAlmacenModels { IDProductoAlmacen = Model.IDProductoAlmacen },
                            UnidadMedida = new UnidadesProductosAlmacenModels { id_unidadProducto = Model.IDUnidadProducto },
                            Cantidad = Model.Cantidad,
                            Conexion = Conexion,
                            Usuario = User.Identity.Name
                        };
                        Datos.ACPrestamosHerramientasAlmacenDetalle(ModelP);
                        if (ModelP.Completado == true)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "Los datos se guardaron correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("Details", new { id = Model.IDPrestamo });
                        }
                        else
                        {
                            if (ModelP.Resultado == -1)
                            {
                                Model.ListaProductos = CDatos.ObtenerComboProductosAlmacenXIDPrestamo(Conexion, Model.IDPrestamo);
                                Model.ListaUnidades = CDatos.ObtenerComboUnidadesXIDProducto(Conexion, Model.IDProductoAlmacen);
                                Model.Existencia = Datos.ObtenerExistenciaXIDProductoIDUnidadIDPrestamo(Conexion, Model.IDPrestamo, Model.IDProductoAlmacen, Model.IDUnidadProducto);
                                TempData["typemessage"] = "2";
                                TempData["message"] = "El producto ya existen para ese prestamo.";
                                return View(Model);
                            }
                            else if (ModelP.Resultado == -3)
                            {
                                Model.ListaProductos = CDatos.ObtenerComboProductosAlmacenXIDPrestamo(Conexion, Model.IDPrestamo);
                                Model.ListaUnidades = CDatos.ObtenerComboUnidadesXIDProducto(Conexion, Model.IDProductoAlmacen);
                                Model.Existencia = Datos.ObtenerExistenciaXIDProductoIDUnidadIDPrestamo(Conexion, Model.IDPrestamo, Model.IDProductoAlmacen, Model.IDUnidadProducto);
                                TempData["typemessage"] = "2";
                                TempData["message"] = "El estatus del prestamos no permite guardar o modificar.";
                                return View(Model);
                            }
                            else {

                                Model.ListaProductos = CDatos.ObtenerComboProductosAlmacenXIDPrestamo(Conexion, Model.IDPrestamo);
                                Model.ListaUnidades = CDatos.ObtenerComboUnidadesXIDProducto(Conexion, Model.IDProductoAlmacen);
                                Model.Existencia = Datos.ObtenerExistenciaXIDProductoIDUnidadIDPrestamo(Conexion, Model.IDPrestamo, Model.IDProductoAlmacen, Model.IDUnidadProducto);
                                TempData["typemessage"] = "2";
                                TempData["message"] = "Ocurrió un error al intentar guardar los datos. Intente más tarde.";
                                return View(Model);
                            }
                        }
                    }
                    else
                    {
                        Model.ListaProductos = CDatos.ObtenerComboProductosAlmacenXIDPrestamo(Conexion, Model.IDPrestamo);
                        Model.ListaUnidades = CDatos.ObtenerComboUnidadesXIDProducto(Conexion, Model.IDProductoAlmacen);
                        Model.Existencia = Datos.ObtenerExistenciaXIDProductoIDUnidadIDPrestamo(Conexion, Model.IDPrestamo, Model.IDProductoAlmacen, Model.IDUnidadProducto);
                        return View(Model);
                    }
                }
                else
                {
                    return RedirectToAction("Details", new { id = Model.IDPrestamo });
                }
            }
            catch
            {
                Model.ListaProductos = CDatos.ObtenerComboProductosAlmacenXIDPrestamo(Conexion, Model.IDPrestamo);
                Model.ListaUnidades = CDatos.ObtenerComboUnidadesXIDProducto(Conexion, Model.IDProductoAlmacen);
                Model.Existencia = Datos.ObtenerExistenciaXIDProductoIDUnidadIDPrestamo(Conexion, Model.IDPrestamo, Model.IDProductoAlmacen, Model.IDUnidadProducto);
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrió un error al intentar guardar los datos. Contacte a soporte técnico.";
                return View(Model);
            }

        }

        // GET: Admin/Prestamos/EditDetails/IDPrestamosDetalle
        public ActionResult EditDetails(int id)
        {
            try
            {
                if (id != 0)
                {
                    Token.SaveToken();
                    _PrestamoHerramienta_Datos Datos = new _PrestamoHerramienta_Datos();
                    PrestamosHerramientasDetalleViewModels Model = Datos.ObtenerDatosSalidaDetalle(Conexion, id);
                    _Combos_Datos CDatos = new _Combos_Datos();
                    Model.ListaProductos = CDatos.ObtenerComboProductosAlmacenXIDPrestamo(Conexion, Model.IDPrestamo);
                    Model.ListaUnidades = CDatos.ObtenerComboUnidadesXIDProducto(Conexion, Model.IDProductoAlmacen);
                    Model.Existencia = Datos.ObtenerExistenciaXIDProductoIDUnidadIDPrestamo(Conexion, Model.IDPrestamo, Model.IDProductoAlmacen, Model.IDUnidadProducto);
                    return View(Model);
                }
                else
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "No se puede cargar la vista";
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

        //POST: Admin/Prestamos/EditDetails/Objeto
        [HttpPost]
        public ActionResult EditDetails(PrestamosHerramientasDetalleViewModels Model)
        {
            _PrestamoHerramienta_Datos Datos = new _PrestamoHerramienta_Datos();
            _Combos_Datos CDatos = new _Combos_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        PrestamoHerramientaDetalleModels ModelP = new PrestamoHerramientaDetalleModels
                        {
                            NuevoRegistro = false,
                            IDPrestamoDetalle = Model.IDPrestamoDetalle,
                            IDPrestamo = Model.IDPrestamo,
                            Producto = new CatProductosAlmacenModels { IDProductoAlmacen = Model.IDProductoAlmacen },
                            UnidadMedida = new UnidadesProductosAlmacenModels { id_unidadProducto = Model.IDUnidadProducto },
                            Cantidad = Model.Cantidad,
                            Conexion = Conexion,
                            Usuario = User.Identity.Name
                        };
                        Datos.ACPrestamosHerramientasAlmacenDetalle(ModelP);
                        if (ModelP.Completado == true)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "Los datos se guardaron correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("Details", new { id = Model.IDPrestamo });
                        }
                        else
                        {
                            if (ModelP.Resultado == -3)
                            {
                                Model.ListaProductos = CDatos.ObtenerComboProductosAlmacenXIDPrestamo(Conexion, Model.IDPrestamo);
                                Model.ListaUnidades = CDatos.ObtenerComboUnidadesXIDProducto(Conexion, Model.IDProductoAlmacen);
                                Model.Existencia = Datos.ObtenerExistenciaXIDProductoIDUnidadIDPrestamo(Conexion, Model.IDPrestamo, Model.IDProductoAlmacen, Model.IDUnidadProducto);
                                TempData["typemessage"] = "2";
                                TempData["message"] = "El estatus del prestamos no permite guardar o modificar.";
                                return View(Model);
                            }
                            else
                            {

                                Model.ListaProductos = CDatos.ObtenerComboProductosAlmacenXIDPrestamo(Conexion, Model.IDPrestamo);
                                Model.ListaUnidades = CDatos.ObtenerComboUnidadesXIDProducto(Conexion, Model.IDProductoAlmacen);
                                Model.Existencia = Datos.ObtenerExistenciaXIDProductoIDUnidadIDPrestamo(Conexion, Model.IDPrestamo, Model.IDProductoAlmacen, Model.IDUnidadProducto);
                                TempData["typemessage"] = "2";
                                TempData["message"] = "Ocurrió un error al intentar guardar los datos. Intente más tarde.";
                                return View(Model);
                            }
                        }
                    }
                    else
                    {
                        Model.ListaProductos = CDatos.ObtenerComboProductosAlmacenXIDPrestamo(Conexion, Model.IDPrestamo);
                        Model.ListaUnidades = CDatos.ObtenerComboUnidadesXIDProducto(Conexion, Model.IDProductoAlmacen);
                        Model.Existencia = Datos.ObtenerExistenciaXIDProductoIDUnidadIDPrestamo(Conexion, Model.IDPrestamo, Model.IDProductoAlmacen, Model.IDUnidadProducto);
                        return View(Model);
                    }
                }
                else
                {
                    return RedirectToAction("Details", new { id = Model.IDPrestamo });
                }
            }
            catch
            {
                Model.ListaProductos = CDatos.ObtenerComboProductosAlmacenXIDPrestamo(Conexion, Model.IDPrestamo);
                Model.ListaUnidades = CDatos.ObtenerComboUnidadesXIDProducto(Conexion, Model.IDProductoAlmacen);
                Model.Existencia = Datos.ObtenerExistenciaXIDProductoIDUnidadIDPrestamo(Conexion, Model.IDPrestamo, Model.IDProductoAlmacen, Model.IDUnidadProducto);
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrió un error al intentar guardar los datos. Contacte a soporte técnico.";
                return View(Model);
            }
        }

        // POST: Admin/Prestamos/ObtenerAlmacenesXIDSucursal/IDsucursal
        [HttpPost]
        public ActionResult ObtenerAlmacenesXIDSucursal(string IDSucursal)
        {
            try
            {
                _Combos_Datos Datos = new _Combos_Datos();
                List<CatAlmacenModels> Lista = Datos.ObtenerAlmacenesXIDSucursal(Conexion, IDSucursal);
                return Json(Lista, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        // POST: Admin/Prestamos/ObtenerEmpleadosXIDSucursal/IDsucursal
        [HttpPost]
        public ActionResult ObtenerEmpleadosXIDSucursal(string IDSucursal)
        {
            try
            {
                _Combos_Datos Datos = new _Combos_Datos();
                List<CatEmpleadoModels> Lista = Datos.ObtenerComboEmpleadosSalidaAlmacen(Conexion, IDSucursal);
                return Json(Lista, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        // POST: Admin/Prestamos/ObtenerUnidadesXIDProducto/IDProducto
        [HttpPost]
        public ActionResult ObtenerUnidadesXIDProducto(string IDProducto)
        {
            try
            {
                _Combos_Datos Datos = new _Combos_Datos();
                List<UnidadesProductosAlmacenModels> Lista = Datos.ObtenerComboUnidadesXIDProducto(Conexion, IDProducto);
                return Json(Lista, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        // POST: Admin/Prestamos/ObtenerExistenciaXIDProducto/IDProducto&IDPrestamo&IDUnidad
        [HttpPost]
        public ActionResult ObtenerExistenciaXIDProducto(string IDProducto, int IDPrestamo, string IDUnidad)
        {
            try
            {
                _PrestamoHerramienta_Datos Datos = new _PrestamoHerramienta_Datos();
                string result = string.Format("{0:F3}", Datos.ObtenerExistenciaXIDProductoIDUnidadIDPrestamo(Conexion, IDPrestamo, IDProducto, IDUnidad));
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult ObtenerUnidadMedidaXIDProducto(string IDProducto)
        {
            try
            {
                _SalidaAlmacen_Datos Datos = new _SalidaAlmacen_Datos();
                string result = Datos.ObtenerUnidadMedidaXIDProducto(Conexion, IDProducto);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Admin/Prestamos/Delete/id
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                _PrestamoHerramienta_Datos Datos = new _PrestamoHerramienta_Datos();
                PrestamoHerramientaModels prestamo = Datos.EliminarPrestamo(Conexion, id, User.Identity.Name);
                if (prestamo.Completado)
                    return Json("true");
                else
                    return Json("");
            }
            catch (Exception)
            {
                return Json("");
            }
        }

        // GET: Admin/SalidaAlmacen/Delete/id
        [HttpPost]
        public ActionResult DeleteDetails(int id)
        {
            try
            {
                _PrestamoHerramienta_Datos Datos = new _PrestamoHerramienta_Datos();
                PrestamoHerramientaDetalleModels PrestamoD = Datos.EliminarPrestamoDetalle(Conexion, id, User.Identity.Name);
                if (PrestamoD.Completado)
                    return Json("true");
                else
                    return Json("");
            }
            catch (Exception)
            {
                return Json("");
            }
        }

        // GET: Admin/SalidaAlmacen/Delete/id
        [HttpPost]
        public ActionResult Procesar(int id)
        {
            try
            {
                _PrestamoHerramienta_Datos Datos = new _PrestamoHerramienta_Datos();
                PrestamoHerramientaModels Prestamo = Datos.ProcesarPrestamoHerramientasAlmacen(Conexion, id, User.Identity.Name);
                return Json(Prestamo.Resultado);
                //if (Prestamo.Resultado == 1)
                //    return Json(1);
                //else if (Prestamo.Resultado == -3)
                //    return Json(-3);
                //else
                //    return Json("");
            }
            catch (Exception)
            {
                return Json("");
            }
        }
    }
}
