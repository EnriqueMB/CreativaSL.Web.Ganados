using CreativaSL.Web.Ganados.App_Start;
using CreativaSL.Web.Ganados.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    public class CuentaUserController : Controller
    {
        private TokenProcessor Token = TokenProcessor.GetInstance();
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
        // GET: Admin/CuentaUser
        public ActionResult Index()
        {
            //return RedirectToAction("Index");
            return View();
        }

        // GET: Admin/CuentaUser/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/CuentaUser/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/CuentaUser/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/CuentaUser/Edit/5
        public ActionResult Edit(string id)
        {
            try
            {
                Token.SaveToken();
                UsuarioModels usuario = new UsuarioModels();
                _Usuario_Datos UsuarioDatos = new _Usuario_Datos();                
                usuario.conexion = Conexion;
                usuario.id_usuario = User.Identity.Name;   
                usuario = UsuarioDatos.ObtenerDetalleUsuarioxID(usuario);
                return View(usuario);               
            }
            catch (Exception ex)
            {
                throw ex;
            }          
        }
        // POST: Admin/CuentaUser/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, UsuarioModels usuario)
        {
            _Usuario_Datos UsuarioDatos = new _Usuario_Datos();
            try
            {
                usuario.conexion = Conexion;
                usuario.id_usuario = User.Identity.Name;
                UsuarioDatos.cCatCuentaUser(usuario);
                if (usuario.Completado == true)
                {
                    TempData["typemessage"] = "1";
                    TempData["message"] = "Los datos se guardaron correctamente, Su próximo inicio de sesión será con su nueva contraseña";
                    Token.ResetToken();
                    //System.Threading.Thread.Sleep(500);
                    return RedirectToAction("Edit");
                }
                else
                {
                    usuario.tablaTipoUsuariosCmb = UsuarioDatos.ObtenerComboTipoUsuario(usuario);
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Ocurrió un error al intentar guardar.";
                    return View(usuario);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET: Admin/CuentaUser/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/CuentaUser/Delete/5
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
    }
}
