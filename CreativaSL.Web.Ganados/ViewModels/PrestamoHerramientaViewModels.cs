using CreativaSL.Web.Ganados.Models;
using CreativaSL.Web.Ganados.Models.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.ViewModels
{
    public class PrestamoHerramientaViewModels
    {
        public PrestamoHerramientaViewModels()
        {
            _IDPrestamo = 0;
            _IDSucursal = string.Empty;
            _IDEmpleado = string.Empty;
            _IDAlmacen = string.Empty;
            _ListaAlmacenes = new List<CatAlmacenModels>();
            _ListaEmpleados = new List<CatEmpleadoModels>();
            _ListaSucursales = new List<CatSucursalesModels>();
        }
        private int _IDPrestamo;
        /// <summary>
        /// Identificador de la Salida de almacén
        /// </summary>
        public int IDPrestamo
        {
            get { return _IDPrestamo; }
            set { _IDPrestamo = value; }
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

        private DateTime _FechaPrestamo;
        /// <summary>
        /// Fecha en la que se realiza la entrada a almacén
        /// </summary>
        [Required(ErrorMessage = "Debe seleccionar una fecha de Prestamo")]
        [Display(Name = "Fecha de salida")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaPrestamo
        {
            get { return _FechaPrestamo; }
            set { _FechaPrestamo = value; }
        }

        private string _Comentario;
        /// <summary>
        /// Información adicional de la salida de almacén
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