using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CFDI_ImpuestoModels
    {
        public int  Clave { get; set; }
        public string Descripcion { get; set; }
        public bool Retencion { get; set; }
        public bool Traslado { get; set; }
        public bool Federal { get; set; }
        public string EntidadEnLaQueAplica { get; set; }

        #region Datos De Control
        public string Conexion { get; set; }
        public int Resultado { get; set; }
        public bool Completado { get; set; }
        public string Usuario { get; set; }
        public int Opcion { get; set; }
        #endregion
    }
}