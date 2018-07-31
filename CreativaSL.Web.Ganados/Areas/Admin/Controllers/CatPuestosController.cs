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
    public class CatPuestosController : Controller
    {
        private TokenProcessor Token = TokenProcessor.GetInstance();
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
        // GET: Admin/CatPuestos
        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                CatPuestoModels Puesto = new CatPuestoModels();
                CatPuestos_Datos PuestoD = new CatPuestos_Datos();
                Puesto.Conexion = Conexion;
                Puesto = PuestoD.ObtenerListaPuesto(Puesto);
                return View(Puesto);
            }
            catch (Exception)
            {
                CatPuestoModels Puesto = new CatPuestoModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Puesto);
            }
        }

        // GET: Admin/CatPuestos/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/CatPuestos/Create
        [HttpGet]
        public ActionResult Create()
        {
            try
            {
                Token.SaveToken();
                CatPuestoModels Puesto = new CatPuestoModels();
                Puesto.EsGerente = true;
                return View(Puesto);
            }
            catch (Exception)
            {
                CatPuestoModels Puesto = new CatPuestoModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Puesto);
            }
        }

        // POST: Admin/CatPuestos/Create
        [HttpPost]
        public ActionResult Create(CatPuestoModels Puesto)
        {
            //CatPuestoModels Puesto = new CatPuestoModels();
            CatPuestos_Datos PuestoDatos = new CatPuestos_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        Puesto.Conexion = Conexion;
                        Puesto.Usuario = User.Identity.Name;
                        Puesto.Opcion = 1;
                        //Puesto.Descripcion = collection["Descripcion"];
                        //Puesto.EsGerente = collection["EsGerente"].StartsWith("true");
                        Puesto = PuestoDatos.AbcCatPuesto(Puesto);
                        if (Puesto.Completado == true)
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
                            return View(Puesto);
                        }
                    }
                    else
                    {
                        return View(Puesto);
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

        // GET: Admin/CatPuestos/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            try
            {
                Token.SaveToken();
                CatPuestoModels Puesto = new CatPuestoModels();
                CatPuestos_Datos PuestoDatos = new CatPuestos_Datos();
                Puesto.IDPuesto = id;
                Puesto.Conexion = Conexion;
                Puesto = PuestoDatos.ObtenerDetalleCatPuesto(Puesto);
                return View(Puesto);
            }
            catch (Exception)
            {
                CatPuestoModels Puesto = new CatPuestoModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Puesto);
            }
        }

        // POST: Admin/CatPuestos/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, CatPuestoModels Puesto)
        {
            //CatPuestoModels Puesto = new CatPuestoModels();
            CatPuestos_Datos PuestoDatos = new CatPuestos_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        Puesto.Conexion = Conexion;
                        Puesto.Usuario = User.Identity.Name;
                        Puesto.Opcion = 2;
                        //int ID = 0;
                        //int.TryParse(collection["IDPuesto"], out ID);
                        //Puesto.IDPuesto = ID;
                        //Puesto.Descripcion = collection["Descripcion"];
                        //Puesto.EsGerente = collection["EsGerente"].StartsWith("true");
                        Puesto = PuestoDatos.AbcCatPuesto(Puesto);
                        if (Puesto.Completado == true)
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
                            return View(Puesto);
                        }
                    }
                    else
                    {
                        return View(Puesto);
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

        // GET: Admin/CatPuestos/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/CatPuestos/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                CatPuestoModels Puesto = new CatPuestoModels();
                CatPuestos_Datos PuestoDatos = new CatPuestos_Datos();
                Puesto.Conexion = Conexion;
                Puesto.IDPuesto = id;
                Puesto.Usuario = User.Identity.Name;
                Puesto = PuestoDatos.EliminarPuesto(Puesto);
                if (Puesto.Completado == true)
                {
                    //TempData["typemessage"] = "1";
                    //TempData["message"] = "El registro se ha eliminado correctamente";
                }
                return Json("");
            }
            catch
            {
                CatPuestoModels Puesto = new CatPuestoModels();
                return View();
            }
        }
    }
}
