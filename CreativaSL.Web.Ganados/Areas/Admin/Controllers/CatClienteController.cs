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
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using CreativaSL.Web.Ganados.Models.System;
using CreativaSL.Web.Ganados.Models.Helpers;
using CreativaSL.Web.Ganados.Models.Datatable;


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
        #region Notas

        [HttpGet]
        public ActionResult Notas(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Verifique sus datos.";
                    return RedirectToAction("Index");
                }


                Token.SaveToken();
                var cliente = new CatClienteModels();
                cliente.IDCliente = id;
                cliente.Conexion = Conexion;

                var clienteDatos = new CatCliente_Datos();
                clienteDatos.ObtenerNota(cliente);

                return View(cliente);
            }
            catch (Exception ex)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Ha ocurrido un error al obtener la nota del cliente, intentelo de nuevo o contacte con soporte técnico.";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Notas(CatClienteModels model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.IDCliente))
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Verifique sus datos.";
                    return RedirectToAction("Index");
                }

                if (Token.IsTokenValid())
                {
                    model.Conexion = Conexion;

                    var clienteDatos = new CatCliente_Datos();
                    clienteDatos.GuardarNota(model);

                    if (model.RespuestaAjax.Success)
                    {
                        TempData["typemessage"] = "1";
                    }
                    else
                    {
                        TempData["typemessage"] = "2";
                    }

                    TempData["message"] = model.RespuestaAjax.Mensaje;
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Verifique sus datos.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Ha ocurrido un al obtener la nota del cliente, intentelo de nuevo o contacte con soporte técnico.";
                return RedirectToAction("Index");
            }
        }
        #endregion
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
                Clientes.ListaTipoCliente = ClientesDatos.ObtenerListaTipoClientes(Clientes);
                return View(Clientes);
            }
            catch (Exception)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("Index");
            }
        }
        private bool validarContacto(string nombreContacto)
        {
            if (string.IsNullOrEmpty(nombreContacto) || string.IsNullOrWhiteSpace(nombreContacto))
            {
                return false;
            }
            return true;
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
                        if (!validarContacto(clienteID.NombreResponsable))
                        {
                            clienteID.Conexion = Conexion;
                            clienteID.ListaCmbSucursal = ClienteDatos.ObteneComboCatSucursal(clienteID);
                            clienteID.ListaRegimenCMB = ClienteDatos.ObtenerComboRegimenFiscal(clienteID);
                            clienteID.ListaTipoCliente = ClienteDatos.ObtenerListaTipoClientes(clienteID);
                            ModelState.AddModelError("", "El nombre del contacto es necesario.");
                            return View(clienteID);
                        }
                        
                        clienteID.Conexion = Conexion;
                        clienteID.Opcion = 1;
                        clienteID.Usuario = User.Identity.Name;
                        clienteID = ClienteDatos.AbcCatClientes(clienteID);
                        if (clienteID.Completado == true)
                        {

                            TempData["typemessage"] = "1";
                            TempData["message"] = "Los datos se guardaron correctamente.";
                            var fotoPerfilPostedFileBase = Request.Files["FotoPerfil"] as HttpPostedFileBase;
                            if (fotoPerfilPostedFileBase != null && fotoPerfilPostedFileBase.ContentLength > 0)
                            {
                                var uploadImageToserver = new UploadFileToServerModel();
                                uploadImageToserver.FileBase = fotoPerfilPostedFileBase;
                                uploadImageToserver.BaseDir = ProjectSettings.BaseDirClienteFotoPerfil;
                                uploadImageToserver.FileName = "fp_" + clienteID.IDCliente;
                                CidFaresHelper.UploadFileToServer(uploadImageToserver);

                                if (uploadImageToserver.Success)
                                {
                                    var responseDb = ClienteDatos.ActualizarFotoPerfil(clienteID.IDCliente,
                                        User.Identity.Name, uploadImageToserver.UrlRelative, clienteID.Conexion);

                                    if (!responseDb.Success)
                                    {
                                        TempData["typemessage"] = "2";
                                        TempData["message"] = "Ha ocurrido un error al guardar la imagen de perfil al cliente, intente subir de nuevo o contacte con soporte técnico.";
                                    }
                                }
                                else
                                {
                                    TempData["typemessage"] = "2";
                                    TempData["message"] = "Ha ocurrido un error al guardar en el servidor la imagen de perfil, intente subir de nuevo o contacte con soporte técnico.";

                                }
                            }
                            Token.ResetToken();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            clienteID.ListaCmbSucursal = ClienteDatos.ObteneComboCatSucursal(clienteID);
                            clienteID.ListaRegimenCMB = ClienteDatos.ObtenerComboRegimenFiscal(clienteID);
                            clienteID.ListaTipoCliente = ClienteDatos.ObtenerListaTipoClientes(clienteID);

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
                        clienteID.ListaTipoCliente = ClienteDatos.ObtenerListaTipoClientes(clienteID);
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
                clienteID.ListaTipoCliente = ClienteDatos.ObtenerListaTipoClientes(clienteID);
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
                Client.ListaTipoCliente = ClienteDatos.ObtenerListaTipoClientes(Client);
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
                        if (!validarContacto(clienteID.NombreResponsable))
                        {
                            ModelState.AddModelError("", "El nombre del contacto es necesario.");
                            return View(clienteID);
                        }
                        clienteID.Conexion = Conexion;
                        clienteID.Opcion = 2;
                        clienteID.Usuario = User.Identity.Name;
                        clienteID = ClienteDatos.AbcCatClientes(clienteID);
                        if (clienteID.Completado == true)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "Los datos se guardaron correctamente.";
                            var fotoPerfilPostedFileBase = Request.Files["FotoPerfil"] as HttpPostedFileBase;
                            if (fotoPerfilPostedFileBase != null && fotoPerfilPostedFileBase.ContentLength > 0)
                            {
                                var uploadImageToserver = new UploadFileToServerModel();
                                uploadImageToserver.FileBase = fotoPerfilPostedFileBase;
                                uploadImageToserver.BaseDir = "/Imagenes/Cliente/FotoPerfil/";
                                uploadImageToserver.FileName = "fp_" + clienteID.IDCliente;
                                CidFaresHelper.UploadFileToServer(uploadImageToserver);

                                if (uploadImageToserver.Success)
                                {
                                    var responseDb = ClienteDatos.ActualizarFotoPerfil(clienteID.IDCliente,
                                        User.Identity.Name, uploadImageToserver.UrlRelative, clienteID.Conexion);

                                    if (!responseDb.Success)
                                    {
                                        TempData["typemessage"] = "2";
                                        TempData["message"] = "Ha ocurrido un error al guardar la imagen de perfil al cliente, intente subir de nuevo o contacte con soporte técnico.";
                                    }
                                }
                                else
                                {
                                    TempData["typemessage"] = "2";
                                    TempData["message"] = "Ha ocurrido un error al guardar en el servidor la imagen de perfil, intente subir de nuevo o contacte con soporte técnico.";

                                }
                            }
                            Token.ResetToken();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            clienteID.ListaCmbSucursal = ClienteDatos.ObteneComboCatSucursal(clienteID);
                            clienteID.ListaRegimenCMB = ClienteDatos.ObtenerComboRegimenFiscal(clienteID);
                            clienteID.ListaTipoCliente = ClienteDatos.ObtenerListaTipoClientes(clienteID);
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
                        clienteID.ListaTipoCliente = ClienteDatos.ObtenerListaTipoClientes(clienteID);
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
                clienteID.ListaTipoCliente = ClienteDatos.ObtenerListaTipoClientes(clienteID);
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
                            var fotoPerfilPostedFileBase = Request.Files["FotoCuenta"] as HttpPostedFileBase;
                            if (fotoPerfilPostedFileBase != null && fotoPerfilPostedFileBase.ContentLength > 0)
                            {
                                var uploadImageToserver = new UploadFileToServerModel();
                                uploadImageToserver.FileBase = fotoPerfilPostedFileBase;
                                uploadImageToserver.BaseDir = "/Imagenes/Cliente/FotoCuentas/";
                                uploadImageToserver.FileName = "fp_" + datosCuenta.IDDatosBancarios;
                                CidFaresHelper.UploadFileToServer(uploadImageToserver);

                                if (uploadImageToserver.Success)
                                {
                                    var responseDb = ClienteDatos.ActualizarFotoCuentas(datosCuenta.IDDatosBancarios,
                                        User.Identity.Name, uploadImageToserver.UrlRelative, datosCuenta.Conexion);
                                    if (!responseDb.Success)
                                    {
                                        TempData["typemessage"] = "2";
                                        TempData["message"] = "Ha ocurrido un error al guardar la imagen de cuentas al cliente, intente subir de nuevo o contacte con soporte técnico.";
                                    }
                                }
                                else
                                {
                                    TempData["typemessage"] = "2";
                                    TempData["message"] = "Ha ocurrido un error al guardar en el servidor la imagen de cuentas, intente subir de nuevo o contacte con soporte técnico.";

                                }
                            }
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
                            var fotoPerfilPostedFileBase = Request.Files["FotoCuenta"] as HttpPostedFileBase;
                            if (fotoPerfilPostedFileBase != null && fotoPerfilPostedFileBase.ContentLength > 0)
                            {
                                var uploadImageToserver = new UploadFileToServerModel();
                                uploadImageToserver.FileBase = fotoPerfilPostedFileBase;
                                uploadImageToserver.BaseDir = "/Imagenes/Cliente/FotoCuentas/";
                                uploadImageToserver.FileName = "fp_" + cuentaID.IDDatosBancarios;
                                CidFaresHelper.UploadFileToServer(uploadImageToserver);

                                if (uploadImageToserver.Success)
                                {
                                    var responseDb = ClienteDatos.ActualizarFotoCuentas(cuentaID.IDDatosBancarios,
                                        User.Identity.Name, uploadImageToserver.UrlRelative, datosCuenta.Conexion);

                                    if (!responseDb.Success)
                                    {
                                        TempData["typemessage"] = "2";
                                        TempData["message"] = "Ha ocurrido un error al actualizar la imagen de cuentas al cliente, intente subir de nuevo o contacte con soporte técnico.";
                                    }
                                }
                                else
                                {
                                    TempData["typemessage"] = "2";
                                    TempData["message"] = "Ha ocurrido un error al actualizar en el servidor la imagen de cuentas, intente subir de nuevo o contacte con soporte técnico.";

                                }
                            }
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
                var uploadImageToserver = new UploadFileToServerModel();
                uploadImageToserver.BaseDir = "/Imagenes/Cliente/FotoPerfil/";
                uploadImageToserver.FileName = "fp_" + Cliente.IDCliente;
                CidFaresHelper.DeleteFilesWithOutExtensionFromServer(uploadImageToserver);
                //TempData["typemessage"] = "1";
                //TempData["message"] = "El registro se ha eliminado correctamente";
                return Json("");
            }
            catch
            {
                CatClienteModels Cliente = new CatClienteModels();

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
                var uploadImageToserver = new UploadFileToServerModel();
                uploadImageToserver.BaseDir = "/Imagenes/Cliente/FotoCuentas/";
                uploadImageToserver.FileName = "fp_" + IDCuenta;
                CidFaresHelper.DeleteFilesWithOutExtensionFromServer(uploadImageToserver);
                if (Datos.Completado)
                {
                    TempData["typemessage"] = "1";
                 //   TempData["message"] = "El registro se ha eliminado correctamente";
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
               // TempData["message"] = "El registro se ha eliminado correctamente";
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

        [HttpGet]
        public ActionResult UPPCliente(string id)
        {
            try
            {
                Token.SaveToken();
                UPPClienteModels upp = new UPPClienteModels();
                CatCliente_Datos ClienteDatos = new CatCliente_Datos();
                upp.Conexion = Conexion;
                upp.id_cliente = id;
                upp = ClienteDatos.ObtenerUPPCliente(upp);
                upp.listaPaises = ClienteDatos.obtenerListaPaises(upp);
                upp.listaEstado = ClienteDatos.obtenerListaEstados(upp);
                upp.listaMunicipio = ClienteDatos.obtenerListaMunicipios(upp);

                if (string.IsNullOrEmpty(upp.Imagen))
                {
                    upp.ImagenMostrar = Auxiliar.SetDefaultImage();
                }
                else
                {
                    upp.ImagenMostrar = upp.Imagen;
                }

                return View(upp);
            }
            catch (Exception)
            {
                UPPClienteModels upp = new UPPClienteModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public ActionResult UPPCliente(UPPClienteModels uppModel)
        {
            CatCliente_Datos ClienteDatos = new CatCliente_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    var uppPostedFileBase = Request.Files["ImagenHttp"] as HttpPostedFileBase;
                    if (uppPostedFileBase != null && uppPostedFileBase.ContentLength > 0)
                    {
                        var fileName = uppModel.id_cliente;
                        var uploadImageUppPsgToserver = new UploadFileToServerModel();
                        uploadImageUppPsgToserver.FileBase = uppPostedFileBase;
                        uploadImageUppPsgToserver.BaseDir = ProjectSettings.BaseDirClienteUppPsg;
                        uploadImageUppPsgToserver.FileName = uppModel.ImagenHttp.Replace(ProjectSettings.BaseDirClienteUppPsg, string.Empty);
                        CidFaresHelper.DeleteFileromServer(uploadImageUppPsgToserver);
                        uploadImageUppPsgToserver.FileName = fileName;
                        CidFaresHelper.UploadFileToServer(uploadImageUppPsgToserver);
                        uppModel.Imagen = uploadImageUppPsgToserver.UrlRelative;
                    }

                    if (ModelState.IsValid)
                    {
                        uppModel.Conexion = Conexion;
                        //upp.id_cliente = id;
                        uppModel.Usuario = User.Identity.Name;
                        uppModel = ClienteDatos.CUPPCliente(uppModel);
                        if (uppModel.Completado)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "Los datos se guardaron correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            uppModel.Conexion = Conexion;
                            //upp.id_cliente = id;
                            uppModel.listaPaises = ClienteDatos.obtenerListaPaises(uppModel);
                            uppModel.listaEstado = ClienteDatos.obtenerListaEstados(uppModel);
                            uppModel.listaMunicipio = ClienteDatos.obtenerListaMunicipios(uppModel);
                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrio un error al intentar guardar los datos. Intente más tarde.";
                            return View(uppModel);
                        }
                    }
                    else
                    {
                        uppModel.Conexion = Conexion;
                        //upp.id_cliente = id;
                        uppModel.listaPaises = ClienteDatos.obtenerListaPaises(uppModel);
                        uppModel.listaEstado = ClienteDatos.obtenerListaEstados(uppModel);
                        uppModel.listaMunicipio = ClienteDatos.obtenerListaMunicipios(uppModel);
                        TempData["typemessage"] = "2";
                        TempData["message"] = "Ocurrio un error al intentar guardar los datos. Intente más tarde.";
                        return View(uppModel);
                    }

                }
                else
                {
                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {
                uppModel.Conexion = Conexion;
                //upp.id_cliente = id;
                uppModel.listaPaises = ClienteDatos.obtenerListaPaises(uppModel);
                uppModel.listaEstado = ClienteDatos.obtenerListaEstados(uppModel);
                uppModel.listaMunicipio = ClienteDatos.obtenerListaMunicipios(uppModel);
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error al intentar guardar los datos. Intente más tarde.";
                return View(uppModel);
            }
        }

        //AJAX OBTIENE TODOS LOS COMBOS MEDIANTE JAVASCRIPT
        [HttpPost]
        public ActionResult Estado(string id)
        {
            try
            {
                UPPClienteModels upp = new UPPClienteModels();
                CatCliente_Datos ClienteDatos = new CatCliente_Datos();
                upp.Conexion = Conexion;
                upp.id_pais = id;
                upp.listaEstado = ClienteDatos.obtenerListaEstados(upp);

                return Json(upp.listaEstado, JsonRequestBehavior.AllowGet);
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
                UPPClienteModels upp = new UPPClienteModels();
                CatCliente_Datos ClienteDatos = new CatCliente_Datos();


                upp.id_estadoCodigo = id;
                upp.id_pais = idPais;
                upp.Conexion = Conexion;
                upp.listaMunicipio = ClienteDatos.obtenerListaMunicipios(upp);
                return Json(upp.listaMunicipio, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }
        #region Documentos Extras
        [HttpPost]
        public ActionResult JsonIndexDocumentosExtras(string id, DataTableAjaxPostModel dataTableAjaxPostModel)
        {
            var ClienteDatos = new CatCliente_Datos();

            var json = ClienteDatos.DocumentosExtras_ObtenerIndex(dataTableAjaxPostModel, id);

            return Content(json, "application/json");
        }

        [HttpGet]
        public ActionResult DocumentosExtras(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Verifique sus datos.";
                return RedirectToAction("Index");
            }

            ViewBag.IdCliente = id;

            return View();
        }
        [HttpGet]
        public ActionResult CreateDocumentoExtra(string IdCliente)
        {
            if (string.IsNullOrWhiteSpace(IdCliente))
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Verifique sus datos.";
                return RedirectToAction("Index");
            }
            Token.SaveToken();
            var model = new DocumentacionExtra_CatClienteModel();
            var CatTipoDocumentacionExtraDatos = new _CatTipoDocumentacionExtra_Datos();

            var modulo = ProjectSettings.ModuloCliente;
            ViewBag.ListaCatTipoDocumentacionExtra = CatTipoDocumentacionExtraDatos.ObtenerComboCatTiposDocumentacionExtraXIdModulo(modulo);
            model.IdCliente = IdCliente;

            return View(model);
        }

        [HttpPost]
        public ActionResult CreateDocumentoExtra(DocumentacionExtra_CatClienteModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (Token.IsTokenValid())
                    {
                        var fotoPerfilPostedFileBase = Request.Files["Archivo"] as HttpPostedFileBase;
                        var uploadFileToserver = new UploadFileToServerModel();
                        uploadFileToserver.FileBase = fotoPerfilPostedFileBase;
                        uploadFileToserver.BaseDir = ProjectSettings.BaseDirClienteDocumentacionExtra;
                        uploadFileToserver.FileName = DateTime.Now.ToString("MM_dd_yyyy_hh_mm_ss");

                        if (fotoPerfilPostedFileBase != null && fotoPerfilPostedFileBase.ContentLength > 0)
                        {
                            CidFaresHelper.UploadFileToServer(uploadFileToserver);

                            if (!uploadFileToserver.Success)
                            {
                                CidFaresHelper.DeleteFilesWithOutExtensionFromServer(uploadFileToserver);
                                var CatTipoDocumentacionExtraDatos = new _CatTipoDocumentacionExtra_Datos();
                                var modulo = ProjectSettings.ModuloCliente;
                                ViewBag.ListaCatTipoDocumentacionExtra = CatTipoDocumentacionExtraDatos.ObtenerComboCatTiposDocumentacionExtraXIdModulo(modulo);

                                TempData["typemessage"] = "2";
                                TempData["message"] = "Ocurrio un error al intentar guardar la imagen de perfil, intentelo más tarde.";
                                return View(model);
                            }

                            model.Archivo = uploadFileToserver.FileName;
                            var datos = new CatCliente_Datos();
                            var respuesta = datos.GuardarDocumentoExtra(model);

                            TempData["typemessage"] = respuesta.Success ? "1" : "2";
                            TempData["message"] = respuesta.Mensaje;

                            if (!respuesta.Success)
                                CidFaresHelper.DeleteFilesWithOutExtensionFromServer(uploadFileToserver);

                            return RedirectToAction("DocumentosExtras", new {id= model.IdCliente });
                        }
                        else
                        {
                            TempData["typemessage"] = "2";
                            TempData["message"] = "Verifique sus datos.";
                            return RedirectToAction("Index");
                        }
                    }
                    else
                    {
                        TempData["typemessage"] = "2";
                        TempData["message"] = "Verifique sus datos.";
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Verifique sus datos.";
                    return RedirectToAction("Index");
                }

            }
            catch (Exception e)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Verifique sus datos.";
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public ActionResult EditDocumentoExtra(string idCliente, string idDocumentacionExtra)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(idCliente) || string.IsNullOrWhiteSpace(idDocumentacionExtra))
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Verifique sus datos.";
                    return RedirectToAction("Index");
                }

                Token.SaveToken();
                var clienteDatos = new CatCliente_Datos();
                var catTipoDocumentacionExtraDatos = new _CatTipoDocumentacionExtra_Datos();
                var modulo = ProjectSettings.ModuloCliente;
                ViewBag.ListaCatTipoDocumentacionExtra = catTipoDocumentacionExtraDatos.ObtenerComboCatTiposDocumentacionExtraXIdModulo(modulo);
                var model = clienteDatos.ObtenerDocumentacionExtra(idCliente, idDocumentacionExtra);
                return View(model);
            }
            catch (Exception)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Verifique sus datos.";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult EditDocumentoExtra(DocumentacionExtra_CatClienteModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (Token.IsTokenValid())
                    {
                        var archivoHttpPostedFileBase = Request.Files["Archivo"] as HttpPostedFileBase;
                        if (archivoHttpPostedFileBase != null && archivoHttpPostedFileBase.ContentLength > 0)
                        {
                            var uploadFileToserver = new UploadFileToServerModel();
                            uploadFileToserver.FileBase = archivoHttpPostedFileBase;
                            uploadFileToserver.BaseDir = ProjectSettings.BaseDirClienteDocumentacionExtra;

                            //borramos la imagen anterior
                            uploadFileToserver.FileName = model.Archivo.Replace(ProjectSettings.BaseDirClienteDocumentacionExtra, string.Empty);
                            CidFaresHelper.DeleteFilesWithOutExtensionFromServer(uploadFileToserver);

                            //generamos el nombre del nuevo archivo
                            uploadFileToserver.FileName = DateTime.Now.ToString("MM_dd_yyyy_hh_mm_ss");
                            CidFaresHelper.UploadFileToServer(uploadFileToserver);

                            if (!uploadFileToserver.Success)
                            {
                                CidFaresHelper.DeleteFilesWithOutExtensionFromServer(uploadFileToserver);
                                throw new Exception("No se ha podido subir el archivo al servidor.");
                            }

                            model.Archivo = uploadFileToserver.FileName;
                        }
                        var clienteDatos = new CatCliente_Datos();
                        var respuestaDb = clienteDatos.GuardarDocumentoExtra(model);

                        TempData["typemessage"] = respuestaDb.Success ? "1" : "2";
                        TempData["message"] = respuestaDb.Mensaje;

                        return RedirectToAction("DocumentosExtras", new { id = model.IdCliente });
                    }
                    else
                    {
                        TempData["typemessage"] = "2";
                        TempData["message"] = "Verifique sus datos.";
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Verifique sus datos.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Verifique sus datos.";
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public ActionResult EliminarDocumentoExtra(string idCliente, string idDocumentacionExtra, string urlArchivo)
        {
            urlArchivo = urlArchivo.Replace(ProjectSettings.BaseDirClienteDocumentacionExtra, string.Empty);
            if (string.IsNullOrWhiteSpace(idCliente) || string.IsNullOrWhiteSpace(idDocumentacionExtra) || string.IsNullOrEmpty(urlArchivo))
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Verifique sus datos.";
                return RedirectToAction("Index");
            }
            var clienteDatos = new CatCliente_Datos();
            var responseDb = clienteDatos.EliminarDocumentoExtra(idCliente, idDocumentacionExtra, urlArchivo);
            if (responseDb.Success)
            {
                var uploadFileToserver = new UploadFileToServerModel();
                uploadFileToserver.BaseDir = ProjectSettings.BaseDirClienteDocumentacionExtra;
                uploadFileToserver.FileName = urlArchivo;
                CidFaresHelper.DeleteFilesWithOutExtensionFromServer(uploadFileToserver);
            }
            TempData["typemessage"] = "1";
            TempData["message"] = responseDb.Mensaje;
            return Json("");
        }
        #endregion
        #region Imagenes
        public ActionResult Imagenes(string id)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(id))
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Verifique sus datos.";
                    return RedirectToAction("Index");
                }
                var datosCliente = new CatCliente_Datos();
                var modelCuenta = new CuentaBancariaClienteModels();

                modelCuenta.Conexion = Conexion;
                modelCuenta.IDCliente = id;
                var model = datosCliente.ObtenerCliente(id);
                ViewBag.ListaCuentasBancarias = datosCliente.ObtenerCuentasBancarias(modelCuenta);
                return View(model);
            }
            catch (Exception)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Verifique sus datos.";
                return RedirectToAction("Index");
            }
        }
        #endregion
    }
}
