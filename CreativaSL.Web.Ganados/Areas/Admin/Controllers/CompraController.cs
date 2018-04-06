﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CreativaSL.Web.Ganados.Filters;
using CreativaSL.Web.Ganados.Models;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    [Autorizado]
    public class CompraController : Controller
    {
        private string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
        private CompraModels Compra;
        private _Compra_Datos CompraDatos;

        // GET: Admin/Compra
        public ActionResult Index()
        {
            try
            {
                Compra = new CompraModels();
                CompraDatos = new _Compra_Datos();
                Compra.Conexion = Conexion;
                Compra = CompraDatos.ObtenerCompraIndex(Compra);
                return View(Compra);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult Create()
        {
            try
            {
                Compra = new CompraModels();
                CompraDatos = new _Compra_Datos();
                Compra.Conexion = Conexion;

                Compra.ListaProveedores = CompraDatos.GetListadoProveedores(Compra);
                Compra.ListaSucursales = CompraDatos.GetListadoSucusales(Compra);
                Compra.ListaLugares = CompraDatos.GetListadoLugares(Compra);
                Compra.ListaChoferes = CompraDatos.GetListadoChoferes(Compra);
                Compra.ListaVehiculos = CompraDatos.GetListadoVehiculos(Compra);
                Compra.ListaJaulas = CompraDatos.GetListadoJaulas(Compra);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View(Compra);
        }
        [HttpGet]
        public ActionResult Edit(string IDCompra)
        {
            Compra = new CompraModels();
            CompraDatos = new _Compra_Datos();
            //Asigno valores para los querys
            Compra.Conexion = Conexion;
            Compra.IDCompra = IDCompra;
            //Obtengo los datos de la compra
            Compra = CompraDatos.GetCompra(Compra);
            Compra.ListaSucursales = CompraDatos.GetListadoSucusales(Compra);
            Compra.ListaFierros = CompraDatos.GetListadoFierros(Compra);
            Compra.ListaProveedores = CompraDatos.GetListadoProveedores(Compra);
            Compra.ListaLugares = CompraDatos.GetListadoLugares(Compra);
            Compra.ListaChoferes = CompraDatos.GetListadoChoferes(Compra);
            Compra.ListaVehiculos = CompraDatos.GetListadoVehiculos(Compra);
            Compra.ListaJaulas = CompraDatos.GetListadoJaulas(Compra);
            Compra.ListaRemolques = CompraDatos.GetListadoRemolques(Compra);

            Compra.GetListaProveedor();
            Compra.GetListaChoferes();
            Compra.GetListadoVehiculos();
            Compra.GetListadoJaulas();
            Compra.GetListadoRemolque();
            Compra.sRangoPeso = CompraDatos.GetRangoPeso(Compra);

            return View(Compra);
        }
        
        public ActionResult Details(int id)
        {


            return View();
        }
        public ActionResult Delete(int id)
        {
            return View();
        }
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

        #region Llenados de tablas Json
        #region Tabla Ganado
        [HttpPost]
        public ContentResult TableJsonGanado(string IDCompra)
        {
            Compra = new CompraModels();
            CompraDatos = new _Compra_Datos();
            Compra.Conexion = Conexion;
            Compra.IDCompra = IDCompra;
            Compra.Mensaje = CompraDatos.GetGanadoXGanadoDetalle(Compra);

            return Content(Compra.Mensaje, "application/json");
        }
        #endregion

        #endregion
        #region Métodos diversos
        #region Imágenes
        [HttpPost]
        public ContentResult SaveImageFierro(string IDCompra)
        {
            string jsString = "{}";
            try
            {
                foreach (string file in Request.Files)
                {
                    var fileContent = Request.Files[file];
                    if (fileContent != null && fileContent.ContentLength > 0)
                    {
                        //Obtengo el stream
                        var stream = fileContent.InputStream;
                        //Genero un array de bytes
                        byte[] fileData = null;
                        using (var binaryReader = new BinaryReader(stream))
                        {
                            fileData = binaryReader.ReadBytes(fileContent.ContentLength);
                        }
                        //Realizo la convercion a base 64
                        string Base64 = Convert.ToBase64String(fileData);
                        //Ya tengo la imagen ahora la guardo en la bd
                        HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                        FormsAuthenticationTicket Ticket = FormsAuthentication.Decrypt(authCookie.Value);

                        Compra = new CompraModels();
                        CompraDatos = new _Compra_Datos();

                        Compra.IDCompra = IDCompra;
                        Compra.Conexion = Conexion;
                        Compra.Fierro.ImgFierro = Base64;
                        Compra.Fierro.NombreFierro = Request.Files[file].FileName;
                        Compra.IDUsuario = Ticket.Name;
                        Compra = CompraDatos.SaveImageFierro(Compra);

                        jsString = "{ \"initialPreview\":[\"<img class='file-preview-image' style='width: auto; height: auto; max-width: 100 %; max-height: 100%; ' src='data: image/png; base64, " + Base64 + "' />\"],\"initialPreviewConfig\":[{\"caption\":\"" + Compra.Fierro.NombreFierro + "\",\"size\":" + fileContent.ContentLength + ",\"width\":\"50px\",\"url\":\"DeleteImageFierro\",\"key\":\"" + Compra.Fierro.IDFierro + "\"}]}";

                        return Content(jsString, "application/json");
                    }
                }
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                jsString = "{\"Mensaje de error\": \"" + ex + "\"}";
                return Content(jsString, "application/json");
            }
            return Content(jsString, "application/json");
        }
        [HttpPost]
        public ContentResult DeleteImageFierro(string key)
        {
            string jsString;
            try
            {
                HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                FormsAuthenticationTicket Ticket = FormsAuthentication.Decrypt(authCookie.Value);

                Compra = new CompraModels();
                CompraDatos = new _Compra_Datos();
                Compra.Conexion = Conexion;
                Compra.Fierro.IDFierro = key;
                Compra.IDUsuario = Ticket.Name;
                Compra = CompraDatos.DeleteImageFierro(Compra);
                jsString = "{\"Mensaje\": \"" + Compra.Mensaje + "\"}";
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                jsString = "{\"Mensaje de error\": \"" + ex + "\"}";
            }
            return Content(jsString, "application/json");
        }
        #endregion
        [HttpPost]
        public ActionResult CreateCompra(CompraModels Compra)
        {
            if (ModelState.IsValid)
            {
                //Obtengo al usuario actual
                CompraDatos = new _Compra_Datos();
                Compra.IDUsuario = User.Identity.Name;
                Compra.Conexion = Conexion;
                Compra = CompraDatos.CreateCompra(Compra);

                Compra.TipoResultado = 1;
                Compra.Mensaje = "Compra creada satisfactoriamente.";
                return RedirectToAction("Edit", "Compra", new { IDCompra = Compra.IDCompra } );
            }
            else
            {
                Compra.TipoResultado = 2;
                Compra.Mensaje = "Ha ocurrido un error al momento de crear la compra, verifique sus datos.";
                return View(Compra);
            }
           
        }
        #endregion

        #region MODALES
        #region Ganado
        [HttpPost]
        public ActionResult ModalGanado(string idGanado)
        {
            Compra = new CompraModels();
            CompraDatos = new _Compra_Datos();
            Compra.Ganado.id_Ganados = idGanado;
            Compra.Conexion = Conexion;
            Compra = CompraDatos.GetCompraGanadoXIDGanado(Compra);
            Compra.ListaEstatusGanado = CompraDatos.GetListadoEstatusGanado(Compra);
            Compra.InicializarComboGeneroGanado();
            return PartialView("ModalGanado", Compra);
        }
        #endregion
        #region Pago
        [HttpPost]
        public ActionResult ModalPago(string idDocPagar)
        {
            Compra = new CompraModels();
            CompraDatos = new _Compra_Datos();
            Compra.IDDocumentoXPagar = idDocPagar;
            Compra.Conexion = Conexion;
            Compra.ListaTipoClasificacion = CompraDatos.GetListadoTipoClasificacion(Compra);
            Compra.ListaFormasPagos = CompraDatos.GetListadoFormaPago(Compra);
            Compra.Conexion = Conexion;
            
            return PartialView("ModalPago", Compra);
        }
        #endregion
        #region Cobro
        [HttpPost]
        public ActionResult ModalCobro(string idDocCobrar)
        {
            Compra = new CompraModels();
            CompraDatos = new _Compra_Datos();
            Compra.Ganado.id_Ganados = idDocCobrar;
            Compra.Conexion = Conexion;

            return PartialView("ModalCobro", Compra);
        }
        #endregion
        #region Evento
        [HttpPost]
        public ActionResult ModalEvento(string IDCompra)
        {
            Compra = new CompraModels();
            CompraDatos = new _Compra_Datos();
            Compra.Ganado.id_Ganados = IDCompra;
            Compra.Conexion = Conexion;

            return PartialView("ModalEvento", Compra);
        }
        #endregion
        #endregion
    }
}
