using CreativaSL.Web.Ganados.App_Start;
using CreativaSL.Web.Ganados.Models;
using CreativaSL.Web.Ganados.ViewModels;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.IO;
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

        public ActionResult ReporteCajaChica(Int64 id)
        {
            try
            {
                _CajaChica_Datos datos = new _CajaChica_Datos();
                Reporte_Datos R = new Reporte_Datos();
                ReporteCajaChica model = datos.ObtenerDatosReporteCajaChica(id);
                DatosEmpresaViewModels x = R.ObtenerDatosEmpresaGeneral(_ConexionRepositorio.CadenaConexion);
                LocalReport Rtp = new LocalReport
                {
                    EnableExternalImages = true
                };
                Rtp.DataSources.Clear();
                string path = Path.Combine(Server.MapPath("~/Reports"), "ReporteCajaChica.rdlc");
                if (System.IO.File.Exists(path))
                {
                    Rtp.ReportPath = path;
                }
                else
                {
                    return RedirectToAction("Index", "HistorialCajaChica");
                }
                ReportParameter[] Parametros = new ReportParameter[6];
                Parametros[0] = new ReportParameter("Empresa", x.RazonFiscal);
                Parametros[1] = new ReportParameter("RFC", x.RFC);
                Parametros[2] = new ReportParameter("Direccion", x.DireccionFiscal);
                Parametros[3] = new ReportParameter("Telefono", x.NumTelefonico1);
                Parametros[4] = new ReportParameter("TelefonoMovil", x.NumTelefonico2);
                Parametros[5] = new ReportParameter("UrlLogo", x.LogoEmpresa);
                
                Rtp.SetParameters(Parametros);
                Rtp.DataSources.Add(new ReportDataSource("Movimientos", model.ListaMovimientos));
                Rtp.DataSources.Add(new ReportDataSource("Arqueo", model.ListaDenominaciones));
                Rtp.DataSources.Add(new ReportDataSource("Conceptos", model.ListaConceptos));
                Rtp.DataSources.Add(new ReportDataSource("MovimientosCheque", model.ListaMovimientosCheque));
                string reportType = "PDF";
                string mimeType;
                string encoding;
                string fileNameExtension;

                string deviceInfo = "<DeviceInfo>" +
                "  <OutputFormat>" + reportType + "</OutputFormat>" +
                "</DeviceInfo>";

                Warning[] warnings;
                string[] streams;
                byte[] renderedBytes;

                renderedBytes = Rtp.Render(
                    reportType,
                    deviceInfo,
                    out mimeType,
                    out encoding,
                    out fileNameExtension,
                    out streams,
                    out warnings);

                return File(renderedBytes, mimeType);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "HistorialCajaChica");
            }
        }

        // GET: Admin/CajaChica/Movimientos/EntradaCajaChica/1
        [HttpGet]
        public ActionResult EntradaCajaChica(Int64 id)
        {
            try
            {
                Token.SaveToken();
                CajaChicaModels cajaChica = new CajaChicaModels();
                ViewBag.IdCaja = id;
                cajaChica.IdCaja = id;
                return View(cajaChica);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // POST: Admin/CajaChica/Movimientos/EntradaCajaChica/1
        [HttpPost]
        public ActionResult EntradaCajaChica(CajaChicaModels model)
        {
            _CajaChica_Datos datos = new _CajaChica_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    ModelState.Remove("IdPropietario");
                    if (ModelState.IsValid)
                    {
                        int Resultado = datos.GuardarMovimientoEntrada(model, User.Identity.Name);
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
                            switch (Resultado)
                            {
                                case -1:
                                    ViewBag.IdCaja = model.IdCaja;
                                    mensajeError = "Contraseña incorrecta.";
                                    break;
                                default:
                                    ViewBag.IdCaja = model.IdCaja;
                                    mensajeError = "Ocurrió un error al intentar guardar los datos. Intente más tarde.";
                                    break;
                            }
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
        //parte de boton ver en cajachica
        public ActionResult ModalTicket2(int ID)
        {
            CajaChicaModels Imagen = new CajaChicaModels();
            Imagen.IdCaja = ID;
  
            _CajaChica_Datos datos = new _CajaChica_Datos();
            datos.ObtenerImagenCajaChica(Imagen);

            
            return PartialView("ModalTicket2", Imagen);
        }
    }
}
