using CreativaSL.Web.Ganados.Filters;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CreativaSL.Web.Ganados.Models;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    [Autorizado]
    public class CatEmpleadoController : Controller
    {
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
        // GET: Admin/CatEmpleado
        public ActionResult Index()
        {
            try
            {
                CatEmpleadoModels Empleado = new CatEmpleadoModels();
                CatEmpleado_Datos EmpleadoDatos = new CatEmpleado_Datos();
                Empleado.Conexion = Conexion;
                Empleado.ListaEmpleado = EmpleadoDatos.ObtenerCatEmpleado(Empleado);
                return View(Empleado);
            }
            catch (Exception)
            {
                CatEmpleadoModels Empleado = new CatEmpleadoModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Empleado);
            }
        }

        // GET: Admin/CatEmpleado/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/CatEmpleado/Create
        public ActionResult Create()
        {
            try
            {
                CatEmpleadoModels Empleado = new CatEmpleadoModels();
                CatEmpleado_Datos EmpleadoDatos = new CatEmpleado_Datos();
                Empleado.Conexion = Conexion;
                Empleado.ListaCmbGrupoSanguineo = EmpleadoDatos.ObteneComboCatGrupoSanguineo(Empleado);
                Empleado.ListaCmbSucursal = EmpleadoDatos.ObteneComboCatSucursal(Empleado);
                Empleado.ListaCmbPuesto = EmpleadoDatos.obtenerComboCatPuesto(Empleado);
                Empleado.ListaCmbCategoriaPuesto = EmpleadoDatos.ObteneComboCatCategoriaPuesto(Empleado);
                return View(Empleado);
            }
            catch (Exception)
            {
                CatEmpleadoModels Empleado = new CatEmpleadoModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("Index");
            }
        }

        // POST: Admin/CatEmpleado/Create
        [HttpPost]
        public ActionResult Create(CatEmpleadoModels DatosEmpleado)
        {
            CatEmpleado_Datos EmpleadoDatos = new CatEmpleado_Datos();
            try
            {
                if (ModelState.IsValid)
                {
                    DatosEmpleado.Conexion = Conexion;
                    DatosEmpleado.Usuario = User.Identity.Name;
                    DatosEmpleado.Opcion = 1;
                    DatosEmpleado.AltaNominal = false;
                    DatosEmpleado = EmpleadoDatos.AbcCatEmpleado(DatosEmpleado);
                    if (DatosEmpleado.Completado)
                    {
                        TempData["typemessage"] = "1";
                        TempData["message"] = "Los datos se guardaron correctamente.";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        DatosEmpleado.ListaCmbGrupoSanguineo = EmpleadoDatos.ObteneComboCatGrupoSanguineo(DatosEmpleado);
                        DatosEmpleado.ListaCmbSucursal = EmpleadoDatos.ObteneComboCatSucursal(DatosEmpleado);
                        DatosEmpleado.ListaCmbPuesto = EmpleadoDatos.obtenerComboCatPuesto(DatosEmpleado);
                        DatosEmpleado.ListaCmbCategoriaPuesto = EmpleadoDatos.ObteneComboCatCategoriaPuesto(DatosEmpleado);
                        TempData["typemessage"] = "2";
                        TempData["message"] = "Ocurrio un error al intentar guardar los datos. Intente más tarde.";
                        return View(DatosEmpleado);
                    }
                }
                else
                {
                    DatosEmpleado.Conexion = Conexion;
                    DatosEmpleado.ListaCmbGrupoSanguineo = EmpleadoDatos.ObteneComboCatGrupoSanguineo(DatosEmpleado);
                    DatosEmpleado.ListaCmbSucursal = EmpleadoDatos.ObteneComboCatSucursal(DatosEmpleado);
                    DatosEmpleado.ListaCmbPuesto = EmpleadoDatos.obtenerComboCatPuesto(DatosEmpleado);
                    DatosEmpleado.ListaCmbCategoriaPuesto = EmpleadoDatos.ObteneComboCatCategoriaPuesto(DatosEmpleado);
                    return View(DatosEmpleado);
                }
            }
            catch
            {
                DatosEmpleado.Conexion = Conexion;
                DatosEmpleado.ListaCmbGrupoSanguineo = EmpleadoDatos.ObteneComboCatGrupoSanguineo(DatosEmpleado);
                DatosEmpleado.ListaCmbSucursal = EmpleadoDatos.ObteneComboCatSucursal(DatosEmpleado);
                DatosEmpleado.ListaCmbPuesto = EmpleadoDatos.obtenerComboCatPuesto(DatosEmpleado);
                DatosEmpleado.ListaCmbCategoriaPuesto = EmpleadoDatos.ObteneComboCatCategoriaPuesto(DatosEmpleado);
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error al intentar guardar los datos. Contacte a soporte técnico.";
                return View(DatosEmpleado);
            }
        }

        // GET: Admin/CatEmpleado/Edit/5
        [HttpGet]
        public ActionResult Edit(string id)
        {
            try
            {
                CatEmpleadoModels Empleado = new CatEmpleadoModels();
                CatEmpleado_Datos EmleadoDatos = new CatEmpleado_Datos();
                Empleado.Conexion = Conexion;
                Empleado.IDEmpleado = id;
                Empleado = EmleadoDatos.ObtenerDetalleCatEmpleado(Empleado);
                Empleado.ListaCmbGrupoSanguineo = EmleadoDatos.ObteneComboCatGrupoSanguineo(Empleado);
                Empleado.ListaCmbSucursal = EmleadoDatos.ObteneComboCatSucursal(Empleado);
                Empleado.ListaCmbPuesto = EmleadoDatos.obtenerComboCatPuesto(Empleado);
                Empleado.ListaCmbCategoriaPuesto = EmleadoDatos.ObteneComboCatCategoriaPuesto(Empleado);
                return View(Empleado);
            }
            catch (Exception)
            {
                CatEmpleadoModels Empleado = new CatEmpleadoModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("Index");
            }
        }

        // POST: Admin/CatEmpleado/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, CatEmpleadoModels DatosEmpleado)
        {
            CatEmpleado_Datos EmpleadoDatos = new CatEmpleado_Datos();
            try
            {
                if (ModelState.IsValid)
                {
                    DatosEmpleado.Conexion = Conexion;
                    DatosEmpleado.Usuario = User.Identity.Name;
                    DatosEmpleado.Opcion = 2;
                    DatosEmpleado = EmpleadoDatos.AbcCatEmpleado(DatosEmpleado);
                    if (DatosEmpleado.Completado)
                    {
                        TempData["typemessage"] = "1";
                        TempData["message"] = "Los datos se guardaron correctamente.";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        DatosEmpleado.ListaCmbGrupoSanguineo = EmpleadoDatos.ObteneComboCatGrupoSanguineo(DatosEmpleado);
                        DatosEmpleado.ListaCmbSucursal = EmpleadoDatos.ObteneComboCatSucursal(DatosEmpleado);
                        DatosEmpleado.ListaCmbPuesto = EmpleadoDatos.obtenerComboCatPuesto(DatosEmpleado);
                        DatosEmpleado.ListaCmbCategoriaPuesto = EmpleadoDatos.ObteneComboCatCategoriaPuesto(DatosEmpleado);
                        TempData["typemessage"] = "2";
                        TempData["message"] = "Ocurrio un error al intentar guardar los datos. Intente más tarde.";
                        return View(DatosEmpleado);
                    }
                }
                else
                {
                    DatosEmpleado.Conexion = Conexion;
                    DatosEmpleado.ListaCmbGrupoSanguineo = EmpleadoDatos.ObteneComboCatGrupoSanguineo(DatosEmpleado);
                    DatosEmpleado.ListaCmbSucursal = EmpleadoDatos.ObteneComboCatSucursal(DatosEmpleado);
                    DatosEmpleado.ListaCmbPuesto = EmpleadoDatos.obtenerComboCatPuesto(DatosEmpleado);
                    DatosEmpleado.ListaCmbCategoriaPuesto = EmpleadoDatos.ObteneComboCatCategoriaPuesto(DatosEmpleado);
                    return View(DatosEmpleado);
                }
            }
            catch
            {
                DatosEmpleado.Conexion = Conexion;
                DatosEmpleado.ListaCmbGrupoSanguineo = EmpleadoDatos.ObteneComboCatGrupoSanguineo(DatosEmpleado);
                DatosEmpleado.ListaCmbSucursal = EmpleadoDatos.ObteneComboCatSucursal(DatosEmpleado);
                DatosEmpleado.ListaCmbPuesto = EmpleadoDatos.obtenerComboCatPuesto(DatosEmpleado);
                DatosEmpleado.ListaCmbCategoriaPuesto = EmpleadoDatos.ObteneComboCatCategoriaPuesto(DatosEmpleado);
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error al intentar guardar los datos. Contacte a soporte técnico.";
                return View(DatosEmpleado);
            }
        }

        // POST: Admin/CatEmpleado/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                CatEmpleadoModels Empleado = new CatEmpleadoModels();
                CatEmpleado_Datos EmpleadoDatos = new CatEmpleado_Datos();
                Empleado.Conexion = Conexion;
                Empleado.Usuario = User.Identity.Name;
                Empleado.IDEmpleado = id;
                EmpleadoDatos.EliminarEmpleado(Empleado);
                TempData["typemessage"] = "1";
                TempData["message"] = "El registro se ha eliminado correctamente";
                return Json("");
            }
            catch
            {
                return View();
            }
        }

        // POST: Admin/CatEmpleado/ObtenerComboCategoriaPuesto/3
        [HttpPost]
        public ActionResult ObtenerComboCategoriaPuesto(int IDP)
        {
            try
            {
                CatEmpleadoModels Empleado = new CatEmpleadoModels();
                CatEmpleado_Datos EmpleadoDatos = new CatEmpleado_Datos();
                Empleado.IDPuesto = IDP;
                Empleado.Conexion = Conexion;
                List<CatCategoriaPuestoModels> Lista = EmpleadoDatos.ObteneComboCatCategoriaPuesto(Empleado);
                return Json(Lista, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        // POST: Admin/CatEmpleado/AltaBajaNomina/5
        [HttpPost]
        public ActionResult AltaBajaNomina(string id, bool id2)
        {
            try
            {
                CatEmpleadoModels Empleado = new CatEmpleadoModels();
                CatEmpleado_Datos EmpleadoDatos = new CatEmpleado_Datos();
                Empleado.Conexion = Conexion;
                Empleado.Usuario = User.Identity.Name;
                Empleado.IDEmpleado = id;
                Empleado.AltaNominal = id2;
                EmpleadoDatos.AltaBajaNominaEmpleado(Empleado);
                if (Empleado.AltaNominal)
                {
                    TempData["typemessage"] = "1";
                    TempData["message"] = "El Empleado fue dado de baja correctamente.";
                    return Json("");
                }
                else
                {
                    TempData["typemessage"] = "1";
                    TempData["message"] = "El Empleado fue dado de alta correctamente.";
                    return Json("");
                }

            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/CatEmpleado/Vacaciones/5
        [HttpGet]
        public ActionResult Vacaciones(string id)
        {
            try
            {
                NominaVacacionesModels Vacaciones = new NominaVacacionesModels();
                CatEmpleado_Datos EmpleadoDatos = new CatEmpleado_Datos();
                Vacaciones.Conexion = Conexion;
                Vacaciones.IDEmpleado = id;
                Vacaciones.ListaNominaVacaciones = EmpleadoDatos.ObtenerVacacionesNomina(Vacaciones);
                return View(Vacaciones);
            }
            catch (Exception)
            {
                NominaVacacionesModels Vacaciones = new NominaVacacionesModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("Index");
            }
        }

        // GET_ Admin/CatEmpleado/CreateVacaciones/5
        [HttpGet]
        public ActionResult CreateVacaciones(string id)
        {
            try
            {
                NominaVacacionesModels Vacaciones = new NominaVacacionesModels();
                Vacaciones.IDEmpleado = id;
                return View(Vacaciones);
            }
            catch (Exception)
            {
                NominaVacacionesModels Vacaciones = new NominaVacacionesModels();
                Vacaciones.IDEmpleado = id;
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("Vacaciones", new { id = Vacaciones.IDEmpleado });
            }
        }

        //POST: Admin/CatEmpleado/CreateVacaciones/5
        [HttpPost]
        public ActionResult CreateVacaciones(NominaVacacionesModels Vacaciones)
        {
            CatEmpleado_Datos EmpleadoDatos = new CatEmpleado_Datos();
            try
            {
                if (ModelState.IsValid)
                {
                    Vacaciones.Conexion = Conexion;
                    Vacaciones.Opcion = 1;
                    Vacaciones.Usuario = User.Identity.Name;
                    Vacaciones = EmpleadoDatos.AVacacionesNomina(Vacaciones);
                    if (Vacaciones.Completado)
                    {
                        TempData["typemessage"] = "1";
                        TempData["message"] = "Los datos se guardaron correctamente.";
                        return RedirectToAction("Vacaciones", new { id = Vacaciones.IDEmpleado });
                    }
                    else
                    {
                        if (Vacaciones.Resultado == 51000)
                        {
                            TempData["typemessage"] = "2";
                            TempData["message"] = "El empleado ya cuenta con esa fecha asignado. Selecciones un nuevo rango de fechas";
                            return View(Vacaciones);
                        }
                        else
                        {
                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrió un error al intentar guardar los datos. Intente más tarde.";
                            return View(Vacaciones);
                        }
                    }
                }
                else
                {
                    return View(Vacaciones);
                }
               
            }
            catch (Exception)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error al intentar guardar los datos. Contacte a soporte técnico.";
                return View(Vacaciones);
            }
        }

        // POST: Admin/CatEmpleado/DeleteVacaciones/5
        [HttpPost]
        public ActionResult DeleteVacaciones(string id, string id2)
        {
            try
            {
                NominaVacacionesModels Datos = new NominaVacacionesModels
                {
                    IDVacaciones = id,
                    IDEmpleado = id2,
                    Conexion = Conexion,
                    Usuario = User.Identity.Name
                };
                CatEmpleado_Datos EmpleadoDatos = new CatEmpleado_Datos();
                EmpleadoDatos.EliminarVacacionesEmpleado(Datos);
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
    }
}
