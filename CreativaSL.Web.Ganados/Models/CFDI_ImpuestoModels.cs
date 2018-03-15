using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CFDI_ImpuestoModels
    {
        public CFDI_ImpuestoModels()
        {
            _Clave = string.Empty;
            _Descripcion = string.Empty;
            _EntidadEnLaQueAplica = string.Empty;
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

        private bool _Retencion;

        public bool Retencion
        {
            get { return _Retencion; }
            set { _Retencion = value; }
        }

        private bool _Traslado;

        public bool Traslado
        {
            get { return _Traslado; }
            set { _Traslado = value; }
        }

        private bool _Federal;

        public bool Federal
        {
            get { return _Federal; }
            set { _Federal = value; }
        }

        private string _EntidadEnLaQueAplica;

        public string EntidadEnLaQueAplicas
        {
            get { return _EntidadEnLaQueAplica; }
            set { _EntidadEnLaQueAplica = value; }
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