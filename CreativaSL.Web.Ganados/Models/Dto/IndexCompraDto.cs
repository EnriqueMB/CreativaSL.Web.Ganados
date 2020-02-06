using System;
using Newtonsoft.Json;

namespace CreativaSL.Web.Ganados.Models.Dto
{
    public class IndexCompraDto
    {

        [JsonProperty("id_compra")]
        public string IdCompra { get; set; }

        [JsonProperty("NombreProveedor")]
        public string NombreProveedor { get; set; }

        [JsonProperty("LugarDestino")]
        public string LugarDestino { get; set; }

        [JsonProperty("GanadoPactadoMachos")]
        public int? GanadoPactadoMachos { get; set; }

        [JsonProperty("GanadoPactadoHembras")]
        public int GanadoPactadoHembras { get; set; }

        [JsonProperty("GanadoPactadoTotal")]
        public int GanadoPactadoTotal { get; set; }

        [JsonProperty("GanadoCompradoMachos")]
        public int GanadoCompradoMachos { get; set; }

        [JsonProperty("GanadoCompradoHembras")]
        public int GanadoCompradoHembras { get; set; }

        [JsonProperty("GanadoCompradoTotal")]
        public int GanadoCompradoTotal { get; set; }

        [JsonProperty("GanadoKilosTotal")]
        public decimal GanadoKilosTotal { get; set; }

        [JsonProperty("MermaPromedio")]
        public decimal MermaPromedio { get; set; }

        [JsonProperty("Estatus")]
        public string Estatus { get; set; }

        [JsonProperty("ClassEstatus")]
        public string ClassEstatus { get; set; }

        [JsonProperty("TotalCobros")]
        public decimal TotalCobros { get; set; }

        [JsonProperty("TotalPagos")]
        public decimal TotalPagos { get; set; }

        [JsonProperty("Folio")]
        public string Folio { get; set; }

        [JsonProperty("FechaProgramada")]
        public string FechaProgramada { get; set; }

        [JsonProperty("FechaTerminada")]
        public string FechaTerminada { get; set; }

        [JsonProperty("TotalKilosGanadoMachos")]
        public decimal TotalKilosGanadoMachos { get; set; }

        [JsonProperty("TotalKilosGanadoHembras")]
        public decimal TotalKilosGanadoHembras { get; set; }

        [JsonProperty("Pendiente")]
        public decimal Pendiente { get; set; }
    }
}