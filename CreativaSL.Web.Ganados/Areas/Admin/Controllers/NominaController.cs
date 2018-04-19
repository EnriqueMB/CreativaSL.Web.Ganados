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
using Rotativa;

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
            Nomina_Datos NominaDatos = new Nomina_Datos();
            _Combos_Datos Combos = new _Combos_Datos();
            try
            {

                ModelState.Remove("IDSucursal");
                if (ModelState.IsValid)
                {
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
                else
                {
                    Nomina.ListaSucursales = Combos.ObtenerComboSucursales(Conexion);
                    return View(Nomina);
                }
            }
            catch (Exception)
            {
               
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Nomina);
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
                NominaModels Nomina = new NominaModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("Index");
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
        //GET: Admin/Nomina/DetalleEmpleado/2
        [HttpPost]
        public ActionResult DetalleEmpleado(NominaModels Nomina)
        {
            Nomina_Datos NominaDatos = new Nomina_Datos();
            try
            {
                Nomina.Conexion = Conexion;
                Nomina.Usuario = User.Identity.Name;
                Nomina = NominaDatos.AgregarConceptoNomina(Nomina);
                if (Nomina.Completado)
                {
                    Nomina.listaConceptoNomina = NominaDatos.ObtenerConceptosNomina(Nomina);
                    Nomina = NominaDatos.ObtenerListasDeConceptosXID(Nomina);
                    TempData["typemessage"] = "1";
                    TempData["message"] = "Los datos se guardarón correctamente.";
                    return View(Nomina);
                    //return RedirectToAction("DetalleEmpleado", "Nomina", new { id = Nomina.IDNomina, id2 = Nomina.IDSucursal, id3 = Nomina.IDEmpleado });
                }
                else
                {
                    if (Nomina.Resultado == -1)
                    {
                        Nomina.listaConceptoNomina = NominaDatos.ObtenerConceptosNomina(Nomina);
                        Nomina = NominaDatos.ObtenerListasDeConceptosXID(Nomina);
                        TempData["typemessage"] = "2";
                        TempData["message"] = "El concepto ya fue insertado.";
                        return View(Nomina);
                       // return RedirectToAction("DetalleEmpleado", "Nomina", new { id = Nomina.IDNomina, id2 = Nomina.IDSucursal, id3 = Nomina.IDEmpleado });
                    }
                    else
                    {
                        Nomina.listaConceptoNomina = NominaDatos.ObtenerConceptosNomina(Nomina);
                        Nomina = NominaDatos.ObtenerListasDeConceptosXID(Nomina);
                        TempData["typemessage"] = "2";
                        TempData["message"] = "Ocurrio un error al intentar guardar los datos. Intente más tarde.";
                        return View(Nomina);
                        //return RedirectToAction("DetalleEmpleado", "Nomina", new { id = Nomina.IDNomina, id2 = Nomina.IDSucursal, id3 = Nomina.IDEmpleado });
                    }
                }
            }
            catch (Exception)
            {
                Nomina.listaConceptoNomina = NominaDatos.ObtenerConceptosNomina(Nomina);
                Nomina = NominaDatos.ObtenerListasDeConceptosXID(Nomina);
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error al intentar guardar los datos. Contacte soporte técnico.";
                return View(Nomina);
               // return RedirectToAction("DetalleEmpleado", "Nomina", new { id = Nomina.IDNomina, id2 = Nomina.IDSucursal, id3 = Nomina.IDEmpleado });
            }
        }
        // GET: Admin/EntregaCombustible/Delete/5
        public ActionResult DeleteConcepto(int id)
        {
            return View();
        }

        // POST: Admin/EntregaCombustible/Delete/5
        [HttpPost]
        public ActionResult DeleteConcepto(string id,string id2,bool id3,FormCollection collection)
        {
            try
            {
                NominaModels Nomina = new NominaModels();
                Nomina_Datos NominaDatos = new Nomina_Datos();
                Nomina.Conexion = Conexion;
                Nomina.IDSucursal = id2;
                Nomina.EsFijo = id3;
                Nomina.IDEmpleado = id;
                Nomina.Usuario = User.Identity.Name;
                Nomina = NominaDatos.ElimnarConceptosNomina(Nomina);

                return Json("");
                // TODO: Add delete logic here


            }
            catch
            {
                NominaModels Nomina = new NominaModels();

                TempData["typemessage"] = "2";
                TempData["message"] = "No se pudo borrar los datos. Por favor contacte a soporte técnico";
                return Json("");

            }
        }


        //GET: Admin/Nomina/RptDiasLaborados/3/3
        [HttpGet]
        public ActionResult RptDiasLaborados(string id, string id2)
        {
            try
            {
                NominaModels Nomina = new NominaModels();
                Nomina_Datos NominaD = new Nomina_Datos();
                Nomina.Conexion = Conexion;
                Nomina.IDNomina = id;
                Nomina.IDSucursal = id2;
                Nomina = NominaD.ObtenerDatosEmpresaTipo1(Nomina);
                NominaD.ObtenerReporteNominaDetalle(Nomina);
                return View(Nomina);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        public ActionResult RptDiasLaborados2(NominaModels Nomina)
        {
            try
            {
                Nomina_Datos NominaD = new Nomina_Datos();
                Nomina.Conexion = Conexion;
                Nomina = NominaD.ObtenerDatosEmpresaTipo1(Nomina);
                NominaD.ObtenerReporteNominaDetalle(Nomina);
                return View(Nomina);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult PDF_RptDiasLaborados(string id, string id2)
        {
            try
            {
                string customSwitches = string.Format("--header-center  \"COMO USAR O ROTATIVA\" " +
                        "--header-spacing \"8\" " +
                        "--header-font-name \"Open Sans\" " +
                        "--footer-font-size \"8\" " +
                        "--footer-font-name \"Open Sans\" " +
                        "--header-font-size \"10\" " +
                        "--footer-right \"Pag: [page] de [toPage]\"");

                var report = new ActionAsPdf("RptDiasLaborados", new { id , id2} )
                {
                    //FileName = "Invoice.pdf",
                    PageOrientation = Rotativa.Options.Orientation.Landscape,
                    CustomSwitches = "--margin-bottom 15 --margin-left 10 --margin-right 10 --margin-top 17 --footer-right \"Fecha: [date]\" " + "--footer-center \"Pagina: [page] de [toPage]\" --footer-line --footer-font-size \"12\" --footer-spacing 5 --footer-font-name \"calibri light\""//"--page-offset 0 --footer-center [page] --footer-font-size 12 --viewport-size 1000x1000"
                    //PageMargins = new Rotativa.Options.Margins(30, 10, 15, 10)
                    //pageMargins = new Rotativa.Options.Margins()
                };
                return report;
             
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }

        //GET: Admin/Nomina/RptSaldos/3/3
        [HttpGet]
        public ActionResult RptSaldos(string id, string id2)
        {
            try
            {
                NominaModels Nomina = new NominaModels();
                Nomina_Datos NominaD = new Nomina_Datos();
                Nomina.Conexion = Conexion;
                Nomina.IDNomina = id;
                Nomina.IDSucursal = id2;
                Nomina = NominaD.ObtenerDatosEmpresaTipo1(Nomina);
                NominaD.ObtenerReporteNominaSaldos(Nomina);
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
