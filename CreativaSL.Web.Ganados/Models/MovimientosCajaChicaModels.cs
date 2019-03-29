using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class MovimientosCajaChicaModels
    {
        public Int64 IdCaja { get; set; }
        public Int64 IdMovimiento { get; set; }
        public DateTime Fecha { get; set; }
        public string Entrega { get; set; }
        public decimal Entrada { get; set; }
        public decimal Salida { get; set; }
        public string Recibe { get; set; }
        public int IdCategoria { get; set; }
        public string Categoria { get; set; }
        public string Concepto { get; set; }
        
        public int IdFormaPago { get; set; }
        public string FormaPago { get; set; }
        public decimal Saldo { get; set; }

        public string FolioCheque { get; set; }

        public string FechaString { get { return string.Format("{0:dd/MM/yyyy}", Fecha); }}
        public string SaldoString { get { return string.Format("{0:c}", Saldo); } }
        public string EntradaString { get { return string.Format("{0:c}", Entrada); } }
        public string SalidaString { get { return string.Format("{0:c}", Salida); } }
    }
}