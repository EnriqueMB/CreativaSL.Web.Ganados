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
    public class FleteController : Controller
    {
        private string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
        private FleteModels Flete;
        private _Flete_Datos FleteDatos;

        // GET: Admin/Flete
        public ActionResult Index()
        {
            try
            {
                Flete = new FleteModels();
                return View(Flete);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /********************************************************************/
        //Funcion Index Json
        #region Funcion index Json
        [HttpPost]
        public ActionResult JsonIndex()
        {
            try
            {
                Flete = new FleteModels();
                FleteDatos = new _Flete_Datos();
                Flete.Conexion = Conexion;

                Flete.RespuestaAjax.Mensaje = Auxiliar.SqlReaderToJson(FleteDatos.ObtenerFleteIndexDataTable(Flete));
                Flete.RespuestaAjax.Success = true;

                return Content(Flete.RespuestaAjax.Mensaje, "application/json");

            }
            catch (Exception ex)
            {
                Flete.RespuestaAjax.Mensaje = ex.ToString();
                Flete.RespuestaAjax.Success = false;
                return Content(Flete.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        #endregion
        /********************************************************************/


        [HttpGet]
        public ActionResult AC_Flete(string IDFlete)
        {
            Flete = new FleteModels
            {
                Conexion = Conexion
            };
            FleteDatos = new _Flete_Datos();
            //cargarmos los datos del flete x id
            if (!string.IsNullOrEmpty(IDFlete) && IDFlete.Length == 36)
            {
                Flete.id_flete = IDFlete;
                Flete = FleteDatos.Flete_get_ACFlete(Flete);
            }
            Flete.ListaEmpresa = FleteDatos.GetListadoEmpresas(Flete);
            Flete.ListaCliente = FleteDatos.GetListadoClientes(Flete);
            Flete.ListaChofer = FleteDatos.GetListadoChoferes(Flete);
            Flete.ListaVehiculo = FleteDatos.GetListadoVehiculos(Flete);
            Flete.ListaJaula = FleteDatos.GetListadoJaulas(Flete);
            Flete.ListaRemolque = FleteDatos.GetListadoRemolque(Flete);
            Flete.Trayecto.ListaRemitente = FleteDatos.GetListadoClientes(Flete);
            Flete.Trayecto.ListaDestinatario = FleteDatos.GetListadoClientes(Flete);
            Flete.Trayecto.ListaLugarOrigen = FleteDatos.GetListadoLugaresXIDProveedorIDCliente(Flete, Flete.Trayecto.Remitente.IDCliente);
            Flete.Trayecto.ListaLugarDestino = FleteDatos.GetListadoLugaresXIDProveedorIDCliente(Flete, Flete.Trayecto.Destinatario.IDCliente);
            Flete.ListaFormaPago = FleteDatos.GetListadoFormaPagos(Flete);
            Flete.ListaMetodoPago = FleteDatos.GetListadoMetodoPago(Flete);

            return View(Flete);
        }

        [HttpGet]
        public ActionResult Edit(string IDFlete)
        {
            Flete = new FleteModels();
            FleteDatos = new _Flete_Datos();
            Flete.Conexion = Conexion;
            Flete.id_flete = IDFlete;
            Flete = FleteDatos.GetEstatusFleteXIDFlete(Flete);

            switch (Flete.Estatus)
            {
                case 0:
                    return RedirectToAction("AC_Flete", "Flete", new { IDFlete = Flete.id_flete });
                case 1:
                    return RedirectToAction("AC_FleteRecepcion", "Flete", new { IDFlete = Flete.id_flete });
                default:
                    return View(Flete);
            }
        }
        //[HttpGet]
        //public ActionResult Continue(string IDCompra)
        //{
        //    Compra = new CompraModels();
        //    CompraDatos = new _Compra_Datos();
        //    //Asigno valores para los querys
        //    Compra.Conexion = Conexion;
        //    Compra.IDCompra = IDCompra;
        //    //Obtengo los datos de la compra
        //    Compra.Estatus = CompraDatos.GetEstatusCompra(Compra);

        //    switch (Compra.Estatus)
        //    {
        //        case 0:
        //            return RedirectToAction("EmbarqueCompra", "Compra", new { IDCompra = Compra.IDCompra });
        //        case 1:
        //            return RedirectToAction("RecepcionCompra", "Compra", new { IDCompra = Compra.IDCompra });
        //        default:
        //            return View(Compra);
        //    }
        //}
        /********************************************************************/
        //Funciones AC
        #region AC_Cliente
        public ActionResult AC_Cliente(FleteModels Flete)
        {
            try
            {
                FleteDatos = new _Flete_Datos();
                Flete.Conexion = Conexion;
                Flete.Usuario = User.Identity.Name;
                Flete = FleteDatos.Flete_ac_FleteCliente(Flete);
                if (Flete.RespuestaAjax.Success)
                    Flete.RespuestaAjax.Mensaje.ToJSON();

                return Content(Flete.RespuestaAjax.ToJSON(), "application/json");
                
            }
            catch (Exception ex)
            {
                Flete.RespuestaAjax.Mensaje = ex.ToString();
                Flete.RespuestaAjax.Success = false;
                return Content(Flete.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        #endregion
        #region AC_Trayecto
        public ActionResult AC_Trayecto(FleteModels Flete)
        {
            try
            {
                if(Flete.id_flete.Length == 36)
                {
                    FleteDatos = new _Flete_Datos();
                    Flete.Conexion = Conexion;
                    Flete.Usuario = User.Identity.Name;
                    Flete = FleteDatos.Flete_ac_FleteTrayecto(Flete);

                    return Content(Flete.RespuestaAjax.ToJSON(), "application/json");
                }
                else
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Flete no válido.";
                    return RedirectToAction("Index", "Flete");
                }
            }
            catch (Exception ex)
            {
                Flete.RespuestaAjax.Mensaje = ex.ToString();
                Flete.RespuestaAjax.Success = false;
                return Content(Flete.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        #endregion
        /********************************************************************/

        /********************************************************************/
        //Funciones Combo
        #region funciones combo
        [HttpPost]
        public ActionResult GetChoferesXIDEmpresa(string IDEmpresa)
        {
            try
            {
                Flete = new FleteModels();
                FleteDatos = new _Flete_Datos();
                Flete.Conexion = Conexion;
                Flete.Empresa.IDEmpresa = IDEmpresa;
                Flete.Usuario = User.Identity.Name;
                Flete.Chofer.ListaChoferes = FleteDatos.GetChoferesXIDEmpresa(Flete);

                return Content(Flete.Chofer.ListaChoferes.ToJSON(), "application/json");
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
                Flete = new FleteModels();
                FleteDatos = new _Flete_Datos();
                Flete.Conexion = Conexion;
                Flete.Empresa.IDEmpresa = IDEmpresa;
                Flete.Usuario = User.Identity.Name;
                Flete.Vehiculo.listaVehiculos = FleteDatos.GetVehiculosXIDEmpresa(Flete);

                return Content(Flete.Vehiculo.listaVehiculos.ToJSON(), "application/json");
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
                Flete = new FleteModels();
                FleteDatos = new _Flete_Datos();
                Flete.Conexion = Conexion;
                Flete.Empresa.IDEmpresa = IDEmpresa;
                Flete.Usuario = User.Identity.Name;
                Flete.Jaula.listaJaulas = FleteDatos.GetJaulasXIDEmpresa(Flete);

                return Content(Flete.Jaula.listaJaulas.ToJSON(), "application/json");
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
                Flete = new FleteModels();
                FleteDatos = new _Flete_Datos();
                Flete.Conexion = Conexion;
                Flete.Empresa.IDEmpresa = IDEmpresa;
                Flete.Usuario = User.Identity.Name;
                Flete.Remolque.listaRemolque = FleteDatos.GetRemolquesXIDEmpresa(Flete);

                return Content(Flete.Remolque.listaRemolque.ToJSON(), "application/json");
            }
            catch
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error. Por favor contacte a soporte técnico";
                return Json("");
            }
        }
        [HttpPost]
        public ActionResult GetLugarXIDRemitente(string IDRemitente)
        {
            try
            {
                Flete = new FleteModels();
                FleteDatos = new _Flete_Datos();
                Flete.Conexion = Conexion;
                Flete.Usuario = User.Identity.Name;
                Flete.Trayecto.ListaLugarOrigen = FleteDatos.GetListadoLugaresXIDProveedorIDCliente(Flete, IDRemitente);

                return Content(Flete.Trayecto.ListaLugarOrigen.ToJSON(), "application/json");
            }
            catch
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error. Por favor contacte a soporte técnico";
                return Json("");
            }
        }
        [HttpPost]
        public ActionResult GetLugarXIDDestino(string IDDestino)
        {
            try
            {
                Flete = new FleteModels();
                FleteDatos = new _Flete_Datos();
                Flete.Conexion = Conexion;
                Flete.Usuario = User.Identity.Name;
                Flete.Trayecto.ListaLugarDestino = FleteDatos.GetListadoLugaresXIDProveedorIDCliente(Flete, IDDestino);

                return Content(Flete.Trayecto.ListaLugarDestino.ToJSON(), "application/json");
            }
            catch
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error. Por favor contacte a soporte técnico";
                return Json("");
            }
        }
        
        #endregion
        /********************************************************************/
    }
}
