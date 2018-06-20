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
        private CultureInfo CultureInfo = new CultureInfo("es-MX");


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
        #endregion

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
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "[spCSLDB_Combo_get_CatProveedor]");
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
                CatEmpresaModels Empresa;

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Combo_get_CatEmpresaCompra");
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


                item.Descripcion = !dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? dr.GetString(dr.GetOrdinal("Descripcion")) : string.Empty;

                item.Rango_inferior = !dr.IsDBNull(dr.GetOrdinal("RangoInferior")) ? dr.GetInt32(dr.GetOrdinal("RangoInferior")) : 0;
                item.Rango_superior = !dr.IsDBNull(dr.GetOrdinal("RangoSuperior")) ? dr.GetInt32(dr.GetOrdinal("RangoSuperior")) : 0;
                item.Id_corral = !dr.IsDBNull(dr.GetOrdinal("IDCorral")) ? dr.GetInt32(dr.GetOrdinal("IDCorral")) : 0;
                item.Genero = !dr.IsDBNull(dr.GetOrdinal("Genero")) ? dr.GetString(dr.GetOrdinal("Genero")) : string.Empty;
                lista.Add(item);
            }
            dr.Close();
            return lista;
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
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Inicio_get_ComprasNoFinalizadas",Compra.fechaStart,Compra.fechaEnd);
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
                    Compra.IDJaula = !dr.IsDBNull(dr.GetOrdinal("id_jaula")) ? dr.GetString(dr.GetOrdinal("id_jaula")) : string.Empty;
                    Compra.IDRemolque = !dr.IsDBNull(dr.GetOrdinal("id_remolque")) ? dr.GetString(dr.GetOrdinal("id_remolque")) : string.Empty;
                    Compra.Flete.kmInicialVehiculo = !dr.IsDBNull(dr.GetOrdinal("kmInicialVehiculo")) ? dr.GetInt32(dr.GetOrdinal("kmInicialVehiculo")) : 0;
                    Compra.Trayecto.id_lugarOrigen = !dr.IsDBNull(dr.GetOrdinal("id_lugarOrigen")) ? dr.GetString(dr.GetOrdinal("id_lugarOrigen")) : string.Empty;
                    Compra.Trayecto.id_lugarDestino = !dr.IsDBNull(dr.GetOrdinal("id_lugarDestino")) ? dr.GetString(dr.GetOrdinal("id_lugarDestino")) : string.Empty;
                    Compra.IDPLugarProveedor = !dr.IsDBNull(dr.GetOrdinal("id_lugar_proveedor")) ? dr.GetString(dr.GetOrdinal("id_lugar_proveedor")) : string.Empty;
                    Compra.Flete.precioFlete = !dr.IsDBNull(dr.GetOrdinal("precioFlete")) ? dr.GetDecimal(dr.GetOrdinal("precioFlete")) : 0;
                    Compra.Flete.NumFleje = !dr.IsDBNull(dr.GetOrdinal("numFleje")) ? dr.GetString(dr.GetOrdinal("numFleje")) : string.Empty;
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
                documentosPorCobrar.DocumentoPorCobraFlete = new DocumentosPorCobrarModels();
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

                    documentosPorCobrar.DocumentoPorCobraFlete.Pagos = !dr.IsDBNull(dr.GetOrdinal("pagosFlete")) ? dr.GetDecimal(dr.GetOrdinal("pagosFlete")) : 0;
                    documentosPorCobrar.DocumentoPorCobraFlete.Impuestos = !dr.IsDBNull(dr.GetOrdinal("impuestoFlete")) ? dr.GetDecimal(dr.GetOrdinal("impuestoFlete")) : 0;
                    documentosPorCobrar.DocumentoPorCobraFlete.Total = !dr.IsDBNull(dr.GetOrdinal("totalFlete")) ? dr.GetDecimal(dr.GetOrdinal("totalFlete")) : 0;
                    documentosPorCobrar.DocumentoPorCobraFlete.Cambio = !dr.IsDBNull(dr.GetOrdinal("cambioFlete")) ? dr.GetDecimal(dr.GetOrdinal("cambioFlete")) : 0;
                    documentosPorCobrar.DocumentoPorCobraFlete.Pendiente = !dr.IsDBNull(dr.GetOrdinal("pendienteFlete")) ? dr.GetDecimal(dr.GetOrdinal("pendienteFlete")) : 0;
                    documentosPorCobrar.DocumentoPorCobraFlete.Id_documentoCobrar = !dr.IsDBNull(dr.GetOrdinal("id_documentoPorCobrarFlete")) ? dr.GetString(dr.GetOrdinal("id_documentoPorCobrarFlete")) : string.Empty;
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
                    documentosPorPagar.Pagos = !dr.IsDBNull(dr.GetOrdinal("pagos")) ? dr.GetDecimal(dr.GetOrdinal("pagos")) : 0;
                    documentosPorPagar.Total = !dr.IsDBNull(dr.GetOrdinal("total")) ? dr.GetDecimal(dr.GetOrdinal("total")) : 0;
                    documentosPorPagar.Pendientes = !dr.IsDBNull(dr.GetOrdinal("pendientes")) ? dr.GetDecimal(dr.GetOrdinal("pendientes")) : 0;
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
        public DocumentoCompraModels GetDocumentoXIDDocumento(DocumentoCompraModels Documento)
        {
            try
            {
                object[] parametros =
                {
                     Documento.IDDocumento
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Documento.Conexion, "spCSLDB_Compra_get_DocumentoXIDDocumento", parametros);

                while (dr.Read())
                {
                    Documento.IDTipoDocumento = !dr.IsDBNull(dr.GetOrdinal("id_tipoDocumento")) ? dr.GetInt16(dr.GetOrdinal("id_tipoDocumento")) : 0;
                    Documento.Clave = !dr.IsDBNull(dr.GetOrdinal("clave")) ? dr.GetString(dr.GetOrdinal("clave")) : string.Empty;
                    //Solo para mostrar
                    Documento.Imagen = !dr.IsDBNull(dr.GetOrdinal("imagen")) ? dr.GetString(dr.GetOrdinal("imagen")) : string.Empty;
                }
                if (string.IsNullOrEmpty(Documento.Imagen))
                {
                    //No hay imagen en el server
                    Documento.MostrarImagen = Auxiliar.SetDefaultImage();
                    Documento.FlagImg = false;
                }
                else
                {
                    //Guardamos el string de la imagen
                    Documento.MostrarImagen = Documento.Imagen;
                    Documento.FlagImg = true;
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
        public List<CatTipoDocumentoModels> GetListaTiposDocumentos(DocumentoCompraModels Documento)
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
                    Compra.GanadosPactadoHembras = !dr.IsDBNull(dr.GetOrdinal("ganadoCompradoHembras")) ? dr.GetInt32(dr.GetOrdinal("ganadoCompradoHembras")) : 0;
                    Compra.GanadosPactadoTotal = !dr.IsDBNull(dr.GetOrdinal("ganadoTotalPactado")) ? dr.GetInt32(dr.GetOrdinal("ganadoTotalPactado")) : 0;
                    Compra.Trayecto.LugarOrigen.descripcion = !dr.IsDBNull(dr.GetOrdinal("lugarOrigen")) ? dr.GetString(dr.GetOrdinal("lugarOrigen")) : string.Empty;
                    Compra.Trayecto.LugarDestino.descripcion = !dr.IsDBNull(dr.GetOrdinal("lugarDestino")) ? dr.GetString(dr.GetOrdinal("lugarDestino")) : string.Empty;
                    Compra.Chofer.Nombre = !dr.IsDBNull(dr.GetOrdinal("nombreChofer")) ? dr.GetString(dr.GetOrdinal("nombreChofer")) : string.Empty; 
                    Compra.Flete.kmInicialVehiculo = !dr.IsDBNull(dr.GetOrdinal("kmInicialVehiculo")) ? dr.GetInt32(dr.GetOrdinal("kmInicialVehiculo")) : 0; 
                    Compra.Flete.precioFlete = !dr.IsDBNull(dr.GetOrdinal("precioFlete")) ? dr.GetDecimal(dr.GetOrdinal("precioFlete")) : 0;
                    Compra.Vehiculo.nombreTipoVehiculo = !dr.IsDBNull(dr.GetOrdinal("vehiculo")) ? dr.GetString(dr.GetOrdinal("vehiculo")) : string.Empty;
                    Compra.LineaFletera = !dr.IsDBNull(dr.GetOrdinal("lineaFletera")) ? dr.GetString(dr.GetOrdinal("lineaFletera")) : string.Empty;
                    Compra.Tolerancia = !dr.IsDBNull(dr.GetOrdinal("tolerancia")) ? dr.GetDecimal(dr.GetOrdinal("tolerancia")) : 0;
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
                    ,Compra.GanadosPactadoMachos
                    ,Compra.GanadosPactadoHembras
                    ,Compra.FechaHoraProgramada
                    ,Compra.Usuario = string.IsNullOrEmpty(Compra.Usuario) ? null : Compra.Usuario
                    ,Compra.IDPLugarProveedor = string.IsNullOrEmpty(Compra.IDPLugarProveedor) ? null : Compra.IDPLugarProveedor
                    ,Compra.Descripcion_bascula
                    ,Compra.Observacion_programacion
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
                    ,Compra.IDJaula= string.IsNullOrEmpty(Compra.IDJaula) ? null : Compra.IDJaula
                    ,Compra.IDRemolque= string.IsNullOrEmpty(Compra.IDRemolque) ? null : Compra.IDRemolque
                    ,Compra.Flete.kmInicialVehiculo
                    ,Compra.Usuario= string.IsNullOrEmpty(Compra.Usuario) ? null : Compra.Usuario
                    ,Compra.Trayecto.id_lugarOrigen = string.IsNullOrEmpty(Compra.Trayecto.id_lugarOrigen) ? null : Compra.Trayecto.id_lugarOrigen
                    ,Compra.Trayecto.id_lugarDestino = string.IsNullOrEmpty(Compra.Trayecto.id_lugarDestino) ? null : Compra.Trayecto.id_lugarDestino
                    ,Compra.IDSucursal
                    ,Compra.Flete.precioFlete
                    ,Compra.Flete.NumFleje
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
                    indiceActual
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
        public DocumentoCompraModels AC_Documento(DocumentoCompraModels Documento)
        {
            try
            {
                object[] parametros =
                {
                     Documento.IDDocumento
                    ,Documento.IDCompra
                    ,Documento.IDTipoDocumento
                    ,Documento.Clave
                    ,Documento.Imagen
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
        public DocumentoCompraModels DEL_DocumentoXIDDocumento(DocumentoCompraModels Documento)
        {
            try
            {
                object[] parametros =
                {
                     Documento.IDDocumento
                    ,Documento.Usuario
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
        public List<CatFierroModels> GetListadoFierrosXIDCompra(CompraModels Compra)
        {
            CatFierroModels Fierro;
            SqlDataReader dr = null;
            object[] parametros =
            {
                Compra.IDCompra
            };

            dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_get_CatFierroXIDCompra", parametros);

            while (dr.Read())
            {
                Fierro = new CatFierroModels
                {
                    IDFierro = !dr.IsDBNull(dr.GetOrdinal("id_fierro")) ? dr.GetString(dr.GetOrdinal("id_fierro")) : string.Empty,
                    NombreFierro = !dr.IsDBNull(dr.GetOrdinal("nombreFierro")) ? dr.GetString(dr.GetOrdinal("nombreFierro")) : string.Empty,
                    ImgFierro = !dr.IsDBNull(dr.GetOrdinal("imgFierro")) ? dr.GetString(dr.GetOrdinal("imgFierro")) : string.Empty
                };
                dr.Close();
                Compra.ListaFierros.Add(Fierro);
            }
            return Compra.ListaFierros;

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
    }
}