using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CompraGanadosModels
    {
        private string _IDGanados;

        public string IDGanados
        {
            get { return _IDGanados; }
            set { _IDGanados = value; }
        }

        private decimal _PesoInicial;

        public decimal PesoInicial
        {
            get { return _PesoInicial; }
            set { _PesoInicial = value; }
        }

        private string _IDCompra;

        public string IDCompra
        {
            get { return _IDCompra; }
            set { _IDCompra = value; }
        }

        private string _PesoFinal;

        public string PesoFinal
        {
            get { return _PesoFinal; }
            set { _PesoFinal = value; }
        }

        private decimal _Merma;

        public decimal Merma
        {
            get { return _Merma; }
            set { _Merma = value; }
        }

        private string _PesoPagado;

        public string PesoPagado
        {
            get { return _PesoPagado; }
            set { _PesoPagado = value; }
        }

        private decimal _PrecioKilo;

        public decimal PrecioKilo
        {
            get { return _PrecioKilo; }
            set { _PrecioKilo = value; }
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