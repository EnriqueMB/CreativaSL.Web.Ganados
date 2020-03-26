using CreativaSL.Web.Ganados.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using CreativaSL.Web.Ganados.Models;
using Newtonsoft.Json;
using CreativaSL.Web.Ganados.Models.Datatable;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    public class JaulasController : Controller
    {
        JaulaModels jaula;
        _Jaula_Datos datos;
        private TokenProcessor Token = TokenProcessor.GetInstance();
        private string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
        // GET: Admin/Jaulas
        public ActionResult Index()
        {
            Token.SaveToken();

            return View();
        }
       
        public ActionResult ObtenerIdsVentas(string IdJaula)
        {
            var jaulaDatos = new _Jaula_Datos();
            var listaidsventas = jaulaDatos.GetIDsVentas(IdJaula);
            var json = JsonConvert.SerializeObject(listaidsventas);
            return Content(json, "application/json");
        }
        public ActionResult ObtenerVentas()
        {
            
            var jaulaDatos = new _Jaula_Datos();
            var listaVentas = jaulaDatos.ObtenerVentas();
            var json = JsonConvert.SerializeObject(listaVentas);
            return Content(json, "application/json");
        }
        public ActionResult ObtenerVehiculo(string IDVehiculo)
        {
            var jauladatos = new _Jaula_Datos();
            var vehiculo = new CatVehiculoModels();            
            vehiculo.Conexion = Conexion;
            vehiculo.IDVehiculo = IDVehiculo;
            vehiculo = jauladatos.ObtenerDetalleCatVehiculo(vehiculo);
            var json = JsonConvert.SerializeObject(vehiculo);
            return Content(json, "application/json");
        }
        [HttpPost]
        public ActionResult DatatableIndex(DataTableAjaxPostModel dataTableAjaxPostModel)
        {
            try
            {
                JaulaModels jaula = new JaulaModels();
                _Jaula_Datos jaulaDatos = new _Jaula_Datos();
                jaula.Conexion = Conexion;
                jaula.RespuestaAjax = new RespuestaAjax();
                jaula.RespuestaAjax.Mensaje = jaulaDatos.DatatableIndex(jaula, dataTableAjaxPostModel);
                jaula.RespuestaAjax.Success = true;
                return Content(jaula.RespuestaAjax.Mensaje, "application/json");
            }
            catch (Exception ex)
            {
                var Mensaje = ex.Message.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                var jaula = new JaulaModels();
                jaula.RespuestaAjax.Mensaje = Mensaje;
                return Content(jaula.RespuestaAjax.Mensaje, "application/json");
            }
        }
        [HttpGet]
        public ActionResult Create()
        {
            jaula = new JaulaModels();
            datos = new _Jaula_Datos();
            jaula.Conexion = Conexion;
            jaula.ListaChoferes = datos.GetChoferes(jaula);
            jaula.ListaVehiculos = datos.GetVehiculos(jaula);
            return View(jaula);
        }

        [HttpPost]
        public ActionResult Create(JaulaModels jaula)
        {
            _Jaula_Datos JaDatos = new _Jaula_Datos();
            try
            {
                if(Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        var clientesSplit = jaula.IDsVentas.Split(',');
                        jaula.Opcion = 1;
                        jaula.Conexion = Conexion;
                        jaula.IDUsuario = User.Identity.Name;
                        
                        
                        jaula = JaDatos.ACJaula(clientesSplit.ToList(),jaula);
                        if (jaula.Completado==true)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "Los datos se guardaron correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            jaula.Conexion = Conexion;
                            jaula.ListaChoferes = datos.GetChoferes(jaula);
                            jaula.ListaVehiculos = datos.GetVehiculos(jaula);
                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrio un error al intentar guardar los datos. Intente más tarde.";
                            return View(jaula);
                        }
                        
                    }
                    else
                    {
                        jaula.Conexion = Conexion;
                        jaula.ListaChoferes = datos.GetChoferes(jaula);
                        jaula.ListaVehiculos = datos.GetVehiculos(jaula);                        
                        return View(jaula);
                    }

                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                jaula = new JaulaModels();
                datos = new _Jaula_Datos();
                jaula.Conexion = Conexion;
                jaula.ListaChoferes = datos.GetChoferes(jaula);
                jaula.ListaVehiculos = datos.GetVehiculos(jaula);
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error al intentar guardar los datos. Contacte a soporte técnico.";
                return View(jaula);
            }
        }
        [HttpGet]
        public ActionResult Edit(string IdJaula)
        {
            jaula = new JaulaModels();
            datos = new _Jaula_Datos();
            
            jaula = datos.GetJaulaID(IdJaula);
            jaula.Conexion = Conexion;
            jaula.ListaChoferes = datos.GetChoferes(jaula);
            jaula.ListaVehiculos = datos.GetVehiculos(jaula);            
            return View(jaula);
        }
        [HttpPost]
        public ActionResult Edit(JaulaModels model)
        {
            _Jaula_Datos JaDatos = new _Jaula_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        var ventasSplit = model.IDsVentas.Split(',');
                        model.Opcion = 2;
                        model.Conexion = Conexion;
                        model.IDUsuario = User.Identity.Name;


                        jaula = JaDatos.ACJaula(ventasSplit.ToList(), model);
                        if (model.Completado == true)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "Los datos se actualizaron correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            model.Conexion = Conexion;
                            model.ListaChoferes = datos.GetChoferes(model);
                            model.ListaVehiculos = datos.GetVehiculos(model);
                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrio un error al intentar actualizar los datos. Intente más tarde.";
                            return View(model);
                        }

                    }
                    else
                    {
                        model.Conexion = Conexion;
                        model.ListaChoferes = datos.GetChoferes(model);
                        model.ListaVehiculos = datos.GetVehiculos(model);
                        return View(model);
                    }

                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                model = new JaulaModels();
                datos = new _Jaula_Datos();
                model.Conexion = Conexion;
                model.ListaChoferes = datos.GetChoferes(model);
                model.ListaVehiculos = datos.GetVehiculos(model);
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error al intentar actualizar los datos. Contacte a soporte técnico.";
                return View(model);
            }
        }
    }
}