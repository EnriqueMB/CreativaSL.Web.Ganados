using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class _Venta2_Datos
    {
        #region Datatables
        public string DatatableGanadoActual(VentaModels2 venta)
        {
            try
            {
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(venta.Conexion, "spCSLDB_Venta_get_DatatableGanadoActual");
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
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(venta.Conexion, "spCSLDB_Venta_get_DatatableGanadoParaVenta");
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

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Venta.Conexion, "spCSLDB_Combo_get_CatEmpresa");
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
                    Venta.Flete.Id_empresa
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
        #endregion
        #region Vehiculos
        public List<CatVehiculoModels> GetVehiculosXIDEmpresa(VentaModels2 Venta)
        {
            try
            {
                object[] parametros =
                {
                    Venta.Flete.Id_empresa
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
                CatClienteModels Cliente;
                List<CatClienteModels> ListaClientes = new List<CatClienteModels>();

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Venta.Conexion, "spCSLDB_Combo_get_CatClienteConLugares");
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

        #endregion

        #region Vistas
        #region FleteVenta
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
                    datos.NombreVenta,                                      datos.Descripcion_bascula,                              datos.Observacion,
                    datos.Usuario,                                          datos.CobrarFlete,                                      datos.Flete.precioFlete,
                    datos.Flete.Id_empresa,                                 datos.Flete.id_vehiculo,                                datos.Flete.id_chofer,
                    datos.Flete.VerificacionJaula.Id_verificacionJaula,     datos.Flete.kmInicialVehiculo,                          datos.Flete.FechaSalida,
                    datos.Flete.HoraSalida,                                 datos.Flete.FechaEmbarque,                              datos.Flete.HoraEmbarque,
                    datos.Flete.NumFleje,
                    datos.Flete.VerificacionJaula.LimpiezaCompleta,         datos.Flete.VerificacionJaula.SoloPiso,                 datos.Flete.VerificacionJaula.Sucia,
                    datos.Flete.VerificacionJaula.PuertasInternas,          datos.Flete.VerificacionJaula.Focos,                    datos.Flete.VerificacionJaula.RiesgosPunzoCortantes,
                    datos.Flete.VerificacionJaula.LlantaRefaccion,          datos.Flete.VerificacionJaula.LlantasBuenEstado,        datos.Flete.VerificacionJaula.PisoAntiadherente
                };

                RespuestaAjax RespuestaAjax = new RespuestaAjax();
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Venta_ac_Flete", parametros);
                while (dr.Read())
                {
                    RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : false;
                    RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("Mensaje")) ? dr.GetString(dr.GetOrdinal("Mensaje")) : string.Empty;
                }

                return RespuestaAjax;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
        #endregion
    }
}