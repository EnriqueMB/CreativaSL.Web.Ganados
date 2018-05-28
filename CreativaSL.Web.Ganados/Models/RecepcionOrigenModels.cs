using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class RecepcionOrigenModels
    {
        public string IDRecepcionOrigen { get; set; }
        public string IDFlete { get; set; }
        public RespuestaAjax RespuestaAjax { get; set; }

        [Required(ErrorMessage ="Ingrese un kilómetraje final")]
        public int KilometrajeFinal { get; set; }

        [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Seleccione una hora de detección")]
        private TimeSpan _HoraDeteccion;

        [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Seleccione una hora de llegada")]
        public TimeSpan HoraLlegada { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Seleccione una fecha")]
        public DateTime FechaLlegada { get; set; }

        public string Observacion { get; set; }

        public string Conexion { get; set; }
        public string Usuario { get; set; }

        public void Inicializar()
        {
            IDRecepcionOrigen = string.Empty;
            KilometrajeFinal = 0;
            HoraLlegada = DateTime.Now.TimeOfDay;
            FechaLlegada = DateTime.Now;
            Observacion = string.Empty;
        }
    }
}