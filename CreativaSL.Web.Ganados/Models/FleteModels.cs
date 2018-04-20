using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class FleteModels
    {
        public FleteModels()
        {
            Cliente = new CatClienteModels();
            Chofer = new CatChoferModels();
            Vehiculo = new CatVehiculoModels();
            Empresa = new CatEmpresaModels();
            Jaula = new CatJaulaModels();
            Remolque = new CatRemolqueModels();
            Trayecto = new TrayectoModels();
            FormaPago = new CFDI_FormaPagoModels();
            MetodoPago = new CFDI_MetodoPagoModels();
            RespuestaAjax = new RespuestaAjax();
        }

        //No tocar
        public int kmInicialVehiculo { get; set; }
        public int kmFinalVehiculo { get; set; }
        public decimal precioFlete { get; set; }
        public string id_flete { get; set; }
        public string id_vehiculo { get; set; }
        public string id_chofer { get; set; }
        public string id_jaula { get; set; }
        public string IDRemolque { get; set; }

        ///Campos unicos de la tabla
        public string Folio { get; set; }
        public decimal TotalImpuestoTrasladado { get; set; }
        public decimal TotalImpuestoRetenido { get; set; }
        public decimal totalFlete { get; set; }
        public DateTime FechaFinalizadoFlete { get; set; }
        public DateTime FechaTentativaEntrega { get; set; }
        public string CondicionPago { get; set; }

        //ID
        public CatEmpresaModels Empresa { get; set; }
        public CatClienteModels Cliente { get; set; }
        public CatVehiculoModels Vehiculo { get; set; }
        public CatChoferModels Chofer { get; set; }
        public CatJaulaModels Jaula { get; set; }
        public CatRemolqueModels Remolque { get; set; }
        public CFDI_MetodoPagoModels MetodoPago { get; set; }
        public CFDI_FormaPagoModels FormaPago { get; set; }

        //Select
        public List<CatEmpresaModels> ListaEmpresa { get; set; }
        public List<CatClienteModels> ListaCliente { get; set; }
        public List<CatVehiculoModels> ListaVehiculo { get; set; }
        public List<CatChoferModels> ListaChofer { get; set; }
        public List<CatJaulaModels> ListaJaula { get; set; }
        public List<CatRemolqueModels> ListaRemolque { get; set; }
        public List<CFDI_FormaPagoModels> ListaFormaPago { get; set; }
        public List<CFDI_MetodoPagoModels> ListaMetodoPago { get; set; }

        //TablaRelacionada
        public FleteImpuestoModels FleteImpuesto { get; set; }
        public TrayectoModels Trayecto { get; set; }

        #region Datos De Control
        public string Conexion { get; set; }
        public int Resultado { get; set; }
        public bool Completado { get; set; }
        public string Usuario { get; set; }
        public int Opcion { get; set; }
        public RespuestaAjax RespuestaAjax { get; set; }
        #endregion
    }
}