using CreativaSL.Web.Ganados.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class RPTPagosProveedorModels
    {
        public RPTPagosProveedorModels()
        {
            _ListaProveedor = new List<RPTPagosProveedorModels>();//se crea la lista
            _datosEmpresa = new DatosEmpresaViewModels();//datos para la empresa, el encabezado
            _Fecha = DateTime.Today;
            _Total = 0;
            _FechaInic = DateTime.Today;
            _FechaFin = DateTime.Today;
            _Conexion = string.Empty;
            _IdSucursal = string.Empty;
        }

        private List<RPTPagosProveedorModels> _ListaProveedor;
        public List<RPTPagosProveedorModels> ListaProveedor
        {
            get { return _ListaProveedor; }
            set { _ListaProveedor = value; }
        }

        private DatosEmpresaViewModels _datosEmpresa;
        public DatosEmpresaViewModels datosEmpresa
        {
            get { return _datosEmpresa; }
            set { _datosEmpresa = value; }
        }

        private DateTime _Fecha;
        public DateTime Fecha
        {
            get { return _Fecha; }
            set { _Fecha = value; }
        }

        private decimal _Total;
        public decimal Total
        {
            get { return _Total; }
            set { _Total = value; }
        }

        private DateTime _FechaInic;
        public DateTime FechaInic
        {
            get { return _FechaInic; }
            set { _FechaInic = value; }
        }


        private DateTime _FechaFin;
        public DateTime FechaFin
        {
            get { return _FechaFin; }
            set { _FechaFin = value; }
        }

        private string _Conexion;
        public string Conexion
        {
            get { return _Conexion; }
            set { _Conexion = value; }
        }

        private string _IdSucursal;
        public string IdSucursal
        {
            get { return _IdSucursal; }
            set { _IdSucursal = value; }
        }

    }
}