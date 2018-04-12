using CreativaSL.Web.Ganados.Filters;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CreativaSL.Web.Ganados.Models;
using System.Data;
using CreativaSL.Web.Ganados.ViewModels;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    [Autorizado]
    public class NominaController : Controller
    {
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");

        // GET: Admin/Nomina
        public ActionResult Index()
        {
            try
            {
                NominaModels Nomina = new NominaModels();
                Nomina_Datos NominaDatos = new Nomina_Datos();
                _Combos_Datos Combos = new _Combos_Datos();
                Nomina.Conexion = Conexion;
                Nomina.ListaSucursales = Combos.ObtenerComboSucursales(Conexion);
                Nomina.ListaNomina = NominaDatos.ObtenerListaNomina(Nomina);
                return View(Nomina);
            }
            catch (Exception)
            {
                NominaModels Nomina = new NominaModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Nomina);
            }
        }

        //POST: Admin/Nomina/3
        [HttpPost]
        public ActionResult Index(NominaModels Nomina)
        {
            try
            {
                Nomina_Datos NominaDatos = new Nomina_Datos();
                _Combos_Datos Combos = new _Combos_Datos();
                Nomina.Conexion = Conexion;
                Nomina.ListaSucursales = Combos.ObtenerComboSucursales(Conexion);
                Nomina.EsBusqueda = true;
                if (!Nomina.BandBusqClave)
                {
                    Nomina.ClaveNomina = string.Empty;
                }
                if (!Nomina.BandIDSucursal)
                {
                    Nomina.IDSucursal = string.Empty;
                }
                if (!Nomina.BandBusqFechas)
                {
                    Nomina.FechaInicio = DateTime.Today;
                    Nomina.FechaFin = DateTime.Today;
                }
                if (string.IsNullOrEmpty(Nomina.IDSucursal))
                {
                    Nomina.BandIDSucursal = false;
                }
                if (!Nomina.BandBusqClave && !Nomina.BandIDSucursal && !Nomina.BandBusqFechas)
                {
                    Nomina.EsBusqueda = false;
                }
                Nomina.ListaNomina = NominaDatos.ObtenerListaNomina(Nomina);
                return View(Nomina);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET: Admin/Nomina/Create
        public ActionResult Create()
        {
            try
            {
                NominaModels Nomina = new NominaModels();
                Nomina_Datos NominaDatos = new Nomina_Datos();
                _Combos_Datos Combos = new _Combos_Datos();
                Nomina.Conexion = Conexion;
                Nomina.ListaSucursales = Combos.ObtenerComboSucursales(Conexion);
                return View(Nomina);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // POST: Admin/Nomina/Create
        [HttpPost]
        public ActionResult Create(NominaModels Nomina)
        {
            Nomina_Datos NominaDatos = new Nomina_Datos();
            _Combos_Datos Combos = new _Combos_Datos();
            try
            {
                if (ModelState.IsValid)
                {
                    Nomina.Conexion = Conexion;
                    Nomina.Usuario = User.Identity.Name;
                    Nomina.TablaEmpladoNomina = new DataTable();
                    Nomina.TablaEmpladoNomina.Columns.Add("IDEmpleado", typeof(string));
                    foreach (EmpleadoNominaViewModels Item in Nomina.ListaEmpleados)
                    {
                        if (Item.AbrirCaja)
                        {
                            object[] data = { Item.IDEmpleado };
                            Nomina.TablaEmpladoNomina.Rows.Add(data);
                        }
                    }
                    Nomina.CountEmpleado = Nomina.TablaEmpladoNomina.Rows.Count;
                    if (Nomina.CountEmpleado == 0)
                    {
                        Nomina.ListaSucursales = Combos.ObtenerComboSucursales(Conexion);
                        ModelState.AddModelError("", "Tienes que seleccionar al menos un empleado para la nómina");
                        return View(Nomina);
                    }
                    else
                    {
                        NominaDatos.ANomina(Nomina);
                        if (Nomina.Completado)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "Los empleados fueron dados de alta correctamente en la nómina.";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            Nomina.ListaSucursales = Combos.ObtenerComboSucursales(Conexion);
                            TempData["typemessage"] = "2";
                            TempData["message"] = "Los empleado no se guardaron correctamente. Intente más tarde.";
                            return View(Nomina);
                        }
                    }
                }
                else
                {
                    Nomina.ListaSucursales = Combos.ObtenerComboSucursales(Conexion);
                    return View(Nomina);
                }
            }
            catch
            {
                Nomina.ListaSucursales = Combos.ObtenerComboSucursales(Conexion);
                TempData["typemessage"] = "2";
                TempData["message"] = "Los empleado no se guardaron correctamente. Contacte a soporte técnico.";
                return View(Nomina);
            }
        }

        // GET: Admin/Nomina/Details/5
        public ActionResult Detalle(string id, string id2)
        {
            try
            {
                NominaModels Nomina = new NominaModels();
                Nomina_Datos NominaDatos = new Nomina_Datos();
                Nomina.Conexion = Conexion;
                Nomina.IDNomina = id;
                Nomina.IDSucursal = id2;
                Nomina.ListaNomina = NominaDatos.ObtenerListaDetalleNomina(Nomina);
                return View(Nomina);
            }
            catch (Exception)
            {
                NominaModels Nomina = new NominaModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Nomina);
            }
        }

        //GET: Admin/Nomina/DetalleEmpleado/2
        [HttpGet]
        public ActionResult DetalleEmpleado(string id, string id2, string id3)
        {
            try
            {
                NominaModels Nomina = new NominaModels();
                Nomina_Datos NominaDatos = new Nomina_Datos();
                Nomina.IDNomina = id;
                Nomina.IDSucursal = id2;
                Nomina.IDEmpleado = id3;
                Nomina.Conexion = Conexion;
                Nomina.listaConceptoNomina = NominaDatos.ObtenerConceptosNomina(Nomina);
                Nomina = NominaDatos.ObtenerListasDeConceptosXID(Nomina);
                return View(Nomina);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public ActionResult DetalleEmpleado(NominaModels Nomina)
        {
            try
            {
                
                Nomina_Datos NominaDatos = new Nomina_Datos();
               
                Nomina.Conexion = Conexion;
                Nomina.listaConceptoNomina = NominaDatos.ObtenerConceptosNomina(Nomina);
                return View(Nomina);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // POST: Admin/Nomina/getDatostablaEmpleado/3
        [HttpPost]
        public ActionResult DatostablaEmpleado(string IDS)
        {
            try
            {
                NominaModels Nomina = new NominaModels();
                Nomina_Datos NominaDatos = new Nomina_Datos();
                Nomina.IDSucursal = IDS;
                Nomina.Conexion = Conexion;

                Nomina.ListaNomina = NominaDatos.ObtenerListaNominaEmpleado(Nomina);
               
                return Content(Nomina.ListaNomina.ToJSON(), "application/json");
                //return Json(Nomina.ListaNomina, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }
    }
}
