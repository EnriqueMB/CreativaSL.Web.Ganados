using CreativaSL.Web.Ganados.Filters;
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
                    Compra = CompraDatos.ABCCompraAlmacen(Compra);
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
                CompraAlmacenModels Almacen = new CompraAlmacenModels();                
                TempData["typemessage"] = "2";
                TempData["message"] = "No se pudo guardar los datos. Por favor contacte a soporte técnico";
                return View(Almacen);
            }
        }

        // GET: Admin/CompraAlmacen/Detail
        public ActionResult Detail(string id)
        {
            try
            {
                CompraAlmacenModels Compra = new CompraAlmacenModels();
                _CompraAlmacen_Datos CompraDatos = new _CompraAlmacen_Datos();
                Compra.Conexion = Conexion;
                Compra.IDCompraAlmacen = id;
                return View(Compra);
            }
            catch (Exception)
            {
                //Mensajes de error al cargar vista
                CatAlmacenModels Almacen = new CatAlmacenModels();
                return View(Almacen);
            }
        }

        // GET: Admin/CompraAlmacen/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/CompraAlmacen/Edit/5
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

        // GET: Admin/CompraAlmacen/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/CompraAlmacen/Delete/5
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
    }
}
