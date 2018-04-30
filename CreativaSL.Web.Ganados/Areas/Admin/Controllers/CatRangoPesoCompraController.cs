using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using CreativaSL.Web.Ganados.Models;
using CreativaSL.Web.Ganados.Filters;
using CreativaSL.Web.Ganados.App_Start;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    [Autorizado]
    public class CatRangoPesoCompraController : Controller
    {
        private TokenProcessor Token = TokenProcessor.GetInstance();
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
        // GET: Admin/CatRangoPesoCompra
        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                CatRangoPesoCompraModels RangoPeso = new CatRangoPesoCompraModels();
                CatRangoPesoCompra_Datos RangoPesoDatos = new CatRangoPesoCompra_Datos();
                RangoPeso.Conexion = Conexion;
                RangoPeso = RangoPesoDatos.ObtenerListaRangoPeso(RangoPeso);
                return View(RangoPeso);
            }
            catch (Exception)
            {
                CatRangoPesoCompraModels RangoPeso = new CatRangoPesoCompraModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(RangoPeso);
            }
        }

        // GET: Admin/CatRangoPesoCompra/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/CatRangoPesoCompra/Create
        [HttpGet]
        public ActionResult Create()
        {
            try
            {
                Token.SaveToken();
                CatRangoPesoCompraModels Rango = new CatRangoPesoCompraModels();
                Rango.EsMacho = true;
                return View(Rango);
            }
            catch (Exception)
            {
                CatRangoPesoCompraModels RangoPeso = new CatRangoPesoCompraModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(RangoPeso);
            }
        }

        // POST: Admin/CatRangoPesoCompra/Create
        [HttpPost]
        public ActionResult Create(CatRangoPesoCompraModels Rango)
        {
           // CatRangoPesoCompraModels Rango = new CatRangoPesoCompraModels();
            CatRangoPesoCompra_Datos RangoDatos = new CatRangoPesoCompra_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        Rango.Conexion = Conexion;
                        Rango.Opcion = 1;
                        Rango.Usuario = User.Identity.Name;
                        Rango = RangoDatos.AbcCatRangoPesoCompra(Rango);
                        if (Rango.Completado == true)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "Los datos se guardarón correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrio un error al intentar guardar los datos. Intente más tarde.";
                            return View(Rango);
                        }
                    }
                    else
                    {
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

        // GET: Admin/CatRangoPesoCompra/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            try
            {
                Token.SaveToken();
                CatRangoPesoCompraModels Rango = new CatRangoPesoCompraModels();
                CatRangoPesoCompra_Datos RangoDatos = new CatRangoPesoCompra_Datos();
                Rango.Conexion = Conexion;
                Rango.IDRango = id;
                Rango = RangoDatos.ObtenerDetalleCatRangoPesoCompra(Rango);
                return View(Rango);
            }
            catch (Exception)
            {
                CatRangoPesoCompraModels RangoPeso = new CatRangoPesoCompraModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(RangoPeso);
            }
        }

        // POST: Admin/CatRangoPesoCompra/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, CatRangoPesoCompraModels Rango)
        {
           // CatRangoPesoCompraModels Rango = new CatRangoPesoCompraModels();
            CatRangoPesoCompra_Datos RangoDatos = new CatRangoPesoCompra_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        Rango.Conexion = Conexion;
                        Rango.Opcion = 2;
                        Rango.Usuario = User.Identity.Name;
                        Rango = RangoDatos.AbcCatRangoPesoCompra(Rango);
                        if (Rango.Completado == true)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "Los datos se guardarón correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrio un error al intentar guardar los datos. Intente más tarde.";
                            return View(Rango);
                        }
                    }
                    else
                    {
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

        // GET: Admin/CatRangoPesoCompra/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/CatRangoPesoCompra/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                CatRangoPesoCompraModels Rango = new CatRangoPesoCompraModels();
                CatRangoPesoCompra_Datos RangoDatos = new CatRangoPesoCompra_Datos();
                Rango.Conexion = Conexion;
                Rango.IDRango = id;
                Rango.Usuario = User.Identity.Name;
                Rango = RangoDatos.EliminarRangoPesoCompra(Rango);
                if (Rango.Completado == true)
                {
                    TempData["typemessage"] = "1";
                    TempData["message"] = "El registro se ha eliminado correctamente";
                }
                return Json("");
            }
            catch
            {
                return View();
            }
        }
    }
}
