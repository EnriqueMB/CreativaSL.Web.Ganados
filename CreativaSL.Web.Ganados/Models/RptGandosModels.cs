﻿using CreativaSL.Web.Ganados.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class RptGandosModels
    {
        public RptGandosModels()
        {
            _cliente = string.Empty;
            _montoTotal = 0;
            _genero = string.Empty;
            _folio = 0;
            _fechahoraVenta = DateTime.Today;
            _fechaInicio = DateTime.Today;
            _fechaFin = DateTime.Today;
            _GanadosMachos = 0;
            _GanadosTotal = 0;
            _GandosHembras = 0;
            _listaGanadosVendidos = new List<RptGandosModels>();
            _listaGanadosTotal = new List<RptGandosModels>();
            _datosEmpresa = new DatosEmpresaViewModels();
            Conexion = string.Empty;
            Completado = false;
            Resultado = 0;
            Usuario = string.Empty;
            _TotalKilos = 0;
            _KiloHembra = 0;
            _KilosMachos = 0;
        }

        private string _NombreSucursal;

        public string NombreSucursal
        {
            get { return _NombreSucursal; }
            set { _NombreSucursal = value; }
        }

        private DateTime _fechahoraVenta;

        public DateTime FechahoraVenta
        {
            get { return _fechahoraVenta; }
            set { _fechahoraVenta = value; }
        }

        private string _cliente;

        public string Cliente
        {
            get { return _cliente; }
            set { _cliente = value; }
        }

        private Int64 _folio;

        public Int64 Folio
        {
            get { return _folio; }
            set { _folio = value; }
        }

        private int _GanadosMachos;

        public int GanadosMachos
        {
            get { return _GanadosMachos; }
            set { _GanadosMachos = value; }
        }

        private int _GandosHembras;

        public int GanadosHembras
        {
            get { return _GandosHembras; }
            set { _GandosHembras = value; }
        }

        private int _GanadosTotal;

        public int GanadosTotal
        {
            get { return _GanadosTotal; }
            set { _GanadosTotal = value; }
        }

        private int _KiloHembra;

        public int KiloHembra
        {
            get { return _KiloHembra; }
            set { _KiloHembra = value; }
        }

        private int _KilosMachos;

        public int KilosMachos
        {
            get { return _KilosMachos; }
            set { _KilosMachos = value; }
        }

        private int _TotalKilos;

        public int TotalKilos
        {
            get { return _TotalKilos; }
            set { _TotalKilos = value; }
        }

        private Decimal _montoTotal;

        public Decimal MontoTotal
        {
            get { return _montoTotal; }
            set { _montoTotal = value; }
        }

        private string _IDSucursal;

        public string IDSucursal
        {
            get { return _IDSucursal; }
            set { _IDSucursal = value; }
        }

        private List<RptGandosModels> _listaGanadosTotal;

        public List<RptGandosModels> listaGanadosTotal
        {
            get { return _listaGanadosTotal; }
            set { _listaGanadosTotal = value; }
        }
        
        private string _genero;

        public string Genero
        {
            get { return _genero; }
            set { _genero = value; }
        }
        
        private DateTime _fechaInicio;

        public DateTime FechaInicio
        {
            get { return _fechaInicio; }
            set { _fechaInicio = value; }
        }
        private List<RptGandosModels> _listaGanadosVendidos;

        public List<RptGandosModels> listaGanadosVendidos
        {
            get { return _listaGanadosVendidos; }
            set { _listaGanadosVendidos = value; }
        }

        private DatosEmpresaViewModels _datosEmpresa;

        public DatosEmpresaViewModels datosEmpresa
        {
            get { return _datosEmpresa; }
            set { _datosEmpresa = value; }
        }
        
        private DateTime _fechaFin;

        public DateTime FechaFin
        {
            get { return _fechaFin; }
            set { _fechaFin = value; }
        }
        #region Datos control
        public string Conexion { get; set; }
        public int Resultado { get; set; }
        public bool Completado { get; set; }
        public string Usuario { get; set; }
        #endregion
    }
}