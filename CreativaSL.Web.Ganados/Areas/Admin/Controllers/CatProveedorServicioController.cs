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
    public class CatProveedorServicioController : Controller
    {
        private TokenProcessor Token = TokenProcessor.GetInstance();
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
        // GET: Admin/CatProveedorServicio
        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                CatProveedorServicioModels provserv = new CatProveedorServicioModels();
                _CatProveedorServicio_Datos Datos = new _CatProveedorServicio_Datos();
                provserv.Conexion = Conexion;
                provserv.listaProveedorServicio = Datos.ObtenerCatProveedores(provserv);
                return View(provserv);
            }
            catch (Exception)
            {
                CatProveedorServicioModels provserv = new CatProveedorServicioModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(provserv);

            }
            
        }

        // GET: Admin/CatProveedorServicio/Details/5
        
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/CatProveedorServicio/Create
        [HttpGet]
        public ActionResult Create()
        {
            try
            {
                Token.SaveToken();
                _Combos_Datos CMB = new _Combos_Datos();
                CatProveedorServicioModels provserv = new CatProveedorServicioModels();
                provserv.ListaSucursal = CMB.ObtenerComboSucursales(Conexion);
                return View(provserv);
            }
            catch (Exception)
            {
                CatProveedorServicioModels provserv = new CatProveedorServicioModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(provserv);
            }
        }

        // POST: Admin/CatProveedorServicio/Create
        [HttpPost]
        public ActionResult Create(CatProveedorServicioModels provserv)
        {
            _Combos_Datos CMB = new _Combos_Datos();
            _CatProveedorServicio_Datos proveedorDatos = new _CatProveedorServicio_Datos();
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                provserv.ListaSucursal = CMB.ObtenerComboSucursales(Conexion);
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrió un error el intentar guardar. Contacte a soporte técnico";
                return View(provserv);
            }
        }

        // GET: Admin/CatProveedorServicio/Edit/5
        public ActionResult Edit(string id)
        {
            try
            {
                Token.SaveToken();
                _Combos_Datos CMB = new _Combos_Datos();
                CatProveedorServicioModels provservr = new CatProveedorServicioModels();
                _CatProveedorServicio_Datos proveedorDatos = new _CatProveedorServicio_Datos();
                provservr.Conexion = Conexion;
                provservr.id_proveedorServicio = id;
                provservr = proveedorDatos.ObtenerDetalleProveedorServicioxID(provservr);
                provservr.ListaSucursal = CMB.ObtenerComboSucursales(Conexion);
                return View(provservr);

            }
            catch (Exception)
            {

                CatProveedorServicioModels provserv = new CatProveedorServicioModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(provserv);
            }
        }

        // POST: Admin/CatProveedorServicio/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, CatProveedorServicioModels provserv)
        {
            _CatProveedorServicio_Datos proveedorDatos = new _CatProveedorServicio_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        provserv.Conexion = Conexion;
                        provserv.Opcion = 2;
                        provserv.Usuario = User.Identity.Name;
                        provserv = proveedorDatos.AbcCatProveedorServicio(provserv);
                        if (provserv.Completado == true)
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
                            return View(provserv);
                        }
                    }
                    else
                    {
                        provserv.Conexion = Conexion;
                        return View(provserv);
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
                return View(provserv);
            }
        }

        // GET: Admin/CatProveedorServicio/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/CatProveedorServicio/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                CatProveedorServicioModels provserv = new CatProveedorServicioModels();
                _CatProveedorServicio_Datos proveedorDatos = new _CatProveedorServicio_Datos();
                provserv.Conexion = Conexion;
                provserv.id_proveedorServicio = id;
                provserv.Opcion = 3;
                provserv.Usuario = User.Identity.Name;
                provserv = proveedorDatos.EliminarProveedorServicio(provserv);
                if (provserv.Completado == true)
                {
                    TempData["typemessage"] = "1";
                    TempData["message"] = "El registro se elimino correctamente.";
                    return Json("");
                }
                else
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Ocurrió un error al intentar eliminar.";
                    return View(provserv);
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
                _CatProveedorServicio_Datos ProveddorDatos = new _CatProveedorServicio_Datos();
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
                _CatProveedorServicio_Datos ProveedorDatos = new _CatProveedorServicio_Datos();
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
            _CatProveedorServicio_Datos ProveedorDatos = new _CatProveedorServicio_Datos();
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
                _CatProveedorServicio_Datos ProveedorDatos = new _CatProveedorServicio_Datos();
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
            _CatProveedorServicio_Datos ProveedorDatos = new _CatProveedorServicio_Datos();
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
                _CatProveedorServicio_Datos ProveedorDatos = new _CatProveedorServicio_Datos();
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
                CatContactosModels Contacto = new CatContactosModels();
                _CatProveedorServicio_Datos ProveddorDatos = new _CatProveedorServicio_Datos();
                Contacto.Conexion = Conexion;
                Contacto.IDProveedor = id;
                Contacto.listaContacto = ProveddorDatos.ObtenerdatosContactosServicio(Contacto);
                return View(Contacto);
            }
            catch (Exception)
            {

                CatContactosModels Contacto = new CatContactosModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("Index");
            }
        }

        //GET: Admin/CatProveedorAlmacen/ContactoProCreate/3
        [HttpGet]
        public ActionResult ContactoProCreate(string id)
        {
            try
            {
                Token.SaveToken();
                CatContactosModels Contacto = new CatContactosModels();
                Contacto.IDProveedor = id;
                return View(Contacto);
            }
            catch (Exception)
            {
                CatContactosModels Contacto = new CatContactosModels();
                Contacto.IDProveedor = id;
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Contacto);
                //return RedirectToAction("ContactoPro", "CatProvvedorAlmacen", new { id = Contacto.IDProveedor });
            }
        }

        //POST: Admin/CatProveedorAlmacen/ContactoProCreate/3
        [HttpPost]
        public ActionResult ContactoProCreate(CatContactosModels Contacto)
        {
            _CatProveedorServicio_Datos ProveedorDatos = new _CatProveedorServicio_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        Contacto.Conexion = Conexion;
                        Contacto.Usuario = User.Identity.Name;
                        Contacto.Opcion = 1;
                        ProveedorDatos.ACDatosContactoProveedorAlmacen(Contacto);
                        if (Contacto.Completado == true)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "Los datos se guardaron correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("ContactoPro", new { id = Contacto.IDProveedor });
                        }
                        else
                        {
                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrió un error al intentar guardar los datos. Intente más tarde.";
                            return View(Contacto);
                        }
                    }
                    else
                    {
                        return View(Contacto);
                    }
                }
                else
                {
                    return RedirectToAction("ContactoPro", new { id = Contacto.IDProveedor });
                }
            }
            catch (Exception)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error al intentar guardar los datos. Contacte a soporte técnico.";
                return View(Contacto);
            }
        }

        //GET: Admin/CatProvvedorAlmacen/ContactoProEdit/3
        [HttpGet]
        public ActionResult ContactoProEdit(string id, string id2)
        {
            try
            {
                Token.SaveToken();
                CatContactosModels Contacto = new CatContactosModels();
                _CatProveedorServicio_Datos ProveedorDatos = new _CatProveedorServicio_Datos();
                Contacto.IDContacto = id;
                Contacto.IDProveedor = id2;
                Contacto.Conexion = Conexion;
                Contacto = ProveedorDatos.ObtenerDetalleCatcontactoProveedor(Contacto);
                return View(Contacto);
            }
            catch (Exception)
            {
                CatContactosModels Contacto = new CatContactosModels();
                Contacto.IDProveedor = id2;
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("ContactoPro", new { id = Contacto.IDProveedor });
            }
        }

        //POST: Admin/CatProvvedorAlmacen/ContactoProEdit/3
        [HttpPost]
        public ActionResult ContactoProEdit(CatContactosModels Contacto)
        {
            _CatProveedorServicio_Datos ProveedorDatos = new _CatProveedorServicio_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        Contacto.Conexion = Conexion;
                        Contacto.Usuario = User.Identity.Name;
                        Contacto.Opcion = 2;
                        ProveedorDatos.ACDatosContactoProveedorAlmacen(Contacto);
                        if (Contacto.Completado == true)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "Los datos se guardaron correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("ContactoPro", new { id = Contacto.IDProveedor });
                        }
                        else
                        {
                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrió un error al intentar guardar los datos. Intente más tarde.";
                            return View(Contacto);
                        }
                    }
                    else
                    {
                        return View(Contacto);
                    }
                }
                else
                {
                    return RedirectToAction("ContactoPro", new { id = Contacto.IDProveedor });
                }
            }
            catch (Exception)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error al intentar guardar los datos. Contacte a soporte técnico.";
                return View(Contacto);
            }
        }

        // POST: Admin/CatProvvedorAlmacen/ContactoProDelete/5
        [HttpPost]
        public ActionResult ContactoProDelete(string id, string id2)
        {
            try
            {
                CatContactosModels Datos = new CatContactosModels
                {
                    IDProveedor = id2,
                    IDContacto = id,
                    Conexion = Conexion,
                    Usuario = User.Identity.Name
                };
                _CatProveedorServicio_Datos ProveedorDatos = new _CatProveedorServicio_Datos();
                ProveedorDatos.EliminarDatoContactoProveedorAlmacen(Datos);
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
