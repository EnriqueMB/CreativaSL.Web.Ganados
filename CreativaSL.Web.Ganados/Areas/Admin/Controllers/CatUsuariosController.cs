﻿using CreativaSL.Web.Ganados.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CreativaSL.Web.Ganados.Filters;
using CreativaSL.Web.Ganados.App_Start;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    [Autorizado]
    public class CatUsuariosController : Controller
    {
        private TokenProcessor Token = TokenProcessor.GetInstance();
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
            catch (Exception )
            {
                UsuarioModels usuario = new UsuarioModels();
               
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(usuario);
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
                Token.SaveToken();
                UsuarioModels usuario = new UsuarioModels();
                _Usuario_Datos UsuarioDatos = new _Usuario_Datos();
                usuario.conexion = Conexion;
                usuario.tablaTipoUsuariosCmb = UsuarioDatos.ObtenerComboTipoUsuario(usuario);
                return View(usuario);
            }
            catch (Exception ex)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("Index");
            }
        }

        // POST: Admin/CatUsuarios/Create
        [HttpPost]
        public ActionResult Create(UsuarioModels usuario)
        {
            _Usuario_Datos UsuarioDatos = new _Usuario_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        usuario.conexion = Conexion;
                        usuario.opcion = 1;
                        usuario.user = User.Identity.Name;
                        usuario = UsuarioDatos.AbcCatUsuarios(usuario);
                        if (usuario.Completado == true)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "Los datos se guardaron correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            usuario.tablaTipoUsuariosCmb = UsuarioDatos.ObtenerComboTipoUsuario(usuario);
                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrió un error al intentar guardar.";
                            return View(usuario);
                        }
                    }
                    else
                    {
                        usuario.conexion = Conexion;
                        usuario.tablaTipoUsuariosCmb = UsuarioDatos.ObtenerComboTipoUsuario(usuario);
                        return View(usuario);
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                usuario.conexion = Conexion;
                usuario.tablaTipoUsuariosCmb = UsuarioDatos.ObtenerComboTipoUsuario(usuario);
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrió un error el intentar guardar. Contacte a soporte técnico";
                return View(usuario);
            }
        }

        // GET: Admin/CatUsuarios/Edit/5
        public ActionResult Edit(string id)
        {
            
            try
            {
                Token.SaveToken();
                UsuarioModels usuario = new UsuarioModels();
                _Usuario_Datos UsuarioDatos = new _Usuario_Datos();
                usuario.conexion = Conexion;
                usuario.id_usuario = id;
                usuario.tablaTipoUsuariosCmb = UsuarioDatos.ObtenerComboTipoUsuario(usuario);
                usuario = UsuarioDatos.ObtenerDetalleUsuarioxID(usuario);
                return View(usuario);
            }
            catch (Exception)
            {
                UsuarioModels usuario = new UsuarioModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(usuario);
            }
        }

        // POST: Admin/CatUsuarios/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, UsuarioModels usuario)
        {
            _Usuario_Datos UsuarioDatos = new _Usuario_Datos();
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        usuario.conexion = Conexion;
                        usuario.opcion = 2;
                        usuario.user = User.Identity.Name;
                        usuario = UsuarioDatos.AbcCatUsuarios(usuario);
                        if (usuario.Completado == true)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "Los datos se guardaron correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            usuario.tablaTipoUsuariosCmb = UsuarioDatos.ObtenerComboTipoUsuario(usuario);
                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrió un error al intentar guardar.";
                            return View(usuario);
                        }
                    }
                    else
                    {
                        usuario.conexion = Conexion;
                        usuario.tablaTipoUsuariosCmb = UsuarioDatos.ObtenerComboTipoUsuario(usuario);
                        return View(usuario);
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch
            {

                usuario.conexion = Conexion;
                usuario.tablaTipoUsuariosCmb = UsuarioDatos.ObtenerComboTipoUsuario(usuario);
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrió un error el intentar guardar. Contacte a soporte técnico";
                return View(usuario);
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
                usuario = UsuarioDatos.EliminarUsuario(usuario);
                TempData["typemessage"] = "1";
                TempData["message"] = "El registro se elimino correctamente.";
                return Json("");
            }
            catch
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrió un error el intentar guardar. Contacte a soporte técnico";
                return View();
            }
        }
        //PERMISO
        public ActionResult Permisos(string id)
        {
            try
            {
                Token.SaveToken();
                UsuarioModels usuario = new UsuarioModels();
                _Usuario_Datos UsuarioDatos = new _Usuario_Datos();
                usuario.conexion = Conexion;
                usuario.id_usuario = id;
                usuario.id_tipoUsuario = 1;
                usuario.listaMenu = UsuarioDatos.ObtenerListaPermisosUsuario(usuario);
                if (usuario.ListaPermisos != null)
                {
                    usuario.numeroMenu = usuario.ListaPermisos.Count;
                }
                else
                {
                    usuario.numeroMenu = 0;
                }
                return View(usuario);
            }
            catch (Exception)
            {
                UsuarioModels usuario = new UsuarioModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Permisos(string id, UsuarioModels userID, FormCollection collection)
        {
            try
            {
                if (Token.IsTokenValid())
                {
                    UsuarioModels usuario = new UsuarioModels();
                    _Usuario_Datos UsuarioDatos = new _Usuario_Datos();
                    usuario.numeroMenu = Convert.ToInt32(collection["Total"]);
                    usuario.opcion = 1;
                    usuario.user = User.Identity.Name;
                    usuario.conexion = Conexion;
                    usuario.id_usuario = collection["id_usuario"];
                    usuario.TablaPermisos = new DataTable();
                    usuario.TablaPermisos.Columns.Add("IDPermiso", typeof(string));
                    usuario.TablaPermisos.Columns.Add("Ver", typeof(bool));
                    usuario.TablaPermisos.Columns.Add("MenuID", typeof(int));
                    foreach (MenuModels Item in userID.ListaMenuPermisos)
                    {
                        object[] data = { Item.IDPermiso, Item.ver, Item.MenuID };
                        usuario.TablaPermisos.Rows.Add(data);
                    }
                    if (UsuarioDatos.GuardarPermisos(usuario) == 1)
                    {
                        TempData["typemessage"] = "1";
                        TempData["message"] = "Los permisos se guardaron correctamente.";
                        Token.ResetToken();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["typemessage"] = "2";
                        TempData["message"] = "Los permisos no se guardaron correctamente. Intente más tarde.";
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Los permisos no se guardaron correctamente. Contacte a soporte técnico.";
                return RedirectToAction("Index");
            }
        }
    }
}
