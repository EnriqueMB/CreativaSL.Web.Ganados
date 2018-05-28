using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class GanadosModels
    {
        public GanadosModels()
        {
            _id_estatus_Ganados = string.Empty;
            _id_sucursal = string.Empty;
            id_tipoGanadosPorPeso = 0;
            _observacion = string.Empty;
            Conexion = string.Empty;
            Usuario = string.Empty;
            numArete = string.Empty;
            Repeso = true;
            genero = string.Empty;
            CompraGanado = new CompraGanadosModels();
        }
        public int IDEstatusGanado { get; set; }
        public int IDCorral { get; set; }

        private string _id_Ganados;

        public string id_Ganados
        {
            get { return _id_Ganados; }
            set { _id_Ganados = value; }
        }
        private string _id_sucursal;

        public string id_sucursal
        {
            get { return _id_sucursal; }
            set { _id_sucursal = value; }
        }
        private string _id_estatus_Ganados;

        public string  genero { get; set; }

        private int _id_tipoGanadosPorPeso;

        public int id_tipoGanadosPorPeso
        {
            get { return _id_tipoGanadosPorPeso; }
            set { _id_tipoGanadosPorPeso = value; }
        }
        private string _observacion;

        public string observacion
        {
            get { return _observacion; }
            set { _observacion = value; }
        }
        private string _numArete;

        public string numArete
        {
            get { return _numArete; }
            set { _numArete = value; }
        }
        private bool _repeso;

        public bool Repeso
        {
            get { return _repeso; }
            set { _repeso = value; }
        }
        #region Datos De Control
        public string Conexion { get; set; }
        public int Resultado { get; set; }
        public bool Completado { get; set; }
        public string Usuario { get; set; }
        public int Opcion { get; set; }
        #endregion

        public CompraGanadosModels CompraGanado { get; set; }

    }
}