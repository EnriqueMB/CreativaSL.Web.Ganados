using CreativaSL.Web.Ganados.App_Start;
using System;
using System.Collections.Generic;
using System.Configuration;
using CreativaSL.Web.Ganados.ViewModels;
using CreativaSL.Web.Ganados.Models;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    public class CatGanadoController : Controller
    {
        private TokenProcessor Token = TokenProcessor.GetInstance();
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
        // GET: Admin/CatGanado
        public ActionResult Index()
        {
            try
            {
                CatGanadoModels GanadoM = new CatGanadoModels();
                _CatGanado_Datos GanadoD = new _CatGanado_Datos();
                GanadoM.Conexion = Conexion;
                GanadoM = GanadoD.ObtenerGanado(GanadoM);
                return View(GanadoM);
            }
            catch (Exception)
            {
                CatGanadoModels GanadoM = new CatGanadoModels();
                GanadoM.ListaGanados = new List<CatGanadoModels>();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(GanadoM);
            }
        }

        // GET: Admin/CatGanado/Details/5
        public ActionResult Transferir()
        {
            try
            {
                CatGanadoModels Ganado = new CatGanadoModels();
                Ganado.Conexion = Conexion;
                _Combos_Datos Datos = new _Combos_Datos();
                Ganado.listaSucursal = Datos.ObtenerComboSucursales(Conexion);
                return View(Ganado);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET: Admin/CatGanado/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/CatGanado/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/CatGanado/Edit/5
        public ActionResult Edit(string id)
        {
            try
            {
                Token.SaveToken();
                CatGanadoModels GanadoM = new CatGanadoModels();
                _CatGanado_Datos GanadoD = new _CatGanado_Datos();
                GanadoM.Conexion = Conexion;
                GanadoM.IDGanado = id;
                GanadoM = GanadoD.ObtenerGanadoXID(GanadoM);
                GanadoM.ListaEventoEnvio = GanadoD.ObtenerComboTipoServicio(Conexion);

                CargarListas(GanadoM.IdSucursal);
                return View(GanadoM);

            }
            catch (Exception)
            {
                CatGanadoModels GanadoM = new CatGanadoModels();
                _CatGanado_Datos GanadoD = new _CatGanado_Datos();
                GanadoM = GanadoD.ObtenerGanadoXID(GanadoM);
                GanadoM.ListaGanados = new List<CatGanadoModels>();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                CargarListas(GanadoM.IdSucursal);
                return View(GanadoM);
            }
        }

        // POST: Admin/CatGanado/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, CatGanadoModels GanadoID)
        {
            _CatGanado_Datos CatGanado_Datos = new _CatGanado_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        GanadoID.Conexion = Conexion;
                        GanadoID.Usuario = User.Identity.Name;
                        GanadoID = CatGanado_Datos.C_Ganado(GanadoID);
                        if (GanadoID.Completado == true)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "Los datos se guardaron correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            GanadoID.ListaEventoEnvio = CatGanado_Datos.ObtenerComboTipoServicio(Conexion);
                            CargarListas(GanadoID.IdSucursal);
                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrio un error al intentar guardar los datos. Intente más tarde.";
                            return View(GanadoID);
                        }
                    }
                    else
                    {
                        GanadoID.Conexion = Conexion;
                        GanadoID.ListaEventoEnvio = CatGanado_Datos.ObtenerComboTipoServicio(Conexion);
                        CargarListas(GanadoID.IdSucursal);
                        return View(GanadoID);

                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }

            }
            catch
            {
                GanadoID.Conexion = Conexion;
                GanadoID.ListaEventoEnvio = CatGanado_Datos.ObtenerComboTipoServicio(Conexion);
                CargarListas(GanadoID.IdSucursal);
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error al intentar guardar los datos. Contacte a soporte técnico.";
                return View(GanadoID);
            }
        }

        // GET: Admin/CatGanado/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/CatGanado/Delete/5
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

        private void CargarListas(string id_sucursal)
        {
            _Combos_Datos oDatosCorral = new _Combos_Datos();
            List<CatCorralesModels> ListaCorralTmp = oDatosCorral.SpCSLDB_Combo_get_CatCorrales(Conexion, id_sucursal);

            var result = ListaCorralTmp.Find(x => x.Id_corral == 0);

            if (result != null)
            {
                ListaCorralTmp.Remove(result);
            }
            ViewBag.ListaCorrales = ListaCorralTmp;
        }

        [HttpPost]
        public ActionResult ObtenerCorralXIdSucursal(string IDSucursal)
        {
            try
            {
                CatCorralModels Corral = new CatCorralModels();
                _Combos_Datos Datos = new _Combos_Datos();
                Corral.Id_sucursal = IDSucursal;
                Corral.ListaCorral = Datos.ObtenerComboCorralXIDSucursal(Conexion, Corral.Id_sucursal);
                return Content(Corral.ListaCorral.ToJSON(), "application/json");
            }
            catch
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error. Por favor contacte a soporte técnico";
                return Json("");
            }
        }

        [HttpPost]
        public ActionResult DatatableGanadoActual(int Id_sucursal)
        {
            try
            {
                CatGanadoModels ganado = new CatGanadoModels();
                _CatGanado_Datos Datos = new _CatGanado_Datos();
                ganado.Conexion = Conexion;
                ganado.RespuestaAjax = new RespuestaAjax();
                ganado.RespuestaAjax.Mensaje = Datos.DatatableGanadoActual(ganado);
                ganado.RespuestaAjax.Success = true;

                return Content(ganado.RespuestaAjax.Mensaje, "application/json");

            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                CatGanadoModels ganado = new CatGanadoModels();
                ganado.RespuestaAjax.Mensaje = Mensaje;
                ganado.RespuestaAjax.Success = false;
                return Content(ganado.RespuestaAjax.ToJSON(), "application/json");
            }
        }
    }
}
