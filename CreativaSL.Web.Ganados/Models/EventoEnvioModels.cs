using System;
using System.Collections.Generic;
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

        private string _Lugar;

        public string Lugar
        {
            get { return _Lugar; }
            set { _Lugar = value; }
        }

        private TimeSpan _HoraDeteccion;

        public TimeSpan HoraDetecccion
        {
            get { return _HoraDeteccion; }
            set { _HoraDeteccion = value; }
        }
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

        public EventoEnvioDetalleModels DetalleEvento { get; set; }
        public CatTipoEventoEnvioModels TipoEvento { get; set; }
        public List<CatTipoEventoEnvioModels> ListaEventos { get; set; }
        public string ImagenBase64 { get; set; }
        public string ExtensionImagenBase64 { get; set; }
        public string ImagenMostrar { get; set; }
        public HttpPostedFile HttpImagen { get; set; }


        #region Datos De Control
        public string Conexion { get; set; }
        public int Resultado { get; set; }
        public bool Completado { get; set; }
        public string Usuario { get; set; }
        public int Opcion { get; set; }
        #endregion
    }
}