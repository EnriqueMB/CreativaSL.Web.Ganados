using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CreativaSL.Web.Ganados.Models;

namespace CreativaSL.Web.Ganados.ViewModel.Venta
{
    public class CostosExtrasViewModel
    {
        public List<CostoExtra> ListaCostosExtras { get; set; }
        public decimal TotalVentaSinCostoExtra { get; set; }
        public decimal TotalCostoExtra { get; set; }

        public decimal TotalVentaConCostoExtra 
        { 
            get
            {
                return TotalVentaSinCostoExtra + TotalCostoExtra;
            }
        }

        public string IdVenta { get; set; }
    }
}