using CreativaSL.Web.Ganados.App_Start;
using CreativaSL.Web.Ganados.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    public class CatClasificacionController : Controller
    {
        private TokenProcessor Token = TokenProcessor.GetInstance();
        // GET: Admin/CatClasificacion
        public ActionResult Index()
        {
            try
            {
                _Clasificacion_Datos regionDatos = new _Clasificacion_Datos();
                List<Clasificacion> Lista = regionDatos.ObtenerCatClasificacionGeneral();
                return View(Lista);
            }
            catch (Exception)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la información";
                return View(new List<Clasificacion>());
            }
        }

        // GET: Admin/CatClasificacion/Create
        public ActionResult Create()
        {
            try
            {
                Token.SaveToken();
                return View(new Clasificacion());
            }
            catch (Exception)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("Index");
            }
        }

        // POST: Admin/CatClasificacion/Create
        [HttpPost]
        public ActionResult Create(Clasificacion model)
        {
            _Clasificacion_Datos datos = new _Clasificacion_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        int Resultado = datos.GuardarClasificacionGeneral(true, model, User.Identity.Name);
                        if (Resultado == 1)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "Datos guardados correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            string mensajeError = "Ocurrió un error al intentar guardar los datos. Intente más tarde.";                            
                            TempData["typemessage"] = "2";
                            TempData["message"] = mensajeError;
                            return View(model);
                        }
                    }
                    else
                    {
                        return View(model);
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Error al procesar los datos";
                return View(model);
            }
        }

        // GET: Admin/CatClasificacion/Edit
        public ActionResult Edit(int id)
        {
            try
            {
                Token.SaveToken();
                _Clasificacion_Datos datos = new _Clasificacion_Datos();
                Clasificacion model = datos.ObtenerDatosGeneral(id);
                return View(model);
            }
            catch (Exception)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("Index");
            }
        }

        // POST: Admin/CatClasificacion/Edit
        [HttpPost]
        public ActionResult Edit(int id, Clasificacion model)
        {
            model.IdTipoClasificacion = id;
            _Clasificacion_Datos datos = new _Clasificacion_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        int Resultado = datos.GuardarClasificacionGeneral(false, model, User.Identity.Name);
                        if (Resultado == 1)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "Datos guardados correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            string mensajeError = "Ocurrió un error al intentar guardar los datos. Intente más tarde.";
                            TempData["typemessage"] = "2";
                            TempData["message"] = mensajeError;
                            return View(model);
                        }
                    }
                    else
                    {
                        return View(model);
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Error al procesar los datos";
                return View(model);
            }
        }

        // POST: Admin/CatClasificacion/Delete
        [HttpPost]
        public ActionResult Delete(int id)
        {
            _Clasificacion_Datos datos = new _Clasificacion_Datos();
            try
            {
                int Resultado = datos.EliminarClasificacionGeneral(id, User.Identity.Name);
                if (Resultado == 1)
                {
                    return Json("true");
                }
                return Json("false");
            }
            catch
            {
                return Json("false");
            }
        }

        

        // GET: Admin/CatClasificacion
        public ActionResult Subcategorias(int id)
        {
            ViewBag.ParentId = id;
            try
            {
                _Clasificacion_Datos regionDatos = new _Clasificacion_Datos();
                List<Clasificacion> Lista = regionDatos.ObtenerCatClasificacion(id);
                return View(Lista);
            }
            catch (Exception)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la información";
                return RedirectToAction("Index");
            }
        }

        // GET: Admin/CatClasificacion/Create
        public ActionResult CreateSub(int id)
        {
            try
            {
                Token.SaveToken();
                return View(new Clasificacion {  ParentId = id });
            }
            catch (Exception)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(new Clasificacion { ParentId = id });
            }
        }

        // POST: Admin/CatClasificacion/Create
        [HttpPost]
        public ActionResult CreateSub(int id, Clasificacion model)
        {
            _Clasificacion_Datos datos = new _Clasificacion_Datos();
            try
            {
                model.ParentId = id;
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        int Resultado = datos.GuardarClasificacion(true, model, User.Identity.Name);
                        if (Resultado == 1)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "Datos guardados correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("Subcategorias", new { id = id });
                        }
                        else
                        {
                            string mensajeError = "Ocurrió un error al intentar guardar los datos. Intente más tarde.";
                            TempData["typemessage"] = "2";
                            TempData["message"] = mensajeError;
                            return View(model);
                        }
                    }
                    else
                    {
                        return View(model);
                    }
                }
                else
                {
                    return RedirectToAction("Subcategorias", new { id = id});
                }
            }
            catch (Exception)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Error al procesar los datos";
                return View(model);
            }
        }

        // GET: Admin/CatClasificacion/Edit
        public ActionResult EditSub(int id, int id2)
        {
            try
            {
                Token.SaveToken();
                _Clasificacion_Datos datos = new _Clasificacion_Datos();
                Clasificacion model = datos.ObtenerDatos(id);
                ViewBag.ParentId = model.ParentId;
                return View(model);
            }
            catch (Exception)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("Subcategorias", new { id = id2 });
            }
        }

        // POST: Admin/CatClasificacion/Edit
        [HttpPost]
        public ActionResult EditSub(int id, int id2, Clasificacion model)
        {
            model.IdTipoClasificacion = id;
            _Clasificacion_Datos datos = new _Clasificacion_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        int Resultado = datos.GuardarClasificacion(false, model, User.Identity.Name);
                        if (Resultado == 1)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "Datos guardados correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("Subcategorias", new { id = id2 });
                        }
                        else
                        {
                            string mensajeError = "Ocurrió un error al intentar guardar los datos. Intente más tarde.";
                            TempData["typemessage"] = "2";
                            TempData["message"] = mensajeError;
                            return View(model);
                        }
                    }
                    else
                    {
                        return View(model);
                    }
                }
                else
                {
                    return RedirectToAction("Subcategorias", new { id = id2 });
                }
            }
            catch (Exception)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Error al procesar los datos";
                return View(model);
            }
        }

        // POST: Admin/CatClasificacion/Delete
        [HttpPost]
        public ActionResult DeleteSub(int id)
        {
            _Clasificacion_Datos datos = new _Clasificacion_Datos();
            try
            {
                int Resultado = datos.EliminarClasificacion(id, User.Identity.Name);
                if (Resultado == 1)
                {
                    return Json("true");
                }
                return Json("false");
            }
            catch
            {
                return Json("false");
            }
        }

    }
}