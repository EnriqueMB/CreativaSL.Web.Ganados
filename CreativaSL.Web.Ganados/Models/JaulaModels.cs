using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class JaulaModels
    {
        #region Datos de control
        public RespuestaAjax RespuestaAjax { get; set; }
        public int Opcion { get; set; }
        public bool Completado { get; set; }
        public string Conexion { get; set; }
        #endregion
        public string IDEmpresa { get; set; }
        public string IDsVentas { get; set; }
        public string IDSucursal { get; set; }
        #region Datos Generales
        public string IDJaula { get; set; }
        public string IDUsuario { get; set; }
        public string Folio { get; set; }        
        public string HoraTraslado { get; set; }
        //[Required(ErrorMessage = "Seleccione una hora de salida")]
        //[Display(Name = "HoraSalida")]
        public TimeSpan HoraSalida { get; set; }
        //[Required(ErrorMessage = "Seleccione una hora de llegada")]
        //[Display(Name = "HoraLlegada")]
        public TimeSpan Horallegada { get; set; }
        //[Display(Name = "Fecha de salida")]
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaSalida { get; set; }
        //[Display(Name = "Fecha de Llegada")]
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Fechallegada { get; set; }
        //[Required(ErrorMessage = "Escriba Obervaciones")]
        //[Display(Name = "Observaciones")]
        public string Observaciones { get; set; }
        //[Required(ErrorMessage = "Seleccione un chofer")]
        //[Display(Name = "IDChofer")]
        public string IDChofer { get; set; }
        //[Required(ErrorMessage = "Seleccione un vehiculo")]
        //[Display(Name = "IDVehiculo")]
        public string IDVehiculo { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Color { get; set; }
        public string PlacaVehiculo { get; set; }
        public string PlacaRemolque { get; set; }
        #endregion
        #region Datos Ganado
        public int CbzMachos { get; set; }
        public int CbzHembra { get; set; }
        public int CbzTotal { get; set; }
        public decimal KgMachos { get; set; }
        public decimal KgHembras { get; set; }
        public decimal KgTotal { get; set; }
        public decimal ImpMacho { get; set; }
        public decimal  ImpHembra { get; set; }
        public decimal  ImpTotal { get; set; }
        public decimal Percepcion { get; set; }
        public decimal  CostoExtra { get; set; }
        public decimal Deduccion { get; set; }
        public decimal Total { get; set; }
        public decimal Pago { get; set; }
        public decimal Pendiente { get; set; }
        
        #endregion
        #region Objetos
        public CatChoferModels Chofer { get; set; }
        public CatVehiculoModels Vehiculo { get; set; }
        public EventoVentaModels Evento { get; set; }
        #endregion
        #region Listas
        public List<CatChoferModels> ListaChoferes { get; set; }
        public List<CatVehiculoModels> ListaVehiculos { get; set; }
        #endregion
        public JaulaModels()
        {
            Fechallegada = DateTime.Now;
            FechaSalida = DateTime.Now;
            ListaVehiculos = new List<CatVehiculoModels>();
            ListaChoferes = new List<CatChoferModels>();
            Chofer = new CatChoferModels();
            Vehiculo = new CatVehiculoModels();
            
        }
        #region Methods
        public void GetListaChoferes()
        {
            if (!string.IsNullOrEmpty(Chofer.IDChofer))
            {
                var item = ListaChoferes.FirstOrDefault(c => c.IDChofer == Chofer.IDChofer);
                if (item == null)
                {
                    ListaChoferes.Add(Chofer);
                    ListaChoferes = ListaChoferes.OrderBy(c => c.Nombre).ToList();
                }
            }

            CatChoferModels cPredeterminado = new CatChoferModels
            {
                IDChofer = string.Empty,
                Nombre = "SELECCIONE UN CHOFER",

            };
            ListaChoferes.Insert(0, cPredeterminado);
        }
        public void GetListadoVehiculos()
        {
            if (!string.IsNullOrEmpty(Vehiculo.IDVehiculo))
            {
                var item = ListaVehiculos.FirstOrDefault(v => v.IDVehiculo == Vehiculo.IDVehiculo);
                if (item == null)
                {
                    ListaVehiculos.Add(Vehiculo);
                    ListaVehiculos = ListaVehiculos.OrderBy(v => v.nombreMarca).ToList();
                }
            }

            CatVehiculoModels vPredeterminado = new CatVehiculoModels
            {
                IDVehiculo = string.Empty,
                nombreMarca = "SELECCIONE UN VEHICULOS",
                Modelo = "GRUPO OCAMPO"
            };
            ListaVehiculos.Insert(0, vPredeterminado);
        }
        #endregion
    }
}