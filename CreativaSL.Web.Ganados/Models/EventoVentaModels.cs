using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class EventoVentaModels
    {
        public string Id_eventoVenta { get; set; }
        public string Id_venta { get; set; }
        public string Id_documentoPorCobrarDetalle { get; set; }
        public int Id_tipoEvento { get; set; }
        public int Cantidad { get; set; }
        public string Lugar { get; set; }
        public DateTime  FechaDeteccion { get; set; }
        public TimeSpan HoraDeteccion { get; set; }
        public string Observacion { get; set; }
        public string ImagenBase64 { get; set; }

        public List<CatTipoEventoEnvioModels> ListaTiposEventos;

        EventoVentaEnvioDetalleModels DetalleEventoVenta;
    }
}