using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CatRangoPesoCompraModels
    {
        public CatRangoPesoCompraModels()
        {
            _ListaRangoPeso = new List<CatRangoPesoCompraModels>();
        }
        private int _IDRango;

        public int IDRango
        {
            get { return _IDRango; }
            set { _IDRango = value; }
        }

        private bool _EsMacho;

        public bool EsMacho
        {
            get { return _EsMacho; }
            set { _EsMacho = value; }
        }

        private decimal _PesoMinimo;
        [Required(ErrorMessage = "El pesos minimo es obligatorio")]
        [Display(Name = "peso minimo")]
        [StringLength(20, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        [RegularExpression(@"^[0-9]+([.])?([0-9]+)?$", ErrorMessage = "Solo números y decimales")]
        public decimal PesoMinimo
        {
            get { return _PesoMinimo; }
            set { _PesoMinimo = value; }
        }
        
        private decimal _PesoMaximo;
        [Required(ErrorMessage = "El pesos maximo es obligatorio")]
        [Display(Name = "peso maximo")]
        [StringLength(20, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        [RegularExpression(@"^[0-9]+([.])?([0-9]+)?$", ErrorMessage = "Solo números y decimales")]
        public decimal PesoMaximo
        {
            get { return _PesoMaximo; }
            set { _PesoMaximo = value; }
        }


        private List<CatRangoPesoCompraModels> _ListaRangoPeso;

        public List<CatRangoPesoCompraModels> ListaRangoPeso
        {
            get { return _ListaRangoPeso; }
            set { _ListaRangoPeso = value; }
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