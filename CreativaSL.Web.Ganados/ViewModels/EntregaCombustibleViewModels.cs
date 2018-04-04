﻿using CreativaSL.Web.Ganados.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.ViewModels
{
    public class EntregaCombustibleViewModels
    {
        public EntregaCombustibleViewModels()
        {
            _IDEntregaCombustible = string.Empty;
            _IDSucursal = string.Empty;
            _IDVehiculo = string.Empty;
            _IDTipoCombustible = 0;
            _Fecha = DateTime.Today;
            _NoTicket = string.Empty;
            _KMInicial = 0;
            _KMFinal = 0;
            _Litros = 0;
            _Total = 0;            
            _ImgTicket = string.Empty;
            _ListaVehiculos = new List<CatVehiculoModels>();
            _ListaTipoCombustible = new List<CatTipoCombustibleModels>();
        }

        private string _IDEntregaCombustible;
        /// <summary>
        /// Identificador de la entrega de combustible
        /// </summary>
        public string IDEntregaCombustible
        {
            get { return _IDEntregaCombustible; }
            set { _IDEntregaCombustible = value; }
        }

        private string _IDSucursal;
        /// <summary>
        /// Identificador de la sucursal a la que se cargará el documento por pagar
        /// </summary>
        public string IDSucursal
        {
            get { return _IDSucursal; }
            set { _IDSucursal = value; }
        }


        private string _IDVehiculo;
        /// <summary>
        /// Identificador del vehículo
        /// </summary>
        public string IDVehiculo
        {
            get { return _IDVehiculo; }
            set { _IDVehiculo = value; }
        }
        
        private int _IDTipoCombustible;
        /// <summary>
        /// Identificador del tipo de combustible
        /// </summary>
        public int IDTipoCombustible
        {
            get { return _IDTipoCombustible; }
            set { _IDTipoCombustible = value; }
        }
        
        private DateTime _Fecha;
        /// <summary>
        /// Fecha en que se realiza la carga de combustible
        /// </summary>
        public DateTime Fecha
        {
            get { return _Fecha; }
            set { _Fecha = value; }
        }

        private string _NoTicket;
        /// <summary>
        /// Número de ticket de compra de combustible
        /// </summary>
        public string NoTicket
        {
            get { return _NoTicket; }
            set { _NoTicket = value; }
        }

        private int _KMInicial;
        /// <summary>
        /// Kilometraje inicial 
        /// </summary>
        public int KMInicial
        {
            get { return _KMInicial; }
            set { _KMInicial = value; }
        }

        private int _KMFinal;
        /// <summary>
        /// Kilometraje final
        /// </summary>
        public int KMFinal
        {
            get { return _KMFinal; }
            set { _KMFinal = value; }
        }

        private decimal _Litros;
        /// <summary>
        /// Cantidad de litros ingresados
        /// </summary>
        public decimal Litros
        {
            get { return _Litros; }
            set { _Litros = value; }
        }
        
        private decimal _Total;
        /// <summary>
        /// Monto total
        /// </summary>
        public decimal Total
        {
            get { return _Total; }
            set { _Total = value; }
        }        

        private string _ImgTicket;
        /// <summary>
        /// Imagen del ticket de la carga de combustible
        /// </summary>
        public string ImgTicket
        {
            get { return _ImgTicket; }
            set { _ImgTicket = value; }
        }

        private List<CatVehiculoModels> _ListaVehiculos;
        /// <summary>
        /// Lista para llenar combo de sucursales
        /// </summary>
        public List<CatVehiculoModels> ListaVehiculos
        {
            get { return _ListaVehiculos; }
            set { _ListaVehiculos = value; }
        }

        private List<CatTipoCombustibleModels> _ListaTipoCombustible;
        /// <summary>
        /// Lista para llenar combo de tipo de combustible
        /// </summary>
        public List<CatTipoCombustibleModels> ListaTipoCombustible
        {
            get { return _ListaTipoCombustible; }
            set { _ListaTipoCombustible = value; }
        }

        private List<CatSucursalesModels> _ListaSucursales;
        /// <summary>
        /// Lista para llenar combo de sucursales
        /// </summary>
        public List<CatSucursalesModels> ListaSucursales
        {
            get { return _ListaSucursales; }
            set { _ListaSucursales = value; }
        }


    }
}