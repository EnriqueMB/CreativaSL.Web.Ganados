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
        string conexion = ConfigurationManager.AppSettings.Get("strConnection");
        // GET: Admin/CatTipoCliente
        public ActionResult Index()
        {
            try
            {
                return View();
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
                        string usuario = User.Identity.Name;
                        RespuestaAjax respuesta = new RespuestaAjax();
                        respuesta = tipoCliente_Datos.AcCatTipoCliente(catTipoCliente, conexion, usuario, 1);
                        TempData["message"] = respuesta.Mensaje;

                        if (respuesta.Success)
                        {
                            TempData["typemessage"] = "1";
                            Token.ResetToken();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            TempData["typemessage"] = "2";
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
                    TempData["typemessage"] = "2";
                    TempData["message"] = "No se puede cargar la vista";
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/CatTipoCliente/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                if(id == null || id == 0)
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "No se puede cargar la vista";
                    return RedirectToAction("Index");
                }
                Token.SaveToken();
                CatTipoClienteModels catTipoCliente = new CatTipoClienteModels();
                catTipoCliente.Id_tipoCliente = id.Value;
                _CatTipoCliente_Datos tipoCliente_Datos = new _CatTipoCliente_Datos();
                catTipoCliente = tipoCliente_Datos.ObtenerDetalleCatTipoCliente(catTipoCliente, conexion);
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
        public ActionResult Edit(CatTipoClienteModels catTipoCliente)
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

                        string usuario = User.Identity.Name;
                        RespuestaAjax respuesta = new RespuestaAjax();
                        respuesta = tipoCliente_Datos.AcCatTipoCliente(catTipoCliente, conexion, usuario, 2);

                        TempData["message"] = respuesta.Mensaje;

                        if (respuesta.Success)
                        {
                            TempData["typemessage"] = "1";
                            Token.ResetToken();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            TempData["typemessage"] = "2";
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
                    TempData["typemessage"] = "2";
                    TempData["message"] = "No se puede cargar la vista";
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View();
            }
        }

        // POST: Admin/CatTipoCliente/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                // TODO: Add delete logic here
                _CatTipoCliente_Datos tipoCliente_Datos = new _CatTipoCliente_Datos();
                string usuario = User.Identity.Name;
                RespuestaAjax respuesta = new RespuestaAjax();
                respuesta = tipoCliente_Datos.EliminarTipoCliente(id, usuario, conexion);

                return Content(respuesta.ToJSON(), "application/json");
            }
            catch
            {
                return View();
            }
        }

        #region DataTable
        [HttpPost]
        public ActionResult LoadTblTipoCliente()
        {
            try
            {
                _CatTipoCliente_Datos Datos = new _CatTipoCliente_Datos();
                string datatable = Datos.CatTipoCliente_index_CatTipoCliente(conexion);

                return Content(datatable, "application/json");
            }
            catch (Exception ex)
            {
                return Content("", "application/json");
            }
        }
        #endregion
    }
}
