using System;
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
        private string SucursalDefault = "892ABBA2-325F-4613-AE2B-E4AB34AADBED";

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
                Compra.Sucursal.IDSucursal = SucursalDefault;
                Compra.ListaProveedores = CompraDatos.GetListadoProveedores(Compra);
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
        // GET: Admin/Compra/Edit/5
        public ActionResult Edit(string IDCompra)
        {
            Compra = new CompraModels();
            CompraDatos = new _Compra_Datos();
            //Asigno valores para los querys
            Compra.Conexion = Conexion;
            Compra.IDCompra = IDCompra;
            Compra.Sucursal.IDSucursal = SucursalDefault;
            //Obtengo los datos de la compra
            Compra = CompraDatos.GetCompra(Compra);
            Compra.ListaFierros = CompraDatos.GetListadoFierros(Compra);
            Compra.ListaProveedores = CompraDatos.GetListadoProveedores(Compra);
            Compra.ListaLugares = CompraDatos.GetListadoLugares(Compra);
            Compra.ListaChoferes = CompraDatos.GetListadoChoferes(Compra);
            Compra.ListaVehiculos = CompraDatos.GetListadoVehiculos(Compra);
            Compra.ListaJaulas = CompraDatos.GetListadoJaulas(Compra);

            return View(Compra);
        }

        [HttpPost]
        public ContentResult TableJsonGanado (string IDCompra)
        {
            Compra = new CompraModels();
            CompraDatos = new _Compra_Datos();
            Compra.Conexion = Conexion;
            Compra.IDCompra = IDCompra;
            Compra.Mensaje = CompraDatos.GetGanadoXGanadoDetalle(Compra);
            //Compra.Mensaje = "{\"data\": [{    \"id_ganado\": \"A004C8A0-CDF9-4FCC-B2ED-8E99CB777B0C\",    \"numArete\": \"07579621\",    \"genero\": \"MACHO \",    \"pesoInicial\": 300,    \"pesoFinal\": 297,    \"diferenciaPeso\": 3,    \"merma\": 1.5,    \"pesoPagado\": 300,    \"precioKilo\": 35.5,    \"totalPagado\": 10650  },        {    \"id_ganado\": \"A004C8A0-CDF9-4FCC-B2ED-8E99CB777B0C\",    \"numArete\": \"07579621\",    \"genero\": \"MACHO \",    \"pesoInicial\": 300,    \"pesoFinal\": 297,    \"diferenciaPeso\": 3,    \"merma\": 1.5,    \"pesoPagado\": 300,    \"precioKilo\": 35.5,    \"totalPagado\": 10650  }    ]}";
            //return Json(Compra.Mensaje, JsonRequestBehavior.AllowGet);      
            return Content(Compra.Mensaje, "application/json");
        }

        [HttpPost]
        public ActionResult SaveCompra(CompraModels Compra)
        {
            if (ModelState.IsValid)
            {
                //Obtengo al usuario actual
                HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
                CompraDatos = new _Compra_Datos();
                Compra.IDUsuario = ticket.Name;
                Compra.Sucursal.IDSucursal = SucursalDefault;
                Compra.Conexion = Conexion;
                Compra = CompraDatos.SaveCompra(Compra);
                Compra.ListaProveedores = CompraDatos.GetListadoProveedores(Compra);
                Compra.ListaLugares = CompraDatos.GetListadoLugares(Compra);
                Compra.ListaChoferes = CompraDatos.GetListadoChoferes(Compra);
                Compra.ListaVehiculos = CompraDatos.GetListadoVehiculos(Compra);
                Compra.ListaJaulas = CompraDatos.GetListadoJaulas(Compra);
                Compra.TipoResultado = 1;
                Compra.Mensaje = "Compra creada satisfactoriamente.";
            }
            else
            {
                Compra.TipoResultado = 2;
                Compra.Mensaje = "Ha ocurrido un error al momento de crear la compra, verifique sus datos.";
            }
            return View("Create", Compra);
        }
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
                Compra.Sucursal.IDSucursal = SucursalDefault;
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

        // GET: Admin/Compra/Details/5
        public ActionResult Details(int id)
        {


            return View();
        }


        // GET: Admin/Compra/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Compra/Delete/5
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

        [HttpPost]
        public JsonResult GuardarCompra(CompraModels Compra)
        {

            var errors = ModelState.Values.SelectMany(v => v.Errors);
            var rm = new ResponseModel();

            if (ModelState.IsValid)
            {
                HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);

                Compra.IDUsuario = ticket.Name;
                rm.Result = "1234-1234-1234-1234";
                rm.Message = "Datos guardados con éxito.";
                rm.Response = true;
            }
            else
            {
                rm.Message = "Verifique su formulario.";
            }
            return Json(rm);
        }
        [HttpPost]
        public PartialViewResult PartialFierros(string ID)
        {
            Compra = new CompraModels();
            Compra.Conexion = Conexion;
            Compra.IDCompra = ID;
            return PartialView(Compra);
        }



        #region MODALES
        #region Ganado
        [HttpGet]
        public ActionResult ModalGanado(string idGanado)
        {
            Compra = new CompraModels();
            CompraDatos = new _Compra_Datos();
            Compra.Ganado.id_Ganados = idGanado;
            Compra.Conexion = Conexion;
            Compra = CompraDatos.GetCompraGanadoXIDGanado(Compra);
            return PartialView("ModalGanado", Compra);
        }
        #endregion
        #region Inventario
        [HttpGet]
        public ActionResult Inventario()
        {
            Compra = new CompraModels();
            return PartialView("ModalInventario", Compra.Ganado);
        }
        #endregion
        #region Renta
        [HttpGet]
        public ActionResult Renta(string opcion)
        {
            //Aquí será la consulta a mostrar y así poder desplegar el modal con los datos correspondientes
            switch (opcion)
            {
                case "Vehiculo":
                    break;
                case "Remolque":
                    break;
                case "Jaula":
                    break;
                default:
                    break;
            }
            Compra = new CompraModels();
            return PartialView("ModalRenta", Compra.Ganado);
        }
        #endregion
        #endregion
    }
}
