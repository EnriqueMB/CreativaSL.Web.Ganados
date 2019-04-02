using CreativaSL.Web.Ganados.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

using CreativaSL.Web.Ganados.Filters;

using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using System.Web.Routing;

namespace CreativaSL.Web.Ganados.WebServices
{
    /// <summary>
    /// Descripción breve de DatatableServ
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]
    public class DatatableServ : System.Web.Services.WebService
    {
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");

        [WebMethod(Description = "Server Side DataTables support", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ComprasAlmacenAjax(object parameters)
        {
            var req = DataTableParameters.Get(parameters);
            var resultSet = new DataTableResultSet();

            int _Start = req.Start;
            int _Length = req.Length;
            string _SearchValue = req.SearchValue;
            int _OrderBy = -1;
            string _OrderDirection = string.Empty;
            int _TipoBusqueda = -1;
            if (req.Order.Count > 0)
            {
                foreach (var aux in req.Order.Keys)
                {
                   _OrderBy = req.Order[aux].Column;
                   _OrderDirection = req.Order[aux].Direction;
                }
            }

            foreach (var busq in req.Columns.Keys)
            {
                if (!string.IsNullOrEmpty(req.Columns[busq].SearchValue))
                {
                    _TipoBusqueda = busq;
                    _SearchValue = req.Columns[busq].SearchValue;
                    break;
                }
            }

            
            _CompraAlmacen_Datos Datos = new _CompraAlmacen_Datos();
            CompraAlmacenModels Resultado = Datos.ObtenerGridComprasAJAX(Conexion, _Start, _Length, _SearchValue, _OrderBy, _OrderDirection);
            resultSet.draw = req.Draw;
            resultSet.recordsTotal = Resultado.TotalRecords;
            resultSet.recordsFiltered = Resultado.SearchRecords;

            foreach (CompraAlmacenModels Item in Resultado.Lista)
            {
                var columns = new List<string>();
                columns.Add(Item.Sucursal.NombreSucursal);
                columns.Add(Item.NumFacturaNota);
                columns.Add(Item.Proveedor.nombreProveedor);
                string estatus = string.Empty;
                switch(Item.IDEstatusCompra)
                {
                    case 1: estatus = @"<td><span class='label label-warning'>" + Item.StatusCompra + @"</span></td>"; break;
                    case 2: estatus = @"<td><span class='label label-primary'>" + Item.StatusCompra  + @"</span></td>"; break;
                    case 3: estatus = @"<td><span class='label label-success'>" + Item.StatusCompra + @"</span></td>"; break;
                }
                columns.Add(estatus);
                columns.Add("<span class='pull-right'>" + Item.MontoTotalFormato + "</span>");
                string acciones = "<div class='visible-md visible-lg hidden-sm hidden-xs'>";
                UrlHelper urlHlp = new UrlHelper(HttpContext.Current.Request.RequestContext, RouteTable.Routes);                
                //string aux = urlHlp.RouteUrl("Admin_default", new { controller = "CompraAlmacen", action = "Detail", id = Item.IDCompraAlmacen, id2="0" });
                if (Item.IDEstatusCompra == 1)
                {
                    acciones += @"  <a title='Editar' href='" + urlHlp.RouteUrl("Admin_default", new { controller = "CompraAlmacen", action = "Edit", id = Item.IDCompraAlmacen }) + @"' class='btn btn-yellow tooltips' data-placement='top' data-original-title='Edit'><i class='fa fa-edit'></i></a>
                                    <a title='Detalles' href='" + urlHlp.RouteUrl("Admin_default", new { controller = "CompraAlmacen", action = "Detail", id = Item.IDCompraAlmacen}) + @"' class='btn btn-blue tooltips' data-placement='top' data-original-title='Detalles'><i class='fa fa-bars'></i></a>
                                    <a title='Procesar' class='btn btn-primary tooltips processRow' href='" + urlHlp.RouteUrl("Admin_default", new { controller = "CompraAlmacen", action = "Process", id = Item.IDCompraAlmacen}) + @"' data-placement='top' data-original-title='Procesar'><i class='fa fa-gear'></i></a>
                                    <a title='Cancelar' class='btn btn-danger tooltips deleteRow' id='delete-" + Item.IDCompraAlmacen + @"' data-hrefa='" + urlHlp.RouteUrl("Admin_default", new { controller = "CompraAlmacen", action = "Cancel", id = Item.IDCompraAlmacen }) + @"' data-placement='top' data-original-title='Cancelar'><i class='fa fa-times'></i></a>";

                }
                else
                {
                    acciones += @"  <label title='No se puede Editar' href='#' class='btn' style='background-color:gray;' data-placement='top' data-original-title='Edit'><i class='fa fa-edit' style='color:white'></i></label>
                                    <a title='Detalles' href='" + urlHlp.RouteUrl("Admin_default", new { controller = "CompraAlmacen", action = "Detail", id = Item.IDCompraAlmacen }) + @"' class='btn btn-blue tooltips' data-placement='top' data-original-title='Detalles'><i class='fa fa-bars'></i></a>
                                    <a title='Procesar' class='btn btn-primary tooltips processRow' href='" + urlHlp.RouteUrl("Admin_default", new { controller = "CompraAlmacen", action = "Process", id = Item.IDCompraAlmacen }) + @"' data-placement='top' data-original-title='Procesar'><i class='fa fa-gear'></i></a>
                                    <a title='Cancelar' class='btn btn-danger tooltips deleteRow' id='delete-" + Item.IDCompraAlmacen + @"' data-hrefa='" + urlHlp.RouteUrl("Admin_default", new { controller = "CompraAlmacen", action = "Cancel", id = Item.IDCompraAlmacen }) + @"' data-placement='top' data-original-title='Cancelar'><i class='fa fa-times'></i></a>";
                }
                acciones += @"  </div>
                                    <div class='visible-xs visible-sm hidden-md hidden-lg'>
                                        <div class='btn-group'>
                                            <a class='btn btn-danger dropdown-toggle btn-sm' data-toggle='dropdown' href='#'>
                                                <i class='fa fa-cog'></i> <span class='caret'></span>
                                            </a>
                                            <ul role='menu' class='dropdown-menu pull-right dropdown-dark'>";
                if (Item.IDEstatusCompra == 1)
                {
                    acciones += @"  <li>
                                        <a role='menuitem' tabindex='-1' href = '" + urlHlp.RouteUrl("Admin_default", new { controller = "CompraAlmacen", action = "Edit", id = Item.IDCompraAlmacen }) + @"'>
                                            <i class='fa fa-edit'></i>Editar
                                        </a>
                                    </li>
                                    <li>
                                        <a role='menuitem' tabindex='-1' href= '" + urlHlp.RouteUrl("Admin_default", new { controller = "CompraAlmacen", action = "Detail", id = Item.IDCompraAlmacen }) + @"'>
                                            <i class='fa fa-gears'></i> Detalles
                                        </a>
                                    </li>
                                    <li>
                                        <a role='menuitem' tabindex='-1' class='processRow' href='" + urlHlp.RouteUrl("Admin_default", new { controller = "CompraAlmacen", action = "Process", Item.IDCompraAlmacen }) + @"'>
                                            <i class='fa fa-caret-square-o-right'></i> Procesar
                                        </a>
                                    </li>
                                    <li>
                                        <a role='menuitem' tabindex='-1' class='deleteRow'  id='delete-" + Item.IDCompraAlmacen + @"' data-hrefa='" + urlHlp.RouteUrl("Admin_default", new { controller = "CompraAlmacen", action = "Delete", id = Item.IDCompraAlmacen }) + @"'>
                                            <i class='fa fa-trash-o'></i> Eliminar
                                        </a>
                                    </li>";
                }
                else
                {
                    acciones += @"  <li>
                                        <a role='menuitem' tabindex='-1' href='#'>
                                            <i class='fa fa-edit'></i> No se puede editar
                                        </a>
                                    </li>
                                    <li>
                                        <a role='menuitem' tabindex='-1' href= '" + urlHlp.RouteUrl("Admin_default", new { controller = "CompraAlmacen", action = "Detail", id = Item.IDCompraAlmacen }) + @"'>
                                            <i class='fa fa-gears'></i> Detalles
                                        </a>
                                    </li>
                                    <li>
                                        <a role='menuitem' tabindex='-1' class='processRow' href='" + urlHlp.RouteUrl("Admin_default", new { controller = "CompraAlmacen", action = "Process", Item.IDCompraAlmacen }) + @"'>
                                            <i class='fa fa-caret-square-o-right'></i> Procesar
                                        </a>
                                    </li>
                                    <li>
                                        <a role='menuitem' tabindex='-1' class='deleteRow' id='delete-" + Item.IDCompraAlmacen + @"' data-hrefa='" + urlHlp.RouteUrl("Admin_default", new { controller = "CompraAlmacen", action = "Delete", id = Item.IDCompraAlmacen }) + @"'>
                                            <i class='fa fa-trash-o'></i> Eliminar
                                        </a>
                                    </li>";
                }
                acciones += @"</ul></div></div>";                     
                columns.Add(acciones);                
                resultSet.data.Add(columns);
            }
            SendResponse(HttpContext.Current.Response, resultSet);
        }

        [WebMethod(Description = "Server Side DataTables support", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void CajaChicaIndex(object parameters)
        {
            var req = DataTableParameters.Get(parameters);
            var resultSet = new DataTableResultSet();
            _CajaChica_Datos Datos = new _CajaChica_Datos();
            CajaChicaModelsResult Resultado = Datos.ObtenerCajasChicasHistorial(req);
            resultSet.draw = req.Draw;
            resultSet.recordsTotal = Resultado.TotalRecords;
            resultSet.recordsFiltered = Resultado.SearchRecords;

            foreach (CajaChicaModels Item in Resultado.Lista)
            {
                var columns = new List<string>();
                columns.Add(Item.FechaAperturaString);
                columns.Add(Item.NombreEmpleado);
                columns.Add(Item.MontoAperturaString);
                //columns.Add(Item.PersonaEntrega);
                columns.Add(Item.SaldoString);
                columns.Add(Item.TotalArqueoString);
                columns.Add(Item.DiferenciaString);
                string acciones = @"";
                columns.Add(acciones);
                resultSet.data.Add(columns);
            }
            SendResponse(HttpContext.Current.Response, resultSet);

        }

        private void SendResponse(HttpResponse response, DataTableResultSet result)
        {
            response.Clear();
            response.Headers.Add("X-Content-Type-Options", "nosniff");
            response.Headers.Add("X-Frame-Options", "SAMEORIGIN");
            response.ContentType = "application/json; charset=utf-8";
            response.Write(result.ToJSON());
            response.Flush();
            response.End();
        }
    }
}
