using CreativaSL.Web.Ganados.Filters;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CreativaSL.Web.Ganados.Models;
using CreativaSL.Web.Ganados.App_Start;
using System.IO;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    [Autorizado]
    public class CatEmpleadoController : Controller
    {
        private TokenProcessor Token = TokenProcessor.GetInstance();
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
                Token.SaveToken();
                CatEmpleadoModels Empleado = new CatEmpleadoModels();
                CatEmpleado_Datos EmpleadoDatos = new CatEmpleado_Datos();
                _Combos_Datos Combos = new _Combos_Datos();
                Empleado.Conexion = Conexion;
                Empleado.ListaCmbGrupoSanguineo = EmpleadoDatos.ObteneComboCatGrupoSanguineo(Empleado);
                Empleado.ListaCmbSucursal = Combos.ObtenerComboSucursales(Conexion);
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
            _Combos_Datos Combos = new _Combos_Datos();
            try
            {
                if (Token.IsTokenValid())
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
                            Token.ResetToken();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            DatosEmpleado.ListaCmbGrupoSanguineo = EmpleadoDatos.ObteneComboCatGrupoSanguineo(DatosEmpleado);
                            DatosEmpleado.ListaCmbSucursal = Combos.ObtenerComboSucursales(Conexion);
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
                        DatosEmpleado.ListaCmbSucursal = Combos.ObtenerComboSucursales(Conexion);
                        DatosEmpleado.ListaCmbPuesto = EmpleadoDatos.obtenerComboCatPuesto(DatosEmpleado);
                        DatosEmpleado.ListaCmbCategoriaPuesto = EmpleadoDatos.ObteneComboCatCategoriaPuesto(DatosEmpleado);
                        return View(DatosEmpleado);
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                DatosEmpleado.Conexion = Conexion;
                DatosEmpleado.ListaCmbGrupoSanguineo = EmpleadoDatos.ObteneComboCatGrupoSanguineo(DatosEmpleado);
                DatosEmpleado.ListaCmbSucursal = Combos.ObtenerComboSucursales(Conexion);
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
                Token.SaveToken();
                CatEmpleadoModels Empleado = new CatEmpleadoModels();
                CatEmpleado_Datos EmleadoDatos = new CatEmpleado_Datos();
                _Combos_Datos Combos = new _Combos_Datos();
                Empleado.Conexion = Conexion;
                Empleado.IDEmpleado = id;
                Empleado = EmleadoDatos.ObtenerDetalleCatEmpleado(Empleado);
                Empleado.ListaCmbGrupoSanguineo = EmleadoDatos.ObteneComboCatGrupoSanguineo(Empleado);
                Empleado.ListaCmbSucursal = Combos.ObtenerComboSucursales(Conexion);
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
            _Combos_Datos Combos = new _Combos_Datos();
            try
            {
                if (Token.IsTokenValid())
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
                            Token.ResetToken();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            DatosEmpleado.ListaCmbGrupoSanguineo = EmpleadoDatos.ObteneComboCatGrupoSanguineo(DatosEmpleado);
                            DatosEmpleado.ListaCmbSucursal = Combos.ObtenerComboSucursales(Conexion);
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
                        DatosEmpleado.ListaCmbSucursal = Combos.ObtenerComboSucursales(Conexion);
                        DatosEmpleado.ListaCmbPuesto = EmpleadoDatos.obtenerComboCatPuesto(DatosEmpleado);
                        DatosEmpleado.ListaCmbCategoriaPuesto = EmpleadoDatos.ObteneComboCatCategoriaPuesto(DatosEmpleado);
                        return View(DatosEmpleado);
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                DatosEmpleado.Conexion = Conexion;
                DatosEmpleado.ListaCmbGrupoSanguineo = EmpleadoDatos.ObteneComboCatGrupoSanguineo(DatosEmpleado);
                DatosEmpleado.ListaCmbSucursal = Combos.ObtenerComboSucursales(Conexion);
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
                //TempData["typemessage"] = "1";
                //TempData["message"] = "El registro se ha eliminado correctamente";
                return Json("");
            }
            catch
            {
                CatEmpleadoModels Empleado = new CatEmpleadoModels();

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

        [HttpPost]
        public ActionResult ObtenerSueldoCategoriaPuesto(string IDCP)
        {
            try
            {
                CatEmpleadoAltaNominaModels Empleado = new CatEmpleadoAltaNominaModels();
                CatEmpleado_Datos EmpleadoDatos = new CatEmpleado_Datos();
                Empleado.IDCategoriaPuesto = IDCP;
                Empleado.Conexion = Conexion;
                Empleado = EmpleadoDatos.GetSueldoBaseCategoriaPuesto(Empleado);
                return Json(Empleado.sueldoBase, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        // POST: Admin/CatEmpleado/AltaBajaNomina/5
        //[HttpPost]
        //public ActionResult AltaBajaNomina(string id, bool id2)
        //{
        //    try
        //    {
        //        CatEmpleadoModels Empleado = new CatEmpleadoModels();
        //        CatEmpleado_Datos EmpleadoDatos = new CatEmpleado_Datos();
        //        Empleado.Conexion = Conexion;
        //        Empleado.Usuario = User.Identity.Name;
        //        Empleado.IDEmpleado = id;
        //        Empleado.AltaNominal = id2;
        //        EmpleadoDatos.AltaBajaNominaEmpleado(Empleado);
        //        if (Empleado.AltaNominal)
        //        {
        //            TempData["typemessage"] = "1";
        //            TempData["message"] = "El Empleado fue dado de baja correctamente.";
        //            return Json("");
        //        }
        //        else
        //        {
        //            TempData["typemessage"] = "1";
        //            TempData["message"] = "El Empleado fue dado de alta correctamente.";
        //            return Json("");
        //        }

        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

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
                Token.SaveToken();
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
                if (Token.IsTokenValid())
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
                            Token.ResetToken();
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
                else
                {
                    return RedirectToAction("Vacaciones", new { id = Vacaciones.IDEmpleado });
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
        // GET: Admin/CatEmpleado/Edit/5
        [HttpGet]
        public ActionResult AltaNomina(string id)
        {
            try
            {
                Token.SaveToken();
                CatEmpleadoAltaNominaModels EmpleadoNomina = new CatEmpleadoAltaNominaModels();
                CatEmpleadoModels Empleado = new CatEmpleadoModels();
                CatEmpleado_Datos EmleadoDatos = new CatEmpleado_Datos();
                EmpleadoNomina.Conexion = Conexion;
                Empleado.Conexion = Conexion;
                EmpleadoNomina.IDEmpleado = id;
                EmpleadoNomina = EmleadoDatos.GetEmpleadoAltaBaja(EmpleadoNomina);
                if (!EmpleadoNomina.Baja)
                {

                    EmpleadoNomina = EmleadoDatos.GetNombreEmpleado(EmpleadoNomina);
                    EmpleadoNomina.ListaCmbPuesto = EmleadoDatos.obtenerComboCatPuesto(Empleado);
                    EmpleadoNomina.ListaCmbCategoriaPuesto = EmleadoDatos.ObteneComboCatCategoriaPuesto(Empleado);
                    return View(EmpleadoNomina);
                }
                else
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "El empleado ya a sido dado de baja una vez";
                    Token.ResetToken();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                CatEmpleadoModels Empleado = new CatEmpleadoModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult AltaNomina(string id, CatEmpleadoAltaNominaModels datos)
        {
            CatEmpleado_Datos EmpleadoDatos = new CatEmpleado_Datos();
            CatEmpleadoModels Empleado = new CatEmpleadoModels();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        datos.Conexion = Conexion;
                        datos.Usuario = User.Identity.Name;
                        datos = EmpleadoDatos.AltaNominaEmpleado(datos);
                        if (datos.Completado && datos.Resultado == 1)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "Los datos se guardaron correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("Index");
                        }
                        else if (datos.Completado && datos.Resultado == 0)
                        {
                            Empleado.Conexion = Conexion;
                            datos.ListaCmbPuesto = EmpleadoDatos.obtenerComboCatPuesto(Empleado);
                            datos.ListaCmbCategoriaPuesto = EmpleadoDatos.ObteneComboCatCategoriaPuesto(Empleado);
                            TempData["typemessage"] = "2";
                            TempData["message"] = "El empleado ya fue dado de baja.";
                            return View(datos);
                        }
                        else
                        {
                            Empleado.Conexion = Conexion;
                            datos.ListaCmbPuesto = EmpleadoDatos.obtenerComboCatPuesto(Empleado);
                            datos.ListaCmbCategoriaPuesto = EmpleadoDatos.ObteneComboCatCategoriaPuesto(Empleado);
                            TempData["typemessage"] = "2";
                            TempData["message"] = "Error al tratar de dar de alta al empleado.";
                            return View(datos);
                        }
                    }
                    else
                    {
                        Empleado.Conexion = Conexion;
                        datos.ListaCmbPuesto = EmpleadoDatos.obtenerComboCatPuesto(Empleado);
                        datos.ListaCmbCategoriaPuesto = EmpleadoDatos.ObteneComboCatCategoriaPuesto(Empleado);
                        return View(datos);
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                datos.Conexion = Conexion;
                datos.ListaCmbPuesto = EmpleadoDatos.obtenerComboCatPuesto(Empleado);
                datos.ListaCmbCategoriaPuesto = EmpleadoDatos.ObteneComboCatCategoriaPuesto(Empleado);
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error al intentar guardar los datos. Contacte a soporte técnico.";
                return View(datos);
            }
        }

        [HttpGet]
        public ActionResult BajaNomina(string id)
        {
            try
            {
                Token.SaveToken();
                CatEmpleadoBajaNominaModels EmpleadoNominaB = new CatEmpleadoBajaNominaModels();
                CatEmpleadoAltaNominaModels EmpleadoNominaA = new CatEmpleadoAltaNominaModels();
                CatEmpleado_Datos EmleadoDatos = new CatEmpleado_Datos();
                EmpleadoNominaB.Conexion = Conexion;
                EmpleadoNominaA.Conexion = Conexion;
                EmpleadoNominaB.IDEmpleado = id;
                EmpleadoNominaA.IDEmpleado = id;
                EmpleadoNominaA = EmleadoDatos.GetNombreEmpleado(EmpleadoNominaA);
                EmpleadoNominaB.NombreCompleto = EmpleadoNominaA.NombreCompleto;
                EmpleadoNominaB.ListaCmbMotivoBaja = EmleadoDatos.ObteneComboCatMotivoBaja(EmpleadoNominaB); 
                return View(EmpleadoNominaB);
            }
            catch (Exception)
            {
                CatEmpleadoModels Empleado = new CatEmpleadoModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("Index");
            }
        }

        public ActionResult BajaNomina(string id, CatEmpleadoBajaNominaModels datos)
        {
            CatEmpleado_Datos EmpleadoDatos = new CatEmpleado_Datos();
            CatEmpleadoModels Empleado = new CatEmpleadoModels();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        datos.Conexion = Conexion;
                        datos.Usuario = User.Identity.Name;
                        datos = EmpleadoDatos.BajaNominaEmpleado(datos);
                        if (datos.Completado && datos.Resultado == 1)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "El empleado fue dado de alta correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("Index");
                        }
                        else if (datos.Completado && datos.Resultado == 0)
                        {
                            datos.ListaCmbMotivoBaja = EmpleadoDatos.ObteneComboCatMotivoBaja(datos);
                            TempData["typemessage"] = "2";
                            TempData["message"] = "El empleado debe estar dado de alta.";
                            return View(datos);
                        }
                        else
                        {
                            datos.ListaCmbMotivoBaja = EmpleadoDatos.ObteneComboCatMotivoBaja(datos);
                            TempData["typemessage"] = "2";
                            TempData["message"] = "Error al dar de baja al empleado.";
                            return View(datos);
                        }
                    }
                    else
                    {
                        datos.Conexion = Conexion;
                        datos.ListaCmbMotivoBaja = EmpleadoDatos.ObteneComboCatMotivoBaja(datos);
                        return View(datos);
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                datos.Conexion = Conexion;
                datos.ListaCmbMotivoBaja = EmpleadoDatos.ObteneComboCatMotivoBaja(datos);
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error al intentar guardar los datos. Contacte a soporte técnico.";
                return View(datos);
            }
        }

        [HttpPost]
        public ActionResult LoadTableArchivos(string id_empleado)
        {
            try
            {
                CatEmpleado_Datos Datos = new CatEmpleado_Datos();
                string datatable = Datos.EMPLEADO_index_Archivo(Conexion, id_empleado);

                return Content(datatable, "application/json");
            }
            catch (Exception ex)
            {
                return Content("", "application/json");
            }
        }

        [HttpGet]
        public ActionResult Archivos(string id, string nombreEmpleado)
        {
            try
            {
                if(string.IsNullOrEmpty(id.Trim()) || string.IsNullOrEmpty(nombreEmpleado))
                {
                    return RedirectToAction("Index");
                }

                ViewBag.NombreEmpleado = nombreEmpleado;
                ViewBag.Id_empleado = id;
                
                return View();
            }
            catch (Exception)
            {
                CatEmpleadoModels Empleado = new CatEmpleadoModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult AgregarArchivo(string id_empleado)
        {
            try
            {
                if (string.IsNullOrEmpty(id_empleado.Trim()))
                {
                    return RedirectToAction("Index");
                }

                ArchivoEmpleadoModels Archivo = new ArchivoEmpleadoModels();

                Archivo.Id_empleado = id_empleado;

                return View(Archivo);
            }
            catch (Exception ex)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Verifique sus datos.";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult AgregarArchivo(ArchivoEmpleadoModels ArchivoModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(ArchivoModel);

                }

                CatEmpleado_Datos Datos = new CatEmpleado_Datos();
                ArchivoModel.UrlArchivo = Guid.NewGuid().ToString() + Path.GetExtension(ArchivoModel.Archivo.FileName);
                ArchivoModel.NombreArchivo = ArchivoModel.Archivo.FileName;
                RespuestaAjax respuesta = Datos.EMPLEADO_ac_Archivo(ArchivoModel, Conexion, User.Identity.Name, 1);

                if (respuesta.Success)
                {
                    ArchivoModel.Archivo.SaveAs(Server.MapPath("~/ArchivosEmpleado/" + ArchivoModel.UrlArchivo));
                    TempData["typemessage"] = "1";
                }
                else
                {
                    TempData["typemessage"] = "2";
                }

                TempData["message"] = respuesta.Mensaje;

                return RedirectToAction("Archivos", "CatEmpleado", new { id = ArchivoModel.Id_empleado, nombreEmpleado = respuesta.Href });
            }
            catch (Exception ex)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Verifique sus datos.";
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult DescargarArchivo(string nombreArchivoServer, string nombreArchivo)
        {
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + "ArchivosEmpleado/";
                byte[] fileBytes = System.IO.File.ReadAllBytes(path + nombreArchivoServer);
                string fileName = nombreArchivo;
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult EliminarArchivo(string nombreArchivoServer, int? id)
        {
            try
            {
                RespuestaAjax respuesta = new RespuestaAjax();

                if ((string.IsNullOrEmpty(nombreArchivoServer.Trim())) || (id == null || id == 0))
                {
                    respuesta.Success = false;
                    respuesta.Mensaje = "Verifique sus datos";
                    return Content(respuesta.ToJSON(), "application/json");
                }

                //Borramos el archivo del servidor para no acumular basura
                string pathRoot = Server.MapPath("~/ArchivosEmpleado");
                string filePath = pathRoot + "\\" + nombreArchivoServer;

                if ((System.IO.File.Exists(filePath)))
                {
                    System.IO.File.Delete(filePath);
                    //Ponemos en activo 0 el archivo

                    CatEmpleado_Datos Datos = new CatEmpleado_Datos();
                    respuesta = Datos.EMPLEADO_del_Archivo(Conexion, id.Value);

                    respuesta.Success = respuesta.Success;
                    respuesta.Mensaje = respuesta.Mensaje;
                    return Content(respuesta.ToJSON(), "application/json");
                }
                else
                {
                    respuesta.Success = false;
                    respuesta.Mensaje = "Verifique sus datos";
                    return Content(respuesta.ToJSON(), "application/json");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
