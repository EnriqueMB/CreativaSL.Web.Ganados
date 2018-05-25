using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class RptProveedorMermaAltaModels
    {
        public RptProveedorMermaAltaModels() {
            _nombreProveedor = string.Empty;
            _merma = 0;
            _mermaTotal = 0;
            _listaRptProveedorMerma = new List<RptProveedorMermaAltaModels>();
            Conexion = string.Empty;
            Resultado = 0;
            Completado = false;
            Opcion = 0;
            Usuario = string.Empty;
        }
        private decimal _toleranciaCompra;

        public decimal toleranciaCompra
        {
            get { return _toleranciaCompra; }
            set { _toleranciaCompra = value; }
        }

        private string _nombreProveedor;

        public string nombreProveedor
        {
            get { return _nombreProveedor; }
            set { _nombreProveedor = value; }
        }
        private int _merma;

        public int merma
        {
            get { return _merma; }
            set { _merma = value; }
        }
        private int _mermaTotal;

        public int mermaTotal
        {
            get { return _mermaTotal; }
            set { _mermaTotal = value; }
        }
        private List<RptProveedorMermaAltaModels> _listaRptProveedorMerma;

        public List<RptProveedorMermaAltaModels> listaRptProveedorMerma
        {
            get { return _listaRptProveedorMerma; }
            set { _listaRptProveedorMerma = value; }
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