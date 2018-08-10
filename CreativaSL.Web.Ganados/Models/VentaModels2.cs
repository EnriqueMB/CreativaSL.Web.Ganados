using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class VentaModels2
    {
        /// <summary>
        /// Merma extra, solicitado por el cliente
        /// </summary>
        public decimal ME { get; set; }
        public string Id_venta { get; set; }
        public string Id_cliente { get; set; }
        public string Id_flete { get; set; }
        public string Id_documentoXCobrar { get; set; }
        public string Id_recepcion { get; set; }
        public string Id_sucursal { get; set; }
        public string Folio { get; set; }
        public decimal MontoTotal { get; set; }
        public VerificacionJaulaModels VerificacionJaula { get; set; }
        /// <summary>
        /// Fecha y hora que guardará el sistema al finalizar la venta
        /// </summary>
        public DateTime FechaHoraVenta { get; set; }
        public int CobrarFlete { get; set; }
        public string Observacion { get; set; }
        public string Descripcion_bascula { get; set; }
        public string NombreVenta { get; set; }
        public int Opcion { get; set; }

        public List<CatSucursalesModels> ListaSucursales { get; set; }
        public List<CatClienteModels> ListaClientes { get; set; }
        public List<CatLugarModels> ListaLugares { get; set; }

        public FleteModels Flete { get; set; }
        public EventoVentaModels EventoVenta { get; set; }
        public RecepcionOrigenVentaModels RecepcionOrigen { get; set; }

        public string Conexion { get; set; }
        public string Usuario { get; set; }
        public RespuestaAjax RespuestaAjax { get; set; }
        public string ListaIDGanadosParaVender { get; set; }

        public int CbzMachos { get; set; }
        public int CbzHembras { get; set; }
        public int CbzTotal { get; set; }
        public decimal KgMachos { get; set; }
        public decimal KgHembras { get; set; }
        public decimal KgTotal { get; set; }
        public decimal CostoMachos { get; set; }
        public decimal CostoHembras { get; set; }
        public decimal CostoTotal { get; set; }

    }
}

