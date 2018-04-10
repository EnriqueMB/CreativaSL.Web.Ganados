using CreativaSL.Web.Ganados.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.ViewModels
{
    public class ServiciosMantenimientoViewModels
    {

        public ServiciosMantenimientoViewModels()
        {
            _IDServicio = string.Empty;
            _IDSucursal = string.Empty;
            _Fecha = DateTime.Today;
            _ImporteTotal = 0;
            _ListaSucursales = new List<CatSucursalesModels>();
            _ListaEmpleados = new List<CatEmpleadoModels>();
        }

        private string _IDServicio;
        public string IDServicio
        {
            get { return _IDServicio; }
            set { _IDServicio = value; }
        }

        private string _IDSucursal;
        public string IDSucursal
        {
            get { return _IDSucursal; }
            set { _IDSucursal = value; }
        }
        
        private DateTime _Fecha;
        public DateTime Fecha
        {
            get { return _Fecha; }
            set { _Fecha = value; }
        }

        private decimal _ImporteTotal;
        public decimal ImporteTotal
        {
            get { return _ImporteTotal; }
            set { _ImporteTotal = value; }
        }

        private List<CatSucursalesModels> _ListaSucursales;

        public List<CatSucursalesModels> ListaSucursales
        {
            get { return _ListaSucursales; }
            set { _ListaSucursales = value; }
        }

        private List<CatEmpleadoModels> _ListaEmpleados;

        public List<CatEmpleadoModels> ListaEmpleados
        {
            get { return _ListaEmpleados; }
            set { _ListaEmpleados = value; }
        }


    }
}