using System;
using System.Web.Mvc;
using CreativaSL.Web.Ganados.App_Start;
using CreativaSL.Web.Ganados.Filters;
using CreativaSL.Web.Ganados.Models;
using CreativaSL.Web.Ganados.Models.Datatable;
using CreativaSL.Web.Ganados.Models.Dto.CatTipoDocumentacionExtra;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    [Autorizado]
    public class CatTipoDocumentacionExtraController : Controller
    {
        private TokenProcessor Token = TokenProcessor.GetInstance();
        private _CatTipoDocumentacionExtra_Datos Datos;

        public CatTipoDocumentacionExtraController()
        {
            Datos = new _CatTipoDocumentacionExtra_Datos();
        }

        #region DataTable
        public ActionResult JsonIndex(DataTableAjaxPostModel dataTableAjaxPostModel)
        {
            try
            {
                var json  = Datos.JsonIndex(dataTableAjaxPostModel);

                return Content(json, "application/json");

            }
            catch (Exception ex)
            { 
                return Content(ex.ToString(), "application/json");
            }
        }
        #endregion
        
        #region Index
        [HttpGet]
        // GET: Admin/CatTipoDocumentacionExtra
        public ActionResult Index()
        {
            return View();
        } 
        #endregion

        #region Create

        [HttpGet]
        public ActionResult Create()
        {
            try
            {
                Token.SaveToken();
                var model = Datos.ObtenerTipoDocumentacionExtra(0);
                return View(model);
            }
            catch (Exception e)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Verifique sus datos o contacte con soporte técnico.";
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public ActionResult Create(CreateEditCatTipoDocumentacionExtraDto documentacionExtra)
        {
            try
            {
                if (Token.IsTokenValid() && ModelState.IsValid)
                {
                    var respuesta = Datos.GuardarTipoDocumentacionExtra(documentacionExtra);
                    TempData["typemessage"] = respuesta.Success ? "1" : "2";
                    TempData["message"] = respuesta.Mensaje;
                    if (respuesta.Success)
                    {
                        TempData["typemessage"] = "1";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["typemessage"] = "2";
                    }
                }
                else
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Verifique sus datos o contacte con soporte técnico.";
                    return RedirectToAction("Index");
                }

                return View(documentacionExtra);
            }
            catch (Exception e)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Verifique sus datos o contacte con soporte técnico.";
                return RedirectToAction("Index");
            }
        }
        #endregion

        #region Edit
        [HttpGet]
        public ActionResult Edit(int IdTipoDocumentacionExtra)
        {
            if (IdTipoDocumentacionExtra == 0)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Verifique sus datos o contacte con soporte técnico.";
                return RedirectToAction("Index");
            }

            try
            {
                Token.SaveToken();
                var model = Datos.ObtenerTipoDocumentacionExtra(IdTipoDocumentacionExtra);
                return View(model);
            }
            catch (Exception e)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Verifique sus datos o contacte con soporte técnico.";
                return RedirectToAction("Index");
            }

        }
        [HttpPost]
        public ActionResult Edit(CreateEditCatTipoDocumentacionExtraDto documentacionExtra)
        {
            try
            {
                if (Token.IsTokenValid() && ModelState.IsValid)
                {
                    var respuesta = Datos.GuardarTipoDocumentacionExtra(documentacionExtra);
                    TempData["typemessage"] = respuesta.Success ? "1" : "2";
                    TempData["message"] = respuesta.Mensaje;
                    if (respuesta.Success)
                    {
                        TempData["typemessage"] = "1";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["typemessage"] = "2";
                    }
                }
                else
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Verifique sus datos o contacte con soporte técnico.";
                    return RedirectToAction("Index");
                }

                return View(documentacionExtra);
            }
            catch (Exception e)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Verifique sus datos o contacte con soporte técnico.";
                return RedirectToAction("Index");
            }
        }
        #endregion

        #region Delete

        [HttpPost]
        public ActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Verifique sus datos o contacte con soporte técnico.";
            }
            else
            {
                var responseDb = Datos.EliminarTipoDocumentacionExtra(id.Value);
                if (responseDb.Success)
                {
                    TempData["typemessage"] = "1";
                }
                else
                {
                    TempData["typemessage"] = "2";
                }
                TempData["message"] = responseDb.Mensaje;
            }
            return Content("Eliminando tipo de documento", "application/json");
        }

        #endregion
    }
}