using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Xml;

namespace CreativaSL.Web.Ganados.Models
{
    public class _Venta2_Datos
    {
        #region Datatables
        public string DatatableEventos(VentaModels2 venta)
        {
            try
            {
                object[] parametros = { venta.EventoVenta.Id_venta };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(venta.Conexion, "spCSLDB_Venta_get_DatatableEventos", parametros);
                string datatable = Auxiliar.SqlReaderToJson(dr);
                dr.Close();
                return datatable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string DatatableGanadoVendidoVivo(VentaModels2 venta)
        {
            try
            {
                object[] parametros = { venta.EventoVenta.Id_venta, venta.EventoVenta.Id_eventoVenta };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(venta.Conexion, "spCSLDB_Venta_get_DatatableGanadoVendidoVivo", parametros);
                string datatable = Auxiliar.SqlReaderToJson(dr);
                dr.Close();
                return datatable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string DatatableGanadoActual(VentaModels2 venta)
        {
            try
            {
                object[] parametro = { venta.Id_sucursal };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(venta.Conexion, "spCSLDB_Venta_get_DatatableGanadoActual", parametro);
                string datatable = Auxiliar.SqlReaderToJson(dr);
                dr.Close();
                return datatable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string DatatableGanadoParaVenta(VentaModels2 venta)
        {
            try
            {
                object[] parametros = { venta.Id_venta };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(venta.Conexion, "spCSLDB_Venta_get_DatatableGanadoParaVenta", parametros);
                string datatable = Auxiliar.SqlReaderToJson(dr);
                dr.Close();
                return datatable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string DatatableGanadoConEvento(VentaModels2 venta)
        {
            try
            {
                object[] parametros = { venta.EventoVenta.Id_venta, venta.EventoVenta.Id_eventoVenta };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(venta.Conexion, "spCSLDB_Venta_get_DatatableGanadoConEvento", parametros);
                string datatable = Auxiliar.SqlReaderToJson(dr);
                dr.Close();
                return datatable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string DatatableIndex(VentaModels2 venta)
        {
            try
            {
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(venta.Conexion, "spCSLDB_Venta_get_Index");
                string datatable = Auxiliar.SqlReaderToJson(dr);
                dr.Close();
                return datatable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string DatatableDetallesDocumentoPorCobrarVenta(VentaModels2 Venta)
        {
            try
            {
                object[] parametros = { Venta.Id_venta, Venta.Id_documentoXCobrar };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Venta.Conexion, "spCSLDB_Venta_get_DetallesDocumentoPorCobrar", parametros);
                string datatable = Auxiliar.SqlReaderToJson(dr);
                dr.Close();
                return datatable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string DatatableDetallesDocumentoPorCobrarVentaDeducciones(VentaModels2 Venta)
        {
            try
            {
                object[] parametros = { Venta.Id_venta, Venta.Id_documentoXCobrar };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Venta.Conexion, "spCSLDB_Venta_get_DetallesDocumentoPorCobrarDeducciones", parametros);
                string datatable = Auxiliar.SqlReaderToJson(dr);
                dr.Close();
                return datatable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string DatatableDetallesDocumentoPorCobrarVentaCobro(VentaModels2 Venta)
        {
            try
            {
                object[] parametros = { Venta.Id_venta, Venta.Id_documentoXCobrar };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Venta.Conexion, "spCSLDB_Venta_get_DetallesDocumentoPorCobrarCobros", parametros);
                string datatable = Auxiliar.SqlReaderToJson(dr);
                dr.Close();
                return datatable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string DatatableImpuestoXIDDocumentoDetalle(ImpuestoModels Impuesto)
        {
            try
            {
                object[] parametros =
                {
                    Impuesto.IDModulo,
                    Impuesto.Id_detalleDoctoCobrar,
                    1
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Impuesto.Conexion, "spCSLDB_Impuesto_get_ImpuestoXIDDocumentoDetalle", parametros);
                string datatable = Auxiliar.SqlReaderToJson(dr);
                dr.Close();
                return datatable;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public string DatatableDocumentos(VentaModels2 Venta)
        {
            object[] parametros =
            {
                Venta.Id_venta
            };

            try
            {
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Venta.Conexion, "spCSLDB_Venta_get_DocumentosXIDVenta", parametros);
                string datatable = Auxiliar.SqlReaderToJson(dr);
                dr.Close();
                return datatable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string DatatableGeneralesGanado(VentaModels2 Venta)
        {
            try
            {
                object[] parametros =
                    {
                        Venta.Id_venta
                    };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Venta.Conexion, "spCSLDB_Venta_get_DatatableGeneralesGanado", parametros);
                string datatable = Auxiliar.SqlReaderToJson(dr);
                dr.Close();
                return datatable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string DatatableGanadoMacho(VentaModels2 Venta)
        {
            try
            {
                object[] parametros =
                    {
                        Venta.Id_venta
                    };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Venta.Conexion, "spCSLDB_Venta_get_DetallesGanadoMacho", parametros);
                string datatable = Auxiliar.SqlReaderToJson(dr);
                dr.Close();
                return datatable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string DatatableGanadoHembra(VentaModels2 Venta)
        {
            try
            {
                object[] parametros =
                    {
                        Venta.Id_venta
                    };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Venta.Conexion, "spCSLDB_Venta_get_DetallesGanadoHembra", parametros);
                string datatable = Auxiliar.SqlReaderToJson(dr);
                dr.Close();
                return datatable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string DatatableDetallesDocXcobrar(VentaModels2 Venta)
        {
            try
            {
                object[] parametros =
                    {
                        Venta.Id_venta
                    };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Venta.Conexion, "spCSLDB_Venta_get_DetallesDocXcobrar", parametros);
                string datatable = Auxiliar.SqlReaderToJson(dr);
                dr.Close();
                return datatable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string DatatableDetalleDocXCobrarPagos(VentaModels2 Venta)
        {
            try
            {
                object[] parametros =
                    {
                        Venta.Id_venta
                    };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Venta.Conexion, "spCSLDB_Venta_get_DetallesDocXcobrarPAGOS", parametros);
                string datatable = Auxiliar.SqlReaderToJson(dr);
                dr.Close();
                return datatable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string GetDocumentosDataTable(CompraModels Compra)
        {
            object[] parametros =
            {
                Compra.IDCompra
            };

            try
            {
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Compra_get_DocumentosXIDCompra", parametros);
                string datatable = Auxiliar.SqlReaderToJson(dr);
                dr.Close();
                return datatable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Combos
        #region Sucursales
        public List<CatSucursalesModels> GetListadoSucursales(VentaModels2 Venta)
        {
            List<CatSucursalesModels> ListaSucursales = new List<CatSucursalesModels>();
            CatSucursalesModels Sucursal;
            SqlDataReader dr = null;
            object[] parametros =
            {
                1
            };

            dr = SqlHelper.ExecuteReader(Venta.Conexion, "spCSLDB_Combo_get_SucursalesXTipoEmpresa", parametros);

            while (dr.Read())
            {
                Sucursal = new CatSucursalesModels
                {
                    IDSucursal = !dr.IsDBNull(dr.GetOrdinal("IDSucursal")) ? dr.GetString(dr.GetOrdinal("IDSucursal")) : string.Empty,
                    NombreSucursal = !dr.IsDBNull(dr.GetOrdinal("NombreSucursal")) ? dr.GetString(dr.GetOrdinal("NombreSucursal")) : string.Empty,
                };

                ListaSucursales.Add(Sucursal);
            }
            dr.Close();
            return ListaSucursales;
        }
        #endregion
        #region Empresa
        public List<CatEmpresaModels> GetListadoEmpresas(VentaModels2 Venta)
        {
            try
            {
                CatEmpresaModels Empresa;
                List<CatEmpresaModels> ListaEmpresas = new List<CatEmpresaModels>();
                object[] parametros = { Venta.CobrarFlete };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Venta.Conexion, "spCSLDB_Combo_get_CatEmpresaVenta", parametros);
                while (dr.Read())
                {
                    Empresa = new CatEmpresaModels
                    {
                        IDEmpresa = !dr.IsDBNull(dr.GetOrdinal("IDEmpresa")) ? dr.GetString(dr.GetOrdinal("IDEmpresa")) : string.Empty,
                        RazonFiscal = !dr.IsDBNull(dr.GetOrdinal("NombreEmpresa")) ? dr.GetString(dr.GetOrdinal("NombreEmpresa")) : string.Empty,
                    };

                    ListaEmpresas.Add(Empresa);
                }
                dr.Close();
                return ListaEmpresas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Choferes
        public List<CatChoferModels> GetChoferesXIDEmpresa(VentaModels2 Venta)
        {
            try
            {
                object[] parametros =
                {
                    Venta.Flete.Id_empresa ,
                    Venta.Id_sucursal

                };
                CatChoferModels Chofer;
                List<CatChoferModels> ListaChoferes = new List<CatChoferModels>();

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Venta.Conexion, "spCSLDB_Combo_get_CatChoferesXIDEmpresa", parametros);
                while (dr.Read())
                {
                    Chofer = new CatChoferModels
                    {
                        IDChofer = !dr.IsDBNull(dr.GetOrdinal("IDChofer")) ? dr.GetString(dr.GetOrdinal("IDChofer")) : string.Empty,
                        Nombre = !dr.IsDBNull(dr.GetOrdinal("NombreCompleto")) ? dr.GetString(dr.GetOrdinal("NombreCompleto")) : string.Empty,
                    };
                    ListaChoferes.Add(Chofer);
                }
                dr.Close();
                return ListaChoferes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CatChoferModels> GetChoferesAuxiliares(VentaModels2 Venta)
        {
            try
            {
                object[] parametros =
                {
                   Venta.Id_sucursal
                };
                CatChoferModels Chofer;
                List<CatChoferModels> ListaChoferes = new List<CatChoferModels>();

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Venta.Conexion, "spCSLDB_Combo_get_CatChoferesXSucursal", parametros);
                while (dr.Read())
                {
                    Chofer = new CatChoferModels
                    {
                        IDChofer = !dr.IsDBNull(dr.GetOrdinal("IDChofer")) ? dr.GetString(dr.GetOrdinal("IDChofer")) : string.Empty,
                        Nombre = !dr.IsDBNull(dr.GetOrdinal("NombreCompleto")) ? dr.GetString(dr.GetOrdinal("NombreCompleto")) : string.Empty,
                    };
                    ListaChoferes.Add(Chofer);
                }
                dr.Close();
                return ListaChoferes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
        #region Vehiculos
        public List<CatVehiculoModels> GetVehiculosXIDEmpresa(VentaModels2 Venta)
        {
            try
            {
                object[] parametros =
                {
                    Venta.Flete.Id_empresa ,
                    Venta.Id_sucursal
                };
                CatVehiculoModels Vehiculo;
                List<CatVehiculoModels> ListaVehiculos = new List<CatVehiculoModels>();

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Venta.Conexion, "spCSLDB_Combo_get_CatVehiculosXIDEmpresa", parametros);
                while (dr.Read())
                {
                    Vehiculo = new CatVehiculoModels
                    {
                        IDVehiculo = !dr.IsDBNull(dr.GetOrdinal("IDVehiculo")) ? dr.GetString(dr.GetOrdinal("IDVehiculo")) : string.Empty,
                        nombreMarca = !dr.IsDBNull(dr.GetOrdinal("NombreVehiculo")) ? dr.GetString(dr.GetOrdinal("NombreVehiculo")) : string.Empty,
                        Modelo = !dr.IsDBNull(dr.GetOrdinal("Tipo")) ? dr.GetString(dr.GetOrdinal("Tipo")) : string.Empty
                    };

                    ListaVehiculos.Add(Vehiculo);
                }
                dr.Close();
                return ListaVehiculos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Lugares Empresa
        public List<CatLugarModels> GetListadoLugaresEmpresa(VentaModels2 Venta)
        {
            try
            {
                List<CatLugarModels> ListaLugares = new List<CatLugarModels>();
                CatLugarModels Lugar = new CatLugarModels();
                object[] parametros = { Venta.Flete.Id_empresa };

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Venta.Conexion, "spCSLDB_Combo_get_CatLugarXIDEmpresa2", parametros);
                while (dr.Read())
                {
                    Lugar = new CatLugarModels
                    {
                        id_lugar = !dr.IsDBNull(dr.GetOrdinal("IDLugar")) ? dr.GetString(dr.GetOrdinal("IDLugar")) : string.Empty,
                        descripcion = !dr.IsDBNull(dr.GetOrdinal("NombreLugar")) ? dr.GetString(dr.GetOrdinal("NombreLugar")) : string.Empty,
                        latitud = float.Parse(dr["GpsLatitud"].ToString()),
                        longitud = float.Parse(dr["GpsLongitud"].ToString()),
                    };

                    ListaLugares.Add(Lugar);
                }
                dr.Close();
                return ListaLugares;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Lugares del cliente
        public List<CatLugarModels> GetListadoLugaresCliente(VentaModels2 Venta)
        {
            try
            {
                List<CatLugarModels> ListaLugares = new List<CatLugarModels>();
                CatLugarModels Lugar = new CatLugarModels();
                object[] parametros = { Venta.Id_cliente };

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Venta.Conexion, "spCSLDB_Combo_get_CatLugarXIDCliente", parametros);
                while (dr.Read())
                {
                    Lugar = new CatLugarModels
                    {
                        id_lugar = !dr.IsDBNull(dr.GetOrdinal("IDLugar")) ? dr.GetString(dr.GetOrdinal("IDLugar")) : string.Empty,
                        descripcion = !dr.IsDBNull(dr.GetOrdinal("NombreLugar")) ? dr.GetString(dr.GetOrdinal("NombreLugar")) : string.Empty,
                        latitud = float.Parse(dr["GpsLatitud"].ToString()),
                        longitud = float.Parse(dr["GpsLongitud"].ToString()),
                    };

                    ListaLugares.Add(Lugar);
                }
                dr.Close();
                return ListaLugares;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Clientes
        public List<CatClienteModels> GetCatClientes(VentaModels2 Venta)
        {
            try
            {
                object[] parametro = { Venta.Id_sucursal };
                CatClienteModels Cliente;
                List<CatClienteModels> ListaClientes = new List<CatClienteModels>();

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Venta.Conexion, "spCSLDB_Combo_get_CatClienteConLugares", parametro);
                while (dr.Read())
                {
                    Cliente = new CatClienteModels
                    {
                        IDCliente = !dr.IsDBNull(dr.GetOrdinal("ID")) ? dr.GetString(dr.GetOrdinal("ID")) : string.Empty,
                        NombreRazonSocial = !dr.IsDBNull(dr.GetOrdinal("Nombre")) ? dr.GetString(dr.GetOrdinal("Nombre")) : string.Empty
                    };

                    ListaClientes.Add(Cliente);
                }
                dr.Close();
                return ListaClientes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Tipos eventos
        public List<CatTipoEventoEnvioModels> GetTiposEventos(VentaModels2 Venta)
        {
            try
            {
                CatTipoEventoEnvioModels item;
                List<CatTipoEventoEnvioModels> lista = new List<CatTipoEventoEnvioModels>();

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Venta.Conexion, "spCSLDB_Combo_get_CatTipoEventoEnvio");
                while (dr.Read())
                {
                    item = new CatTipoEventoEnvioModels();

                    item.IDTipoEventoEnvio = !dr.IsDBNull(dr.GetOrdinal("idTipoEvento")) ? dr.GetInt32(dr.GetOrdinal("idTipoEvento")) : 0;
                    item.Descripcion = !dr.IsDBNull(dr.GetOrdinal("nombreEvento")) ? dr.GetString(dr.GetOrdinal("nombreEvento")) : string.Empty;
                    item.MarcarMerma = !dr.IsDBNull(dr.GetOrdinal("marcarMerma")) ? dr.GetBoolean(dr.GetOrdinal("marcarMerma")) : false;

                    lista.Add(item);
                }
                dr.Close();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Tipos deduccion venta
        /// <summary>
        /// la deducción de la venta de ganado
        /// </summary>
        /// <param name="Venta"></param>
        /// <returns></returns>
        public List<CatTipoClasificacionCobroModels> GetTiposDeduccionVentaGanado(VentaModels2 Venta)
        {
            try
            {
                CatTipoClasificacionCobroModels item;
                List<CatTipoClasificacionCobroModels> lista = new List<CatTipoClasificacionCobroModels>();

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Venta.Conexion, "spCSLDB_Combo_get_CatTipoClasificacionCobro");
                while (dr.Read())
                {
                    item = new CatTipoClasificacionCobroModels();

                    item.Id_tipoClasificacionCobro = !dr.IsDBNull(dr.GetOrdinal("ID")) ? dr.GetInt16(dr.GetOrdinal("ID")) : 0;
                    item.Descripcion = !dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? dr.GetString(dr.GetOrdinal("Descripcion")) : string.Empty;

                    lista.Add(item);
                }
                dr.Close();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Tipos deduccion venta
        /// <summary>
        /// la deducción de la venta que absorvera la empresa, por lo tanto es un documento por pagar
        /// </summary>
        /// <param name="Venta"></param>
        /// <returns></returns>
        public List<CatTipoClasificacionModels> GetTiposDeduccion(VentaModels2 Venta)
        {
            try
            {
                CatTipoClasificacionModels item;
                List<CatTipoClasificacionModels> lista = new List<CatTipoClasificacionModels>();

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Venta.Conexion, "spCSLDB_Combo_get_CatTipoClasificacionAll");
                while (dr.Read())
                {
                    item = new CatTipoClasificacionModels();

                    item.IDTipoClasificacionGasto = !dr.IsDBNull(dr.GetOrdinal("IDTipoClasificacion")) ? dr.GetInt32(dr.GetOrdinal("IDTipoClasificacion")) : 0;
                    item.Descripcion = !dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? dr.GetString(dr.GetOrdinal("Descripcion")) : string.Empty;

                    lista.Add(item);
                }
                dr.Close();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

        #region Tipos deduccion venta
        public List<CatTipoClasificacionCobroModels> GetTiposDeduccionCobro(VentaModels2 Venta)
        {
            try
            {
                CatTipoClasificacionCobroModels item;
                List<CatTipoClasificacionCobroModels> lista = new List<CatTipoClasificacionCobroModels>();

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Venta.Conexion, "spCSLDB_Combo_get_CatTipoClasificacionCobro");
                while (dr.Read())
                {
                    item = new CatTipoClasificacionCobroModels();

                    item.Id_tipoClasificacionCobro = !dr.IsDBNull(dr.GetOrdinal("ID")) ? dr.GetInt16(dr.GetOrdinal("ID")) : 0;
                    item.Descripcion = !dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? dr.GetString(dr.GetOrdinal("Descripcion")) : string.Empty;

                    lista.Add(item);
                }
                dr.Close();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        public List<CFDI_FormaPagoModels> GetListadoFormaPagos(VentaModels2 Venta)
        {

            try
            {
                CFDI_FormaPagoModels FormaPago;
                List<CFDI_FormaPagoModels> ListaFormaPago = new List<CFDI_FormaPagoModels>();

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Venta.Conexion, "spCSLDB_Combo_get_CFDIFormaPago");
                while (dr.Read())
                {
                    FormaPago = new CFDI_FormaPagoModels
                    {
                        Clave = !dr.IsDBNull(dr.GetOrdinal("ID")) ? dr.GetInt16(dr.GetOrdinal("ID")) : 0,
                        Descripcion = !dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? dr.GetString(dr.GetOrdinal("Descripcion")) : string.Empty,
                    };

                    ListaFormaPago.Add(FormaPago);
                }
                dr.Close();
                return ListaFormaPago;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CFDI_MetodoPagoModels> GetListadoMetodoPago(VentaModels2 Venta)
        {

            try
            {
                CFDI_MetodoPagoModels MetodoPago;
                List<CFDI_MetodoPagoModels> ListaMetodoPago = new List<CFDI_MetodoPagoModels>();

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Venta.Conexion, "spCSLDB_Combo_get_CFDIMetodoPago");
                while (dr.Read())
                {
                    MetodoPago = new CFDI_MetodoPagoModels
                    {
                        Clave = !dr.IsDBNull(dr.GetOrdinal("ID")) ? dr.GetString(dr.GetOrdinal("ID")) : string.Empty,
                        Descripcion = !dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? dr.GetString(dr.GetOrdinal("Descripcion")) : string.Empty,
                    };

                    ListaMetodoPago.Add(MetodoPago);
                }
                dr.Close();
                return ListaMetodoPago;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #region CFDI Forma de pago
        public List<CFDI_FormaPagoModels> GetListadoCFDIFormaPago(DocumentosPorCobrarDetallePagosModels DocumentoPorCobrarDetallePagos)
        {
            try
            {
                CFDI_FormaPagoModels item;
                List<CFDI_FormaPagoModels> lista = new List<CFDI_FormaPagoModels>();
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(DocumentoPorCobrarDetallePagos.Conexion, "spCSLDB_Combo_get_CFDIFormaPago");
                while (dr.Read())
                {
                    item = new CFDI_FormaPagoModels
                    {
                        Clave = !dr.IsDBNull(dr.GetOrdinal("ID")) ? dr.GetInt16(dr.GetOrdinal("ID")) : 0,
                        Descripcion = !dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? dr.GetString(dr.GetOrdinal("Descripcion")) : string.Empty,
                        Bancarizado = !dr.IsDBNull(dr.GetOrdinal("Bancarizado")) ? dr.GetInt32(dr.GetOrdinal("Bancarizado")) : 0,
                    };
                    lista.Add(item);
                }
                dr.Close();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Tipo de clasificacion
        public List<CatTipoClasificacionCobroModels> GetListadoTipoClasificacion(DocumentosPorCobrarDetalleModels DocumentosPorCobrarModels)
        {
            try
            {
                CatTipoClasificacionCobroModels item;
                List<CatTipoClasificacionCobroModels> lista = new List<CatTipoClasificacionCobroModels>();
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(DocumentosPorCobrarModels.Conexion, "spCSLDB_Combo_get_CatTipoClasificacionCobro");
                while (dr.Read())
                {
                    item = new CatTipoClasificacionCobroModels
                    {
                        Id_tipoClasificacionCobro = !dr.IsDBNull(dr.GetOrdinal("ID")) ? dr.GetInt16(dr.GetOrdinal("ID")) : 0,
                        Descripcion = !dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? dr.GetString(dr.GetOrdinal("Descripcion")) : string.Empty,
                        Inventario = !dr.IsDBNull(dr.GetOrdinal("Inventario")) ? dr.GetBoolean(dr.GetOrdinal("Inventario")) : false
                    };
                    lista.Add(item);
                }
                dr.Close();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region CFDI Productos y servicios
        public List<CFDI_ProductoServicioModels> GetListadoCFDIProductosServiciosCompra(DocumentosPorCobrarDetalleModels DocumentosPorCobrarModels)
        {
            try
            {
                CFDI_ProductoServicioModels item;
                List<CFDI_ProductoServicioModels> lista = new List<CFDI_ProductoServicioModels>();
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(DocumentosPorCobrarModels.Conexion, "spCSLDB_Combo_get_CFDIProductoServicioCompra");
                while (dr.Read())
                {
                    item = new CFDI_ProductoServicioModels
                    {
                        Clave = !dr.IsDBNull(dr.GetOrdinal("ID")) ? dr.GetString(dr.GetOrdinal("ID")) : string.Empty,
                        Descripcion = !dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? dr.GetString(dr.GetOrdinal("Descripcion")) : string.Empty,
                    };
                    lista.Add(item);
                }
                dr.Close();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Almacenes con producto habilitado para generar un detalle del documento por cobrar
        public List<CatAlmacenModels> GetAlmacenesHabilitados(DocumentosPorCobrarDetalleModels DocumentoPorCobrarDetallePagos)
        {
            try
            {
                SqlDataReader dr = null;
                List<CatAlmacenModels> lista = new List<CatAlmacenModels>();
                CatAlmacenModels item;
                dr = SqlHelper.ExecuteReader(DocumentoPorCobrarDetallePagos.Conexion, "spCSLDB_Combo_get_Almacenes_DocumentoDetalleCobro");
                while (dr.Read())
                {
                    item = new CatAlmacenModels();
                    item.IDAlmacen = !dr.IsDBNull(dr.GetOrdinal("id_almacen")) ? dr.GetString(dr.GetOrdinal("id_almacen")) : string.Empty;
                    item.Descripcion = !dr.IsDBNull(dr.GetOrdinal("descripcion")) ? dr.GetString(dr.GetOrdinal("descripcion")) : string.Empty;
                    lista.Add(item);
                }
                dr.Close();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Almacenes con producto habilitado para cobrar
        public List<CatProductosAlmacenModels> GetProductosAlmacen(DocumentosPorCobrarDetalleModels Documento)
        {
            try
            {
                object[] parametro =
                {
                    Documento.Id_almacen
                };
                SqlDataReader dr = null;
                List<CatProductosAlmacenModels> lista = new List<CatProductosAlmacenModels>();
                CatProductosAlmacenModels item;
                dr = SqlHelper.ExecuteReader(Documento.Conexion, "spCSLDB_combos_get_ProductosAlmacen_DocumentoPorCobrarDetalle", parametro);
                while (dr.Read())
                {
                    item = new CatProductosAlmacenModels();
                    item.IDProductoAlmacen = !dr.IsDBNull(dr.GetOrdinal("id_productoAlmacen")) ? dr.GetString(dr.GetOrdinal("id_productoAlmacen")) : string.Empty;
                    item.Nombre = !dr.IsDBNull(dr.GetOrdinal("nombre")) ? dr.GetString(dr.GetOrdinal("nombre")) : string.Empty;
                    item.Existencia = !dr.IsDBNull(dr.GetOrdinal("existencia")) ? dr.GetDecimal(dr.GetOrdinal("existencia")) : 0;
                    item.PrecioUnidad = !dr.IsDBNull(dr.GetOrdinal("precioUnidad")) ? dr.GetDecimal(dr.GetOrdinal("precioUnidad")) : 0;
                    item.Id_unidadProducto = !dr.IsDBNull(dr.GetOrdinal("id_unidadProducto")) ? dr.GetString(dr.GetOrdinal("id_unidadProducto")) : string.Empty;
                    lista.Add(item);
                }
                dr.Close();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        public List<CFDI_ImpuestoModels> GetListadoImpuesto(ImpuestoModels ImpuestoVenta)
        {

            try
            {
                CFDI_ImpuestoModels Impuesto;
                ImpuestoVenta.ListaImpuesto = new List<CFDI_ImpuestoModels>();

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(ImpuestoVenta.Conexion, "spCSLDB_Combo_get_CFDIImpuesto");
                while (dr.Read())
                {
                    Impuesto = new CFDI_ImpuestoModels
                    {
                        Clave = !dr.IsDBNull(dr.GetOrdinal("ID")) ? dr.GetInt16(dr.GetOrdinal("ID")) : 0,
                        Descripcion = !dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? dr.GetString(dr.GetOrdinal("Descripcion")) : string.Empty,
                    };

                    ImpuestoVenta.ListaImpuesto.Add(Impuesto);
                }
                dr.Close();
                return ImpuestoVenta.ListaImpuesto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CFDI_TipoImpuestoModels> GetListadoTipoImpuesto(ImpuestoModels ImpuestoVenta)
        {

            try
            {
                CFDI_TipoImpuestoModels TipoImpuesto;
                ImpuestoVenta.ListaTipoImpuesto = new List<CFDI_TipoImpuestoModels>();

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(ImpuestoVenta.Conexion, "spCSLDB_Combo_get_CFDITipoImpuesto");
                while (dr.Read())
                {
                    TipoImpuesto = new CFDI_TipoImpuestoModels
                    {
                        Clave = !dr.IsDBNull(dr.GetOrdinal("ID")) ? dr.GetInt16(dr.GetOrdinal("ID")) : 0,
                        Descripcion = !dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? dr.GetString(dr.GetOrdinal("Descripcion")) : string.Empty,
                    };

                    ImpuestoVenta.ListaTipoImpuesto.Add(TipoImpuesto);
                }
                dr.Close();
                return ImpuestoVenta.ListaTipoImpuesto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CFDI_TipoFactorModels> GetListadoTipoFactor(ImpuestoModels ImpuestoVenta)
        {

            try
            {
                CFDI_TipoFactorModels TipoFactor;
                ImpuestoVenta.ListaTipoFactor = new List<CFDI_TipoFactorModels>();

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(ImpuestoVenta.Conexion, "spCSLDB_Combo_get_CFDITipoFactor");
                while (dr.Read())
                {
                    TipoFactor = new CFDI_TipoFactorModels
                    {
                        Clave = !dr.IsDBNull(dr.GetOrdinal("ID")) ? dr.GetInt16(dr.GetOrdinal("ID")) : 0,
                        Descripcion = !dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? dr.GetString(dr.GetOrdinal("Descripcion")) : string.Empty,
                    };

                    ImpuestoVenta.ListaTipoFactor.Add(TipoFactor);
                }
                dr.Close();
                return ImpuestoVenta.ListaTipoFactor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CatTipoDocumentoModels> GetListaTiposDocumentos(DocumentoModels Documento)
        {
            try
            {
                CatTipoDocumentoModels TipoDocumento;
                List<CatTipoDocumentoModels> ListaTipoDocumentos = new List<CatTipoDocumentoModels>();

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Documento.Conexion, "spCSLDB_Combo_get_CatTipoDocumento2");
                while (dr.Read())
                {
                    TipoDocumento = new CatTipoDocumentoModels
                    {
                        IDTipoDocumento = !dr.IsDBNull(dr.GetOrdinal("id_tipoDocumento")) ? dr.GetInt16(dr.GetOrdinal("id_tipoDocumento")) : 0,
                        Descripcion = !dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? dr.GetString(dr.GetOrdinal("Descripcion")) : string.Empty,
                    };

                    ListaTipoDocumentos.Add(TipoDocumento);
                }
                dr.Close();
                return ListaTipoDocumentos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Vistas
        #region VentaFlete
        public VentaModels2 GetVentaFlete(VentaModels2 Venta)
        {
            object[] parametros =
            {
                Venta.Id_venta
            };
            SqlDataReader dr = null;
            dr = SqlHelper.ExecuteReader(Venta.Conexion, "spCSLDB_Venta_get_VentaFleteXIDVenta", parametros);

            while (dr.Read())
            {
                Venta.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : false;

                if (Venta.RespuestaAjax.Success)
                {
                    Venta.Id_cliente = !dr.IsDBNull(dr.GetOrdinal("id_cliente")) ? dr.GetString(dr.GetOrdinal("id_cliente")) : string.Empty;
                    Venta.Id_documentoXCobrar = !dr.IsDBNull(dr.GetOrdinal("id_documentoXCobrar")) ? dr.GetString(dr.GetOrdinal("id_documentoXCobrar")) : string.Empty;
                    Venta.Id_sucursal = !dr.IsDBNull(dr.GetOrdinal("id_sucursal")) ? dr.GetString(dr.GetOrdinal("id_sucursal")) : string.Empty;
                    Venta.Folio = !dr.IsDBNull(dr.GetOrdinal("folio")) ? dr.GetString(dr.GetOrdinal("folio")) : string.Empty;
                    Venta.CobrarFlete = !dr.IsDBNull(dr.GetOrdinal("cobrarFlete")) ? dr.GetInt16(dr.GetOrdinal("cobrarFlete")) : 0;
                    Venta.Descripcion_bascula = !dr.IsDBNull(dr.GetOrdinal("descripcion_bascula")) ? dr.GetString(dr.GetOrdinal("descripcion_bascula")) : string.Empty;
                    Venta.Observacion = !dr.IsDBNull(dr.GetOrdinal("observacion")) ? dr.GetString(dr.GetOrdinal("observacion")) : string.Empty;
                    Venta.Flete.precioFlete = !dr.IsDBNull(dr.GetOrdinal("precioFlete")) ? dr.GetDecimal(dr.GetOrdinal("precioFlete")) : 0;
                    Venta.Flete.Id_documentoPorCobrar = !dr.IsDBNull(dr.GetOrdinal("id_documentoPorCobrarFlete")) ? dr.GetString(dr.GetOrdinal("id_documentoPorCobrarFlete")) : string.Empty;
                    Venta.Flete.id_vehiculo = !dr.IsDBNull(dr.GetOrdinal("id_vehiculo")) ? dr.GetString(dr.GetOrdinal("id_vehiculo")) : string.Empty;
                    Venta.Flete.id_chofer = !dr.IsDBNull(dr.GetOrdinal("id_chofer")) ? dr.GetString(dr.GetOrdinal("id_chofer")) : string.Empty;

                    Venta.Flete.FechaEmbarque = !dr.IsDBNull(dr.GetOrdinal("fechaEmbarque")) ? dr.GetDateTime(dr.GetOrdinal("fechaEmbarque")) : DateTime.Now;
                    Venta.Flete.HoraEmbarque = !dr.IsDBNull(dr.GetOrdinal("horaEmbarque")) ? dr.GetTimeSpan(dr.GetOrdinal("horaEmbarque")) : DateTime.Now.TimeOfDay;

                    Venta.Flete.FechaSalida = !dr.IsDBNull(dr.GetOrdinal("fechaSalida")) ? dr.GetDateTime(dr.GetOrdinal("fechaSalida")) : DateTime.Now;
                    Venta.Flete.HoraSalida = !dr.IsDBNull(dr.GetOrdinal("horaSalida")) ? dr.GetTimeSpan(dr.GetOrdinal("horaSalida")) : DateTime.Now.TimeOfDay;

                    Venta.NombreVenta = !dr.IsDBNull(dr.GetOrdinal("nombreVenta")) ? dr.GetString(dr.GetOrdinal("nombreVenta")) : string.Empty;
                    Venta.Flete.VerificacionJaula.LimpiezaCompleta = !dr.IsDBNull(dr.GetOrdinal("limpiezaCompleta")) ? dr.GetBoolean(dr.GetOrdinal("limpiezaCompleta")) : false;
                    Venta.Flete.VerificacionJaula.LlantaRefaccion = !dr.IsDBNull(dr.GetOrdinal("llantaRefaccion")) ? dr.GetBoolean(dr.GetOrdinal("llantaRefaccion")) : false;
                    Venta.Flete.VerificacionJaula.LlantasBuenEstado = !dr.IsDBNull(dr.GetOrdinal("llantasBuenEstado")) ? dr.GetBoolean(dr.GetOrdinal("llantasBuenEstado")) : false;
                    Venta.Flete.VerificacionJaula.PisoAntiadherente = !dr.IsDBNull(dr.GetOrdinal("pisoAntiadherente")) ? dr.GetBoolean(dr.GetOrdinal("pisoAntiadherente")) : false;
                    Venta.Flete.VerificacionJaula.PuertasInternas = !dr.IsDBNull(dr.GetOrdinal("puertasInternas")) ? dr.GetBoolean(dr.GetOrdinal("puertasInternas")) : false;
                    Venta.Flete.VerificacionJaula.RiesgosPunzoCortantes = !dr.IsDBNull(dr.GetOrdinal("riesgosPunzoCortantes")) ? dr.GetBoolean(dr.GetOrdinal("riesgosPunzoCortantes")) : false;
                    Venta.Flete.VerificacionJaula.SoloPiso = !dr.IsDBNull(dr.GetOrdinal("soloPiso")) ? dr.GetBoolean(dr.GetOrdinal("soloPiso")) : false;
                    Venta.Flete.VerificacionJaula.Sucia = !dr.IsDBNull(dr.GetOrdinal("sucia")) ? dr.GetBoolean(dr.GetOrdinal("sucia")) : false;
                    Venta.Flete.NumFleje = !dr.IsDBNull(dr.GetOrdinal("numFleje")) ? dr.GetString(dr.GetOrdinal("numFleje")) : string.Empty;
                    Venta.Flete.kmInicialVehiculo = !dr.IsDBNull(dr.GetOrdinal("kmInicialVehiculo")) ? dr.GetInt32(dr.GetOrdinal("kmInicialVehiculo")) : 0;
                    Venta.Flete.Id_empresa = !dr.IsDBNull(dr.GetOrdinal("id_empresa")) ? dr.GetString(dr.GetOrdinal("id_empresa")) : string.Empty;
                    Venta.Flete.Trayecto.id_lugarOrigen = !dr.IsDBNull(dr.GetOrdinal("id_lugarOrigen")) ? dr.GetString(dr.GetOrdinal("id_lugarOrigen")) : string.Empty;
                    Venta.Flete.Trayecto.id_lugarDestino = !dr.IsDBNull(dr.GetOrdinal("id_lugarDestino")) ? dr.GetString(dr.GetOrdinal("id_lugarDestino")) : string.Empty;

                    Venta.Flete.FechaTentativaEntrega = !dr.IsDBNull(dr.GetOrdinal("fechaTentativaEntrega")) ? dr.GetDateTime(dr.GetOrdinal("fechaTentativaEntrega")) : DateTime.Now;
                    Venta.Flete.CondicionPago = !dr.IsDBNull(dr.GetOrdinal("condicionPago")) ? dr.GetString(dr.GetOrdinal("condicionPago")) : string.Empty;
                    Venta.Flete.Id_metodoPago = !dr.IsDBNull(dr.GetOrdinal("id_metodoPago")) ? dr.GetString(dr.GetOrdinal("id_metodoPago")) : string.Empty;
                    Venta.Flete.Id_formaPago = !dr.IsDBNull(dr.GetOrdinal("id_formaPago")) ? dr.GetInt16(dr.GetOrdinal("id_formaPago")) : 0;
                    Venta.TipoVenta = !dr.IsDBNull(dr.GetOrdinal("id_tipoVenta")) ? dr.GetInt16(dr.GetOrdinal("id_tipoVenta")) : 0;
                    Venta.Flete.id_choferAuxilar = !dr.IsDBNull(dr.GetOrdinal("id_choferAuxiliar")) ? dr.GetString(dr.GetOrdinal("id_choferAuxiliar")) : string.Empty;
                }
                else
                {
                    Venta.RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                }
            }
            dr.Close();
            return Venta;
        }
        #endregion
        #region VentaGanado
        public VentaModels2 GetVentaGanado(VentaModels2 Venta)
        {
            object[] parametros =
            {
                Venta.Id_venta
            };
            SqlDataReader dr = null;
            dr = SqlHelper.ExecuteReader(Venta.Conexion, "spCSLDB_Venta_get_VentaGanadoXIDVenta", parametros);

            while (dr.Read())
            {
                Venta.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : false;

                if (Venta.RespuestaAjax.Success)
                {
                    Venta.CbzMachos = !dr.IsDBNull(dr.GetOrdinal("cbzMachos")) ? dr.GetInt32(dr.GetOrdinal("cbzMachos")) : 0;
                    Venta.CbzHembras = !dr.IsDBNull(dr.GetOrdinal("cbzHembras")) ? dr.GetInt32(dr.GetOrdinal("cbzHembras")) : 0;
                    Venta.CbzTotal = !dr.IsDBNull(dr.GetOrdinal("cbzTotal")) ? dr.GetInt32(dr.GetOrdinal("cbzTotal")) : 0;
                    Venta.KgMachos = !dr.IsDBNull(dr.GetOrdinal("kgMachos")) ? dr.GetDecimal(dr.GetOrdinal("kgMachos")) : 0;
                    Venta.KgHembras = !dr.IsDBNull(dr.GetOrdinal("kgHembras")) ? dr.GetDecimal(dr.GetOrdinal("kgHembras")) : 0;
                    Venta.KgTotal = !dr.IsDBNull(dr.GetOrdinal("kgTotal")) ? dr.GetDecimal(dr.GetOrdinal("kgTotal")) : 0;
                    Venta.CostoMachos = !dr.IsDBNull(dr.GetOrdinal("costoMachos")) ? dr.GetDecimal(dr.GetOrdinal("costoMachos")) : 0;
                    Venta.CostoHembras = !dr.IsDBNull(dr.GetOrdinal("costoHembras")) ? dr.GetDecimal(dr.GetOrdinal("costoHembras")) : 0;
                    Venta.CostoTotal = !dr.IsDBNull(dr.GetOrdinal("costoTotal")) ? dr.GetDecimal(dr.GetOrdinal("costoTotal")) : 0;
                    Venta.ME = !dr.IsDBNull(dr.GetOrdinal("me")) ? dr.GetDecimal(dr.GetOrdinal("me")) : 0;
                    Venta.Id_sucursal = !dr.IsDBNull(dr.GetOrdinal("id_sucursal")) ? dr.GetString(dr.GetOrdinal("id_sucursal")) : string.Empty;
                    Venta.TipoVenta = !dr.IsDBNull(dr.GetOrdinal("tipoVenta")) ? dr.GetInt16(dr.GetOrdinal("tipoVenta")) : 0;
                    Venta.MontoTotalGanado = !dr.IsDBNull(dr.GetOrdinal("montoTotalGanado")) ? dr.GetDecimal(dr.GetOrdinal("montoTotalGanado")) : 0;
                    Venta.MermaExtraMachos = !dr.IsDBNull(dr.GetOrdinal("mermaExtraMachos")) ? dr.GetDecimal(dr.GetOrdinal("mermaExtraMachos")) : 0;
                    Venta.MermaExtraHembras = !dr.IsDBNull(dr.GetOrdinal("mermaExtraHembras")) ? dr.GetDecimal(dr.GetOrdinal("mermaExtraHembras")) : 0;
                }
                else
                {
                    Venta.RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                }
            }
            dr.Close();
            return Venta;
        }
        #endregion
        #region VentaEventoRecepcion
        public VentaModels2 GetVentaEventoRecepcion(VentaModels2 Venta)
        {
            object[] parametros =
            {
                Venta.Id_venta
            };
            SqlDataReader dr = null;
            dr = SqlHelper.ExecuteReader(Venta.Conexion, "spCSLDB_Venta_get_VentaEventoRecepcionXIDVenta", parametros);

            while (dr.Read())
            {
                Venta.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : false;

                if (Venta.RespuestaAjax.Success)
                {
                    Venta.RecepcionOrigen.Id_recepcionOrigenVenta = !dr.IsDBNull(dr.GetOrdinal("id_recepcionOrigenVenta")) ? dr.GetInt32(dr.GetOrdinal("id_recepcionOrigenVenta")) : 0;
                    Venta.RecepcionOrigen.Id_flete = !dr.IsDBNull(dr.GetOrdinal("id_flete")) ? dr.GetString(dr.GetOrdinal("id_flete")) : string.Empty;
                    Venta.RecepcionOrigen.KilometrajeFinal = !dr.IsDBNull(dr.GetOrdinal("kilometrajeFinal")) ? dr.GetInt32(dr.GetOrdinal("kilometrajeFinal")) : 0;
                    Venta.RecepcionOrigen.HoraLlegada = !dr.IsDBNull(dr.GetOrdinal("horaLlegada")) ? dr.GetTimeSpan(dr.GetOrdinal("horaLlegada")) : DateTime.Now.TimeOfDay;
                    Venta.RecepcionOrigen.FechaLlegada = !dr.IsDBNull(dr.GetOrdinal("fechaLlegada")) ? dr.GetDateTime(dr.GetOrdinal("fechaLlegada")) : DateTime.Now;
                    Venta.RecepcionOrigen.Observacion = !dr.IsDBNull(dr.GetOrdinal("obsevacion")) ? dr.GetString(dr.GetOrdinal("obsevacion")) : string.Empty;
                    Venta.RecepcionOrigen.MermaDestino = !dr.IsDBNull(dr.GetOrdinal("merma")) ? dr.GetDecimal(dr.GetOrdinal("merma")) : 0;
                }
                else
                {
                    Venta.RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                }
            }
            dr.Close();
            return Venta;
        }
        #endregion
        #region VentaEvento
        public VentaModels2 GetVentaEvento(VentaModels2 Venta)
        {
            object[] parametros =
            {
                Venta.EventoVenta.Id_venta,
                Venta.EventoVenta.Id_eventoVenta
            };
            SqlDataReader dr = null;
            dr = SqlHelper.ExecuteReader(Venta.Conexion, "spCSLDB_Venta_get_VentaEventoXIDVenta", parametros);

            while (dr.Read())
            {
                Venta.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : false;

                if (Venta.RespuestaAjax.Success)
                {
                    Venta.EventoVenta.Id_eventoVenta = !dr.IsDBNull(dr.GetOrdinal("id_eventoVenta")) ? dr.GetInt32(dr.GetOrdinal("id_eventoVenta")) : 0;
                    Venta.EventoVenta.Id_documentoPorCobrarDetalle = !dr.IsDBNull(dr.GetOrdinal("id_documentoPorCobrarDetalle")) ? dr.GetString(dr.GetOrdinal("id_documentoPorCobrarDetalle")) : string.Empty;
                    Venta.EventoVenta.Id_tipoEvento = !dr.IsDBNull(dr.GetOrdinal("id_tipoEvento")) ? dr.GetInt32(dr.GetOrdinal("id_tipoEvento")) : 0;
                    Venta.EventoVenta.Cantidad = !dr.IsDBNull(dr.GetOrdinal("cantidad")) ? dr.GetInt32(dr.GetOrdinal("cantidad")) : 0;
                    Venta.EventoVenta.Lugar = !dr.IsDBNull(dr.GetOrdinal("lugar")) ? dr.GetString(dr.GetOrdinal("lugar")) : string.Empty;
                    Venta.EventoVenta.FechaDeteccion = !dr.IsDBNull(dr.GetOrdinal("fechaDeteccion")) ? dr.GetDateTime(dr.GetOrdinal("fechaDeteccion")) : DateTime.Now;
                    Venta.EventoVenta.HoraDeteccion = !dr.IsDBNull(dr.GetOrdinal("horaDeteccion")) ? dr.GetTimeSpan(dr.GetOrdinal("horaDeteccion")) : DateTime.Now.TimeOfDay;
                    Venta.EventoVenta.Observacion = !dr.IsDBNull(dr.GetOrdinal("observacion")) ? dr.GetString(dr.GetOrdinal("observacion")) : string.Empty;
                    Venta.EventoVenta.ImagenBase64 = !dr.IsDBNull(dr.GetOrdinal("imagenBase64")) ? dr.GetString(dr.GetOrdinal("imagenBase64")) : string.Empty;
                    Venta.EventoVenta.MontoDeduccion = !dr.IsDBNull(dr.GetOrdinal("deduccion")) ? dr.GetDecimal(dr.GetOrdinal("deduccion")) : 0;
                    Venta.EventoVenta.Id_TipoDeDeduccion = !dr.IsDBNull(dr.GetOrdinal("id_deduccion")) ? dr.GetInt32(dr.GetOrdinal("id_deduccion")) : 0;
                    Venta.EventoVenta.Id_conceptoDocumento = !dr.IsDBNull(dr.GetOrdinal("id_conceptoDocumento")) ? dr.GetInt32(dr.GetOrdinal("id_conceptoDocumento")) : 0;
                }
                else
                {
                    Venta.RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                }
            }
            dr.Close();
            return Venta;
        }
        #endregion
        #region VentaDocumentos
        public DocumentoModels GetVentaDocumentos(DocumentoModels Documentos)
        {
            object[] parametros =
            {
                Documentos.Id_servicio
            };
            SqlDataReader dr = null;
            dr = SqlHelper.ExecuteReader(Documentos.Conexion, "spCSLDB_Venta_get_VentaDocumentosXIDVenta", parametros);

            while (dr.Read())
            {
                Documentos.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : false;

                if (Documentos.RespuestaAjax.Success)
                {
                    Documentos.Id_servicio = !dr.IsDBNull(dr.GetOrdinal("id_venta")) ? dr.GetString(dr.GetOrdinal("id_venta")) : string.Empty;
                    Documentos.Id_documentoPorPagar = !dr.IsDBNull(dr.GetOrdinal("id_documentoPorPagar")) ? dr.GetString(dr.GetOrdinal("id_documentoPorPagar")) : string.Empty;
                    Documentos.CobrarFlete = !dr.IsDBNull(dr.GetOrdinal("cobrarFlete")) ? dr.GetInt16(dr.GetOrdinal("cobrarFlete")) : 0;
                    Documentos.Id_conceptoSalidaDeduccion = !dr.IsDBNull(dr.GetOrdinal("id_conceptoDocumento")) ? dr.GetInt16(dr.GetOrdinal("id_conceptoDocumento")) : 0;
                    Documentos.PrecioUnitarioDocumentacion = !dr.IsDBNull(dr.GetOrdinal("precioUnitario")) ? dr.GetDecimal(dr.GetOrdinal("precioUnitario")) : 0;
                    Documentos.Id_detalleDoctoCobrar = !dr.IsDBNull(dr.GetOrdinal("id_detalleDoctoCobrar")) ? dr.GetString(dr.GetOrdinal("id_detalleDoctoCobrar")) : string.Empty;
                    Documentos.Id_deduccion = !dr.IsDBNull(dr.GetOrdinal("id_deduccion")) ? dr.GetInt32(dr.GetOrdinal("id_deduccion")) : 0;
                }
                else
                {
                    Documentos.RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                }
            }
            dr.Close();
            return Documentos;
        }
        public DocumentoModels AC_CostoDocumentos(DocumentoModels Documento)
        {
            try
            {
                object[] parametros =
                {
                     Documento.Id_servicio
                    ,Documento.Usuario
                    ,Documento.PrecioUnitarioDocumentacion
                    ,Documento.Id_detalleDoctoCobrar
                    ,Documento.Id_conceptoSalidaDeduccion
                    ,Documento.Id_deduccion
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Documento.Conexion, "spCSLDB_Venta_ac_PrecioDocumento", parametros);

                while (dr.Read())
                {
                    Documento.RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                    Documento.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : true;
                }
                dr.Close();
                return Documento;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Vista cobro
        public DocumentosPorCobrarDetallePagosModels GetDetalleDocumentoPago(DocumentosPorCobrarDetallePagosModels DocumentoPago)
        {
            try
            {
                object[] parametros =
                {
                    DocumentoPago.Id_padre,
                    DocumentoPago.Id_documentoPorCobrarDetallePagos
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(DocumentoPago.Conexion, "spCSLDB_Venta_get_GetDetalleDocumentoPago", parametros);
                while (dr.Read())
                {
                    DocumentoPago.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : false;
                    if (DocumentoPago.RespuestaAjax.Success)
                    {
                        DocumentoPago.Id_documentoPorCobrarDetallePagos = !dr.IsDBNull(dr.GetOrdinal("id_documentoPorCobrarDetallePagos")) ? dr.GetString(dr.GetOrdinal("id_documentoPorCobrarDetallePagos")) : string.Empty;
                        DocumentoPago.Monto = !dr.IsDBNull(dr.GetOrdinal("monto")) ? dr.GetDecimal(dr.GetOrdinal("monto")) : 0;
                        DocumentoPago.Id_documentoPorCobrarDetallePagosBancarizado = !dr.IsDBNull(dr.GetOrdinal("id_documentoPorCobrarDetallePagosBancarizado")) ? dr.GetString(dr.GetOrdinal("id_documentoPorCobrarDetallePagosBancarizado")) : string.Empty;
                        DocumentoPago.NombreBancoOrdenante = !dr.IsDBNull(dr.GetOrdinal("nombreBancoOrdenante")) ? dr.GetString(dr.GetOrdinal("nombreBancoOrdenante")) : string.Empty;
                        DocumentoPago.NumeroAutorizacion = !dr.IsDBNull(dr.GetOrdinal("numeroAutorizacion")) ? dr.GetString(dr.GetOrdinal("numeroAutorizacion")) : string.Empty;
                        DocumentoPago.NombreBancoBeneficiante = !dr.IsDBNull(dr.GetOrdinal("nombreBancoBeneficiante")) ? dr.GetString(dr.GetOrdinal("nombreBancoBeneficiante")) : string.Empty;
                        DocumentoPago.TipoCadenaPago = !dr.IsDBNull(dr.GetOrdinal("tipoCadenaPago")) ? dr.GetString(dr.GetOrdinal("tipoCadenaPago")) : string.Empty;
                        DocumentoPago.Id_documentoPorCobrar = !dr.IsDBNull(dr.GetOrdinal("id_documentoPorCobrar")) ? dr.GetString(dr.GetOrdinal("id_documentoPorCobrar")) : string.Empty;
                        DocumentoPago.Observacion = !dr.IsDBNull(dr.GetOrdinal("observacion")) ? dr.GetString(dr.GetOrdinal("observacion")) : string.Empty;
                        DocumentoPago.Id_cuentaBancariaOrdenante = !dr.IsDBNull(dr.GetOrdinal("id_cuentaBancariaOrdenante")) ? dr.GetString(dr.GetOrdinal("id_cuentaBancariaOrdenante")) : string.Empty;
                        DocumentoPago.Id_cuentaBancariaBeneficiante = !dr.IsDBNull(dr.GetOrdinal("id_cuentaBancariaBeneficiante")) ? dr.GetString(dr.GetOrdinal("id_cuentaBancariaBeneficiante")) : string.Empty;
                        DocumentoPago.RfcEmisorOrdenante = !dr.IsDBNull(dr.GetOrdinal("rfcEmisorOrdenante")) ? dr.GetString(dr.GetOrdinal("rfcEmisorOrdenante")) : string.Empty;
                        DocumentoPago.RfcEmisorBeneficiario = !dr.IsDBNull(dr.GetOrdinal("rfcEmisorBeneficiario")) ? dr.GetString(dr.GetOrdinal("rfcEmisorBeneficiario")) : string.Empty;
                        DocumentoPago.FolioIFE = !dr.IsDBNull(dr.GetOrdinal("folioIFE")) ? dr.GetString(dr.GetOrdinal("folioIFE")) : string.Empty;
                        DocumentoPago.Id_formaPago = !dr.IsDBNull(dr.GetOrdinal("id_formaPago")) ? dr.GetInt16(dr.GetOrdinal("id_formaPago")) : 0;
                        DocumentoPago.fecha = !dr.IsDBNull(dr.GetOrdinal("fecha")) ? dr.GetDateTime(dr.GetOrdinal("fecha")) : DateTime.Now;
                        DocumentoPago.Id_cuentaBancariaBeneficiante = !dr.IsDBNull(dr.GetOrdinal("id_cuentaBancariaBeneficiante")) ? dr.GetString(dr.GetOrdinal("id_cuentaBancariaBeneficiante")) : string.Empty;
                        DocumentoPago.NombreBancoOrdenante = !dr.IsDBNull(dr.GetOrdinal("nombreBancoOrdenante")) ? dr.GetString(dr.GetOrdinal("nombreBancoOrdenante")) : string.Empty;
                        DocumentoPago.NumCuentaOrdenante = !dr.IsDBNull(dr.GetOrdinal("numCuentaOrdenante")) ? dr.GetString(dr.GetOrdinal("numCuentaOrdenante")) : string.Empty;
                        DocumentoPago.NumCuentaBeneficiante = !dr.IsDBNull(dr.GetOrdinal("numCuentaBeneficiante")) ? dr.GetString(dr.GetOrdinal("numCuentaBeneficiante")) : string.Empty;
                        DocumentoPago.Bancarizado = !dr.IsDBNull(dr.GetOrdinal("bancarizado")) ? dr.GetBoolean(dr.GetOrdinal("bancarizado")) : false;
                        DocumentoPago.ImagenBase64 = !dr.IsDBNull(dr.GetOrdinal("imagen")) ? dr.GetString(dr.GetOrdinal("imagen")) : string.Empty;
                    }
                    else
                    {
                        DocumentoPago.RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;

                    }
                }
                dr.Close();
                return DocumentoPago;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DocumentosPorCobrarDetallePagosModels GetNombreEmpresaCliente(DocumentosPorCobrarDetallePagosModels DocumentoPorCobrarDetallePagos)
        {
            try
            {
                object[] parametros =
                {
                    DocumentoPorCobrarDetallePagos.Id_padre
                };
                SqlDataReader dr = null;
                string nombre = string.Empty;

                dr = SqlHelper.ExecuteReader(DocumentoPorCobrarDetallePagos.Conexion, "spCSLDB_Venta_get_NombreCliente_Empresa", parametros);
                while (dr.Read())
                {
                    DocumentoPorCobrarDetallePagos.NombreEmpresa = !dr.IsDBNull(dr.GetOrdinal("nombreEmpresa")) ? dr.GetString(dr.GetOrdinal("nombreEmpresa")) : string.Empty;
                    DocumentoPorCobrarDetallePagos.NombreProveedor_Cliente = !dr.IsDBNull(dr.GetOrdinal("nombreProveedor")) ? dr.GetString(dr.GetOrdinal("nombreProveedor")) : string.Empty;
                }
                dr.Close();
                return DocumentoPorCobrarDetallePagos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CuentaBancariaModels> GetListadoCuentasBancarias(DocumentosPorCobrarDetallePagosModels DocumentoPorCobrarDetallePagos)
        {
            try
            {
                object[] parametros =
                {
                    DocumentoPorCobrarDetallePagos.TipoCuentaBancaria,
                    DocumentoPorCobrarDetallePagos.Id_padre
                };
                CuentaBancariaModels item;
                List<CuentaBancariaModels> lista = new List<CuentaBancariaModels>();
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(DocumentoPorCobrarDetallePagos.Conexion, "spCSLDB_Venta_get_CuentasBancarias", parametros);
                while (dr.Read())
                {
                    item = new CuentaBancariaModels();
                    item.IDDatosBancarios = !dr.IsDBNull(dr.GetOrdinal("id_datosBancarios")) ? dr.GetString(dr.GetOrdinal("id_datosBancarios")) : string.Empty;
                    item.Banco = new CatBancoModels();
                    item.Banco.IDBanco = !dr.IsDBNull(dr.GetOrdinal("id_banco")) ? dr.GetInt32(dr.GetOrdinal("id_banco")) : 0;
                    item.Banco.Descripcion = !dr.IsDBNull(dr.GetOrdinal("descripcion")) ? dr.GetString(dr.GetOrdinal("descripcion")) : string.Empty;
                    item.Titular = !dr.IsDBNull(dr.GetOrdinal("titular")) ? dr.GetString(dr.GetOrdinal("titular")) : string.Empty;
                    item.Clabe = !dr.IsDBNull(dr.GetOrdinal("clabeInterbancaria")) ? dr.GetString(dr.GetOrdinal("clabeInterbancaria")) : string.Empty;
                    item.NumCuenta = !dr.IsDBNull(dr.GetOrdinal("numCuenta")) ? dr.GetString(dr.GetOrdinal("numCuenta")) : string.Empty;
                    item.NumTarjeta = !dr.IsDBNull(dr.GetOrdinal("numTarjeta")) ? dr.GetString(dr.GetOrdinal("numTarjeta")) : string.Empty;

                    lista.Add(item);
                }
                dr.Close();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Vista Transacciones
        public VentaTransacionesModels GetTransacciones(VentaTransacionesModels Transacciones)
        {
            try
            {
                object[] parametros =
                {
                     Transacciones.Id_venta
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Transacciones.Conexion, "spCSLDB_Venta_get_Transacciones", parametros);

                while (dr.Read())
                {
                    Transacciones.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : true;
                    if (Transacciones.RespuestaAjax.Success)
                    {
                        Transacciones.DocumentosPorCobrar = new DocumentosPorCobrarModels();
                        Transacciones.DocumentosPorCobrar.Id_documentoCobrar = !dr.IsDBNull(dr.GetOrdinal("id_documentoXCobrar")) ? dr.GetString(dr.GetOrdinal("id_documentoXCobrar")) : string.Empty;
                        Transacciones.DocumentosPorCobrar.Pagos = !dr.IsDBNull(dr.GetOrdinal("pagos")) ? dr.GetDecimal(dr.GetOrdinal("pagos")) : 0;
                        Transacciones.DocumentosPorCobrar.Pendiente = !dr.IsDBNull(dr.GetOrdinal("pendiente")) ? dr.GetDecimal(dr.GetOrdinal("pendiente")) : 0;
                        Transacciones.DocumentosPorCobrar.Cambio = !dr.IsDBNull(dr.GetOrdinal("cambio")) ? dr.GetDecimal(dr.GetOrdinal("cambio")) : 0;
                        Transacciones.DocumentosPorCobrar.Impuestos = !dr.IsDBNull(dr.GetOrdinal("impuestos")) ? dr.GetDecimal(dr.GetOrdinal("impuestos")) : 0;
                        Transacciones.Subtotal = !dr.IsDBNull(dr.GetOrdinal("subtotalPercepcion")) ? dr.GetDecimal(dr.GetOrdinal("subtotalPercepcion")) : 0;
                        Transacciones.DocumentosPorCobrar.Total = !dr.IsDBNull(dr.GetOrdinal("total")) ? dr.GetDecimal(dr.GetOrdinal("total")) : 0;
                        Transacciones.DocumentosPorCobrar.TotalPercepciones = !dr.IsDBNull(dr.GetOrdinal("totalPercepcion")) ? dr.GetDecimal(dr.GetOrdinal("totalPercepcion")) : 0;
                        Transacciones.DocumentosPorCobrar.TotalDeducciones = !dr.IsDBNull(dr.GetOrdinal("totalDeduccion")) ? dr.GetDecimal(dr.GetOrdinal("totalDeduccion")) : 0;
                    }
                    else
                    {
                        Transacciones.RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                    }
                }
                dr.Close();
                return Transacciones;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Documento detalles producto servicio
        public DocumentosPorCobrarDetalleModels GetDetalleDocumentoPorCobrar(DocumentosPorCobrarDetalleModels documento)
        {
            try
            {
                SqlDataReader dr = null;

                object[] parametros =
                {
                    documento.Id_servicio,
                    documento.Id_documentoCobrar,
                    documento.Id_detalleDoctoCobrar
                };
                dr = SqlHelper.ExecuteReader(documento.Conexion, "spCSLDB_Venta_get_DetalleDocumentoPorCobrar", parametros);
                while (dr.Read())
                {
                    documento.Id_documentoCobrar = !dr.IsDBNull(dr.GetOrdinal("id_documentoCobrar")) ? dr.GetString(dr.GetOrdinal("id_documentoCobrar")) : string.Empty;
                    documento.Id_productoServicio = !dr.IsDBNull(dr.GetOrdinal("id_productoServicio")) ? dr.GetString(dr.GetOrdinal("id_productoServicio")) : string.Empty;
                    documento.Id_conceptoDocumento = !dr.IsDBNull(dr.GetOrdinal("id_conceptoDocumento")) ? dr.GetInt16(dr.GetOrdinal("id_conceptoDocumento")) : 0;
                    documento.Cantidad = !dr.IsDBNull(dr.GetOrdinal("cantidad")) ? dr.GetDecimal(dr.GetOrdinal("cantidad")) : 0;
                    documento.PrecioUnitario = !dr.IsDBNull(dr.GetOrdinal("precioUnitario")) ? dr.GetDecimal(dr.GetOrdinal("precioUnitario")) : 0;
                    documento.Subtotal = !dr.IsDBNull(dr.GetOrdinal("subtotal")) ? dr.GetDecimal(dr.GetOrdinal("subtotal")) : 0; ;
                    documento.Id_producto = !dr.IsDBNull(dr.GetOrdinal("id_productoServicio")) ? dr.GetString(dr.GetOrdinal("id_productoServicio")) : string.Empty;
                    documento.Id_almacen = !dr.IsDBNull(dr.GetOrdinal("id_almacen")) ? dr.GetString(dr.GetOrdinal("id_almacen")) : string.Empty;
                    documento.Id_unidadProducto = !dr.IsDBNull(dr.GetOrdinal("id_unidadProducto")) ? dr.GetString(dr.GetOrdinal("id_unidadProducto")) : string.Empty;
                    documento.Inventario = !dr.IsDBNull(dr.GetOrdinal("inventario")) ? dr.GetBoolean(dr.GetOrdinal("inventario")) : false;
                    documento.NombreAlmacen = !dr.IsDBNull(dr.GetOrdinal("nombreAlmacen")) ? dr.GetString(dr.GetOrdinal("nombreAlmacen")) : string.Empty;
                    documento.NombreProducto = !dr.IsDBNull(dr.GetOrdinal("nombreProducto")) ? dr.GetString(dr.GetOrdinal("nombreProducto")) : string.Empty;
                }
                dr.Close();
                return documento;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DocumentosPorCobrarDetalleModels GetDetalleDocumentoPorCobrarEdit(DocumentosPorCobrarDetalleModels documento)
        {
            try
            {
                SqlDataReader dr = null;

                object[] parametros =
                {
                    documento.Id_servicio,
                    documento.Id_documentoCobrar,
                    documento.Id_detalleDoctoCobrar
                };
                dr = SqlHelper.ExecuteReader(documento.Conexion, "spCSLDB_Flete_get_DetalleDocumentoPorCobrarEdit", parametros);
                while (dr.Read())
                {
                    documento.Id_documentoCobrar = !dr.IsDBNull(dr.GetOrdinal("id_documentoCobrar")) ? dr.GetString(dr.GetOrdinal("id_documentoCobrar")) : string.Empty;
                    documento.Id_productoServicio = !dr.IsDBNull(dr.GetOrdinal("id_productoServicio")) ? dr.GetString(dr.GetOrdinal("id_productoServicio")) : string.Empty;
                    documento.DescripcionDocumento = !dr.IsDBNull(dr.GetOrdinal("descripcion")) ? dr.GetString(dr.GetOrdinal("descripcion")) : string.Empty;
                    documento.Cantidad = !dr.IsDBNull(dr.GetOrdinal("cantidad")) ? dr.GetDecimal(dr.GetOrdinal("cantidad")) : 0;
                    documento.PrecioUnitario = !dr.IsDBNull(dr.GetOrdinal("precioUnitario")) ? dr.GetDecimal(dr.GetOrdinal("precioUnitario")) : 0;
                    documento.Subtotal = !dr.IsDBNull(dr.GetOrdinal("subtotal")) ? dr.GetDecimal(dr.GetOrdinal("subtotal")) : 0; ;
                    documento.Inventario = !dr.IsDBNull(dr.GetOrdinal("inventario")) ? dr.GetBoolean(dr.GetOrdinal("inventario")) : false;
                    documento.NombreAlmacen = !dr.IsDBNull(dr.GetOrdinal("nombreAlmacen")) ? dr.GetString(dr.GetOrdinal("nombreAlmacen")) : string.Empty;
                    documento.NombreProducto = !dr.IsDBNull(dr.GetOrdinal("nombreProducto")) ? dr.GetString(dr.GetOrdinal("nombreProducto")) : string.Empty;
                }
                dr.Close();
                return documento;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Vista Impuesto
        public ImpuestoModels Get_AC_Impuestos(ImpuestoModels Impuesto)
        {
            try
            {
                object[] parametros =
                {
                     Impuesto.IDModulo,
                     Impuesto.Id_detalleDoctoCobrar,
                     1
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Impuesto.Conexion, "spCSLDB_Impuesto_get_Impuesto", parametros);

                while (dr.Read())
                {
                    Impuesto.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : true;
                    if (Impuesto.RespuestaAjax.Success)
                    {
                        Impuesto.PrecioProducto = !dr.IsDBNull(dr.GetOrdinal("precioUnitario")) ? dr.GetDecimal(dr.GetOrdinal("precioUnitario")) : 0;
                        Impuesto.TotalImpuestoRetencion = !dr.IsDBNull(dr.GetOrdinal("impuestos_retenidos")) ? dr.GetDecimal(dr.GetOrdinal("impuestos_retenidos")) : 0;
                        Impuesto.TotalImpuestoTrasladado = !dr.IsDBNull(dr.GetOrdinal("impuestos_trasladados")) ? dr.GetDecimal(dr.GetOrdinal("impuestos_trasladados")) : 0;
                        Impuesto.TotalImpuestos = !dr.IsDBNull(dr.GetOrdinal("impuestos")) ? dr.GetDecimal(dr.GetOrdinal("impuestos")) : 0;
                        Impuesto.SubTotal = !dr.IsDBNull(dr.GetOrdinal("subtotal")) ? dr.GetDecimal(dr.GetOrdinal("subtotal")) : 0;
                        Impuesto.Total = !dr.IsDBNull(dr.GetOrdinal("total")) ? dr.GetDecimal(dr.GetOrdinal("total")) : 0;
                    }
                    else
                    {
                        Impuesto.RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                    }
                }
                dr.Close();
                return Impuesto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ImpuestoModels AC_ImpuestoProductoServicio(ImpuestoModels Impuesto)
        {
            try
            {
                object[] parametros =
                {
                    Impuesto.IDModulo,
                    Impuesto.IDImpuesto,//@id_documentoCobrarDetalleImpuesto char(36)
                    Impuesto.Id_detalleDoctoCobrar,//,@id_detalleDoctoCobrar char(36)
                    Impuesto.TipoImpuesto.Clave,//,@id_tipoImpuesto smallint
                    Impuesto.Impuesto.Clave,//,@id_impuesto smallint
                    Impuesto.TipoFactor.Clave,//,@id_tipoFactor smallint
                    Impuesto.Base,//,@base decimal(18,3)
                    Impuesto.TasaCuota,//,@tasaCuota decimal(18,6)
                    Impuesto.Importe,//,@importe money
                    Impuesto.Usuario//,@id_usuario char(36)
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Impuesto.Conexion, "spCSLDB_Venta_ac_ImpuestoProductoServicio", parametros);
                while (dr.Read())
                {
                    Impuesto.RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                    Impuesto.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : true;
                }
                dr.Close();
                return Impuesto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ImpuestoModels GetImpuestoXIDImpuesto(ImpuestoModels Impuesto)
        {
            try
            {
                SqlDataReader dr = null;

                object[] parametros =
                {
                    Impuesto.IDImpuesto
                };
                dr = SqlHelper.ExecuteReader(Impuesto.Conexion, "spCSLDB_Impuesto_get_ImpuestoXIDImpuesto", parametros);
                while (dr.Read())
                {
                    Impuesto.Impuesto.Clave = !dr.IsDBNull(dr.GetOrdinal("id_impuesto")) ? dr.GetInt16(dr.GetOrdinal("id_impuesto")) : 0;
                    Impuesto.TipoFactor.Clave = !dr.IsDBNull(dr.GetOrdinal("id_tipoFactor")) ? dr.GetInt16(dr.GetOrdinal("id_tipoFactor")) : 0;
                    Impuesto.TipoImpuesto.Clave = !dr.IsDBNull(dr.GetOrdinal("id_tipoImpuesto")) ? dr.GetInt16(dr.GetOrdinal("id_tipoImpuesto")) : 0;
                    Impuesto.Base = !dr.IsDBNull(dr.GetOrdinal("base")) ? dr.GetDecimal(dr.GetOrdinal("base")) : 0;
                    Impuesto.TasaCuota = !dr.IsDBNull(dr.GetOrdinal("tasaCuota")) ? dr.GetDecimal(dr.GetOrdinal("tasaCuota")) : 0;
                    Impuesto.Importe = !dr.IsDBNull(dr.GetOrdinal("importe")) ? dr.GetDecimal(dr.GetOrdinal("importe")) : 0;
                    Impuesto.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : false;
                }
                dr.Close();
                return Impuesto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
        #region Vista documento
        public DocumentoModels GetDocumentoXIDDocumento(DocumentoModels Documento)
        {
            try
            {
                object[] parametros =
                {
                     Documento.IDDocumento, Documento.Id_servicio
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Documento.Conexion, "spCSLDB_Venta_get_DocumentoXIDDocumento", parametros);

                while (dr.Read())
                {
                    Documento.IDTipoDocumento = !dr.IsDBNull(dr.GetOrdinal("id_tipoDocumento")) ? dr.GetInt16(dr.GetOrdinal("id_tipoDocumento")) : 0;
                    Documento.Clave = !dr.IsDBNull(dr.GetOrdinal("clave")) ? dr.GetString(dr.GetOrdinal("clave")) : string.Empty;
                    //Solo para mostrar
                    Documento.ImagenServer = !dr.IsDBNull(dr.GetOrdinal("imagen")) ? dr.GetString(dr.GetOrdinal("imagen")) : string.Empty;
                }
                if (string.IsNullOrEmpty(Documento.ImagenServer))
                {
                    //No hay imagen en el server
                    Documento.MostrarImagen = Auxiliar.SetDefaultImage();
                }
                else
                {
                    //Guardamos el string de la imagen
                    Documento.MostrarImagen = Documento.ImagenServer;
                }
                Documento.ExtensionImagenBase64 = Auxiliar.ObtenerExtensionImagenBase64(Documento.MostrarImagen);
                dr.Close();

                return Documento;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Vista detalles
        public VentaDetalleModels Get_detallesVenta(VentaDetalleModels VentaDetalles)
        {
            try
            {
                object[] parametros =
                {
                     VentaDetalles.Id_venta
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(VentaDetalles.Conexion, "spCSLDB_Venta_get_Detalles", parametros);

                while (dr.Read())
                {
                    VentaDetalles.VentaFolio = !dr.IsDBNull(dr.GetOrdinal("folio")) ? dr.GetString(dr.GetOrdinal("folio")) : string.Empty;
                    VentaDetalles.VentaSucursal = !dr.IsDBNull(dr.GetOrdinal("nombreSuc")) ? dr.GetString(dr.GetOrdinal("nombreSuc")) : string.Empty;
                    VentaDetalles.VentaFecha = !dr.IsDBNull(dr.GetOrdinal("fechaHoraVenta")) ? dr.GetString(dr.GetOrdinal("fechaHoraVenta")) : string.Empty;
                    VentaDetalles.VentaMerma = !dr.IsDBNull(dr.GetOrdinal("merma")) ? dr.GetString(dr.GetOrdinal("merma")) : "0";
                    VentaDetalles.VentaMermaMachos = !dr.IsDBNull(dr.GetOrdinal("mermaExtraMachos")) ? dr.GetString(dr.GetOrdinal("mermaExtraMachos")) : "0";
                    VentaDetalles.VentaMermaHembras = !dr.IsDBNull(dr.GetOrdinal("mermaExtraHembras")) ? dr.GetString(dr.GetOrdinal("mermaExtraHembras")) : "0";
                    VentaDetalles.VentaObservacion = !dr.IsDBNull(dr.GetOrdinal("observacion")) ? dr.GetString(dr.GetOrdinal("observacion")) : string.Empty;

                    VentaDetalles.FleteLineaFletera = !dr.IsDBNull(dr.GetOrdinal("lineaFletera")) ? dr.GetString(dr.GetOrdinal("lineaFletera")) : string.Empty;
                    VentaDetalles.FleteLugarOrigen = !dr.IsDBNull(dr.GetOrdinal("lugarOrigen")) ? dr.GetString(dr.GetOrdinal("lugarOrigen")) : string.Empty;
                    VentaDetalles.FleteLugarDestino = !dr.IsDBNull(dr.GetOrdinal("lugarDestino")) ? dr.GetString(dr.GetOrdinal("lugarDestino")) : string.Empty;
                    VentaDetalles.FleteNombreChofer = !dr.IsDBNull(dr.GetOrdinal("nombreCompletoChofer")) ? dr.GetString(dr.GetOrdinal("nombreCompletoChofer")) : string.Empty;
                    VentaDetalles.FletePrecio = !dr.IsDBNull(dr.GetOrdinal("precioFlete")) ? dr.GetDecimal(dr.GetOrdinal("precioFlete")) : 0;

                    VentaDetalles.ClienteNombreRazonSocialCompleto = !dr.IsDBNull(dr.GetOrdinal("nombreCompletoCliente")) ? dr.GetString(dr.GetOrdinal("nombreCompletoCliente")) : string.Empty;
                    VentaDetalles.ClienteRegimenFiscal = !dr.IsDBNull(dr.GetOrdinal("regimenFiscalCliente")) ? dr.GetString(dr.GetOrdinal("regimenFiscalCliente")) : string.Empty;
                    VentaDetalles.ClienteDireccion = !dr.IsDBNull(dr.GetOrdinal("direccionCliente")) ? dr.GetString(dr.GetOrdinal("direccionCliente")) : string.Empty;
                    VentaDetalles.ClientePSG = !dr.IsDBNull(dr.GetOrdinal("psgCliente")) ? dr.GetString(dr.GetOrdinal("psgCliente")) : string.Empty;

                    VentaDetalles.EmpresaRazonSocial = !dr.IsDBNull(dr.GetOrdinal("NombreEmpresa")) ? dr.GetString(dr.GetOrdinal("NombreEmpresa")) : string.Empty;
                    VentaDetalles.EmpresaDireccion = !dr.IsDBNull(dr.GetOrdinal("DireccionEmpresa")) ? dr.GetString(dr.GetOrdinal("DireccionEmpresa")) : string.Empty;
                    VentaDetalles.EmpresaRFC = !dr.IsDBNull(dr.GetOrdinal("RFCEmpresa")) ? dr.GetString(dr.GetOrdinal("RFCEmpresa")) : string.Empty;
                    VentaDetalles.EmpresaPSG = !dr.IsDBNull(dr.GetOrdinal("psgEmpresa")) ? dr.GetString(dr.GetOrdinal("psgEmpresa")) : string.Empty;

                    VentaDetalles.DocumentosPrecioDocumentacion = !dr.IsDBNull(dr.GetOrdinal("DocumentosPrecioDocumentacion")) ? dr.GetDecimal(dr.GetOrdinal("DocumentosPrecioDocumentacion")) : 0;
                    VentaDetalles.DocumentosTipoSalidaDocumentacion = !dr.IsDBNull(dr.GetOrdinal("DocumentosTipoSalidaDocumentacion")) ? dr.GetString(dr.GetOrdinal("DocumentosTipoSalidaDocumentacion")) : string.Empty;

                    VentaDetalles.Id_documentoPorCobrar = !dr.IsDBNull(dr.GetOrdinal("id_documentoXCobrar")) ? dr.GetString(dr.GetOrdinal("id_documentoXCobrar")) : string.Empty;
                }
                
                dr.Close();

                return VentaDetalles;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #endregion

        #region AC
        #region AC_Flete
        public RespuestaAjax AC_Flete(VentaModels2 datos)
        {
            try
            {
                object[] parametros =
                {
                    datos.Id_venta,                                         datos.Id_cliente,                                       datos.Id_flete,
                    datos.Id_documentoXCobrar,                              datos.Id_sucursal,
                    //datos.NombreVenta,
                    datos.Descripcion_bascula,                              datos.Observacion,
                    datos.Usuario,                                          datos.CobrarFlete,                                      datos.Flete.precioFlete,
                    datos.Flete.Id_empresa,                                 datos.Flete.id_vehiculo,                                datos.Flete.id_chofer,
                    //datos.Flete.VerificacionJaula.Id_verificacionJaula,
                    datos.Flete.kmInicialVehiculo,                          datos.Flete.FechaSalida,
                    datos.Flete.HoraSalida,                                 datos.Flete.FechaEmbarque,                              datos.Flete.HoraEmbarque,
                    //datos.Flete.NumFleje,
                    //datos.Flete.VerificacionJaula.LimpiezaCompleta,         datos.Flete.VerificacionJaula.SoloPiso,                 datos.Flete.VerificacionJaula.Sucia,
                    //datos.Flete.VerificacionJaula.PuertasInternas,          datos.Flete.VerificacionJaula.Focos,                    datos.Flete.VerificacionJaula.RiesgosPunzoCortantes,
                    //datos.Flete.VerificacionJaula.LlantaRefaccion,          datos.Flete.VerificacionJaula.LlantasBuenEstado,        datos.Flete.VerificacionJaula.PisoAntiadherente,
                    datos.Flete.Trayecto.id_lugarOrigen,                    datos.Flete.Trayecto.id_lugarDestino,
                    //datos.Flete.CondicionPago,
                    //datos.Flete.MetodoPago.Clave,                           datos.Flete.FormaPago.Clave,
                    datos.Flete.FechaTentativaEntrega,                      datos.TipoVenta, datos.Flete.id_choferAuxilar
                };

                RespuestaAjax RespuestaAjax = new RespuestaAjax();
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Venta_ac_Flete", parametros);
                while (dr.Read())
                {
                    RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : false;
                    RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("Mensaje")) ? dr.GetString(dr.GetOrdinal("Mensaje")) : string.Empty;
                }
                dr.Close();
                return RespuestaAjax;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
        #region AC_Ganado
        //public RespuestaAjax AC_Ganado(VentaModels2 datos)
        //{
        //    try
        //    {
        //        object[] parametros =
        //        {
        //            datos.Id_venta, datos.ListaIDGanadosParaVender, datos.Usuario, datos.ME, datos.MontoTotalGanado
        //        };

        //        RespuestaAjax RespuestaAjax = new RespuestaAjax();
        //        SqlDataReader dr = null;
        //        dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Venta_ac_Ganado", parametros);
        //        while (dr.Read())
        //        {
        //            RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : false;
        //            RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("Mensaje")) ? dr.GetString(dr.GetOrdinal("Mensaje")) : string.Empty;
        //        }
        //        dr.Close();
        //        return RespuestaAjax;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}

        public void AC_Ganado(VentaModels2 Modelo, List<GanadoParaVender> Lista)
        {
            try
            {
                // construct sql connection and sql command objects.
                using (SqlConnection sqlcon = new SqlConnection(Modelo.Conexion))
                {
                    using (SqlCommand cmd = new SqlCommand("spCSLDB_Venta_ac_Ganado", sqlcon))
                    {
                        //parametros de entrada
                        cmd.CommandType = CommandType.StoredProcedure;

                        DataTable dataTable;
                        dataTable = new DataTable("Items");

                        dataTable.Columns.Add("Id_ganado", typeof(string));
                        dataTable.Columns.Add("MermaExtra", typeof(decimal));
                        dataTable.Columns.Add("Subtotal", typeof(decimal));

                        foreach (var item in Lista)
                        {
                            dataTable.Rows.Add(item.Id_ganado, item.MermaExtra, item.Subtotal);
                        }

                        cmd.Parameters.Add("@id_venta", SqlDbType.Char).Value = Modelo.Id_venta;
                        cmd.Parameters.Add("@tbl_GanadoParaVenta", SqlDbType.Structured).Value = dataTable;
                        cmd.Parameters.Add("@id_usuario", SqlDbType.Char).Value = Modelo.Usuario;
                        cmd.Parameters.Add("@montoTotalGanado", SqlDbType.Money).Value = Modelo.MontoTotalGanado;


                        //parametros de salida
                        cmd.Parameters.Add(new SqlParameter("@mensaje", SqlDbType.NVarChar, -1)); //-1 para tipo MAX
                        cmd.Parameters["@mensaje"].Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(new SqlParameter("@success", SqlDbType.Bit));
                        cmd.Parameters["@success"].Direction = ParameterDirection.Output;
                        // execute
                        sqlcon.Open();

                        cmd.ExecuteNonQuery();
                        Modelo.RespuestaAjax.Mensaje = cmd.Parameters["@mensaje"].Value.ToString();

                        bool success;

                        if (Boolean.TryParse(cmd.Parameters["@success"].Value.ToString(), out success))
                        {
                            Modelo.RespuestaAjax.Success = success;
                        }
                    }
                }
            }
            catch (Exception ex )
            {
                throw;
            }
        }

        #endregion
        #region AC_Evento
        public RespuestaAjax AC_Evento(EventoVentaModels Evento)
        {
            try
            {
                object[] parametros =
                {
                    Evento.Id_eventoVenta,      Evento.Id_venta,
                    Evento.Id_tipoEvento,       
                    Evento.Cantidad,
                    Evento.Lugar,               Evento.FechaDeteccion,
                    Evento.HoraDeteccion,       Evento.Observacion,
                    Evento.ImagenBase64,        Evento.ListaIDGanadosDelEvento,
                    Evento.Usuario,             Evento.Id_conceptoDocumento,
                    Evento.MontoDeduccion,      Evento.Id_TipoDeDeduccion
                };

                RespuestaAjax RespuestaAjax = new RespuestaAjax();
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Evento.Conexion, "spCSLDB_Venta_ac_Evento", parametros);
                while (dr.Read())
                {
                    RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : false;
                    RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("Mensaje")) ? dr.GetString(dr.GetOrdinal("Mensaje")) : string.Empty;
                }
                dr.Close();
                return RespuestaAjax;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
        #region AC_Recepcion
        public RespuestaAjax AC_Recepcion(VentaModels2 Venta)
        {
            try
            {
                object[] parametros =
                {
                    Venta.Id_venta,                     Venta.RecepcionOrigen.KilometrajeFinal,
                    Venta.RecepcionOrigen.HoraLlegada,  Venta.RecepcionOrigen.FechaLlegada,
                    Venta.RecepcionOrigen.Observacion,  Venta.Usuario,
                    Venta.RecepcionOrigen.MermaDestino, Venta.RecepcionOrigen.Id_recepcionOrigenVenta
                };

                RespuestaAjax RespuestaAjax = new RespuestaAjax();
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Venta.Conexion, "spCSLDB_Venta_ac_RecepcionOrigen", parametros);
                while (dr.Read())
                {
                    RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : false;
                    RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("Mensaje")) ? dr.GetString(dr.GetOrdinal("Mensaje")) : string.Empty;
                }
                dr.Close();
                return RespuestaAjax;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
        #region AC_Comprobante Pago
        public DocumentosPorCobrarDetallePagosModels AC_ComprobanteCobro(DocumentosPorCobrarDetallePagosModels DocumentosPorCobrarModels)
        {
            try
            {
                object[] parametros =
                {
                    DocumentosPorCobrarModels.Id_documentoPorCobrarDetallePagos,    DocumentosPorCobrarModels.Id_padre,
                    DocumentosPorCobrarModels.Id_formaPago,                         DocumentosPorCobrarModels.Monto,
                    DocumentosPorCobrarModels.Observacion,                          DocumentosPorCobrarModels.fecha,
                    DocumentosPorCobrarModels.Id_cuentaBancariaOrdenante,           DocumentosPorCobrarModels.Id_documentoPorCobrarDetallePagosBancarizado,
                    DocumentosPorCobrarModels.Id_cuentaBancariaBeneficiante,        DocumentosPorCobrarModels.NombreBancoOrdenante,
                    DocumentosPorCobrarModels.NumeroAutorizacion,                   DocumentosPorCobrarModels.NumCuentaOrdenante,
                    DocumentosPorCobrarModels.NombreBancoBeneficiante,              DocumentosPorCobrarModels.NumCuentaBeneficiante,
                    DocumentosPorCobrarModels.FolioIFE,                             DocumentosPorCobrarModels.Usuario,
                    DocumentosPorCobrarModels.Bancarizado,                          DocumentosPorCobrarModels.RfcEmisorOrdenante,
                    DocumentosPorCobrarModels.RfcEmisorBeneficiario,                DocumentosPorCobrarModels.ImagenBase64
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(DocumentosPorCobrarModels.Conexion, "spCSLDB_Venta_AC_DetallesPago", parametros);
                while (dr.Read())
                {
                    DocumentosPorCobrarModels.RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                    DocumentosPorCobrarModels.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : false;
                }
                dr.Close();
                return DocumentosPorCobrarModels;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region AC_Producto/servicio detalle del documento por cobrar
        public DocumentosPorCobrarDetalleModels AC_ProductoServicio(DocumentosPorCobrarDetalleModels DocumentosPorCobrarModels)
        {
            try
            {
                object[] parametros =
                {
                    DocumentosPorCobrarModels.Id_servicio,
                    DocumentosPorCobrarModels.Id_detalleDoctoCobrar,            DocumentosPorCobrarModels.Id_documentoCobrar,
                    DocumentosPorCobrarModels.Id_productoServicio,              DocumentosPorCobrarModels.Id_conceptoDocumento,
                    DocumentosPorCobrarModels.Cantidad,                         DocumentosPorCobrarModels.PrecioUnitario,
                    DocumentosPorCobrarModels.Subtotal,                         DocumentosPorCobrarModels.Usuario,
                    DocumentosPorCobrarModels.TipoServicio,                     DocumentosPorCobrarModels.Id_almacen,
                    DocumentosPorCobrarModels.Id_unidadProducto,                DocumentosPorCobrarModels.Id_producto,
                    DocumentosPorCobrarModels.Existencia
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(DocumentosPorCobrarModels.Conexion, "spCSLDB_Venta_AC_ServicioProducto_DocumentoPorCobrarDetalle", parametros);
                while (dr.Read())
                {
                    DocumentosPorCobrarModels.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : false;
                    DocumentosPorCobrarModels.RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                }
                dr.Close();
                return DocumentosPorCobrarModels;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion
        #region AC_Documento
        public DocumentoModels AC_Documento(DocumentoModels Documento)
        {
            try
            {
                object[] parametros =
                {
                     Documento.IDDocumento
                    ,Documento.Id_servicio
                    ,Documento.IDTipoDocumento
                    ,Documento.Clave
                    ,Documento.ImagenServer
                    ,Documento.Usuario
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Documento.Conexion, "spCSLDB_Venta_ac_Documento", parametros);

                while (dr.Read())
                {
                    Documento.RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                    Documento.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : true;
                }
                dr.Close();
                return Documento;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region AC_Impuestos

        #endregion

        #endregion

        #region DEL
        #region Del_Evento
        public RespuestaAjax Del_Evento(EventoVentaModels Evento)
        {
            try
            {
                object[] parametros =
                {
                    Evento.Id_eventoVenta,      Evento.Id_venta,
                    Evento.Usuario
                };

                RespuestaAjax RespuestaAjax = new RespuestaAjax();
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Evento.Conexion, "spCSLDB_Venta_del_Evento", parametros);
                while (dr.Read())
                {
                    RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : false;
                    RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("Mensaje")) ? dr.GetString(dr.GetOrdinal("Mensaje")) : string.Empty;
                }
                dr.Close();
                return RespuestaAjax;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
        #region DEL_ComprobanteCobro
        public DocumentosPorCobrarDetallePagosModels DEL_ComprobanteCobro(DocumentosPorCobrarDetallePagosModels DocumentosPorCobrarModels)
        {
            try
            {
                object[] parametros =
                {
                    DocumentosPorCobrarModels.Id_documentoPorCobrarDetallePagos,    DocumentosPorCobrarModels.Id_documentoPorCobrar,
                    DocumentosPorCobrarModels.Usuario
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(DocumentosPorCobrarModels.Conexion, "spCSLDB_DocumentoPorCobrar_Del_DetallesPago", parametros);
                while (dr.Read())
                {
                    DocumentosPorCobrarModels.RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                    DocumentosPorCobrarModels.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : false;
                }
                dr.Close();
                return DocumentosPorCobrarModels;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Del Producto/Servicio compra
        public DocumentosPorCobrarDetalleModels DEL_ProductoServicioCompra(DocumentosPorCobrarDetalleModels DocumentosPorCobrarModels)
        {
            try
            {
                object[] parametros =
                {
                    DocumentosPorCobrarModels.Id_servicio,
                    DocumentosPorCobrarModels.Id_detalleDoctoCobrar,    DocumentosPorCobrarModels.Id_documentoCobrar,
                    DocumentosPorCobrarModels.Usuario
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(DocumentosPorCobrarModels.Conexion, "spCSLDB_Venta_DEL_ProductoServicio", parametros);
                while (dr.Read())
                {
                    DocumentosPorCobrarModels.RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                    DocumentosPorCobrarModels.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : false;
                }
                dr.Close();
                return DocumentosPorCobrarModels;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Del Impuesto
        public ImpuestoModels Del_Impuesto(ImpuestoModels ImpuestoProducto)
        {
            try
            {
                object[] parametros =
                {
                    ImpuestoProducto.IDModulo
                    ,ImpuestoProducto.IDImpuesto
                    ,ImpuestoProducto.Usuario
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(ImpuestoProducto.Conexion, "spCSLDB_Venta_del_ImpuestoXIDDocumentoDetalle", parametros);
                while (dr.Read())
                {
                    ImpuestoProducto.RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                    ImpuestoProducto.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : true;
                }
                dr.Close();
                return ImpuestoProducto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Del Documento
        public DocumentoModels DEL_DocumentoXIDDocumento(DocumentoModels Documento)
        {
            try
            {
                object[] parametros =
                {
                     Documento.IDDocumento
                    ,Documento.Usuario
                    ,Documento.Id_servicio
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Documento.Conexion, "spCSLDB_Venta_del_DocumentoXIDDocumento", parametros);

                while (dr.Read())
                {
                    Documento.RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                    Documento.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : true;
                }
                dr.Close();
                return Documento;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Del Venta
        public VentaModels2 Venta_del_Venta(VentaModels2 Venta)
        {
            try
            {
                object[] parametros =
                {
                    Venta.Id_venta
                    ,Venta.Usuario
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Venta.Conexion, "spCSLDB_Venta_del_Venta", parametros);
                while (dr.Read())
                {
                    Venta.RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                    Venta.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : true;
                }
                dr.Close();
                return Venta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #endregion

        #region Otros
        #region Cambiar estatus
        public RespuestaAjax CambiarEstatusCompra(VentaModels2 Venta)
        {
            try
            {
                object[] parametros =
                {
                     Venta.Id_venta
                    ,Venta.Usuario
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Venta.Conexion, "spCSLDB_Venta_CambiarStatus", parametros);
                RespuestaAjax RespuestaAjax = new RespuestaAjax();
                while (dr.Read())
                {
                    RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                    RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : true;
                }
                dr.Close();
                return RespuestaAjax;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Carta Porte
        public CartaPorteModels GetCartaPorte(VentaModels2 Venta)
        {
            try
            {
                CartaPorteModels CartaPorte = new CartaPorteModels();
                object[] parametros = { Venta.Id_flete };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Venta.Conexion, "spCSLDB_get_Venta_CartaPorte", parametros);

                while (dr.Read())
                {
                    CartaPorte.LogoRFC = !dr.IsDBNull(dr.GetOrdinal("logoRFC")) ? dr.GetString(dr.GetOrdinal("logoRFC")) : string.Empty;
                    CartaPorte.Folio = !dr.IsDBNull(dr.GetOrdinal("folio")) ? dr.GetString(dr.GetOrdinal("folio")) : string.Empty;
                    CartaPorte.NombreCliente = !dr.IsDBNull(dr.GetOrdinal("cliente")) ? dr.GetString(dr.GetOrdinal("cliente")) : string.Empty;
                    CartaPorte.RFCCliente = !dr.IsDBNull(dr.GetOrdinal("rfc")) ? dr.GetString(dr.GetOrdinal("rfc")) : string.Empty;
                    CartaPorte.NombreConductor = !dr.IsDBNull(dr.GetOrdinal("nombreConductor")) ? dr.GetString(dr.GetOrdinal("nombreConductor")) : string.Empty;
                    CartaPorte.Vehiculo = !dr.IsDBNull(dr.GetOrdinal("vehiculo")) ? dr.GetString(dr.GetOrdinal("vehiculo")) : string.Empty;
                    CartaPorte.PlacaVehiculo = !dr.IsDBNull(dr.GetOrdinal("placas")) ? dr.GetString(dr.GetOrdinal("placas")) : string.Empty;
                    CartaPorte.Remitente = !dr.IsDBNull(dr.GetOrdinal("nombreRemitente")) ? dr.GetString(dr.GetOrdinal("nombreRemitente")) : string.Empty;
                    CartaPorte.Destinatario = !dr.IsDBNull(dr.GetOrdinal("nombreDestinatario")) ? dr.GetString(dr.GetOrdinal("nombreDestinatario")) : string.Empty;
                    CartaPorte.DomicilioRemitente = !dr.IsDBNull(dr.GetOrdinal("DireccionRemitente")) ? dr.GetString(dr.GetOrdinal("DireccionRemitente")) : string.Empty;
                    CartaPorte.DomicilioDestinatario = !dr.IsDBNull(dr.GetOrdinal("DireccionDestinatario")) ? dr.GetString(dr.GetOrdinal("DireccionDestinatario")) : string.Empty;
                    CartaPorte.LugarOrigen = !dr.IsDBNull(dr.GetOrdinal("recogeraEn")) ? dr.GetString(dr.GetOrdinal("recogeraEn")) : string.Empty;
                    CartaPorte.LugarDestino = !dr.IsDBNull(dr.GetOrdinal("entregaraEn")) ? dr.GetString(dr.GetOrdinal("entregaraEn")) : string.Empty;
                    CartaPorte.FechaEntrega = !dr.IsDBNull(dr.GetOrdinal("fechaTentativaEntrega")) ? dr.GetDateTime(dr.GetOrdinal("fechaTentativaEntrega")) : DateTime.Now;
                    CartaPorte.PesoAproximado = !dr.IsDBNull(dr.GetOrdinal("pesoAproximado")) ? dr.GetDecimal(dr.GetOrdinal("pesoAproximado")) : 0;
                    CartaPorte.Total = !dr.IsDBNull(dr.GetOrdinal("totalFlete")) ? dr.GetDecimal(dr.GetOrdinal("totalFlete")) : 0;
                    CartaPorte.ImporteConLetra = CartaPorte.Total.NumeroALetras();
                    CartaPorte.CondicionPago = !dr.IsDBNull(dr.GetOrdinal("condicionPago")) ? dr.GetString(dr.GetOrdinal("condicionPago")) : string.Empty;
                    CartaPorte.FormaPago = !dr.IsDBNull(dr.GetOrdinal("formaPago")) ? dr.GetString(dr.GetOrdinal("formaPago")) : string.Empty;
                    CartaPorte.MetodoPago = !dr.IsDBNull(dr.GetOrdinal("metodoPago")) ? dr.GetString(dr.GetOrdinal("metodoPago")) : string.Empty;
                }
                dr.Close();
                return CartaPorte;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CartaPorteDetallesModels> GetCartaPorteDetalles(VentaModels2 Venta)
        {
            try
            {
                List<CartaPorteDetallesModels> ListaCartaPorteDetalles = new List<CartaPorteDetallesModels>();
                CartaPorteDetallesModels CartaPorte;
                object[] parametros = { Venta.Id_flete };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Venta.Conexion, "spCSLDB_Venta_get_CartaPorteDetalles", parametros);

                while (dr.Read())
                {
                    CartaPorte = new CartaPorteDetallesModels();
                    CartaPorte.Cantidad = !dr.IsDBNull(dr.GetOrdinal("cantidad")) ? dr.GetDecimal(dr.GetOrdinal("cantidad")) : 0;
                    CartaPorte.UnidadMedida = !dr.IsDBNull(dr.GetOrdinal("unidad")) ? dr.GetString(dr.GetOrdinal("unidad")) : string.Empty;
                    CartaPorte.DescripcionProductoServicio = !dr.IsDBNull(dr.GetOrdinal("descripcion")) ? dr.GetString(dr.GetOrdinal("descripcion")) : string.Empty;
                    CartaPorte.PrecioUnitario = !dr.IsDBNull(dr.GetOrdinal("precioUnitario")) ? dr.GetDecimal(dr.GetOrdinal("precioUnitario")) : 0;
                    CartaPorte.ImpuestosTrasladados = !dr.IsDBNull(dr.GetOrdinal("impuestos_trasladados")) ? dr.GetDecimal(dr.GetOrdinal("impuestos_trasladados")) : 0;
                    CartaPorte.ImpuestosRetenidos = !dr.IsDBNull(dr.GetOrdinal("impuestos_retenidos")) ? dr.GetDecimal(dr.GetOrdinal("impuestos_retenidos")) : 0;
                    CartaPorte.Subtotal = !dr.IsDBNull(dr.GetOrdinal("subtotal")) ? dr.GetDecimal(dr.GetOrdinal("subtotal")) : 0;
                    CartaPorte.Total = !dr.IsDBNull(dr.GetOrdinal("total")) ? dr.GetDecimal(dr.GetOrdinal("total")) : 0;

                    ListaCartaPorteDetalles.Add(CartaPorte);
                }
                dr.Close();
                return ListaCartaPorteDetalles;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Reporte Ganado
        public List<ReporteGanadoModels> GetReporteGanadoDetalles(VentaModels2 Venta)
        {
            try
            {
                List<ReporteGanadoModels> Lista = new List<ReporteGanadoModels>();
                ReporteGanadoModels Item;
                object[] parametros = { Venta.Id_venta };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Venta.Conexion, "spCSLDB_Venta_get_ReporteListaGanado", parametros);

                while (dr.Read())
                {
                    Item = new ReporteGanadoModels();
                    Item.NumArete = !dr.IsDBNull(dr.GetOrdinal("numArete")) ? dr.GetString(dr.GetOrdinal("numArete")) : string.Empty;
                    Item.Genero = !dr.IsDBNull(dr.GetOrdinal("genero")) ? dr.GetString(dr.GetOrdinal("genero")) : string.Empty;
                    Item.ImagenBase64_1 = !dr.IsDBNull(dr.GetOrdinal("imagen1")) ? dr.GetString(dr.GetOrdinal("imagen1")) : Auxiliar.SinImagen();
                    Item.ImagenBase64_2 = !dr.IsDBNull(dr.GetOrdinal("imagen2")) ? dr.GetString(dr.GetOrdinal("imagen2")) : Auxiliar.SinImagen();
                    Item.ImagenBase64_3 = !dr.IsDBNull(dr.GetOrdinal("imagen3")) ? dr.GetString(dr.GetOrdinal("imagen3")) : Auxiliar.SinImagen();
                    Item.NombreCorral = !dr.IsDBNull(dr.GetOrdinal("corral")) ? dr.GetString(dr.GetOrdinal("corral")) : string.Empty;
                    Item.PesoInicial = !dr.IsDBNull(dr.GetOrdinal("pesoInicial")) ? dr.GetString(dr.GetOrdinal("pesoInicial")) : string.Empty;
                    Item.PesoFinal = !dr.IsDBNull(dr.GetOrdinal("pesoFinal")) ? dr.GetString(dr.GetOrdinal("pesoFinal")) : string.Empty;
                    Item.PesoPagado = !dr.IsDBNull(dr.GetOrdinal("pesoPagado")) ? dr.GetString(dr.GetOrdinal("pesoPagado")) : string.Empty;
                    Item.PrecioPorKilo = !dr.IsDBNull(dr.GetOrdinal("precioKilo")) ? dr.GetString(dr.GetOrdinal("precioKilo")) : string.Empty;
                    Item.Subtotal = !dr.IsDBNull(dr.GetOrdinal("subtotal")) ? dr.GetString(dr.GetOrdinal("subtotal")) : string.Empty;
                    Item.Repeso = !dr.IsDBNull(dr.GetOrdinal("repeso")) ? dr.GetBoolean(dr.GetOrdinal("repeso")) : false;

                    Item.PesoInicial = Auxiliar.StringToKilos(Item.PesoInicial);
                    Item.PesoFinal = Auxiliar.StringToKilos(Item.PesoFinal);
                    Item.PesoPagado = Auxiliar.StringToKilos(Item.PesoPagado);

                    Item.PrecioPorKilo = Auxiliar.StringToMoneda_MX(Item.PrecioPorKilo);
                    Item.Subtotal = Auxiliar.StringToMoneda_MX(Item.Subtotal);

                    Lista.Add(Item);
                }
                dr.Close();
                return Lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ReporteCabeceraGanado GetReporteCabeceraGanadoDetalles(VentaModels2 Venta)
        {
            try
            {
                ReporteCabeceraGanado Item = new ReporteCabeceraGanado(); ;
                object[] parametros = { Venta.Id_venta };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Venta.Conexion, "spCSLDB_Venta_get_ReporteCabeceraListaGanado", parametros);

                while (dr.Read())
                {
                    Item.NombreChofer = !dr.IsDBNull(dr.GetOrdinal("nombreChofer")) ? dr.GetString(dr.GetOrdinal("nombreChofer")) : string.Empty;
                    Item.UnidadVehiculo = !dr.IsDBNull(dr.GetOrdinal("numeroUnidad")) ? dr.GetString(dr.GetOrdinal("numeroUnidad")) : string.Empty;
                    Item.ModeloVehiculo = !dr.IsDBNull(dr.GetOrdinal("modelo")) ? dr.GetString(dr.GetOrdinal("modelo")) : string.Empty;
                    Item.MarcaVehiculo = !dr.IsDBNull(dr.GetOrdinal("marca")) ? dr.GetString(dr.GetOrdinal("marca")) : string.Empty;
                    Item.ColorVehiculo = !dr.IsDBNull(dr.GetOrdinal("color")) ? dr.GetString(dr.GetOrdinal("color")) : string.Empty;
                    Item.CapacidadVehiculo = !dr.IsDBNull(dr.GetOrdinal("capacidad")) ? dr.GetString(dr.GetOrdinal("capacidad")) : string.Empty;
                    Item.GPS = !dr.IsDBNull(dr.GetOrdinal("gps")) ? dr.GetString(dr.GetOrdinal("gps")) : string.Empty;
                    Item.FechaHoraSalida = !dr.IsDBNull(dr.GetOrdinal("fechaHoraSalida")) ? dr.GetString(dr.GetOrdinal("fechaHoraSalida")) : string.Empty;
                    Item.FechaHoraEmbarque = !dr.IsDBNull(dr.GetOrdinal("fechaHoraEmbarque")) ? dr.GetString(dr.GetOrdinal("fechaHoraEmbarque")) : string.Empty;
                    Item.LugarOrigen = !dr.IsDBNull(dr.GetOrdinal("lugarOrigen")) ? dr.GetString(dr.GetOrdinal("lugarOrigen")) : string.Empty;
                    Item.LugarDestino = !dr.IsDBNull(dr.GetOrdinal("lugarDestino")) ? dr.GetString(dr.GetOrdinal("lugarDestino")) : string.Empty;
                    Item.PSGOrigen = !dr.IsDBNull(dr.GetOrdinal("PSGOrigen")) ? dr.GetString(dr.GetOrdinal("PSGOrigen")) : string.Empty;
                    Item.PSGDestino = !dr.IsDBNull(dr.GetOrdinal("PSGDestino")) ? dr.GetString(dr.GetOrdinal("PSGDestino")) : string.Empty;
                    Item.TotalGanadoMachos = !dr.IsDBNull(dr.GetOrdinal("totalGanadoMachos")) ? dr.GetInt32(dr.GetOrdinal("totalGanadoMachos")) : 0;
                    Item.TotalGanadoHembras = !dr.IsDBNull(dr.GetOrdinal("totalGanadoHembras")) ? dr.GetInt32(dr.GetOrdinal("totalGanadoHembras")) : 0;
                    Item.TotalGanado = !dr.IsDBNull(dr.GetOrdinal("totalGanado")) ? dr.GetInt32(dr.GetOrdinal("totalGanado")) : 0;
                    Item.TotalKilosGanado = !dr.IsDBNull(dr.GetOrdinal("totalKilosGanado")) ? dr.GetDecimal(dr.GetOrdinal("totalKilosGanado")) : 0;
                    Item.PlacaJaula = !dr.IsDBNull(dr.GetOrdinal("placaJaula")) ? dr.GetString(dr.GetOrdinal("placaJaula")) : string.Empty;
                    Item.PlacaTracto = !dr.IsDBNull(dr.GetOrdinal("placas")) ? dr.GetString(dr.GetOrdinal("placas")) : string.Empty;
                }
                dr.Close();
                return Item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CatFierroModels> GetReporteFierrosVenta(VentaModels2 Venta)
        {
            try
            {
                CatFierroModels Item;
                List<CatFierroModels> Lista = new List<CatFierroModels>();

                object[] parametros = { Venta.Id_venta };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Venta.Conexion, "spCSLDB_Venta_get_ListaFierros", parametros);

                while (dr.Read())
                {
                    Item = new CatFierroModels();
                    Item.ImgFierro = !dr.IsDBNull(dr.GetOrdinal("fierro")) ? dr.GetString(dr.GetOrdinal("fierro")) : string.Empty;
                    Lista.Add(Item);
                }
                dr.Close();
                return Lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #endregion

        public List<CatRangoPesoVentaModels> GetListadoPrecioRangoPeso(string id_venta, string conexion)
        {
            try
            {
                object[] parametros = { id_venta };
                CatRangoPesoVentaModels RangoPeso;
                List<CatRangoPesoVentaModels> listaRangoPeso = new List<CatRangoPesoVentaModels>();

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(conexion, "spCSLDB_Venta_get_ListadoPrecioRangoPesoXIDTipoCliente", parametros);
                while (dr.Read())
                {
                    RangoPeso = new CatRangoPesoVentaModels
                    {
                        Id_rango = !dr.IsDBNull(dr.GetOrdinal("id_rango")) ? dr.GetInt32(dr.GetOrdinal("id_rango")) : 0,
                        PesoMinimo = !dr.IsDBNull(dr.GetOrdinal("pesoMinimo")) ? dr.GetDecimal(dr.GetOrdinal("pesoMinimo")) : 0,
                        PesoMaximo = !dr.IsDBNull(dr.GetOrdinal("pesoMaximo")) ? dr.GetDecimal(dr.GetOrdinal("pesoMaximo")) : 0,
                        EsMacho = !dr.IsDBNull(dr.GetOrdinal("esMacho")) ? dr.GetBoolean(dr.GetOrdinal("esMacho")) : false,
                        Precio = !dr.IsDBNull(dr.GetOrdinal("precio")) ? dr.GetDecimal(dr.GetOrdinal("precio")) : 0
                    };

                    listaRangoPeso.Add(RangoPeso);
                }
                dr.Close();
                return listaRangoPeso;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ComprobanteVentaCabeceraModels GetComprobanteVentaCabecera(VentaModels2 Venta)
        {
            try
            {
                ComprobanteVentaCabeceraModels Item = new ComprobanteVentaCabeceraModels();
                object[] parametros = { Venta.Id_venta };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Venta.Conexion, "spCSLDB_Venta_get_ComprobanteVentaDetallesCabecera", parametros);

                while (dr.Read())
                {
                    Item.LogoEmpresa = !dr.IsDBNull(dr.GetOrdinal("logoEmpresa")) ? dr.GetString(dr.GetOrdinal("logoEmpresa")) : string.Empty;
                    Item.NombreEmpresa = !dr.IsDBNull(dr.GetOrdinal("nombreEmpresa")) ? dr.GetString(dr.GetOrdinal("nombreEmpresa")) : string.Empty;
                    Item.RubroEmpresa = !dr.IsDBNull(dr.GetOrdinal("rubroEmpresa")) ? dr.GetString(dr.GetOrdinal("rubroEmpresa")) : string.Empty;
                    Item.TelefonoEmpresa = !dr.IsDBNull(dr.GetOrdinal("telefonoEmpresa")) ? dr.GetString(dr.GetOrdinal("telefonoEmpresa")) : string.Empty;
                    Item.DireccionEmpresa = !dr.IsDBNull(dr.GetOrdinal("direccionEmpresa")) ? dr.GetString(dr.GetOrdinal("direccionEmpresa")) : string.Empty;
                    Item.Folio = !dr.IsDBNull(dr.GetOrdinal("folio")) ? dr.GetString(dr.GetOrdinal("folio")) : string.Empty;
                    Item.NombreCliente = !dr.IsDBNull(dr.GetOrdinal("nombreCliente")) ? dr.GetString(dr.GetOrdinal("nombreCliente")) : string.Empty;
                    Item.TelefonoCliente = !dr.IsDBNull(dr.GetOrdinal("telefonoCliente")) ? dr.GetString(dr.GetOrdinal("telefonoCliente")) : string.Empty;
                    Item.RFCPCliente = !dr.IsDBNull(dr.GetOrdinal("rfcCliente")) ? dr.GetString(dr.GetOrdinal("rfcCliente")) : string.Empty;
                    Item.AnnoImpresion = !dr.IsDBNull(dr.GetOrdinal("anno")) ? dr.GetString(dr.GetOrdinal("anno")) : string.Empty;
                    Item.MesImpresion = !dr.IsDBNull(dr.GetOrdinal("mes")) ? dr.GetString(dr.GetOrdinal("mes")) : string.Empty;
                    Item.DiaImpresion = !dr.IsDBNull(dr.GetOrdinal("dia")) ? dr.GetString(dr.GetOrdinal("dia")) : string.Empty;
                    Item.TipoVenta = !dr.IsDBNull(dr.GetOrdinal("id_tipoVenta")) ? dr.GetInt32(dr.GetOrdinal("id_tipoVenta")) : 0;
                    Item.TotalPorCobrarGanado = !dr.IsDBNull(dr.GetOrdinal("TotalPorCobrarGanado")) ? dr.GetDecimal(dr.GetOrdinal("TotalPorCobrarGanado")) : 0;
                    Item.CostoFlete = !dr.IsDBNull(dr.GetOrdinal("costoFlete")) ? dr.GetDecimal(dr.GetOrdinal("costoFlete")) : 0;
                }
                dr.Close();
                return Item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ComprobanteVentaDetallesModels> GetComprobanteVentaDetalles(VentaModels2 Venta)
        {
            try
            {
                List<ComprobanteVentaDetallesModels> Lista = new List<ComprobanteVentaDetallesModels>();
                ComprobanteVentaDetallesModels Item;
                object[] parametros = { Venta.Id_venta };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Venta.Conexion, "spCSLDB_Venta_get_ComprobanteVentaDetalles", parametros);

                while (dr.Read())
                {
                    Item = new ComprobanteVentaDetallesModels();
                    Item.Cantidad = !dr.IsDBNull(dr.GetOrdinal("Cantidad")) ? dr.GetDecimal(dr.GetOrdinal("Cantidad")) : 0;
                    Item.Genero = !dr.IsDBNull(dr.GetOrdinal("Genero")) ? dr.GetString(dr.GetOrdinal("Genero")) : string.Empty;
                    Item.TotalKilos = !dr.IsDBNull(dr.GetOrdinal("totalKilos")) ? dr.GetDecimal(dr.GetOrdinal("totalKilos")) : 0;
                    Item.PrecioPorKilo = !dr.IsDBNull(dr.GetOrdinal("precioKilo")) ? dr.GetDecimal(dr.GetOrdinal("precioKilo")) : 0;
                    Item.Subtotal = !dr.IsDBNull(dr.GetOrdinal("precioTotal")) ? dr.GetDecimal(dr.GetOrdinal("precioTotal")) : 0;
                    Item.TipoVenta = !dr.IsDBNull(dr.GetOrdinal("tipoVenta")) ? dr.GetInt32(dr.GetOrdinal("tipoVenta")) : 0;

                    Lista.Add(Item);
                }
                dr.Close();

                if (Lista.Count == 0)
                {
                    Item = new ComprobanteVentaDetallesModels();
                    Item.Genero = "Sin gando";
                    Lista.Add(Item);
                }

                return Lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ComprobanteVentaPagosModels> GetComprobanteVentaDetallesPagos(VentaModels2 Venta)
        {
            try
            {
                List<ComprobanteVentaPagosModels> Lista = new List<ComprobanteVentaPagosModels>();
                ComprobanteVentaPagosModels Item;
                object[] parametros = { Venta.Id_venta };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Venta.Conexion, "spCSLDB_Venta_get_ComprobanteVentaDetallesPagos", parametros);

                while (dr.Read())
                {
                    Item = new ComprobanteVentaPagosModels();
                    Item.FormaPago = !dr.IsDBNull(dr.GetOrdinal("descripcion")) ? dr.GetString(dr.GetOrdinal("descripcion")) : string.Empty;
                    Item.FechaPago = !dr.IsDBNull(dr.GetOrdinal("fecha")) ? dr.GetString(dr.GetOrdinal("fecha")) : string.Empty;
                    Item.MontoPagado = !dr.IsDBNull(dr.GetOrdinal("monto")) ? dr.GetDecimal(dr.GetOrdinal("monto")) : 0;

                    Lista.Add(Item);
                }
                dr.Close();
                return Lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public VentaEnvioCorreo GetVentaEnvioCorreo(VentaEnvioCorreo Venta)
        {
            object[] parametros =
            {
                Venta.IdVenta
            };
            SqlDataReader dr = null;
            dr = SqlHelper.ExecuteReader(Venta.Conexion, "[dbo].[spCSLDB_Venta_get_correo]", parametros);
            while (dr.Read())
            {
                Venta.FechaEmbarque = !dr.IsDBNull(dr.GetOrdinal("FechaEmbarque")) ? dr.GetDateTime(dr.GetOrdinal("FechaEmbarque")) : DateTime.Today;
                Venta.ProveedorVenta = !dr.IsDBNull(dr.GetOrdinal("PorveedorVenta")) ? dr.GetString(dr.GetOrdinal("PorveedorVenta")) : string.Empty;
                Venta.CabezaHembras = !dr.IsDBNull(dr.GetOrdinal("CabezasHembras")) ? dr.GetInt32(dr.GetOrdinal("CabezasHembras")) : 0;
                Venta.PesoHembra = !dr.IsDBNull(dr.GetOrdinal("PesosHembras")) ? dr.GetDecimal(dr.GetOrdinal("PesosHembras")) : 0;
                Venta.CabezaMachos = !dr.IsDBNull(dr.GetOrdinal("CabezasMacho")) ? dr.GetInt32(dr.GetOrdinal("CabezasMacho")) : 0;
                Venta.PesoMachos = !dr.IsDBNull(dr.GetOrdinal("PesosMachos")) ? dr.GetDecimal(dr.GetOrdinal("PesosMachos")) : 0;
                Venta.TotalGeneral = !dr.IsDBNull(dr.GetOrdinal("TotalGeneral")) ? dr.GetDecimal(dr.GetOrdinal("TotalGeneral")) : 0;
                Venta.LugarDestino = !dr.IsDBNull(dr.GetOrdinal("lugarDestino")) ? dr.GetString(dr.GetOrdinal("lugarDestino")) : string.Empty;
                Venta.NombreChofer = !dr.IsDBNull(dr.GetOrdinal("NombreChofer")) ? dr.GetString(dr.GetOrdinal("NombreChofer")) : string.Empty;
                Venta.TelefonoMovil = !dr.IsDBNull(dr.GetOrdinal("TelefonoMovil")) ? dr.GetString(dr.GetOrdinal("TelefonoMovil")) : string.Empty;
                Venta.Modelo = !dr.IsDBNull(dr.GetOrdinal("Modelo")) ? dr.GetString(dr.GetOrdinal("Modelo")) : string.Empty;
                Venta.Color = !dr.IsDBNull(dr.GetOrdinal("Color")) ? dr.GetString(dr.GetOrdinal("Color")) : string.Empty;
                Venta.Placas = !dr.IsDBNull(dr.GetOrdinal("Placas")) ? dr.GetString(dr.GetOrdinal("Placas")) : string.Empty;
                Venta.GPS = !dr.IsDBNull(dr.GetOrdinal("GPS")) ? dr.GetString(dr.GetOrdinal("GPS")) : string.Empty;
                Venta.PlacasJaulas = !dr.IsDBNull(dr.GetOrdinal("PlacasJaulas")) ? dr.GetString(dr.GetOrdinal("PlacasJaulas")) : string.Empty;
                Venta.ColorJaula = !dr.IsDBNull(dr.GetOrdinal("ColorJaula")) ? dr.GetString(dr.GetOrdinal("ColorJaula")) : string.Empty;
                Venta.Marca = !dr.IsDBNull(dr.GetOrdinal("Marca")) ? dr.GetString(dr.GetOrdinal("Marca")) : string.Empty;
                Venta.ChoferAuxiliar = !dr.IsDBNull(dr.GetOrdinal("ChoferAuxiliar")) ? dr.GetString(dr.GetOrdinal("ChoferAuxiliar")) : string.Empty;
            }
            dr.Close();
            return Venta;
        }

        public VentaEnvioCorreo ObtenerComboCorreoClientesContacto(VentaEnvioCorreo Datos)
        {
            try
            {
                DataSet Ds = SqlHelper.ExecuteDataset(Datos.Conexion, "[dbo].[spCSLDB_Combo_get_CatClienteCorreo]", Datos.IdVenta);
                if (Ds != null)
                {
                    if (Ds.Tables.Count == 1)
                    {
                        List<VentaEnvioCorreo> ListaPrinc = new List<VentaEnvioCorreo>();
                        VentaEnvioCorreo Item;
                        DataTableReader DTR = Ds.Tables[0].CreateDataReader();
                        DataTable Tbl1 = Ds.Tables[0];
                        while (DTR.Read())
                        {
                            Item = new VentaEnvioCorreo();
                            Item.VentaCorreoDetalle = new List<VentaEnvioCorreo>();
                            Item.Correo = !DTR.IsDBNull(DTR.GetOrdinal("CorreoCliente")) ? DTR.GetString(DTR.GetOrdinal("CorreoCliente")) : string.Empty;
                            Item.Tipo = !DTR.IsDBNull(DTR.GetOrdinal("Cliente")) ? DTR.GetString(DTR.GetOrdinal("Cliente")) : string.Empty;
                            Item.NombreCliente = !DTR.IsDBNull(DTR.GetOrdinal("NombreCliente")) ? DTR.GetString(DTR.GetOrdinal("NombreCliente")) : string.Empty;
                            //string Aux = DTR.GetString(2);
                            string Aux = !DTR.IsDBNull(DTR.GetOrdinal("TablaContacto")) ? DTR.GetString(DTR.GetOrdinal("TablaContacto")) : string.Empty;
                            Aux = string.Format("<Main>{0}</Main>", Aux);
                            XmlDocument xm = new XmlDocument();
                            xm.LoadXml(Aux);
                            XmlNodeList Registros = xm.GetElementsByTagName("Main");
                            XmlNodeList Lista = ((XmlElement)Registros[0]).GetElementsByTagName("B");
                            List<VentaEnvioCorreo> ListaAux = new List<VentaEnvioCorreo>();
                            VentaEnvioCorreo ItemAux;
                            foreach (XmlElement Nodo in Lista)
                            {
                                ItemAux = new VentaEnvioCorreo();
                                XmlNodeList CorreoContacto = Nodo.GetElementsByTagName("CorreoContacto");
                                XmlNodeList Tipo = Nodo.GetElementsByTagName("Contacto");
                                XmlNodeList Nombre = Nodo.GetElementsByTagName("NombreContacto");
                                ItemAux.Correo = CorreoContacto[0].InnerText;
                                ItemAux.Tipo = Tipo[0].InnerText;
                                ItemAux.NombreCliente = Nombre[0].InnerText;
                                Item.VentaCorreoDetalle.Add(ItemAux);
                            }
                            ListaPrinc.Add(Item);
                        }
                        DTR.Close();
                        Datos.VentaCorreo = ListaPrinc;
                    }
                }
                return Datos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ComprobanteVentaDetallesDeduccionesModels> GetComprobanteVentaDetallesDeducciones(VentaModels2 Venta)
        {
            try
            {
                List<ComprobanteVentaDetallesDeduccionesModels> Lista = new List<ComprobanteVentaDetallesDeduccionesModels>();
                ComprobanteVentaDetallesDeduccionesModels Item;
                object[] parametros = { Venta.Id_venta };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Venta.Conexion, "spCSLDB_Venta_get_ComprobanteVentaDetallesDeducciones", parametros);

                while (dr.Read())
                {
                    Item = new ComprobanteVentaDetallesDeduccionesModels();
                    Item.Descripcion = !dr.IsDBNull(dr.GetOrdinal("descripcion")) ? dr.GetString(dr.GetOrdinal("descripcion")) : string.Empty;
                    Item.Monto = !dr.IsDBNull(dr.GetOrdinal("total")) ? dr.GetDecimal(dr.GetOrdinal("total")) : 0;
                    Item.Fecha = !dr.IsDBNull(dr.GetOrdinal("fecins")) ? dr.GetDateTime(dr.GetOrdinal("fecins")) : DateTime.Today;

                    Lista.Add(Item);
                }
                dr.Close();

                return Lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}