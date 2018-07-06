﻿using System;
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

        EventoVentaEnvioDetalleModels DetalleEventoVenta;
    }
}