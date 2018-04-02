using CreativaSL.Web.Ganados.Models.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CatMarcaVehiculoModels
    {
        public CatMarcaVehiculoModels()
        {
            _Descripcion = string.Empty;
            Conexion = string.Empty;
            Usuario = string.Empty;
            _ListaMarcas = new List<CatMarcaVehiculoModels>();
        }

        private int _IDMarca;

        public int IDMarca
        {
            get { return _IDMarca; }
            set { _IDMarca = value; }
        }

        private string _Descripcion;
        [Required(ErrorMessage = "La marca es obligatorio")]
        [Display(Name = "marca")]
        [StringLength(80, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        [Texto(ErrorMessage = "Solo Letras y números")]
        public string Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }

        private List<CatMarcaVehiculoModels> _ListaMarcas;

        public List<CatMarcaVehiculoModels> ListaMarcas
        {
            get { return _ListaMarcas; }
            set { _ListaMarcas = value; }
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