using CreativaSL.Web.Ganados.App_Start;
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
    
    public class TicketController : Controller
    {
        private TokenProcessor Token = TokenProcessor.GetInstance();
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
        // GET: Admin/Ticket
        public ActionResult Index()
        {
            try
            {
                ConfiguracionModels ticket = new ConfiguracionModels();
                _Configuracion_Datos datos = new _Configuracion_Datos();
                ticket.Conexion = Conexion;
                ticket.listaTicket = datos.ObtenerListaTicket(ticket);
                return View(ticket);
            }
            catch (Exception ex)
            {
                ConfiguracionModels ticket = new ConfiguracionModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(ticket);
            }
        }
        public ActionResult Edit(int id)
        {
            try
            {
                Token.SaveToken();
                ConfiguracionModels ticket = new ConfiguracionModels();
                _Configuracion_Datos datos = new _Configuracion_Datos();
                ticket.Conexion = Conexion;
                ticket.idTicket = id;
                ticket = datos.ObtenerTicket(ticket);

                return View(ticket);
            }
            catch (Exception ex)
            {
                ConfiguracionModels ticket = new ConfiguracionModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(ticket);
            }
        }
        [HttpPost]
        public ActionResult Edit(int id, ConfiguracionModels ticket)
        {
            try
            {
                if(Token.IsTokenValid())
                {
                    if(ModelState.IsValid)
                    {
                        _Configuracion_Datos datos = new _Configuracion_Datos();
                        ticket.Conexion = Conexion;
                        ticket.idTicket = id;
                        ticket.Usuario = User.Identity.Name;
                        ticket.textoTicket1 = "Uno";
                        ticket.textoTicket2 = "Dos";
                        ticket.textoTicket3 = "Tres";
                        ticket = datos.C_Configuracion(ticket);
                        if (ticket.Completado == true)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "Los datos se guardarón correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrio un error al intentar guardar los datos. Intente más tarde.";
                            return View(ticket);
                        }
                    }
                    else
                    {
                        return View(ticket);
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error al intentar guardar los datos. Contacte a soporte técnico.";
                return View(ticket);
            }
        }
    }
}