using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CreativaSL.Web.Ganados.App_Start;
using CreativaSL.Web.Ganados.Filters;
using CreativaSL.Web.Ganados.Models;
using System.Configuration;
using System.Web.Mvc;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    [Autorizado]
    public class CatTipoClienteController : Controller
    {
        private TokenProcessor Token = TokenProcessor.GetInstance();
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
        // GET: Admin/CatTipoCliente
        public ActionResult Index()
        {
            try
            {
                CatTipoClienteModels TipoCliente = new CatTipoClienteModels();
                _CatTipoCliente_Datos TipoCliente_Datos = new _CatTipoCliente_Datos();
                TipoCliente.Conexion = Conexion;
                TipoCliente.listaTipoCliente = TipoCliente_Datos.ObtenerCatTipoCliente(TipoCliente);
                return View(TipoCliente);
            }
            catch (Exception ex)
            {
                CatClienteModels Cliente = new CatClienteModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Cliente);
            }
            

        }

        // GET: Admin/CatTipoCliente/Create
        public ActionResult Create()
        {
            try
            {
                Token.SaveToken();
                CatTipoClienteModels catTipoCliente = new CatTipoClienteModels();
                _CatTipoCliente_Datos tipoCliente_Datos = new _CatTipoCliente_Datos();
                catTipoCliente.Conexion = Conexion;
                return View(catTipoCliente);
            }
            catch (Exception ex)
            {
                CatTipoClienteModels catTipoCliente = new CatTipoClienteModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(catTipoCliente);
            }
        }

        // POST: Admin/CatTipoCliente/Create
        [HttpPost]
        public ActionResult Create(CatTipoClienteModels catTipoCliente)
        {
            _CatTipoCliente_Datos tipoCliente_Datos = new _CatTipoCliente_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        catTipoCliente.Conexion = Conexion;
                        catTipoCliente.id_tipoCliente = 1;
                        catTipoCliente.Opcion = 1;
                        catTipoCliente.Usuario = User.Identity.Name;
                        catTipoCliente = tipoCliente_Datos.AcCatTipoCliente(catTipoCliente);
                        if (catTipoCliente.Completado == true)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "El registro se guardo correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrió un error al guardar el registro.";
                            return View(catTipoCliente);
                        }
                    }
                    else
                    {
                        return View(catTipoCliente);
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/CatTipoCliente/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                Token.SaveToken();
                CatTipoClienteModels catTipoCliente = new CatTipoClienteModels();
                _CatTipoCliente_Datos tipoCliente_Datos = new _CatTipoCliente_Datos();
                catTipoCliente.Conexion = Conexion;
                catTipoCliente.id_tipoCliente = id;
                catTipoCliente = tipoCliente_Datos.ObtenerDetalleCatTipoCliente(catTipoCliente);
                return View(catTipoCliente);
            }
            catch (Exception)
            {
                CatTipoProveedorModels TipoProveedor = new CatTipoProveedorModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(TipoProveedor);
            }
        }

        // POST: Admin/CatTipoCliente/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, CatTipoClienteModels catTipoCliente)
        {
            try
            {
                // TODO: Add update logic here

                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        // TODO: Add insert logic here
                        _CatTipoCliente_Datos tipoCliente_Datos = new _CatTipoCliente_Datos();
                        catTipoCliente.Conexion = Conexion;

                        catTipoCliente.id_tipoCliente = id;
                        catTipoCliente.Opcion = 2;
                        catTipoCliente.Usuario = User.Identity.Name;
                        catTipoCliente = tipoCliente_Datos.AcCatTipoCliente(catTipoCliente);
                        if (catTipoCliente.Completado == true)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "El registro se guardo correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("Index");
                        }
                        else
                        {

                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrió un error al guardar el registro.";
                            return View(catTipoCliente);
                        }


                    }
                    else
                    {
                        return View(catTipoCliente);
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/CatTipoCliente/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/CatTipoCliente/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                CatTipoClienteModels catTipoCliente = new CatTipoClienteModels();
                _CatTipoCliente_Datos tipoCliente_Datos = new _CatTipoCliente_Datos();
                catTipoCliente.Conexion = Conexion;
                catTipoCliente.id_tipoCliente = id;
                catTipoCliente.Usuario = User.Identity.Name;
                catTipoCliente = tipoCliente_Datos.EliminarTipoCliente(catTipoCliente);

                if (catTipoCliente.Completado == true)
                {
                    TempData["typemessage"] = "1";
                    TempData["message"] = "El registro se elimino correctamente.";
                    Token.ResetToken();
                    return Json("1");
                }
                else
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "No se pudo eliminar el registro";
                    return Json("2");
                }
            }
            catch
            {
                CatClienteModels Producto = new CatClienteModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se pudo borrar los datos. Por favor contacte a soporte técnico";
                return Json("");
            }
        }
    }
}
