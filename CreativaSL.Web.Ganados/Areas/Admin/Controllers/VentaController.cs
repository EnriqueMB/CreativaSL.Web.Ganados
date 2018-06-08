using CreativaSL.Web.Ganados.App_Start;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using CreativaSL.Web.Ganados.Models;
using System.Configuration;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    public class VentaController : Controller
    {
        private TokenProcessor Token = TokenProcessor.GetInstance();
        private string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
        // GET: Admin/Venta
        public ActionResult Index()
        {
            return View();
        }

        #region Vista Ganado
        [HttpGet]
        public ActionResult Ganado()
        {
            try
            {
                Token.SaveToken();
                return View();
            }
            catch (Exception ex)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista, error: " + ex.ToString();
                throw;
            }
        }
        #endregion


        public ActionResult ExportToExcel() {

            _Venta2_Datos v2 = new _Venta2_Datos();
            FleteModels f = new FleteModels();
            f.Conexion = Conexion;
            f.id_flete = "82115E9B-AB50-47D9-966C-2F4CFF92AED6";


            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition",
             "attachment;filename=GridViewExport.xls");
            Response.Charset = "";

            Response.ContentType = "application/vnd.ms-excel";

            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridView GridView1 = new GridView();
            GridView1.DataSource = v2.GetDocumentosDataTable(f);
            GridView1.DataBind();
            GridView1.AllowPaging = false;
            GridView1.DataBind();

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow row = GridView1.Rows[i];

                //Apply text style to each Row
                row.Attributes.Add("class", "textmode");
            }

            GridView1.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode { mso-number-format:\@; } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

            return View();
            //{
            //    //var products = new System.Data.DataTable("teste");
            //    //products.Columns.Add("col1", typeof(int));
            //    //products.Columns.Add("col2", typeof(string));

            //    //products.Rows.Add(1, "product 1");
            //    //products.Rows.Add(2, "product 2");
            //    //products.Rows.Add(3, "product 3");
            //    //products.Rows.Add(4, "product 4");
            //    //products.Rows.Add(5, "product 5");
            //    //products.Rows.Add(6, "product 6");
            //    //products.Rows.Add(7, "product 7");

            //    _Venta2_Datos v2 = new _Venta2_Datos();
            //    FleteModels f = new FleteModels();
            //    f.Conexion = Conexion;
            //    f.id_flete = "82115E9B-AB50-47D9-966C-2F4CFF92AED6";
            //    // instantiate the GridView control from System.Web.UI.WebControls namespace
            //    // set the data source

            //    GridView gridview = new GridView();
            //    gridview.DataSource = v2.GetDocumentosDataTable(f);
            //    gridview.DataBind();

            //    // Clear all the content from the current response
            //    Response.ClearContent();
            //    Response.Buffer = true;
            //    // set the header
            //    Response.AddHeader("content-disposition", "attachment; filename = itfunda.xls");
            //    Response.ContentType = "application/ms-excel";
            //    Response.Charset = "";
            //    // create HtmlTextWriter object with StringWriter
            //    using (StringWriter sw = new StringWriter())
            //    {
            //        using (HtmlTextWriter htw = new HtmlTextWriter(sw))
            //        {
            //            // render the GridView to the HtmlTextWriter
            //            gridview.RenderControl(htw);
            //            // Output the GridView content saved into StringWriter
            //            Response.Output.Write(sw.ToString());
            //            Response.Flush();
            //            Response.End();
            //        }
            //    }
            //    return View();
        }

        #region Modal evento
        [HttpPost]
        public ActionResult ModalEvento()
        {
            return PartialView("ModalEvento");
        }
        #endregion
    }
}
