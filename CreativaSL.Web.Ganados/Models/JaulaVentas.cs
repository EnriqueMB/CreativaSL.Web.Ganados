using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class JaulaVentas
    {
        public string Id_venta { get; set; }
        public string Folio { get; set; }
        public string Sucursal { get; set; }
        public int CantTotal { get; set; }
        public decimal PesoTotal { get; set; }
        public int CbzMacho { get; set; }
        public int CbzHembra { get; set; }
        public decimal KgMacho { get; set; }
        public decimal KgHembra { get; set; }
        public decimal ImporteMacho { get; set; }
        public decimal ImporteHembra { get; set; }
        public decimal ImporteTotal { get; set; }
        public decimal Percepcion { get; set; }
        public decimal Deduccion { get; set; }
        public decimal CostoExtra { get; set; }
        public decimal Pago { get; set; }
        public decimal Pendiente { get; set; }
        public decimal Total { get; set; }

    }
}