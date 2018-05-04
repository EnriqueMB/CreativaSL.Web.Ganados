using CreativaSL.Web.Ganados.Filters;
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
    [Autorizado]
    public class CompraAlmacenController : Controller
    {
        private TokenProcessor Token = TokenProcessor.GetInstance();
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");

        // GET: Admin/CompraAlmacen
        public ActionResult Index()
        {
            try
            {
                CompraAlmacenModels Compras = new CompraAlmacenModels();
                _CompraAlmacen_Datos CompraDatos = new _CompraAlmacen_Datos();
                Compras.Conexion = Conexion;
                Compras.ListaCompras = CompraDatos.ObtenerGridCompras(Compras);
                return View(Compras);
            }
            catch(Exception)
            {
                CompraAlmacenModels Compras = new CompraAlmacenModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Compras);
            }
        }
        // GET: Admin/CompraAlmacen/Create
        //Capturar compra 
        public ActionResult Create()
        {
            try
            {
                CapturarCompraViewModels Model = new CapturarCompraViewModels();
                _CompraAlmacen_Datos CompraDatos = new _CompraAlmacen_Datos();
                Model.ListaSucursal = CompraDatos.ObtenerComboSucursales(Conexion);
                Model.ListaProveedor = CompraDatos.ObtenerComboProveedores(Conexion);
                return View(Model);
            }
            catch (Exception)
            {
                //Mensajes de error al cargar vista
                CapturarCompraViewModels Model = new CapturarCompraViewModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Model);
            }
        }

        // POST: Admin/CompraAlmacen/Create
        //Capturar Compra
        [HttpPost]
        public ActionResult Create(CapturarCompraViewModels Model)
        {
            try
            {
                _CompraAlmacen_Datos CompraDatos = new _CompraAlmacen_Datos();
                if (ModelState.IsValid)
                {
                    CompraAlmacenModels Compra = new CompraAlmacenModels
                    {
                        Sucursal = new CatSucursalesModels { IDSucursal = Model.IDSucursal },
                        Proveedor = new CatProveedorModels { IDProveedor = Model.IDProveedorAlmacen },
                        NumFacturaNota = Model.NumFactNota,
                        Fecha = Model.FechaCompra,
                        Conexion = Conexion,
                        Opcion = 1,
                        Usuario = User.Identity.Name
                    };
                    Compra = CompraDatos.ACCompraAlmacen(Compra);
                    if (Compra.Completado)
                    {
                        TempData["typemessage"] = "1";
                        TempData["message"] = "El registro se guardó correctamente.";
                        return RedirectToAction("Detail", new { id = Compra.IDCompraAlmacen});
                    }
                    else
                    {
                        Model.ListaProveedor = CompraDatos.ObtenerComboProveedores(Conexion);
                        Model.ListaSucursal = CompraDatos.ObtenerComboSucursales(Conexion);
                        TempData["typemessage"] = "2";
                        TempData["message"] = "Ocurrió un error al guardar el registro.";
                        return View(Model);
                    }
                }
                else
                {
                    return View(Model);
                }

            }
            catch (Exception)
            {
                CompraAlmacenDetallesViewModels Almacen = new CompraAlmacenDetallesViewModels();                
                TempData["typemessage"] = "2";
                TempData["message"] = "No se pudo guardar los datos. Por favor contacte a soporte técnico";
                return View(Almacen);
            }
        }
        [HttpGet]
        public ActionResult Edit(string id)
        {
            try
            {
                Token.SaveToken();
                CapturarCompraViewModels Model = new CapturarCompraViewModels();
                CompraAlmacenModels compraAlmacen = new CompraAlmacenModels();
                _CompraAlmacen_Datos CompraDatos = new _CompraAlmacen_Datos();
                Model.ListaSucursal = CompraDatos.ObtenerComboSucursales(Conexion);
                Model.ListaProveedor = CompraDatos.ObtenerComboProveedores(Conexion);
                compraAlmacen.Conexion = Conexion;
                compraAlmacen.IDCompraAlmacen = id;
                compraAlmacen = CompraDatos.ObtenerCompraAlmacen(compraAlmacen);
                Model.IDSucursal = compraAlmacen.Sucursal.IDSucursal;
                Model.IDProveedorAlmacen = compraAlmacen.Proveedor.IDProveedor;
                Model.NumFactNota = compraAlmacen.NumFacturaNota;
                Model.FechaCompra = compraAlmacen.Fecha;
                return View(Model);
            }
            catch (Exception)
            {
                CapturarCompraViewModels Model = new CapturarCompraViewModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Model);
            }

        }
        // POST: Admin/CompraAlmacen/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, CapturarCompraViewModels Model)
        {
            try
            {
                _CompraAlmacen_Datos CompraDatos = new _CompraAlmacen_Datos();

                if (ModelState.IsValid)
                {
                    CompraAlmacenModels Compra = new CompraAlmacenModels
                    {
                        Sucursal = new CatSucursalesModels { IDSucursal = Model.IDSucursal },
                        Proveedor = new CatProveedorModels { IDProveedor = Model.IDProveedorAlmacen },
                        NumFacturaNota = Model.NumFactNota,
                        Fecha = Model.FechaCompra,
                        Conexion = Conexion,
                        IDCompraAlmacen = id,
                        Opcion = 2,
                        Usuario = User.Identity.Name
                    };
                    Compra = CompraDatos.ACCompraAlmacen(Compra);
                    if (Compra.Completado)
                    {
                        TempData["typemessage"] = "1";
                        TempData["message"] = "El registro se guardó correctamente.";
                        return RedirectToAction("Index", new { id = Compra.IDCompraAlmacen });
                    }
                    else
                    {
                        Model.ListaProveedor = CompraDatos.ObtenerComboProveedores(Conexion);
                        Model.ListaSucursal = CompraDatos.ObtenerComboSucursales(Conexion);
                        TempData["typemessage"] = "2";
                        TempData["message"] = "Ocurrió un error al guardar el registro.";
                        return View(Model);
                    }
                }
                else
                {
                    return View(Model);
                }

            }
            catch (Exception)
            {
                CompraAlmacenModels Almacen = new CompraAlmacenModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se pudo guardar los datos. Por favor contacte a soporte técnico";
                return View(Almacen);
            }
        }
        // GET: Admin/CompraAlmacen/Detail
        // Detalles compra
        public ActionResult Detail(string id)
        {
            try
            {
                CompraAlmacenDetalleModels Detalle = new CompraAlmacenDetalleModels();
                _CompraAlmacen_Datos CompraDatos = new _CompraAlmacen_Datos();
                Detalle.Conexion = Conexion;
                Detalle.IDCompraAlmacen = id;
                Detalle.ListCompraDetalle = CompraDatos.ObtenerGridCompraDetalle(Detalle);
                Detalle.id_status = CompraDatos.ObtenerIdStatus(Detalle);
                //TempData["id_status"] = id_status;
                return View(Detalle);
            }
            catch (Exception)
            {
                CompraAlmacenDetalleModels Detalle = new CompraAlmacenDetalleModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Detalle);
            }
        }
        [HttpGet]
        public ActionResult AddProduct(string id)
        {
            try
            {
                CompraAlmacenDetallesViewModels DetalleCompra = new CompraAlmacenDetallesViewModels();
                _CompraAlmacen_Datos DetalleCompraDatos = new _CompraAlmacen_Datos();
                DetalleCompra.IDCompraAlmacen = id;
                DetalleCompra.Conexion = Conexion;
                DetalleCompra.ListProducto = DetalleCompraDatos.ObtenerComboProducto(DetalleCompra);
                DetalleCompra.ListUnidadMedida = DetalleCompraDatos.ObtenerComboUnidadMedida(DetalleCompra);
                return View(DetalleCompra);
            }
            catch (Exception ex)
            {
                CompraAlmacenDetallesViewModels DetalleCompra = new CompraAlmacenDetallesViewModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(DetalleCompra);
            }
        }
        [HttpPost]
        public ActionResult AddProduct(string id,CompraAlmacenDetallesViewModels Model)
        {
            try
            {
                _CompraAlmacen_Datos datos = new _CompraAlmacen_Datos();
                if(ModelState.IsValid)
                {
                    CompraAlmacenDetalleModels detalle = new CompraAlmacenDetalleModels
                    {
                        Producto = new CatProductosAlmacenModels { IDProductoAlmacen = Model.IDProductoAlmacen },
                        Cantidad = Model.Cantidad,
                        IDUnidadProducto = Model.id_unidadProducto,
                        PrecioUnitario = Model.PrecioUnitario,
                        IDCompraAlmacen = id,
                        Conexion = Conexion,
                        Usuario = User.Identity.Name,
                        Opcion = 1
                    };
                    detalle = datos.ABCCompraAlmacenDetalle(detalle);
                    if(detalle.Completado)
                    {
                        TempData["typemessage"] = "1";
                        TempData["message"] = "El registro se guardó correctamente.";
                        return RedirectToAction("Detail", new { id = detalle.IDCompraAlmacen });
                    }
                    else
                    {
                        Model.Conexion = Conexion;
                        if (detalle.Resultado == 2)
                        {
                            Model.Conexion = Conexion;
                            Model.ListProducto = datos.ObtenerComboProducto(Model);
                            Model.ListUnidadMedida = datos.ObtenerComboUnidadMedida(Model);
                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ya existe un producto con esa unidad de medida seleccionada";
                            return View(Model);
                        }
                        else
                        {
                            Model.ListProducto = datos.ObtenerComboProducto(Model);
                            Model.ListUnidadMedida = datos.ObtenerComboUnidadMedida(Model);
                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrió un error al guardar el registro.";
                            return View(Model);
                        }
                    }
                }
                else
                {
                    return View(Model);
                }
            }
            catch (Exception)
            {
                CompraAlmacenDetallesViewModels Almacen = new CompraAlmacenDetallesViewModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se pudo guardar los datos. Por favor contacte a soporte técnico";
                return View(Almacen);
            }
        }
        [HttpPost]
        public ActionResult Unidad(string id)
        {
            try
            {
                CompraAlmacenDetallesViewModels Model = new CompraAlmacenDetallesViewModels();
                _CompraAlmacen_Datos CompraDatos = new _CompraAlmacen_Datos();
                Model.IDProductoAlmacen = id;
                Model.Conexion = Conexion;
                Model.ListUnidadMedida = CompraDatos.ObtenerComboUnidadMedida(Model);
                return Json(Model.ListUnidadMedida, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Process(string id)
        {
            try
            {
                CompraAlmacenModels Compra = new CompraAlmacenModels();
                _CompraAlmacen_Datos CompraDatos = new _CompraAlmacen_Datos();
                Compra.Conexion = Conexion;
                Compra.IDCompraAlmacen = id;
                Compra.Usuario = User.Identity.Name;
                CompraDatos.ProcesarCompraAlmacen(Compra);
                if (Compra.Completado)
                    return Json("true");
                else
                    return Json("");
            }
            catch (Exception)
            {
                return Json("");
            }
        }

        [HttpPost]
        public ActionResult Cancel(string id, CompraAlmacenModels Compra)
        {
            try
            {
                _CompraAlmacen_Datos CompraDatos = new _CompraAlmacen_Datos();
                Compra.IDCompraAlmacen = id;
                Compra.Conexion = Conexion;
                Compra.Usuario = User.Identity.Name;
                Compra = CompraDatos.DeleteCompraAlmacen(Compra);
                if (Compra.Completado == true)
                {
                    TempData["typemessage"] = "1";
                    TempData["message"] = "El registro se ha eliminado correctamente";
                    return View(Compra);
                }
                else
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Ocurrió un error al eliminar el registro.";
                    return View(Compra);
                }
            }
            catch (Exception ex)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "No se pudo borrar los datos. Por favor contacte a soporte técnico";
                return View(Compra);
            }
        }
        public ActionResult Delete(string id, string id_compraAlmacen, CompraAlmacenDetalleModels Compra)
        {
            try
            {
                _CompraAlmacen_Datos CompraDatos = new _CompraAlmacen_Datos();
                Compra.IDCompraAlmacenDetalle = id;
                Compra.IDCompraAlmacen = id_compraAlmacen;
                Compra.Conexion = Conexion;
                Compra.Usuario = User.Identity.Name;
                Compra = CompraDatos.DeleteCompraAlmacenDetalle(Compra);
                if(Compra.Completado == true)
                {
                    TempData["typemessage"] = "1";
                    TempData["message"] = "El registro se ha eliminado correctamente";
                    return Json("");
                }
                else
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Ocurrió un error al eliminar el registro.";
                    return Json("");
                }
            }
            catch (Exception ex)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "No se pudo borrar los datos. Por favor contacte a soporte técnico";
                return Json("");
            }
        }
    }
}
