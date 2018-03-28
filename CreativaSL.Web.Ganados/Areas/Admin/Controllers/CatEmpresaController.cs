using System.Web.Mvc;
using CreativaSL.Web.Ganados.Models;
using System.Configuration;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    public class CatEmpresaController : Controller
    {
        private CatEmpresaModels Empresa;
        private _CatEmpresa_Datos EmpresaDatos;
        private string Conexion = ConfigurationManager.AppSettings.Get("strConnection");

        // GET: Admin/CatEmpresa
        public ActionResult Index()
        {
            Empresa = new CatEmpresaModels
            {
                Conexion = Conexion
            };
            EmpresaDatos = new _CatEmpresa_Datos();
            Empresa.ListaEmpresas = EmpresaDatos.GetListadoEmpresas(Empresa);
            return View(Empresa);
        }

        // GET: Admin/CatEmpresa/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/CatEmpresa/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/CatEmpresa/Create
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

        // GET: Admin/CatEmpresa/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/CatEmpresa/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/CatEmpresa/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/CatEmpresa/Delete/5
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
