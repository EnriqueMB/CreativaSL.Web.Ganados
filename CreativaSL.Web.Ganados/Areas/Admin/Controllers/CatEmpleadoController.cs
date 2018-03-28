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
                Empleado.ListaCmbGrupoSanguineo = EmleadoDatos.ObteneComboCatGrupoSanguineo(Empleado);
                Empleado.ListaCmbSucursal = EmleadoDatos.ObteneComboCatSucursal(Empleado);
                Empleado.ListaCmbPuesto = EmleadoDatos.obtenerComboCatPuesto(Empleado);
                Empleado.ListaCmbCategoriaPuesto = EmleadoDatos.ObteneComboCatCategoriaPuesto(Empleado);
                Empleado = EmleadoDatos.ObtenerDetalleCatEmpleado(Empleado);
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

        // GET: Admin/CatEmpleado/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/CatEmpleado/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
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
    }
}
