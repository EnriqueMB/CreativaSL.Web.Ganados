using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class GanadosModels
    {
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

        public string id_estatus_Ganados
        {
            get { return _id_estatus_Ganados; }
            set { _id_estatus_Ganados = value; }
        }
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
        private bool _ranchoCliente;

        public bool ranchoCliente
        {
            get { return _ranchoCliente; }
            set { _ranchoCliente = value; }
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