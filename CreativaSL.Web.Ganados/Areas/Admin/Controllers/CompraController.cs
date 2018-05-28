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
using CreativaSL.Web.Ganados.App_Start;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    [Autorizado]
    public class CompraController : Controller
    {
        private string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
        private TokenProcessor Token = TokenProcessor.GetInstance();
        private CompraModels Compra;
        private _Compra_Datos CompraDatos;

        #region AgendarCompra
        [HttpGet]
        public ActionResult AgendarCompra(string IDCompra)
        {
            try
            {
                Token.SaveToken();
                Compra = new CompraModels();
                CompraDatos = new _Compra_Datos();
                Compra.Conexion = Conexion;

                if (!string.IsNullOrEmpty(IDCompra))
                {
                    Compra.IDCompra = IDCompra;
                    Compra = CompraDatos.GetCompraProgramada(Compra);
                    Compra = CompraDatos.GetCompraEmbarque(Compra);
                }
                Compra.ListaEmpresas = CompraDatos.GetListadoEmpresas(Compra);
                Compra.ListaSucursales = CompraDatos.GetListadoSucursales(Compra);
                Compra.ListaProveedores = CompraDatos.GetListaProveedores(Compra);
                Compra.ListaLugares = CompraDatos.GetListadoLugaresLugarXIDEmpresa(Compra);
                Compra.ListaChoferes = CompraDatos.GetChoferesXIDEmpresa(Compra);
                Compra.ListaVehiculos = CompraDatos.GetVehiculosXIDEmpresa(Compra);
                Compra.ListaLugaresProveedor = CompraDatos.GetListadoLugaresProveedorXIDProveedor(Compra);
            }
            catch (Exception ex)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista, error: " + ex.ToString();
            }
            return View(Compra);
        }
        #endregion
        #region CompraGanado
        [HttpGet]
        public ActionResult GanadoCompra(string IDCompra)
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
                    Compra = CompraDatos.GetGanadoCompra(Compra);
                    Compra.ListadoPrecioRangoPesoString = CompraDatos.GetListadoPrecioRangoPeso(Compra).ToJSON();
                    Compra.ListaEstatusGanadoString = CompraDatos.GetListadoEstatusGanado(Compra).ToJSON();
                    Compra.ListaCorralesString = CompraDatos.GetListaCorrales(Compra).ToJSON();

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
        #endregion
        #region Funcion Json Documentos
        [HttpPost]
        public ActionResult TableJsonDocumentos(string IDFlete)
        {
            try
            {
                Compra = new CompraModels();
                CompraDatos = new _Compra_Datos();
                Compra.Conexion = Conexion;
                Compra.IDFlete = IDFlete;

                Compra.RespuestaAjax.Mensaje = Auxiliar.SqlReaderToJson(CompraDatos.GetDocumentosDataTable(Compra));
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
        public ActionResult TableJsonDocumentosDetalles(string IDCompra)
        {
            try
            {
                Compra = new CompraModels();
                CompraDatos = new _Compra_Datos();
                Compra.Conexion = Conexion;
                Compra.IDCompra = IDCompra;

                Compra.RespuestaAjax.Mensaje = Auxiliar.SqlReaderToJson(CompraDatos.GetDocumentosPorCobrarDetalles(Compra));
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
        #region Funcion Json Index
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
        #region Funcion Json Ganado
        [HttpPost]
        public ActionResult TableJsonGanadoCompra(string  IDCompra)
        {
            try
            {
                CompraDatos = new _Compra_Datos();
                Compra = new CompraModels();
                Compra.Conexion = Conexion;
                Compra.IDCompra = IDCompra;
                Compra.RespuestaAjax.Mensaje = Auxiliar.SqlReaderToJson(CompraDatos.TableJsonGanadoCompra(Compra));
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
        #region Index
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
        #endregion
        #region Edit
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
                    return RedirectToAction("AgendarCompra", "Compra", new { IDCompra = Compra.IDCompra });
                case 1:
                    return RedirectToAction("AgendarCompra", "Compra", new { IDCompra = Compra.IDCompra });
                case 2:
                    return RedirectToAction("GanadoCompra", "Compra", new { IDCompra = Compra.IDCompra });
                default:
                    return View(Compra);
            }
        }
        #endregion
        #region Funciones AC DEL
        #region Proveedor
        [HttpPost]
        public ActionResult AC_Proveedor(CompraModels Compra)
        {
            try
            {
                string[] camposValidar = { "IDProveedor", "IDSucursal", "IDPLugarProveedor", "GanadosPactadoMachos", "GanadosPactadoHembras", "FechaHoraProgramada" };
                bool formularioValido = true;
                var errors = ModelState.Keys.Where(k => ModelState[k].Errors.Count > 0).Select(k => new { propertyName = k, errorMessage = ModelState[k].Errors[0].ErrorMessage });

                foreach (var item in errors)
                {
                    for (int j = 0; j < camposValidar.Count(); j++)
                    {

                        if (string.Equals(item.propertyName, camposValidar[j]))
                        {
                            formularioValido = false;
                            break;
                        }
                    }
                }

                if (formularioValido)
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
        #endregion
        #region Flete
        [HttpPost]
        public ActionResult AC_Flete(CompraModels Compra)
        {
            try
            {
                string[] camposValidar = { "IDEmpresa", "IDSucursal", "IDChofer", "IDVehiculo", "Flete.kmInicialVehiculo" };
                bool formularioValido = true;
                var errors = ModelState.Keys.Where(k => ModelState[k].Errors.Count > 0).Select(k => new { propertyName = k, errorMessage = ModelState[k].Errors[0].ErrorMessage });

                foreach (var item in errors)
                {
                    for (int j = 0; j < camposValidar.Count(); j++)
                    {

                        if (string.Equals(item.propertyName, camposValidar[j]))
                        {
                            formularioValido = false;
                            break;
                        }
                    }
                }
                if (formularioValido)
                {
                    CompraDatos = new _Compra_Datos();
                    Compra.Conexion = Conexion;
                    Compra.Usuario = User.Identity.Name;
                    Compra = CompraDatos.Compras_ac_Flete(Compra);

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
        #region Ganado
        [HttpPost]
        public ActionResult AC_Ganado(string IDCompra, string IDGanado, string numArete, string id_genero, int id_estado,
            decimal peso, decimal repeso, decimal merma, decimal peso_pagar, decimal costo_kilo, int id_corral, string Id_detalleDocumentoPorCobrar,
            int indiceActual)
        {
            try
            {
                CompraDatos = new _Compra_Datos();
                Compra = new CompraModels();
                Compra.IDCompra = IDCompra;
                Compra.Ganado.id_Ganados = IDGanado;
                Compra.Ganado.numArete = numArete;
                Compra.Ganado.genero = id_genero;
                Compra.Ganado.IDEstatusGanado = id_estado;
                Compra.Ganado.CompraGanado.PesoInicial = peso;
                Compra.Ganado.CompraGanado.PesoFinal = repeso;
                Compra.Ganado.CompraGanado.Merma = merma;
                Compra.Ganado.CompraGanado.PesoPagado = peso_pagar;
                Compra.Ganado.CompraGanado.PrecioKilo = costo_kilo;
                Compra.Ganado.IDCorral = id_corral;
                Compra.Ganado.CompraGanado.Id_detalleDocumentoPorCobrar = Id_detalleDocumentoPorCobrar;


                Compra.Conexion = Conexion;
                Compra.Usuario = User.Identity.Name;
                Compra = CompraDatos.Compras_ac_Ganado(Compra, indiceActual);

                return Content(Compra.RespuestaAjax.ToJSON(), "application/json");
            }
            catch (Exception ex)
            {
                Compra.RespuestaAjax.Mensaje = ex.ToString();
                Compra.RespuestaAjax.Success = false;
                return Content(Compra.RespuestaAjax.ToJSON(), "application/json");
            }
        }

        [HttpPost]
        public ActionResult DEL_Ganado(string IDCompra, string IDGanado, string Id_detalleDocumentoPorCobrar)
        {
            try
            {
                CompraDatos = new _Compra_Datos();
                Compra = new CompraModels();
                Compra.IDCompra = IDCompra;
                Compra.Ganado.id_Ganados = IDGanado;
                Compra.Ganado.CompraGanado.Id_detalleDocumentoPorCobrar = Id_detalleDocumentoPorCobrar;
                Compra.Conexion = Conexion;
                Compra.Usuario = User.Identity.Name;

                Compra = CompraDatos.Compras_del_Ganado(Compra);

                return Content(Compra.RespuestaAjax.ToJSON(), "application/json");
            }
            catch (Exception ex)
            {
                Compra.RespuestaAjax.Mensaje = ex.ToString();
                Compra.RespuestaAjax.Success = false;
                return Content(Compra.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        #endregion




        #endregion
        #region Funciones combo
        #region Chofer
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
        #endregion
        #region Vehiculo
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
        #endregion
        #region Jaula
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
        #endregion
        #region Remolque
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
        #region Lugar
        [HttpPost]
        public ActionResult GetLugaresProveedorXIDProveedor(string IDProveedor)
        {
            try
            {
                Compra = new CompraModels();
                CompraDatos = new _Compra_Datos();
                Compra.Conexion = Conexion;
                Compra.IDProveedor = IDProveedor;
                Compra.Usuario = User.Identity.Name;
                Compra.ListaLugaresProveedor = CompraDatos.GetListadoLugaresProveedorXIDProveedor(Compra);

                return Content(Compra.ListaLugaresProveedor.ToJSON(), "application/json");
            }
            catch
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error. Por favor contacte a soporte técnico";
                return Json("");
            }
        }
        [HttpPost]
        public ActionResult GetLugaresXIDEmpresa(string IDEmpresa)
        {
            try
            {
                Compra = new CompraModels();
                CompraDatos = new _Compra_Datos();
                Compra.Conexion = Conexion;
                Compra.IDEmpresa = IDEmpresa;
                Compra.Usuario = User.Identity.Name;
                Compra.ListaLugares = CompraDatos.GetListadoLugaresLugarXIDEmpresa(Compra);

                return Content(Compra.ListaLugares.ToJSON(), "application/json");
            }
            catch
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error. Por favor contacte a soporte técnico";
                return Json("");
            }
        }
        #endregion
        #endregion
        #region Modales
        #region ListaPrecios
        public ActionResult ModalListaPrecios(int IDTipoProveedor)
        {
            Compra = new CompraModels();
            CompraDatos = new _Compra_Datos();
            Compra.Proveedor.IDTipoProveedor = IDTipoProveedor;
            Compra.Conexion = Conexion;
            Compra.ListaRangoPeso = CompraDatos.GetListadoPrecioRangoPeso(Compra);

            return PartialView("ModalListadoPrecios", Compra);
        }
        #endregion
        #endregion

        [HttpGet]
        public ActionResult CambiarEstatus(string IDCompra)
        {
            Compra = new CompraModels();
            CompraDatos = new _Compra_Datos();
            Compra.Conexion = Conexion;
            Compra.IDCompra = IDCompra;
            Compra = CompraDatos.CambiarEstatusCompra(Compra);

            if (Compra.RespuestaAjax.Success)
            {
                TempData["typemessage"] = "1";
                TempData["message"] = "Estatus cambiado con éxito";
            }
            else
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "No se pudo cambiar el estatus de la compra";
            }

            return RedirectToAction("Index", "Compra");
        }
        [HttpGet]
        public ActionResult Transacciones(string IDCompra)
        {
            Compra = new CompraModels();
            CompraDatos = new _Compra_Datos();
            //Asigno valores para los querys
            Compra.Conexion = Conexion;
            Compra.IDCompra = IDCompra;
            //obtengo los generales
            Compra.DocumentoPorCobrar = CompraDatos.GetGeneralesDocumentoPorCobrar(Compra);


            return View(Compra);
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
                    //Compra = CompraDatos.GetCompra(Compra);
                    //Obtengo el listado de precios
                    //Compra.ListadoPrecioRangoPeso = Auxiliar.SqlReaderToJson(CompraDatos.GetSqlDataReaderListadoPrecioRangoPeso(Compra));

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
        /********************************************************************/
       

        #region MODALES
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
