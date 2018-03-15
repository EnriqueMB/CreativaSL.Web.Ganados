using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CFDI_ProductoServicioModels
    {
        public CFDI_ProductoServicioModels()
        {
            _Clave = string.Empty;
            _Descripcion = string.Empty;
            _FechaInicioVigencia = DateTime.Now;
            _FechaFinVigencia = DateTime.Now;
            _IDComplemento = string.Empty;
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

        private int _IncluirIEPSTrasladado;

        public int _IncluidIEPSTrasladado
        {
            get { return _IncluirIEPSTrasladado; }
            set { _IncluirIEPSTrasladado = value; }
        }

        private int _IncluirIVATrasladado;

        public int IncluirIVATrasladado
        {
            get { return _IncluirIVATrasladado; }
            set { _IncluirIVATrasladado = value; }
        }

        private string _IDComplemento;

        public string IDComplemento
        {
            get { return _IDComplemento; }
            set { _IDComplemento = value; }
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