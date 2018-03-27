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
        public ActionResult Details(string id)
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
                Clientes.EsPersonaFisica = false;
                Clientes.Conexion = Conexion;
                Clientes.ListaCmbSucursal = ClientesDatos.ObteneComboCatSucursal(Clientes);
                Clientes.ListaRegimenCMB = ClientesDatos.ObtenerComboRegimenFiscal(Clientes);
                return View(Clientes);
            }
            catch (Exception)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("Index");
            }
        }
                
        // POST: Admin/CatClientes/Create
        [HttpPost]
        public ActionResult Create(CatClienteModels clienteID)
        {
            CatCliente_Datos ClienteDatos = new CatCliente_Datos();
            try
            {
                if (ModelState.IsValid)
                {
                    clienteID.Conexion = Conexion;
                    clienteID.Opcion = 1;
                    clienteID.Usuario = User.Identity.Name;
                    clienteID = ClienteDatos.AbcCatClientes(clienteID);
                    if (clienteID.Completado == true)
                    {
                        TempData["typemessage"] = "1";
                        TempData["message"] = "Los datos se guardaron correctamente.";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        clienteID.ListaCmbSucursal = ClienteDatos.ObteneComboCatSucursal(clienteID);
                        clienteID.ListaRegimenCMB = ClienteDatos.ObtenerComboRegimenFiscal(clienteID);
                        TempData["typemessage"] = "2";
                        TempData["message"] = "Ocurrio un error al intentar guardar los datos. Intente más tarde.";
                        return View(clienteID);
                    }
                }
                else
                {
                    clienteID.Conexion = Conexion;
                    clienteID.ListaCmbSucursal = ClienteDatos.ObteneComboCatSucursal(clienteID);
                    clienteID.ListaRegimenCMB = ClienteDatos.ObtenerComboRegimenFiscal(clienteID);
                    return View(clienteID);
                }
            }
            catch
            {
                clienteID.Conexion = Conexion;
                clienteID.ListaCmbSucursal = ClienteDatos.ObteneComboCatSucursal(clienteID);
                clienteID.ListaRegimenCMB = ClienteDatos.ObtenerComboRegimenFiscal(clienteID);
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error al intentar guardar los datos. Contacte a soporte técnico.";
                return View(clienteID);
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
                Client = ClienteDatos.ObtenerDetalleCatCliente(Client);
                Client.ListaCmbSucursal = ClienteDatos.ObteneComboCatSucursal(Client);
                Client.ListaRegimenCMB = ClienteDatos.ObtenerComboRegimenFiscal(Client);
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
        public ActionResult Edit(string id, CatClienteModels clienteID)
        {
            CatCliente_Datos ClienteDatos = new CatCliente_Datos();
            try
            {
                if (ModelState.IsValid)
                {
                    clienteID.Conexion = Conexion;
                    clienteID.Opcion = 2;
                    clienteID.Usuario = User.Identity.Name;
                    clienteID = ClienteDatos.AbcCatClientes(clienteID);
                    if (clienteID.Completado == true)
                    {
                        TempData["typemessage"] = "1";
                        TempData["message"] = "Los datos se guardaron correctamente.";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        clienteID.ListaCmbSucursal = ClienteDatos.ObteneComboCatSucursal(clienteID);
                        clienteID.ListaRegimenCMB = ClienteDatos.ObtenerComboRegimenFiscal(clienteID);
                        TempData["typemessage"] = "2";
                        TempData["message"] = "Ocurrio un error al intentar guardar los datos. Intente más tarde.";
                        return View(clienteID);
                    }
                }
                else
                {
                    clienteID.Conexion = Conexion;
                    clienteID.ListaCmbSucursal = ClienteDatos.ObteneComboCatSucursal(clienteID);
                    clienteID.ListaRegimenCMB = ClienteDatos.ObtenerComboRegimenFiscal(clienteID);
                    return View(clienteID);
                }
            }
            catch
            {
                clienteID.Conexion = Conexion;
                clienteID.ListaCmbSucursal = ClienteDatos.ObteneComboCatSucursal(clienteID);
                clienteID.ListaRegimenCMB = ClienteDatos.ObtenerComboRegimenFiscal(clienteID);
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error al intentar guardar los datos. Contacte a soporte técnico.";
                return View(clienteID);
            }            
        }

        // GET: Admin/CatClientes/Cuentas/5
        public ActionResult Cuentas(string id)
        {
            try
            {
                CatClienteModels Cliente = new CatClienteModels();
                CatCliente_Datos ClienteD = new CatCliente_Datos();
                Cliente.Conexion = Conexion;
                
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

        // POST: Admin/CatClientes/ObtenerRegimenFiscalXBoolEsPersonaFisica
        [HttpPost]
        public ActionResult ObtenerRegimenFiscalXBoolEsPersonaFisica(bool band)
        {
            try
            {
                CatClienteModels Clientes = new CatClienteModels();
                CatCliente_Datos ClientesDatos = new CatCliente_Datos();
                Clientes.EsPersonaFisica = band;
                Clientes.Conexion = Conexion;
                List<CFDI_RegimenFiscalModels> Lista = ClientesDatos.ObtenerComboRegimenFiscal(Clientes);
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
