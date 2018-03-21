using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CatCategoriaPuestoModels
    {
        public CatCategoriaPuestoModels() {
            _listaPuestos = new List<CatPuestoModels>();
            _id_categoria = string.Empty;
            _id_puesto = 0;
            _descripcion = string.Empty;
            _sueldoBase = 0;
             //Datos de control
            Activo = false;
            Usuario = string.Empty;
            Conexion = string.Empty;
            Completado = false;
            Opcion = 0;
            Resultado = 0;

        }
        private List<CatCategoriaPuestoModels> _listaCategoriaPuesto;

        public List<CatCategoriaPuestoModels> listaCategoriaPuesto
        {
            get { return _listaCategoriaPuesto; }
            set { _listaCategoriaPuesto = value; }
        }

        private List<CatPuestoModels> _listaPuestos;
        [Required(ErrorMessage = "El Puesto es obligatorio")]
        [Display(Name = "Puesto")]

        [RegularExpression(@"^[1-9][0-9]*$", ErrorMessage = "Seleccione un puesto")]
        public List<CatPuestoModels> listaPuestos
        {
            get { return _listaPuestos; }
            set { _listaPuestos = value; }
        }

        private string _id_categoria;

        public string id_categoria
        {
            get { return _id_categoria; }
            set { _id_categoria = value; }
        }
        private string _puesto;

        public string puesto
        {
            get { return _puesto; }
            set { _puesto = value; }
        }

        private int _id_puesto;
       
        public int id_puesto
        {
            get { return _id_puesto; }
            set { _id_puesto = value; }
        }
        private string _descripcion;
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [Display(Name = "nombre")]
        [StringLength(300, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        [RegularExpression(@"^[A-Za-záéíóúñÁÉÍÓÚÑ0-9\(\)\-\,\.\;\:\s]*$", ErrorMessage = "Solo Letras y números")]
        public string descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }
        private decimal _sueldoBase;
        [Required(ErrorMessage = "El sueldo es obligatorio")]
        [Display(Name = "sueldo")]

        //[RegularExpression(@"^[1-9][0-9]\.\$*$", ErrorMessage = "Ingrese un sueldo")]
        public decimal sueldoBase
        {
            get { return _sueldoBase; }
            set { _sueldoBase = value; }
        }

        #region Datos De Control
        public string Conexion { get; set; }
        public int Resultado { get; set; }
        public bool Completado { get; set; }
        public string Usuario { get; set; }
        public int Opcion { get; set; }
        public bool Activo { get; set; }
        #endregion
    }
}