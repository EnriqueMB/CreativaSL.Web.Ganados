using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class VentaModels2
    {
        public string Id_venta { get; set; }
        public string Id_cliente { get; set; }
        public string Id_flete { get; set; }
        public string Id_documentoXCobrar { get; set; }
        public string Id_recepcion { get; set; }
        public string Id_sucursal { get; set; }
        public string Folio { get; set; }
        public decimal MontoTotal { get; set; }
        public DateTime FechaHoraVenta { get; set; }
        public DateTime FechaHoraEmbarque { get; set; }
        public DateTime FechaHoraLlegada { get; set; }
        public bool CobrarFlete { get; set; }
        public string Observacion { get; set; }
        public string Descripcion_bascula { get; set; }

        public List<CatSucursalesModels> ListaSucursales { get; set; }
        public List<CatClienteModels> ListaClientes { get; set; }

        public FleteModels Flete { get; set; }

        public string Conexion { get; set; }
        public string Usuario { get; set; }
        public RespuestaAjax RespuestaAjax { get; set; }
    }
}

