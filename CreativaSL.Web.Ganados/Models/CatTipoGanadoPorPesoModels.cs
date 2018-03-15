using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CatTipoGanadosPorPesoModels
    {
        private int _IDTipoGanadosPorPeso;

        public int IDTipoGanadosPorPeso
        {
            get { return _IDTipoGanadosPorPeso; }
            set { _IDTipoGanadosPorPeso = value; }
        }

        private string _Descripcion;

        public string Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }

        private decimal _PesoMinimo;

        public decimal PesoMinimo
        {
            get { return _PesoMinimo; }
            set { _PesoMinimo = value; }
        }

        private decimal _PesoMaximo;

        public decimal PesoMaximo
        {
            get { return _PesoMaximo; }
            set { _PesoMaximo = value; }
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