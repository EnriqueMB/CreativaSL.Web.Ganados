using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class DocumentosPorCobrarDetallePagosModels
    {
        public decimal pendiente { get; set; }
        public int id_status { get; set; }
        public string Id_compra { get; set; }
        public string Id_documentoPorCobrarDetallePagos { get; set; }
        public string Id_documentoPorCobrar { get; set; }
        public int Id_formaPago { get; set; }
        public decimal Monto { get; set; }
        public string Observacion { get; set; }
        public DateTime fecha { get; set; }

        //public DocumentosPorCobrarDetallePagosBancarizadoModels DocumentoPorCobrarDetallePagosBancarizado;

        public List<CFDI_FormaPagoModels> ListaFormaPagos;
        public List<ListaGenerica> ListaAsignar { get; set; }

        public string Conexion { get; set; }
        public string Usuario { get; set; }
        public RespuestaAjax RespuestaAjax { get; set; }
        public int TipoServicio { get; set; }
        public string Id_padre { get; set; }
        public bool Bancarizado { get; set; }

        //Para desplegar las cuentas bancarias de cada uno
        public string IDProveedor { get; set; }
        public string IDEmpresa { get; set; }
        public int TipoCuentaBancaria { get; set; }

        //imagen
        public string ImagenBase64 { get; set; }
        public string ExtensionImagenBase64 { get; set; }
        public string ImagenMostrar { get; set; }
        public HttpPostedFileBase HttpImagen { get; set; }


        //agregamos los campos de DocumentosPorCobrarDetallePagosBancarizadoModels, ya que no los recibe el controlador??
        public string Id_documentoPorCobrarDetallePagosBancarizado { get; set; }

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

        //vista producto servicio
        public List<CatTipoClasificacionCobroModels> ListaTipoClasificacionCobro { get; set; }
        public List<DocumentosPorCobrarDetallePagosModels> ListaPagosDocumento { get; set; }
        public string Descripcion { get; set; }
        public bool Completado { get; internal set; }
    }
}
