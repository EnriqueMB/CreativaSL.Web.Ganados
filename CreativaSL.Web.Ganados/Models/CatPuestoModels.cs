using CreativaSL.Web.Ganados.Models.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CatPuestoModels
    {
        public CatPuestoModels()
        {
            _Descripcion = string.Empty;
            _ListaPuesto = new List<CatPuestoModels>();
            _EsGerente = true;
        }

        private int _IDPuesto;

        public int IDPuesto
        {
            get { return _IDPuesto; }
            set { _IDPuesto = value; }
        }

        private string _Descripcion;
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [Display(Name = "nombre")]
        [StringLength(300, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        [Texto(ErrorMessage = "Solo Letras y números")]
        public string Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }

        private bool _EsGerente;

        public bool EsGerente
        {
            get { return _EsGerente; }
            set { _EsGerente = value; }
        }

        private List<CatPuestoModels> _ListaPuesto;

        public List<CatPuestoModels> ListaPuesto
        {
            get { return _ListaPuesto; }
            set { _ListaPuesto = value; }
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