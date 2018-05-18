using CreativaSL.Web.Ganados.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CreativaSL.Web.Ganados.Filters;
using CreativaSL.Web.Ganados.App_Start;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    [Autorizado]
    public class CatProveedorAlmacenController : Controller
    {
        private TokenProcessor Token = TokenProcessor.GetInstance();
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
        // GET: Admin/CatProveedorAlmacen
        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                CatProveedorAlmacenModels proveedor = new CatProveedorAlmacenModels();
                _CatProveedorAlmacen_Datos proveedorDatos = new _CatProveedorAlmacen_Datos();
                proveedor.Conexion = Conexion;
                proveedor = proveedorDatos.ObtenerListaProveedorAlmacen(proveedor);
                return View(proveedor);
            }
            catch (Exception)
            {

                CatProveedorAlmacenModels proveedor = new CatProveedorAlmacenModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(proveedor);
            }
        }

        // GET: Admin/CatProveedorAlmacen/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/CatProveedorAlmacen/Create
        [HttpGet]
        public ActionResult Create()
        {
            try
            {
                Token.SaveToken();
                _Combos_Datos CMB = new _Combos_Datos();
                CatProveedorAlmacenModels proveedor = new CatProveedorAlmacenModels();
                proveedor.ListaSucursal = CMB.ObtenerComboSucursales(Conexion);
                return View(proveedor);
            }
            catch (Exception)
            {
                CatProveedorAlmacenModels proveedor = new CatProveedorAlmacenModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(proveedor);
            }
        }

        // POST: Admin/CatProveedorAlmacen/Create
        [HttpPost]
        public ActionResult Create(CatProveedorAlmacenModels proveedor)
        {
            _Combos_Datos CMB = new _Combos_Datos();
            _CatProveedorAlmacen_Datos proveedorDatos = new _CatProveedorAlmacen_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        proveedor.Conexion = Conexion;
                        proveedor.Opcion = 1;
                        proveedor.Usuario = User.Identity.Name;
                        proveedor = proveedorDatos.AbcCatProveedorAlmacen(proveedor);
                        if (proveedor.Completado == true)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "Los datos se guardaron correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            proveedor.ListaSucursal = CMB.ObtenerComboSucursales(Conexion);
                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrió un error al intentar guardar.";
                            return View(proveedor);
                        }
                    }
                    else
                    {
                        proveedor.Conexion = Conexion;
                        proveedor.ListaSucursal = CMB.ObtenerComboSucursales(Conexion);
                        return View(proveedor);
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                proveedor.ListaSucursal = CMB.ObtenerComboSucursales(Conexion);
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrió un error el intentar guardar. Contacte a soporte técnico";
                return View(proveedor);
            }
        }

        // GET: Admin/CatProveedorAlmacen/Edit/5
        public ActionResult Edit(string id)
        {
            try
            {
                Token.SaveToken();
                _Combos_Datos CMB = new _Combos_Datos();
                CatProveedorAlmacenModels proveedor = new CatProveedorAlmacenModels();
                _CatProveedorAlmacen_Datos proveedorDatos = new _CatProveedorAlmacen_Datos();
                proveedor.Conexion = Conexion;
                proveedor.IDProveedorAlmacen = id;
                proveedor = proveedorDatos.ObtenerDetalleProveedorAlmacenxID(proveedor);
                proveedor.ListaSucursal = CMB.ObtenerComboSucursales(Conexion);
                return View(proveedor);
            }
            catch (Exception)
            {
                CatProveedorAlmacenModels proveedor = new CatProveedorAlmacenModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(proveedor);
            }
        }

        // POST: Admin/CatProveedorAlmacen/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, CatProveedorAlmacenModels proveedor)
        {
            _CatProveedorAlmacen_Datos proveedorDatos = new _CatProveedorAlmacen_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        proveedor.Conexion = Conexion;
                        proveedor.Opcion = 2;
                        proveedor.Usuario = User.Identity.Name;
                        proveedor = proveedorDatos.AbcCatProveedorAlmacen(proveedor);
                        if (proveedor.Completado == true)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "Los datos se guardaron correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrió un error al intentar guardar.";
                            return View(proveedor);
                        }
                    }
                    else
                    {
                        proveedor.Conexion = Conexion;
                        return View(proveedor);
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrió un error el intentar guardar. Contacte a soporte técnico";
                return View(proveedor);
            }
        }

        // GET: Admin/CatProveedorAlmacen/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/CatProveedorAlmacen/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                CatProveedorAlmacenModels proveedor = new CatProveedorAlmacenModels();
                _CatProveedorAlmacen_Datos proveedorDatos = new _CatProveedorAlmacen_Datos();
                proveedor.Conexion = Conexion;
                proveedor.IDProveedorAlmacen = id;
                proveedor.Opcion = 3;
                proveedor.Usuario= User.Identity.Name;
                proveedor = proveedorDatos.EliminarProveedorAlmacen(proveedor);
                if (proveedor.Completado == true)
                {
                    TempData["typemessage"] = "1";
                    TempData["message"] = "El registro se elimino correctamente.";
                    return Json("");
                }
                else
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Ocurrió un error al intentar eliminar.";
                    return View(proveedor);
                }
            }
            catch
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrió un error el intentar guardar. Contacte a soporte técnico";
                return View();
            }
        }

        //GET: Admin/CatProvvedorAlmacen/DatosBancarios/1
        [HttpGet]
        public ActionResult DatosBancarios(string id)
        {
            try
            {
                CuentaBancariasProveedorAlmacenModels CuentasBancarias = new CuentaBancariasProveedorAlmacenModels();
                _CatProveedorAlmacen_Datos ProveddorDatos = new _CatProveedorAlmacen_Datos();
                CuentasBancarias.Conexion = Conexion;
                CuentasBancarias.IDProveedorAlmacen = id;
                CuentasBancarias.ListaCuentaProveedorAlmacen = ProveddorDatos.ObtenerCuentasBancarias(CuentasBancarias);
                return View(CuentasBancarias);
            }
            catch (Exception)
            {
                CuentaBancariasProveedorAlmacenModels CuentasBancarias = new CuentaBancariasProveedorAlmacenModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("Index");
            }
        }

        //GET: Admin/CatProvvedorAlmacen/DatosBancariosCreate/3
        [HttpGet]
        public ActionResult DatosBancariosCreate(string id)
        {
            try
            {
                Token.SaveToken();
                CuentaBancariasProveedorAlmacenModels Cuenta = new CuentaBancariasProveedorAlmacenModels();
                _CatProveedorAlmacen_Datos ProveedorDatos = new _CatProveedorAlmacen_Datos();
                Cuenta.IDProveedorAlmacen = id;
                Cuenta.Conexion = Conexion;
                Cuenta.ListaCmbBancos = ProveedorDatos.ObteneComboCatBancos(Cuenta);
                return View(Cuenta);
            }
            catch (Exception)
            {
                CuentaBancariasProveedorAlmacenModels Cuenta = new CuentaBancariasProveedorAlmacenModels();
                Cuenta.IDProveedorAlmacen = id;
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Cuenta);
                //return RedirectToAction("DatosBancarios", new { id = Cuenta.IDProveedorAlmacen });
            }
        }

        //POST:Admin/CatProvvedorAlmacen/DatosBancariosCreate/3
        [HttpPost]
        public ActionResult DatosBancariosCreate(CuentaBancariasProveedorAlmacenModels IDCuentaBancoP)
        {
            _CatProveedorAlmacen_Datos ProveedorDatos = new _CatProveedorAlmacen_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        IDCuentaBancoP.Conexion = Conexion;
                        IDCuentaBancoP.Usuario = User.Identity.Name;
                        IDCuentaBancoP.Opcion = 1;
                        ProveedorDatos.ACDatosBancariosProveedorAlmacen(IDCuentaBancoP);
                        if (IDCuentaBancoP.Completado == true)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "Los datos se guardaron correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("DatosBancarios", new { id = IDCuentaBancoP.IDProveedorAlmacen });
                        }
                        else
                        {
                            IDCuentaBancoP.ListaCmbBancos = ProveedorDatos.ObteneComboCatBancos(IDCuentaBancoP);
                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrió un error al intentar guardar los datos. Intente más tarde.";
                            return View(IDCuentaBancoP);
                        }
                    }
                    else
                    {
                        IDCuentaBancoP.Conexion = Conexion;
                        IDCuentaBancoP.ListaCmbBancos = ProveedorDatos.ObteneComboCatBancos(IDCuentaBancoP);
                        return View(IDCuentaBancoP);
                    }
                }
                else
                {
                    return RedirectToAction("DatosBancarios", new { id = IDCuentaBancoP.IDProveedorAlmacen });
                }
            }
            catch (Exception)
            {
                IDCuentaBancoP.Conexion = Conexion;
                IDCuentaBancoP.ListaCmbBancos = ProveedorDatos.ObteneComboCatBancos(IDCuentaBancoP);
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error al intentar guardar los datos. Contacte a soporte técnico.";
                return View(IDCuentaBancoP);
            }
        }

        //GET: Admin/CatProvvedorAlmacen/DatosBancariosEdit/3
        [HttpGet]
        public ActionResult DatosBancariosEdit(string id, string id2)
        {
            try
            {
                Token.SaveToken();
                CuentaBancariasProveedorAlmacenModels Cuenta = new CuentaBancariasProveedorAlmacenModels();
                _CatProveedorAlmacen_Datos ProveedorDatos = new _CatProveedorAlmacen_Datos();
                Cuenta.IDDatosBancarios = id;
                Cuenta.IDProveedorAlmacen = id2;
                Cuenta.Conexion = Conexion;
                Cuenta = ProveedorDatos.ObtenerDetalleCuentaBancaria(Cuenta);
                Cuenta.ListaCmbBancos = ProveedorDatos.ObteneComboCatBancos(Cuenta);
                return View(Cuenta);
            }
            catch (Exception)
            {
                CuentaBancariasProveedorAlmacenModels Cuenta = new CuentaBancariasProveedorAlmacenModels();
                Cuenta.IDProveedorAlmacen = id2;
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("DatosBancarios", new { id = Cuenta.IDProveedorAlmacen });
            }
        }

        //POST: Admin/CatProvvedorAlmacen/DatosBancariosEdit/3
        [HttpPost]
        public ActionResult DatosBancariosEdit(CuentaBancariasProveedorAlmacenModels IDCuentaBancoP)
        {
            _CatProveedorAlmacen_Datos ProveedorDatos = new _CatProveedorAlmacen_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        IDCuentaBancoP.Conexion = Conexion;
                        IDCuentaBancoP.Usuario = User.Identity.Name;
                        IDCuentaBancoP.Opcion = 2;
                        ProveedorDatos.ACDatosBancariosProveedorAlmacen(IDCuentaBancoP);
                        if (IDCuentaBancoP.Completado == true)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "Los datos se guardaron correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("DatosBancarios", new { id = IDCuentaBancoP.IDProveedorAlmacen });
                        }
                        else
                        {
                            IDCuentaBancoP.ListaCmbBancos = ProveedorDatos.ObteneComboCatBancos(IDCuentaBancoP);
                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrió un error al intentar guardar los datos. Intente más tarde.";
                            return View(IDCuentaBancoP);
                        }
                    }
                    else
                    {
                        IDCuentaBancoP.Conexion = Conexion;
                        IDCuentaBancoP.ListaCmbBancos = ProveedorDatos.ObteneComboCatBancos(IDCuentaBancoP);
                        return View(IDCuentaBancoP);
                    }
                }
                else
                {
                    return RedirectToAction("DatosBancarios", new { id = IDCuentaBancoP.IDProveedorAlmacen });
                }
            }
            catch (Exception)
            {
                IDCuentaBancoP.Conexion = Conexion;
                IDCuentaBancoP.ListaCmbBancos = ProveedorDatos.ObteneComboCatBancos(IDCuentaBancoP);
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error al intentar guardar los datos. Contacte a soporte técnico.";
                return View(IDCuentaBancoP);
            }
        }

        // POST: Admin/CatProvvedorAlmacen/DatosBancariosDelete/5
        [HttpPost]
        public ActionResult DatosBancariosDelete(string id, string id2)
        {
            try
            {
                CuentaBancariasProveedorAlmacenModels Datos = new CuentaBancariasProveedorAlmacenModels
                {
                    IDProveedorAlmacen = id2,
                    IDDatosBancarios = id,
                    Conexion = Conexion,
                    Usuario = User.Identity.Name
                };
                _CatProveedorAlmacen_Datos ProveedorDatos = new _CatProveedorAlmacen_Datos();
                ProveedorDatos.EliminarDatosBancariosProveedorAlmacen(Datos);
                if (Datos.Completado)
                {
                    TempData["typemessage"] = "1";
                    TempData["message"] = "El registro se ha eliminado correctamente";
                    return Json("");
                }
                else
                { return Json(""); }
            }
            catch
            {
                return View();
            }
        }

        //GET: Admin/CatProvvedorAlmacen/ContactoPro/1
        [HttpGet]
        public ActionResult ContactoPro(string id)
        {
            try
            {
                //CuentaBancariasProveedorAlmacenModels CuentasBancarias = new CuentaBancariasProveedorAlmacenModels();
                //_CatProveedorAlmacen_Datos ProveddorDatos = new _CatProveedorAlmacen_Datos();
                //CuentasBancarias.Conexion = Conexion;
                //CuentasBancarias.IDProveedorAlmacen = id;
                //CuentasBancarias.ListaCuentaProveedorAlmacen = ProveddorDatos.ObtenerCuentasBancarias(CuentasBancarias);
                return View();
            }
            catch (Exception)
            {
                return View();
                //CuentaBancariasProveedorAlmacenModels CuentasBancarias = new CuentaBancariasProveedorAlmacenModels();
                //TempData["typemessage"] = "2";
                //TempData["message"] = "No se puede cargar la vista";
                //return RedirectToAction("Index");
            }
        }

    }
}
