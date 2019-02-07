using CreativaSL.Web.Ganados.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class RptVentasMermasModels
    {
        public RptVentasMermasModels()
        {
            IDSucursal = string.Empty;
            NombreCliente = string.Empty;
            NombreSucursal = string.Empty;
            FolioVenta = 0;
            FechaFin = DateTime.Today;
            FechaInicial = DateTime.Today;
            FechaVenta = DateTime.Today;
            MermaCliente = 0;
            MermaVenta = 0;
            CabezasHembras = 0;
            CabezasMachos = 0;
            PesosHembras = 0;
            PesosMachos = 0;
            TotalCabezas = 0;
            TotalPesos = 0;
            TotalVenta = 0;
            Flete = string.Empty;
            Conexion = string.Empty;
            _ListaVentaMerma = new List<RptVentasMermasModels>();
            DatosEmpresa = new DatosEmpresaViewModels();
        }
        public string IDSucursal { get; set; }
        public string NombreSucursal { get; set; }
        public long FolioVenta { get; set; }
        public DateTime FechaVenta { get; set; }
        public string NombreCliente { get; set; }
        public decimal MermaCliente { get; set; }
        public int CabezasHembras { get; set; }
        public int PesosHembras { get; set; }
        public int CabezasMachos { get; set; }
        public int PesosMachos { get; set; }
        public int TotalCabezas { get; set; }
        public int TotalPesos { get; set; }
        public string Flete { get; set; }
        public decimal TotalVenta { get; set; }
        public decimal MermaVenta { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFin { get; set; }
        public string Conexion { get; set; }
        private List<RptVentasMermasModels> _ListaVentaMerma;

        public List<RptVentasMermasModels> ListaVentaMerma
        {
            get { return _ListaVentaMerma; }
            set { _ListaVentaMerma = value; }
        }

        private DatosEmpresaViewModels _DatosEmpresa;

        public DatosEmpresaViewModels DatosEmpresa
        {
            get { return _DatosEmpresa; }
            set { _DatosEmpresa = value; }
        }
    }
}