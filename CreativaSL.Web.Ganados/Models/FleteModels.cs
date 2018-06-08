using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            FechaTentativaEntrega = DateTime.Now;
            Documentos = new Flete_TipoDocumentoModels();
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
        public decimal TotalFlete { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FechaFinalizadoFlete { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FechaTentativaEntrega { get; set; }
        public string CondicionPago { get; set; }
        public int Estatus { get; set; }
        public string Id_documentoPorCobrar { get; set; }
        public string Id_empresa { get; set; }

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
        public List<Flete_ProductoModels> ListaProductos { get; set; }

        //TablaRelacionada
        public FleteImpuestoModels FleteImpuesto { get; set; }
        public TrayectoModels Trayecto { get; set; }
        public Flete_TipoDocumentoModels Documentos { get; set; }
        public Flete_ProductoModels Producto { get; set; }
        public RecepcionDestinoModels RecepcionDestino { get; set; }
        public RecepcionOrigenModels RecepcionOrigen { get; set; }

        //doc por cobrar
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