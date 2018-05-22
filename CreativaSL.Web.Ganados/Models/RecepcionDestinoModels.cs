using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class RecepcionDestinoModels
    {
        public string IDFlete { get; set; }

        [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
        public TimeSpan HoraLlegada { get; set; }

        [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
        public TimeSpan HoraDescarga { get; set; }

        public bool RecepcionDocumentos { get; set; }
        public bool ValijaSellada { get; set; }
        public string Conexion { get; set; }
        public string Usuario { get; set; }
        public RespuestaAjax RespuestaAjax { get; set; }

        private string _id_recepcion;
        public string id_recepcion
        {
            get { return _id_recepcion; }
            set { _id_recepcion = value; }
        }

        private decimal _kiloTotalRecibido;
        public decimal kiloTotalRecibido
        {
            get { return _kiloTotalRecibido; }
            set { _kiloTotalRecibido = value; }
        }

        private int _GanadosTotal;
        public int GanadosTotal
        {
            get { return _GanadosTotal; }
            set { _GanadosTotal = value; }
        }

        private int _GanadosMachos;
        public int GanadosMachos
        {
            get { return _GanadosMachos; }
            set { _GanadosMachos = value; }
        }

        private int _GanadosHembras;
        public int GanadosHembras
        {
            get { return _GanadosHembras; }
            set { _GanadosHembras = value; }
        }

        private DateTime _fechaLlegada;
        public DateTime fechaLlegada
        {
            get { return _fechaLlegada; }
            set { _fechaLlegada = value; }
        }

        private string _recibidoPor;
        public string recibidoPor
        {
            get { return _recibidoPor; }
            set { _recibidoPor = value; }
        }
        private string _observacion;

        public string observacion
        {
            get { return _observacion; }
            set { _observacion = value; }
        }

        public void Inicializar()
        {
            observacion = string.Empty;
            recibidoPor = string.Empty;
            fechaLlegada = DateTime.Now;
            GanadosHembras = 0;
            GanadosMachos = 0;
            GanadosTotal = 0;
            kiloTotalRecibido = 0;
            id_recepcion = string.Empty;
            IDFlete = string.Empty;
            HoraLlegada = DateTime.Now.TimeOfDay;
            HoraDescarga = DateTime.Now.TimeOfDay;
            RecepcionDocumentos = false;
            ValijaSellada = false;
        }

    }
}