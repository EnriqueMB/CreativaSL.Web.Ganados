using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class _Flete_Datos
    {

        #region Index
        public SqlDataReader ObtenerFleteIndexDataTable(FleteModels Flete)
        {
            try
            {
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Flete.Conexion, "spCSLDB_Flete_index");
                return dr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Get
        #region Get AC_Flete
        public FleteModels Flete_get_ACFlete(FleteModels Flete)
        {
            try
            {
                object[] parametros =
                {
                     Flete.id_flete
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Flete.Conexion, "spCSLDB_Flete_get_ACFlete", parametros);

                while (dr.Read())
                {
                    Flete.Empresa.IDEmpresa = !dr.IsDBNull(dr.GetOrdinal("id_empresa")) ? dr.GetString(dr.GetOrdinal("id_empresa")) : string.Empty;//f.id_empresa
                    Flete.Cliente.IDCliente = !dr.IsDBNull(dr.GetOrdinal("id_cliente")) ? dr.GetString(dr.GetOrdinal("id_cliente")) : string.Empty;//,f.id_cliente
                    Flete.Vehiculo.IDVehiculo = !dr.IsDBNull(dr.GetOrdinal("id_vehiculo")) ? dr.GetString(dr.GetOrdinal("id_vehiculo")) : string.Empty;//,f.id_vehiculo
                    Flete.Chofer.IDChofer = !dr.IsDBNull(dr.GetOrdinal("id_chofer")) ? dr.GetString(dr.GetOrdinal("id_chofer")) : string.Empty;//,f.id_chofer
                    Flete.Jaula.IDJaula = !dr.IsDBNull(dr.GetOrdinal("id_jaula")) ? dr.GetString(dr.GetOrdinal("id_jaula")) : string.Empty;//,f.id_jaula
                    Flete.Remolque.IDRemolque = !dr.IsDBNull(dr.GetOrdinal("id_remolque")) ? dr.GetString(dr.GetOrdinal("id_remolque")) : string.Empty;//,f.id_remolque
                    Flete.kmInicialVehiculo = !dr.IsDBNull(dr.GetOrdinal("kmInicialVehiculo")) ? dr.GetInt32(dr.GetOrdinal("kmInicialVehiculo")) : 0;//,f.kmInicialVehiculo
                    Flete.FechaTentativaEntrega = !dr.IsDBNull(dr.GetOrdinal("fechaTentativaEntrega")) ? dr.GetDateTime(dr.GetOrdinal("fechaTentativaEntrega")) : DateTime.Now;//,f.fechaTentativaEntrega
                    Flete.Folio = !dr.IsDBNull(dr.GetOrdinal("folio")) ? dr.GetString(dr.GetOrdinal("folio")) : string.Empty;//,f.folio
                    Flete.Trayecto.id_trayecto = !dr.IsDBNull(dr.GetOrdinal("id_trayecto")) ? dr.GetString(dr.GetOrdinal("id_trayecto")) : string.Empty;//,t.id_trayecto
                    Flete.Trayecto.Destinatario.IDCliente = !dr.IsDBNull(dr.GetOrdinal("id_destinatario")) ? dr.GetString(dr.GetOrdinal("id_destinatario")) : string.Empty;//,t.id_destinatario
                    Flete.Trayecto.Remitente.IDCliente = !dr.IsDBNull(dr.GetOrdinal("id_remitente")) ? dr.GetString(dr.GetOrdinal("id_remitente")) : string.Empty;//,t.id_remitente
                    Flete.Trayecto.LugarOrigen.id_lugar = !dr.IsDBNull(dr.GetOrdinal("id_lugarOrigen")) ? dr.GetString(dr.GetOrdinal("id_lugarOrigen")) : string.Empty;//,t.id_lugarOrigen
                    Flete.Trayecto.LugarOrigen.Direccion = !dr.IsDBNull(dr.GetOrdinal("direccionLugarOrigen")) ? dr.GetString(dr.GetOrdinal("direccionLugarOrigen")) : string.Empty;//,(select direccion from dbo.tbl_CatLugar where id_lugar = t.id_lugarOrigen) as direccionLugarOrigen
                    Flete.Trayecto.LugarOrigen.descripcion = !dr.IsDBNull(dr.GetOrdinal("descripcionLugarOrigen")) ? dr.GetString(dr.GetOrdinal("descripcionLugarOrigen")) : string.Empty;//,(select descripcion from dbo.tbl_CatLugar where id_lugar = t.id_lugarOrigen) as descripcionLugarOrigen
                    Flete.Trayecto.LugarDestino.id_lugar = !dr.IsDBNull(dr.GetOrdinal("id_lugarDestino")) ? dr.GetString(dr.GetOrdinal("id_lugarDestino")) : string.Empty;//,t.id_lugarDestino
                    Flete.Trayecto.LugarDestino.Direccion = !dr.IsDBNull(dr.GetOrdinal("direccionLugarDestino")) ? dr.GetString(dr.GetOrdinal("direccionLugarDestino")) : string.Empty;//,(select direccion from dbo.tbl_CatLugar where id_lugar = t.id_lugarDestino) as direccionLugarDestino
                    Flete.Trayecto.LugarDestino.descripcion = !dr.IsDBNull(dr.GetOrdinal("descripcionLugarDestino")) ? dr.GetString(dr.GetOrdinal("descripcionLugarDestino")) : string.Empty;//,(select descripcion from dbo.tbl_CatLugar where id_lugar = t.id_lugarDestino) as descripcionLugarDestino
                }
                return Flete;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Get EstatusFleteXIDFlete
        public FleteModels GetEstatusFleteXIDFlete(FleteModels Flete)
        {
            try
            {
                object[] parametros =
                {
                     Flete.id_flete
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Flete.Conexion, "spCSLDB_Flete_get_EstatusFleteXIDFlete", parametros);

                while (dr.Read())
                {
                    Flete.Estatus = !dr.IsDBNull(dr.GetOrdinal("estatus")) ? dr.GetInt16(dr.GetOrdinal("estatus")) : 0;//,(select descripcion from dbo.tbl_CatLugar where id_lugar = t.id_lugarDestino) as descripcionLugarDestino
                }
                return Flete;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #endregion

        #region Combos
        public List<CatClienteModels> GetListadoClientes(FleteModels Flete)
        {

            try
            {
                CatClienteModels Cliente;
                Flete.Cliente.ListaClientes = new List<CatClienteModels>();

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Flete.Conexion, "spCSLDB_Combo_get_AllClienteProveedor");
                while (dr.Read())
                {
                    Cliente = new CatClienteModels
                    {
                        IDCliente = !dr.IsDBNull(dr.GetOrdinal("IDCliente")) ? dr.GetString(dr.GetOrdinal("IDCliente")) : string.Empty,
                        NombreRazonSocial = !dr.IsDBNull(dr.GetOrdinal("NombreCliente")) ? dr.GetString(dr.GetOrdinal("NombreCliente")) : string.Empty,
                        RFC = !dr.IsDBNull(dr.GetOrdinal("RFC")) ? dr.GetString(dr.GetOrdinal("RFC")) : string.Empty,
                    };

                    Flete.Cliente.ListaClientes.Add(Cliente);
                }
                return Flete.Cliente.ListaClientes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CatEmpresaModels> GetListadoEmpresas(FleteModels Flete)
        {

            try
            {
                CatEmpresaModels Empresa;
                Flete.Empresa.ListaEmpresas = new List<CatEmpresaModels>();

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Flete.Conexion, "spCSLDB_Combo_get_CatEmpresa");
                while (dr.Read())
                {
                    Empresa = new CatEmpresaModels
                    {
                        IDEmpresa = !dr.IsDBNull(dr.GetOrdinal("IDEmpresa")) ? dr.GetString(dr.GetOrdinal("IDEmpresa")) : string.Empty,
                        RazonFiscal = !dr.IsDBNull(dr.GetOrdinal("NombreEmpresa")) ? dr.GetString(dr.GetOrdinal("NombreEmpresa")) : string.Empty
                    };

                    Flete.Empresa.ListaEmpresas.Add(Empresa);
                }
                return Flete.Empresa.ListaEmpresas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CatChoferModels> GetListadoChoferes(FleteModels Flete)
        {

            try
            {
                CatChoferModels Chofer;
                Flete.Chofer.ListaChoferes = new List<CatChoferModels>();

                SqlDataReader dr = null;
                object[] parametros =
                {
                    Flete.Empresa.IDEmpresa
                };
                dr = SqlHelper.ExecuteReader(Flete.Conexion, "spCSLDB_Combo_get_CatChoferesXIDEmpresa", parametros);
                while (dr.Read())
                {
                    Chofer = new CatChoferModels
                    {
                        IDChofer = !dr.IsDBNull(dr.GetOrdinal("IDChofer")) ? dr.GetString(dr.GetOrdinal("IDChofer")) : string.Empty,
                        Nombre = !dr.IsDBNull(dr.GetOrdinal("NombreCompleto")) ? dr.GetString(dr.GetOrdinal("NombreCompleto")) : string.Empty
                    };

                    Flete.Chofer.ListaChoferes.Add(Chofer);
                }
                return Flete.Chofer.ListaChoferes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CatVehiculoModels> GetListadoVehiculos(FleteModels Flete)
        {

            try
            {
                CatVehiculoModels Vehiculo;
                Flete.Vehiculo.listaVehiculos = new List<CatVehiculoModels>();

                SqlDataReader dr = null;
                object[] parametros =
                {
                    Flete.Empresa.IDEmpresa
                };
                dr = SqlHelper.ExecuteReader(Flete.Conexion, "spCSLDB_Combo_get_CatVehiculosXIDEmpresa", parametros);
                while (dr.Read())
                {
                    Vehiculo = new CatVehiculoModels
                    {
                        IDVehiculo = !dr.IsDBNull(dr.GetOrdinal("IDVehiculo")) ? dr.GetString(dr.GetOrdinal("IDVehiculo")) : string.Empty,
                        nombreMarca = !dr.IsDBNull(dr.GetOrdinal("NombreVehiculo")) ? dr.GetString(dr.GetOrdinal("NombreVehiculo")) : string.Empty,
                        nombreTipoVehiculo = !dr.IsDBNull(dr.GetOrdinal("Tipo")) ? dr.GetString(dr.GetOrdinal("Tipo")) : string.Empty,
                    };

                    Flete.Vehiculo.listaVehiculos.Add(Vehiculo);
                }
                return Flete.Vehiculo.listaVehiculos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CatJaulaModels> GetListadoJaulas(FleteModels Flete)
        {

            try
            {
                CatJaulaModels Jaula;
                Flete.Jaula.listaJaulas = new List<CatJaulaModels>();

                SqlDataReader dr = null;
                object[] parametros =
                {
                    Flete.Empresa.IDEmpresa
                };
                dr = SqlHelper.ExecuteReader(Flete.Conexion, "spCSLDB_Combo_get_CatJaulaXIDEmpresa", parametros);
                while (dr.Read())
                {
                    Jaula = new CatJaulaModels
                    {
                        IDJaula = !dr.IsDBNull(dr.GetOrdinal("IDJaula")) ? dr.GetString(dr.GetOrdinal("IDJaula")) : string.Empty,
                        Matricula = !dr.IsDBNull(dr.GetOrdinal("Placa")) ? dr.GetString(dr.GetOrdinal("Placa")) : string.Empty,
                    };

                    Flete.Jaula.listaJaulas.Add(Jaula);
                }
                return Flete.Jaula.listaJaulas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CatRemolqueModels> GetListadoRemolque(FleteModels Flete)
        {

            try
            {
                CatRemolqueModels Remolque;
                Flete.Remolque.listaRemolque = new List<CatRemolqueModels>();

                SqlDataReader dr = null;
                object[] parametros =
                {
                    Flete.Empresa.IDEmpresa
                };
                dr = SqlHelper.ExecuteReader(Flete.Conexion, "spCSLDB_Combo_get_CatRemolqueXIDEmpresa", parametros);
                while (dr.Read())
                {
                    Remolque = new CatRemolqueModels
                    {
                        IDRemolque = !dr.IsDBNull(dr.GetOrdinal("IDRemolque")) ? dr.GetString(dr.GetOrdinal("IDRemolque")) : string.Empty,
                        placa = !dr.IsDBNull(dr.GetOrdinal("Placa")) ? dr.GetString(dr.GetOrdinal("Placa")) : string.Empty,
                    };

                    Flete.Remolque.listaRemolque.Add(Remolque);
                }
                return Flete.Remolque.listaRemolque;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CatLugarModels> GetListadoLugaresXIDProveedorIDCliente(FleteModels Flete, string id)
        {
            try
            {
                CatLugarModels Lugar;
                List<CatLugarModels> ListaLugares = new List<CatLugarModels>();

                SqlDataReader dr = null;
                object[] parametros =
                {
                    id
                };
                dr = SqlHelper.ExecuteReader(Flete.Conexion, "spCSLDB_Combo_get_CatLugarXIDProveedorIDCliente", parametros);
                while (dr.Read())
                {
                    Lugar = new CatLugarModels
                    {
                        id_lugar = !dr.IsDBNull(dr.GetOrdinal("IDLugar")) ? dr.GetString(dr.GetOrdinal("IDLugar")) : string.Empty,
                        descripcion = !dr.IsDBNull(dr.GetOrdinal("NombreLugar")) ? dr.GetString(dr.GetOrdinal("NombreLugar")) : string.Empty,
                        latitud = float.Parse(dr["GpsLatitud"].ToString()),
                        longitud = float.Parse(dr["GpsLongitud"].ToString()),
                        Direccion = !dr.IsDBNull(dr.GetOrdinal("Direccion")) ? dr.GetString(dr.GetOrdinal("Direccion")) : string.Empty,
                    };

                    ListaLugares.Add(Lugar);
                }
                return ListaLugares;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CFDI_FormaPagoModels> GetListadoFormaPagos(FleteModels Flete)
        {

            try
            {
                CFDI_FormaPagoModels FormaPago;
                Flete.ListaFormaPago = new List<CFDI_FormaPagoModels>();

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Flete.Conexion, "spCSLDB_Combo_get_CFDIFormaPago");
                while (dr.Read())
                {
                    FormaPago = new CFDI_FormaPagoModels
                    {
                        Clave = !dr.IsDBNull(dr.GetOrdinal("ID")) ? dr.GetInt16(dr.GetOrdinal("ID")) : 0,
                        Descripcion = !dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? dr.GetString(dr.GetOrdinal("Descripcion")) : string.Empty,
                    };

                    Flete.ListaFormaPago.Add(FormaPago);
                }
                return Flete.ListaFormaPago;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CFDI_MetodoPagoModels> GetListadoMetodoPago(FleteModels Flete)
        {

            try
            {
                CFDI_MetodoPagoModels MetodoPago;
                Flete.ListaMetodoPago = new List<CFDI_MetodoPagoModels>();

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Flete.Conexion, "spCSLDB_Combo_get_CFDIMetodoPago");
                while (dr.Read())
                {
                    MetodoPago = new CFDI_MetodoPagoModels
                    {
                        Clave = !dr.IsDBNull(dr.GetOrdinal("ID")) ? dr.GetString(dr.GetOrdinal("ID")) : string.Empty,
                        Descripcion = !dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? dr.GetString(dr.GetOrdinal("Descripcion")) : string.Empty,
                    };

                    Flete.ListaMetodoPago.Add(MetodoPago);
                }
                return Flete.ListaMetodoPago;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CatChoferModels> GetChoferesXIDEmpresa(FleteModels Flete)
        {
            try
            {
                object[] parametros =
                {
                    Flete.Empresa.IDEmpresa
                };
                CatChoferModels Chofer;

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Flete.Conexion, "spCSLDB_Combo_get_CatChoferesXIDEmpresa", parametros);
                while (dr.Read())
                {
                    Chofer = new CatChoferModels
                    {
                        IDChofer = !dr.IsDBNull(dr.GetOrdinal("IDChofer")) ? dr.GetString(dr.GetOrdinal("IDChofer")) : string.Empty,
                        Nombre = !dr.IsDBNull(dr.GetOrdinal("NombreCompleto")) ? dr.GetString(dr.GetOrdinal("NombreCompleto")) : string.Empty,
                    };

                    Flete.Chofer.ListaChoferes.Add(Chofer);
                }
                return Flete.Chofer.ListaChoferes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CatVehiculoModels> GetVehiculosXIDEmpresa(FleteModels Flete)
        {
            try
            {
                object[] parametros =
                {
                    Flete.Empresa.IDEmpresa
                };
                CatVehiculoModels Vehiculo;

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Flete.Conexion, "spCSLDB_Combo_get_CatVehiculosXIDEmpresa", parametros);
                while (dr.Read())
                {
                    Vehiculo = new CatVehiculoModels
                    {
                        IDVehiculo = !dr.IsDBNull(dr.GetOrdinal("IDVehiculo")) ? dr.GetString(dr.GetOrdinal("IDVehiculo")) : string.Empty,
                        nombreMarca = !dr.IsDBNull(dr.GetOrdinal("NombreVehiculo")) ? dr.GetString(dr.GetOrdinal("NombreVehiculo")) : string.Empty,
                        Modelo = !dr.IsDBNull(dr.GetOrdinal("Tipo")) ? dr.GetString(dr.GetOrdinal("Tipo")) : string.Empty
                    };

                    Flete.Vehiculo.listaVehiculos.Add(Vehiculo);
                }
                return Flete.Vehiculo.listaVehiculos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CatJaulaModels> GetJaulasXIDEmpresa(FleteModels Flete)
        {
            try
            {
                object[] parametros =
                {
                    Flete.Empresa.IDEmpresa
                };
                CatJaulaModels Jaula;

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Flete.Conexion, "spCSLDB_Combo_get_CatJaulaXIDEmpresa", parametros);
                while (dr.Read())
                {
                    Jaula = new CatJaulaModels
                    {
                        IDJaula = !dr.IsDBNull(dr.GetOrdinal("IDJaula")) ? dr.GetString(dr.GetOrdinal("IDJaula")) : string.Empty,
                        Matricula = !dr.IsDBNull(dr.GetOrdinal("Placa")) ? dr.GetString(dr.GetOrdinal("Placa")) : string.Empty,
                    };

                    Flete.Jaula.listaJaulas.Add(Jaula);
                }
                return Flete.Jaula.listaJaulas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CatRemolqueModels> GetRemolquesXIDEmpresa(FleteModels Flete)
        {
            try
            {
                object[] parametros =
                {
                    Flete.Empresa.IDEmpresa
                };
                CatRemolqueModels Remolque;

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Flete.Conexion, "spCSLDB_Combo_get_CatRemolqueXIDEmpresa", parametros);
                while (dr.Read())
                {
                    Remolque = new CatRemolqueModels
                    {
                        IDRemolque = !dr.IsDBNull(dr.GetOrdinal("IDRemolque")) ? dr.GetString(dr.GetOrdinal("IDRemolque")) : string.Empty,
                        placa = !dr.IsDBNull(dr.GetOrdinal("Placa")) ? dr.GetString(dr.GetOrdinal("Placa")) : string.Empty,
                    };

                    Flete.Remolque.listaRemolque.Add(Remolque);
                }
                return Flete.Remolque.listaRemolque;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        #endregion

        #region Funciones AC
        #region AC_Cliente
        public FleteModels Flete_ac_FleteCliente(FleteModels Flete)
        {
            try
            {
                object[] parametros =
                {
                     Flete.id_flete
                    ,Flete.Empresa.IDEmpresa
                    ,Flete.Cliente.IDCliente
                    ,Flete.Vehiculo.IDVehiculo
                    ,Flete.Chofer.IDChofer
                    ,Flete.Jaula.IDJaula
                    ,Flete.Remolque.IDRemolque
                    ,Flete.kmInicialVehiculo
                    ,Flete.FechaTentativaEntrega
                    ,Flete.Usuario
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Flete.Conexion, "spCSLDB_Flete_ac_FleteCliente", parametros);

                while (dr.Read())
                {
                    Flete.RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                    Flete.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : true;
                }
                return Flete;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region AC_Cliente
        public FleteModels Flete_ac_FleteTrayecto(FleteModels Flete)
        {
            try
            {
                object[] parametros =
                {
                     Flete.Trayecto.id_trayecto
                    ,Flete.id_flete
                    ,Flete.Trayecto.Remitente.IDCliente
                    ,Flete.Trayecto.LugarOrigen.id_lugar
                    ,Flete.Trayecto.Destinatario.IDCliente
                    ,Flete.Trayecto.LugarDestino.id_lugar
                    ,Flete.Usuario
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Flete.Conexion, "spCSLDB_Flete_ac_FleteTrayecto", parametros);

                while (dr.Read())
                {
                    Flete.RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                    Flete.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : true;
                }
                return Flete;
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