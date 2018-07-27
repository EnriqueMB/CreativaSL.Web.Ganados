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
    public class CatTipoVehiculoController : Controller
    {
        private TokenProcessor Token = TokenProcessor.GetInstance();
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
        // GET: Admin/CatTipoVehiculo
        public ActionResult Index()
        {
            try
            {
                CatTipoVehiculoModels TipoVehiculo = new CatTipoVehiculoModels();
                _CatTipoVehiculos_Datos TipoVehiculoDatos = new _CatTipoVehiculos_Datos();
                TipoVehiculo.Conexion = Conexion;
                TipoVehiculo = TipoVehiculoDatos.ObtenerListaTipoVehiculos(TipoVehiculo);
                return View(TipoVehiculo);
            }
            catch (Exception)
            {
                CatTipoVehiculoModels TipoVehiculo = new CatTipoVehiculoModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(TipoVehiculo);
            }

        }

        // GET: Admin/CatTipoVehiculo/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/CatTipoVehiculo/Create
        public ActionResult Create()
        {
            try
            {
                Token.SaveToken();
                CatTipoVehiculoModels TipoVehiculo = new CatTipoVehiculoModels();
                return View(TipoVehiculo);
            }
            catch (Exception)
            {
                CatTipoVehiculoModels TipoVehiculo = new CatTipoVehiculoModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(TipoVehiculo);
            }
        }

        // POST: Admin/CatTipoVehiculo/Create
        [HttpPost]
        public ActionResult Create(CatTipoVehiculoModels TipoVehiculo)
        {
            _CatTipoVehiculos_Datos TipoVehiculoDatos = new _CatTipoVehiculos_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        TipoVehiculo.Conexion = Conexion;
                        TipoVehiculo.Usuario = User.Identity.Name;
                        TipoVehiculo.Opcion = 1;

                        TipoVehiculo = TipoVehiculoDatos.AcCatTipoVehiculo(TipoVehiculo);
                        if (TipoVehiculo.Completado == true)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "Los datos se guardarón correctamente.";
                            Token.SaveToken();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrio un error al intentar guardar los datos. Intente más tarde.";
                            return View(TipoVehiculo);
                        }
                    }
                    else
                    {
                        return View(TipoVehiculo);
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

        // GET: Admin/CatTipoVehiculo/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                Token.SaveToken();
                CatTipoVehiculoModels TipoVehiculo = new CatTipoVehiculoModels();
                _CatTipoVehiculos_Datos TipoVehiculoDatos = new _CatTipoVehiculos_Datos();
                TipoVehiculo.Conexion = Conexion;
                TipoVehiculo.IDTipoVehiculo = id;
                TipoVehiculo = TipoVehiculoDatos.ObtenerDetalleCatMarcaVehiculo(TipoVehiculo);
                return View(TipoVehiculo);
            }
            catch (Exception)
            {
                CatTipoVehiculoModels TipoVehiculo = new CatTipoVehiculoModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(TipoVehiculo);
            }
        }

        // POST: Admin/CatTipoVehiculo/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, CatTipoVehiculoModels TipoVehiculo)
        {
            //CatTipoVehiculoModels TipoVehiculo = new CatTipoVehiculoModels();
            _CatTipoVehiculos_Datos TipoVehiculoDatos = new _CatTipoVehiculos_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        TipoVehiculo.Conexion = Conexion;
                        TipoVehiculo.Usuario = User.Identity.Name;
                        TipoVehiculo.Opcion = 2;
                        //int ID = 0;
                        //int.TryParse(collection["IDTipoVehiculo"], out ID);
                        //TipoVehiculo.IDTipoVehiculo = ID;
                        //TipoVehiculo.Descripcion = collection["Descripcion"];
                        TipoVehiculo = TipoVehiculoDatos.AcCatTipoVehiculo(TipoVehiculo);
                        if (TipoVehiculo.Completado == true)
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
                            return View(TipoVehiculo);
                        }
                    }
                    else
                    {
                        return View(TipoVehiculo);
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

        // GET: Admin/CatTipoVehiculo/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/CatTipoVehiculo/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                CatTipoVehiculoModels TipoVehiculo = new CatTipoVehiculoModels();
                _CatTipoVehiculos_Datos TipoVehiculoDatos = new _CatTipoVehiculos_Datos();
                TipoVehiculo.Conexion = Conexion;
                TipoVehiculo.Usuario = User.Identity.Name;
                TipoVehiculo.IDTipoVehiculo = id;
                TipoVehiculo = TipoVehiculoDatos.EliminarTipoVehiculo(TipoVehiculo);
                if (TipoVehiculo.Completado == true)
                {
                    //TempData["typemessage"] = "1";
                    //TempData["message"] = "El registro se ha eliminado correctamente";
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
