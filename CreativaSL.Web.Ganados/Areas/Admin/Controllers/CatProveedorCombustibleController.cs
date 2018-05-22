using CreativaSL.Web.Ganados.App_Start;
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
    public class CatProveedorCombustibleController : Controller
    {
        private TokenProcessor Token = TokenProcessor.GetInstance();
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
        // GET: Admin/CatProveedorCombustible
        public ActionResult Index()
        {
            try
            {
                _Combos_Datos cmb = new _Combos_Datos();
                CatProveedorCombustibleModels combustibleModels = new CatProveedorCombustibleModels();
                _CatProveedorCombustible_Datos datos = new _CatProveedorCombustible_Datos();
                combustibleModels.Conexion = Conexion;
                combustibleModels.listaProveedoresCombustible = datos.ObtenerCatProveedores(combustibleModels);
                //combustibleModels = 
                return View(combustibleModels);
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        // GET: Admin/CatProveedorCombustible/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/CatProveedorCombustible/Create
        public ActionResult Create()
        {
            try
            {
                Token.SaveToken();
                _Combos_Datos cmb = new _Combos_Datos();
                CatProveedorCombustibleModels combustibleModels = new CatProveedorCombustibleModels();
                _CatProveedorCombustible_Datos datos = new _CatProveedorCombustible_Datos();
                combustibleModels.Conexion = Conexion;
                combustibleModels.listaSucursal = cmb.ObtenerComboSucursales(combustibleModels.Conexion);
               
                return View(combustibleModels);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // POST: Admin/CatProveedorCombustible/Create
        [HttpPost]
        public ActionResult Create(CatProveedorCombustibleModels combustibleModels)
        {
            _CatProveedorCombustible_Datos Proovedordatos = new _CatProveedorCombustible_Datos();
            _Combos_Datos cmb = new _Combos_Datos();
            combustibleModels.Conexion = Conexion;
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {

                        
                        combustibleModels.Usuario = User.Identity.Name;
                        combustibleModels.Opcion = 1;
                        combustibleModels = Proovedordatos.acCatProveedorCombustible(combustibleModels);
                        if (combustibleModels.Completado)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "Los datos se guardaron correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            
                            combustibleModels.listaSucursal = cmb.ObtenerComboSucursales(combustibleModels.Conexion);

                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrio un error al intentar guardar los datos. Intente más tarde.";
                            return View(combustibleModels);
                        }
                    }
                    else
                    {
                        
                        combustibleModels.listaSucursal = cmb.ObtenerComboSucursales(combustibleModels.Conexion);

                        return View(combustibleModels);
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
              
                combustibleModels.listaSucursal = cmb.ObtenerComboSucursales(combustibleModels.Conexion);
                
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error al intentar guardar los datos. Contacte a soporte técnico.";
                return View(combustibleModels);
            }
        }

        // GET: Admin/CatProveedorCombustible/Edit/5
        public ActionResult Edit(string id)
        {
            try
            {
                Token.SaveToken();
                _Combos_Datos cmb = new _Combos_Datos();
                CatProveedorCombustibleModels combustibleModels = new CatProveedorCombustibleModels();
                _CatProveedorCombustible_Datos datos = new _CatProveedorCombustible_Datos();
                combustibleModels.Conexion = Conexion;
                combustibleModels.IDProveedor = id;
                combustibleModels.listaSucursal = cmb.ObtenerComboSucursales(combustibleModels.Conexion);
                combustibleModels = datos.ObtenerDetalleCatProveedor(combustibleModels);

                return View(combustibleModels);
            }
            catch (Exception ex)
            {
                CatProveedorModels Proveedor = new CatProveedorModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("Index");
            }
        }

        // POST: Admin/CatProveedorCombustible/Edit/5
        [HttpPost]
        public ActionResult Edit(CatProveedorCombustibleModels combustibleModels)
        {
            _CatProveedorCombustible_Datos Proovedordatos = new _CatProveedorCombustible_Datos();
            _Combos_Datos cmb = new _Combos_Datos();
            combustibleModels.Conexion = Conexion;
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {


                        combustibleModels.Usuario = User.Identity.Name;
                        combustibleModels.Opcion = 2;
                        combustibleModels = Proovedordatos.acCatProveedorCombustible(combustibleModels);
                        if (combustibleModels.Completado)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "Los datos se guardaron correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("Index");
                        }
                        else
                        {

                            combustibleModels.listaSucursal = cmb.ObtenerComboSucursales(combustibleModels.Conexion);

                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrio un error al intentar guardar los datos. Intente más tarde.";
                            return View(combustibleModels);
                        }
                    }
                    else
                    {

                        combustibleModels.listaSucursal = cmb.ObtenerComboSucursales(combustibleModels.Conexion);

                        return View(combustibleModels);
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {

                combustibleModels.listaSucursal = cmb.ObtenerComboSucursales(combustibleModels.Conexion);

                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error al intentar guardar los datos. Contacte a soporte técnico.";
                return View(combustibleModels);
            }
        }

        // GET: Admin/CatProveedorCombustible/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/CatProveedorCombustible/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                CatProveedorCombustibleModels Proveedor = new CatProveedorCombustibleModels();
                _CatProveedorCombustible_Datos ProveedorDatos = new _CatProveedorCombustible_Datos();
                Proveedor.Conexion = Conexion;
                Proveedor.IDProveedor = id;
                Proveedor.Usuario = User.Identity.Name;
                Proveedor = ProveedorDatos.EliminarProveedorCombustible(Proveedor);

                return Json("");
                // TODO: Add delete logic here


            }
            catch
            {
                CatProductosModels Producto = new CatProductosModels();

                TempData["typemessage"] = "2";
                TempData["message"] = "No se pudo borrar los datos. Por favor contacte a soporte técnico";
                return Json("");

            }
        }
        [HttpGet]
        public ActionResult Cuentas(string id)
        {
            try
            {
                CuentaBancariaProveedorModels Cuenta = new CuentaBancariaProveedorModels();
                _CatProveedorCombustible_Datos ProveedorDatos = new _CatProveedorCombustible_Datos();
                Cuenta.Conexion = Conexion;
                Cuenta.IDProveedor = id;
                Cuenta.ListaCuentaBancaria = ProveedorDatos.ObtenerCuentasBancarias(Cuenta);
                return View(Cuenta);
            }
            catch (Exception)
            {
                CuentaBancariaProveedorModels Cuenta = new CuentaBancariaProveedorModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("Index");
            }
        }
        //GET: Admin/CatProveedor/CreateCuenta/3
        [HttpGet]
        public ActionResult CreateCuenta(string id)
        {
            try
            {
                Token.SaveToken();
                CuentaBancariaProveedorModels Cuenta = new CuentaBancariaProveedorModels();
                _CatProveedorCombustible_Datos ProveedorDatos = new _CatProveedorCombustible_Datos();
                Cuenta.IDProveedor = id;
                Cuenta.Conexion = Conexion;
                Cuenta.ListaCmbBancos = ProveedorDatos.ObteneComboCatBancos(Cuenta);
                return View(Cuenta);
            }
            catch (Exception)
            {
                CuentaBancariaProveedorModels Cuenta = new CuentaBancariaProveedorModels();
                Cuenta.IDProveedor = id;
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("Cuentas", new { id = Cuenta.IDProveedor });
            }
        }

        //POST:Admin/CatProveedor/createCuenta/3
        [HttpPost]
        public ActionResult CreateCuenta(string id, CuentaBancariaProveedorModels IDCuentaBancoP)
        {
            _CatProveedorCombustible_Datos ProveedorDatos = new _CatProveedorCombustible_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        IDCuentaBancoP.Conexion = Conexion;
                        IDCuentaBancoP.Usuario = User.Identity.Name;
                        IDCuentaBancoP.Opcion = 1;
                        ProveedorDatos.ACDatosBancariosProveedor(IDCuentaBancoP);
                        if (IDCuentaBancoP.Completado == true)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "Los datos se guardaron correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("Cuentas", new { id = IDCuentaBancoP.IDProveedor });
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
                    return RedirectToAction("Cuentas", new { id = IDCuentaBancoP.IDProveedor });
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
        //GET: Admin/CatProveedor/EditarCuenta/3
        [HttpGet]
        public ActionResult EditCuenta(string id, string id2)
        {
            try
            {
                Token.SaveToken();
                CuentaBancariaProveedorModels Cuenta = new CuentaBancariaProveedorModels();
                _CatProveedorCombustible_Datos ProveedorDatos = new _CatProveedorCombustible_Datos();
                Cuenta.IDDatosBancarios = id;
                Cuenta.IDProveedor = id2;
                Cuenta.Conexion = Conexion;
                Cuenta = ProveedorDatos.ObtenerDetalleCuentaBancaria(Cuenta);
                Cuenta.ListaCmbBancos = ProveedorDatos.ObteneComboCatBancos(Cuenta);
                return View(Cuenta);
            }
            catch (Exception)
            {
                CuentaBancariaProveedorModels Cuenta = new CuentaBancariaProveedorModels();
                Cuenta.IDProveedor = id2;
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("Cuentas", new { id = Cuenta.IDProveedor });
            }
        }

        //POST: Admin/CatProveedor/EditarCuenta/3
        [HttpPost]
        public ActionResult EditCuenta(string id, string id2, CuentaBancariaProveedorModels IDCuentaBancoP)
        {
            _CatProveedorCombustible_Datos ProveedorDatos = new _CatProveedorCombustible_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        IDCuentaBancoP.Conexion = Conexion;
                        IDCuentaBancoP.Usuario = User.Identity.Name;
                        IDCuentaBancoP.Opcion = 2;
                        ProveedorDatos.ACDatosBancariosProveedor(IDCuentaBancoP);
                        if (IDCuentaBancoP.Completado == true)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "Los datos se guardaron correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("Cuentas", new { id = IDCuentaBancoP.IDProveedor });
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
                    return RedirectToAction("Cuentas", new { id = IDCuentaBancoP.IDProveedor });
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
        // POST: Admin/CatProvedor/Delete/5
        [HttpPost]
        public ActionResult DeleteCuenta(string id, string id2)
        {
            try
            {
                CuentaBancariaProveedorModels Datos = new CuentaBancariaProveedorModels
                {
                    IDProveedor = id2,
                    IDDatosBancarios = id,
                    Conexion = Conexion,
                    Usuario = User.Identity.Name
                };
                _CatProveedorCombustible_Datos ProveedorDatos = new _CatProveedorCombustible_Datos();
                ProveedorDatos.EliminarDatosBancariosProveedor(Datos);
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
        [HttpGet]
        public ActionResult DatosContacto(string id, string id2)
        {

            try
            {
                Token.SaveToken();
                CatProveedorCombustibleModels Proveedor = new CatProveedorCombustibleModels();
                _CatProveedorCombustible_Datos ProveedorDatos = new _CatProveedorCombustible_Datos();
                Proveedor.IDProveedor = id;
                Proveedor.IDSucursal = id2;
                Proveedor.Conexion = Conexion;
                Proveedor.listaDatosContactos = ProveedorDatos.obtenerDatosContacto(Proveedor);

                return View(Proveedor);
            }
            catch (Exception ex)
            {
                CatProveedorModels Proveedor = new CatProveedorModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error al intentar cargar los datos. Contacte a soporte técnico.";
                return View(Proveedor);

            }
        }
        [HttpGet]
        public ActionResult NuevoDatosContactoEdit(string id, string id2, string id3)
        {
            try
            {
                Token.SaveToken();
                CatContactosModels Contactos = new CatContactosModels();
                _CatProveedorCombustible_Datos ProveedorDatos = new _CatProveedorCombustible_Datos();
                Contactos.IDProveedor = id;
                Contactos.IDSucursal = id2;
                Contactos.IDContacto = id3;
                Contactos.Conexion = Conexion;
                Contactos = ProveedorDatos.ObtenerDetalleCatDatosXProveedor(Contactos);
                return View(Contactos);
            }
            catch (Exception ex)
            {
                CatProveedorModels Proveedor = new CatProveedorModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error al intentar cargar los datos. Contacte a soporte técnico.";
                return View(Proveedor);

            }
        }
        [HttpPost]
        public ActionResult NuevoDatosContactoEdit(CatContactosModels Contactos)
        {
            try
            {

                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        _CatProveedorCombustible_Datos ProveedorDatos = new _CatProveedorCombustible_Datos();

                        Contactos.Conexion = Conexion;
                        Contactos.Usuario = User.Identity.Name;
                        Contactos.Opcion = 2;
                        Contactos = ProveedorDatos.AcContactoProveedor(Contactos);
                        if (Contactos.Completado)
                        {

                            TempData["typemessage"] = "1";
                            TempData["message"] = "Los datos se guardaron correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("DatosContacto", "CatProveedorCombustible", new { id = Contactos.IDProveedor, id2 = Contactos.IDSucursal });
                        }
                        else
                        {

                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrió un error al intentar guardar los datos. Intente más tarde.";
                            return RedirectToAction("NuevoDatosContacto", "CatProveedorCombustible", new { id = Contactos.IDProveedor, id2 = Contactos.IDSucursal });
                        }
                    }
                    else
                    {
                        TempData["typemessage"] = "2";
                        TempData["message"] = "Ocurrió un error al intentar guardar los datos. Intente más tarde.";
                        return View(Contactos);
                    }
                }
                else
                {
                    return RedirectToAction("NuevoDatosContacto", "CatProveedorCombustible", new { id = Contactos.IDProveedor, id2 = Contactos.IDSucursal });
                }



            }
            catch (Exception ex)
            {
                CatProveedorModels Proveedor = new CatProveedorModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error al intentar cargar los datos. Contacte a soporte técnico.";
                return View(Proveedor);

            }
        }

        [HttpGet]
        public ActionResult NuevoDatosContacto(string id, string id2)
        {
            try
            {
                Token.SaveToken();
                CatContactosModels Contactos = new CatContactosModels();
                _CatProveedorCombustible_Datos ProveedorDatos = new _CatProveedorCombustible_Datos();
                Contactos.IDProveedor = id;
                Contactos.IDSucursal = id2;
                Contactos.Conexion = Conexion;

                return View(Contactos);
            }
            catch (Exception ex)
            {
                CatProveedorModels Proveedor = new CatProveedorModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error al intentar cargar los datos. Contacte a soporte técnico.";
                return View(Proveedor);

            }
        }
        [HttpPost]
        public ActionResult NuevoDatosContacto(CatContactosModels Contactos)
        {
            try
            {

                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        _CatProveedorCombustible_Datos ProveedorDatos = new _CatProveedorCombustible_Datos();

                        Contactos.Conexion = Conexion;
                        Contactos.Usuario = User.Identity.Name;
                        Contactos.Opcion = 1;
                        Contactos = ProveedorDatos.AcContactoProveedor(Contactos);
                        if (Contactos.Completado)
                        {

                            TempData["typemessage"] = "1";
                            TempData["message"] = "Los datos se guardaron correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("DatosContacto", "CatProveedorCombustible", new { id = Contactos.IDProveedor, id2 = Contactos.IDSucursal });
                        }
                        else
                        {

                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrió un error al intentar guardar los datos. Intente más tarde.";
                            return RedirectToAction("NuevoDatosContacto", "CatProveedorCombustible", new { id = Contactos.IDProveedor, id2 = Contactos.IDSucursal });
                        }
                    }
                    else
                    {
                        TempData["typemessage"] = "2";
                        TempData["message"] = "Ocurrió un error al intentar guardar los datos. Intente más tarde.";
                        return View(Contactos);
                    }
                }
                else
                {
                    return RedirectToAction("NuevoDatosContacto", "CatProveedorCombustible", new { id = Contactos.IDProveedor, id2 = Contactos.IDSucursal });
                }



            }
            catch (Exception ex)
            {
                CatProveedorModels Proveedor = new CatProveedorModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error al intentar cargar los datos. Contacte a soporte técnico.";
                return View(Proveedor);

            }
        }
        // POST: Admin/CatProvedor/Delete/5
        [HttpPost]
        public ActionResult DeleteDatosContacto(string id, string id2,string id3)
        {
            try
            {
                CatContactosModels Datos = new CatContactosModels
                {
                    IDProveedor = id,
                    IDContacto = id3,
                    Conexion = Conexion,
                    Usuario = User.Identity.Name
                };
                _CatProveedorCombustible_Datos ProveedorDatos = new _CatProveedorCombustible_Datos();
                ProveedorDatos.EliminarProveedorDatosContacto(Datos);
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

    }
}
