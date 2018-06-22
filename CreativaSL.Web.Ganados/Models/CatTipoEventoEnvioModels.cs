using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CatTipoEventoEnvioModels
    {
        public RespuestaAjax RespuestaAjax { get; set; }

        private int _IDTipoEventoEnvio;

        public int IDTipoEventoEnvio
        {
            get { return _IDTipoEventoEnvio; }
            set { _IDTipoEventoEnvio = value; }
        }

        private string _Descripcion;

        /// <summary>
        /// Nombre del evento
        /// </summary>
        [Required(ErrorMessage = "Ingrese un nombre del evento")]
        [StringLength(80, ErrorMessage = "Longitud máxima de 80 caracteres.")]
        [Display(Name = "Descripción del evento")]
        public string Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }

        /// <summary>
        /// Para cambia el precio por kilo del ganado en una compra o para realizar una deducción en una venta o flete
        /// </summary>
        private bool _MarcarMerma;

        public bool MarcarMerma
        {
            get { return _MarcarMerma; }
            set { _MarcarMerma = value; }
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