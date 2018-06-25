using CreativaSL.Web.Ganados.Models;
using CreativaSL.Web.Ganados.Models.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required(ErrorMessage = "Se requiere el identificador del servicio.")]
        [Display(Name = "IDServicio")]
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
        [Required(ErrorMessage = "Seleccione un servicio.")]
        [Display(Name = "Servicio")]
        public string IDTipoServicio
        {
            get { return _IDTipoServicio; }
            set { _IDTipoServicio = value; }
        }

        private string _Encargado;
        [Required(ErrorMessage = "Se require el nombre del responsable.")]
        [Display(Name = "Encargado")]
        public string Encargado
        {
            get { return _Encargado; }
            set { _Encargado = value; }
        }

        private decimal _Importe;
        [Required(ErrorMessage = "{0} es requerido")]
        [Display(Name = "$ Mano de obra")]
        public decimal Importe
        {
            get { return _Importe; }
            set { _Importe = value; }
        }

        private decimal _ImporteRefacciones;
        [Required(ErrorMessage = "{0} es requerido")]
        [Display(Name = "$ Refacciones ")]
        public decimal ImporteRefacciones
        {
            get { return _ImporteRefacciones; }
            set { _ImporteRefacciones = value; }
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