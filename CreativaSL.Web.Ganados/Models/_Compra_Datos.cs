using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace CreativaSL.Web.Ganados.Models
{
    public class _Compra_Datos
    {
        #region Json Datatables
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
        public string GetDocumentosPorCobrarDetalles(CompraModels Compra)
        {
            object[] parametros =
            {
                Compra.IDCompra
            };

            try
            {
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Compras_get_DocumentoPorCobrarDetalles", parametros);
                string datatable = Auxiliar.SqlReaderToJson(dr);
                dr.Close();
                return datatable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string TableJsonEventoCompra(CompraModels Compra)
        {
            object[] parametros =
            {
                Compra.IDCompra
            };

            try
            {
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Compras_get_EventosCompra", parametros);
                string datatable = Auxiliar.SqlReaderToJson(dr);
                dr.Close();
                return datatable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string GetProductoGanadoNoAccidentadoXIDCompra(EventoEnvioModels Evento)
        {
            try
            {
                object[] parametros =
                    {
                        Evento.IDCompra,
                        Evento.IDEvento
                    };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Evento.Conexion, "spCSLDB_Compra_get_GanadoSINEventoXIDCompra", parametros);
                string datatable = Auxiliar.SqlReaderToJson(dr);
                dr.Close();
                return datatable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string GetProductoGanadoAccidentadoXIDEvento(EventoEnvioModels Evento)
        {
            try
            {
                object[] parametros =
                    {
                        Evento.IDCompra,
                        Evento.IDEvento
                    };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Evento.Conexion, "spCSLDB_Compra_get_GanadoCONEventoXIDCompra", parametros);
                string datatable = Auxiliar.SqlReaderToJson(dr);
                dr.Close();
                return datatable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #region DatatableDocumentosPorCobrarDetalles
        public string DatatableDocumentosPorCobrarDetalles(DocumentosPorCobrarDetalleModels Documento)
        {
            object[] parametros =
            {
                Documento.Id_documentoCobrar,
                Documento.Id_servicio
            };

            try
            {
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Documento.Conexion, "spCSLDB_Compra_get_DocumentosPorCobrarDetalles", parametros);
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
        #region DatatableDocumentosPorCobrarDetallesPagos
        public string DatatableDocumentosPorCobrarDetallesPagos(DocumentosPorCobrarDetallePagosModels Documento)
        {
            object[] parametros =
            {
                Documento.Id_documentoPorCobrar,
                Documento.Id_compra
            };

            try
            {
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Documento.Conexion, "spCSLDB_Compra_get_DetallesDocumentoPorCobrarCobros", parametros);
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

        #region DatatableDocumentosPorPagarDetallesPagos
        public string DatatableDocumentosPorPagarDetallesPagos(DocumentoPorPagarDetallePagosModels Documento)
        {
            object[] parametros =
            {
                Documento.Id_documentoPorPagar,
                Documento.Id_compra
            };

            try
            {
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Documento.Conexion, "spCSLDB_Compra_get_DetallesDocumentoPorPagarCobros", parametros);
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


        #region Index
        public string ObtenerCompraIndexDataTable(CompraModels CompraModels)
        {
            try
            {
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(CompraModels.Conexion, "spCSLDB_Compras_IndexVentas");
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
        #region Ganado
        public string TableJsonGanadoCompra(CompraModels Compra)
        {
            try
            {
                object[] parametros = { Compra.IDCompra };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Compras_get_Ganado", parametros);
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
        #region DatatableDocumentosPorPagarDetalles
        public string DatatableDocumentosPorPagarDetalles(string Id_documentoPorPagar, string Id_compra, string conexion)
        {
            object[] parametros =
            {
                Id_documentoPorPagar,
                Id_compra
            };

            try
            {
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(conexion, "spCSLDB_Compra_get_DocumentosPorPagarDetalles", parametros);
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
        #region Detalles
        public string JsonGeneralesGanado(CompraModels Compra)
        {
            try
            {
                object[] parametros =
                    {
                        Compra.IDCompra
                    };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Compras_get_JsonGeneralesGanado", parametros);
                string datatable = Auxiliar.SqlReaderToJson(dr);
                dr.Close();
                return datatable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string JsonDetallesGanadoMacho(CompraModels Compra)
        {
            try
            {
                object[] parametros =
                    {
                        Compra.IDCompra
                    };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Compra_get_DetallesGanadoMacho", parametros);
                string datatable = Auxiliar.SqlReaderToJson(dr);
                dr.Close();
                return datatable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string JsonDetallesGanadoHembra(CompraModels Compra)
        {
            try
            {
                object[] parametros =
                    {
                        Compra.IDCompra
                    };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Compra_get_DetallesGanadoHembra", parametros);
                string datatable = Auxiliar.SqlReaderToJson(dr);
                dr.Close();
                return datatable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string JsonDetallesDocXpagar(CompraModels Compra)
        {
            try
            {
                object[] parametros =
                    {
                        Compra.IDCompra
                    };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Compra_get_DetallesDocXpagar", parametros);
                string datatable = Auxiliar.SqlReaderToJson(dr);
                dr.Close();
                return datatable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string DatatableDetalleDocXCobrarPagos(CompraModels Compra)
        {
            try
            {
                object[] parametros =
                    {
                        Compra.IDCompra
                    };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Compra_get_DetallesDocXcobrarPAGOS", parametros);
                string datatable = Auxiliar.SqlReaderToJson(dr);
                dr.Close();
                return datatable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string DatatableDetalleDocXPagarPagos(CompraModels Compra)
        {
            try
            {
                object[] parametros =
                    {
                        Compra.IDCompra
                    };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Compra_get_DetallesDocXpagarPAGOS", parametros);
                string datatable = Auxiliar.SqlReaderToJson(dr);
                dr.Close();
                return datatable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string JsonDetallesDocXcobrar(CompraModels Compra)
        {
            try
            {
                object[] parametros =
                    {
                        Compra.IDCompra
                    };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Compra_get_DetallesDocXcobrar", parametros);
                string datatable = Auxiliar.SqlReaderToJson(dr);
                dr.Close();
                return datatable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string GetGanadoProgramado(CompraModels Compra)
        {
            try
            {
                object[] parametros =
                    {
                        Compra.IDCompra
                    };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Compra_get_GanadoProgramadoXIDCompra", parametros);
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
        public string DatatableImpuestoXIdDocDetalle(CompraImpuestoModels CompraImpuesto)
        {
            try
            {
                object[] parametros =
                {
                    CompraImpuesto.IDCompra,
                    CompraImpuesto.Id_detalleDoctoCobrar
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(CompraImpuesto.Conexion, "spCSLDB_Compra_get_ImpuestoXIDDocumentoDetalle", parametros);
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
        public List<CatSucursalesModels> GetListadoSucursales(CompraModels Compra)
        {
            CatSucursalesModels Sucursal;
            SqlDataReader dr = null;
            object[] parametros =
            {
                1
            };

            dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Combo_get_SucursalesXTipoEmpresa", parametros);

            while (dr.Read())
            {
                Sucursal = new CatSucursalesModels
                {
                    IDSucursal = !dr.IsDBNull(dr.GetOrdinal("IDSucursal")) ? dr.GetString(dr.GetOrdinal("IDSucursal")) : string.Empty,
                    NombreSucursal = !dr.IsDBNull(dr.GetOrdinal("NombreSucursal")) ? dr.GetString(dr.GetOrdinal("NombreSucursal")) : string.Empty,
                };

                Compra.ListaSucursales.Add(Sucursal);
            }
            dr.Close();
            return Compra.ListaSucursales;
        }
        #endregion
        #region Proveedores
        public List<CatProveedorModels> GetListaProveedores(CompraModels Compra)
        {
            try
            {
                CatProveedorModels Proveedor;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Combo_get_CatProveedorConLugar");
                while (dr.Read())
                {
                    Proveedor = new CatProveedorModels
                    {
                        IDProveedor = !dr.IsDBNull(dr.GetOrdinal("IDProveedor")) ? dr.GetString(dr.GetOrdinal("IDProveedor")) : string.Empty,
                        NombreRazonSocial = !dr.IsDBNull(dr.GetOrdinal("NombreProveedor")) ? dr.GetString(dr.GetOrdinal("NombreProveedor")) : string.Empty,

                    };
                    Compra.ListaProveedores.Add(Proveedor);
                }
                dr.Close();
                return Compra.ListaProveedores;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Lugares
        public List<CatLugarModels> GetListadoLugares(CompraModels Compra)
        {
            try
            {
                List<CatLugarModels> ListaLugares = new List<CatLugarModels>();
                CatLugarModels Lugar = new CatLugarModels();

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Combo_get_CatLugarXIDEmpresa");
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
        public List<CatLugarModels> GetListadoLugaresProveedorXIDProveedor(CompraModels Compra)
        {
            try
            {
                CatLugarModels Lugar = new CatLugarModels();
                object[] parametros =
                {
                    Compra.IDProveedor
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Combo_get_CatLugarProvedorXIDProveedor", parametros);
                while (dr.Read())
                {
                    Lugar = new CatLugarModels
                    {
                        id_lugar = !dr.IsDBNull(dr.GetOrdinal("IDLugar")) ? dr.GetString(dr.GetOrdinal("IDLugar")) : string.Empty,
                        descripcion = !dr.IsDBNull(dr.GetOrdinal("NombreLugar")) ? dr.GetString(dr.GetOrdinal("NombreLugar")) : string.Empty,
                        latitud = float.Parse(dr["GpsLatitud"].ToString()),
                        longitud = float.Parse(dr["GpsLongitud"].ToString()),
                        Direccion = !dr.IsDBNull(dr.GetOrdinal("Direccion")) ? dr.GetString(dr.GetOrdinal("Direccion")) : string.Empty
                    };

                    Compra.ListaLugaresProveedor.Add(Lugar);
                }
                dr.Close();
                return Compra.ListaLugaresProveedor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CatLugarModels> GetListadoLugaresLugarXIDEmpresa(CompraModels Compra)
        {
            try
            {
                CatLugarModels Lugar = new CatLugarModels();
                List<CatLugarModels> listaLugares = new List<CatLugarModels>();
                object[] parametros =
                {
                    Compra.IDEmpresa
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Combo_get_CatLugarXIDEmpresa", parametros);
                while (dr.Read())
                {
                    Lugar = new CatLugarModels();

                    Lugar.id_lugar = !dr.IsDBNull(dr.GetOrdinal("IDLugar")) ? dr.GetString(dr.GetOrdinal("IDLugar")) : string.Empty;
                    Lugar.descripcion = !dr.IsDBNull(dr.GetOrdinal("NombreLugar")) ? dr.GetString(dr.GetOrdinal("NombreLugar")) : string.Empty;
                    Lugar.latitud = float.Parse(dr["GpsLatitud"].ToString());
                    Lugar.longitud = float.Parse(dr["GpsLongitud"].ToString());

                    listaLugares.Add(Lugar);
                }
                dr.Close();
                return listaLugares;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Empresa
        public List<CatEmpresaModels> GetListadoEmpresas(CompraModels Compra)
        {
            try
            {
                object[] parametros = { Compra.TipoFlete };
                CatEmpresaModels Empresa;

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Combo_get_CatEmpresaCompra", parametros);
                while (dr.Read())
                {
                    Empresa = new CatEmpresaModels
                    {
                        IDEmpresa = !dr.IsDBNull(dr.GetOrdinal("IDEmpresa")) ? dr.GetString(dr.GetOrdinal("IDEmpresa")) : string.Empty,
                        RazonFiscal = !dr.IsDBNull(dr.GetOrdinal("NombreEmpresa")) ? dr.GetString(dr.GetOrdinal("NombreEmpresa")) : string.Empty,
                    };

                    Compra.ListaEmpresas.Add(Empresa);
                }
                dr.Close();
                return Compra.ListaEmpresas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Choferes
        public List<CatChoferModels> GetChoferesXIDEmpresa(CompraModels Compra)
        {
            try
            {
                object[] parametros =
                {
                    Compra.IDEmpresa
                };
                CatChoferModels Chofer;

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Combo_get_CatChoferesXIDEmpresa", parametros);
                while (dr.Read())
                {
                    Chofer = new CatChoferModels
                    {
                        IDChofer = !dr.IsDBNull(dr.GetOrdinal("IDChofer")) ? dr.GetString(dr.GetOrdinal("IDChofer")) : string.Empty,
                        Nombre = !dr.IsDBNull(dr.GetOrdinal("NombreCompleto")) ? dr.GetString(dr.GetOrdinal("NombreCompleto")) : string.Empty,
                    };

                    Compra.ListaChoferes.Add(Chofer);
                }
                dr.Close();
                return Compra.ListaChoferes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Vehiculos
        public List<CatVehiculoModels> GetVehiculosXIDEmpresa(CompraModels Compra)
        {
            try
            {
                object[] parametros =
                {
                    Compra.IDEmpresa
                };
                CatVehiculoModels Vehiculo;

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Combo_get_CatVehiculosXIDEmpresa", parametros);
                while (dr.Read())
                {
                    Vehiculo = new CatVehiculoModels
                    {
                        IDVehiculo = !dr.IsDBNull(dr.GetOrdinal("IDVehiculo")) ? dr.GetString(dr.GetOrdinal("IDVehiculo")) : string.Empty,
                        nombreMarca = !dr.IsDBNull(dr.GetOrdinal("NombreVehiculo")) ? dr.GetString(dr.GetOrdinal("NombreVehiculo")) : string.Empty,
                        Modelo = !dr.IsDBNull(dr.GetOrdinal("Tipo")) ? dr.GetString(dr.GetOrdinal("Tipo")) : string.Empty
                    };

                    Compra.ListaVehiculos.Add(Vehiculo);
                }
                dr.Close();
                return Compra.ListaVehiculos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Jaulas
        public List<CatJaulaModels> GetJaulasXIDEmpresa(CompraModels Compra)
        {
            try
            {
                object[] parametros =
                {
                    Compra.IDEmpresa
                };
                CatJaulaModels Jaula;

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Combo_get_CatJaulaXIDEmpresa", parametros);
                while (dr.Read())
                {
                    Jaula = new CatJaulaModels
                    {
                        IDJaula = !dr.IsDBNull(dr.GetOrdinal("IDJaula")) ? dr.GetString(dr.GetOrdinal("IDJaula")) : string.Empty,
                        Matricula = !dr.IsDBNull(dr.GetOrdinal("Placa")) ? dr.GetString(dr.GetOrdinal("Placa")) : string.Empty,
                    };

                    Compra.ListaJaulas.Add(Jaula);
                }
                dr.Close();
                return Compra.ListaJaulas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Remolques
        public List<CatRemolqueModels> GetRemolquesXIDEmpresa(CompraModels Compra)
        {
            try
            {
                object[] parametros =
                {
                    Compra.IDEmpresa
                };
                CatRemolqueModels Remolque;

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Combo_get_CatRemolqueXIDEmpresa", parametros);
                while (dr.Read())
                {
                    Remolque = new CatRemolqueModels
                    {
                        IDRemolque = !dr.IsDBNull(dr.GetOrdinal("IDRemolque")) ? dr.GetString(dr.GetOrdinal("IDRemolque")) : string.Empty,
                        placa = !dr.IsDBNull(dr.GetOrdinal("Placa")) ? dr.GetString(dr.GetOrdinal("Placa")) : string.Empty,
                    };

                    Compra.ListaRemolques.Add(Remolque);
                }
                dr.Close();
                return Compra.ListaRemolques;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Corrales
        public List<CatCorralesModels> GetListaCorrales(CompraModels Compra)
        {
            CatCorralesModels item;
            List<CatCorralesModels> lista = new List<CatCorralesModels>();
            SqlDataReader dr = null;

            dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Combo_get_CatCorrales");

            while (dr.Read())
            {
                item = new CatCorralesModels();

                item.Id_corral = !dr.IsDBNull(dr.GetOrdinal("IDCorral")) ? dr.GetInt32(dr.GetOrdinal("IDCorral")) : 0;
                item.Descripcion = !dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? dr.GetString(dr.GetOrdinal("Descripcion")) : string.Empty;

                lista.Add(item);
            }
            dr.Close();
            return lista;
        }
        #endregion
        #region Fierros
        public List<CatFierroModels> GetListaFierros(CompraModels Compra)
        {
            CatFierroModels item;
            List<CatFierroModels> lista = new List<CatFierroModels>();
            SqlDataReader dr = null;

            dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Combo_get_CatFierro");

            while (dr.Read())
            {
                item = new CatFierroModels();
                item.IDFierro = !dr.IsDBNull(dr.GetOrdinal("ID")) ? dr.GetString(dr.GetOrdinal("ID")) : string.Empty;
                item.NombreFierro = !dr.IsDBNull(dr.GetOrdinal("Nombre")) ? dr.GetString(dr.GetOrdinal("Nombre")) : string.Empty;
                lista.Add(item);
            }
            dr.Close();
            return lista;
        }
        #endregion
        #region Metodo de pago
        public List<CFDI_MetodoPagoModels> GetMetodosPagos(CompraModels Compra)
        {
            try
            {
                CFDI_MetodoPagoModels item;
                List<CFDI_MetodoPagoModels> lista = new List<CFDI_MetodoPagoModels>();

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Combo_get_CFDIMetodoPago");
                while (dr.Read())
                {
                    item = new CFDI_MetodoPagoModels
                    {
                        Clave = !dr.IsDBNull(dr.GetOrdinal("ID")) ? dr.GetString(dr.GetOrdinal("ID")) : string.Empty,
                        Descripcion = !dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? dr.GetString(dr.GetOrdinal("Descripcion")) : string.Empty
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
        #region CFDI Forma de pago
        public List<CFDI_FormaPagoModels> GetListadoCFDIFormaPago(CompraModels Compra)
        {
            try
            {
                CFDI_FormaPagoModels item;
                List<CFDI_FormaPagoModels> lista = new List<CFDI_FormaPagoModels>();
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Combo_get_CFDIFormaPago");
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
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Cuentas bancarias grupo ocampo
        public List<CuentaBancariaModels> GetListadoCuentasBancariasGrupoOcampo(CompraModels Compra)
        {
            try
            {
                CuentaBancariaModels item;
                List<CuentaBancariaModels> lista = new List<CuentaBancariaModels>();
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Combo_get_CuentasBancariasGrupoOcampo");
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
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Cuentas bancarias proveedor
        public List<CuentaBancariaModels> GetListadoCuentasBancariasProveedorXIDProveedor(CompraModels Compra)
        {
            try
            {
                object[] parametros =
                {
                    Compra.IDProveedor
                };
                CuentaBancariaModels item;
                List<CuentaBancariaModels> lista = new List<CuentaBancariaModels>();
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Combo_get_CuentasBancariasProveedorXIDProveedor", parametros);
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
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Tipo de clasificacion cobro
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
        #region Tipo de clasificacion pago
        public List<CatTipoClasificacionModels> GetListadoTipoClasificacionPago(DocumentoModels Documento)
        {
            try
            {
                CatTipoClasificacionModels item;
                List<CatTipoClasificacionModels> lista = new List<CatTipoClasificacionModels>();
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Documento.Conexion, "spCSLDB_Combo_get_CatTipoClasificacionAll");
                while (dr.Read())
                {
                    item = new CatTipoClasificacionModels
                    {
                        IDTipoClasificacionGasto = !dr.IsDBNull(dr.GetOrdinal("IDTipoClasificacion")) ? dr.GetInt32(dr.GetOrdinal("IDTipoClasificacion")) : 0,
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
        #endregion

        #region Otras funciones
        public CompraModels CambiarEstatusCompra(CompraModels Compra)
        {
            try
            {
                object[] parametros =
                {
                     Compra.IDCompra
                    ,Compra.Usuario
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Compras_CambiarStatus", parametros);

                while (dr.Read())
                {
                    Compra.RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                    Compra.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : true;
                }
                dr.Close();
                return Compra;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //LISTA DE COMPRAS NO FINALIZADAS
        public List<CalendarioModels> GetListaComprasNofinalizadas(CalendarioModels Compra)
        {

            try
            {
                List<CalendarioModels> Lista = new List<CalendarioModels>();
                CalendarioModels Item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Inicio_get_ComprasNoFinalizadas", Compra.fechaStart, Compra.fechaEnd);
                while (dr.Read())
                {
                    Item = new CalendarioModels();
                    Item.IDProveedor = !dr.IsDBNull(dr.GetOrdinal("nombreRazonSocial")) ? dr.GetString(dr.GetOrdinal("nombreRazonSocial")) : string.Empty;
                    DateTime FechaHoraProgramada = !dr.IsDBNull(dr.GetOrdinal("fechaHoraProgramada")) ? dr.GetDateTime(dr.GetOrdinal("fechaHoraProgramada")) : DateTime.Now;
                    Item.start = FechaHoraProgramada.ToString("yyyy-MM-dd");
                    Item.title = Item.IDProveedor;
                    Item.Estatus = !dr.IsDBNull(dr.GetOrdinal("estatus")) ? dr.GetInt16(dr.GetOrdinal("estatus")) : 0;
                    Item.GanadosPactadoMachos = !dr.IsDBNull(dr.GetOrdinal("ganadoPactadoMachos")) ? dr.GetInt32(dr.GetOrdinal("ganadoPactadoMachos")) : 0;
                    Item.GanadosPactadoHembras = !dr.IsDBNull(dr.GetOrdinal("ganadoPactadoHembras")) ? dr.GetInt32(dr.GetOrdinal("ganadoPactadoHembras")) : 0;
                    Item.GuiaTransito = !dr.IsDBNull(dr.GetOrdinal("guiaTransito")) ? dr.GetString(dr.GetOrdinal("guiaTransito")) : string.Empty;
                    Item.estatusDesc = !dr.IsDBNull(dr.GetOrdinal("estatusDesc")) ? dr.GetString(dr.GetOrdinal("estatusDesc")) : string.Empty;

                    Lista.Add(Item);
                }
                dr.Close();
                Compra.listaCompra = Lista;
                return Compra.listaCompra;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Get
        public CompraModels GetRecepcionCompra(CompraModels Compra)
        {
            try
            {
                object[] parametros =
                {
                    Compra.IDCompra
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Compras_get_RecepcionCompra", parametros);

                while (dr.Read())
                {
                    Compra.IDRecepcion = !dr.IsDBNull(dr.GetOrdinal("id_recepcionOrigenCompra")) ? dr.GetString(dr.GetOrdinal("id_recepcionOrigenCompra")) : string.Empty;
                    Compra.RecepcionOrigen = new RecepcionOrigenModels();
                    Compra.RecepcionOrigen.KilometrajeFinal = !dr.IsDBNull(dr.GetOrdinal("kilometrajeFinal")) ? dr.GetInt32(dr.GetOrdinal("kilometrajeFinal")) : 0;
                    Compra.RecepcionOrigen.FechaLlegada = !dr.IsDBNull(dr.GetOrdinal("fechaLlegada")) ? dr.GetDateTime(dr.GetOrdinal("fechaLlegada")) : DateTime.Now;
                    Compra.RecepcionOrigen.HoraLlegada = !dr.IsDBNull(dr.GetOrdinal("horaLlegada")) ? dr.GetTimeSpan(dr.GetOrdinal("horaLlegada")) : DateTime.Now.TimeOfDay;
                    Compra.RecepcionOrigen.Observacion = !dr.IsDBNull(dr.GetOrdinal("observacion")) ? dr.GetString(dr.GetOrdinal("observacion")) : string.Empty;
                }
                dr.Close();
                return Compra;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int GetEstatusCompra(CompraModels Compra)
        {
            try
            {
                object[] parametros =
                {
                    Compra.IDCompra,
                };

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Compras_get_EstatusCompra", parametros);
                while (dr.Read())
                {
                    Compra.Estatus = !dr.IsDBNull(dr.GetOrdinal("estatus")) ? dr.GetInt16(dr.GetOrdinal("estatus")) : -1;
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Compra.Estatus;
        }
        public CompraModels GetCompraProgramada(CompraModels Compra)
        {
            try
            {
                object[] parametros =
                {
                    Compra.IDCompra
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Compras_get_CompraProgramada", parametros);

                while (dr.Read())
                {
                    Compra.IDSucursal = !dr.IsDBNull(dr.GetOrdinal("id_sucursal")) ? dr.GetString(dr.GetOrdinal("id_sucursal")) : string.Empty;
                    Compra.FechaHoraProgramada = !dr.IsDBNull(dr.GetOrdinal("fechaHoraProgramada")) ? dr.GetDateTime(dr.GetOrdinal("fechaHoraProgramada")) : DateTime.Now;
                    Compra.IDProveedor = !dr.IsDBNull(dr.GetOrdinal("id_proveedor")) ? dr.GetString(dr.GetOrdinal("id_proveedor")) : string.Empty;
                    Compra.IDPLugarProveedor = !dr.IsDBNull(dr.GetOrdinal("id_lugar_proveedor")) ? dr.GetString(dr.GetOrdinal("id_lugar_proveedor")) : string.Empty;
                    Compra.GanadosPactadoMachos = !dr.IsDBNull(dr.GetOrdinal("ganadoPactadoMachos")) ? dr.GetInt32(dr.GetOrdinal("ganadoPactadoMachos")) : 0;
                    Compra.GanadosPactadoHembras = !dr.IsDBNull(dr.GetOrdinal("ganadoPactadoHembras")) ? dr.GetInt32(dr.GetOrdinal("ganadoPactadoHembras")) : 0;
                    Compra.Folio = !dr.IsDBNull(dr.GetOrdinal("folio")) ? dr.GetString(dr.GetOrdinal("folio")) : string.Empty;
                    Compra.Observacion_programacion = !dr.IsDBNull(dr.GetOrdinal("observacion_programacion")) ? dr.GetString(dr.GetOrdinal("observacion_programacion")) : string.Empty;
                    Compra.Descripcion_bascula = !dr.IsDBNull(dr.GetOrdinal("descripcion_bascula")) ? dr.GetString(dr.GetOrdinal("descripcion_bascula")) : string.Empty;
                    Compra.HoraProgramada = !dr.IsDBNull(dr.GetOrdinal("horaProgramada")) ? dr.GetTimeSpan(dr.GetOrdinal("horaProgramada")) : DateTime.Today.TimeOfDay;
                }
                dr.Close();
                return Compra;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public CompraModels GetCompraEmbarque(CompraModels Compra)
        {
            try
            {
                object[] parametros =
                {
                    Compra.IDCompra
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Compras_get_CompraEmbarque", parametros);

                while (dr.Read())
                {
                    Compra.IDFlete = !dr.IsDBNull(dr.GetOrdinal("id_flete")) ? dr.GetString(dr.GetOrdinal("id_flete")) : string.Empty;
                    Compra.IDProveedor = !dr.IsDBNull(dr.GetOrdinal("id_proveedor")) ? dr.GetString(dr.GetOrdinal("id_proveedor")) : string.Empty;
                    Compra.IDEmpresa = !dr.IsDBNull(dr.GetOrdinal("id_empresa")) ? dr.GetString(dr.GetOrdinal("id_empresa")) : string.Empty;
                    Compra.IDChofer = !dr.IsDBNull(dr.GetOrdinal("id_chofer")) ? dr.GetString(dr.GetOrdinal("id_chofer")) : string.Empty;
                    Compra.IDVehiculo = !dr.IsDBNull(dr.GetOrdinal("id_vehiculo")) ? dr.GetString(dr.GetOrdinal("id_vehiculo")) : string.Empty;
                    Compra.Flete.kmInicialVehiculo = !dr.IsDBNull(dr.GetOrdinal("kmInicialVehiculo")) ? dr.GetInt32(dr.GetOrdinal("kmInicialVehiculo")) : 0;
                    Compra.Trayecto.id_lugarOrigen = !dr.IsDBNull(dr.GetOrdinal("id_lugarOrigen")) ? dr.GetString(dr.GetOrdinal("id_lugarOrigen")) : string.Empty;
                    Compra.Trayecto.id_lugarDestino = !dr.IsDBNull(dr.GetOrdinal("id_lugarDestino")) ? dr.GetString(dr.GetOrdinal("id_lugarDestino")) : string.Empty;
                    Compra.IDPLugarProveedor = !dr.IsDBNull(dr.GetOrdinal("id_lugar_proveedor")) ? dr.GetString(dr.GetOrdinal("id_lugar_proveedor")) : string.Empty;
                    Compra.DocumentosPorCobrarDetallePagos.Monto = !dr.IsDBNull(dr.GetOrdinal("precioFlete")) ? dr.GetDecimal(dr.GetOrdinal("precioFlete")) : 0;
                    //Compra.Flete.NumFleje = !dr.IsDBNull(dr.GetOrdinal("numFleje")) ? dr.GetString(dr.GetOrdinal("numFleje")) : string.Empty;
                    Compra.TipoFlete = !dr.IsDBNull(dr.GetOrdinal("TipoFlete")) ? dr.GetInt16(dr.GetOrdinal("TipoFlete")) : 0;
                    //Compra.Flete.Id_metodoPago = !dr.IsDBNull(dr.GetOrdinal("id_metodoPago")) ? dr.GetString(dr.GetOrdinal("id_metodoPago")) : string.Empty;
                    //Compra.Flete.CondicionPago = !dr.IsDBNull(dr.GetOrdinal("condicionPago")) ? dr.GetString(dr.GetOrdinal("condicionPago")) : string.Empty;
                    //Compra.DocumentosPorCobrarDetallePagos.Id_formaPago = !dr.IsDBNull(dr.GetOrdinal("id_formaPago")) ? dr.GetInt16(dr.GetOrdinal("id_formaPago")) : 0;
                    //Compra.DocumentosPorCobrarDetallePagos.fecha = !dr.IsDBNull(dr.GetOrdinal("fechaPago")) ? dr.GetDateTime(dr.GetOrdinal("fechaPago")) : DateTime.Now;
                    //Compra.DocumentosPorCobrarDetallePagos.Observacion = !dr.IsDBNull(dr.GetOrdinal("observacionPago")) ? dr.GetString(dr.GetOrdinal("observacionPago")) : string.Empty;
                    //Compra.DocumentosPorCobrarDetallePagos.Bancarizado = !dr.IsDBNull(dr.GetOrdinal("bancarizado")) ? dr.GetBoolean(dr.GetOrdinal("bancarizado")) : false;
                    //Compra.DocumentosPorCobrarDetallePagos.Id_cuentaBancariaOrdenante = !dr.IsDBNull(dr.GetOrdinal("id_cuentaBancariaOrdenante")) ? dr.GetString(dr.GetOrdinal("id_cuentaBancariaOrdenante")) : string.Empty;
                    //Compra.DocumentosPorCobrarDetallePagos.Id_cuentaBancariaBeneficiante = !dr.IsDBNull(dr.GetOrdinal("id_cuentaBancariaBeneficiante")) ? dr.GetString(dr.GetOrdinal("id_cuentaBancariaBeneficiante")) : string.Empty;
                    //Compra.DocumentosPorCobrarDetallePagos.NombreBancoOrdenante = !dr.IsDBNull(dr.GetOrdinal("nombreBancoOrdenante")) ? dr.GetString(dr.GetOrdinal("nombreBancoOrdenante")) : string.Empty;
                    //Compra.DocumentosPorCobrarDetallePagos.NombreBancoBeneficiante = !dr.IsDBNull(dr.GetOrdinal("nombreBancoBeneficiante")) ? dr.GetString(dr.GetOrdinal("nombreBancoBeneficiante")) : string.Empty;
                    //Compra.DocumentosPorCobrarDetallePagos.NumeroAutorizacion = !dr.IsDBNull(dr.GetOrdinal("numeroAutorizacion")) ? dr.GetString(dr.GetOrdinal("numeroAutorizacion")) : string.Empty;
                    //Compra.DocumentosPorCobrarDetallePagos.NumCuentaOrdenante = !dr.IsDBNull(dr.GetOrdinal("numCuentaOrdenante")) ? dr.GetString(dr.GetOrdinal("numCuentaOrdenante")) : string.Empty;
                    //Compra.DocumentosPorCobrarDetallePagos.NumCuentaBeneficiante = !dr.IsDBNull(dr.GetOrdinal("numCuentaBeneficiante")) ? dr.GetString(dr.GetOrdinal("numCuentaBeneficiante")) : string.Empty;
                    //Compra.DocumentosPorCobrarDetallePagos.FolioIFE = !dr.IsDBNull(dr.GetOrdinal("folioIFE")) ? dr.GetString(dr.GetOrdinal("folioIFE")) : string.Empty;
                    //Compra.DocumentosPorCobrarDetallePagos.ImagenBase64 = !dr.IsDBNull(dr.GetOrdinal("imagen")) ? dr.GetString(dr.GetOrdinal("imagen")) : string.Empty;
                }
                dr.Close();
                return Compra;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CFDI_FormaPagoModels> GetListadoFormaPago(CompraModels Compra)
        {
            CFDI_FormaPagoModels FormaPago;
            SqlDataReader dr = null;
            object[] parametros =
               {
                    Compra.Sucursal.IDSucursal
                };
            dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_COMPRAS_GetListadoFormaPago", parametros);

            while (dr.Read())
            {
                FormaPago = new CFDI_FormaPagoModels
                {
                    Clave = !dr.IsDBNull(dr.GetOrdinal("clave")) ? dr.GetInt16(dr.GetOrdinal("clave")) : 0,
                    Descripcion = !dr.IsDBNull(dr.GetOrdinal("descripcion")) ? dr.GetString(dr.GetOrdinal("descripcion")) : string.Empty,
                    Bancarizado = !dr.IsDBNull(dr.GetOrdinal("bancarizado")) ? dr.GetInt32(dr.GetOrdinal("bancarizado")) : 0,
                };

                Compra.ListaFormasPagos.Add(FormaPago);
            }
            dr.Close();
            return Compra.ListaFormasPagos;
        }
        public List<CatTipoClasificacionModels> GetListadoTipoClasificacion(CompraModels Compra)
        {
            CatTipoClasificacionModels TipoClasificacion;
            SqlDataReader dr = null;
            object[] parametros =
               {
                    Compra.Sucursal.IDSucursal
                };
            dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_COMPRAS_GetListadoTipoClasificacion", parametros);

            while (dr.Read())
            {
                TipoClasificacion = new CatTipoClasificacionModels
                {
                    IDTipoClasificacionGasto = !dr.IsDBNull(dr.GetOrdinal("id_tipoClasificacionGasto")) ? dr.GetInt16(dr.GetOrdinal("id_tipoClasificacionGasto")) : 0,
                    Descripcion = !dr.IsDBNull(dr.GetOrdinal("descripcion")) ? dr.GetString(dr.GetOrdinal("descripcion")) : string.Empty,
                };

                Compra.ListaTipoClasificacion.Add(TipoClasificacion);
            }
            dr.Close();
            return Compra.ListaTipoClasificacion;
        }
        public CompraModels GetGanadoCompra(CompraModels Compra)
        {
            try
            {
                object[] parametros =
                {
                    Compra.IDCompra
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Compras_get_GeneralesCompra", parametros);

                while (dr.Read())
                {
                    Compra.GanadosCompradoMachos = !dr.IsDBNull(dr.GetOrdinal("ganadoCompradoMachos")) ? dr.GetInt32(dr.GetOrdinal("ganadoCompradoMachos")) : 0;
                    Compra.GanadosCompradoHembras = !dr.IsDBNull(dr.GetOrdinal("ganadoCompradoHembras")) ? dr.GetInt32(dr.GetOrdinal("ganadoCompradoHembras")) : 0;
                    Compra.GanadosCompradoTotal = !dr.IsDBNull(dr.GetOrdinal("ganadoCompradoTotal")) ? dr.GetInt32(dr.GetOrdinal("ganadoCompradoTotal")) : 0;
                    Compra.MermaMachos = !dr.IsDBNull(dr.GetOrdinal("mermaMachos")) ? dr.GetDecimal(dr.GetOrdinal("mermaMachos")) : 0;
                    Compra.MermaHembras = !dr.IsDBNull(dr.GetOrdinal("mermaHembras")) ? dr.GetDecimal(dr.GetOrdinal("mermaHembras")) : 0;
                    Compra.MermaTotal = !dr.IsDBNull(dr.GetOrdinal("mermaTotal")) ? dr.GetDecimal(dr.GetOrdinal("mermaTotal")) : 0;
                    Compra.KilosMachos = !dr.IsDBNull(dr.GetOrdinal("kilosMachos")) ? dr.GetDecimal(dr.GetOrdinal("kilosMachos")) : 0;
                    Compra.KilosHembras = !dr.IsDBNull(dr.GetOrdinal("kilosHembras")) ? dr.GetDecimal(dr.GetOrdinal("kilosHembras")) : 0;
                    Compra.KilosTotal = !dr.IsDBNull(dr.GetOrdinal("kilosTotal")) ? dr.GetDecimal(dr.GetOrdinal("kilosTotal")) : 0;
                    Compra.Tolerancia = !dr.IsDBNull(dr.GetOrdinal("tolerancia")) ? dr.GetDecimal(dr.GetOrdinal("tolerancia")) : 0;
                    Compra.MontoTotalGanado = !dr.IsDBNull(dr.GetOrdinal("montoTotalGanado")) ? dr.GetDecimal(dr.GetOrdinal("montoTotalGanado")) : 0;
                    Compra.Proveedor.IDTipoProveedor = !dr.IsDBNull(dr.GetOrdinal("id_tipoProveedor")) ? dr.GetInt16(dr.GetOrdinal("id_tipoProveedor")) : 0;
                }
                dr.Close();
                return Compra;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CatRangoPesoCompraModels> GetListadoPrecioRangoPeso(CompraModels Compra)
        {
            try
            {
                object[] parametros =
                {
                    Compra.Proveedor.IDTipoProveedor
                };
                CatRangoPesoCompraModels RangoPeso;
                List<CatRangoPesoCompraModels> listaRangoPeso = new List<CatRangoPesoCompraModels>();

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Compras_get_ListadoPrecioRangoPesoXIDTipoProveedor", parametros);
                while (dr.Read())
                {
                    RangoPeso = new CatRangoPesoCompraModels
                    {
                        IDRango = !dr.IsDBNull(dr.GetOrdinal("id_rango")) ? dr.GetInt16(dr.GetOrdinal("id_rango")) : 0,
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
        #region Get EventoXIDEvento
        public EventoEnvioModels GetEventoXIDEvento(EventoEnvioModels Evento)
        {
            try
            {
                object[] parametros =
                {
                     Evento.IDEvento,
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Evento.Conexion, "spCSLDB_Compra_get_EventoXIDEvento", parametros);

                while (dr.Read())
                {
                    Evento.IDEvento = !dr.IsDBNull(dr.GetOrdinal("id_eventoCompra")) ? dr.GetInt32(dr.GetOrdinal("id_eventoCompra")) : 0;
                    Evento.IDTipoEvento = !dr.IsDBNull(dr.GetOrdinal("id_tipoEvento")) ? dr.GetInt32(dr.GetOrdinal("id_tipoEvento")) : 0;
                    Evento.Cantidad = !dr.IsDBNull(dr.GetOrdinal("cantidad")) ? dr.GetInt32(dr.GetOrdinal("cantidad")) : 0;
                    Evento.Lugar = !dr.IsDBNull(dr.GetOrdinal("lugar")) ? dr.GetString(dr.GetOrdinal("lugar")) : string.Empty;
                    Evento.FechaDeteccion = !dr.IsDBNull(dr.GetOrdinal("fechaDeteccion")) ? dr.GetDateTime(dr.GetOrdinal("fechaDeteccion")) : DateTime.Today;
                    Evento.HoraDetecccion = !dr.IsDBNull(dr.GetOrdinal("horaDeteccion")) ? dr.GetTimeSpan(dr.GetOrdinal("horaDeteccion")) : DateTime.Now.TimeOfDay;
                    Evento.Observacion = !dr.IsDBNull(dr.GetOrdinal("observacion")) ? dr.GetString(dr.GetOrdinal("observacion")) : string.Empty;
                    Evento.ImagenBase64 = !dr.IsDBNull(dr.GetOrdinal("imagenBase64")) ? dr.GetString(dr.GetOrdinal("imagenBase64")) : string.Empty;
                }
                dr.Close();
                return Evento;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Get Lista Tipos de Eventos
        public List<CatTipoEventoEnvioModels> GetListaTiposEventos(EventoEnvioModels Evento)
        {
            try
            {
                CatTipoEventoEnvioModels TipoEvento;
                List<CatTipoEventoEnvioModels> ListaTiposEventos = new List<CatTipoEventoEnvioModels>();

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Evento.Conexion, "spCSLDB_Combo_get_CatTipoEventoEnvio");
                while (dr.Read())
                {
                    TipoEvento = new CatTipoEventoEnvioModels
                    {
                        IDTipoEventoEnvio = !dr.IsDBNull(dr.GetOrdinal("idTipoEvento")) ? dr.GetInt32(dr.GetOrdinal("idTipoEvento")) : 0,
                        Descripcion = !dr.IsDBNull(dr.GetOrdinal("nombreEvento")) ? dr.GetString(dr.GetOrdinal("nombreEvento")) : string.Empty,
                    };

                    ListaTiposEventos.Add(TipoEvento);
                }
                dr.Close();
                return ListaTiposEventos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        public DocumentosPorCobrarModels GetGeneralesDocumentoPorCobrar(CompraModels Compra)
        {
            try
            {
                DocumentosPorCobrarModels documentosPorCobrar = new DocumentosPorCobrarModels();

                object[] parametros =
                {
                    Compra.IDCompra
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Compras_get_GeneralesDocumentoPorCobrar", parametros);
                while (dr.Read())
                {
                    documentosPorCobrar.Id_documentoCobrar = !dr.IsDBNull(dr.GetOrdinal("id_documentoPorCobrarCompra")) ? dr.GetString(dr.GetOrdinal("id_documentoPorCobrarCompra")) : string.Empty;
                    documentosPorCobrar.Pagos = !dr.IsDBNull(dr.GetOrdinal("pagosCompra")) ? dr.GetDecimal(dr.GetOrdinal("pagosCompra")) : 0;
                    documentosPorCobrar.Impuestos = !dr.IsDBNull(dr.GetOrdinal("impuestoCompra")) ? dr.GetDecimal(dr.GetOrdinal("impuestoCompra")) : 0;
                    documentosPorCobrar.Total = !dr.IsDBNull(dr.GetOrdinal("totalCompra")) ? dr.GetDecimal(dr.GetOrdinal("totalCompra")) : 0;
                    documentosPorCobrar.Cambio = !dr.IsDBNull(dr.GetOrdinal("cambioCompra")) ? dr.GetDecimal(dr.GetOrdinal("cambioCompra")) : 0;
                    documentosPorCobrar.Pendiente = !dr.IsDBNull(dr.GetOrdinal("pendienteCompra")) ? dr.GetDecimal(dr.GetOrdinal("pendienteCompra")) : 0;
                    documentosPorCobrar.Subtotal = !dr.IsDBNull(dr.GetOrdinal("subtotalCompra")) ? dr.GetDecimal(dr.GetOrdinal("subtotalCompra")) : 0;
                }
                dr.Close();
                return documentosPorCobrar;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DocumentoPorPagarModels GetGeneralesDocumentoPorPagar(CompraModels Compra)
        {
            try
            {
                DocumentoPorPagarModels documentosPorPagar = new DocumentoPorPagarModels();

                object[] parametros =
                {
                    Compra.IDCompra
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Compras_get_GeneralesDocumentoPorPagar", parametros);
                while (dr.Read())
                {
                    documentosPorPagar.IDDocumentoPagar = !dr.IsDBNull(dr.GetOrdinal("id_documentoPorPagar")) ? dr.GetString(dr.GetOrdinal("id_documentoPorPagar")) : string.Empty;
                    documentosPorPagar.Impuestos = !dr.IsDBNull(dr.GetOrdinal("impuestos")) ? dr.GetDecimal(dr.GetOrdinal("impuestos")) : 0;
                    documentosPorPagar.subtotal = !dr.IsDBNull(dr.GetOrdinal("subtotal")) ? dr.GetDecimal(dr.GetOrdinal("subtotal")) : 0;
                    documentosPorPagar.Total = !dr.IsDBNull(dr.GetOrdinal("total")) ? dr.GetDecimal(dr.GetOrdinal("total")) : 0;
                    documentosPorPagar.Pagos = !dr.IsDBNull(dr.GetOrdinal("pagos")) ? dr.GetDecimal(dr.GetOrdinal("pagos")) : 0;
                    documentosPorPagar.Pendientes = !dr.IsDBNull(dr.GetOrdinal("pendientes")) ? dr.GetDecimal(dr.GetOrdinal("pendientes")) : 0;
                    documentosPorPagar.Cambio = !dr.IsDBNull(dr.GetOrdinal("cambio")) ? dr.GetDecimal(dr.GetOrdinal("cambio")) : 0;
                }
                dr.Close();
                return documentosPorPagar;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        #region Documento
        public DocumentoModels GetGeneralesDocumentosCompra(DocumentoModels Documento)
        {
            try
            {
                object[] parametros =
                {
                     Documento.Id_servicio, 1
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Documento.Conexion, "spCSLDB_get_DocumentoGeneral", parametros);

                while (dr.Read())
                {
                    Documento.PrecioUnitarioDocumentacion = !dr.IsDBNull(dr.GetOrdinal("total")) ? dr.GetDecimal(dr.GetOrdinal("total")) : 0;
                    Documento.Id_conceptoSalidaDeduccion = !dr.IsDBNull(dr.GetOrdinal("id_conceptoDocumento")) ? dr.GetInt32(dr.GetOrdinal("id_conceptoDocumento")) : 0;
                    Documento.Id_documentoPorPagar = !dr.IsDBNull(dr.GetOrdinal("id_documentoPorPagar")) ? dr.GetString(dr.GetOrdinal("id_documentoPorPagar")) : string.Empty;
                }
                
                dr.Close();

                return Documento;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DocumentoModels GetDocumentoXIDDocumento(DocumentoModels Documento)
        {
            try
            {
                object[] parametros =
                {
                     Documento.IDDocumento, Documento.Id_servicio
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Documento.Conexion, "spCSLDB_Compra_get_DocumentoXIDDocumento", parametros);

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

        #region Details
        public CompraModels GetDetails(CompraModels Compra)
        {
            try
            {
                object[] parametros =
                {
                    Compra.IDCompra
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Compra_get_DetallesGenerales", parametros);

                while (dr.Read())
                {
                    Compra.NombreEmpresa = !dr.IsDBNull(dr.GetOrdinal("razonFiscal")) ? dr.GetString(dr.GetOrdinal("razonFiscal")) : string.Empty;
                    Compra.DireccionEmpresa = !dr.IsDBNull(dr.GetOrdinal("direccionFiscal")) ? dr.GetString(dr.GetOrdinal("direccionFiscal")) : string.Empty;
                    Compra.RFCEmpresa = !dr.IsDBNull(dr.GetOrdinal("RFC")) ? dr.GetString(dr.GetOrdinal("RFC")) : string.Empty;
                    Compra.Proveedor.NombreRazonSocial = !dr.IsDBNull(dr.GetOrdinal("nombreRazonSocial")) ? dr.GetString(dr.GetOrdinal("nombreRazonSocial")) : string.Empty;
                    Compra.UPP = !dr.IsDBNull(dr.GetOrdinal("UPP")) ? dr.GetString(dr.GetOrdinal("UPP")) : string.Empty;
                    Compra.Proveedor.telefonoCasa = !dr.IsDBNull(dr.GetOrdinal("telefonoCasa")) ? dr.GetString(dr.GetOrdinal("telefonoCasa")) : string.Empty;
                    Compra.Proveedor.telefonoCelular = !dr.IsDBNull(dr.GetOrdinal("telefonoCelular")) ? dr.GetString(dr.GetOrdinal("telefonoCelular")) : string.Empty;
                    Compra.Proveedor.RFC = !dr.IsDBNull(dr.GetOrdinal("rfcProveedor")) ? dr.GetString(dr.GetOrdinal("rfcProveedor")) : string.Empty;
                    Compra.FechaHoraProgramada = !dr.IsDBNull(dr.GetOrdinal("fechaHoraProgramada")) ? dr.GetDateTime(dr.GetOrdinal("fechaHoraProgramada")) : DateTime.Now;
                    Compra.GanadosPactadoMachos = !dr.IsDBNull(dr.GetOrdinal("ganadoPactadoMachos")) ? dr.GetInt32(dr.GetOrdinal("ganadoPactadoMachos")) : 0;
                    Compra.GanadosPactadoHembras = !dr.IsDBNull(dr.GetOrdinal("ganadoPactadoHembras")) ? dr.GetInt32(dr.GetOrdinal("ganadoPactadoHembras")) : 0;
                    Compra.GanadosPactadoTotal = !dr.IsDBNull(dr.GetOrdinal("ganadoTotalPactado")) ? dr.GetInt32(dr.GetOrdinal("ganadoTotalPactado")) : 0;
                    Compra.Trayecto.LugarOrigen.descripcion = !dr.IsDBNull(dr.GetOrdinal("lugarOrigen")) ? dr.GetString(dr.GetOrdinal("lugarOrigen")) : string.Empty;
                    Compra.Trayecto.LugarDestino.descripcion = !dr.IsDBNull(dr.GetOrdinal("lugarDestino")) ? dr.GetString(dr.GetOrdinal("lugarDestino")) : string.Empty;
                    Compra.Chofer.Nombre = !dr.IsDBNull(dr.GetOrdinal("nombreChofer")) ? dr.GetString(dr.GetOrdinal("nombreChofer")) : string.Empty;
                    Compra.Flete.kmInicialVehiculo = !dr.IsDBNull(dr.GetOrdinal("kmInicialVehiculo")) ? dr.GetInt32(dr.GetOrdinal("kmInicialVehiculo")) : 0;
                    Compra.Flete.precioFlete = !dr.IsDBNull(dr.GetOrdinal("precioFlete")) ? dr.GetDecimal(dr.GetOrdinal("precioFlete")) : 0;
                    Compra.Vehiculo.nombreTipoVehiculo = !dr.IsDBNull(dr.GetOrdinal("vehiculo")) ? dr.GetString(dr.GetOrdinal("vehiculo")) : string.Empty;
                    Compra.LineaFletera = !dr.IsDBNull(dr.GetOrdinal("lineaFletera")) ? dr.GetString(dr.GetOrdinal("lineaFletera")) : string.Empty;
                    Compra.Tolerancia = !dr.IsDBNull(dr.GetOrdinal("tolerancia")) ? dr.GetDecimal(dr.GetOrdinal("tolerancia")) : 0;
                    Compra.PrecioPorDocumentacion = !dr.IsDBNull(dr.GetOrdinal("subtotal")) ? dr.GetDecimal(dr.GetOrdinal("subtotal")) : 0;
                    Compra.TipoSalidaDocumentacion = !dr.IsDBNull(dr.GetOrdinal("TipoSalidaDocumentacion")) ? dr.GetString(dr.GetOrdinal("TipoSalidaDocumentacion")) : string.Empty;
                    Compra.StringTipoFlete = !dr.IsDBNull(dr.GetOrdinal("tipoFlete")) ? dr.GetString(dr.GetOrdinal("tipoFlete")) : string.Empty;
                }
                dr.Close();
                return Compra;
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
                dr = SqlHelper.ExecuteReader(documento.Conexion, "spCSLDB_Compra_get_DocumentoPorCobrarDetalle", parametros);
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
                    documento.DescripcionDocumento = !dr.IsDBNull(dr.GetOrdinal("descripcion")) ? dr.GetString(dr.GetOrdinal("descripcion")) : string.Empty;
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

        //checar
        public CompraModels GetGanado(CompraModels Compra)
        {
            try
            {
                object[] parametros =
                {
                    Compra.Ganado.id_Ganados
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Compras_get_CompraGanadoXIDGanado", parametros);
                while (dr.Read())
                {
                    Compra.IDSucursal = !dr.IsDBNull(dr.GetOrdinal("id_sucursal")) ? dr.GetString(dr.GetOrdinal("id_sucursal")) : string.Empty;
                    Compra.CompraGanado.IDCompra = !dr.IsDBNull(dr.GetOrdinal("id_compra")) ? dr.GetString(dr.GetOrdinal("id_compra")) : string.Empty;
                    Compra.Ganado.numArete = !dr.IsDBNull(dr.GetOrdinal("numArete")) ? dr.GetString(dr.GetOrdinal("numArete")) : string.Empty;
                    Compra.Ganado.genero = !dr.IsDBNull(dr.GetOrdinal("genero")) ? dr.GetString(dr.GetOrdinal("genero")) : string.Empty;
                    Compra.Ganado.Repeso = !dr.IsDBNull(dr.GetOrdinal("repeso")) ? dr.GetBoolean(dr.GetOrdinal("repeso")) : true;
                    Compra.CompraGanado.Merma = !dr.IsDBNull(dr.GetOrdinal("merma")) ? dr.GetDecimal(dr.GetOrdinal("merma")) : 0;
                    Compra.CompraGanado.PesoFinal = !dr.IsDBNull(dr.GetOrdinal("pesoFinal")) ? dr.GetDecimal(dr.GetOrdinal("pesoFinal")) : 0;
                    Compra.CompraGanado.PesoInicial = !dr.IsDBNull(dr.GetOrdinal("pesoInicial")) ? dr.GetDecimal(dr.GetOrdinal("pesoInicial")) : 0;
                    Compra.CompraGanado.PesoPagado = !dr.IsDBNull(dr.GetOrdinal("PesoPagado")) ? dr.GetDecimal(dr.GetOrdinal("PesoPagado")) : 0;
                    Compra.CompraGanado.PrecioKilo = !dr.IsDBNull(dr.GetOrdinal("precioKilo")) ? dr.GetDecimal(dr.GetOrdinal("precioKilo")) : 0;
                    Compra.CompraGanado.TotalPagado = !dr.IsDBNull(dr.GetOrdinal("totalPagado")) ? dr.GetDecimal(dr.GetOrdinal("totalPagado")) : 0;
                    Compra.CompraGanado.DiferenciaPeso = !dr.IsDBNull(dr.GetOrdinal("DiferenciaPeso")) ? dr.GetDecimal(dr.GetOrdinal("DiferenciaPeso")) : 0;
                    Compra.EstatusGanado.id_estatusGanado = !dr.IsDBNull(dr.GetOrdinal("id_estatusGanado")) ? dr.GetInt16(dr.GetOrdinal("id_estatusGanado")) : 0;
                    Compra.EstatusGanado.descripcion = !dr.IsDBNull(dr.GetOrdinal("estatusGanado")) ? dr.GetString(dr.GetOrdinal("estatusGanado")) : string.Empty;
                    Compra.Ganado.observacion = !dr.IsDBNull(dr.GetOrdinal("observacion")) ? dr.GetString(dr.GetOrdinal("observacion")) : string.Empty;
                    Compra.Sucursal.MermaPredeterminada = !dr.IsDBNull(dr.GetOrdinal("mermaPredeterminada")) ? dr.GetDecimal(dr.GetOrdinal("mermaPredeterminada")) : 0;
                }
                dr.Close();
                return Compra;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public SqlDataReader GetSqlDataReaderListadoPrecioRangoPeso(CompraModels Compra)
        {
            SqlDataReader dr = null;
            object[] parametros =
               {
                    Compra.IDProveedor
                };
            dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Compras_get_ListadoPrecioRangoPesoXIDProveedor", parametros);
            dr.Close();
            return dr;
        }
        public string GetGanadoXGanadoDetalle(CompraModels Compra)
        {
            try
            {
                object[] parametros =
                {
                    Compra.IDCompra
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Compras_get_GanadoXGanadoDetalleXIDCompra", parametros);
                dr.Close();
                return Auxiliar.SqlReaderToJson(dr);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        #region AC DEL
        #region Proveedor
        public CompraModels Compras_ac_Proveedor(CompraModels Compra)
        {
            try
            {
                object[] parametros =
                {
                     Compra.IDCompra  = string.IsNullOrEmpty(Compra.IDCompra) ? null : Compra.IDCompra
                    ,Compra.IDSucursal = string.IsNullOrEmpty(Compra.IDSucursal) ? null : Compra.IDSucursal
                    ,Compra.IDProveedor = string.IsNullOrEmpty(Compra.IDProveedor) ? null : Compra.IDProveedor
                    ,Compra.FechaHoraProgramada
                    ,Compra.Usuario = string.IsNullOrEmpty(Compra.Usuario) ? null : Compra.Usuario
                    ,Compra.IDPLugarProveedor = string.IsNullOrEmpty(Compra.IDPLugarProveedor) ? null : Compra.IDPLugarProveedor
                    ,Compra.Descripcion_bascula
                    ,Compra.Observacion_programacion
                    ,Compra.GanadosPactadoMachos
                    ,Compra.GanadosPactadoHembras
                    ,Compra.HoraProgramada
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Compras_ac_Proveedor", parametros);

                while (dr.Read())
                {
                    Compra.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                    Compra.Completado = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : true;
                }
                dr.Close();
                return Compra;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Flete
        public CompraModels Compras_ac_Flete(CompraModels Compra)
        {
            try
            {
                object[] parametros =
                {
                     Compra.IDCompra = string.IsNullOrEmpty(Compra.IDCompra) ? null : Compra.IDCompra
                    ,Compra.IDFlete= string.IsNullOrEmpty(Compra.IDFlete) ? null : Compra.IDFlete
                    ,Compra.IDEmpresa= string.IsNullOrEmpty(Compra.IDEmpresa) ? null : Compra.IDEmpresa
                    ,Compra.IDChofer= string.IsNullOrEmpty(Compra.IDChofer) ? null : Compra.IDChofer
                    ,Compra.IDVehiculo= string.IsNullOrEmpty(Compra.IDVehiculo) ? null : Compra.IDVehiculo
                    ,Compra.Flete.kmInicialVehiculo
                    ,Compra.Usuario= string.IsNullOrEmpty(Compra.Usuario) ? null : Compra.Usuario
                    ,Compra.Trayecto.id_lugarOrigen = string.IsNullOrEmpty(Compra.Trayecto.id_lugarOrigen) ? null : Compra.Trayecto.id_lugarOrigen
                    ,Compra.Trayecto.id_lugarDestino = string.IsNullOrEmpty(Compra.Trayecto.id_lugarDestino) ? null : Compra.Trayecto.id_lugarDestino
                    ,Compra.IDSucursal
                    ,Compra.DocumentosPorCobrarDetallePagos.Monto
                    //,Compra.Flete.NumFleje
                    ,Compra.TipoFlete
                    //,Compra.Flete.Id_metodoPago
                    //,Compra.DocumentosPorCobrarDetallePagos.Id_formaPago
                    //,Compra.Flete.CondicionPago
                    //,Compra.DocumentosPorCobrarDetallePagos.fecha = (Compra.DocumentosPorCobrarDetallePagos.fecha == null) ? DateTime.Now : Compra.DocumentosPorCobrarDetallePagos.fecha
                    //,Compra.DocumentosPorCobrarDetallePagos.Observacion
                    //,Compra.DocumentosPorCobrarDetallePagos.Id_cuentaBancariaBeneficiante
                    //,Compra.DocumentosPorCobrarDetallePagos.NombreBancoBeneficiante
                    //,Compra.DocumentosPorCobrarDetallePagos.NumCuentaBeneficiante
                    //,Compra.DocumentosPorCobrarDetallePagos.Id_cuentaBancariaOrdenante
                    //,Compra.DocumentosPorCobrarDetallePagos.NombreBancoOrdenante
                    //,Compra.DocumentosPorCobrarDetallePagos.NumCuentaOrdenante
                    //,Compra.DocumentosPorCobrarDetallePagos.ImagenBase64
                    //,Compra.DocumentosPorCobrarDetallePagos.FolioIFE
                    //,Compra.DocumentosPorCobrarDetallePagos.NumeroAutorizacion

                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Compras_ac_Flete", parametros);

                while (dr.Read())
                {
                    Compra.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : true;
                    Compra.RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                }
                dr.Close();
                return Compra;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Ganado
        public CompraModels Compras_ac_Ganado(CompraModels Compra, int indiceActual)
        {
            try
            {
                object[] parametros =
                {
                    Compra.IDCompra,
                    Compra.Ganado.id_Ganados,
                    Compra.Ganado.numArete,
                    Compra.Ganado.genero,
                    Compra.Ganado.IDEstatusGanado,
                    Compra.Ganado.CompraGanado.PesoInicial,
                    Compra.Ganado.CompraGanado.PesoFinal,
                    Compra.Ganado.CompraGanado.Merma,
                    Compra.Ganado.CompraGanado.PesoPagado,
                    Compra.Ganado.CompraGanado.PrecioKilo,
                    Compra.Ganado.IDCorral,
                    Compra.Usuario,
                    Compra.Ganado.CompraGanado.Id_detalleDocumentoPorCobrar,
                    indiceActual,
                    Compra.Ganado.IDFierro1,
                    Compra.Ganado.IDFierro2,
                    Compra.Ganado.IDFierro3
                };

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Compra_ac_Ganado", parametros);

                while (dr.Read())
                {
                    Compra.RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;

                    Compra.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : true;
                }
                dr.Close();
                return Compra;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public CompraModels Compras_del_Ganado(CompraModels Compra)
        {
            try
            {
                object[] parametros =
                {
                    Compra.IDCompra,
                    Compra.Ganado.id_Ganados,
                    Compra.Usuario,
                    Compra.Ganado.CompraGanado.Id_detalleDocumentoPorCobrar
                };

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Compra_del_Ganado", parametros);

                while (dr.Read())
                {
                    Compra.RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                    Compra.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : true;
                }
                dr.Close();
                return Compra;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public CompraModels Compras_del_GanadoProgramado2(CompraModels Compra)
        {
            try
            {
                object[] parametros =
                {
                    Compra.IDCompra,
                    Compra.Id_ganadoProgramado,
                    Compra.Usuario
                };

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Compra_del_GanadoProgramado2", parametros);

                while (dr.Read())
                {
                    Compra.RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                    Compra.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : true;
                }
                dr.Close();
                return Compra;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Evento
        public EventoEnvioModels AC_Evento(EventoEnvioModels Evento)
        {
            try
            {
                object[] parametros =
                {
                   Evento.IDEvento      ,Evento.IDTipoEvento            ,Evento.Cantidad
                  ,Evento.Lugar         ,Evento.FechaDeteccion          ,Evento.HoraDetecccion      ,Evento.Observacion
                  ,Evento.ImagenBase64  ,Evento.ListaProductosEvento    ,Evento.Usuario             ,Evento.IDCompra
                };

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Evento.Conexion, "spCSLDB_Compras_ac_Evento", parametros);

                while (dr.Read())
                {
                    Evento.RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                    Evento.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : true;
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                Evento.RespuestaAjax.Mensaje = ex.ToString();
                Evento.RespuestaAjax.Success = false;
            }

            return Evento;
        }
        public EventoEnvioModels DEL_Evento(EventoEnvioModels Evento)
        {
            try
            {
                object[] parametros =
                {
                    Evento.IDEvento,
                    Evento.Usuario
                };

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Evento.Conexion, "spCSLDB_Compras_del_Evento", parametros);

                while (dr.Read())
                {
                    Evento.RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                    Evento.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : true;
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                Evento.RespuestaAjax.Mensaje = ex.ToString();
                Evento.RespuestaAjax.Success = false;
            }

            return Evento;
        }
        #endregion
        #region Recepcion Origen
        public CompraModels AC_RecepcionOrigen(CompraModels Compra)
        {
            try
            {
                object[] parametros =
                {
                    Compra.IDCompra,                             Compra.RecepcionOrigen.FechaLlegada,
                    Compra.RecepcionOrigen.HoraLlegada,          Compra.RecepcionOrigen.KilometrajeFinal,
                    Compra.RecepcionOrigen.Observacion,          Compra.IDRecepcion,
                    Compra.Usuario
                };

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Compra_ac_Recepcion", parametros);

                while (dr.Read())
                {
                    Compra.RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                    Compra.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : true;
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                Compra.RespuestaAjax.Mensaje = ex.ToString();
                Compra.RespuestaAjax.Success = false;
            }

            return Compra;
        }
        #endregion
        #region Documentos
        public DocumentoModels AC_CostoDocumentos(DocumentoModels Documento)
        {
            try
            {
                object[] parametros =
                {
                     Documento.Id_servicio
                    ,Documento.Id_documentoPorPagar
                    ,Documento.Usuario
                    ,Documento.PrecioUnitarioDocumentacion
                    ,Documento.Id_conceptoSalidaDeduccion
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Documento.Conexion, "spCSLDB_Compras_ac_Documento", parametros);

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
                dr = SqlHelper.ExecuteReader(Documento.Conexion, "spCSLDB_Compra_ac_Documento", parametros);

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
                dr = SqlHelper.ExecuteReader(Documento.Conexion, "spCSLDB_Compra_del_DocumentoXIDDocumento", parametros);

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
        #region Ganado programado
        public CompraModels Compras_ac_GanadoProgramado(CompraModels Compra, int indiceActual)
        {
            try
            {
                object[] parametros =
                {
                    Compra.IDCompra,
                    Compra.Ganado.id_Ganados,
                    Compra.Ganado.numArete,
                    Compra.Ganado.genero,
                    Compra.Ganado.IDFierro1,
                    Compra.Ganado.IDFierro2,
                    Compra.Ganado.IDFierro3,
                    indiceActual,
                    Compra.Usuario,
                };

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Compra_ac_GanadoProgramado", parametros);

                while (dr.Read())
                {
                    Compra.RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                    Compra.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : true;
                }
                dr.Close();
                return Compra;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public CompraModels Compras_del_GanadoProgramado(CompraModels Compra)
        {
            try
            {
                object[] parametros =
                {
                    Compra.IDCompra,
                    Compra.Ganado.id_Ganados,
                    Compra.Usuario
                };

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Compra_del_GanadoProgramado", parametros);

                while (dr.Read())
                {
                    Compra.RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                    Compra.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : true;
                }
                dr.Close();
                return Compra;
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
                dr = SqlHelper.ExecuteReader(DocumentosPorCobrarModels.Conexion, "spCSLDB_Compra_AC_ServicioProducto_DocumentoPorCobrarDetalle", parametros);
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
                dr = SqlHelper.ExecuteReader(DocumentosPorCobrarModels.Conexion, "spCSLDB_Compra_DEL_ProductoServicio", parametros);
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
        #region AC_Comprobante Cobro
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
                dr = SqlHelper.ExecuteReader(DocumentosPorCobrarModels.Conexion, "spCSLDB_Compra_AC_DetallesCobro", parametros);
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
        #region DEL documento por cobrar impuesto
        public CompraImpuestoModels Compra_del_ImpuestoDocPorCobrar(CompraImpuestoModels CompraImpuesto)
        {
            try
            {
                object[] parametros =
                {
                    CompraImpuesto.IDCompra
                    ,CompraImpuesto.IDImpuesto
                    ,CompraImpuesto.Usuario
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(CompraImpuesto.Conexion, "spCSLDB_Compra_del_DocumentoPorCobrarImpuesto", parametros);
                while (dr.Read())
                {
                    CompraImpuesto.RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                    CompraImpuesto.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : true;
                }
                dr.Close();
                return CompraImpuesto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region AC_Comprobante Pago
        public DocumentoPorPagarDetallePagosModels AC_ComprobantePago(DocumentoPorPagarDetallePagosModels DocumentosPorPagarModels)
        {
            try
            {
                object[] parametros =
                {
                    DocumentosPorPagarModels.Id_documentoPorPagarDetallePagos,    DocumentosPorPagarModels.Id_padre,
                    DocumentosPorPagarModels.Id_formaPago,                         DocumentosPorPagarModels.Monto,
                    DocumentosPorPagarModels.Observacion,                          DocumentosPorPagarModels.fecha,
                    DocumentosPorPagarModels.Id_cuentaBancariaOrdenante,           DocumentosPorPagarModels.Id_documentoPorPagarDetallePagosBancarizado,
                    DocumentosPorPagarModels.Id_cuentaBancariaBeneficiante,        DocumentosPorPagarModels.NombreBancoOrdenante,
                    DocumentosPorPagarModels.NumeroAutorizacion,                   DocumentosPorPagarModels.NumCuentaOrdenante,
                    DocumentosPorPagarModels.NombreBancoBeneficiante,              DocumentosPorPagarModels.NumCuentaBeneficiante,
                    DocumentosPorPagarModels.FolioIFE,                             DocumentosPorPagarModels.Usuario,
                    DocumentosPorPagarModels.Bancarizado,                          DocumentosPorPagarModels.RfcEmisorOrdenante,
                    DocumentosPorPagarModels.RfcEmisorBeneficiario,                DocumentosPorPagarModels.ImagenBase64
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(DocumentosPorPagarModels.Conexion, "spCSLDB_Compra_AC_DetallesPago", parametros);
                while (dr.Read())
                {
                    DocumentosPorPagarModels.RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                    DocumentosPorPagarModels.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : false;
                }
                dr.Close();
                return DocumentosPorPagarModels;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #endregion
        
        #region Imagenes
        public CompraModels DeleteImageFierro(CompraModels Compra)
        {
            try
            {
                object[] parametros =
                {
                    Compra.Fierro.IDFierro,
                    Compra.IDUsuario
                };

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Compras_DeleteImageFierro", parametros);
                while (dr.Read())
                {
                    Compra.Mensaje = !dr.IsDBNull(dr.GetOrdinal("Mensaje")) ? dr.GetString(dr.GetOrdinal("Mensaje")) : string.Empty;
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Compra;
        }
        public CompraModels SaveImageFierro(CompraModels Compra)
        {
            try
            {
                object[] parametros =
                {
                    Compra.IDCompra,
                    Compra.Fierro.ImgFierro,
                    Compra.Fierro.NombreFierro,
                    Compra.IDUsuario
                };

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Compras_SaveImageFierro", parametros);
                while (dr.Read())
                {
                    Compra.Fierro.IDFierro = !dr.IsDBNull(dr.GetOrdinal("id_fierro")) ? dr.GetString(dr.GetOrdinal("id_fierro")) : string.Empty;
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Compra;
        }
        public List<CatFierroModels> GetListadoFierros(CompraModels Compra)
        {
            CatFierroModels Fierro;
            List<CatFierroModels> ListaFierros = new List<CatFierroModels>();
            SqlDataReader dr = null;

            dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Combo_get_CatFierro");

            while (dr.Read())
            {
                Fierro = new CatFierroModels
                {
                    IDFierro = !dr.IsDBNull(dr.GetOrdinal("ID")) ? dr.GetString(dr.GetOrdinal("ID")) : string.Empty,
                    NombreFierro = !dr.IsDBNull(dr.GetOrdinal("Nombre")) ? dr.GetString(dr.GetOrdinal("Nombre")) : string.Empty,
                };
                ListaFierros.Add(Fierro);
            }
            dr.Close();
            return ListaFierros;

        }
        #endregion

        public string GetRangoPeso(CompraModels Compra)
        {
            object[] parametros =
                {
                    Compra.IDCompra
                };
            try
            {
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_COMPRAS_GetListadoPrecioRangoPeso", parametros);
                dr.Close();
                return Auxiliar.SqlReaderToJson(dr);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public RespuestaAjax GetFierroXIDFierro(CompraModels Compra)
        {

            try
            {
                object[] parametros =
                {
                    Compra.Fierro.IDFierro
                };
                RespuestaAjax RespuestaAjax = new RespuestaAjax();
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_get_FierroXIDFierro", parametros);
                while (dr.Read())
                {
                    RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : false;
                    RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                }
                dr.Close();
                return RespuestaAjax;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public CompraModels GetImagenesProveedor(CompraModels Compra)
        {
            try
            {
                object[] parametros =
                {
                    Compra.IDCompra
                };
                RespuestaAjax RespuestaAjax = new RespuestaAjax();
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Compra_get_ImagenesProveedor", parametros);
                while (dr.Read())
                {

                    Compra.Proveedor.NombreRazonSocial = !dr.IsDBNull(dr.GetOrdinal("nombreRazonSocial")) ? dr.GetString(dr.GetOrdinal("nombreRazonSocial")) : string.Empty;
                    Compra.Proveedor.TipoProveedor = !dr.IsDBNull(dr.GetOrdinal("TipoProveedor")) ? dr.GetString(dr.GetOrdinal("TipoProveedor")) : string.Empty;
                    Compra.Proveedor.RFC = !dr.IsDBNull(dr.GetOrdinal("rfc")) ? dr.GetString(dr.GetOrdinal("rfc")) : string.Empty;
                    Compra.Proveedor.ImgINE = !dr.IsDBNull(dr.GetOrdinal("imgINE")) ? dr.GetString(dr.GetOrdinal("imgINE")) : string.Empty;
                    Compra.Proveedor.ImgManifestacionFierro = !dr.IsDBNull(dr.GetOrdinal("imgManifestacionFierro")) ? dr.GetString(dr.GetOrdinal("imgManifestacionFierro")) : string.Empty;
                    Compra.UPP = !dr.IsDBNull(dr.GetOrdinal("imagenUPP")) ? dr.GetString(dr.GetOrdinal("imagenUPP")) : string.Empty;
                }
                dr.Close();
                return Compra;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        #region Reporte Ganado
        public List<ReporteGanadoModels> GetReporteGanadoDetalles(CompraModels Compra)
        {
            try
            {
                List<ReporteGanadoModels> Lista = new List<ReporteGanadoModels>();
                ReporteGanadoModels Item;
                object[] parametros = { Compra.IDCompra };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Compra_get_ReporteListaGanadoProgramado", parametros);

                while (dr.Read())
                {
                    Item = new ReporteGanadoModels();
                    Item.NumArete = !dr.IsDBNull(dr.GetOrdinal("numArete")) ? dr.GetString(dr.GetOrdinal("numArete")) : string.Empty;
                    Item.Genero = !dr.IsDBNull(dr.GetOrdinal("genero")) ? dr.GetString(dr.GetOrdinal("genero")) : string.Empty;
                    Item.ImagenBase64_1 = !dr.IsDBNull(dr.GetOrdinal("imgFierro1")) ? dr.GetString(dr.GetOrdinal("imgFierro1")) : Auxiliar.SinImagen();
                    Item.ImagenBase64_2 = !dr.IsDBNull(dr.GetOrdinal("imgFierro2")) ? dr.GetString(dr.GetOrdinal("imgFierro2")) : Auxiliar.SinImagen();
                    Item.ImagenBase64_3 = !dr.IsDBNull(dr.GetOrdinal("imgFierro3")) ? dr.GetString(dr.GetOrdinal("imgFierro3")) : Auxiliar.SinImagen();

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
                dr = SqlHelper.ExecuteReader(DocumentoPago.Conexion, "spCSLDB_Compra_get_GetDetalleDocumentoPago", parametros);
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

                dr = SqlHelper.ExecuteReader(DocumentoPorCobrarDetallePagos.Conexion, "spCSLDB_Compra_get_NombreProveedor_Empresa", parametros);
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
                dr = SqlHelper.ExecuteReader(DocumentoPorCobrarDetallePagos.Conexion, "spCSLDB_Compra_get_CuentasBancarias", parametros);
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
        #endregion

        #region Vista pago
        public DocumentoPorPagarDetallePagosModels GetDetalleDocumentoXPagar(DocumentoPorPagarDetallePagosModels DocumentoPago)
        {
            try
            {
                object[] parametros =
                {
                    DocumentoPago.Id_padre,
                    DocumentoPago.Id_documentoPorPagarDetallePagos
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(DocumentoPago.Conexion, "spCSLDB_Compra_get_GetDetalleDocumentoXPagar", parametros);
                while (dr.Read())
                {
                    DocumentoPago.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : false;
                    if (DocumentoPago.RespuestaAjax.Success)
                    {
                        DocumentoPago.Id_documentoPorPagarDetallePagos = !dr.IsDBNull(dr.GetOrdinal("id_documentoPorPagarDetallePagos")) ? dr.GetString(dr.GetOrdinal("id_documentoPorPagarDetallePagos")) : string.Empty;
                        DocumentoPago.Monto = !dr.IsDBNull(dr.GetOrdinal("monto")) ? dr.GetDecimal(dr.GetOrdinal("monto")) : 0;
                        DocumentoPago.Id_documentoPorPagarDetallePagosBancarizado = !dr.IsDBNull(dr.GetOrdinal("id_documentoPorPagarDetallePagosBancarizado")) ? dr.GetString(dr.GetOrdinal("id_documentoPorPagarDetallePagosBancarizado")) : string.Empty;
                        DocumentoPago.NombreBancoOrdenante = !dr.IsDBNull(dr.GetOrdinal("nombreBancoOrdenante")) ? dr.GetString(dr.GetOrdinal("nombreBancoOrdenante")) : string.Empty;
                        DocumentoPago.NumeroAutorizacion = !dr.IsDBNull(dr.GetOrdinal("numeroAutorizacion")) ? dr.GetString(dr.GetOrdinal("numeroAutorizacion")) : string.Empty;
                        DocumentoPago.NombreBancoBeneficiante = !dr.IsDBNull(dr.GetOrdinal("nombreBancoBeneficiante")) ? dr.GetString(dr.GetOrdinal("nombreBancoBeneficiante")) : string.Empty;
                        DocumentoPago.TipoCadenaPago = !dr.IsDBNull(dr.GetOrdinal("tipoCadenaPago")) ? dr.GetString(dr.GetOrdinal("tipoCadenaPago")) : string.Empty;
                        DocumentoPago.Id_documentoPorPagar = !dr.IsDBNull(dr.GetOrdinal("id_documentoPorPagar")) ? dr.GetString(dr.GetOrdinal("id_documentoPorPagar")) : string.Empty;
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
        public DocumentoPorPagarDetallePagosModels GetNombreEmpresaCliente_PorPagar(DocumentoPorPagarDetallePagosModels DocumentoPorPagarDetallePagos)
        {
            try
            {
                object[] parametros =
                {
                    DocumentoPorPagarDetallePagos.Id_padre
                };
                SqlDataReader dr = null;
                string nombre = string.Empty;

                dr = SqlHelper.ExecuteReader(DocumentoPorPagarDetallePagos.Conexion, "spCSLDB_Compra_get_NombreProveedor_Empresa", parametros);
                while (dr.Read())
                {
                    DocumentoPorPagarDetallePagos.NombreEmpresa = !dr.IsDBNull(dr.GetOrdinal("nombreEmpresa")) ? dr.GetString(dr.GetOrdinal("nombreEmpresa")) : string.Empty;
                    DocumentoPorPagarDetallePagos.NombreProveedor_Cliente = !dr.IsDBNull(dr.GetOrdinal("nombreProveedor")) ? dr.GetString(dr.GetOrdinal("nombreProveedor")) : string.Empty;
                }
                dr.Close();
                return DocumentoPorPagarDetallePagos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CuentaBancariaModels> GetListadoCuentasBancarias_Pagos(DocumentoPorPagarDetallePagosModels DocumentoPorPagarDetallePagos)
        {
            try
            {
                object[] parametros =
                {
                    DocumentoPorPagarDetallePagos.TipoCuentaBancaria,
                    DocumentoPorPagarDetallePagos.Id_padre
                };
                CuentaBancariaModels item;
                List<CuentaBancariaModels> lista = new List<CuentaBancariaModels>();
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(DocumentoPorPagarDetallePagos.Conexion, "spCSLDB_Compra_get_CuentasBancarias", parametros);
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
        #region DEL_ComprobantePago
        public DocumentoPorPagarDetallePagosModels DEL_ComprobantePago(DocumentoPorPagarDetallePagosModels DocumentosPorPagar)
        {
            try
            {
                object[] parametros =
                {
                    DocumentosPorPagar.Id_documentoPorPagarDetallePagos,    DocumentosPorPagar.Id_documentoPorPagar,
                    DocumentosPorPagar.Usuario
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(DocumentosPorPagar.Conexion, "spCSLDB_DocumentoPorPagar_DEL_DetallesPago", parametros);
                while (dr.Read())
                {
                    DocumentosPorPagar.RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                    DocumentosPorPagar.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : false;
                }
                dr.Close();
                return DocumentosPorPagar;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #endregion 

        #region Vista Impuestos documento por cobrar

        public CompraImpuestoModels Get_ImpuestosProductoServicioCompra(CompraImpuestoModels CompraImpuesto)
        {
            try
            {
                object[] parametros =
                {
                     CompraImpuesto.IDCompra,
                     CompraImpuesto.Id_detalleDoctoCobrar
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(CompraImpuesto.Conexion, "spCSLDB_Compra_get_GeneralesImpuesto", parametros);

                while (dr.Read())
                {
                    CompraImpuesto.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : true;
                    if (CompraImpuesto.RespuestaAjax.Success)
                    {
                        CompraImpuesto.PrecioProducto = !dr.IsDBNull(dr.GetOrdinal("precioUnitario")) ? dr.GetDecimal(dr.GetOrdinal("precioUnitario")) : 0;
                        CompraImpuesto.TotalImpuestoRetencion = !dr.IsDBNull(dr.GetOrdinal("impuestos_retenidos")) ? dr.GetDecimal(dr.GetOrdinal("impuestos_retenidos")) : 0;
                        CompraImpuesto.TotalImpuestoTrasladado = !dr.IsDBNull(dr.GetOrdinal("impuestos_trasladados")) ? dr.GetDecimal(dr.GetOrdinal("impuestos_trasladados")) : 0;
                        CompraImpuesto.TotalImpuestos = !dr.IsDBNull(dr.GetOrdinal("impuestos")) ? dr.GetDecimal(dr.GetOrdinal("impuestos")) : 0;
                        CompraImpuesto.SubTotal = !dr.IsDBNull(dr.GetOrdinal("subtotal")) ? dr.GetDecimal(dr.GetOrdinal("subtotal")) : 0;
                        CompraImpuesto.Total = !dr.IsDBNull(dr.GetOrdinal("total")) ? dr.GetDecimal(dr.GetOrdinal("total")) : 0;
                    }
                    else
                    {
                        CompraImpuesto.RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                    }
                }
                dr.Close();
                return CompraImpuesto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public CompraImpuestoModels GetImpuestoXIDImpuesto(CompraImpuestoModels CompraImpuesto)
        {
            try
            {
                SqlDataReader dr = null;

                object[] parametros =
                {
                    CompraImpuesto.IDImpuesto
                };
                dr = SqlHelper.ExecuteReader(CompraImpuesto.Conexion, "spCSLDB_Compra_get_ImpuestoXIDImpuesto", parametros);
                while (dr.Read())
                {
                    CompraImpuesto.Impuesto.Clave = !dr.IsDBNull(dr.GetOrdinal("id_impuesto")) ? dr.GetInt16(dr.GetOrdinal("id_impuesto")) : 0;
                    CompraImpuesto.TipoFactor.Clave = !dr.IsDBNull(dr.GetOrdinal("id_tipoFactor")) ? dr.GetInt16(dr.GetOrdinal("id_tipoFactor")) : 0;
                    CompraImpuesto.TipoImpuesto.Clave = !dr.IsDBNull(dr.GetOrdinal("id_tipoImpuesto")) ? dr.GetInt16(dr.GetOrdinal("id_tipoImpuesto")) : 0;
                    CompraImpuesto.Base = !dr.IsDBNull(dr.GetOrdinal("base")) ? dr.GetDecimal(dr.GetOrdinal("base")) : 0;
                    CompraImpuesto.TasaCuota = !dr.IsDBNull(dr.GetOrdinal("tasaCuota")) ? dr.GetDecimal(dr.GetOrdinal("tasaCuota")) : 0;
                    CompraImpuesto.Importe = !dr.IsDBNull(dr.GetOrdinal("importe")) ? dr.GetDecimal(dr.GetOrdinal("importe")) : 0;
                    CompraImpuesto.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : false;
                }
                dr.Close();
                return CompraImpuesto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CFDI_ImpuestoModels> GetListadoImpuesto(CompraImpuestoModels CompraImpuesto)
        {
            try
            {
                CFDI_ImpuestoModels Impuesto;
                List<CFDI_ImpuestoModels> ListaImpuesto = new List<CFDI_ImpuestoModels>();

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(CompraImpuesto.Conexion, "spCSLDB_Combo_get_CFDIImpuesto");
                while (dr.Read())
                {
                    Impuesto = new CFDI_ImpuestoModels
                    {
                        Clave = !dr.IsDBNull(dr.GetOrdinal("ID")) ? dr.GetInt16(dr.GetOrdinal("ID")) : 0,
                        Descripcion = !dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? dr.GetString(dr.GetOrdinal("Descripcion")) : string.Empty,
                    };

                    ListaImpuesto.Add(Impuesto);
                }
                dr.Close();
                return ListaImpuesto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CFDI_TipoImpuestoModels> GetListadoTipoImpuesto(CompraImpuestoModels CompraImpuesto)
        {
            try
            {
                CFDI_TipoImpuestoModels TipoImpuesto;
                List<CFDI_TipoImpuestoModels> ListaTipoImpuesto = new List<CFDI_TipoImpuestoModels>();

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(CompraImpuesto.Conexion, "spCSLDB_Combo_get_CFDITipoImpuesto");
                while (dr.Read())
                {
                    TipoImpuesto = new CFDI_TipoImpuestoModels
                    {
                        Clave = !dr.IsDBNull(dr.GetOrdinal("ID")) ? dr.GetInt16(dr.GetOrdinal("ID")) : 0,
                        Descripcion = !dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? dr.GetString(dr.GetOrdinal("Descripcion")) : string.Empty,
                    };

                    ListaTipoImpuesto.Add(TipoImpuesto);
                }
                dr.Close();
                return ListaTipoImpuesto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CFDI_TipoFactorModels> GetListadoTipoFactor(CompraImpuestoModels CompraImpuesto)
        {

            try
            {
                CFDI_TipoFactorModels TipoFactor;
                List<CFDI_TipoFactorModels> ListaTipoFactor = new List<CFDI_TipoFactorModels>();

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(CompraImpuesto.Conexion, "spCSLDB_Combo_get_CFDITipoFactor");
                while (dr.Read())
                {
                    TipoFactor = new CFDI_TipoFactorModels
                    {
                        Clave = !dr.IsDBNull(dr.GetOrdinal("ID")) ? dr.GetInt16(dr.GetOrdinal("ID")) : 0,
                        Descripcion = !dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? dr.GetString(dr.GetOrdinal("Descripcion")) : string.Empty,
                    };

                    ListaTipoFactor.Add(TipoFactor);
                }
                dr.Close();
                return ListaTipoFactor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public CompraImpuestoModels Compra_ac_ImpuestoProductoServicio(CompraImpuestoModels CompraImpuesto)
        {
            try
            {
                object[] parametros =
                {
                    CompraImpuesto.IDCompra,
                    CompraImpuesto.IDImpuesto,//@id_documentoCobrarDetalleImpuesto char(36)
                    CompraImpuesto.Id_detalleDoctoCobrar,//,@id_detalleDoctoCobrar char(36)
                    CompraImpuesto.TipoImpuesto.Clave,//,@id_tipoImpuesto smallint
                    CompraImpuesto.Impuesto.Clave,//,@id_impuesto smallint
                    CompraImpuesto.TipoFactor.Clave,//,@id_tipoFactor smallint
                    CompraImpuesto.Base,//,@base decimal(18,3)
                    CompraImpuesto.TasaCuota,//,@tasaCuota decimal(18,6)
                    CompraImpuesto.Importe,//,@importe money
                    CompraImpuesto.Usuario//,@id_usuario char(36)
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(CompraImpuesto.Conexion, "spCSLDB_Compra_ac_ImpuestoProductoServicio", parametros);
                while (dr.Read())
                {
                    CompraImpuesto.RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                    CompraImpuesto.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : true;
                }
                dr.Close();
                return CompraImpuesto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Comprobante compra
        public ComprobanteCompraCabeceraModels GetComprobanteCompraCabecera(CompraModels Compra)
        {
            try
            {
                ComprobanteCompraCabeceraModels Item = new ComprobanteCompraCabeceraModels();
                object[] parametros = { Compra.IDCompra };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Compra_get_ComprobanteCompraDetallesCabecera", parametros);

                while (dr.Read())
                {
                    Item.UrlLogo = !dr.IsDBNull(dr.GetOrdinal("logoEmpresa")) ? dr.GetString(dr.GetOrdinal("logoEmpresa")) : string.Empty;
                    Item.NombreEmpresa = !dr.IsDBNull(dr.GetOrdinal("nombreEmpresa")) ? dr.GetString(dr.GetOrdinal("nombreEmpresa")) : string.Empty;
                    Item.RubroEmpresa = !dr.IsDBNull(dr.GetOrdinal("rubroEmpresa")) ? dr.GetString(dr.GetOrdinal("rubroEmpresa")) : string.Empty;
                    Item.TelefonoEmpresa = !dr.IsDBNull(dr.GetOrdinal("telefonoEmpresa")) ? dr.GetString(dr.GetOrdinal("telefonoEmpresa")) : string.Empty;
                    Item.DireccionEmpresa = !dr.IsDBNull(dr.GetOrdinal("direccionEmpresa")) ? dr.GetString(dr.GetOrdinal("direccionEmpresa")) : string.Empty;
                    Item.Folio = !dr.IsDBNull(dr.GetOrdinal("folio")) ? dr.GetString(dr.GetOrdinal("folio")) : string.Empty;
                    Item.NombreProveedor = !dr.IsDBNull(dr.GetOrdinal("nombreProveedor")) ? dr.GetString(dr.GetOrdinal("nombreProveedor")) : string.Empty;
                    Item.TelefonoProveedor = !dr.IsDBNull(dr.GetOrdinal("telefonoProveedor")) ? dr.GetString(dr.GetOrdinal("telefonoProveedor")) : string.Empty;
                    Item.RFCProveedor = !dr.IsDBNull(dr.GetOrdinal("rfcProveedor")) ? dr.GetString(dr.GetOrdinal("rfcProveedor")) : string.Empty;
                    Item.AnnoImpresion = !dr.IsDBNull(dr.GetOrdinal("anno")) ? dr.GetString(dr.GetOrdinal("anno")) : string.Empty;
                    Item.MesImpresion = !dr.IsDBNull(dr.GetOrdinal("mes")) ? dr.GetString(dr.GetOrdinal("mes")) : string.Empty;
                    Item.DiaImpresion = !dr.IsDBNull(dr.GetOrdinal("dia")) ? dr.GetString(dr.GetOrdinal("dia")) : string.Empty;
                }
                dr.Close();
                return Item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ComprobanteCompraDetallesModels> GetComprobanteCompraDetalles(CompraModels Compra)
        {
            try
            {
                List<ComprobanteCompraDetallesModels> Lista = new List<ComprobanteCompraDetallesModels>();
                ComprobanteCompraDetallesModels Item;
                object[] parametros = { Compra.IDCompra };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Compra_get_ComprobanteCompraDetalles", parametros);

                while (dr.Read())
                {
                    Item = new ComprobanteCompraDetallesModels();
                    Item.Concepto = !dr.IsDBNull(dr.GetOrdinal("descripcion")) ? dr.GetString(dr.GetOrdinal("descripcion")) : string.Empty;
                    Item.Cantidad = !dr.IsDBNull(dr.GetOrdinal("cantidad")) ? dr.GetDecimal(dr.GetOrdinal("cantidad")) : 0;
                    Item.Subtotal = !dr.IsDBNull(dr.GetOrdinal("subtotal")) ? dr.GetDecimal(dr.GetOrdinal("subtotal")) : 0;

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
    }
}
