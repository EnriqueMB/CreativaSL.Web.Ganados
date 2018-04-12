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

        // GET: Admin/Compra
        public ActionResult Index()
        {
            try
            {
                Compra = new CompraModels();
                return View(Compra);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public ActionResult CreatePart3(string IDCompra)
        {
            if (string.IsNullOrEmpty(IDCompra))
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista.";
                return RedirectToAction("Index");
            }
            else
            {
                try
                {
                    Compra = new CompraModels();
                    CompraDatos = new _Compra_Datos();
                    Compra.IDCompra = IDCompra;
                    Compra.Conexion = Conexion;
                    //Obtengo los datos de la compra
                    Compra = CompraDatos.GetCompra(Compra);
                    //Obteno los listados
                    Compra.ListaSucursales = CompraDatos.GetListadoSucursales(Compra);
                    Compra.ListaProveedores = CompraDatos.GetListaProveedores(Compra);
                    Compra.ListaEmpresas = CompraDatos.GetListadoEmpresas(Compra);
                    Compra.ListaLugares = CompraDatos.GetListadoLugares(Compra);
                    Compra.ListaChoferes = CompraDatos.GetChoferesXIDEmpresa(Compra);
                    Compra.ListaVehiculos = CompraDatos.GetVehiculosXIDEmpresa(Compra);
                    Compra.ListaRemolques = CompraDatos.GetRemolquesXIDEmpresa(Compra);
                    Compra.ListaJaulas = CompraDatos.GetJaulasXIDEmpresa(Compra);
                    Compra.ListaFierros = CompraDatos.GetListadoFierrosXIDCompra(Compra);

                    return View(Compra);
                }
                catch (Exception ex)
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "No se puede cargar la vista, error: " + ex.ToString();
                    return View(Compra);
                }
            }
        }

        /********************************************************************/
        [HttpGet]
        public ActionResult ProgramarCompra(string IDCompra)
        {
            try
            {
                Compra = new CompraModels();
                CompraDatos = new _Compra_Datos();
                Compra.Conexion = Conexion;
                Compra.ListaSucursales = CompraDatos.GetListadoSucursales(Compra);
                Compra.ListaProveedores = CompraDatos.GetListaProveedores(Compra);
                Compra.ListaLugares = CompraDatos.GetListadoLugares(Compra);

                if (!string.IsNullOrEmpty(IDCompra))
                {
                    Compra.IDCompra = IDCompra;
                    Compra = CompraDatos.GetCompra(Compra);
                }
            }
            catch (Exception ex)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista, error: " + ex.ToString();
            }
            return View(Compra);
        }
        [HttpPost]
        public ActionResult ProgramarCompra(CompraModels Compra)
        {
            try
            {
                CompraDatos = new _Compra_Datos();
                if (ModelState.IsValid)
                {
                    Compra.Conexion = Conexion;
                    Compra.Usuario = User.Identity.Name;
                    Compra = CompraDatos.Compras_ac_Proveedor(Compra);

                    //Si abc fue completado correctamente
                    if (Compra.Completado == true)
                    {
                        TempData["typemessage"] = "1";
                        TempData["message"] = "El registro se guardo correctamente.";
                        Compra.IDCompra = Compra.Mensaje;
                        return RedirectToAction("Index", "Compra", new { Compra.IDCompra });
                    }
                    else
                    {
                        Compra.ListaSucursales = CompraDatos.GetListadoSucursales(Compra);
                        Compra.ListaProveedores = CompraDatos.GetListaProveedores(Compra);
                        Compra.ListaLugares = CompraDatos.GetListadoLugares(Compra);
                        TempData["typemessage"] = "2";
                        TempData["message"] = "Ocurrió un error al guardar el registro. Error: " + Compra.Mensaje;
                        return View(Compra);
                    }
                }
                else
                {
                    Compra.ListaSucursales = CompraDatos.GetListadoSucursales(Compra);
                    Compra.ListaProveedores = CompraDatos.GetListaProveedores(Compra);
                    Compra.ListaLugares = CompraDatos.GetListadoLugares(Compra);
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Verifique su formulario.";
                    return View(Compra);
                }
            }
            catch (Exception ex)
            {
                Compra.ListaSucursales = CompraDatos.GetListadoSucursales(Compra);
                Compra.ListaProveedores = CompraDatos.GetListaProveedores(Compra);
                Compra.ListaLugares = CompraDatos.GetListadoLugares(Compra);
                TempData["typemessage"] = "2";
                TempData["message"] = "No se pudo guardar los datos. Por favor contacte a soporte técnico, error: " + ex.ToString();
                return View(Compra);
            }
        }
        /********************************************************************/
        [HttpGet]
        public ActionResult EmbarqueCompra(string IDCompra)
        {
            if (string.IsNullOrEmpty(IDCompra))
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista.";
                return RedirectToAction("Index");
            }
            else
            {
                try
                {
                    Compra = new CompraModels();
                    CompraDatos = new _Compra_Datos();
                    Compra.IDCompra = IDCompra;
                    Compra.Conexion = Conexion;
                    //Obtengo los datos de la compra
                    Compra = CompraDatos.GetCompra(Compra);
                    //Obteno los listados
                    Compra.ListaSucursales = CompraDatos.GetListadoSucursales(Compra);
                    Compra.ListaProveedores = CompraDatos.GetListaProveedores(Compra);
                    Compra.ListaEmpresas = CompraDatos.GetListadoEmpresas(Compra);
                    Compra.ListaLugares = CompraDatos.GetListadoLugares(Compra);
                    Compra.ListaChoferes = CompraDatos.GetChoferesXIDEmpresa(Compra);
                    Compra.ListaVehiculos = CompraDatos.GetVehiculosXIDEmpresa(Compra);
                    Compra.ListaRemolques = CompraDatos.GetRemolquesXIDEmpresa(Compra);
                    Compra.ListaJaulas = CompraDatos.GetJaulasXIDEmpresa(Compra);
                    Compra.ListaFierros = CompraDatos.GetListadoFierrosXIDCompra(Compra);

                    return View(Compra);
                }
                catch (Exception ex)
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "No se puede cargar la vista, error: " + ex.ToString();
                    return View(Compra);
                }
            }
        }
        /********************************************************************/
        [HttpGet]
        public ActionResult RecepcionCompra(string IDCompra)
        {
            if (string.IsNullOrEmpty(IDCompra))
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista.";
                return RedirectToAction("Index");
            }
            else
            {
                try
                {
                    Compra = new CompraModels();
                    CompraDatos = new _Compra_Datos();
                    Compra.IDCompra = IDCompra;
                    Compra.Conexion = Conexion;
                    //Obtengo los datos de la compra
                    Compra = CompraDatos.GetCompra(Compra);
                    //Obtengo el listado de precios
                    Compra.ListadoPrecioRangoPeso = Auxiliar.SqlReaderToJson(CompraDatos.GetListadoPrecioRangoPeso(Compra));

                    return View(Compra);
                }
                catch (Exception ex)
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "No se puede cargar la vista, error: " + ex.ToString();
                    return View(Compra);
                }
            }
        }
        /********************************************************************/
        //Funciones Combo
        #region funciones combo
        [HttpPost]
        public ActionResult GetChoferesXIDEmpresa(string IDEmpresa)
        {
            try
            {
                Compra = new CompraModels();
                CompraDatos = new _Compra_Datos();
                Compra.Conexion = Conexion;
                Compra.IDEmpresa = IDEmpresa;
                Compra.Usuario = User.Identity.Name;
                Compra.ListaChoferes = CompraDatos.GetChoferesXIDEmpresa(Compra);

                return Content(Compra.ListaChoferes.ToJSON(), "application/json");
            }
            catch
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error. Por favor contacte a soporte técnico";
                return Json("");
            }
        }
        [HttpPost]
        public ActionResult GetVehiculosXIDEmpresa(string IDEmpresa)
        {
            try
            {
                Compra = new CompraModels();
                CompraDatos = new _Compra_Datos();
                Compra.Conexion = Conexion;
                Compra.IDEmpresa = IDEmpresa;
                Compra.Usuario = User.Identity.Name;
                Compra.ListaVehiculos = CompraDatos.GetVehiculosXIDEmpresa(Compra);

                return Content(Compra.ListaVehiculos.ToJSON(), "application/json");
            }
            catch
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error. Por favor contacte a soporte técnico";
                return Json("");
            }
        }
        [HttpPost]
        public ActionResult GetJaulasXIDEmpresa(string IDEmpresa)
        {
            try
            {
                Compra = new CompraModels();
                CompraDatos = new _Compra_Datos();
                Compra.Conexion = Conexion;
                Compra.IDEmpresa = IDEmpresa;
                Compra.Usuario = User.Identity.Name;
                Compra.ListaJaulas = CompraDatos.GetJaulasXIDEmpresa(Compra);

                return Content(Compra.ListaJaulas.ToJSON(), "application/json");
            }
            catch
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error. Por favor contacte a soporte técnico";
                return Json("");
            }
        }
        [HttpPost]
        public ActionResult GetRemolquesXIDEmpresa(string IDEmpresa)
        {
            try
            {
                Compra = new CompraModels();
                CompraDatos = new _Compra_Datos();
                Compra.Conexion = Conexion;
                Compra.IDEmpresa = IDEmpresa;
                Compra.Usuario = User.Identity.Name;
                Compra.ListaRemolques = CompraDatos.GetRemolquesXIDEmpresa(Compra);

                return Content(Compra.ListaRemolques.ToJSON(), "application/json");
            }
            catch
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error. Por favor contacte a soporte técnico";
                return Json("");
            }
        }
        #endregion
        //Funciones SaveUpdPestañas
        #region Funciones SaveUpdPestañas
        [HttpPost]
        public ActionResult A_Proveedor(CompraModels Compra)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CompraDatos = new _Compra_Datos();
                    Compra.Conexion = Conexion;
                    Compra.Usuario = User.Identity.Name;
                    Compra = CompraDatos.Compras_ac_Proveedor(Compra);

                    Compra.RespuestaAjax.Mensaje = Compra.Mensaje;
                    Compra.RespuestaAjax.Success = Compra.Completado;

                    return Content(Compra.RespuestaAjax.ToJSON(), "application/json");
                }
                else
                {
                    Compra.RespuestaAjax.Mensaje = "Verifique su formulario.";
                    Compra.RespuestaAjax.Success = false;
                    return Content(Compra.RespuestaAjax.ToJSON(), "application/json");
                }
            }
            catch (Exception ex)
            {
                Compra.RespuestaAjax.Mensaje = ex.ToString();
                Compra.RespuestaAjax.Success = false;
                return Content(Compra.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        [HttpPost]
        public ActionResult AC_Flete(CompraModels Compra)
        {
            try
            {
                ModelState.Remove("IDProveedor");
                if (ModelState.IsValid)
                {
                    CompraDatos = new _Compra_Datos();
                    Compra.Conexion = Conexion;
                    Compra.Usuario = User.Identity.Name;
                    Compra = CompraDatos.Compras_ac_Flete(Compra);

                    Compra.RespuestaAjax.Mensaje = Compra.Mensaje;
                    Compra.RespuestaAjax.Success = Compra.Completado;

                    return Content(Compra.RespuestaAjax.ToJSON(), "application/json");
                }
                else
                {
                    Compra.RespuestaAjax.Mensaje = "Verifique su formulario.";
                    Compra.RespuestaAjax.Success = false;
                    return Content(Compra.RespuestaAjax.ToJSON(), "application/json");
                }
            }
            catch (Exception ex)
            {
                Compra.RespuestaAjax.Mensaje = ex.ToString();
                Compra.RespuestaAjax.Success = false;
                return Content(Compra.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        [HttpPost]
        public ActionResult A_Documentos(CompraModels Compra)
        {
            try
            {
                ModelState.Remove("IDProveedor");
                if (ModelState.IsValid)
                {
                    CompraDatos = new _Compra_Datos();
                    Compra.Conexion = Conexion;
                    Compra.Usuario = User.Identity.Name;
                    Compra = CompraDatos.Compras_a_Documentos(Compra);

                    Compra.RespuestaAjax.Mensaje = Compra.Mensaje;
                    Compra.RespuestaAjax.Success = Compra.Completado;

                    return Content(Compra.RespuestaAjax.ToJSON(), "application/json");
                }
                else
                {
                    Compra.RespuestaAjax.Mensaje = "Verifique su formulario.";
                    Compra.RespuestaAjax.Success = false;
                    return Content(Compra.RespuestaAjax.ToJSON(), "application/json");
                }
            }
            catch (Exception ex)
            {
                Compra.RespuestaAjax.Mensaje = ex.ToString();
                Compra.RespuestaAjax.Success = false;
                return Content(Compra.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        #endregion
        //Funciones imagenes - fierro
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
        //Funcion Index Json
        #region Funcion index Json
        [HttpPost]
        public ActionResult JsonIndex(CompraModels Compra)
        {
            try
            {
                CompraDatos = new _Compra_Datos();
                Compra.Conexion = Conexion;

                Compra.RespuestaAjax.Mensaje = Auxiliar.SqlReaderToJson(CompraDatos.ObtenerCompraIndexDataTable(Compra));
                Compra.RespuestaAjax.Success = true;

                return Content(Compra.RespuestaAjax.Mensaje, "application/json");

            }
            catch (Exception ex)
            {
                Compra.RespuestaAjax.Mensaje = ex.ToString();
                Compra.RespuestaAjax.Success = false;
                return Content(Compra.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        #endregion


        [HttpGet]
        public ActionResult Edit(string IDCompra)
        {
            Compra = new CompraModels();
            CompraDatos = new _Compra_Datos();
            //Asigno valores para los querys
            Compra.Conexion = Conexion;
            Compra.IDCompra = IDCompra;
            //Obtengo los datos de la compra
            Compra.Estatus = CompraDatos.GetEstatusCompra(Compra);

            switch (Compra.Estatus)
            {
                case 0:
                    return RedirectToAction("ProgramarCompra", "Compra", new { IDCompra = Compra.IDCompra });
                case 1:
                    return RedirectToAction("EmbarqueCompra", "Compra", new { IDCompra = Compra.IDCompra });
                default:
                    return View(Compra);
            }
        }

        [HttpGet]
        public ActionResult Continue(string IDCompra)
        {
            Compra = new CompraModels();
            CompraDatos = new _Compra_Datos();
            //Asigno valores para los querys
            Compra.Conexion = Conexion;
            Compra.IDCompra = IDCompra;
            //Obtengo los datos de la compra
            Compra.Estatus = CompraDatos.GetEstatusCompra(Compra);

            switch (Compra.Estatus)
            {
                case 0:
                    return RedirectToAction("EmbarqueCompra", "Compra", new { IDCompra = Compra.IDCompra });
                case 1:
                    return RedirectToAction("RecepcionCompra", "Compra", new { IDCompra = Compra.IDCompra });
                default:
                    return View(Compra);
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

        #region MODALES
        #region Ganado
        [HttpPost]
        public ActionResult ModalGanado(string IDGanado, decimal Merma)
        {
            Compra = new CompraModels();
            CompraDatos = new _Compra_Datos();
            Compra.Ganado.id_Ganados = IDGanado;
            Compra.Sucursal.MermaPredeterminada = Merma;
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
