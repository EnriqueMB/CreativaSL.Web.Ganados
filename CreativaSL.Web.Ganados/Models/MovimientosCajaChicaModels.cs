using CreativaSL.Web.Ganados.Models.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class MovimientosCajaChicaModels
    {
        public MovimientosCajaChicaModels()
        {
            FolioCheque = string.Empty;
            Concepto = string.Empty;
        }

        public Int64 IDTipoMovimiento { get; set; }
        public Int64 IdCaja { get; set; }
        public Int64 IdMovimiento { get; set; }
        public DateTime Fecha { get; set; }
        public string Entrega { get; set; }
        public decimal Entrada { get; set; }
        [Required(ErrorMessage = "Ingrese el monto.")]
        [Display(Name = "Monto.")]
        [DecimalMayor0(ErrorMessage = "Ingrese un dato válido")]
        public decimal Salida { get; set; }
        public string Recibe { get; set; }

        [Required(ErrorMessage = "Seleccione una categoría.")]
        [Display(Name = "Categoría")]
        public int IdCategoria { get; set; }
        public string Categoria { get; set; }
        [Required(ErrorMessage = "Ingrese el concepto.")]
        [Display(Name = "Categoría")]
        public string Concepto { get; set; }
        [Required(ErrorMessage = "Seleccione una forma de pago.")]
        [Display(Name = "Forma de pago")]
        public int IdFormaPago { get; set; }
        public string FormaPago { get; set; }
        public decimal Saldo { get; set; }

        [Required(ErrorMessage = "Ingrese el folio.")]
        [Display(Name = "Folio")]
        public string FolioCheque { get; set; }
        public string Alias { get; set; }
        public string FotoCheque { get; set; }
        public bool Estatus { get; set; }

        public string FechaString { get { return string.Format("{0:dd/MM/yyyy}", Fecha); } }
        public string SaldoString { get { return string.Format("{0:c}", Saldo); } }
        public string EntradaString { get { return string.Format("{0:c}", Entrada); } }
        public string SalidaString { get { return string.Format("{0:c}", Salida); } }
    }
}