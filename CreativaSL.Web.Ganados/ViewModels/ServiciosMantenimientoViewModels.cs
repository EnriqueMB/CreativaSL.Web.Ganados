using CreativaSL.Web.Ganados.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            _ListaProveedores = new List<CatProveedorModels>();
        }

        private string _ID;
        /// <summary>
        /// ID del vehículo, Remolque, etc., al que se le aplica el servicio
        /// </summary>
        [Required(ErrorMessage ="Se requiere un identificador del vehículo.")]
        [Display(Name = "Vehículo")]
        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        
        private string _IDServicio;
        public string IDServicio
        {
            get { return _IDServicio; }
            set { _IDServicio = value; }
        }

        private string _IDSucursal;
        [Required(ErrorMessage = "Seleccione una sucursal.")]
        [Display(Name = "Sucursal")]
        public string IDSucursal
        {
            get { return _IDSucursal; }
            set { _IDSucursal = value; }
        }

        private string _IDProveedor;
        [Required(ErrorMessage = "Seleccione un proveedor.")]
        [Display(Name = "Proveedor")]
        public string IDProveedor
        {
            get { return _IDProveedor; }
            set { _IDProveedor = value; }
        }
        
        private DateTime _Fecha;
        [Required(ErrorMessage = "Debe seleccionar una fecha del servicio")]
        [Display(Name = "Fecha de servicio")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Fecha
        {
            get { return _Fecha; }
            set { _Fecha = value; }
        }

        private string _Observaciones;
        public string Observaciones
        {
            get { return _Observaciones; }
            set { _Observaciones = value; }
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

        private List<CatProveedorModels> _ListaProveedores;
        public List<CatProveedorModels> ListaProveedores
        {
            get { return _ListaProveedores; }
            set { _ListaProveedores = value; }
        }


        private int _Tipo;
        public int Tipo
        {
            get { return _Tipo; }
            set { _Tipo = value; }
        }

    }
}