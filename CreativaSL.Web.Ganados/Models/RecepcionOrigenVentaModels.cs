using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class RecepcionOrigenVentaModels
    {
        public int Id_recepcionOrigenVenta { get; set; }
        public string Id_flete { get; set; }
        public string Id_venta { get; set; }
        public int KilometrajeFinal { get; set; }
        public TimeSpan HoraLlegada { get; set; }
        public DateTime FechaLlegada { get; set; }
        public string Observacion { get; set; }
    }
}