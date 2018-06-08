using CreativaSL.Web.Ganados.Models;
using CreativaSL.Web.Ganados.Models.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.ViewModels
{
    public class ConciliacionAlmacenViewModels
    {
        public ConciliacionAlmacenViewModels()
        {
            _IDConciliacion = string.Empty;
            _IDSucursal = string.Empty;
            _IDAlmacen = string.Empty;
            _IDTipoConciliacion = 0;
            _FechaConciliacion = DateTime.Today;
            _Comentarios = string.Empty;
            _ListaSucursales = new List<CatSucursalesModels>();
            _ListaAlmacenes = new List<CatAlmacenModels>();
            _ListaTipoConciliacion = new List<CatTipoConciliacionModels>();
        }

        private string _IDConciliacion;
        /// <summary>
        /// IDentificador de la conciliación
        /// </summary>
        public string IDConciliacion
        {
            get { return _IDConciliacion; }
            set { _IDConciliacion = value; }
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

        private int _IDTipoConciliacion;
        [CombosInt(ErrorMessage = "Seleccione un tipo de conciliación")]
        [Display(Name = "Tipo de conciliación")]
        public int IDTipoConciliacion
        {
            get { return _IDTipoConciliacion; }
            set { _IDTipoConciliacion = value; }
        }
        
        private DateTime _FechaConciliacion;
        /// <summary>
        /// Fecha en la que se realiza la entrada a almacén
        /// </summary>
        [Required(ErrorMessage = "Debe seleccionar una fecha de conciliación")]
        [Display(Name = "Fecha de conciliación")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaConciliacion
        {
            get { return _FechaConciliacion; }
            set { _FechaConciliacion = value; }
        }

        private string _Comentarios;
        /// <summary>
        /// Información adicional de la concililación de almacén
        /// </summary>
        [Texto(ErrorMessage = "Ingrese un texto válido para {0}")]
        [Display(Name = "Comentarios adicionales")]
        public string Comentarios
        {
            get { return _Comentarios; }
            set { _Comentarios = value; }
        }


        private List<CatSucursalesModels> _ListaSucursales;
        public List<CatSucursalesModels> ListaSucursales
        {
            get { return _ListaSucursales; }
            set { _ListaSucursales = value; }
        }

        private List<CatAlmacenModels> _ListaAlmacenes;
        public List<CatAlmacenModels> ListaAlmacenes
        {
            get { return _ListaAlmacenes; }
            set { _ListaAlmacenes = value; }
        }

        private List<CatTipoConciliacionModels> _ListaTipoConciliacion;
        public List<CatTipoConciliacionModels> ListaTipoConciliacion
        {
            get { return _ListaTipoConciliacion; }
            set { _ListaTipoConciliacion = value; }
        }

    }
}