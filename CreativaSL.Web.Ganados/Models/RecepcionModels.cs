using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class RecepcionModels
    {
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
        private DateTime _fechaHoraLlegada;

        public DateTime fechaHoraLlegada
        {
            get { return _fechaHoraLlegada; }
            set { _fechaHoraLlegada = value; }
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

    }
}