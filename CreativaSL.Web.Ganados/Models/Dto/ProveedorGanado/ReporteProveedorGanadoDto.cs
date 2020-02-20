using System.Collections.Generic;

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

        public string UppPsgBase64 { get; set; }
        public string IneBase64 { get; set; }
        public string ManifestacionFierroBase64 { get; set; }

        #region Mostrar tablas

        public bool MostrarTablaContactos { get; set; }
        public bool MostrarTablaCuentasBancarias { get; set; }
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



    }
}