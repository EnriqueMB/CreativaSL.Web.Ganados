using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CatVehiculoModels
    {
        private string _IDVehiculo;

        public string IDVehiculo
        {
            get { return _IDVehiculo; }
            set { _IDVehiculo = value; }
        }

        private string _IDSucursal;

        public string IDSucursal
        {
            get { return _IDSucursal; }
            set { _IDSucursal = value; }
        }

        private int _IDTipoVehiculo;

        public int IDTipoVehiculo
        {
            get { return _IDTipoVehiculo; }
            set { _IDTipoVehiculo = value; }
        }

        private int _IDMarca;

        public int IDMarca
        {
            get { return _IDMarca; }
            set { _IDMarca = value; }
        }

        private bool _EsPropio;

        public bool EsPropio
        {
            get { return _EsPropio; }
            set { _EsPropio = value; }
        }

        private string _Capacidad;

        public string Capacidad
        {
            get { return _Capacidad; }
            set { _Capacidad = value; }
        }

        private string _Modelo;

        public string Modelo
        {
            get { return _Modelo; }
            set { _Modelo = value; }
        }

        private string _Color;

        public string Color
        {
            get { return _Color; }
            set { _Color = value; }
        }

        private string _Placas;

        public string Placas
        {
            get { return _Placas; }
            set { _Placas = value; }
        }

        private string _Remolque;

        public string Remolque
        {
            get { return _Remolque; }
            set { _Remolque = value; }
        }

        private string _NoSerie;

        public string NoSerie
        {
            get { return _NoSerie; }
            set { _NoSerie = value; }
        }

        private bool _Estatus;

        public bool Estatus
        {
            get { return _Estatus; }
            set { _Estatus = value; }
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