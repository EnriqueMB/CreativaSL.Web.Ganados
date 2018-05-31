using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class DocumentosPorCobrarDetallePagosBancarizadoModels
    {
        public string Id_documentoPorCobrarDetallePagosBancarizado { get; set; }
        public string Id_documentoPorCobrarDetallePagos { get; set; }

        public string NombreBancoOrdenante { get; set; }
        public string RfcEmisorOrdenante { get; set; }
        public string NumCuentaOrdenante { get; set; }
        public string NumClabeOrdenante { get; set; }
        public string NumTarjetaOrdenante { get; set; }

        public string NombreBancoBeneficiante { get; set; }
        public string RfcEmisorBeneficiario { get; set; }
        public string NumCuentaBeneficiante { get; set; }
        public string NumClabeBeneficiante { get; set; }
        public string NumTarjetaBeneficiante { get; set; }

        public string NumeroAutorizacion { get; set; }
        public string TipoCadenaPago { get; set; }
        public string FolioIFE { get; set; }

        public string Id_cuentaBancariaOrdenante { get; set; }
        public string Id_cuentaBancariaBeneficiante { get; set; }

        public string NombreEmpresa { get; set; }
        public string NombreProveedor_Cliente { get; set; }

        
        public List<CuentaBancariaModels> ListaCuentasBancariasEmpresa { get; set; }
        public List<CuentaBancariaModels> ListaCuentasBancariasProveedor { get; set; }
    }
}
