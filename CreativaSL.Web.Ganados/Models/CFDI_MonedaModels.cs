using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CFDI_MonedaModels
    {
        public CFDI_MonedaModels()
        {
            _Clave = string.Empty;
            _Descripcion = string.Empty;
            _FechaInicioVigencia = DateTime.Now;
            _FechaFinVigencia = DateTime.Now;
            Conexion = string.Empty;
            Usuario = string.Empty;
        }
        private string _Clave;

        public string Clave
        {
            get { return _Clave; }
            set { _Clave = value; }
        }

        private string _Descripcion;

        public string Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }

        private decimal _Decimales;

        public decimal Decimales
        {
            get { return _Decimales; }
            set { _Decimales = value; }
        }

        private decimal _PorcentajeVariacion;

        public decimal PorcentajeVariacion
        {
            get { return _PorcentajeVariacion; }
            set { _PorcentajeVariacion = value; }
        }

        private DateTime _FechaInicioVigencia;

        public DateTime FechaInicioVigencia
        {
            get { return _FechaInicioVigencia; }
            set { _FechaInicioVigencia = value; }
        }

        private DateTime _FechaFinVigencia;

        public DateTime FechaFinVigencia
        {
            get { return _FechaFinVigencia; }
            set { _FechaFinVigencia = value; }
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