using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CreativaSL.Web.Ganados.Filters;
using CreativaSL.Web.Ganados.Models;

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
            _Compra_Datos CompraDatos = new _Compra_Datos();
            Compra.Conexion = Conexion;
            Compra.IDCompra = IDCompra;
            //Compra = CompraDatos.GetCompraPacta(Compra);
            Compra.Sucursal.IDSucursal = SucursalDefault;
            Compra.ListaProveedores = CompraDatos.GetListadoProveedores(Compra);
            Compra.ListaLugares = CompraDatos.GetListadoLugares(Compra);
            Compra.ListaChoferes = CompraDatos.GetListadoChoferes(Compra);
            Compra.ListaVehiculos = CompraDatos.GetListadoVehiculos(Compra);
            Compra.ListaJaulas = CompraDatos.GetListadoJaulas(Compra);

            return View(Compra);
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




        /*VERIFICAR ESTOS*/
        // GET: Admin/Compra/Fierros/5
        public ActionResult Fierros(string id)
        {
            Compra = new CompraModels();
            CompraDatos = new _Compra_Datos();
            Compra.Conexion = Conexion;
            Compra.IDCompra = id;
            Compra = CompraDatos.GetFierros(Compra);

            return View(Compra);
        }
        // GET: Admin/Compra/Flete/5
        public ActionResult Flete(string id)
        {
            Compra = new CompraModels();
            CompraDatos = new _Compra_Datos();
            Compra.Conexion = Conexion;
            Compra.IDCompra = id;
            Compra = CompraDatos.GetFlete(Compra);
            //Compra.ListaChoferes = CompraDatos.GetChoferes(Compra);
            //Compra.ListaVehiculos = CompraDatos.GetVehiculos(Compra);


            /*Relleno el combobox de lugares INICIO*/
            //Compra = CompraDatos.ObtenerLugares(Compra, 1);
            CompraModels CompraLugar = new CompraModels();
            Compra.Lugar = new CatLugarModels
            {
                listaLugares = new List<CatLugarModels>()
            };


            foreach (System.Data.DataRow Lugar in Compra.TablaLugares.Rows)
            {
                CompraLugar.Lugar = new CatLugarModels
                {
                    id_lugar = Lugar["id_lugar"].ToString() + " | " + Lugar["gpsLatitud"].ToString() + " | " + Lugar["gpsLongitud"].ToString(),
                    descripcion = Lugar["descripcion"].ToString()
                };

                Compra.Lugar.listaLugares.Add(CompraLugar.Lugar);
            }
            var ListLugaresInicio = new SelectList(Compra.Lugar.listaLugares, "id_lugar", "descripcion");
            ViewData["cmbLugarInicio"] = ListLugaresInicio;

            /*Relleno el combobox de lugares FINAL*/
            Compra = new CompraModels
            {
                Conexion = Conexion
            };
            //Compra = CompraDatos.ObtenerLugares(Compra, 0);
            Compra.Lugar = new CatLugarModels
            {
                listaLugares = new List<CatLugarModels>()
            };

            foreach (System.Data.DataRow Lugar in Compra.TablaLugares.Rows)
            {
                CompraLugar.Lugar = new CatLugarModels
                {
                    id_lugar = Lugar["id_lugar"].ToString() + " | " + Lugar["gpsLatitud"].ToString() + " | " + Lugar["gpsLongitud"].ToString(),
                    descripcion = Lugar["descripcion"].ToString()
                };

                Compra.Lugar.listaLugares.Add(CompraLugar.Lugar);
            }
            var ListLugaresFinal = new SelectList(Compra.Lugar.listaLugares, "id_lugar", "descripcion");
            ViewData["cmbLugarFinal"] = ListLugaresFinal;



            return View(Compra);
        }
        public ActionResult Ganado(string id)
        {
            Compra = new CompraModels();
            return View(Compra);
        }
        public ActionResult OtrosMovimientos(string id)
        {
            Compra = new CompraModels();
            return View(Compra);
        }
        public ActionResult Pagos(string id)
        {
            Compra = new CompraModels();
            return View(Compra);
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


        /*ACTION DELETE*/
        /*FIERROS*/
        [HttpPost]
        public JsonResult EliminarFierro(string id_fierro)
        {

            return Json("a");
        }

        #region MODALES
        #region Ganado
        [HttpGet]
        public ActionResult NuevoGanado()
        {
            Compra = new CompraModels();   
            return PartialView("ModalGanado", Compra.Ganado);
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
