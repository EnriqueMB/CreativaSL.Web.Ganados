using System;

namespace CreativaSL.Web.Ganados.Models.Dto.CajaChica
{
    public class ReportCajaChicaImagenesDto
    {
        public string IdCajaChicaDetalle { get; set; }
        public string ConceptoSalida { get; set; }
        public string Descripcion { get; set; }
        public string FechaHora { get; set; }
        public decimal Monto { get; set; }
        public string FormaPago { get; set; }
        public string PersonaEntrega { get; set; }
        public string PersonaRecibe { get; set; }
        public string FolioCheque { get; set; }
        public string Alias { get; set; }
        public string Imagen { get; set; }
    }
}