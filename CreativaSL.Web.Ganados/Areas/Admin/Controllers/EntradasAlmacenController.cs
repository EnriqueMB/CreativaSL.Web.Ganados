using CreativaSL.Web.Ganados.Models;
using CreativaSL.Web.Ganados.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    public class EntradasAlmacenController : Controller
    {
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
        // GET: Admin/EntradasAlmacen
        public ActionResult Index(string Folio)
        {
            try
            {
                List<EntradaAlmacenModels> Model = new List<EntradaAlmacenModels>();
                _EntradaAlmacen_Datos Datos = new _EntradaAlmacen_Datos();
                Model = Datos.ObtenerEntradas(Conexion, Folio);
                return View(Model);
            }
            catch (Exception)
            {
                return View(new List<EntradaAlmacenModels>());
            }
        }

        // GET: Admin/EntradasAlmacen/Create
        public ActionResult Create()
        {
            try
            {
                EntradaAlmacenViewModels Model = new EntradaAlmacenViewModels();
                _Combos_Datos Datos = new _Combos_Datos();
                Model.ListaAlmacenes = Datos.ObtenerAlmacenesXIDCompra(Conexion, string.Empty);
                Model.ListaCompras = Datos.ObtenerComprasProcesadas(Conexion);
                Model.FechaEntrada = DateTime.Today;
                return View(Model);
            }
            catch (Exception)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("Index");
            }
        }

        // POST: Admin/EntradasAlmacen/Create
        [HttpPost]
        public ActionResult Create(EntradaAlmacenViewModels Model)
        {
            _EntradaAlmacen_Datos Datos = new _EntradaAlmacen_Datos();
            _Combos_Datos CDatos = new _Combos_Datos();
            try
            {
                if (ModelState.IsValid)
                {
                    EntradaAlmacenModels ModelP = new EntradaAlmacenModels
                    {
                        NuevoRegistro = true,
                        IDEntradaAlmacen = string.Empty,
                        Almacen = new CatAlmacenModels { IDAlmacen = Model.IDAlmacen },
                        CompraAlmacen = new CompraAlmacenModels { IDCompraAlmacen = Model.IDCompraAlmacen },
                        FechaEntrada = Model.FechaEntrada,
                        Comentario = Model.Comentario,
                        Conexion = Conexion,
                        Usuario = User.Identity.Name
                    };
                    Datos.ACEntradaAlmacen(ModelP);
                    if (ModelP.Completado == true)
                    {
                        TempData["typemessage"] = "1";
                        TempData["message"] = "Los datos se guardaron correctamente.";
                        return RedirectToAction("CreateDetail", new { id = ModelP.IDEntradaAlmacen });
                    }
                    else
                    {
                        Model.ListaAlmacenes = CDatos.ObtenerAlmacenes(Conexion);
                        Model.ListaCompras = CDatos.ObtenerComprasProcesadas(Conexion);
                        TempData["typemessage"] = "2";
                        TempData["message"] = "Ocurrió un error al intentar guardar los datos. Intente más tarde.";
                        return View(Model);
                    }
                }
                else
                {
                    Model.ListaAlmacenes = CDatos.ObtenerAlmacenes(Conexion);
                    Model.ListaCompras = CDatos.ObtenerComprasProcesadas(Conexion);
                    return View(Model);
                }
            }
            catch
            {
                Model.ListaAlmacenes = CDatos.ObtenerAlmacenes(Conexion);
                Model.ListaCompras = CDatos.ObtenerComprasProcesadas(Conexion);
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrió un error al intentar guardar los datos. Contacte a soporte técnico.";
                return View(Model);
            }
        }

        // GET: Admin/EntradasAlmacen/Create
        public ActionResult CreateDetail(string id)
        {
            try
            {
                _EntradaAlmacen_Datos Datos = new _EntradaAlmacen_Datos();
                EntradaAlmacenDetalleViewModels Model = new EntradaAlmacenDetalleViewModels();
                Model.ListaDetalle = Datos.ObtenerDetalleEntradaXID(Conexion, id);
                Model.IDEntrega = id;
                return View(Model);
            }
            catch (Exception)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("Index");
            }
        }

        // POST: Admin/EntradasAlmacen/Create
        [HttpPost]
        public ActionResult CreateDetail(EntradaAlmacenDetalleViewModels Model)
        {
            _EntradaAlmacen_Datos Datos = new _EntradaAlmacen_Datos();
            _Combos_Datos CDatos = new _Combos_Datos();
            try
            {
                if (ModelState.IsValid)
                {
                    EntradaAlmacenDetalleModels ModelP = new EntradaAlmacenDetalleModels
                    {
                        IDEntradaAlmacen = Model.IDEntrega,
                        ListaDetalle = Model.ListaDetalle,
                        Conexion = Conexion,
                        Usuario = User.Identity.Name
                    };
                    ModelP.LlenarTabla();
                    Datos.ACEntradaAlmacenDetalle(ModelP);
                    if(ModelP.Completado)
                    {
                        TempData["typemessage"] = "1";
                        TempData["message"] = "Datos guardados correctamente";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["typemessage"] = "2";
                        TempData["message"] = "Ocurrió un error al guardar los datos.";
                        return View(Model);
                    }
                }
                else
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Errores en el modelo.";
                    return View(Model);
                }
            }
            catch
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrió un error al intentar guardar los datos. Contacte a soporte técnico.";
                return View(Model);
            }
        }

        // GET: Admin/EntradasAlmacen
        public ActionResult Procesar(string id, string id2)
        {
            try
            {
                return View();
            }
            catch (Exception)
            {
                return View();
            }
        }


        // POST: Admin/EntradasAlmacen/ObtenerAlmacenesXIDSucursal/IDsucursal
        [HttpPost]
        public ActionResult ObtenerAlmacenesXIDSucursal(string IDCompra)
        {
            try
            {
                _Combos_Datos Datos = new _Combos_Datos();
                List<CatAlmacenModels> Lista = Datos.ObtenerAlmacenesXIDCompra(Conexion, IDCompra );
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