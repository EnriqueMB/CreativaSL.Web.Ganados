using CreativaSL.Web.Ganados.Filters;
using CreativaSL.Web.Ganados.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    [Autorizado]
    public class CatProveedorController : Controller
    {
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
        // GET: Admin/CatProveedor
        public ActionResult Index()
        {
            try
            {
                CatProveedorModels Proveedor = new CatProveedorModels();
                _CatProveedor_Datos ProveedorDatos = new _CatProveedor_Datos();
                Proveedor.Conexion = Conexion;
                Proveedor.listaProveedores = ProveedorDatos.ObtenerCatProveedores(Proveedor);
                return View(Proveedor);
            }
            catch (Exception ex)
            {
                CatProveedorModels Proveedor = new CatProveedorModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Proveedor);
            }
        }

        // GET: Admin/CatProveedor/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/CatProveedor/Create
        public ActionResult Create()
        {
            try
            {
                CatProveedorModels Proveedor = new CatProveedorModels();
                _CatProveedor_Datos ProveedorDatos = new _CatProveedor_Datos();
                Proveedor.Conexion = Conexion;
                Proveedor.listaSucursal = ProveedorDatos.obtenerListaSucursales(Proveedor);
                Proveedor.listaTipoProveedor = ProveedorDatos.obtenerListaTipoProveedor(Proveedor);
                Proveedor.ListaGeneroCMB = ProveedorDatos.ObteneComboCatGenero(Proveedor);
                return View(Proveedor);
            }
            catch (Exception ex)
            {
                CatProveedorModels Proveedor = new CatProveedorModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("Index");
            }
        }

        // POST: Admin/CatProveedor/Create
        [HttpPost]
        public ActionResult Create(CatProveedorModels Proveedor)
        {
            _CatProveedor_Datos ProveedorDatos = new _CatProveedor_Datos();
            try
            {
                if (ModelState.IsValid)
                {
                    HttpPostedFileBase bannerImage = Request.Files[0] as HttpPostedFileBase;
                    if (bannerImage != null && bannerImage.ContentLength > 0)
                    {
                        Stream s = bannerImage.InputStream;
                        Bitmap img = new Bitmap(s);
                        Proveedor.ImgINE = img.ToBase64String(ImageFormat.Png);
                    }
                    HttpPostedFileBase bannerImage2 = Request.Files[1] as HttpPostedFileBase;
                    if (bannerImage2 != null && bannerImage2.ContentLength > 0)
                    {
                        Stream s = bannerImage2.InputStream;
                        Bitmap img = new Bitmap(s);
                        Proveedor.ImgManifestacionFierro = img.ToBase64String(ImageFormat.Png);
                    }
                    Proveedor.Conexion = Conexion;
                    Proveedor.Usuario = User.Identity.Name;
                    Proveedor.Opcion = 1;
                    Proveedor = ProveedorDatos.AcCatProveedor(Proveedor);
                    if (Proveedor.Completado)
                    {
                        TempData["typemessage"] = "1";
                        TempData["message"] = "Los datos se guardaron correctamente.";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        Proveedor.listaSucursal = ProveedorDatos.obtenerListaSucursales(Proveedor);
                        Proveedor.listaTipoProveedor = ProveedorDatos.obtenerListaTipoProveedor(Proveedor);
                        Proveedor.ListaGeneroCMB = ProveedorDatos.ObteneComboCatGenero(Proveedor);
                        TempData["typemessage"] = "2";
                        TempData["message"] = "Ocurrio un error al intentar guardar los datos. Intente más tarde.";
                        return View(Proveedor);
                    }
                }
                else
                {
                    Proveedor.Conexion = Conexion;
                    Proveedor.listaSucursal = ProveedorDatos.obtenerListaSucursales(Proveedor);
                    Proveedor.listaTipoProveedor = ProveedorDatos.obtenerListaTipoProveedor(Proveedor);
                    Proveedor.ListaGeneroCMB = ProveedorDatos.ObteneComboCatGenero(Proveedor);
                    return View(Proveedor);
                }
            }
            catch (Exception ex)
            {
                Proveedor.Conexion = Conexion;
                Proveedor.listaSucursal = ProveedorDatos.obtenerListaSucursales(Proveedor);
                Proveedor.listaTipoProveedor = ProveedorDatos.obtenerListaTipoProveedor(Proveedor);
                Proveedor.ListaGeneroCMB = ProveedorDatos.ObteneComboCatGenero(Proveedor);
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error al intentar guardar los datos. Contacte a soporte técnico.";
                return View(Proveedor);
            }
        }

        // GET: Admin/CatProveedor/Edit/5
        public ActionResult Edit(string id)
        {
            try
            {
                CatProveedorModels Proveedor = new CatProveedorModels();
                _CatProveedor_Datos ProveedorDatos = new _CatProveedor_Datos();
                Proveedor.IDProveedor = id;
                Proveedor.Conexion = Conexion;
                Proveedor = ProveedorDatos.ObtenerDetalleCatProveedor(Proveedor);
                Proveedor.listaSucursal = ProveedorDatos.obtenerListaSucursales(Proveedor);
                Proveedor.listaTipoProveedor = ProveedorDatos.obtenerListaTipoProveedor(Proveedor);
                Proveedor.ListaGeneroCMB = ProveedorDatos.ObteneComboCatGenero(Proveedor);
                return View(Proveedor);
            }
            catch (Exception ex)
            {
                CatProveedorModels Proveedor = new CatProveedorModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("Index");
            }
        }

        // POST: Admin/CatProveedor/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, CatProveedorModels Proveedor)
        {
            _CatProveedor_Datos ProveedorDatos = new _CatProveedor_Datos();
            try
            {
                ModelState.Remove("ImgINEE");
                ModelState.Remove("ImgManifestacionFierros");
                if (ModelState.IsValid)
                {
                    HttpPostedFileBase bannerImage = Request.Files[0] as HttpPostedFileBase;
                    if (!string.IsNullOrEmpty(bannerImage.FileName))
                    {
                        if (bannerImage != null && bannerImage.ContentLength > 0)
                        {
                            Stream s = bannerImage.InputStream;
                            Bitmap img = new Bitmap(s);
                            Proveedor.ImgINE = img.ToBase64String(ImageFormat.Png);
                        }
                    }
                    else
                    {
                        Proveedor.BandINE = true;
                    }
                    HttpPostedFileBase bannerImage2 = Request.Files[1] as HttpPostedFileBase;
                    if (!string.IsNullOrEmpty(bannerImage2.FileName))
                    {
                        if (bannerImage2 != null && bannerImage2.ContentLength > 0)
                        {
                            Stream s = bannerImage2.InputStream;
                            Bitmap img = new Bitmap(s);
                            Proveedor.ImgManifestacionFierro = img.ToBase64String(ImageFormat.Png);
                        }
                    }
                    else
                    {
                        Proveedor.BandMF = true;
                    }
                    Proveedor.Conexion = Conexion;
                    Proveedor.Usuario = User.Identity.Name;
                    Proveedor.Opcion = 2;
                    Proveedor = ProveedorDatos.AcCatProveedor(Proveedor);
                    if (Proveedor.Completado)
                    {
                        TempData["typemessage"] = "1";
                        TempData["message"] = "Los datos se guardaron correctamente.";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        Proveedor.listaSucursal = ProveedorDatos.obtenerListaSucursales(Proveedor);
                        Proveedor.listaTipoProveedor = ProveedorDatos.obtenerListaTipoProveedor(Proveedor);
                        Proveedor.ListaGeneroCMB = ProveedorDatos.ObteneComboCatGenero(Proveedor);
                        TempData["typemessage"] = "2";
                        TempData["message"] = "Ocurrio un error al intentar guardar los datos. Intente más tarde.";
                        return View(Proveedor);
                    }
                }
                else
                {
                    Proveedor.Conexion = Conexion;
                    Proveedor.listaSucursal = ProveedorDatos.obtenerListaSucursales(Proveedor);
                    Proveedor.listaTipoProveedor = ProveedorDatos.obtenerListaTipoProveedor(Proveedor);
                    Proveedor.ListaGeneroCMB = ProveedorDatos.ObteneComboCatGenero(Proveedor);
                    return View(Proveedor);
                }
            }
            catch (Exception ex)
            {
                Proveedor.Conexion = Conexion;
                Proveedor.listaSucursal = ProveedorDatos.obtenerListaSucursales(Proveedor);
                Proveedor.listaTipoProveedor = ProveedorDatos.obtenerListaTipoProveedor(Proveedor);
                Proveedor.ListaGeneroCMB = ProveedorDatos.ObteneComboCatGenero(Proveedor);
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error al intentar guardar los datos. Contacte a soporte técnico.";
                return View(Proveedor);
            }
        }

        // GET: Admin/CatProveedor/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/CatProveedor/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                CatProveedorModels Proveedor = new CatProveedorModels();
                _CatProveedor_Datos ProveedorDatos = new _CatProveedor_Datos();
                Proveedor.Conexion = Conexion;
                Proveedor.IDProveedor = id;
                Proveedor.Usuario = User.Identity.Name;
                Proveedor = ProveedorDatos.EliminarProveedor(Proveedor);

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

        // GET: Admin/CatProveedor/Cuentas/5
        [HttpGet]
        public ActionResult Cuentas(string id)
        {
            try
            {
                CuentaBancariaProveedorModels Cuenta = new CuentaBancariaProveedorModels();
                _CatProveedor_Datos ProveedorDatos = new _CatProveedor_Datos();
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
                CuentaBancariaProveedorModels Cuenta = new CuentaBancariaProveedorModels();
                _CatProveedor_Datos ProveedorDatos = new _CatProveedor_Datos();
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
            _CatProveedor_Datos ProveedorDatos = new _CatProveedor_Datos();
            try
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
                CuentaBancariaProveedorModels Cuenta = new CuentaBancariaProveedorModels();
                _CatProveedor_Datos ProveedorDatos = new _CatProveedor_Datos();
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
            _CatProveedor_Datos ProveedorDatos = new _CatProveedor_Datos();
            try
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
                _CatProveedor_Datos ProveedorDatos = new _CatProveedor_Datos();
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

        //GET: Admin/CatProveedor/LugarProveedor/4
        [HttpGet]
        public ActionResult LugarProveedor(string id, string id2)
        {
            try
            {
                ProveedorLugarModels ProveedorL = new ProveedorLugarModels();
                _CatProveedor_Datos ProveedorDatos = new _CatProveedor_Datos();
                ProveedorL.Conexion = Conexion;
                ProveedorL.IDProveedor = id;
                ProveedorL.IDSucursal = id2;
                ProveedorL.ListaProveedorLugar = ProveedorDatos.ObtenerLugaresProveedor(ProveedorL);
                return View(ProveedorL);
            }
            catch (Exception)
            {
                ProveedorLugarModels Proveedor = new ProveedorLugarModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Proveedor);
            }
        }

        //GET:Admin/CatProveedor/CreateLugar/3
        [HttpGet]
        public ActionResult CreateLugar(string id, string id2)
        {
            try
            {
                ProveedorLugarModels ProveedorLugar = new ProveedorLugarModels();
                _CatProveedor_Datos ProveedorDatos = new _CatProveedor_Datos();
                ProveedorLugar.Conexion = Conexion;
                ProveedorLugar.IDProveedor = id;
                ProveedorLugar.IDSucursal = id2;
                ProveedorLugar.ListaLugares = ProveedorDatos.obtenerComboLugares(ProveedorLugar);
                return View(ProveedorLugar);
            }
            catch (Exception)
            {
                ProveedorLugarModels Proveedor = new ProveedorLugarModels();
                Proveedor.IDProveedor = id;
                Proveedor.IDSucursal = id2;
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("LugarProveedor", "CatProveedor", new { id = Proveedor.IDProveedor, id2 = Proveedor.IDSucursal });
            }
        }

        //POST: Admin/CatProvedor/CreateLugar/3
        [HttpPost]
        public ActionResult CreateLugar(ProveedorLugarModels Proveedor)
        {
            _CatProveedor_Datos ProveedorDatos = new _CatProveedor_Datos();
            try
            {
                if (ModelState.IsValid)
                {
                    Proveedor.Conexion = Conexion;
                    Proveedor.Usuario = User.Identity.Name;
                    ProveedorDatos.ACProveedorLugares(Proveedor);
                    if (Proveedor.Completado)
                    {
                        TempData["typemessage"] = "1";
                        TempData["message"] = "Los datos se guardaron correctamente.";
                        return RedirectToAction("LugarProveedor", "CatProveedor", new { id = Proveedor.IDProveedor, id2 = Proveedor.IDSucursal });
                    }
                    else
                    {
                        Proveedor.ListaLugares = ProveedorDatos.obtenerComboLugares(Proveedor);
                        TempData["typemessage"] = "2";
                        TempData["message"] = "Ocurrió un error al intentar guardar los datos. Intente más tarde.";
                        return RedirectToAction("CreateLugar", "CatProveedor", new { id = Proveedor.IDProveedor, id2 = Proveedor.IDSucursal });
                    }
                }
                else
                {
                    Proveedor.Conexion = Conexion;
                    Proveedor.ListaLugares = ProveedorDatos.obtenerComboLugares(Proveedor);
                    return RedirectToAction("CreateLugar", "CatProveedor", new { id = Proveedor.IDProveedor, id2 = Proveedor.IDSucursal });
                }
            }
            catch (Exception)
            {
                Proveedor.Conexion = Conexion;
                Proveedor.ListaLugares = ProveedorDatos.obtenerComboLugares(Proveedor);
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error al intentar guardar los datos. Contacte a soporte técnico.";
                return RedirectToAction("CreateLugar", "CatProveedor", new { id = Proveedor.IDProveedor, id2 = Proveedor.IDSucursal });
            }
        }

        // POST: Admin/CatProvedor/Delete/5
        [HttpPost]
        public ActionResult DeleteLugar(string id)
        {
            try
            {
                ProveedorLugarModels Datos = new ProveedorLugarModels
                {
                    IDProveedorLugar = id,
                    Conexion = Conexion,
                    Usuario = User.Identity.Name
                };
                _CatProveedor_Datos ProveedorDatos = new _CatProveedor_Datos();
                ProveedorDatos.EliminarProveedorLugar(Datos);
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

        //GET: Admin/CatProveedor/PrecioProveedor/3
        [HttpGet]
        public ActionResult PrecioProveedor(string id)
        {
            try
            {
                RangoPrecioProveedorModels RangoProveedor = new RangoPrecioProveedorModels();
                _CatProveedor_Datos ProveedorDato = new _CatProveedor_Datos();
                RangoProveedor.Conexion = Conexion;
                RangoProveedor.IDProveedor = id;
                RangoProveedor.ListaRangoPrecioProveedor = ProveedorDato.ObtenerPrecioXRangoPesoProveedor(RangoProveedor);
                return View(RangoProveedor);
            }
            catch (Exception)
            {
                RangoPrecioProveedorModels Proveedor = new RangoPrecioProveedorModels();
                TempData["typrmessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Proveedor);
            }
        }

        //GET: Admin/CatProveedor/EditPrecio/3
        [HttpGet]
        public ActionResult EditPrecio(int id, string id2)
        {
            try
            {
                RangoPrecioProveedorModels RangoProveedor = new RangoPrecioProveedorModels();
                _CatProveedor_Datos ProvedorDatos = new _CatProveedor_Datos();
                RangoProveedor.Conexion = Conexion;
                RangoProveedor.IDProveedor = id2;
                RangoProveedor.IDRango = id;
                RangoProveedor = ProvedorDatos.ObtenerDetallePrecioXProveedor(RangoProveedor);
                return View(RangoProveedor);
            }
            catch (Exception)
            {
                RangoPrecioProveedorModels Proveedor = new RangoPrecioProveedorModels();
                Proveedor.IDProveedor = id2;
                TempData["typrmessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("PrecioProveedor", "CatProveedor", new { id = Proveedor.IDProveedor});
            }
        }

        //POST: Admin/CatProveedor/EditPrecio/5
        [HttpPost]
        public ActionResult EditPrecio(RangoPrecioProveedorModels RangoPrecio)
        {
            _CatProveedor_Datos ProveedorDatos = new _CatProveedor_Datos();
            try
            {
                if (ModelState.IsValid)
                {
                    RangoPrecio.Conexion = Conexion;
                    RangoPrecio.Usuario = User.Identity.Name;
                    ProveedorDatos.ACPrecioPoRangoPesoProveedor(RangoPrecio);
                    if (RangoPrecio.Completado)
                    {
                        if (RangoPrecio.Resultado == 1)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "Los datos se guardaron correctamente.";
                            return RedirectToAction("PrecioProveedor", "CatProveedor", new { id = RangoPrecio.IDProveedor });
                        }
                        else if (RangoPrecio.Resultado == 2)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "Los datos actualizado correctamente.";
                            return RedirectToAction("PrecioProveedor", "CatProveedor", new { id = RangoPrecio.IDProveedor });
                        }
                        else
                        {
                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrió un error al intentar guardar los datos. Intente más tarde.";
                            return View(RangoPrecio);
                        }
                    }
                    else
                    {
                        TempData["typemessage"] = "2";
                        TempData["message"] = "Ocurrió un error al intentar guardar los datos. Intente más tarde.";
                        return View(RangoPrecio);
                    }
                }
                else
                {
                    return View(RangoPrecio);//return RedirectToAction("EditPrecio", "CatProveedor", new { id = RangoPrecio.IDRango, id2 = RangoPrecio.IDProveedor });
                }
            }
            catch (Exception)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error al intentar guardar los datos. Contacte a soporte técnico.";
                return View(RangoPrecio);
            }
        }
        [HttpGet]
        public ActionResult UPPProveedor(string id)
        {
            try
            {
                UPPProvedorModels uPPProvedor = new UPPProvedorModels();
                _CatProveedor_Datos ProveedorDatos = new _CatProveedor_Datos();
                uPPProvedor.Conexion = Conexion;
                uPPProvedor.id_proveedor = id;
                uPPProvedor = ProveedorDatos.ObtenerUPPProveedor(uPPProvedor);
                uPPProvedor.listaPaises = ProveedorDatos.obtenerListaPaises(uPPProvedor);

                var List = new SelectList(uPPProvedor.listaPaises, "id_pais", "descripcion");
                ViewData["cmbPaises"] = List;

                uPPProvedor.listaEstado = ProveedorDatos.obtenerListaEstados(uPPProvedor);
                var Lista = new SelectList(uPPProvedor.listaEstado, "codigoEstado", "descripcion");
                ViewData["cmbEstados"] = Lista;

                uPPProvedor.listaMunicipio = ProveedorDatos.obtenerListaMunicipios(uPPProvedor);
                var Listam = new SelectList(uPPProvedor.listaMunicipio, "id_municipio", "descripcion");
                ViewData["cmbMunicipios"] = Listam;

                return View(uPPProvedor);
            }
            catch (Exception)
            {
                UPPProvedorModels uPPProvedor = new UPPProvedorModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("Index");
            }
        }
        public ActionResult UPPProveedor(string id, UPPProvedorModels uPPProvedor)
        {
            _CatProveedor_Datos ProveedorDatos = new _CatProveedor_Datos();
            try
            {
                HttpPostedFileBase bannerImage = Request.Files[0] as HttpPostedFileBase;
                if (!string.IsNullOrEmpty(bannerImage.FileName))
                {
                    if (bannerImage != null && bannerImage.ContentLength > 0)
                    {
                        Stream s = bannerImage.InputStream;
                        Bitmap img = new Bitmap(s);
                        uPPProvedor.Imagen = img.ToBase64String(ImageFormat.Png);
                    }
                }
                else
                {
                    uPPProvedor.BandImg = true;
                    ModelState.AddModelError(string.Empty, "Cargar imagen UPPProveedor");
                }
                if (ModelState.IsValid)
                {
                    uPPProvedor.Conexion = Conexion;
                    uPPProvedor.id_proveedor = id;
                    uPPProvedor.Usuario = User.Identity.Name;
                    uPPProvedor = ProveedorDatos.CUPPProveedor(uPPProvedor);
                    if (uPPProvedor.Completado)
                    {
                        TempData["typemessage"] = "1";
                        TempData["message"] = "Los datos se guardaron correctamente.";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        uPPProvedor.Conexion = Conexion;
                        uPPProvedor.id_proveedor = id;
                        uPPProvedor.listaPaises = ProveedorDatos.obtenerListaPaises(uPPProvedor);
                        var List = new SelectList(uPPProvedor.listaPaises, "id_pais", "descripcion");
                        ViewData["cmbPaises"] = List;

                        uPPProvedor.listaEstado = ProveedorDatos.obtenerListaEstados(uPPProvedor);
                        var Lista = new SelectList(uPPProvedor.listaEstado, "codigoEstado", "descripcion");
                        ViewData["cmbEstados"] = Lista;

                        uPPProvedor.listaMunicipio = ProveedorDatos.obtenerListaMunicipios(uPPProvedor);
                        var Listam = new SelectList(uPPProvedor.listaMunicipio, "id_municipio", "descripcion");
                        ViewData["cmbMunicipios"] = Listam;

                        TempData["typemessage"] = "2";
                        TempData["message"] = "Ocurrio un error al intentar guardar los datos. Intente más tarde.";
                        return View(uPPProvedor);
                    }
                }
                else
                {
                    uPPProvedor.Conexion = Conexion;
                    uPPProvedor.id_proveedor = id;
                    uPPProvedor.listaPaises = ProveedorDatos.obtenerListaPaises(uPPProvedor);
                    var List = new SelectList(uPPProvedor.listaPaises, "id_pais", "descripcion");
                    ViewData["cmbPaises"] = List;

                    uPPProvedor.listaEstado = ProveedorDatos.obtenerListaEstados(uPPProvedor);
                    var Lista = new SelectList(uPPProvedor.listaEstado, "codigoEstado", "descripcion");
                    ViewData["cmbEstados"] = Lista;

                    uPPProvedor.listaMunicipio = ProveedorDatos.obtenerListaMunicipios(uPPProvedor);
                    var Listam = new SelectList(uPPProvedor.listaMunicipio, "id_municipio", "descripcion");
                    ViewData["cmbMunicipios"] = Listam;

                    TempData["typemessage"] = "2";
                    TempData["message"] = "Ocurrio un error al intentar guardar los datos. Intente más tarde.";
                    return View(uPPProvedor);
                }
            }
            catch (Exception ex)
            {
                // UPPProvedorModels uPPProvedor = new UPPProvedorModels();
                uPPProvedor.Conexion = Conexion;
                uPPProvedor.id_proveedor = id;
                uPPProvedor.listaPaises = ProveedorDatos.obtenerListaPaises(uPPProvedor);
                var List = new SelectList(uPPProvedor.listaPaises, "id_pais", "descripcion");
                ViewData["cmbPaises"] = List;

                uPPProvedor.listaEstado = ProveedorDatos.obtenerListaEstados(uPPProvedor);
                var Lista = new SelectList(uPPProvedor.listaEstado, "codigoEstado", "descripcion");
                ViewData["cmbEstados"] = Lista;

                uPPProvedor.listaMunicipio = ProveedorDatos.obtenerListaMunicipios(uPPProvedor);
                var Listam = new SelectList(uPPProvedor.listaMunicipio, "id_municipio", "descripcion");
                ViewData["cmbMunicipios"] = Listam;

                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error al intentar guardar los datos. Intente más tarde.";
                return View(uPPProvedor);
            }
        }
        //AJAX OBTIENE TODOS LOS COMBOS MEDIANTE JAVASCRIPT
        [HttpPost]
        public ActionResult Estado(string id)
        {
            try
            {
                UPPProvedorModels uPPProvedor = new UPPProvedorModels();
                _CatProveedor_Datos LugarDatos = new _CatProveedor_Datos();
                uPPProvedor.Conexion = Conexion;
                uPPProvedor.id_pais = id;
                uPPProvedor.listaEstado = LugarDatos.obtenerListaEstados(uPPProvedor);

                return Json(uPPProvedor.listaEstado, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]

        public ActionResult Municipio(string idPais, string id)
        {
            try
            {
                UPPProvedorModels uPPProvedor = new UPPProvedorModels();
                _CatProveedor_Datos LugarDatos = new _CatProveedor_Datos();


                uPPProvedor.id_estadoCodigo = id;
                uPPProvedor.id_pais = idPais;
                uPPProvedor.Conexion = Conexion;
                uPPProvedor.listaMunicipio = LugarDatos.obtenerListaMunicipios(uPPProvedor);
                return Json(uPPProvedor.listaMunicipio, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }
    }
}
