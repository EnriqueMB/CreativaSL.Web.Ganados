using CreativaSL.Web.Ganados.Models.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CajaChicaModels
    {
        public string ImagenCajaChica { get; set; }

        public Int64 IdCaja { get; set; }
        public DateTime FechaApertura { get; set; }
        public string NombreEmpleado { get; set; }
        [Required(ErrorMessage = "Ingrese el monto de apertura.")]
        [Display(Name = "Monto de apertura")]
        [DecimalMayor0(ErrorMessage = "Ingrese un dato válido")]
        public decimal MontoApertura { get; set; }
        public decimal Saldo { get; set; }
        public string PersonaEntrega { get; set; }

        [Required(ErrorMessage = "Seleccione un propietario de la caja chica.")]
        [Display(Name = "Propietario")]
        public string IdPropietario { get; set; }
        public string IdPersonaEntrega { get; set; }
        [Required(ErrorMessage = "Ingrese la clave de aprobación.")]
        [Display(Name = "Clave")]
        public string KeyWord { get; set; }

        public string SaldoString
        {
            get { return string.Format("{0:c}", Saldo); }
        }
        public string MontoAperturaString
        {
            get { return string.Format("{0:c}", MontoApertura); }
        }
        public string FechaAperturaString { get { return string.Format("{0:dd/MM/yyyy}", FechaApertura); } }


        public decimal TotalArqueo { get; set; }
        public decimal Diferencia { get; set; }

        public string TotalArqueoString
        {
            get { return string.Format("{0:c}", TotalArqueo); }
        }

        public string DiferenciaString
        {
            get { return string.Format("{0:c}", Diferencia); }
        }

        public string Observacion { get; set; }
    }

    public class CajaChicaModelsResult
    {
        public List<CajaChicaModels> Lista { get; set; }
        public int TotalRecords { get; set; }
        public int SearchRecords { get; set; }
    }
}