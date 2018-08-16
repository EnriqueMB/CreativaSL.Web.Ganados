using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class EventoCompraModels
    {
        public bool Success { get; set; }
        public int Id_eventoCompra { get; set; }
        public string Id_compra { get; set; }
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

        public EventoVentaEnvioDetalleModels DetalleEventoVenta;
        
        public string ListaIDGanadosDelEvento { get; set; }

        public string Conexion { get; set; }
        public string Usuario { get; set; }
        public RespuestaAjax RespuestaAjax { get; set; }
    }
}