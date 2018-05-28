using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CompraGanadosModels
    {
        public CompraGanadosModels()
        {
            IDCompra = string.Empty;
            IDGanados = string.Empty;
            DiferenciaPeso = 0;
            Merma = 0;
            PesoInicial = 0;
            PesoFinal = 0;
            PesoPagado = 0;
            PesoSugerido = 0;
            PrecioKilo = 0;
            PrecioSugeridoXkilo = 0;
            Repeso = true;
            TotalPagado = 0;
            TotalSugerido = 0;
        }
        public string Id_detalleDocumentoPorCobrar { get; set; }
        public decimal TotalPagado { get; set; }
        public bool Repeso { get; set; }
        public decimal DiferenciaPeso { get; set; }
        public decimal PesoSugerido { get; set; }
        public decimal PrecioSugeridoXkilo { get; set; }
        public decimal TotalSugerido { get; set; }

        private string _IDCompra;
        private string _IDGanados;
        private decimal _Merma;
        private decimal _PesoInicial;
        private decimal _PesoFinal;
        private decimal _PesoPagado;
        private decimal _PrecioKilo;

        public string IDCompra
        {
            get { return _IDCompra; }
            set { _IDCompra = value; }
        }
        public string IDGanados
        {
            get { return _IDGanados; }
            set { _IDGanados = value; }
        }
        public decimal Merma
        {
            get { return _Merma; }
            set { _Merma = value; }
        }
        public decimal PesoInicial
        {
            get { return _PesoInicial; }
            set { _PesoInicial = value; }
        }
        public decimal PesoFinal
        {
            get { return _PesoFinal; }
            set { _PesoFinal = value; }
        }
        public decimal PesoPagado
        {
            get { return _PesoPagado; }
            set { _PesoPagado = value; }
        }
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