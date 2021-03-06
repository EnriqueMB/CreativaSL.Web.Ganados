﻿using CreativaSL.Web.Ganados.App_Start;
using System;
using System.Collections.Generic;
using System.Configuration;
using CreativaSL.Web.Ganados.Models;
using System.Web.Mvc;
using System.Data;
using CreativaSL.Web.Ganados.Filters;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    public class CatGanadoController : Controller
    {
        private TokenProcessor Token = TokenProcessor.GetInstance();
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");

        
        // GET: Admin/CatGanado
        [TransferenciaGanado]
        public ActionResult Index()
        {
            try
            {
                CatGanadoModels GanadoM = new CatGanadoModels();
                _CatGanado_Datos GanadoD = new _CatGanado_Datos();
                GanadoM.Conexion = Conexion;
                GanadoM = GanadoD.ObtenerGanado(GanadoM);
                ViewBag.PuedeTransferirGanado = (bool)System.Web.HttpContext.Current.Session["PuedeTransferirGanado"];

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

        // GET: Admin/CatGanado/Transferir/5
        [TransferenciaGanado]
        public ActionResult Transferir()
        {
            try
            {
                bool puedeTransferirGanado = (bool)System.Web.HttpContext.Current.Session["PuedeTransferirGanado"];
                if (!puedeTransferirGanado)
                {
                    return RedirectToAction("Index");
                }

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

        //POST: Admin/CatGanado/Tranferir/1
        [TransferenciaGanado]
        [HttpPost]
        public ActionResult Transferir(List<GanadoParaVender> ListaGanadosParaVender, string IDSucursal, Int64 IDCorral)
        {
            try
            {
                bool puedeTransferirGanado = (bool)System.Web.HttpContext.Current.Session["PuedeTransferirGanado"];
                if (!puedeTransferirGanado)
                {
                    return RedirectToAction("Index");
                }
                _CatGanado_Datos Datos = new _CatGanado_Datos();
                DataTable dataTable;
                dataTable = new DataTable("Items");
                dataTable.Columns.Add("Id_ganado", typeof(string));
                dataTable.Columns.Add("MermaExtra", typeof(decimal));
                dataTable.Columns.Add("Subtotal", typeof(decimal));
                foreach (var item in ListaGanadosParaVender)
                {
                    dataTable.Rows.Add(item.Id_ganado, 0, 0);
                }
                int Resultado = Datos.ActializarGanado(Conexion, User.Identity.Name, dataTable, IDSucursal, IDCorral);
                if (Resultado == 1)
                    return Json("Correcto");
                else
                    return Json("");
            }
            catch (Exception)
            {
                return Json("");
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
        public ActionResult DatatableGanadoActual()
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
