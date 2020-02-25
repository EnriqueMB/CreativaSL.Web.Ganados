namespace CreativaSL.Web.Ganados.Models.Dto.ProveedorGanado
{
    public class ReporteProveedorGanadoDto
    {
        public string IdProveedor { get; set; }
        public string Sucursal { get; set; }
        public string TipoProveedor { get; set; }
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
        public string IneUrl { get; set; }
        public string ManifestacionFierroUrl { get; set; }

        #region Mostrar tablas

        public bool MostrarTablaContactos { get; set; }
        public bool MostrarTablaCuentasBancarias { get; set; }
        public bool MostrarTablaDocumentacionExtra { get; set; }
        public bool MostrarTablaCompras { get; set; }
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

        #region Compras

        public string CompraId { get; set; }
        public string CompraFecha { get; set; }
        public decimal CompraMerma { get; set; }
        public int CompraCantidadGanadoMacho { get; set; }
        public int CompraCantidadGanadoHembra { get; set; }
        public int CompraCantidadGanadoTotal { get; set; }
        public decimal CompraKilosGanadoMacho { get; set; }
        public decimal CompraKilosGanadoHembra { get; set; }
        public decimal CompraKilosGanadoTotal { get; set; }
        public decimal CompraImporteGanadoMacho { get; set; }
        public decimal CompraImporteGanadoHembra { get; set; }
        public decimal CompraImporteGanadoTotal { get; set; }
        public decimal CompraImporteDeducciones { get; set; }
        public decimal CompraImporteTotal { get; set; }
        #endregion
    }
}