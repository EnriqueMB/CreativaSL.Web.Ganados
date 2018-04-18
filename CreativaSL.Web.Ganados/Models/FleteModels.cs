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
            LugarOrigen = new CatLugarModels();
            LugarDestino = new CatLugarModels();
            Remitente = new CatClienteModels();
            Destinatario = new CatClienteModels();
            FormaPago = new CFDI_FormaPagoModels();
            Impuesto = new CFDI_ImpuestoModels();
            MetodoPago = new CFDI_MetodoPagoModels();
            Moneda = new CFDI_MonedaModels();
    }

        //No tocar
        public string id_flete { get; set; }
        public string id_vehiculo { get; set; }
        public string id_chofer { get; set; }
        public string id_jaula { get; set; }
        public int kmInicialVehiculo { get; set; }
        public int kmFinalVehiculo { get; set; }
        public decimal precioFlete { get; set; }
        public string IDRemolque { get; set; }
        ///

        public DateTime FechaEntrega { get; set; }
        public DateTime FechaFlete { get; set; }
        public string Folio { get; set; }
        public string CondicionPago { get; set; }

        public CatClienteModels Cliente { get; set; }
        public CatChoferModels Chofer { get; set; }
        public CatVehiculoModels Vehiculo { get; set; }
        public CatEmpresaModels Empresa { get; set; }
        public CatJaulaModels Jaula { get; set; }
        public CatRemolqueModels Remolque { get; set; }
        public TrayectoModels Trayecto { get; set; }
        public CatLugarModels LugarOrigen { get; set; }
        public CatLugarModels LugarDestino { get; set; }
        public CatClienteModels Remitente { get; set; }
        public CatClienteModels Destinatario { get; set; }
        public CFDI_FormaPagoModels FormaPago { get; set; }
        public CFDI_ImpuestoModels Impuesto { get; set; }
        public CFDI_MetodoPagoModels MetodoPago { get; set; }
        public CFDI_MonedaModels Moneda { get; set; }

        public List<CFDI_FormaPagoModels> ListaFormaPago { get; set; }
        public List<CFDI_ImpuestoModels> ListaImpuestos { get; set; }
        public List<CFDI_MetodoPagoModels> ListaMetodoPago { get; set; }


        #region Datos De Control
        public string Conexion { get; set; }
        public int Resultado { get; set; }
        public bool Completado { get; set; }
        public string Usuario { get; set; }
        public int Opcion { get; set; }
        #endregion
    }
}