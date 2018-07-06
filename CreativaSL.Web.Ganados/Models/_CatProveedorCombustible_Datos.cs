using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class _CatProveedorCombustible_Datos
    {

        public List<CatProveedorCombustibleModels> ObtenerCatProveedores(CatProveedorCombustibleModels Datos)
        {
            try
            {
                List<CatProveedorCombustibleModels> lista = new List<CatProveedorCombustibleModels>();
                CatProveedorCombustibleModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Catalogo_get_CatProveedorCombustible");
                while (dr.Read())
                {
                    item = new CatProveedorCombustibleModels();
                    item.IDProveedor = !dr.IsDBNull(dr.GetOrdinal("IDProveedorCombustible")) ? dr.GetString(dr.GetOrdinal("IDProveedorCombustible")) : string.Empty;
                    item.IDSucursal = !dr.IsDBNull(dr.GetOrdinal("IDSucursal")) ? dr.GetString(dr.GetOrdinal("IDSucursal")) : string.Empty;
                    item.nombreSucursal = !dr.IsDBNull(dr.GetOrdinal("nombreSucursal")) ? dr.GetString(dr.GetOrdinal("nombreSucursal")) : string.Empty;
                    item.NombreRazonSocial = !dr.IsDBNull(dr.GetOrdinal("RazonSocial")) ? dr.GetString(dr.GetOrdinal("RazonSocial")) : string.Empty;
                    item.RFC = !dr.IsDBNull(dr.GetOrdinal("RFC")) ? dr.GetString(dr.GetOrdinal("RFC")) : string.Empty;
                    item.Direccion = !dr.IsDBNull(dr.GetOrdinal("Direccion")) ? dr.GetString(dr.GetOrdinal("Direccion")) : string.Empty;
                    item.telefonoCelular = !dr.IsDBNull(dr.GetOrdinal("TelefonoCelular")) ? dr.GetString(dr.GetOrdinal("TelefonoCelular")) : string.Empty;
                    item.telefonoCasa = !dr.IsDBNull(dr.GetOrdinal("TelefonoCasa")) ? dr.GetString(dr.GetOrdinal("TelefonoCasa")) : string.Empty;
                    item.correo = !dr.IsDBNull(dr.GetOrdinal("CorreoElectronico")) ? dr.GetString(dr.GetOrdinal("CorreoElectronico")) : string.Empty;
                    item.fechaIngreso = !dr.IsDBNull(dr.GetOrdinal("FechaIngreso")) ? dr.GetDateTime(dr.GetOrdinal("FechaIngreso")) : DateTime.Now;
                    item.observaciones = !dr.IsDBNull(dr.GetOrdinal("Observaciones")) ? dr.GetString(dr.GetOrdinal("Observaciones")) : string.Empty;
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
        public CatProveedorCombustibleModels acCatProveedorCombustible(CatProveedorCombustibleModels datos)
        {
            try
            {
                object[] parametros =
                {
                   datos.Opcion,
                   datos.IDProveedor ?? string.Empty,

                   datos.IDSucursal ?? string.Empty,

                   datos.NombreRazonSocial?? string.Empty,
                   datos.Direccion?? string.Empty,
                   datos.RFC ?? string.Empty,
                   datos.correo ?? string.Empty,
                   datos.telefonoCasa ?? string.Empty,
                   datos.telefonoCelular ?? string.Empty,
                   datos.observaciones ?? string.Empty,
                   
                    datos.fechaIngreso !=null? datos.fechaIngreso:DateTime.Now,
                   datos.Usuario ?? string.Empty

                    };
                object aux = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_Catalogo_ac_CatProveedorCombustible", parametros);
                datos.IDProveedor = aux.ToString();

                if (!string.IsNullOrEmpty(datos.IDProveedor))
                {
                    datos.Completado = true;
                }
                else
                {
                    datos.Completado = false;
                }
                return datos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public CatProveedorCombustibleModels ObtenerDetalleCatProveedor(CatProveedorCombustibleModels datos)
        {
            try
            {
                object[] parametros = { datos.IDProveedor };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Catalogo_get_CatProveedorCombustibleXID", parametros);
                while (dr.Read())
                {
                    datos.IDProveedor = dr["id_proveedorCombustible"].ToString();
                   
                    datos.IDSucursal = dr["id_sucursal"].ToString();
                    datos.NombreRazonSocial = dr["nombreRazonSocial"].ToString();
                    datos.RFC = dr["rfc"].ToString();
                   
                    datos.Direccion = dr["direccion"].ToString();
                    datos.correo = dr["correo"].ToString();
                    datos.telefonoCasa = dr["telefonoCasa"].ToString();
                    datos.telefonoCelular = dr["telefonoCelular"].ToString();
                    
                    datos.fechaIngreso = !dr.IsDBNull(dr.GetOrdinal("FechaIngreso")) ? dr.GetDateTime(dr.GetOrdinal("FechaIngreso")) : DateTime.Now;
                    
                    datos.observaciones = !dr.IsDBNull(dr.GetOrdinal("observaciones")) ? dr.GetString(dr.GetOrdinal("observaciones")) : string.Empty;
                    // datos.merma= !dr.IsDBNull(dr.GetOrdinal("merma")) ? dr.GetInt32(dr.GetOrdinal("merma")) : 0;
                }
                dr.Close();
                return datos;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        public CatProveedorCombustibleModels EliminarProveedorCombustible(CatProveedorCombustibleModels datos)
        {
            try
            {
                object[] parametros =
                {
                    datos.IDProveedor, datos.Usuario
                };
                object aux = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_Catalogo_del_CatProveedorCombustible", parametros);
                datos.IDProveedor = aux.ToString();
                if (!string.IsNullOrEmpty(datos.IDProveedor))
                {
                    datos.Completado = true;
                }
                else
                {
                    datos.Completado = false;
                }
                return datos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CuentaBancariaProveedorModels> ObtenerCuentasBancarias(CuentaBancariaProveedorModels Datos)
        {
            try
            {
                List<CuentaBancariaProveedorModels> lista = new List<CuentaBancariaProveedorModels>();
                CuentaBancariaProveedorModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Catalogo_get_CatProveedoresCombustibleDatosBancarios", Datos.IDProveedor);
                while (dr.Read())
                {
                    item = new CuentaBancariaProveedorModels();
                    item.IDDatosBancarios = !dr.IsDBNull(dr.GetOrdinal("IDDatosBancarios")) ? dr.GetString(dr.GetOrdinal("IDDatosBancarios")) : string.Empty;
                    item.Banco.Descripcion = !dr.IsDBNull(dr.GetOrdinal("NombreBanco")) ? dr.GetString(dr.GetOrdinal("NombreBanco")) : string.Empty;
                    item.Titular = !dr.IsDBNull(dr.GetOrdinal("NombreTitular")) ? dr.GetString(dr.GetOrdinal("NombreTitular")) : string.Empty;
                    item.NumTarjeta = !dr.IsDBNull(dr.GetOrdinal("NumeroTarjeta")) ? dr.GetString(dr.GetOrdinal("NumeroTarjeta")) : string.Empty;
                    item.NumCuenta = !dr.IsDBNull(dr.GetOrdinal("NumeroCuenta")) ? dr.GetString(dr.GetOrdinal("NumeroCuenta")) : string.Empty;
                    item.Clabe = !dr.IsDBNull(dr.GetOrdinal("ClaveInterbancaria")) ? dr.GetString(dr.GetOrdinal("ClaveInterbancaria")) : string.Empty;
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
        public void ACDatosBancariosProveedor(CuentaBancariaProveedorModels datos)
        {
            try
            {
                object[] parametros = { datos.Opcion,
                                        datos.IDDatosBancarios ?? string.Empty,
                                        datos.IDProveedor ?? string.Empty,
                                        datos.IDBanco,
                                        datos.Titular ?? string.Empty,
                                        datos.NumCuenta ?? string.Empty,
                                        datos.NumTarjeta ?? string.Empty,
                                        datos.Clabe ?? string.Empty,
                                        datos.Usuario ?? string.Empty};
                object result = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_Catalogos_ac_DatosBancariosProveedorCombustible", parametros);
                if (result != null)
                {
                    if (!string.IsNullOrEmpty(result.ToString()))
                    {
                        datos.Completado = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CatBancoModels> ObteneComboCatBancos(CuentaBancariaProveedorModels Datos)
        {
            try
            {
                List<CatBancoModels> lista = new List<CatBancoModels>();
                CatBancoModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_combo_get_CatBancos");
                while (dr.Read())
                {
                    item = new CatBancoModels();
                    item.IDBanco = !dr.IsDBNull(dr.GetOrdinal("IDBanco")) ? dr.GetInt32(dr.GetOrdinal("IDBanco")) : 0;
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
        public CuentaBancariaProveedorModels ObtenerDetalleCuentaBancaria(CuentaBancariaProveedorModels datos)
        {
            try
            {
                object[] parametros = { datos.IDDatosBancarios, datos.IDProveedor };
                SqlDataReader Dr = null;
                Dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Catalogo_get_CatProveedorCombustibleDatosBancoXID", parametros);
                while (Dr.Read())
                {
                    datos.IDBanco = !Dr.IsDBNull(Dr.GetOrdinal("Banco")) ? Dr.GetInt32(Dr.GetOrdinal("Banco")) : -1;
                    datos.Titular = !Dr.IsDBNull(Dr.GetOrdinal("Titular")) ? Dr.GetString(Dr.GetOrdinal("Titular")) : string.Empty;
                    datos.NumTarjeta = !Dr.IsDBNull(Dr.GetOrdinal("NumTarjeta")) ? Dr.GetString(Dr.GetOrdinal("NumTarjeta")) : string.Empty;
                    datos.NumCuenta = !Dr.IsDBNull(Dr.GetOrdinal("NumCuenta")) ? Dr.GetString(Dr.GetOrdinal("NumCuenta")) : string.Empty;
                    datos.Clabe = !Dr.IsDBNull(Dr.GetOrdinal("Clabe")) ? Dr.GetString(Dr.GetOrdinal("Clabe")) : string.Empty;
                    datos.Completado = true;
                }
                Dr.Close();
                return datos;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        public CuentaBancariaProveedorModels EliminarDatosBancariosProveedor(CuentaBancariaProveedorModels datos)
        {
            try
            {
                object[] parametros =
                {
                    datos.IDDatosBancarios, datos.Usuario
                };
                object aux = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_shared_del_CuentaBancariaXID", parametros);
                if (aux != null)
                {
                    int Resultado = 0;
                    int.TryParse(aux.ToString(), out Resultado);
                    if (Resultado == 1)
                    {
                        datos.Completado = true;
                    }
                }
                return datos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CatContactosModels> obtenerDatosContacto(CatProveedorCombustibleModels datos)
        {
            try
            {
                List<CatContactosModels> lista = new List<CatContactosModels>();
                CatContactosModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Catalogo_get_DatosContactoXProveedorCombustible", datos.IDProveedor);
                while (dr.Read())
                {
                    item = new CatContactosModels();

                    item.IDContacto = dr["id_contacto"].ToString();
                    item.nombreContacto = dr["nombre"].ToString();
                    item.correo = dr["correoElectronico"].ToString();
                    item.telefonoContacto = dr["telefonoContacto"].ToString();
                    item.celularContacto = !dr.IsDBNull(dr.GetOrdinal("celularContacto")) ? dr.GetString(dr.GetOrdinal("celularContacto")) : string.Empty;
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
        public CatContactosModels AcContactoProveedor(CatContactosModels datos)
        {
            try
            {
                object[] parametros =
                {
                   datos.Opcion,
                   datos.IDProveedor ?? string.Empty,
                   datos.IDContacto ?? string.Empty,
                   datos.IDSucursal ?? string.Empty,
                   datos.nombreContacto ?? string.Empty,
                   datos.apPaterno ?? string.Empty,
                   datos.apMaterno?? string.Empty,
                   datos.correo ?? string.Empty,
                   datos.celularContacto ?? string.Empty,
                   datos.telefonoContacto ?? string.Empty,
                   datos.observacion ?? string.Empty,
                    datos.direccion ?? string.Empty,
                   datos.Usuario ?? string.Empty

                    };
                object aux = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_Catalogo_ac_CatContactoXproveedorCombustible", parametros);
                datos.IDContacto = aux.ToString();

                if (!string.IsNullOrEmpty(datos.IDContacto))
                {
                    datos.Completado = true;
                }
                else
                {
                    datos.Completado = false;
                }
                return datos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public CatContactosModels ObtenerDetalleCatDatosXProveedor(CatContactosModels datos)
        {
            try
            {
                object[] parametros = { datos.IDProveedor, datos.IDContacto };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Catalogo_get_DatosContactoXProveedorCombustibleXID", parametros);
                while (dr.Read())
                {
                    datos.IDContacto = dr["id_contacto"].ToString();

                    datos.nombreContacto = dr["nombreContacto"].ToString();
                    datos.apMaterno = dr["apellidomaterno"].ToString();
                    datos.apPaterno = dr["apellidoPaterno"].ToString();

                    datos.telefonoContacto = dr["telefonoContacto"].ToString();
                    datos.correo = dr["correoElectronico"].ToString();
                    datos.celularContacto = dr["celularContacto"].ToString();
                    datos.direccion = dr["direccion"].ToString();

                    datos.observacion = !dr.IsDBNull(dr.GetOrdinal("observacion")) ? dr.GetString(dr.GetOrdinal("observacion")) : string.Empty;
                }
                dr.Close();
                return datos;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        public CatContactosModels EliminarProveedorDatosContacto(CatContactosModels datos)
        {
            try
            {
                object[] parametros =
                {
                    datos.IDContacto, datos.Usuario
                };
                object aux = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_shared_del_CatContactoXID", parametros);
                if (aux != null)
                {
                    int Resultado = 0;
                    int.TryParse(aux.ToString(), out Resultado);
                    if (Resultado == 1)
                    {
                        datos.Completado = true;
                    }
                }
                return datos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}