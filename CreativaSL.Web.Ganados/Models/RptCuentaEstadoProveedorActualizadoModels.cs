using CreativaSL.Web.Ganados.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class RptCuentaEstadoProveedorActualizadoModels
    {
        public string TipoProveedor { get; set; }
        public string NombreProveedor { get; set; }
        public long NumeroFolio { get; set; }
        public decimal Anticipo { get; set; }
        public decimal Finiquito { get; set; }
        public decimal GranTotal { get; set; }
        public decimal NuevoTotal { get; set; }
        public DatosEmpresaViewModels DatosEmpresa { get; set; }

        public string Conexion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public DateTime FechaPago { get; set; }
    }
}