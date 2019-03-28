using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class EventoFleteModels
    {
        public int  Id_conceptoDocumento { get; set; }
        public int Id_eventoFlete { get; set; }
        public string Id_flete { get; set; }
        public string Id_documentoPorCobrarDetalle { get; set; }
        public int Id_tipoEvento { get; set; }
        public int Cantidad { get; set; }
        public string Lugar { get; set; }
        public DateTime FechaDeteccion { get; set; }
        public TimeSpan HoraDeteccion { get; set; }
        public string Observacion { get; set; }
        /// <summary>
        /// Imagen del server
        /// </summary>
        public string ImagenBase64 { get; set; }
        /// <summary>
        /// Imagen a mostrar, puede ser que sea del server pero en caso que no tenga se pone el default
        /// </summary>
        public string ImagenMostrar { get; set; }
        /// <summary>
        /// Extension de la imagen base 64
        /// </summary>
        public string ExtensionImagenBase64 { get; set; }
        /// <summary>
        /// Imagen del input type: file
        /// </summary>
        public HttpPostedFileBase HttpImagen { get; set; }

        public List<CatTipoEventoEnvioModels> ListaTiposEventos;

        public decimal MontoDeduccion { get; set; }

        public bool AplicaGanado { get; set; }
        public bool AplicaDeduccion { get; set; }

        public EventoVentaEnvioDetalleModels DetalleEventoVenta;
        public List<CatTipoClasificacionCobroModels> ListaDeTiposDeduccion;
        public int Id_TipoDeDeduccion { get; set; }
        public string ListaIDGanadosDelEvento { get; set; }

        public string Conexion { get; set; }
        public string Usuario { get; set; }
        public RespuestaAjax RespuestaAjax { get; set; }


    }
}