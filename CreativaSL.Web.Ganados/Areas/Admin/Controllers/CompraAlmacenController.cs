using CreativaSL.Web.Ganados.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    public class CompraAlmacenController : Controller
    {
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");

        // GET: Admin/CompraAlmacen
        public ActionResult Index()
        {
            try
            {
                CompraAlmacenModels Compras = new CompraAlmacenModels();
                string IdSucursal = string.Empty;
                Compras.ListaCompras = this.ObtenerGridCompras(IdSucursal);
                return View(Compras);
            }
            catch(Exception)
            {
                CompraAlmacenModels Compras = new CompraAlmacenModels();
                return View(Compras);
            }
        }

        // GET: Admin/CompraAlmacen/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/CompraAlmacen/Create
        public ActionResult Create()
        {
            try
            {
                CompraAlmacenModels Almacen = new CompraAlmacenModels();
                Almacen.ListaSucursales = this.ObtenerComboSucursales();
                Almacen.ListaProveedores = this.ObtenerComboProveedores();
                var list = new SelectList(Almacen.ListaSucursales, "IDSucursal", "NombreSucursal");
                var list2 = new SelectList(Almacen.ListaProveedores, "IDProveedor", "NombreRazonSocial");
                ViewData["cmbSucursal"] = list;
                ViewData["cmbProveedor"] = list2;
                return View(Almacen);
            }
            catch (Exception)
            {
                //Mensajes de error al cargar vista
                CatAlmacenModels Almacen = new CatAlmacenModels();
                return View(Almacen);
            }
        }

        // POST: Admin/CompraAlmacen/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                CompraAlmacenModels Compra = new CompraAlmacenModels();
                CatChofer_Datos ChoferDatos = new CatChofer_Datos();
                Compra.Conexion = Conexion;
                //Almacen.Licencia = collection["Licencia"].StartsWith("true");
                Compra.NumFacturaNota = collection["NumFacturaNota"];
                //Almacen.vigencia = DateTime.ParseExact(collection["vigencia"], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                //Chofer.Nombre = collection["nombre"];
                //Chofer.ApPaterno = collection["ApPaterno"];
                //Chofer.ApMaterno = collection["ApMaterno"];
                Compra.Usuario = User.Identity.Name;
                //Chofer = ChoferDatos.AbcCatChofer(Chofer);
                //Si abc fue completado correctamente
                Compra.Completado = true;
                Compra.IDCompraAlmacen = "COMPRA0001";
                if (Compra.Completado == true)
                {
                    TempData["typemessage"] = "1";
                    TempData["message"] = "El registro se guardó correctamente.";
                    return RedirectToAction("Detail", new { id=Compra.IDCompraAlmacen } );
                }
                else
                {
                    Compra.ListaProveedores = this.ObtenerComboProveedores();
                    Compra.ListaSucursales = this.ObtenerComboSucursales();
                    var list = new SelectList(Compra.ListaSucursales, "IDSucursal", "NombreSucursal");
                    var list2 = new SelectList(Compra.ListaProveedores, "IDProveedor", "NombreRazonSocial");
                    ViewData["cmbSucursal"] = list;
                    ViewData["cmbProveedor"] = list2;
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Ocurrió un error al guardar el registro.";
                    return View(Compra);
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

        #region Métodos

        /// <summary>
        /// Obtener el listado de compras de una sucursal
        /// </summary>
        /// <param name="IdSucursal">Id de la sucursal de la que se requieren las compras</param>
        /// <returns></returns>
        private List<CompraAlmacenModels> ObtenerGridCompras(string IdSucursal)
        {
            try
            {
                List<CompraAlmacenModels> Lista = new List<CompraAlmacenModels>();
                Lista.Add(new CompraAlmacenModels { NumFacturaNota = "FCT-19283", Proveedor = new CatProveedorModels { NombreRazonSocial = "INSUMMOS DEL SURESTE S. A. DE C. V." }, IDEstatusCompra = 1, MontoTotal = 9785.90m });
                Lista.Add(new CompraAlmacenModels { NumFacturaNota = "FCT-09323", Proveedor = new CatProveedorModels { NombreRazonSocial = "AGROINSA S. A. DE C. V." }, IDEstatusCompra = 2, MontoTotal = 5423.47m });
                Lista.Add(new CompraAlmacenModels { NumFacturaNota = "FCT-87683", Proveedor = new CatProveedorModels { NombreRazonSocial = "SUPPORT S. A. DE C. V." }, IDEstatusCompra = 3, MontoTotal = 3289.47m });
                return Lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtener el listado de sucursales para mostrarlo en una lista desplegable
        /// </summary>
        /// <returns></returns>
        private List<CatSucursalesModels> ObtenerComboSucursales()
        {
            try
            {
                List<CatSucursalesModels> Lista = new List<CatSucursalesModels>();
                Lista.Add(new CatSucursalesModels { IDSucursal = "SUC01", NombreSucursal = "Matriz Grupo Ocampo" });
                Lista.Add(new CatSucursalesModels { IDSucursal = "SUC02", NombreSucursal = "Sucursal Frailesca" });
                return Lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtener el listado de proveedores para mostrarlo en una lista desplegable
        /// </summary>
        /// <returns></returns>
        private List<CatProveedorModels> ObtenerComboProveedores()
        {
            try
            {
                List<CatProveedorModels> Lista = new List<CatProveedorModels>();
                Lista.Add(new CatProveedorModels { IDProveedor = "PROV01", NombreRazonSocial = "INSUMMOS DEL SURESTE S. A. DE C. V." });
                Lista.Add(new CatProveedorModels { IDProveedor = "PROV02", NombreRazonSocial = "AGROINSA S. A. DE C. V." });
                Lista.Add(new CatProveedorModels { IDProveedor = "PROV02", NombreRazonSocial = "SUPPORT S. A. DE C. V." });
                return Lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


    }
}
