using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CFDI_RegimenFiscalModels
    {
        public CFDI_RegimenFiscalModels()
        {
            _Clave = string.Empty;
            _Descripcion = string.Empty;
            _Fisica = false;
            _Moral = false;
            _FechaInicioVigencia = DateTime.Now;
            _FechaFinVigencia = DateTime.Now;
            _ListaRegimen = new List<CFDI_RegimenFiscalModels>();
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

        private bool _Fisica;

        public bool Fisica
        {
            get { return _Fisica; }
            set { _Fisica = value; }
        }

        private bool _Moral;

        public bool Moral
        {
            get { return _Moral; }
            set { _Moral = value; }
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

        private List<CFDI_RegimenFiscalModels> _ListaRegimen;

        public List<CFDI_RegimenFiscalModels> ListaRegimen
        {
            get { return _ListaRegimen; }
            set { _ListaRegimen = value; }
        }
        private string _Fechafin;

        public string FechaFin
        {
            get { return _Fechafin; }
            set { _Fechafin = value; }
        }

        private string _FechaInicio;

        public string FechaInicio
        {
            get { return _FechaInicio; }
            set { _FechaInicio = value; }
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