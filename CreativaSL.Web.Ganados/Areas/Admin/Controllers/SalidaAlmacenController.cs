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
    public class SalidaAlmacenController : Controller
    {
        private TokenProcessor Token = TokenProcessor.GetInstance();
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");

        // GET: Admin/SalidaAlmacen
        public ActionResult Index()
        {
            try
            {
                List<SalidaAlmacenModels> Model = new List<SalidaAlmacenModels>();
                _SalidaAlmacen_Datos Datos = new _SalidaAlmacen_Datos();
                Model = Datos.ObtenerSalidas(Conexion);
                return View(Model);
            }
            catch (Exception)
            {
                return View(new List<SalidaAlmacenModels>());
            }
        }

        // GET: Admin/SalidaAlmacen/Create
        public ActionResult Create()
        {
            try
            {
                Token.SaveToken();
                SalidaAlmacenViewModels Model = new SalidaAlmacenViewModels();
                _Combos_Datos Datos = new _Combos_Datos();
                Model.ListaSucursales = Datos.ObtenerComboSucursales(Conexion);
                Model.ListaAlmacenes = Datos.ObtenerAlmacenesXIDSucursal(Conexion, string.Empty);
                Model.ListaEmpleados = Datos.ObtenerComboEmpleadosSalidaAlmacen(Conexion, string.Empty);
                Model.FechaSalida = DateTime.Today;
                return View(Model);
            }
            catch (Exception)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("Index");
            }
        }

        // POST: Admin/SalidaAlmacen/Create
        [HttpPost]
        public ActionResult Create(SalidaAlmacenViewModels Model)
        {
            _SalidaAlmacen_Datos Datos = new _SalidaAlmacen_Datos();
            _Combos_Datos CDatos = new _Combos_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        SalidaAlmacenModels ModelP = new SalidaAlmacenModels
                        {
                            NuevoRegistro = true,
                            IDSalidaAlmacen = string.Empty,
                            Almacen = new CatAlmacenModels { IDAlmacen = Model.IDAlmacen },
                            Sucursal = new CatSucursalesModels { IDSucursal = Model.IDSucursal },
                            Empleado = new CatEmpleadoModels { IDEmpleado = Model.IDEmpleado },
                            FechaSalida = Model.FechaSalida,
                            Comentario = Model.Comentario,
                            Conexion = Conexion,
                            Usuario = User.Identity.Name
                        };
                        Datos.ACSalidaAlmacen(ModelP);
                        if (ModelP.Completado == true)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "Los datos se guardaron correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("CreateDetails", new { id = ModelP.IDSalidaAlmacen });
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

        // GET: Admin/SalidaAlmacen/Edit
        public ActionResult Edit(string id)
        {
            try
            {
                Token.SaveToken();
                SalidaAlmacenViewModels Model = new SalidaAlmacenViewModels();
                _Combos_Datos CDatos = new _Combos_Datos();
                _SalidaAlmacen_Datos Datos = new _SalidaAlmacen_Datos();
                Model = Datos.ObtenerDetalleSalida(Conexion, id);
                if (!string.IsNullOrWhiteSpace(Model.IDSalidaAlmacen))
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

        // POST: Admin/SalidaAlmacen/Create
        [HttpPost]
        public ActionResult Edit(SalidaAlmacenViewModels Model)
        {
            _SalidaAlmacen_Datos Datos = new _SalidaAlmacen_Datos();
            _Combos_Datos CDatos = new _Combos_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        SalidaAlmacenModels ModelP = new SalidaAlmacenModels
                        {
                            NuevoRegistro = false,
                            IDSalidaAlmacen = Model.IDSalidaAlmacen,
                            Almacen = new CatAlmacenModels { IDAlmacen = Model.IDAlmacen },
                            Sucursal = new CatSucursalesModels { IDSucursal = Model.IDSucursal },
                            Empleado = new CatEmpleadoModels { IDEmpleado = Model.IDEmpleado },
                            FechaSalida = Model.FechaSalida,
                            Comentario = Model.Comentario,
                            Conexion = Conexion,
                            Usuario = User.Identity.Name
                        };
                        Datos.ACSalidaAlmacen(ModelP);
                        if (ModelP.Completado == true)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "Los datos se guardaron correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("Details", new { id = ModelP.IDSalidaAlmacen });
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
        
        // GET: Admin/SalidaAlmacen/Details
        public ActionResult Details(string id)
        {
            try
            {
                List<SalidaAlmacenDetalleModels> Model = new List<SalidaAlmacenDetalleModels>();
                _SalidaAlmacen_Datos Datos = new _SalidaAlmacen_Datos();
                ViewBag.IDSalida = id;
                Model = Datos.ObtenerListaDetalleSalida(Conexion, id);
                return View(Model);
            }
            catch (Exception)
            {
                return View(new List<SalidaAlmacenDetalleModels>());
            }
        }
        
        // GET: Admin/SalidaAlmacen/Create
        public ActionResult CreateDetails(string id)
        {
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    Token.SaveToken();
                    SalidaAlmacenDetalleViewModels Model = new SalidaAlmacenDetalleViewModels();
                    _Combos_Datos Datos = new _Combos_Datos();
                    Model.IDSalida = id;
                    Model.ListaProductos = Datos.ObtenerComboProductosAlmacenXIDSalida(Conexion, id);
                    Model.ListaUnidades = new List<UnidadesProductosAlmacenModels>(); //Datos.ObtenerComboUnidadesXIDProducto(Conexion, string.Empty);
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

        // POST: Admin/SalidaAlmacen/Create
        [HttpPost]
        public ActionResult CreateDetails(SalidaAlmacenDetalleViewModels Model)
        {
            _SalidaAlmacen_Datos Datos = new _SalidaAlmacen_Datos();
            _Combos_Datos CDatos = new _Combos_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        SalidaAlmacenDetalleModels ModelP = new SalidaAlmacenDetalleModels
                        {
                            NuevoRegistro = true,
                            IDSalidaDetalle = Model.IDSalidaDetalle,
                            IDSalida = Model.IDSalida,
                            Producto = new CatProductosAlmacenModels { IDProductoAlmacen = Model.IDProductoAlmacen },
                            UnidadMedida = new UnidadesProductosAlmacenModels { id_unidadProducto = Model.IDUnidadProducto },
                            Cantidad = Model.Cantidad,
                            Conexion = Conexion,
                            Usuario = User.Identity.Name
                        };
                        Datos.ACSalidaAlmacenDetalle(ModelP);
                        if (ModelP.Completado == true)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "Los datos se guardaron correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("Details", new { id = Model.IDSalida });
                        }
                        else
                        {
                            Model.ListaProductos = CDatos.ObtenerComboProductosAlmacenXIDSalida(Conexion, Model.IDSalida);
                            Model.ListaUnidades = CDatos.ObtenerComboUnidadesXIDProducto(Conexion, Model.IDProductoAlmacen);
                            Model.Existencia = Datos.ObtenerExistenciaXIDProductoIDUnidadIDSalida(Conexion, Model.IDSalida, Model.IDProductoAlmacen, Model.IDUnidadProducto);
                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrió un error al intentar guardar los datos. Intente más tarde.";
                            return View(Model);
                        }
                    }
                    else
                    {
                        Model.ListaProductos = CDatos.ObtenerComboProductosAlmacenXIDSalida(Conexion, Model.IDSalida);
                        Model.ListaUnidades = CDatos.ObtenerComboUnidadesXIDProducto(Conexion, Model.IDProductoAlmacen);
                        Model.Existencia = Datos.ObtenerExistenciaXIDProductoIDUnidadIDSalida(Conexion, Model.IDSalida, Model.IDProductoAlmacen, Model.IDUnidadProducto);
                        return View(Model);
                    }
                }
                else
                {
                    return RedirectToAction("Details", new { id= Model.IDSalida });
                }
            }
            catch
            {
                Model.ListaProductos = CDatos.ObtenerComboProductosAlmacenXIDSalida(Conexion, Model.IDSalida);
                Model.ListaUnidades = CDatos.ObtenerComboUnidadesXIDProducto(Conexion, Model.IDProductoAlmacen);
                Model.Existencia = Datos.ObtenerExistenciaXIDProductoIDUnidadIDSalida(Conexion, Model.IDSalida, Model.IDProductoAlmacen, Model.IDUnidadProducto);
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrió un error al intentar guardar los datos. Contacte a soporte técnico.";
                return View(Model);
            }
        }

        // GET: Admin/SalidaAlmacen/Create
        public ActionResult EditDetails(string id)
        {
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    Token.SaveToken();
                    _SalidaAlmacen_Datos Datos = new _SalidaAlmacen_Datos();
                    SalidaAlmacenDetalleViewModels Model = Datos.ObtenerDatosSalidaDetalle(Conexion, id);
                    _Combos_Datos CDatos = new _Combos_Datos();
                    Model.ListaProductos = CDatos.ObtenerComboProductosAlmacenXIDSalida(Conexion, Model.IDSalida);
                    Model.ListaUnidades = CDatos.ObtenerComboUnidadesXIDProducto(Conexion, Model.IDProductoAlmacen);
                    Model.Existencia = Datos.ObtenerExistenciaXIDProductoIDUnidadIDSalida(Conexion, Model.IDSalida, Model.IDProductoAlmacen, Model.IDUnidadProducto);
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

        // POST: Admin/SalidaAlmacen/Create
        [HttpPost]
        public ActionResult EditDetails(SalidaAlmacenDetalleViewModels Model)
        {
            _SalidaAlmacen_Datos Datos = new _SalidaAlmacen_Datos();
            _Combos_Datos CDatos = new _Combos_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        SalidaAlmacenDetalleModels ModelP = new SalidaAlmacenDetalleModels
                        {
                            NuevoRegistro = false,
                            IDSalidaDetalle = Model.IDSalidaDetalle,
                            IDSalida = Model.IDSalida,
                            Producto = new CatProductosAlmacenModels { IDProductoAlmacen = Model.IDProductoAlmacen },
                            UnidadMedida = new UnidadesProductosAlmacenModels { id_unidadProducto = Model.IDUnidadProducto },
                            Cantidad = Model.Cantidad,
                            Conexion = Conexion,
                            Usuario = User.Identity.Name
                        };
                        Datos.ACSalidaAlmacenDetalle(ModelP);
                        if (ModelP.Completado == true)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "Los datos se guardaron correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("Details", new { id = Model.IDSalida });
                        }
                        else
                        {
                            Model.ListaProductos = CDatos.ObtenerComboProductosAlmacenXIDSalida(Conexion, Model.IDSalida);
                            Model.ListaUnidades = CDatos.ObtenerComboUnidadesXIDProducto(Conexion, Model.IDProductoAlmacen);
                            Model.Existencia = Datos.ObtenerExistenciaXIDProductoIDUnidadIDSalida(Conexion, Model.IDSalida, Model.IDProductoAlmacen, Model.IDUnidadProducto);
                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrió un error al intentar guardar los datos. Intente más tarde.";
                            return View(Model);
                        }
                    }
                    else
                    {
                        Model.ListaProductos = CDatos.ObtenerComboProductosAlmacenXIDSalida(Conexion, Model.IDSalida);
                        Model.ListaUnidades = CDatos.ObtenerComboUnidadesXIDProducto(Conexion, Model.IDProductoAlmacen);
                        Model.Existencia = Datos.ObtenerExistenciaXIDProductoIDUnidadIDSalida(Conexion, Model.IDSalida, Model.IDProductoAlmacen, Model.IDUnidadProducto);
                        return View(Model);
                    }
                }
                else
                {
                    return RedirectToAction("Details", new { id = Model.IDSalida });
                }
            }
            catch
            {
                Model.ListaProductos = CDatos.ObtenerComboProductosAlmacenXIDSalida(Conexion, Model.IDSalida);
                Model.ListaUnidades = CDatos.ObtenerComboUnidadesXIDProducto(Conexion, Model.IDProductoAlmacen);
                Model.Existencia = Datos.ObtenerExistenciaXIDProductoIDUnidadIDSalida(Conexion, Model.IDSalida, Model.IDProductoAlmacen, Model.IDUnidadProducto);
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrió un error al intentar guardar los datos. Contacte a soporte técnico.";
                return View(Model);
            }
        }

        /* ***************** ****************** START SECTION AJAX POST ******************* ************************ */
        #region AJAX POST
        // POST: Admin/SalidaAlmacen/ObtenerAlmacenesXIDSucursal/IDsucursal
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

        // POST: Admin/SalidaAlmacen/ObtenerEmpleadosXIDSucursal/IDsucursal
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

        // POST: Admin/SalidaAlmacen/ObtenerUnidadesXIDProducto/IDProducto
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
        
        // POST: Admin/SalidaAlmacen/ObtenerExistenciaXIDProducto/IDProducto&IDSalida&IDUnidad
        [HttpPost]
        public ActionResult ObtenerExistenciaXIDProducto(string IDProducto, string IDSalida, string IDUnidad)
        {
            try
            {
                _SalidaAlmacen_Datos Datos = new _SalidaAlmacen_Datos();
                string result = string.Format("{0:F3}", Datos.ObtenerExistenciaXIDProductoIDUnidadIDSalida(Conexion, IDSalida, IDProducto, IDUnidad));
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Admin/SalidaAlmacen/Delete/id
        [HttpPost]
        public ActionResult Delete(string id)
        {
            try
            {
                _SalidaAlmacen_Datos SalidaDatos = new _SalidaAlmacen_Datos();
                SalidaAlmacenModels Salida = SalidaDatos.EliminarSalida(Conexion, id, User.Identity.Name);
                if (Salida.Completado)
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
        public ActionResult Procesar(string id)
        {
            try
            {
                _SalidaAlmacen_Datos SalidaDatos = new _SalidaAlmacen_Datos();
                SalidaAlmacenModels Salida = SalidaDatos.ProcesarSalidaAlmacen(Conexion, id, User.Identity.Name);
                if (Salida.Completado)
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
        public ActionResult DeleteDetails(string id)
        {
            try
            {
                _SalidaAlmacen_Datos SalidaDatos = new _SalidaAlmacen_Datos();
                SalidaAlmacenDetalleModels Salida = SalidaDatos.EliminarSalidaDetalle(Conexion, id, User.Identity.Name);
                if (Salida.Completado)
                    return Json("true");
                else
                    return Json("");
            }
            catch (Exception)
            {
                return Json("");
            }
        }

        #endregion
        /* ***************** ****************** END SECTION AJAX POST ******************* ************************ */

        //public ActionResult VerListaTest()
        //{
        //    try
        //    {
        //        Token.SaveToken();
        //        List<CatProveedorModels> Lista = new List<CatProveedorModels>();
        //        Lista.Add(new CatProveedorModels { IDProveedor = "0001", nombreProveedor = "Proveedor 001" });
        //        Lista.Add(new CatProveedorModels { IDProveedor = "0002", nombreProveedor = "Proveedor 002" });
        //        Lista.Add(new CatProveedorModels { IDProveedor = "0003", nombreProveedor = "Proveedor 003" });
        //        Lista.Add(new CatProveedorModels { IDProveedor = "0004", nombreProveedor = "Proveedor 004" });
        //        Lista.Add(new CatProveedorModels { IDProveedor = "0005", nombreProveedor = "Proveedor 005" });
        //        Lista.Add(new CatProveedorModels { IDProveedor = "0006", nombreProveedor = "Proveedor 006" });
        //        Lista.Add(new CatProveedorModels { IDProveedor = "0007", nombreProveedor = "Proveedor 007" });
        //        Lista.Add(new CatProveedorModels { IDProveedor = "0008", nombreProveedor = "Proveedor 008" });
        //        Lista.Add(new CatProveedorModels { IDProveedor = "0009", nombreProveedor = "Proveedor 009" });
        //        Lista.Add(new CatProveedorModels { IDProveedor = "0010", nombreProveedor = "Proveedor 010" });
        //        Lista.Add(new CatProveedorModels { IDProveedor = "0011", nombreProveedor = "Proveedor 011" });
        //        Lista.Add(new CatProveedorModels { IDProveedor = "0012", nombreProveedor = "Proveedor 012" });
        //        Lista.Add(new CatProveedorModels { IDProveedor = "0013", nombreProveedor = "Proveedor 013" });
        //        Lista.Add(new CatProveedorModels { IDProveedor = "0014", nombreProveedor = "Proveedor 014" });
        //        Lista.Add(new CatProveedorModels { IDProveedor = "0015", nombreProveedor = "Proveedor 015" });
        //        Lista.Add(new CatProveedorModels { IDProveedor = "0016", nombreProveedor = "Proveedor 016" });
        //        Lista.Add(new CatProveedorModels { IDProveedor = "0017", nombreProveedor = "Proveedor 017" });
        //        Lista.Add(new CatProveedorModels { IDProveedor = "0018", nombreProveedor = "Proveedor 018" });
        //        Lista.Add(new CatProveedorModels { IDProveedor = "0019", nombreProveedor = "Proveedor 019" });
        //        Lista.Add(new CatProveedorModels { IDProveedor = "0020", nombreProveedor = "Proveedor 020" });
        //        Lista.Add(new CatProveedorModels { IDProveedor = "0021", nombreProveedor = "Proveedor 021" });
        //        Lista.Add(new CatProveedorModels { IDProveedor = "0022", nombreProveedor = "Proveedor 022" });
        //        return View(Lista);
        //    }
        //    catch (Exception)
        //    {
        //        TempData["typemessage"] = "2";
        //        TempData["message"] = "No se puede cargar la vista";
        //        return RedirectToAction("Index");
        //    }
        //}
        //[HttpPost]
        //public ActionResult VerListaTest(FormCollection values, List<CatProveedorModels> Model)
        //{
        //    try
        //    {
        //        return View();   
        //    }
        //    catch (Exception)
        //    {
        //        TempData["typemessage"] = "2";
        //        TempData["message"] = "No se puede cargar la vista";
        //        return RedirectToAction("Index");
        //    }
        //}
    }
}