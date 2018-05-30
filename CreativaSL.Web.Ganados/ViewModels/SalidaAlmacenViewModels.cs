using CreativaSL.Web.Ganados.Models;
using CreativaSL.Web.Ganados.Models.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.ViewModels
{
    public class SalidaAlmacenViewModels
    {
        public SalidaAlmacenViewModels()
        {
            _ListaSucursales = new List<CatSucursalesModels>();
            _ListaAlmacenes = new List<CatAlmacenModels>();
            _ListaEmpleados = new List<CatEmpleadoModels>();
        }

        private string _IDSalidaAlmacen;
        /// <summary>
        /// Identificador de la Salida de almacén
        /// </summary>
        public string IDSalidaAlmacen
        {
            get { return _IDSalidaAlmacen; }
            set { _IDSalidaAlmacen = value; }
        }

        private string _IDSucursal;
        /// <summary>
        /// Identificador de la sucursal
        /// </summary>
        [Required(ErrorMessage = "Seleccione una sucursal")]
        [Display(Name = "Sucursal")]
        public string IDSucursal
        {
            get { return _IDSucursal; }
            set { _IDSucursal = value; }
        }
        
        private string _IDAlmacen;
        /// <summary>
        /// Identificador del almacén
        /// </summary>
        [Required(ErrorMessage = "Seleccione un almacén")]
        [Display(Name = "Almacén")]
        public string IDAlmacen
        {
            get { return _IDAlmacen; }
            set { _IDAlmacen = value; }
        }

        private string _IDEmpleado;
        /// <summary>
        /// Identificador del empleado
        /// </summary>
        [Required(ErrorMessage = "Seleccione un empleado")]
        [Display(Name = "Empleado")]
        public string IDEmpleado
        {
            get { return _IDEmpleado; }
            set { _IDEmpleado = value; }
        }
        
        private DateTime _FechaSalida;
        /// <summary>
        /// Fecha en la que se realiza la entrada a almacén
        /// </summary>
        [Required(ErrorMessage = "Debe seleccionar una fecha de salida")]
        [Display(Name = "Fecha de salida")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaSalida
        {
            get { return _FechaSalida; }
            set { _FechaSalida = value; }
        }

        private string _Comentario;
        /// <summary>
        /// Información adicional de la entrada de almacén
        /// </summary>
        [Texto(ErrorMessage = "Ingrese un texto válido para {0}")]
        [Display(Name = "Comentarios adicionales")]
        public string Comentario
        {
            get { return _Comentario; }
            set { _Comentario = value; }
        }

        private List<CatSucursalesModels> _ListaSucursales;
        /// <summary>
        /// Lista de sucursales para combo
        /// </summary>
        public List<CatSucursalesModels> ListaSucursales
        {
            get { return _ListaSucursales; }
            set { _ListaSucursales = value; }
        }
        
        private List<CatAlmacenModels> _ListaAlmacenes;
        /// <summary>
        /// Lista de almacenes 
        /// </summary>
        public List<CatAlmacenModels> ListaAlmacenes
        {
            get { return _ListaAlmacenes; }
            set { _ListaAlmacenes = value; }
        }

        private List<CatEmpleadoModels> _ListaEmpleados;
        /// <summary>
        /// Lista de empleados para combo
        /// </summary>
        public List<CatEmpleadoModels> ListaEmpleados
        {
            get { return _ListaEmpleados; }
            set { _ListaEmpleados = value; }
        }

    }
}