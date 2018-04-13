using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.ViewModels
{
    public class DatosEmpresaViewModels
    {
        public DatosEmpresaViewModels()
        {
            NombreSucursal = string.Empty;
            RazonFiscal = string.Empty;
            DireccionFiscal = string.Empty;
            RFC = string.Empty;
            NumTelefonico1 = string.Empty;
            NumTelefonico2 = string.Empty;
            Email = string.Empty;
            HorarioAtencion = string.Empty;
            Representante = string.Empty;
            LogoEmpresa = string.Empty;
            ImagenContruida = string.Empty;
        }

        public string NombreSucursal { get; set; }
        
        public string RazonFiscal { get; set; }
        
        public string DireccionFiscal { get; set; }
        
        public string RFC { get; set; }
        
        public string NumTelefonico1 { get; set; }
        
        public string NumTelefonico2 { get; set; }
        
        public string Email { get; set; }
        
        public string HorarioAtencion { get; set; }
        
        public string Representante { get; set; }

        public string LogoEmpresa { get; set; }

        public string ImagenContruida { get; set; }
    }
}