using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class EventoEnvioModels
    {
        private string _IDEvento;

        public string IDEvento
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

        private DateTime _FechaHoraDeteccion;

        public DateTime FechaHoraDetecccion
        {
            get { return _FechaHoraDeteccion; }
            set { _FechaHoraDeteccion = value; }
        }

        #region Datos De Control
        public string Conexion { get; set; }
        public int Resultado { get; set; }
        public bool Completado { get; set; }
        public string Usuario { get; set; }
        public int Opcion { get; set; }
        #endregion
    }
}