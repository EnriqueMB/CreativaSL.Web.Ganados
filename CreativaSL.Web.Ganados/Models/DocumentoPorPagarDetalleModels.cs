using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class DocumentoPorPagarDetalleModels
    {
        private string _IDDetalleDoctoPagar;

        public string IDDetalleDoctoPagar
        {
            get { return _IDDetalleDoctoPagar; }
            set { _IDDetalleDoctoPagar = value; }
        }

        private string _IDDocumentoPagar;

        public string IDDocumentoPagar
        {
            get { return _IDDocumentoPagar; }
            set { _IDDocumentoPagar = value; }
        }

        private string _IDTipoConciliacion;

        public string IDTipoConciliacion
        {
            get { return _IDTipoConciliacion; }
            set { _IDTipoConciliacion = value; }
        }

        private string _IDConceptoDocumento;

        public string IDConceptoDocumento
        {
            get { return _IDConceptoDocumento; }
            set { _IDConceptoDocumento = value; }
        }

        private decimal _PrecioUnitario;

        public decimal PrecioUnitario
        {
            get { return _PrecioUnitario; }
            set { _PrecioUnitario = value; }
        }

        private decimal _SubTotal;

        public decimal SubTotal
        {
            get { return _SubTotal; }
            set { _SubTotal = value; }
        }

        #region Datos De Control
        public string Conexion { get; set; }
        public int Resultado { get; set; }
        public bool Completado { get; set; }
        public string Usuario { get; set; }
        public int Opcion { get; set; }
        #endregion
    }
}