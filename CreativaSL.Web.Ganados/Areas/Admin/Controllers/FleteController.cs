using CreativaSL.Web.Ganados.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    public class FleteController : Controller
    {
        private string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
        private FleteModels Flete;
        private _Flete_Datos FleteDatos;

        // GET: Admin/Flete
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AC_Flete(string IDFlete)
        {
            //cargarmos los datos del flete x id
            if (!string.IsNullOrEmpty(IDFlete))
            {

                return View(Flete);
            }
            else
            {
                //si no tiene id cargamos una vista con los datos predeterminados
                try
                {
                    Flete = new FleteModels();
                    FleteDatos = new _Flete_Datos();
                    Flete.id_flete = IDFlete;
                    Flete.Conexion = Conexion;

                    Flete.ListaEmpresa = FleteDatos.GetListadoEmpresas(Flete);
                    Flete.ListaCliente = FleteDatos.GetListadoClientes(Flete);
                    Flete.ListaChofer = FleteDatos.GetListadoChoferes(Flete);
                    Flete.ListaVehiculo = FleteDatos.GetListadoVehiculos(Flete);
                    Flete.ListaJaula = FleteDatos.GetListadoJaulas(Flete);
                    Flete.ListaRemolque = FleteDatos.GetListadoRemolque(Flete);
                    Flete.Trayecto.ListaRemitente = FleteDatos.GetListadoClientes(Flete);
                    Flete.Trayecto.ListaDestinatario = FleteDatos.GetListadoClientes(Flete);
                    Flete.Trayecto.ListaLugarOrigen = FleteDatos.GetListadoLugaresXIDProveedorIDCliente(Flete, Flete.Trayecto.id_lugarOrigen);
                    Flete.Trayecto.ListaLugarDestino = FleteDatos.GetListadoLugaresXIDProveedorIDCliente(Flete, Flete.Trayecto.id_lugarDestino);
                    Flete.ListaFormaPago = FleteDatos.GetListadoFormaPagos(Flete);
                    Flete.ListaMetodoPago = FleteDatos.GetListadoMetodoPago(Flete);

                    return View(Flete);
                }
                catch (Exception ex)
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "No se puede cargar la vista, error: " + ex.ToString();
                    return View(Flete);
                }
            }
        }
        /********************************************************************/
        //Funciones AC
        #region AC_Cliente
        public ActionResult AC_Cliente(FleteModels Flete)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Flete = new FleteModels();
                    FleteDatos = new _Flete_Datos();
                    Flete.Conexion = Conexion;
                    Flete.Usuario = User.Identity.Name;

                    //Flete = FleteDatos.Compras_ac_Ganado(Compra);

                    //Flete.RespuestaAjax.Mensaje = Compra.Mensaje;
                    //Flete.RespuestaAjax.Success = Compra.Completado;

                    return Content(Flete.RespuestaAjax.ToJSON(), "application/json");
                }
                else
                {
                    Flete.RespuestaAjax.Mensaje = "Verifique su formulario.";
                    Flete.RespuestaAjax.Success = false;
                    return Content(Flete.RespuestaAjax.ToJSON(), "application/json");
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
