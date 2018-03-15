using CreativaSL.Web.Ganados.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    public class CatUsuariosController : Controller
    {
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
        // GET: Admin/CatUsuarios
        public ActionResult Index()
        {
            

            try
            {
                UsuarioModels usuario = new UsuarioModels();
                _Usuario_Datos UsuarioDatos = new _Usuario_Datos();
                usuario.conexion = Conexion;
                usuario= UsuarioDatos.ObtenerUsuarios(usuario);
                return View(usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        // GET: Admin/CatUsuarios/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/CatUsuarios/Create
        public ActionResult Create()
        {
            try
            {
                UsuarioModels usuario = new UsuarioModels();
                _Usuario_Datos UsuarioDatos = new _Usuario_Datos();
                usuario.conexion = Conexion;
                usuario.tablaTipoUsuariosCmb = UsuarioDatos.ObtenerComboTipoUsuario(usuario);
                var listUsuarios = new SelectList(usuario.tablaTipoUsuariosCmb, "id_tipoUsuario", "tipoUsuario");
                ViewData["cmbUsuarios"]=listUsuarios;  
                return View();
            }
            catch (Exception ex){
                throw ex;
            }
           
        }

        // POST: Admin/CatUsuarios/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                UsuarioModels usuario = new UsuarioModels();
                _Usuario_Datos UsuarioDatos = new _Usuario_Datos();
                usuario.conexion = Conexion;
                usuario.id_usuario = "";
                usuario.id_tipoUsuario = Convert.ToInt32(collection["tablaTipoUsuariosCmb"]);
                usuario.nombre = collection["nombre"];
                usuario.apPat = collection["apPat"];
                usuario.apMat = collection["apMat"];
                usuario.email = collection["email"];
                usuario.direccion = collection["direccion"];
                usuario.cuenta = collection["cuenta"];
                usuario.password = collection["password"];
                usuario.telefono = collection["telefono"];
                usuario.opcion = 1;
                usuario.user= User.Identity.Name;
                usuario = UsuarioDatos.AbcCatUsuarios(usuario);
                if (usuario.Completado == true)
                {
                    TempData["typemessage"] = "1";
                    TempData["message"] = "Los datos se guardaron correctamente.";
                    return RedirectToAction("Index");
                }
                else
                {
                    usuario.tablaTipoUsuariosCmb = UsuarioDatos.ObtenerComboTipoUsuario(usuario);
                    var listUsuarios = new SelectList(usuario.tablaTipoUsuariosCmb, "id_tipoUsuario", "tipoUsuario");
                    ViewData["cmbUsuarios"] = listUsuarios;
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Ocurrió un error al intentar guardar.";
                    return RedirectToAction("Create", "CatUsuarios");
                }
                
            }
            catch
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrió un error el intentar guardar. Contacte a soporte técnico";
                return View();
            }
        }

        // GET: Admin/CatUsuarios/Edit/5
        public ActionResult Edit(string id)
        {
            UsuarioModels usuario = new UsuarioModels();
            _Usuario_Datos UsuarioDatos = new _Usuario_Datos();
            usuario.conexion = Conexion;
            usuario.id_usuario = id;
            usuario.tablaTipoUsuariosCmb = UsuarioDatos.ObtenerComboTipoUsuario(usuario);
            var listUsuarios = new SelectList(usuario.tablaTipoUsuariosCmb, "id_tipoUsuario", "tipoUsuario");
            ViewData["cmbUsuarios"] = listUsuarios;
            usuario = UsuarioDatos.ObtenerDetalleUsuarioxID(usuario);
            return View(usuario);
        }

        // POST: Admin/CatUsuarios/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, FormCollection collection)
        {
            try
            {
                UsuarioModels usuario = new UsuarioModels();
                _Usuario_Datos UsuarioDatos = new _Usuario_Datos();
                usuario.conexion = Conexion;
                usuario.id_usuario = id;
                usuario.id_tipoUsuario = Convert.ToInt32(collection["tablaTipoUsuariosCmb"]);
                usuario.nombre = collection["nombre"];
                usuario.apPat = collection["apPat"];
                usuario.apMat = collection["apMat"];
                usuario.email = collection["email"];
                usuario.direccion = collection["direccion"];
               
                usuario.telefono = collection["telefono"];
                usuario.opcion = 2;
                usuario.user = User.Identity.Name;
                usuario = UsuarioDatos.AbcCatUsuarios(usuario);
                if (usuario.Completado == true)
                {
                    TempData["typemessage"] = "1";
                    TempData["message"] = "Los datos se guardaron correctamente.";
                    return RedirectToAction("Index");
                }
                else
                {
                    usuario.tablaTipoUsuariosCmb = UsuarioDatos.ObtenerComboTipoUsuario(usuario);
                    var listUsuarios = new SelectList(usuario.tablaTipoUsuariosCmb, "id_tipoUsuario", "tipoUsuario");
                    ViewData["cmbUsuarios"] = listUsuarios;
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Ocurrió un error al intentar guardar.";
                    return RedirectToAction("Edit", "CatUsuarios");
                }

            }
            catch
            {

                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrió un error el intentar guardar. Contacte a soporte técnico";
                return View();
            }
        }

        // GET: Admin/CatUsuarios/Delete/5
        public ActionResult Delete(string id)
        {
            return View();
        }

        // POST: Admin/CatUsuarios/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                UsuarioModels usuario = new UsuarioModels();
                _Usuario_Datos UsuarioDatos = new _Usuario_Datos();
                usuario.conexion = Conexion;
                usuario.id_usuario = id;
                
                usuario.opcion = 3;
                usuario.user = User.Identity.Name;
                usuario = UsuarioDatos.AbcCatUsuarios(usuario);
                if (usuario.Completado == true)
                {
                    TempData["typemessage"] = "1";
                    TempData["message"] = "El registro se elimino correctamente.";
                    return RedirectToAction("Index");
                }
                else
                {
                  
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Ocurrió un error al intentar guardar.";
                    return RedirectToAction("Index");
                }

            }
            catch
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrió un error el intentar guardar. Contacte a soporte técnico";
                return View();
            }
        }
    }
}
