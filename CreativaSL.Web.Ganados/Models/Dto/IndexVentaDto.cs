using Newtonsoft.Json;

namespace CreativaSL.Web.Ganados.Models.Dto
{
    public class IndexVentaDto
    {
        [JsonProperty("id_venta")] 
        public string IdVenta { get; set; }

        [JsonProperty("folio")]
        public string Folio { get; set; }

        [JsonProperty("fecha")]
        public string Fecha { get; set; }

        [JsonProperty("nombreContacto")] 
        public string NombreContacto { get; set; }

        [JsonProperty("lugarDestino")] 
        public string LugarDestino { get; set; }

        [JsonProperty("totalGanado")]
        public int TotalGanado { get; set; }

        [JsonProperty("total")]
        public decimal Total { get; set; }

        [JsonProperty("estatus")]
        public string Estatus { get; set; }

        [JsonProperty("css")]
        public string Css { get; set; }

        [JsonProperty("id_estatus")]
        public int  IdEstatus { get; set; }

        [JsonProperty("cantidadGanadoMachos")]
        public int CantidadGanadoMachos { get; set; }

        [JsonProperty("cantidadGanadoHembras")]
        public int CantidadGanadoHembras { get; set; }

        [JsonProperty("kilosGanadoMachos")]
        public decimal KilosGanadoMachos { get; set; }

        [JsonProperty("kilosGanadoHembras")]
        public decimal KilosGanadoHembras { get; set; }

        [JsonProperty("kilosGanadoTotal")]
        public decimal KilosGanadoTotal { get; set; }

        [JsonProperty("nombreSucursal")]
        public string NombreSucursal { get; set; }

        [JsonProperty("draw")]
        public int Draw { get; set; }

        [JsonProperty("RecordsTotal")]
        public decimal RecordsTotal { get; set; }

        [JsonProperty("RecordsFiltered")]
        public decimal RecordsFiltered { get; set; }
    }
}