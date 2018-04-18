using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class FleteImpuestoModels
    {
        public string IDFleteImpuesto { get; set; }
        public decimal Base { get; set; }
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
        public string Conexion { get; set; }
        public int Resultado { get; set; }
        public bool Completado { get; set; }
        public string Usuario { get; set; }
        public int Opcion { get; set; }
        #endregion
    }
}