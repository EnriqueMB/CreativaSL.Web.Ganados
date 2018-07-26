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
using CreativaSL.Web.Ganados.App_Start;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    [Autorizado]
    public class CatClienteController : Controller
    {
        private TokenProcessor Token = TokenProcessor.GetInstance();
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
                Token.SaveToken();
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
                if (Token.IsTokenValid())
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
                            Token.ResetToken();
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
                else
                {
                    return RedirectToAction("Index");
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
                Token.SaveToken();
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
                if (Token.IsTokenValid())
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
                            Token.ResetToken();
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
                else
                {
                    return RedirectToAction("Index");
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
        [HttpGet]
        public ActionResult Cuentas(string id)
        {
            try
            {
                IEnumerable<CuentaBancariaModels> CuentasBanc = Enumerable.Empty<CuentaBancariaModels>();
                CuentaBancariaModels Cuenta = new CuentaBancariaModels { Conexion = Conexion, Cliente = new CatClienteModels { IDCliente = id } };
                CatCliente_Datos ClienteD = new CatCliente_Datos();
                CuentasBanc = ClienteD.ObtenerCuentasXIDCliente(Cuenta);
                ViewBag.ID = id;
                return View(CuentasBanc);
            }
            catch (Exception)
            {
                IEnumerable<CuentaBancariaModels> CuentasBanc = Enumerable.Empty<CuentaBancariaModels>();
                ViewBag.ID = id;
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(CuentasBanc);
            }
        }

        // GET: Admin/CatClientes/AgregarCuenta/5
        [HttpGet]
        public ActionResult AgregarCuenta(string id)
        {
            try
            {
                Token.SaveToken();
                CuentaBancariaViewModels Model = new CuentaBancariaViewModels();
                CatCliente_Datos ClientesDatos = new CatCliente_Datos();
                Model.IDCliente = id;
                Model.ListaBancos = ClientesDatos.ObtenerComboCatBancos(Conexion);
                return View(Model);
            }
            catch (Exception)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("Cuentas", new { id = id });
            }
        }

        // POST: Admin/CatClientes/AgregarCuenta
        [HttpPost]
        public ActionResult AgregarCuenta(CuentaBancariaViewModels cuentaID)
        {
            CatCliente_Datos ClienteDatos = new CatCliente_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        CuentaBancariaModels datosCuenta = new CuentaBancariaModels
                        {
                            Banco = new CatBancoModels { IDBanco = cuentaID.IDBanco },
                            Cliente = new CatClienteModels { IDCliente = cuentaID.IDCliente },
                            Titular = cuentaID.Titular,
                            NumCuenta = cuentaID.NumCuenta,
                            NumTarjeta = cuentaID.NumTarjeta,
                            Clabe = cuentaID.Clabe,
                            Conexion = Conexion,
                            NuevoRegistro = true,
                            Usuario = User.Identity.Name
                        };
                        ClienteDatos.ACDatosBancariosCliente(datosCuenta);
                        if (datosCuenta.Completado == true)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "Los datos se guardaron correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("Cuentas", new { id = cuentaID.IDCliente });
                        }
                        else
                        {
                            cuentaID.ListaBancos = ClienteDatos.ObtenerComboCatBancos(Conexion);
                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrió un error al intentar guardar los datos. Intente más tarde.";
                            return View(cuentaID);
                        }
                    }
                    else
                    {
                        cuentaID.ListaBancos = ClienteDatos.ObtenerComboCatBancos(Conexion);
                        return View(cuentaID);
                    }
                }
                else
                {
                    return RedirectToAction("Cuentas", new { id = cuentaID.IDCliente });
                }
            }
            catch
            {
                cuentaID.ListaBancos = ClienteDatos.ObtenerComboCatBancos(Conexion);
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error al intentar guardar los datos. Contacte a soporte técnico.";
                return View(cuentaID);
            }
        }

        // GET: Admin/CatClientes/EditarCuenta/5
        [HttpGet]
        public ActionResult EditarCuenta(string id, string idC)
        {
            CatCliente_Datos ClienteDatos = new CatCliente_Datos();
            try
            {
                Token.SaveToken();
                CuentaBancariaModels Datos = new CuentaBancariaModels { Conexion = Conexion, IDDatosBancarios = id, Cliente = new CatClienteModels { IDCliente = idC } };
                ClienteDatos.ObtenerDetalleDatosBancariosCliente(Datos);
                CuentaBancariaViewModels ViewCuenta = Datos.GetViewCB();
                ViewCuenta.IDCliente = idC;
                ViewCuenta.IDDatosBancarios = id;
                ViewCuenta.ListaBancos = ClienteDatos.ObtenerComboCatBancos(Conexion);
                return View(ViewCuenta);
            }
            catch (Exception)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("Cuentas", new { id = idC });
            }
        }

        // POST: Admin/CatClientes/EditarCuenta
        [HttpPost]
        public ActionResult EditarCuenta(CuentaBancariaViewModels cuentaID)
        {
            CatCliente_Datos ClienteDatos = new CatCliente_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        CuentaBancariaModels datosCuenta = new CuentaBancariaModels
                        {
                            IDDatosBancarios = cuentaID.IDDatosBancarios,
                            Banco = new CatBancoModels { IDBanco = cuentaID.IDBanco },
                            Cliente = new CatClienteModels { IDCliente = cuentaID.IDCliente },
                            Titular = cuentaID.Titular,
                            NumCuenta = cuentaID.NumCuenta,
                            NumTarjeta = cuentaID.NumTarjeta,
                            Clabe = cuentaID.Clabe,
                            Conexion = Conexion,
                            NuevoRegistro = false,
                            Usuario = User.Identity.Name
                        };
                        ClienteDatos.ACDatosBancariosCliente(datosCuenta);
                        if (datosCuenta.Completado == true)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "Los datos se guardaron correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("Cuentas", new { id = cuentaID.IDCliente });
                        }
                        else
                        {
                            cuentaID.ListaBancos = ClienteDatos.ObtenerComboCatBancos(Conexion);
                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrió un error al intentar guardar los datos. Intente más tarde.";
                            return View(cuentaID);
                        }
                    }
                    else
                    {
                        cuentaID.ListaBancos = ClienteDatos.ObtenerComboCatBancos(Conexion);
                        return View(cuentaID);
                    }
                }
                else
                {
                    return RedirectToAction("Cuentas", new { id = cuentaID.IDCliente });
                }
            }
            catch
            {
                cuentaID.ListaBancos = ClienteDatos.ObtenerComboCatBancos(Conexion);
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error al intentar guardar los datos. Contacte a soporte técnico.";
                return View(cuentaID);
            }
        }


        // POST: Admin/CatClientes/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, string id2)
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
                return Json("");
            }
        }


        // POST: Admin/CatClientes/Delete/5
        [HttpPost]
        public ActionResult DeleteDatosBancarios(string IDCuenta, string IDCliente)
        {
            try
            {
                CuentaBancariaModels Datos = new CuentaBancariaModels
                { Cliente = new CatClienteModels { IDCliente = IDCliente },
                    IDDatosBancarios = IDCuenta,
                    Conexion = Conexion,
                    Usuario = User.Identity.Name };
                CatCliente_Datos ClienteDatos = new CatCliente_Datos();
                ClienteDatos.EliminarDatosBancarios(Datos);
                if (Datos.Completado)
                {
                    TempData["typemessage"] = "1";
                    TempData["message"] = "El registro se ha eliminado correctamente";
                    return Json("");
                }
                else
                { return Json(""); }
            }
            catch
            {
                return Json("");
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

        //ASIGNAR LUGARES AL CLIENTE
       // [HttpPost]
        public ActionResult Lugares(string id, string id2)
        {
            try
            {
                ClienteLugarModels ClienteLugar = new ClienteLugarModels();
                CatCliente_Datos ClienteD = new CatCliente_Datos();
                ClienteLugar.IDCliente = id;
                ClienteLugar.IDSucursal = id2;
                ClienteLugar.Conexion = Conexion;
                ClienteLugar = ClienteD.ObtenerLugares(ClienteLugar);
                return View(ClienteLugar);
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
        [HttpGet]
        public ActionResult ClienteLugar(string id, string id2)
        {
            CatCliente_Datos ClienteDatos = new CatCliente_Datos();
            try
            {
                Token.SaveToken();
                ClienteLugarModels ClienteLugar = new ClienteLugarModels();
                ClienteLugar.IDCliente = id;
                ClienteLugar.Conexion = Conexion;
                ClienteLugar.IDSucursal = id2;
                ClienteLugar.listaLugares = ClienteDatos.obtenerLugaresClientes(ClienteLugar);
                return View(ClienteLugar);
            }
            catch (Exception)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("Lugares", new { id = id, id2 = id2 });
            }
        }
        [HttpPost]
        public ActionResult ClienteLugar(ClienteLugarModels ClienteLugar)
        {
            CatCliente_Datos ClienteDatos = new CatCliente_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        ClienteLugar.Conexion = Conexion;
                        ClienteLugar.Opcion = 1;
                        //ClienteLugar.listaLugares = ClienteDatos.obtenerLugaresClientes(ClienteLugar);
                        ClienteDatos.ACLugaresCliente(ClienteLugar);
                        if (ClienteLugar.Completado == true)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "Los datos se guardaron correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("Lugares", new { id = ClienteLugar.IDCliente, id2 = ClienteLugar.IDSucursal });
                        }
                        else
                        {
                            ClienteLugar.Conexion = Conexion;
                            ClienteLugar.listaLugares = ClienteDatos.obtenerLugaresClientes(ClienteLugar);
                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrió un error al intentar guardar los datos. Intente más tarde.";
                            return RedirectToAction("ClienteLugar", new { id = ClienteLugar.IDCliente, id2 = ClienteLugar.IDSucursal });
                        }
                    }
                    else
                    {
                        ClienteLugar.Conexion = Conexion;
                        ClienteLugar.listaLugares = ClienteDatos.obtenerLugaresClientes(ClienteLugar);
                        return View(ClienteLugar);
                    }
                }
                else
                {
                    return RedirectToAction("Lugares", new { id = ClienteLugar.IDCliente, id2 = ClienteLugar.IDSucursal });
                }
            }
            catch (Exception)
            {
                ClienteLugar.Conexion = Conexion;
                ClienteLugar.listaLugares = ClienteDatos.obtenerLugaresClientes(ClienteLugar);
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("Lugares", new { id = ClienteLugar.IDCliente, id2 = ClienteLugar.IDSucursal });
            }
        }
        [HttpPost]
        public ActionResult DeleteLugar(string id)
        {
            try
            {
                ClienteLugarModels Cliente = new ClienteLugarModels();
                CatCliente_Datos ClienteDatos = new CatCliente_Datos();
                Cliente.Conexion = Conexion;
                Cliente.Usuario = User.Identity.Name;
                Cliente.IDClienteLugar = id;
                //Cliente.IDSucursal = id2;

                ClienteDatos.EliminarLugarCliente(Cliente);
                TempData["typemessage"] = "1";
                TempData["message"] = "El registro se ha eliminado correctamente";
                return Json("");
            }
            catch
            {
                return View();
            }
        }
        [HttpGet]
        public ActionResult ContactosCliente(string id)
        {
            try
            {
                CatContactosModels clienteContactos = new CatContactosModels();
                CatCliente_Datos ClienteD = new CatCliente_Datos();
                clienteContactos.IDCliente = id;
                clienteContactos.Conexion = Conexion;
                clienteContactos = ClienteD.ObtenerContactosCliente(clienteContactos);
                return View(clienteContactos);
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

        [HttpGet]
        public ActionResult CreateContacto(string id)
        {
            try
            {
                Token.SaveToken();
                CatContactosModels contacto = new CatContactosModels();
                CatCliente_Datos ClientesDatos = new CatCliente_Datos();
                contacto.IDCliente = id;
                contacto.Conexion = Conexion;
                return View(contacto);
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
        public ActionResult CreateContacto(CatContactosModels contactoID)
        {
            CatCliente_Datos ClienteDatos = new CatCliente_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        contactoID.Conexion = Conexion;
                        contactoID.Opcion = 1;
                        contactoID.Usuario = User.Identity.Name;
                        contactoID = ClienteDatos.AcContactoCliente(contactoID);
                        if (contactoID.Completado == true)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "Los datos se guardaron correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("ContactosCliente", new { id=contactoID.IDCliente});
                        }
                        else
                        {
                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrio un error al intentar guardar los datos. Intente más tarde.";
                            return View(contactoID);
                        }
                    }
                    else
                    {
                        return View(contactoID);
                    }
                }
                else
                {
                    return RedirectToAction("ContactosCliente", new { id = contactoID.IDCliente });
                }
            }
            catch
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error al intentar guardar los datos. Contacte a soporte técnico.";
                return View(contactoID);
            }
        }

        [HttpGet]
        public ActionResult EditContacto(string id)
        {
            try
            {
                Token.SaveToken();
                CatContactosModels contacto = new CatContactosModels();
                CatCliente_Datos ClienteDatos = new CatCliente_Datos();
                contacto.Conexion = Conexion;
                contacto.IDContacto = id;
                contacto = ClienteDatos.ObtenerDetalleContactoCliente(contacto);
                return View(contacto);
            }
            catch (Exception)
            {
                CatContactosModels contacto = new CatContactosModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(contacto);
            }
        }

        // POST: Admin/CatClientes/Edit/5
        [HttpPost]
        public ActionResult EditContacto(CatContactosModels contactoID)
        {
            CatCliente_Datos ClienteDatos = new CatCliente_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        contactoID.Conexion = Conexion;
                        contactoID.Opcion = 2;
                        contactoID.Usuario = User.Identity.Name;
                        contactoID = ClienteDatos.AcContactoCliente(contactoID);
                        if (contactoID.Completado == true)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "Los datos se guardaron correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("ContactosCliente", new { id = contactoID.IDCliente });
                        }
                        else
                        {
                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrio un error al intentar guardar los datos. Intente más tarde.";
                            return View(contactoID);
                        }
                    }
                    else
                    {
                        return View(contactoID);
                    }
                }
                else
                {
                    return RedirectToAction("ContactosCliente");
                }
            }
            catch
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error al intentar guardar los datos. Contacte a soporte técnico.";
                return View(contactoID);
            }
        }
        [HttpPost]
        public ActionResult DeleteContacto(string id)
        {
            try
            {
                CatContactosModels Contacto = new CatContactosModels();
                CatCliente_Datos ClienteDatos = new CatCliente_Datos();
                Contacto.Conexion = Conexion;
                Contacto.Usuario = User.Identity.Name;
                Contacto.IDContacto = id;
                ClienteDatos.EliminarContactoCliente(Contacto);
                TempData["typemessage"] = "1";
                TempData["message"] = "El registro se ha eliminado correctamente";
                return Json("");
            }
            catch
            {
                return View();
            }
        }
    }
}
