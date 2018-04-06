using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CatConceptoDetalleDocumentosModels
    {
        public CatConceptoDetalleDocumentosModels()
        {
            _IDConceptosDocumento = string.Empty;
            _IDTipoConciliacion = 0;
            _Clave = string.Empty;
            _concepto = string.Empty;
            _Descripcion = string.Empty;
            _listaConceptosDetalle = new List<CatConceptoDetalleDocumentosModels>();
            _listTipoConciliacion = new List<CatTipoConciliacionModels>();
        }
        private string _IDConceptosDocumento;

        public string IDConceptosDocumento
        {
            get { return _IDConceptosDocumento; }
            set { _IDConceptosDocumento = value; }
        }

        private int _IDTipoConciliacion;

        public int IDTipoConciliacion
        {
            get { return _IDTipoConciliacion; }
            set { _IDTipoConciliacion = value; }
        }

        private string _Clave;

        public string Clave
        {
            get { return _Clave; }
            set { _Clave = value; }
        }
        private string _concepto;

        public string Concepto
        {
            get { return _concepto; }
            set { _concepto = value; }
        }

        private string _Descripcion;

        public string Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }
        private List<CatConceptoDetalleDocumentosModels> _listaConceptosDetalle;

        public List<CatConceptoDetalleDocumentosModels> listaConceptosDetalles
        {
            get { return _listaConceptosDetalle; }
            set { _listaConceptosDetalle = value; }
        }

        private List<CatTipoConciliacionModels> _listTipoConciliacion;

        public List<CatTipoConciliacionModels> listTipoConciliacion
        {
            get { return _listTipoConciliacion; }
            set { _listTipoConciliacion = value; }
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