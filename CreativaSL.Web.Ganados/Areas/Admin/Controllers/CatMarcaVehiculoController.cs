using CreativaSL.Web.Ganados.Filters;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CreativaSL.Web.Ganados.Models;
using CreativaSL.Web.Ganados.App_Start;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    [Autorizado]
    public class CatMarcaVehiculoController : Controller
    {
        private TokenProcessor Token = TokenProcessor.GetInstance();
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
        // GET: Admin/CatMarcaVehiculo
        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                CatMarcaVehiculoModels Marca = new CatMarcaVehiculoModels();
                CatMarcaVehiculo_Datos MarcaDatos = new CatMarcaVehiculo_Datos();
                Marca.Conexion = Conexion;
                Marca = MarcaDatos.ObtenerListaMarcas(Marca);
                return View(Marca);
            }
            catch (Exception)
            {
                CatMarcaVehiculoModels Marca = new CatMarcaVehiculoModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Marca);
            }
        }

        // GET: Admin/CatMarcaVehiculo/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/CatMarcaVehiculo/Create
        [HttpGet]
        public ActionResult Create()
        {
            try
            {
                Token.SaveToken();
                CatMarcaVehiculoModels Marca = new CatMarcaVehiculoModels();
                return View(Marca);
            }
            catch (Exception)
            {
                CatMarcaVehiculoModels Marca = new CatMarcaVehiculoModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Marca);
            }
        }

        // POST: Admin/CatMarcaVehiculo/Create
        [HttpPost]
        public ActionResult Create(CatMarcaVehiculoModels Marca)
        {
            //CatMarcaVehiculoModels Marca = new CatMarcaVehiculoModels();
            CatMarcaVehiculo_Datos MarcaDatos = new CatMarcaVehiculo_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        Marca.Conexion = Conexion;
                        Marca.Usuario = User.Identity.Name;
                        Marca.Opcion = 1;
                        //Marca.Descripcion = collection["Descripcion"];
                        Marca = MarcaDatos.AbcCatMarcaVehiculo(Marca);
                        if (Marca.Completado == true)
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
                            return View(Marca);
                        }
                    }
                    else
                    {
                        return View(Marca);
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

        // GET: Admin/CatMarcaVehiculo/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            try
            {
                Token.SaveToken();
                CatMarcaVehiculoModels Marca = new CatMarcaVehiculoModels();
                CatMarcaVehiculo_Datos MarcaDatos = new CatMarcaVehiculo_Datos();
                Marca.Conexion = Conexion;
                Marca.IDMarca = id;
                Marca = MarcaDatos.ObtenerDetalleCatMarcaVehiculo(Marca);
                return View(Marca);
            }
            catch (Exception)
            {
                CatMarcaVehiculoModels Marca = new CatMarcaVehiculoModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Marca);
            }
        }

        // POST: Admin/CatMarcaVehiculo/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, CatMarcaVehiculoModels Marca)
        {
           // CatMarcaVehiculoModels Marca = new CatMarcaVehiculoModels();
            CatMarcaVehiculo_Datos MarcaDatos = new CatMarcaVehiculo_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        Marca.Conexion = Conexion;
                        Marca.Usuario = User.Identity.Name;
                        Marca.Opcion = 2;
                        //int ID = 0;
                        //int.TryParse(collection["IDMarca"], out ID);
                        //Marca.IDMarca = ID;
                        //Marca.Descripcion = collection["Descripcion"];
                        Marca = MarcaDatos.AbcCatMarcaVehiculo(Marca);
                        if (Marca.Completado == true)
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
                            return View(Marca);
                        }
                    }
                    else
                    {
                        return View(Marca);
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

        // GET: Admin/CatMarcaVehiculo/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                return View();
            }
            catch (Exception)
            {

                throw;
            }
        }

        // POST: Admin/CatMarcaVehiculo/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                CatMarcaVehiculoModels Marca = new CatMarcaVehiculoModels();
                CatMarcaVehiculo_Datos MarcaDatos = new CatMarcaVehiculo_Datos();
                Marca.Conexion = Conexion;
                Marca.Usuario = User.Identity.Name;
                Marca.IDMarca = id;
                Marca = MarcaDatos.EliminarMarca(Marca);
                if (Marca.Completado == true)
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
