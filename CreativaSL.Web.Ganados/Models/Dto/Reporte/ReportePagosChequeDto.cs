namespace CreativaSL.Web.Ganados.Models.Dto.Reporte
{
    public class ReportePagosChequeDto
    {
        public string IdSucursal { get; set; }
        public string NombreSucursal { get; set; }
        public string PagarA { get; set; }
        public decimal Monto { get; set; }
        public string Fecha { get; set; }
    }
}