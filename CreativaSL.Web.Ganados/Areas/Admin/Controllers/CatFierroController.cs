using CreativaSL.Web.Ganados.Filters;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CreativaSL.Web.Ganados.Models;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using CreativaSL.Web.Ganados.App_Start;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    [Autorizado]
    public class CatFierroController : Controller
    {
        private TokenProcessor Token = TokenProcessor.GetInstance();
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");

        public ActionResult DatatableIndex()
        {
            try
            {
                CatFierroModels fierro = new CatFierroModels();
                CatFierro_Datos fierroDatos = new CatFierro_Datos();

                fierro.Conexion = Conexion;
                fierro.RespuestaAjax = new RespuestaAjax();
                fierro.RespuestaAjax.Mensaje = fierroDatos.DatatableIndex(fierro);
                fierro.RespuestaAjax.Success = true;

                return Content(fierro.RespuestaAjax.Mensaje, "application/json");

            }
            catch (Exception ex)
            {
                CatFierroModels fierro = new CatFierroModels();
                fierro.RespuestaAjax = new RespuestaAjax();
                fierro.RespuestaAjax.Mensaje = ex.ToString();
                fierro.RespuestaAjax.Success = false;
                return Content(fierro.RespuestaAjax.ToJSON(), "application/json");
            }
        }

        // GET: Admin/CatFierro
        public ActionResult Index()
        {
            try
            {
                return View();
            }
            catch (Exception)
            {
                CatFierroModels Fierro = new CatFierroModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Fierro);
            }
        }

        // GET: Admin/CatFierro/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/CatFierro/Create
        [HttpGet]
        public ActionResult Create()
        {
            try
            {
                Token.SaveToken();
                CatFierroModels Fierro = new CatFierroModels();
                return View(Fierro);
            }
            catch (Exception)
            {
                CatFierroModels Fierro = new CatFierroModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Fierro);
            }
        }

       // POST: Admin/CatFierro/Create
       [HttpPost]
        public ActionResult Create(CatFierroModels Fierro)
        {
            try
            {
                if (Token.IsTokenValid())
                {
                    CatFierro_Datos FierroDatos = new CatFierro_Datos();
                    Fierro.Conexion = Conexion;
                    Fierro.Opcion = 1;
                    Fierro.Usuario = User.Identity.Name;
                    string[] tmp = Fierro.ImgFierro.Split(',');
                    Fierro.ImgFierro = tmp[1];

                    Fierro = FierroDatos.AbcCatFierro(Fierro);
                    if (Fierro.Completado == true)
                    {
                        TempData["typemessage"] = "1";
                        TempData["message"] = "Los datos se guardarón correctamente.";
                        Token.ResetToken();
                        Fierro.RespuestaAjax = new RespuestaAjax();
                        Fierro.RespuestaAjax.Success = true;

                        return Content(Fierro.RespuestaAjax.ToJSON(), "application/json");
                    }
                    else
                    {
                        TempData["typemessage"] = "2";
                        TempData["message"] = "Ocurrio un error al intentar guardar los datos. Intente más tarde.";
                        Fierro.RespuestaAjax = new RespuestaAjax();
                        Fierro.RespuestaAjax.Success = false;

                        return Content(Fierro.RespuestaAjax.ToJSON(), "application/json");
                    }
                }
                else
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Verifique sus datos";

                    Fierro.RespuestaAjax = new RespuestaAjax();
                    Fierro.RespuestaAjax.Success = false;

                    return Content(Fierro.RespuestaAjax.Mensaje, "application/json");
                }
                return View(Fierro);
            }
            catch (Exception ex)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error al intentar guardar los datos. Contacte a soporte técnico.";
                Fierro.RespuestaAjax = new RespuestaAjax();
                Fierro.RespuestaAjax.Success = false;

                return Content(Fierro.RespuestaAjax.Mensaje, "application/json");
            }
        }

        // GET: Admin/CatFierro/Edit/5
        [HttpGet]
        public ActionResult Edit(string IDFierro)
        {
            try
            {
                Token.SaveToken();
                CatFierroModels Fierro = new CatFierroModels();
                CatFierro_Datos FierroDatos = new CatFierro_Datos();
                Fierro.IDFierro = IDFierro;
                Fierro.Conexion = Conexion;
                Fierro = FierroDatos.ObtenerDetalleCatFierro(Fierro);
                return View(Fierro);
            }
            catch (Exception)
            {
                CatFierroModels Fierro = new CatFierroModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Fierro);
            }
        }

        // POST: Admin/CatFierro/Edit/5
        [HttpPost]
        public ActionResult Edit(CatFierroModels Fierro)
        {
            try
            {
                if(Token.IsTokenValid())
                {
                    CatFierro_Datos FierroDatos = new CatFierro_Datos();
                    Fierro.Conexion = Conexion;
                    Fierro.Opcion = 2;
                    Fierro.Usuario = User.Identity.Name;
                    string[] tmp = Fierro.ImgFierro.Split(',');
                    Fierro.ImgFierro = tmp[1];
                    Fierro = FierroDatos.AbcCatFierro(Fierro);

                    if (Fierro.Completado == true)
                    {
                        TempData["typemessage"] = "1";
                        TempData["message"] = "Los datos se guardarón correctamente.";
                        Token.ResetToken();
                        Fierro.RespuestaAjax = new RespuestaAjax();
                        Fierro.RespuestaAjax.Success = true;

                        return Content(Fierro.RespuestaAjax.ToJSON(), "application/json");
                    }
                    else
                    {
                        TempData["typemessage"] = "2";
                        TempData["message"] = "Ocurrio un error al intentar guardar los datos. Intente más tarde.";
                        Fierro.RespuestaAjax = new RespuestaAjax();
                        Fierro.RespuestaAjax.Success = false;

                        return Content(Fierro.RespuestaAjax.ToJSON(), "application/json");
                    }
                }
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Verifique sus datos";

                    Fierro.RespuestaAjax = new RespuestaAjax();
                    Fierro.RespuestaAjax.Success = false;

                    return Content(Fierro.RespuestaAjax.Mensaje, "application/json");
                }
            }
            catch (Exception ex)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error al intentar guardar los datos. Contacte a soporte técnico.";
                Fierro.RespuestaAjax = new RespuestaAjax();
                Fierro.RespuestaAjax.Success = false;

                return Content(Fierro.RespuestaAjax.Mensaje, "application/json");
            }
        }

        // GET: Admin/CatFierro/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/CatFierro/Delete/5
        [HttpPost]
        public ActionResult Delete(string IDFierro)
        {
            try
            {
                CatFierro_Datos FierroDatos = new CatFierro_Datos();
                CatFierroModels Fierro = new CatFierroModels();
                Fierro.Conexion = Conexion;
                Fierro.IDFierro = IDFierro;
                Fierro.Usuario = User.Identity.Name;

                Fierro = FierroDatos.EliminarFierro(Fierro);
                if (Fierro.Completado == true)
                {
                    TempData["typemessage"] = "1";
                    TempData["message"] = "El fierro se ha eliminado correctamente.";
                    Token.ResetToken();
                    Fierro.RespuestaAjax = new RespuestaAjax();
                    Fierro.RespuestaAjax.Success = true;

                    return Content(Fierro.RespuestaAjax.ToJSON(), "application/json");
                }
                else
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Ocurrio un error al intentar eliminar el fierro. Intente más tarde.";
                    Fierro.RespuestaAjax = new RespuestaAjax();
                    Fierro.RespuestaAjax.Success = false;

                    return Content(Fierro.RespuestaAjax.ToJSON(), "application/json");
                }
            }
            catch (Exception ex)
            {
                CatFierroModels Fierro = new CatFierroModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error al intentar guardar los datos. Contacte a soporte técnico.";
                Fierro.RespuestaAjax = new RespuestaAjax();
                Fierro.RespuestaAjax.Success = false;

                return Content(Fierro.RespuestaAjax.Mensaje, "application/json");
            }
        }
    }
}
