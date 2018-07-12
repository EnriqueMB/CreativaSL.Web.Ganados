using CreativaSL.Web.Ganados.Models.Validaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class FleteImpuestoModels
    {
        #region Constructor
        public FleteImpuestoModels()
        {
            Impuesto = new CFDI_ImpuestoModels();
            TipoImpuesto = new CFDI_TipoImpuestoModels();
            TipoFactor = new CFDI_TipoFactorModels();
            RespuestaAjax = new RespuestaAjax();
        }
        #endregion

        public string Id_documentoCobrarDetalleImpuesto { get; set; }
        public string Id_detalleDoctoCobrar { get; set; }

        public string IDFleteImpuesto { get; set; }
        [SAT_BaseAttribute(ErrorMessage = "La Base debe ser mayor que cero, hasta 6 decimales.")]
        public decimal Base { get; set; }
        [SAT_TasaCuotaAttribute(ErrorMessage = "La Tasa o Cuota debe ser un número entero, seguido de hasta 6 decimales.")]
        public decimal TasaCuota { get; set; }
        public decimal Importe { get; set; }
        
        public CFDI_ImpuestoModels Impuesto { get; set; }
        public CFDI_TipoImpuestoModels TipoImpuesto { get; set; }
        public CFDI_TipoFactorModels TipoFactor { get; set; }

        public List<CFDI_ImpuestoModels> ListaImpuesto { get; set; }
        public List<CFDI_TipoImpuestoModels> ListaTipoImpuesto { get; set; }
        public List<CFDI_TipoFactorModels> ListaTipoFactor { get; set; }
        
        //Tabla relacionada
        public string IDFlete { get; set; }

        #region Datos De Control
        public RespuestaAjax RespuestaAjax { get; set; }
        public string Conexion { get; set; }
        public string Usuario { get; set; }
        #endregion

        #region Métodos
        public decimal ObtenerImporte()
        {
            //[cfdi_TipoFactor]
            //clave  descripcion
            //  1       Tasa
            //  2       Cuota
            //  3       Exento

            if (TipoFactor.Clave == 1 || TipoFactor.Clave == 2)
            {
                Importe = Base * TasaCuota;
            }
            else
            {
                Importe = 0;
            }

            return Importe;
        }
        #endregion
    }
}