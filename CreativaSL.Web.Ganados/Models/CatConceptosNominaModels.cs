using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using CreativaSL.Web.Ganados.Models.Validaciones;

namespace CreativaSL.Web.Ganados.Models
{
    public class CatConceptosNominaModels
    {

        public CatConceptosNominaModels()
        {
            _IDConceptoNomina = 0;
            _Calculado = false;
            _SumaResta = false;
            _SoloLectura = false;
            Conexion = string.Empty;
            Usuario = string.Empty;
        }
        private int _IDConceptoNomina;
        /// <summary>
        /// EL identificador del Concepto
        /// </summary>
        public int IDConceptoNomina
        {
            get { return _IDConceptoNomina; }
            set { _IDConceptoNomina = value; }
        }

        private string _Descripcion;
        /// <summary>
        /// La descripcion del Concepto
        /// </summary>
        [Required(ErrorMessage ="Debe ingresar la descripcion del Concepto")]
        [Display(Name = "Descripcion")]
        [StringLength(300, ErrorMessage = "El número de caracteres del campo {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        [Texto(ErrorMessage = "Ingrese un dato válido para Nombre/Razón Social")]
        public string Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }

        private bool _Calculado;
        /// <summary>
        /// 
        /// </summary>
        public bool Calculado
        {
            get { return _Calculado; }
            set { _Calculado = value; }
        }

        private bool _SumaResta;
        /// <summary>
        /// 
        /// </summary>
        public bool SumaResta
        {
            get { return _SumaResta; }
            set { _SumaResta = value; }
        }

        private bool _SoloLectura;
        /// <summary>
        /// 
        /// </summary>
        public bool SoloLectura
        {
            get { return _SoloLectura; }
            set { _SoloLectura = value; }
        }

        private List<CatConceptosNominaModels> _ListaConceptoNomina;
        /// <summary>
        /// Lista para llenar las tablas de conceptos
        /// </summary>
        public List<CatConceptosNominaModels> LIstaConceptoNomina
        {
            get { return _ListaConceptoNomina; }
            set { _ListaConceptoNomina = value; }
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