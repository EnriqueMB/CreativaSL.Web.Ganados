using CreativaSL.Web.Ganados.App_Start;
using CreativaSL.Web.Ganados.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    public class CajaChicaController : Controller
    {
        private TokenProcessor Token = TokenProcessor.GetInstance();
        // GET: Admin/CajaChica
        public ActionResult Index()
        {
            try
            {
                _CajaChica_Datos regionDatos = new _CajaChica_Datos();
                List<CajaChicaModels> Lista = regionDatos.ObtenerCajasChicasAbiertas();
                return View(Lista);
            }
            catch (Exception)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la información";
                return View(new List<CajaChicaModels>());
            }
        }

        // GET: Admin/CajaChica/Create
        public ActionResult Create()
        {
            try
            {
                Token.SaveToken();
                _CajaChica_Datos datos = new _CajaChica_Datos();
                List<CatEmpleadoModels> ListaEmp = datos.LlenarComboEmpleadosContadores(false);
                ViewBag.ListaEmpleados = ListaEmp;
                return View(new CajaChicaModels());
            }
            catch (Exception)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                ViewBag.ListaEmpleados = new List<CatEmpleadoModels>();
                return View(new CajaChicaModels());
            }
        }


        // POST: Admin/CajaChica/Create
        [HttpPost]
        public ActionResult Create(CajaChicaModels model)
        {
            _CajaChica_Datos datos = new _CajaChica_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        int Resultado = datos.CrearCajaChica(model, User.Identity.Name);
                        if (Resultado == 1)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "Datos guardados correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            string mensajeError = string.Empty;
                            switch(Resultado)
                            {
                                case -1: mensajeError = "Contraseña incorrecta.";
                                    break;
                                case -2: mensajeError = "El empleado tiene caja abierta.";
                                    break;
                                case -3: mensajeError = "No se pudo guardar el registro. Intente nuevamente.";
                                    break;
                                default: mensajeError = "Ocurrió un error al intentar guardar los datos. Intente más tarde.";
                                    break;
                            }
                            TempData["typemessage"] = "2";
                            TempData["message"] = mensajeError;
                            ViewBag.ListaEmpleados = datos.LlenarComboEmpleadosContadores(true);
                            return View(model);
                        }
                    }
                    else
                    {
                        ViewBag.ListaEmpleados = datos.LlenarComboEmpleadosContadores(true);
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
                ViewBag.ListaEmpleados = datos.LlenarComboEmpleadosContadores(true);
                return View(model);
            }
        }

        // GET: Admin/CajaChica/ArqueoCaja
        public ActionResult ArqueoCaja(Int64 id)
        {
            try
            {
                Token.SaveToken();
                _CajaChica_Datos regionDatos = new _CajaChica_Datos();
                List<ArqueoCajaChicaModels> Lista = regionDatos.ObtenerDenominaciones(id);
                ViewBag.IdCaja = id;
                return View(Lista);
            }
            catch (Exception)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la información";
                return View(new List<ArqueoCajaChicaModels>());
            }
        }

        // POST: Admin/CajaChica/ArqueoCaja
        [HttpPost]
        public ActionResult ArqueoCaja(Int64 idCaja, List<ArqueoCajaChicaModels> model)
        {
            _CajaChica_Datos datos = new _CajaChica_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        int Resultado = datos.GuardarArqueoCaja(idCaja, model, User.Identity.Name);
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
                            ViewBag.IdCaja = idCaja;
                            return View(model);
                        }
                    }
                    else
                    {
                        ViewBag.IdCaja = idCaja;
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
                ViewBag.IdCaja = idCaja;
                return View(model);
            }
        }

        // POST: Admin/CajaChica/Delete
        [HttpPost]
        public ActionResult Delete(Int64 id)
        {
            _CajaChica_Datos datos = new _CajaChica_Datos();
            try
            {
                int Resultado = datos.EliminarCaja(id, User.Identity.Name);
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

        // GET: Admin/CajaChica/Movimientos
        public ActionResult Movimientos(Int64 id)
        {
            try
            {
                _CajaChica_Datos regionDatos = new _CajaChica_Datos();
                List<MovimientosCajaChicaModels> Lista = regionDatos.ObtenerDetalleXIdCaja(id);
                ViewBag.IdCaja = id;
                return View(Lista);
            }
            catch (Exception)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la información";
                return RedirectToAction("Index");
            }
        }

    }
}
