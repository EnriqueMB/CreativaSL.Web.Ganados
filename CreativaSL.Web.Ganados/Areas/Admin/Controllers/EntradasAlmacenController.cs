using CreativaSL.Web.Ganados.App_Start;
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
        private TokenProcessor Token = TokenProcessor.GetInstance();
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

        // GET: Admin/EntradasAlmacen
        public ActionResult Details(string id)
        {
            try
            {
                List<CantidadEntregaViewModels> Model = new List<CantidadEntregaViewModels>();
                _EntradaAlmacen_Datos Datos = new _EntradaAlmacen_Datos();
                Model = Datos.ObtenerDetalleEntradaXID(Conexion, id);
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
                Token.SaveToken();
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
                if (Token.IsTokenValid())
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
                            Token.ResetToken();
                            return RedirectToAction("CreateDetail", new { id = ModelP.IDEntradaAlmacen });
                        }
                        else
                        {
                            Model.ListaAlmacenes = CDatos.ObtenerAlmacenesXIDCompra(Conexion, Model.IDCompraAlmacen);
                            Model.ListaCompras = CDatos.ObtenerComprasProcesadas(Conexion);
                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrió un error al intentar guardar los datos. Intente más tarde.";
                            return View(Model);
                        }
                    }
                    else
                    {
                        Model.ListaAlmacenes = CDatos.ObtenerAlmacenesXIDCompra(Conexion, Model.IDCompraAlmacen);
                        Model.ListaCompras = CDatos.ObtenerComprasProcesadas(Conexion);
                        return View(Model);
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                Model.ListaAlmacenes = CDatos.ObtenerAlmacenesXIDCompra(Conexion, Model.IDCompraAlmacen);
                Model.ListaCompras = CDatos.ObtenerComprasProcesadas(Conexion);
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrió un error al intentar guardar los datos. Contacte a soporte técnico.";
                return View(Model);
            }
        }

        // GET: Admin/EntradasAlmacen/Edit
        public ActionResult Edit(string id)
        {
            try
            {
                Token.SaveToken();
                EntradaAlmacenViewModels Model = new EntradaAlmacenViewModels();
                _Combos_Datos Datos = new _Combos_Datos();
                _EntradaAlmacen_Datos DatosEntrada = new _EntradaAlmacen_Datos();
                Model = DatosEntrada.ObtenerDatosEntradaXID(Conexion, id);
                Model.ListaCompras = Datos.ObtenerComprasProcesadas(Conexion);
                Model.ListaAlmacenes = Datos.ObtenerAlmacenesXIDCompra(Conexion, Model.IDCompraAlmacen);
                return View(Model);
            }
            catch (Exception)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("Index");
            }
        }

        // POST: Admin/EntradasAlmacen/Edit
        [HttpPost]
        public ActionResult Edit(EntradaAlmacenViewModels Model)
        {
            _EntradaAlmacen_Datos Datos = new _EntradaAlmacen_Datos();
            _Combos_Datos CDatos = new _Combos_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        EntradaAlmacenModels ModelP = new EntradaAlmacenModels
                        {
                            NuevoRegistro = false,
                            IDEntradaAlmacen = Model.IDEntradaAlmacen,
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
                            Token.ResetToken();
                            return RedirectToAction("CreateDetail", new { id = ModelP.IDEntradaAlmacen });
                        }
                        else
                        {
                            Model.ListaCompras = CDatos.ObtenerComprasProcesadas(Conexion);
                            Model.ListaAlmacenes = CDatos.ObtenerAlmacenesXIDCompra(Conexion, Model.IDCompraAlmacen);
                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrió un error al intentar guardar los datos. Intente más tarde.";
                            return View(Model);
                        }
                    }
                    else
                    {
                        Model.ListaCompras = CDatos.ObtenerComprasProcesadas(Conexion);
                        Model.ListaAlmacenes = CDatos.ObtenerAlmacenesXIDCompra(Conexion, Model.IDCompraAlmacen);
                        return View(Model);
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                Model.ListaCompras = CDatos.ObtenerComprasProcesadas(Conexion);
                Model.ListaAlmacenes = CDatos.ObtenerAlmacenesXIDCompra(Conexion, Model.IDCompraAlmacen);
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
                Token.SaveToken();
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
                if (Token.IsTokenValid())
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
                        if (ModelP.Completado)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "Datos guardados correctamente";
                            Token.ResetToken();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            string mensaje = "Ocurrió un error al guardar los datos.";
                            switch(ModelP.Resultado)
                            {
                                case -1: mensaje = "No se pudo realizar la entrada a almacén.";
                                    break;
                                case -2: mensaje = "Existe al menos un producto que no tiene existencia suficiente.";
                                    break;
                            }
                            TempData["typemessage"] = "2";
                            TempData["message"] = mensaje;
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
                else
                {
                    return RedirectToAction("Index");
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
        [HttpPost]
        public ActionResult Procesar(string id)
        {
            try
            {
                _EntradaAlmacen_Datos EntradaDatos = new _EntradaAlmacen_Datos();
                EntradaAlmacenModels Entrada = EntradaDatos.ProcesarEntrada(Conexion, id, User.Identity.Name);
                if (Entrada.Completado)
                    return Json("true");
                else
                    return Json("");
            }
            catch (Exception)
            {
                return Json("");
            }
        }

        // GET: Admin/EntradasAlmacen
        [HttpPost]
        public ActionResult Delete(string id)
        {
            try
            {
                _EntradaAlmacen_Datos EntradaDatos = new _EntradaAlmacen_Datos();
                EntradaAlmacenModels Entrada = EntradaDatos.EliminarEntrada(Conexion, id, User.Identity.Name);
                if (Entrada.Completado)
                    return Json("true");
                else
                    return Json("");
            }
            catch (Exception)
            {
                return Json("");
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