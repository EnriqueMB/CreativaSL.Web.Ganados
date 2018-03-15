using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class FleteModels
    {
        private string _id_flete;

        public string id_flete
        {
            get { return _id_flete; }
            set { _id_flete = value; }
        }
        private string _id_vehiculo;

        public string id_vehiculo
        {
            get { return _id_vehiculo; }
            set { _id_vehiculo = value; }
        }
        private string _id_chofer;

        public string id_chofer
        {
            get { return _id_chofer; }
            set { _id_chofer = value; }
        }
        private string _id_jaula;

        public string id_jaula
        {
            get { return _id_jaula; }
            set { _id_jaula = value; }
        }
        private int _kmInicialVehiculo;

        public int kmInicialVehiculo
        {
            get { return _kmInicialVehiculo; }
            set { _kmInicialVehiculo = value; }
        }
        private int _kmFinalVehiculo;

        public int kmFinalVehiculo
        {
            get { return _kmFinalVehiculo; }
            set { _kmFinalVehiculo = value; }
        }
        private decimal _precioFlete;

        public decimal precioFlete
        {
            get { return _precioFlete; }
            set { _precioFlete = value; }
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