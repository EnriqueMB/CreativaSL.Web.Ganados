using CreativaSL.Web.Ganados.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.ViewModels
{
    public class ServiciosMantenimientoDetalleViewModels
    {
        public ServiciosMantenimientoDetalleViewModels()
        {

        }

        private string _IDServicio;

        public string IDServicio
        {
            get { return _IDServicio; }
            set { _IDServicio = value; }
        }

        private string _IDServicioDetalle;

        public string IDServicioDetalle
        {
            get { return _IDServicioDetalle; }
            set { _IDServicioDetalle = value; }
        }

        private string _IDTipoServicio;

        public string IDTipoServicio
        {
            get { return _IDTipoServicio; }
            set { _IDTipoServicio = value; }
        }

        private string _Encargado;

        public string Encargado
        {
            get { return _Encargado; }
            set { _Encargado = value; }
        }

        private decimal _Importe;

        public decimal Importe
        {
            get { return _Importe; }
            set { _Importe = value; }
        }

        private string _Observaciones;

        public string Observaciones
        {
            get { return _Observaciones; }
            set { _Observaciones = value; }
        }


        private List<CatTipoServicioModels> _ListaTipoServicios;

        public List<CatTipoServicioModels> ListaTipoServicios
        {
            get { return _ListaTipoServicios; }
            set { _ListaTipoServicios = value; }
        }


    }
}