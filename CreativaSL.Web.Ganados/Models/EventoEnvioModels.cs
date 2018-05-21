using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class EventoEnvioModels
    {
        private int _IDEvento;

        public int IDEvento
        {
            get { return _IDEvento; }
            set { _IDEvento = value; }
        }

        private string _IDEnvio;

        public string IDEnvio
        {
            get { return _IDEnvio; }
            set { _IDEnvio = value; }
        }

        [Required(ErrorMessage = "Seleccione un tipo de evento")]
        private int _IDTipoEvento;
        public int IDTipoEvento
        {
            get { return _IDTipoEvento; }
            set { _IDTipoEvento = value; }
        }

        private int _Cantidad;
        public int Cantidad
        {
            get { return _Cantidad; }
            set { _Cantidad = value; }
        }

        [Required(ErrorMessage = "Seleccione un lugar")]
        private string _Lugar;
        public string Lugar
        {
            get { return _Lugar; }
            set { _Lugar = value; }
        }

        [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Seleccione una hora")]
        private TimeSpan _HoraDeteccion;
        public TimeSpan HoraDetecccion
        {
            get { return _HoraDeteccion; }
            set { _HoraDeteccion = value; }
        }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Fecha de Deteccion")]
        [Required(ErrorMessage = "Seleccione una fecha")]
        private DateTime _FechaDeteccion;
        public DateTime FechaDeteccion
        {
            get { return _FechaDeteccion; }
            set { _FechaDeteccion = value; }
        }


        private string _Observacion;

        public string Observacion
        {
            get { return _Observacion; }
            set { _Observacion = value; }
        }
        public RespuestaAjax RespuestaAjax { get; set; }
        public EventoEnvioDetalleModels DetalleEvento { get; set; }
        public CatTipoEventoEnvioModels TipoEvento { get; set; }
        public List<CatTipoEventoEnvioModels> ListaEventos { get; set; }
        public string ImagenBase64 { get; set; }
        public string ExtensionImagenBase64 { get; set; }
        public string ImagenMostrar { get; set; }
        public HttpPostedFileBase HttpImagen { get; set; }
        public string ListaProductosEvento { get; set; }

        #region Datos De Control
        public string Conexion { get; set; }
        public int Resultado { get; set; }
        public bool Completado { get; set; }
        public string Usuario { get; set; }
        public int Opcion { get; set; }
        #endregion
    }
}