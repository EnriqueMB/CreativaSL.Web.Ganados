using CreativaSL.Web.Ganados.App_Start;
using CreativaSL.Web.Ganados.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    public class CatRangoPesoVentaController : Controller
    {
        private TokenProcessor Token = TokenProcessor.GetInstance();
        private string conexion = ConfigurationManager.AppSettings.Get("strConnection");

        // GET: Admin/CatRangoPrecioCliente
        public ActionResult Index()
        {
            return View();
        }

        // GET: Admin/CatRangoPrecioCliente/Create
        public ActionResult Create()
        {
            try
            {
                Token.SaveToken();
                CatRangoPesoVentaModels Rango = new CatRangoPesoVentaModels();
                _CatRangoPesoVenta_Datos RangoDatos = new _CatRangoPesoVenta_Datos();
                ViewBag.ListaTipoClientes = RangoDatos.ObtenerListaTipoCliente(Rango, conexion);
                return View(Rango);
            }
            catch (Exception)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("Index");
            }
        }

        // POST: Admin/CatRangoPrecioCliente/Create
        [HttpPost]
        public ActionResult Create(CatRangoPesoVentaModels Rango)
        {
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        _CatRangoPesoVenta_Datos RangoDatos = new _CatRangoPesoVenta_Datos();
                        string usuario = User.Identity.Name;
                        RespuestaAjax respuesta  = RangoDatos.AbcCatRangoPesoVenta(Rango, 1, usuario, conexion);
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
                            ViewBag.ListaTipoClientes = RangoDatos.ObtenerListaTipoCliente(Rango, conexion);
                            return View(Rango);
                        }
                    }
                    else
                    {
                        _CatRangoPesoVenta_Datos RangoDatos = new _CatRangoPesoVenta_Datos();
                        ViewBag.ListaTipoClientes = RangoDatos.ObtenerListaTipoCliente(Rango, conexion);
                        return View(Rango);
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
                TempData["message"] = "Ocurrio un error al intentar guardar los datos. Contacte a soporte técnico.";
                return RedirectToAction("Index");
            }
        }

        // GET: Admin/CatRangoPrecioCliente/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                if(id==null || id == 0)
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Verifique sus datos.";
                    return RedirectToAction("Index");
                }

                Token.SaveToken();
                CatRangoPesoVentaModels Rango = new CatRangoPesoVentaModels();
                _CatRangoPesoVenta_Datos RangoDatos = new _CatRangoPesoVenta_Datos();
                Rango.Id_rango = id.Value;
                Rango = RangoDatos.ObtenerDetalleCatRangoPesoVenta(Rango, conexion);
                ViewBag.ListaTipoClientes = RangoDatos.ObtenerListaTipoCliente(Rango, conexion);
                return View(Rango);
            }
            catch (Exception)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista.";
                return RedirectToAction("Index");
            }
        }

        // POST: Admin/CatRangoPrecioCliente/Edit/5
        [HttpPost]
        public ActionResult Edit(CatRangoPesoVentaModels Rango)
        {
            // CatRangoPesoCompraModels Rango = new CatRangoPesoCompraModels();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        _CatRangoPesoVenta_Datos RangoDatos = new _CatRangoPesoVenta_Datos();
                        string usuario = User.Identity.Name;
                        RespuestaAjax respuesta = RangoDatos.AbcCatRangoPesoVenta(Rango, 2, usuario, conexion);
                        TempData["message"] = respuesta.Mensaje;

                        if (respuesta.Success)
                        {
                            TempData["typemessage"] = "1";
                            Token.ResetToken();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ViewBag.ListaTipoClientes = RangoDatos.ObtenerListaTipoCliente(Rango, conexion);
                            TempData["typemessage"] = "2";
                            return View(Rango);
                        }
                    }
                    else
                    {
                        _CatRangoPesoVenta_Datos RangoDatos = new _CatRangoPesoVenta_Datos();
                        ViewBag.ListaTipoClientes = RangoDatos.ObtenerListaTipoCliente(Rango, conexion);
                        return View(Rango);
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
                TempData["message"] = "Ocurrio un error al intentar guardar los datos. Contacte a soporte técnico.";
                return RedirectToAction("Index");
            }
        }

        // POST: Admin/CatRangoPrecioCliente/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id)
        {
            try
            {
                if(id == null || id == 0)
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Verifique sus datos.";
                    return RedirectToAction("Index");
                }
                _CatRangoPesoVenta_Datos RangoDatos = new _CatRangoPesoVenta_Datos();
                RespuestaAjax respuesta = new RespuestaAjax();
                string usuario = User.Identity.Name;
                respuesta = RangoDatos.EliminarRangoPesoCompra(id.Value, conexion, usuario);

                return Content(respuesta.ToJSON(), "application/json");
            }
            catch
            {
                return View();
            }
        }

        #region DataTable
        [HttpPost]
        public ActionResult LoadTblRangoPesoVenta()
        {
            try
            {
                _CatRangoPesoVenta_Datos Datos = new _CatRangoPesoVenta_Datos();
                string datatable = Datos.RangoPesoVenta_index_RangoPesoVenta(conexion);

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
