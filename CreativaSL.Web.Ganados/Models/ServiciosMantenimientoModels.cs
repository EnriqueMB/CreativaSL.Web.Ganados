using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class ServiciosMantenimientoModels
    {

        public ServiciosMantenimientoModels()
        {
            _ListaRemolques = new List<CatRemolqueModels>();
            _ListaVehiculos = new List<CatVehiculoModels>();
        }


        private string _IDServicio;

        public string IDServicio
        {
            get { return _IDServicio; }
            set { _IDServicio = value; }
        }

        private CatSucursalesModels _Sucursal;

        public CatSucursalesModels Sucursal
        {
            get { return _Sucursal; }
            set { _Sucursal = value; }
        } 

        private DateTime _Fecha;

        public DateTime Fecha
        {
            get { return _Fecha; }
            set { _Fecha = value; }
        }

        
        public string FechaFormat
        {
            get { return _Fecha.ToShortDateString(); }
        }


        private string _ServiciosRealizados;

        public string ServiciosRealizados
        {
            get { return _ServiciosRealizados; }
            set { _ServiciosRealizados = value; }
        }


        private decimal _ImporteTotal;

        public decimal ImporteTotal
        {
            get { return _ImporteTotal; }
            set { _ImporteTotal = value; }
        }

        public string ImporteTotalFormat
        {
            get { return string.Format("{0:c}", _ImporteTotal); }
        }


        private List<CatVehiculoModels> _ListaVehiculos;
        /// <summary>
        /// Lista de vehículos a los que se les puede realizar mantenimiento
        /// </summary>
        public List<CatVehiculoModels> ListaVehiculos
        {
            get { return _ListaVehiculos; }
            set { _ListaVehiculos = value; }
        }

        private List<CatRemolqueModels> _ListaRemolques;
        /// <summary>
        /// Lista de remolques a los que se les puede realizar mantenimiento
        /// </summary>
        public List<CatRemolqueModels> ListaRemolques
        {
            get { return _ListaRemolques; }
            set { _ListaRemolques = value; }
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