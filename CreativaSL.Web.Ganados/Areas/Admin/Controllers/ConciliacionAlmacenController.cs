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
    public class ConciliacionAlmacenController : Controller
    {
        private TokenProcessor Token = TokenProcessor.GetInstance();
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");

        // GET: Admin/ConciliacionAlmacen
        public ActionResult Index()
        {
            try
            {
                List<ConciliacionAlmacenModels> Model = new List<ConciliacionAlmacenModels>();
                _ConciliacionAlmacen_Datos Datos = new _ConciliacionAlmacen_Datos();
                Model = Datos.ObtenerConciliaciones(Conexion);
                return View(Model);
            }
            catch (Exception)
            {
                return View(new List<ConciliacionAlmacenModels>());
            }
        }

        // GET: Admin/ConciliacionAlmacen/Create/id
        public ActionResult Create(int id)
        {
            try
            {
                if (id == 1 || id == 2)
                {
                    Token.SaveToken();
                    ConciliacionAlmacenViewModels Model = new ConciliacionAlmacenViewModels();
                    _Combos_Datos CDatos = new _Combos_Datos();
                    Model.ListaSucursales = CDatos.ObtenerComboSucursales(Conexion);
                    Model.ListaAlmacenes = CDatos.ObtenerAlmacenesXIDSucursal(Conexion, string.Empty);
                    //Model.ListaTipoConciliacion = CDatos.ObtenerComboTipoConciliacion(Conexion);
                    Model.FechaConciliacion = DateTime.Today;
                    Model.IDTipoConciliacion = id;
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

        // POST: Admin/ConciliacionAlmacen/Create
        [HttpPost]
        public ActionResult Create(ConciliacionAlmacenViewModels Model)
        {
            _ConciliacionAlmacen_Datos Datos = new _ConciliacionAlmacen_Datos();
            _Combos_Datos CDatos = new _Combos_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        ConciliacionAlmacenModels ModelP = new ConciliacionAlmacenModels
                        {
                            NuevoRegistro = true,
                            IDConciliacionAlmacen = string.Empty,
                            Almacen = new CatAlmacenModels { IDAlmacen = Model.IDAlmacen },
                            Sucursal = new CatSucursalesModels { IDSucursal = Model.IDSucursal },
                            TipoConciliacion = new CatTipoConciliacionModels { IDTipoConciliacion = Model.IDTipoConciliacion },
                            FechaConciliacion = Model.FechaConciliacion,
                            Comentario = Model.Comentarios,
                            Conexion = Conexion,
                            Usuario = User.Identity.Name
                        };
                        Datos.ACConciliacionAlmacen(ModelP);
                        if (ModelP.Completado == true)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "Los datos se guardaron correctamente.";
                            Token.ResetToken();
                            if(Model.IDTipoConciliacion == 1)
                                return RedirectToAction("CreateDetailPos", new { id = ModelP.IDConciliacionAlmacen });
                            else
                                return RedirectToAction("CreateDetailNeg", new { id = ModelP.IDConciliacionAlmacen });
                        }
                        else
                        {
                            Model.ListaSucursales = CDatos.ObtenerComboSucursales(Conexion);
                            Model.ListaAlmacenes = CDatos.ObtenerAlmacenesXIDSucursal(Conexion, Model.IDSucursal);
                            Model.ListaTipoConciliacion = CDatos.ObtenerComboTipoConciliacion(Conexion);
                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrió un error al intentar guardar los datos. Intente más tarde.";
                            return View(Model);
                        }
                    }
                    else
                    {
                        Model.ListaSucursales = CDatos.ObtenerComboSucursales(Conexion);
                        Model.ListaAlmacenes = CDatos.ObtenerAlmacenesXIDSucursal(Conexion, Model.IDSucursal);
                        Model.ListaTipoConciliacion = CDatos.ObtenerComboTipoConciliacion(Conexion);
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
                Model.ListaTipoConciliacion = CDatos.ObtenerComboTipoConciliacion(Conexion);
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrió un error al intentar guardar los datos. Contacte a soporte técnico.";
                return View(Model);
            }
        }

        // GET: Admin/ConciliacionAlmacen/Edit/id
        public ActionResult Edit(string id)
        {
            try
            {
                Token.SaveToken();
                ConciliacionAlmacenViewModels Model = new ConciliacionAlmacenViewModels();
                _Combos_Datos CDatos = new _Combos_Datos();
                _ConciliacionAlmacen_Datos Datos = new _ConciliacionAlmacen_Datos();
                Model = Datos.ObtenerDatosDetalleConciliacion(Conexion, id);
                Model.ListaSucursales = CDatos.ObtenerComboSucursales(Conexion);
                Model.ListaAlmacenes = CDatos.ObtenerAlmacenesXIDSucursal(Conexion, Model.IDSucursal);
                Model.ListaTipoConciliacion = CDatos.ObtenerComboTipoConciliacion(Conexion);
                return View(Model);
            }
            catch (Exception)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("Index");
            }
        }

        // POST: Admin/ConciliacionAlmacen/Edit
        [HttpPost]
        public ActionResult Edit(ConciliacionAlmacenViewModels Model)
        {
            _ConciliacionAlmacen_Datos Datos = new _ConciliacionAlmacen_Datos();
            _Combos_Datos CDatos = new _Combos_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        ConciliacionAlmacenModels ModelP = new ConciliacionAlmacenModels
                        {
                            NuevoRegistro = false,
                            IDConciliacionAlmacen = Model.IDConciliacion,
                            Almacen = new CatAlmacenModels { IDAlmacen = Model.IDAlmacen },
                            Sucursal = new CatSucursalesModels { IDSucursal = Model.IDSucursal },
                            TipoConciliacion = new CatTipoConciliacionModels { IDTipoConciliacion = Model.IDTipoConciliacion },
                            FechaConciliacion = Model.FechaConciliacion,
                            Comentario = Model.Comentarios,
                            Conexion = Conexion,
                            Usuario = User.Identity.Name
                        };
                        Datos.ACConciliacionAlmacen(ModelP);
                        if (ModelP.Completado == true)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "Los datos se guardaron correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("Details", new { id = ModelP.IDConciliacionAlmacen });
                        }
                        else
                        {
                            Model.ListaSucursales = CDatos.ObtenerComboSucursales(Conexion);
                            Model.ListaAlmacenes = CDatos.ObtenerAlmacenesXIDSucursal(Conexion, Model.IDSucursal);
                            Model.ListaTipoConciliacion = CDatos.ObtenerComboTipoConciliacion(Conexion);
                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrió un error al intentar guardar los datos. Intente más tarde.";
                            return View(Model);
                        }
                    }
                    else
                    {
                        Model.ListaSucursales = CDatos.ObtenerComboSucursales(Conexion);
                        Model.ListaAlmacenes = CDatos.ObtenerAlmacenesXIDSucursal(Conexion, Model.IDSucursal);
                        Model.ListaTipoConciliacion = CDatos.ObtenerComboTipoConciliacion(Conexion);
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
                Model.ListaTipoConciliacion = CDatos.ObtenerComboTipoConciliacion(Conexion);
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrió un error al intentar guardar los datos. Contacte a soporte técnico.";
                return View(Model);
            }
        }

        // GET: Admin/ConciliacionAlmacen/Details/id
        public ActionResult Details(string id)
        {
            try
            {
                List<ConciliacionAlmacenDetalleModel> Model = new List<ConciliacionAlmacenDetalleModel>();
                _ConciliacionAlmacen_Datos Datos = new _ConciliacionAlmacen_Datos();
                ConciliacionAlmacenModels Result = Datos.ObtenerDetalleConciliaciones(Conexion, id);
                Model = Result.ListaDetalle;
                ViewBag.IDTipoConciliacion = Result.TipoConciliacion.IDTipoConciliacion;
                ViewBag.IDConciliacion = id;
                return View(Model);
            }
            catch (Exception)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrió un error al intentar guardar los datos. Contacte a soporte técnico.";
                return RedirectToAction("Index");
            }
        }

        // GET: Admin/ConciliacionAlmacen/CreateDetailPos/id
        public ActionResult CreateDetailPos(string id)
        {
            try
            {
                Token.SaveToken();
                ConciliacionAlmacenDetalleViewModels Model = new ConciliacionAlmacenDetalleViewModels();
                _Combos_Datos CDatos = new _Combos_Datos();
                _ConciliacionAlmacen_Datos Datos = new _ConciliacionAlmacen_Datos();
                Model.IDConciliacion = id;
                Model.ListaProductos = CDatos.ObtenerComboProductosAlmacenXIDConciliacion(Conexion, id);
                Model.ListaUnidades = CDatos.ObtenerComboUnidadesXIDProducto(Conexion, string.Empty);
                Model.Existencia = 0;//Datos.ObtenerExistenciaXIDProductoIDUnidadIDConciliacion(Conexion, Model.IDConciliacion, Model.IDProducto, Model.IDUnidadMedida);
                Model.Cantidad = 0;
                Model.Precio = 0;
                return View(Model);
            }
            catch (Exception)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("Index");
            }
        }

        // POST: Admin/ConciliacionAlmacen/CreateDetailsPos
        [HttpPost]
        public ActionResult CreateDetailPos(ConciliacionAlmacenDetalleViewModels Model)
        {
            _ConciliacionAlmacen_Datos Datos = new _ConciliacionAlmacen_Datos();
            _Combos_Datos CDatos = new _Combos_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        ConciliacionAlmacenDetalleModel ModelP = new ConciliacionAlmacenDetalleModel
                        {
                            NuevoRegistro = true,
                            IDConciliacionAlmacenDetalle = string.Empty,
                            IDConciliacionAlmacen = Model.IDConciliacion,
                            Producto = new CatProductosAlmacenModels { IDProductoAlmacen = Model.IDProductoAlmacen },
                            UnidadMedida = new UnidadesProductosAlmacenModels { id_unidadProducto = Model.IDUnidadProducto },
                            Cantidad = Model.Cantidad,
                            Precio = Model.Precio,
                            Conexion = Conexion,
                            Usuario = User.Identity.Name
                        };
                        Datos.ACConciliacionAlmacenDetallePos(ModelP);
                        if (ModelP.Completado == true)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "Los datos se guardaron correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("Details", new { id = ModelP.IDConciliacionAlmacen });
                        }
                        else
                        {
                            Model.ListaProductos = CDatos.ObtenerComboProductosAlmacenXIDConciliacion(Conexion, Model.IDConciliacion);
                            Model.ListaUnidades = CDatos.ObtenerComboUnidadesXIDProducto(Conexion, Model.IDProductoAlmacen);
                            Model.Cantidad = Datos.ObtenerExistenciaXIDProductoIDUnidadIDConciliacion(Conexion, Model.IDConciliacion, Model.IDProductoAlmacen, Model.IDUnidadProducto);
                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrió un error al intentar guardar los datos. Intente más tarde.";
                            return View(Model);
                        }
                    }
                    else
                    {
                        Model.ListaProductos = CDatos.ObtenerComboProductosAlmacenXIDConciliacion(Conexion, Model.IDConciliacion);
                        Model.ListaUnidades = CDatos.ObtenerComboUnidadesXIDProducto(Conexion, Model.IDProductoAlmacen);
                        Model.Cantidad = Datos.ObtenerExistenciaXIDProductoIDUnidadIDConciliacion(Conexion, Model.IDConciliacion, Model.IDProductoAlmacen, Model.IDUnidadProducto);
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
                Model.ListaProductos = CDatos.ObtenerComboProductosAlmacenXIDConciliacion(Conexion, Model.IDConciliacion);
                Model.ListaUnidades = CDatos.ObtenerComboUnidadesXIDProducto(Conexion, Model.IDProductoAlmacen);
                Model.Cantidad = Datos.ObtenerExistenciaXIDProductoIDUnidadIDConciliacion(Conexion, Model.IDConciliacion, Model.IDProductoAlmacen, Model.IDUnidadProducto);
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrió un error al intentar guardar los datos. Contacte a soporte técnico.";
                return View(Model);
            }
        }

        // GET: Admin/ConciliacionAlmacen/CreateDetailNeg/id
        public ActionResult CreateDetailNeg(string id)
        {
            try
            {
                Token.SaveToken();
                ConciliacionAlmacenDetalleNegViewModels Model = new ConciliacionAlmacenDetalleNegViewModels();
                _Combos_Datos CDatos = new _Combos_Datos();
                _ConciliacionAlmacen_Datos Datos = new _ConciliacionAlmacen_Datos();
                Model.IDConciliacion = id;
                Model.ListaProductos = CDatos.ObtenerComboProductosAlmacenXIDConciliacion(Conexion, id);
                Model.ListaUnidades = CDatos.ObtenerComboUnidadesXIDProducto(Conexion, string.Empty);
                Model.Cantidad = 0;//Datos.ObtenerExistenciaXIDProductoIDUnidadIDConciliacion(Conexion, Model.IDConciliacion, Model.IDProducto, Model.IDUnidadMedida);
                return View(Model);
            }
            catch (Exception)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("Index");
            }
        }

        // POST: Admin/ConciliacionAlmacen/CreateDetailsPos
        [HttpPost]
        public ActionResult CreateDetailNeg(ConciliacionAlmacenDetalleNegViewModels Model)
        {
            _ConciliacionAlmacen_Datos Datos = new _ConciliacionAlmacen_Datos();
            _Combos_Datos CDatos = new _Combos_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        ConciliacionAlmacenDetalleModel ModelP = new ConciliacionAlmacenDetalleModel
                        {
                            NuevoRegistro = true,
                            IDConciliacionAlmacenDetalle = string.Empty,
                            IDConciliacionAlmacen = Model.IDConciliacion,
                            Producto = new CatProductosAlmacenModels { IDProductoAlmacen = Model.IDProductoAlmacen },
                            UnidadMedida = new UnidadesProductosAlmacenModels { id_unidadProducto = Model.IDUnidadProducto },
                            Cantidad = Model.Cantidad,
                            Conexion = Conexion,
                            Usuario = User.Identity.Name
                        };
                        Datos.ACConciliacionAlmacenDetallePos(ModelP);
                        if (ModelP.Completado == true)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "Los datos se guardaron correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("Details", new { id = ModelP.IDConciliacionAlmacen });
                        }
                        else
                        {
                            Model.ListaProductos = CDatos.ObtenerComboProductosAlmacenXIDConciliacion(Conexion, Model.IDConciliacion);
                            Model.ListaUnidades = CDatos.ObtenerComboUnidadesXIDProducto(Conexion, Model.IDProductoAlmacen);
                            Model.Cantidad = Datos.ObtenerExistenciaXIDProductoIDUnidadIDConciliacion(Conexion, Model.IDConciliacion, Model.IDProductoAlmacen, Model.IDUnidadProducto);
                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrió un error al intentar guardar los datos. Intente más tarde.";
                            return View(Model);
                        }
                    }
                    else
                    {
                        Model.ListaProductos = CDatos.ObtenerComboProductosAlmacenXIDConciliacion(Conexion, Model.IDConciliacion);
                        Model.ListaUnidades = CDatos.ObtenerComboUnidadesXIDProducto(Conexion, Model.IDProductoAlmacen);
                        Model.Cantidad = Datos.ObtenerExistenciaXIDProductoIDUnidadIDConciliacion(Conexion, Model.IDConciliacion, Model.IDProductoAlmacen, Model.IDUnidadProducto);
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
                Model.ListaProductos = CDatos.ObtenerComboProductosAlmacenXIDConciliacion(Conexion, Model.IDConciliacion);
                Model.ListaUnidades = CDatos.ObtenerComboUnidadesXIDProducto(Conexion, Model.IDProductoAlmacen);
                Model.Cantidad = Datos.ObtenerExistenciaXIDProductoIDUnidadIDConciliacion(Conexion, Model.IDConciliacion, Model.IDProductoAlmacen, Model.IDUnidadProducto);
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrió un error al intentar guardar los datos. Contacte a soporte técnico.";
                return View(Model);
            }
        }

        // GET: Admin/ConciliacionAlmacen/CreateDetailPos/id
        public ActionResult EditDetailPos(string id)
        {
            try
            {
                Token.SaveToken();
                ConciliacionAlmacenDetalleViewModels Model = new ConciliacionAlmacenDetalleViewModels();
                _Combos_Datos CDatos = new _Combos_Datos();
                _ConciliacionAlmacen_Datos Datos = new _ConciliacionAlmacen_Datos();
                Model = Datos.ObtenerDatosConciliacionDetalle(Conexion, id);
                Model.ListaProductos = CDatos.ObtenerComboProductosAlmacenXIDConciliacion(Conexion, Model.IDConciliacion);
                Model.ListaUnidades = CDatos.ObtenerComboUnidadesXIDProducto(Conexion, Model.IDProductoAlmacen);
                Model.Existencia = Datos.ObtenerExistenciaXIDProductoIDUnidadIDConciliacion(Conexion, Model.IDConciliacion, Model.IDProductoAlmacen, Model.IDUnidadProducto);
                return View(Model);
            }
            catch (Exception)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("Index");
            }
        }

        // POST: Admin/ConciliacionAlmacen/CreateDetailsPos
        [HttpPost]
        public ActionResult EditDetailPos(ConciliacionAlmacenDetalleViewModels Model)
        {
            _ConciliacionAlmacen_Datos Datos = new _ConciliacionAlmacen_Datos();
            _Combos_Datos CDatos = new _Combos_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        ConciliacionAlmacenDetalleModel ModelP = new ConciliacionAlmacenDetalleModel
                        {
                            NuevoRegistro = false,
                            IDConciliacionAlmacenDetalle = Model.IDConciliacionDetalle,
                            IDConciliacionAlmacen = Model.IDConciliacion,
                            Producto = new CatProductosAlmacenModels { IDProductoAlmacen = Model.IDProductoAlmacen },
                            UnidadMedida = new UnidadesProductosAlmacenModels { id_unidadProducto = Model.IDUnidadProducto },
                            Cantidad = Model.Cantidad,
                            Precio = Model.Precio,
                            Conexion = Conexion,
                            Usuario = User.Identity.Name
                        };
                        Datos.ACConciliacionAlmacenDetallePos(ModelP);
                        if (ModelP.Completado == true)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "Los datos se guardaron correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("Details", new { id = ModelP.IDConciliacionAlmacen });
                        }
                        else
                        {
                            Model.ListaProductos = CDatos.ObtenerComboProductosAlmacenXIDConciliacion(Conexion, Model.IDConciliacion);
                            Model.ListaUnidades = CDatos.ObtenerComboUnidadesXIDProducto(Conexion, Model.IDProductoAlmacen);
                            Model.Cantidad = Datos.ObtenerExistenciaXIDProductoIDUnidadIDConciliacion(Conexion, Model.IDConciliacion, Model.IDProductoAlmacen, Model.IDUnidadProducto);
                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrió un error al intentar guardar los datos. Intente más tarde.";
                            return View(Model);
                        }
                    }
                    else
                    {
                        Model.ListaProductos = CDatos.ObtenerComboProductosAlmacenXIDConciliacion(Conexion, Model.IDConciliacion);
                        Model.ListaUnidades = CDatos.ObtenerComboUnidadesXIDProducto(Conexion, Model.IDProductoAlmacen);
                        Model.Cantidad = Datos.ObtenerExistenciaXIDProductoIDUnidadIDConciliacion(Conexion, Model.IDConciliacion, Model.IDProductoAlmacen, Model.IDUnidadProducto);
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
                Model.ListaProductos = CDatos.ObtenerComboProductosAlmacenXIDConciliacion(Conexion, Model.IDConciliacion);
                Model.ListaUnidades = CDatos.ObtenerComboUnidadesXIDProducto(Conexion, Model.IDProductoAlmacen);
                Model.Cantidad = Datos.ObtenerExistenciaXIDProductoIDUnidadIDConciliacion(Conexion, Model.IDConciliacion, Model.IDProductoAlmacen, Model.IDUnidadProducto);
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrió un error al intentar guardar los datos. Contacte a soporte técnico.";
                return View(Model);
            }
        }

        // GET: Admin/ConciliacionAlmacen/CreateDetailNeg/id
        public ActionResult EditDetailNeg(string id)
        {
            try
            {
                Token.SaveToken();
                ConciliacionAlmacenDetalleNegViewModels Model = new ConciliacionAlmacenDetalleNegViewModels();
                _ConciliacionAlmacen_Datos Datos = new _ConciliacionAlmacen_Datos();
                _Combos_Datos CDatos = new _Combos_Datos();
                ConciliacionAlmacenDetalleViewModels ModelTemp = Datos.ObtenerDatosConciliacionDetalle(Conexion, id);
                Model = new ConciliacionAlmacenDetalleNegViewModels {
                            IDConciliacion = ModelTemp.IDConciliacion,
                            IDConciliacionDetalle = ModelTemp.IDConciliacionDetalle,
                            IDProductoAlmacen = ModelTemp.IDProductoAlmacen,
                            IDUnidadProducto = ModelTemp.IDUnidadProducto,
                            Cantidad = ModelTemp.Cantidad
                            };
                Model.ListaProductos = CDatos.ObtenerComboProductosAlmacenXIDConciliacion(Conexion, Model.IDConciliacion);
                Model.ListaUnidades = CDatos.ObtenerComboUnidadesXIDProducto(Conexion, Model.IDProductoAlmacen);
                Model.Existencia = Datos.ObtenerExistenciaXIDProductoIDUnidadIDConciliacion(Conexion, Model.IDConciliacion, Model.IDProductoAlmacen, Model.IDUnidadProducto);
                return View(Model);
            }
            catch (Exception)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("Index");
            }
        }

        // POST: Admin/ConciliacionAlmacen/CreateDetailsPos
        [HttpPost]
        public ActionResult EditDetailNeg(ConciliacionAlmacenDetalleNegViewModels Model)
        {
            _ConciliacionAlmacen_Datos Datos = new _ConciliacionAlmacen_Datos();
            _Combos_Datos CDatos = new _Combos_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        ConciliacionAlmacenDetalleModel ModelP = new ConciliacionAlmacenDetalleModel
                        {
                            NuevoRegistro = false,
                            IDConciliacionAlmacenDetalle = Model.IDConciliacionDetalle,
                            IDConciliacionAlmacen = Model.IDConciliacion,
                            Producto = new CatProductosAlmacenModels { IDProductoAlmacen = Model.IDProductoAlmacen },
                            UnidadMedida = new UnidadesProductosAlmacenModels { id_unidadProducto = Model.IDUnidadProducto },
                            Cantidad = Model.Cantidad,
                            Conexion = Conexion,
                            Usuario = User.Identity.Name
                        };
                        Datos.ACConciliacionAlmacenDetallePos(ModelP);
                        if (ModelP.Completado == true)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "Los datos se guardaron correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("Details", new { id = ModelP.IDConciliacionAlmacen });
                        }
                        else
                        {
                            Model.ListaProductos = CDatos.ObtenerComboProductosAlmacenXIDConciliacion(Conexion, Model.IDConciliacion);
                            Model.ListaUnidades = CDatos.ObtenerComboUnidadesXIDProducto(Conexion, Model.IDProductoAlmacen);
                            Model.Cantidad = Datos.ObtenerExistenciaXIDProductoIDUnidadIDConciliacion(Conexion, Model.IDConciliacion, Model.IDProductoAlmacen, Model.IDUnidadProducto);
                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrió un error al intentar guardar los datos. Intente más tarde.";
                            return View(Model);
                        }
                    }
                    else
                    {
                        Model.ListaProductos = CDatos.ObtenerComboProductosAlmacenXIDConciliacion(Conexion, Model.IDConciliacion);
                        Model.ListaUnidades = CDatos.ObtenerComboUnidadesXIDProducto(Conexion, Model.IDProductoAlmacen);
                        Model.Cantidad = Datos.ObtenerExistenciaXIDProductoIDUnidadIDConciliacion(Conexion, Model.IDConciliacion, Model.IDProductoAlmacen, Model.IDUnidadProducto);
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
                Model.ListaProductos = CDatos.ObtenerComboProductosAlmacenXIDConciliacion(Conexion, Model.IDConciliacion);
                Model.ListaUnidades = CDatos.ObtenerComboUnidadesXIDProducto(Conexion, Model.IDProductoAlmacen);
                Model.Cantidad = Datos.ObtenerExistenciaXIDProductoIDUnidadIDConciliacion(Conexion, Model.IDConciliacion, Model.IDProductoAlmacen, Model.IDUnidadProducto);
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrió un error al intentar guardar los datos. Contacte a soporte técnico.";
                return View(Model);
            }
        }


        /* ***************** ****************** START SECTION AJAX POST ******************* ************************ */
        #region AJAX POST
        // POST: Admin/ConciliacionAlmacen/ObtenerAlmacenesXIDSucursal/IDsucursal
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

        // POST: Admin/ConciliacionAlmacen/ObtenerUnidadesXIDProducto/IDProducto
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

        // POST: Admin/ConciliacionAlmacen/ObtenerExistenciaXIDProducto/IDProducto&IDSalida&IDUnidad
        [HttpPost]
        public ActionResult ObtenerExistenciaXIDProducto(string IDProducto, string IDConciliacion, string IDUnidad)
        {
            try
            {
                _ConciliacionAlmacen_Datos Datos = new _ConciliacionAlmacen_Datos();
                string result = string.Format("{0:F3}", Datos.ObtenerExistenciaXIDProductoIDUnidadIDConciliacion(Conexion, IDConciliacion, IDProducto, IDUnidad));
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Admin/ConciliacionAlmacen/Delete/id
        [HttpPost]
        public ActionResult Delete(string id)
        {
            try
            {
                _ConciliacionAlmacen_Datos Datos = new _ConciliacionAlmacen_Datos();
                ConciliacionAlmacenModels Model = Datos.EliminarConciliacion(Conexion, id, User.Identity.Name);
                if (Model.Completado)
                    return Json("true");
                else
                    return Json("");
            }
            catch (Exception)
            {
                return Json("");
            }
        }

        // GET: Admin/ConciliacionAlmacen/Procesar/id
        [HttpPost]
        public ActionResult Procesar(string id)
        {
            try
            {
                _ConciliacionAlmacen_Datos Datos = new _ConciliacionAlmacen_Datos();
                ConciliacionAlmacenModels Model = Datos.ProcesarConciliacion(Conexion, id, User.Identity.Name);
                if (Model.Completado)
                    return Json("true");
                else
                    return Json("");
            }
            catch (Exception)
            {
                return Json("");
            }
        }

        // GET: Admin/ConciliacionAlmacen/DeleteDetails/id
        [HttpPost]
        public ActionResult DeleteDetails(string id)
        {
            try
            {
                _ConciliacionAlmacen_Datos Datos = new _ConciliacionAlmacen_Datos();
                ConciliacionAlmacenDetalleModel Model = Datos.EliminarConciliacionDetalle(Conexion, id, User.Identity.Name);
                if (Model.Completado)
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

    }
}