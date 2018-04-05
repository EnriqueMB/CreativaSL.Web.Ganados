using CreativaSL.Web.Ganados.Models.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Web;
using System.Web.Mvc;

namespace CreativaSL.Web.Ganados.Models
{
    public class CatUnidadMedidaModels
    {
        private int _IDUnidadMedida;

        public int IDUnidadMedida
        {
            get { return _IDUnidadMedida; }
            set { _IDUnidadMedida = value; }
        }

        private string _Descripcion;
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [Display(Name = "Nombre")]
        [StringLength(1000, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        public string Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }
        private List<CatUnidadMedidaModels> _LUnidades;
        
        public List<CatUnidadMedidaModels> LUnidades
        {
            get { return _LUnidades; }
            set { _LUnidades = value; }
        }

        #region Datos De Control
        public string Conexion { get; set; }
        public int Resultado { get; set; }
        public bool Completado { get; set; }
        public string Usuario { get; set; }
        public int Opcion { get; set; }
        #endregion
    }
}