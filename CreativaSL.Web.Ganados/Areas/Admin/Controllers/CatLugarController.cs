using CreativaSL.Web.Ganados.Filters;
using CreativaSL.Web.Ganados.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    [Autorizado]
    public class CatLugarController : Controller
    {
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
        // GET: Admin/CatLugar
        public ActionResult Index()
        {
            try
            {
                CatLugarModels Lugar = new CatLugarModels();
                _CatLugar_Datos LugarDatos = new _CatLugar_Datos();
                Lugar.conexion = Conexion;
                Lugar.listaLugares = LugarDatos.obtenerCatLugar(Lugar);
                return View(Lugar);
            }
            catch (Exception ex)
            {
                CatLugarModels Lugar = new CatLugarModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Lugar);
            }
        }

        // GET: Admin/CatLugar/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/CatLugar/Create
        public ActionResult Create()
        {
            try
            {

                CatLugarModels Lugar = new CatLugarModels();
                _CatLugar_Datos LugarDatos = new _CatLugar_Datos();
                Lugar.conexion = Conexion;
                Lugar.lat = "16.99873295324791";
                Lugar.lng = "-92.9919061984375";
                Lugar.listaPaises = LugarDatos.obtenerListaPaises(Lugar);
                var List= new SelectList(Lugar.listaPaises, "id_pais", "descripcion");
                ViewData["cmbPaises"] = List;

                Lugar.listaEstado = LugarDatos.obtenerListaEstados(Lugar);
                var Lista = new SelectList(Lugar.listaEstado, "codigoEstado", "descripcion");
                ViewData["cmbEstados"] = Lista;

                Lugar.listaMunicipio = LugarDatos.obtenerListaMunicipios(Lugar);
                var Listam = new SelectList(Lugar.listaMunicipio, "id_municipio", "descripcion");
                ViewData["cmbMunicipios"] = Listam;
                return View(Lugar);
            }
            catch (Exception ex)
            {
                CatLugarModels Lugar = new CatLugarModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Lugar);
            }
        }

        // POST: Admin/CatLugar/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {

                CatLugarModels Lugar = new CatLugarModels();
                _CatLugar_Datos LugarDatos = new _CatLugar_Datos();
                Lugar.conexion = Conexion;
                Lugar.opcion = 1;
                Lugar.latitud = Convert.ToSingle(collection["lat"]);
                Lugar.longitud = Convert.ToSingle(collection["lng"]);
                Lugar.descripcion = collection["descripcion"];
                Lugar.ejido = collection["ejido"];
                Lugar.id_pais = collection["listaPaises"];
                Lugar.id_estadoCodigo = collection["listaEstado"];
                Lugar.id_municipio = Convert.ToInt32(collection["listaMunicipio"]);
                Lugar.user = User.Identity.Name;
                Lugar = LugarDatos.AbcCatLugar(Lugar);
                if (Lugar.Completado == true)
                {
                    TempData["typemessage"] = "1";
                    TempData["message"] = "El registro se guardo correctamente.";

                }
                else
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Ocurrió un error al guardar el registro.";
                }
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                CatLugarModels Lugar = new CatLugarModels();
               
                TempData["typemessage"] = "2";
                TempData["message"] = "No se pudo guardar los datos. Por favor contacte a soporte técnico";
                return View(Lugar);
            }
        }

        // GET: Admin/CatLugar/Edit/5
        public ActionResult Edit(string id)
        {
            try
            {

                CatLugarModels Lugar = new CatLugarModels();
                _CatLugar_Datos LugarDatos = new _CatLugar_Datos();
                Lugar.conexion = Conexion;
                Lugar.id_lugar = id;
                Lugar = LugarDatos.ObtenerDetalleCatLugar(Lugar);
               
                Lugar.listaPaises = LugarDatos.obtenerListaPaises(Lugar);
                var List = new SelectList(Lugar.listaPaises, "id_pais", "descripcion");
                ViewData["cmbPaises"] = List;

                Lugar.listaEstado = LugarDatos.obtenerListaEstados(Lugar);
                var Lista = new SelectList(Lugar.listaEstado, "codigoEstado", "descripcion");
                ViewData["cmbEstados"] = Lista;

                Lugar.listaMunicipio = LugarDatos.obtenerListaMunicipios(Lugar);
                var Listam = new SelectList(Lugar.listaMunicipio, "id_municipio", "descripcion");
                ViewData["cmbMunicipios"] = Listam;
                return View(Lugar);
            }
            catch (Exception ex)
            {
                CatLugarModels Lugar = new CatLugarModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Lugar);
            }
        }

        // POST: Admin/CatLugar/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, FormCollection collection)
        {
            try
            {

                CatLugarModels Lugar = new CatLugarModels();
                _CatLugar_Datos LugarDatos = new _CatLugar_Datos();
                Lugar.conexion = Conexion;
                Lugar.opcion = 2;
                Lugar.id_lugar = id;
                Lugar.latitud = Convert.ToSingle(collection["lat"]);
                Lugar.longitud = Convert.ToSingle(collection["lng"]);
                Lugar.descripcion = collection["descripcion"];
                Lugar.ejido = collection["ejido"];
                Lugar.id_pais = collection["listaPaises"];
                Lugar.id_estadoCodigo = collection["listaEstado"];
                Lugar.id_municipio = Convert.ToInt32(collection["listaMunicipio"]);
                Lugar.user = User.Identity.Name;
                Lugar = LugarDatos.AbcCatLugar(Lugar);
                if (Lugar.Completado == true)
                {
                    TempData["typemessage"] = "1";
                    TempData["message"] = "El registro se editó correctamente.";

                }
                else
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Ocurrió un error al guardar el registro.";
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                CatLugarModels Lugar = new CatLugarModels();

                TempData["typemessage"] = "2";
                TempData["message"] = "No se pudo guardar los datos. Por favor contacte a soporte técnico";
                return View(Lugar);
            }
        }

        // GET: Admin/CatLugar/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/CatLugar/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                CatLugarModels Lugar = new CatLugarModels();
                _CatLugar_Datos LugarDatos = new _CatLugar_Datos();
                Lugar.conexion = Conexion;
                Lugar.id_lugar = id;
                Lugar.user = User.Identity.Name;
                Lugar = LugarDatos.EliminarLugar(Lugar);
                TempData["typemessage"] = "1";
                TempData["message"] = "El registro se ha eliminado correctamente";
                return Json("");
                // TODO: Add delete logic here


            }
            catch
            {
                CatChoferModels Chofer = new CatChoferModels();
               
                TempData["typemessage"] = "2";
                TempData["message"] = "No se pudo borrar los datos. Por favor contacte a soporte técnico";
                return Json("");

            }
        }
        //AJAX OBTIENE TODOS LOS COMBOS MEDIANTE JAVASCRIPT
        [HttpPost]
        public ActionResult Estado(string id)
        {
            try
            {
                CatLugarModels Lugar = new CatLugarModels();
                _CatLugar_Datos LugarDatos = new _CatLugar_Datos();
                Lugar.conexion = Conexion;
                Lugar.id_pais = id;
                Lugar.listaEstado = LugarDatos.obtenerListaEstados(Lugar);


                return Json(Lugar.listaEstado, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
       
        public ActionResult Municipio(string idPais, string id)
        {
            try
            {
                CatLugarModels Lugar = new CatLugarModels();
                _CatLugar_Datos LugarDatos = new _CatLugar_Datos();

              
                Lugar.id_estadoCodigo = id;
                Lugar.id_pais = idPais;
                Lugar.conexion = Conexion;
                Lugar.listaMunicipio = LugarDatos.obtenerListaMunicipios(Lugar);
                return Json(Lugar.listaMunicipio, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }
    }
}
