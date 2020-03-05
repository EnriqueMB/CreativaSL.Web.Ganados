using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models.Dto.Cliente
{
    public class ReporteClienteDto
    {

        public string IdCliente { get; set; }
        public string Sucursal { get; set; }
        public string TipoCliente { get; set; }
        public string RazonSocial_Nombre { get; set; }
        public string Rfc { get; set; }
        public string Direccion { get; set; }
        public string FechaIngreso { get; set; }
        public string Tolerancia { get; set; }
        public string FotoPerfilUrl { get; set; }
        public string Observacion { get; set; }
        public string Telefonos { get; set; }
        public string Email { get; set; }
        public string UppPsgUrl { get; set; }       

        #region Mostrar tablas

        public bool MostrarTablaContactos { get; set; }
        public bool MostrarTablaCuentasBancarias { get; set; }
        public bool MostrarTablaDocumentacionExtra { get; set; }
        public bool MostrarTablaVentas { get; set; }
        #endregion

        #region Contactos
        public string ContactoId { get; set; }
        public string ContactoNombre { get; set; }
        public string ContactoEmail { get; set; }
        public string ContactoTelefono { get; set; }
        public string ContactoDireccion { get; set; }
        public string ContactoObservacion { get; set; }
        #endregion

        #region Cuentas bancarias
        public string CuentaBancariaId { get; set; }
        public string BancoNombre { get; set; }
        public string CuentaBancariaTitular { get; set; }
        public string CuentaBancariaNumTarjeta { get; set; }
        public string CuentaBancariaNumCuenta { get; set; }
        public string CuentaBancariaClabeInterbancaria { get; set; }
        public string CuentaBancariaImagenUrl { get; set; }
        #endregion

        #region Documentos extras
        public string DocumentacionExtraId { get; set; }
        public string DocumentacionExtraTipoDocumentacionExtra { get; set; }
        public string DocumentacionExtraImagenUrl { get; set; }
        #endregion

        #region Ventas

        public string VentaId { get; set; }
        public string VentaFecha { get; set; }
        public decimal VentaMerma { get; set; }
        public int VentaCantidadGanadoMacho { get; set; }
        public int VentaCantidadGanadoHembra { get; set; }
        public int VentaCantidadGanadoTotal { get; set; }
        public decimal VentaKilosGanadoMacho { get; set; }
        public decimal VentaKilosGanadoHembra { get; set; }
        public decimal VentaKilosGanadoTotal { get; set; }
        public decimal VentaImporteGanadoMacho { get; set; }
        public decimal VentaImporteGanadoHembra { get; set; }
        public decimal VentaImporteGanadoTotal { get; set; }
        public decimal VentaImporteDeducciones { get; set; }
        public decimal VentaImporteTotal { get; set; }
        public decimal Flete { get; set; }
        #endregion
    }
}