using CreativaSL.Web.Ganados.Filters;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CreativaSL.Web.Ganados.Models;
using System.Data;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    [Autorizado]
    public class CatClienteController : Controller
    {
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
        // GET: Admin/CatClientes
        public ActionResult Index()
        {
            try
            {
                CatClienteModels Cliente = new CatClienteModels();
                CatCliente_Datos ClienteD = new CatCliente_Datos();
                Cliente.Conexion = Conexion;
                Cliente = ClienteD.ObtenerClientes(Cliente);
                return View(Cliente);
            }
            catch (Exception)
            {
                CatClienteModels Cliente = new CatClienteModels();
                Cliente.ListaClientes = new List<CatClienteModels>();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Cliente);
            }
        }

        // GET: Admin/CatClientes/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/CatClientes/Create
        [HttpGet]
        public ActionResult Create()
        {
            try
            {
                CatClienteModels Clientes = new CatClienteModels();
                CatCliente_Datos ClientesDatos = new CatCliente_Datos();
                Clientes.EsPersonaFisica = true;
                Clientes.Conexion = Conexion;
                Clientes.ListaCmbSucursal = ClientesDatos.ObteneComboCatSucursal(Clientes);
                var list = new SelectList(Clientes.ListaCmbSucursal, "IDSucursal", "NombreSucursal");
                ViewData["cmbSucursal"] = list;
                Clientes.ListaRegimenCMB = ClientesDatos.ObtenerComboRegimenFiscal(Clientes);
                var list1 = new SelectList(Clientes.ListaRegimenCMB, "Clave", "Descripcion");
                ViewData["cmbRegimenFiscal"] = list1;
                return View(Clientes);
            }
            catch (Exception)
            {
                CatClienteModels Cliente = new CatClienteModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Cliente);
            }
        }

        // POST: Admin/CatClientes/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                CatClienteModels Cliente = new CatClienteModels();
                CatCliente_Datos ClienteDatos = new CatCliente_Datos();
                Cliente.Conexion = Conexion;
                Cliente.Opcion = 1;
                Cliente.IDSucursal = collection["ListaCmbSucursal"];
                Cliente.IDRegimenFiscal = collection["ListaRegimenCMB"];
                Cliente.NombreRazonSocial = collection["NombreRazonSocial"];
                Cliente.RFC = collection["RFC"];
                Cliente.EsPersonaFisica = collection["EsPersonaFisica"].StartsWith("true");
                Cliente.Usuario = User.Identity.Name;
                Cliente = ClienteDatos.AbcCatClientes(Cliente);
                if (Cliente.Completado == true)
                {
                    TempData["typemessage"] = "1";
                    TempData["message"] = "Los datos se guardarón correctamente.";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Ocurrio un error al intentar guardar los datos. Intente más tarde.";
                    return RedirectToAction("Create");
                }
            }
            catch
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error al intentar guardar los datos. Contacte a soporte técnico.";
                return RedirectToAction("Index");
            }
        }

        // GET: Admin/CatClientes/Edit/5
        [HttpGet]
        public ActionResult Edit(string id)
        {
            try
            {
                CatClienteModels Client = new CatClienteModels();
                CatCliente_Datos ClienteDatos = new CatCliente_Datos();
                Client.Conexion = Conexion;
                Client.IDCliente = id;
                Client.ListaCmbSucursal = ClienteDatos.ObteneComboCatSucursal(Client);
                var list = new SelectList(Client.ListaCmbSucursal, "IDSucursal", "NombreSucursal");
                ViewData["cmbSucursal"] = list;
                Client.ListaRegimenCMB = ClienteDatos.ObtenerComboRegimenFiscal(Client);
                var list1 = new SelectList(Client.ListaRegimenCMB, "Clave", "Descripcion");
                ViewData["cmbRegimenFiscal"] = list1;
                Client = ClienteDatos.ObtenerDetalleCatCliente(Client);
                return View(Client);
            }
            catch (Exception)
            {
                CatClienteModels Cliente = new CatClienteModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Cliente);
            }
        }

        // POST: Admin/CatClientes/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, FormCollection collection)
        {
            try
            {
                CatClienteModels Cliente = new CatClienteModels();
                CatCliente_Datos ClienteDatos = new CatCliente_Datos();
                Cliente.Conexion = Conexion;
                Cliente.Opcion = 2;
                Cliente.IDCliente = collection["IDCliente"];
                Cliente.IDSucursal = collection["ListaCmbSucursal"];
                Cliente.IDRegimenFiscal = collection["ListaRegimenCMB"];
                Cliente.NombreRazonSocial = collection["NombreRazonSocial"];
                Cliente.RFC = collection["RFC"];
                Cliente.EsPersonaFisica = collection["EsPersonaFisica"].StartsWith("true");
                Cliente.Usuario = User.Identity.Name;
                Cliente = ClienteDatos.AbcCatClientes(Cliente);
                if (Cliente.Completado == true)
                {
                    TempData["typemessage"] = "1";
                    TempData["message"] = "Los datos se guardarón correctamente.";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Ocurrio un error al intentar guardar los datos. Intente más tarde.";
                    return RedirectToAction("Edit", new { id = Cliente.IDCliente});
                }
            }
            catch
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error al intentar guardar los datos. Contacte a soporte técnico.";
                return RedirectToAction("Index");
            }
        }

        // GET: Admin/CatClientes/Delete/5
        public ActionResult Delete(string id, string id2)
        {
            try
            {
                return View();
            }
            catch (Exception)
            {

                throw;
            }
        }

        // POST: Admin/CatClientes/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, string id2, FormCollection collection)
        {
            try
            {
                CatClienteModels Cliente = new CatClienteModels();
                CatCliente_Datos ClienteDatos = new CatCliente_Datos();
                Cliente.Conexion = Conexion;
                Cliente.Usuario = User.Identity.Name;
                Cliente.IDCliente = id;
                Cliente.IDSucursal = id2;
                ClienteDatos.EliminarCliente(Cliente);
                TempData["typemessage"] = "1";
                TempData["message"] = "El registro se ha eliminado correctamente";
                return Json("");
            }
            catch
            {
                return View();
            }
        }
        // POST: Admin/CatClientes/ObtenerDatosRegimen/5
        [HttpPost]
        public ActionResult ObtenerDatosRegimen(string id)
        {
            try
            {
                CatClienteModels Cliente = new CatClienteModels();
                CatCliente_Datos ClienteDatos = new CatCliente_Datos();

                List<CFDI_RegimenFiscalModels> listaDatosRegimen = new List<CFDI_RegimenFiscalModels>();
                Cliente.Conexion = Conexion;
                Cliente.IDRegimenFiscal = id;
                
                listaDatosRegimen = ClienteDatos.ObtenerDatosRegimen(Cliente);
                return Json(listaDatosRegimen, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }
    }
}
