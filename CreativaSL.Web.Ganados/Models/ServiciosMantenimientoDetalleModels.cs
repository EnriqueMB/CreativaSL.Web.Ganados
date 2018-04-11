using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class ServiciosMantenimientoDetalleModels
    {
        public ServiciosMantenimientoDetalleModels()
        {
            _IDServicio = string.Empty;
            _IDServicioDetalle = string.Empty;
            _Importe = 0;
            _Observaciones = string.Empty;
            _Encargado = string.Empty;
            _TipoServicio = new CatTipoServicioModels();
        }


        private string _IDServicio;

        public string IDServicio
        {
            get { return _IDServicio; }
            set { _IDServicio = value; }
        }

        private string _IDServicioDetalle;

        public string IDServicioDetalle
        {
            get { return _IDServicioDetalle; }
            set { _IDServicioDetalle = value; }
        }

        private decimal _Importe;

        public decimal Importe
        {
            get { return _Importe; }
            set { _Importe = value; }
        }

        public string ImporteFormat
        {
            get { return string.Format("{0:c}", _Importe); }
        }


        private string _Encargado;

        public string Encargado
        {
            get { return _Encargado; }
            set { _Encargado = value; }
        }

        private string _Observaciones;

        public string Observaciones
        {
            get { return _Observaciones; }
            set { _Observaciones = value; }
        }


        private CatTipoServicioModels _TipoServicio;
        /// <summary>
        /// Objeto que contiene los datos de la acción realizada
        /// </summary>
        public CatTipoServicioModels TipoServicio
        {
            get { return _TipoServicio; }
            set { _TipoServicio = value; }
        }





        #region Datos De Control
        public bool NuevoRegistro { get; set; }
        public string Conexion { get; set; }
        public int Resultado { get; set; }
        public bool Completado { get; set; }
        public string Usuario { get; set; }
        public int Opcion { get; set; }
        #endregion
    }
}