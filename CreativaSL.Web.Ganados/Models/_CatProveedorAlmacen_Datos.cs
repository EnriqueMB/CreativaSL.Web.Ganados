using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Xml;
using System.Linq;

namespace CreativaSL.Web.Ganados.Models
{
    public class _CatProveedorAlmacen_Datos
    {
        public CatProveedorAlmacenModels AbcCatProveedorAlmacen(CatProveedorAlmacenModels datos)
        {
            try
            {
                object[] parametros = { datos.Opcion,
                            datos.IDProveedorAlmacen ?? string.Empty,
                            datos.IDSucursal ?? string.Empty,
                            datos.RazonSocial ?? string.Empty,
                            datos.RFC ?? string.Empty,
                            datos.TelefonoCelular ?? string.Empty,
                            datos.TelefonoCasa ?? string.Empty,
                            datos.Correo ?? string.Empty,
                            datos.FechaIngreso != null ? datos.FechaIngreso : DateTime.Today,
                            datos.Direccion ?? string.Empty,
                            datos.Observaciones ?? string.Empty,
                            datos.Usuario ?? string.Empty };
                object aux = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_Catalogo_ac_CatProveedorAlmacen", parametros);
                datos.IDProveedorAlmacen =aux.ToString();
                if (!string.IsNullOrEmpty(datos.IDProveedorAlmacen))
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
        public CatProveedorAlmacenModels EliminarProveedorAlmacen(CatProveedorAlmacenModels datos)
        {
            try
            {
                object[] parametros =
                {
                    datos.IDProveedorAlmacen, datos.Usuario
                };
                object aux = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_Catalogo_del_CatProveedorAlmacen", parametros);
                datos.IDProveedorAlmacen = aux.ToString();
                if (!string.IsNullOrEmpty(datos.IDProveedorAlmacen))
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
        public CatProveedorAlmacenModels ObtenerDetalleProveedorAlmacenxID(CatProveedorAlmacenModels datos)
        {
            try
            {
                object[] parametros = { datos.IDProveedorAlmacen };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Catalogo_get_CatProveedorAlmacenXID", parametros);
                while (dr.Read())
                {
                    datos.IDProveedorAlmacen = !dr.IsDBNull(dr.GetOrdinal("IDProveedor")) ? dr.GetString(dr.GetOrdinal("IDProveedor")) : string.Empty;// Convert.ToInt32(dr["ID_UnidadMedida"].ToString());
                    datos.IDSucursal = !dr.IsDBNull(dr.GetOrdinal("IDSucursal")) ? dr.GetString(dr.GetOrdinal("IDSucursal")) : string.Empty;
                    datos.RazonSocial = !dr.IsDBNull(dr.GetOrdinal("NombreSucursal")) ? dr.GetString(dr.GetOrdinal("NombreSucursal")) : string.Empty;
                    datos.RFC = !dr.IsDBNull(dr.GetOrdinal("Rfc")) ? dr.GetString(dr.GetOrdinal("Rfc")) : string.Empty;
                    datos.TelefonoCelular = !dr.IsDBNull(dr.GetOrdinal("TelefonoCelular")) ? dr.GetString(dr.GetOrdinal("TelefonoCelular")) : string.Empty;
                    datos.TelefonoCasa = !dr.IsDBNull(dr.GetOrdinal("TelefonoCasa")) ? dr.GetString(dr.GetOrdinal("TelefonoCasa")) : string.Empty;
                    datos.Correo = !dr.IsDBNull(dr.GetOrdinal("Correo")) ? dr.GetString(dr.GetOrdinal("Correo")) : string.Empty;
                    datos.FechaIngreso = !dr.IsDBNull(dr.GetOrdinal("FechaIngreso")) ? dr.GetDateTime(dr.GetOrdinal("FechaIngreso")) : DateTime.Today;
                    datos.Direccion = !dr.IsDBNull(dr.GetOrdinal("Direccion")) ? dr.GetString(dr.GetOrdinal("Direccion")) : string.Empty;
                    datos.Observaciones = !dr.IsDBNull(dr.GetOrdinal("Obsevaciones")) ? dr.GetString(dr.GetOrdinal("Obsevaciones")) : string.Empty;
                }
                return datos;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        public CatProveedorAlmacenModels ObtenerListaProveedorAlmacen(CatProveedorAlmacenModels datos)
        {
            try
            {
                List<CatProveedorAlmacenModels> Lista = new List<CatProveedorAlmacenModels>();
                CatProveedorAlmacenModels Item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Catalogo_get_CatProveedorAlmacen");
                while (dr.Read())
                {
                    Item = new CatProveedorAlmacenModels();
                    Item.IDProveedorAlmacen = !dr.IsDBNull(dr.GetOrdinal("IDProveedor")) ? dr.GetString(dr.GetOrdinal("IDProveedor")) : string.Empty;
                    Item.RazonSocial = !dr.IsDBNull(dr.GetOrdinal("RazonSocial")) ? dr.GetString(dr.GetOrdinal("RazonSocial")) : string.Empty;
                    Item.Direccion = !dr.IsDBNull(dr.GetOrdinal("Direccion")) ? dr.GetString(dr.GetOrdinal("Direccion")) : string.Empty;
                    Item.RFC= !dr.IsDBNull(dr.GetOrdinal("Rfc")) ? dr.GetString(dr.GetOrdinal("Rfc")) : string.Empty;
                    Item.Correo = !dr.IsDBNull(dr.GetOrdinal("Correo")) ? dr.GetString(dr.GetOrdinal("Correo")) : string.Empty;
                    Item.NombreSucursal = !dr.IsDBNull(dr.GetOrdinal("NombreSucursal")) ? dr.GetString(dr.GetOrdinal("NombreSucursal")) : string.Empty;
                    Lista.Add(Item);
                }
                datos.LProveedorA= Lista;
                return datos;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Cuentas Bancarias Proveedor Almacen

        public List<CuentaBancariasProveedorAlmacenModels> ObtenerCuentasBancarias(CuentaBancariasProveedorAlmacenModels Datos)
        {
            try
            {
                List<CuentaBancariasProveedorAlmacenModels> lista = new List<CuentaBancariasProveedorAlmacenModels>();
                CuentaBancariasProveedorAlmacenModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Catalogo_get_CatProveedorAlmacenDatosBancarios", Datos.IDProveedorAlmacen);
                while (dr.Read())
                {
                    item = new CuentaBancariasProveedorAlmacenModels();
                    item.IDDatosBancarios = !dr.IsDBNull(dr.GetOrdinal("IDDatosBancarios")) ? dr.GetString(dr.GetOrdinal("IDDatosBancarios")) : string.Empty;
                    item.Banco.Descripcion = !dr.IsDBNull(dr.GetOrdinal("NombreBanco")) ? dr.GetString(dr.GetOrdinal("NombreBanco")) : string.Empty;
                    item.Titular = !dr.IsDBNull(dr.GetOrdinal("NombreTitular")) ? dr.GetString(dr.GetOrdinal("NombreTitular")) : string.Empty;
                    item.NumTarjeta = !dr.IsDBNull(dr.GetOrdinal("NumeroTarjeta")) ? dr.GetString(dr.GetOrdinal("NumeroTarjeta")) : string.Empty;
                    item.NumCuenta = !dr.IsDBNull(dr.GetOrdinal("NumeroCuenta")) ? dr.GetString(dr.GetOrdinal("NumeroCuenta")) : string.Empty;
                    item.ClabeInterbancaria = !dr.IsDBNull(dr.GetOrdinal("ClaveInterbancaria")) ? dr.GetString(dr.GetOrdinal("ClaveInterbancaria")) : string.Empty;
                    lista.Add(item);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CatBancoModels> ObteneComboCatBancos(CuentaBancariasProveedorAlmacenModels Datos)
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
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ACDatosBancariosProveedorAlmacen(CuentaBancariasProveedorAlmacenModels datos)
        {
            try
            {
                object[] parametros = { datos.Opcion,
                                        datos.IDDatosBancarios ?? string.Empty,
                                        datos.IDProveedorAlmacen ?? string.Empty,
                                        datos.IDBanco,
                                        datos.Titular ?? string.Empty,
                                        datos.NumCuenta ?? string.Empty,
                                        datos.NumTarjeta ?? string.Empty,
                                        datos.ClabeInterbancaria ?? string.Empty,
                                        datos.Usuario ?? string.Empty};
                object result = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_Catalogos_ac_DatosBancariosProveedorAlmacen", parametros);
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

        public CuentaBancariasProveedorAlmacenModels ObtenerDetalleCuentaBancaria(CuentaBancariasProveedorAlmacenModels datos)
        {
            try
            {
                object[] parametros = { datos.IDDatosBancarios, datos.IDProveedorAlmacen };
                SqlDataReader Dr = null;
                Dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Catalogo_get_CatProveedorAlmacenDatosBancoXID", parametros);
                while (Dr.Read())
                {
                    datos.IDBanco = !Dr.IsDBNull(Dr.GetOrdinal("Banco")) ? Dr.GetInt32(Dr.GetOrdinal("Banco")) : -1;
                    datos.Titular = !Dr.IsDBNull(Dr.GetOrdinal("Titular")) ? Dr.GetString(Dr.GetOrdinal("Titular")) : string.Empty;
                    datos.NumTarjeta = !Dr.IsDBNull(Dr.GetOrdinal("NumTarjeta")) ? Dr.GetString(Dr.GetOrdinal("NumTarjeta")) : string.Empty;
                    datos.NumCuenta = !Dr.IsDBNull(Dr.GetOrdinal("NumCuenta")) ? Dr.GetString(Dr.GetOrdinal("NumCuenta")) : string.Empty;
                    datos.ClabeInterbancaria = !Dr.IsDBNull(Dr.GetOrdinal("Clabe")) ? Dr.GetString(Dr.GetOrdinal("Clabe")) : string.Empty;
                    datos.Completado = true;
                }
                return datos;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CuentaBancariasProveedorAlmacenModels EliminarDatosBancariosProveedorAlmacen(CuentaBancariasProveedorAlmacenModels datos)
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

        #endregion

        #region Contacto Proveedor Almacen

        public List<CatContactosModels> ObtenerdatosContactosProveedor(CatContactosModels Datos)
        {
            try
            {
                List<CatContactosModels> lista = new List<CatContactosModels>();
                CatContactosModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Catalogo_get_CatProveedorAlmacenCatContacto", Datos.IDProveedor);
                while (dr.Read())
                {
                    item = new CatContactosModels();
                    item.IDContacto = !dr.IsDBNull(dr.GetOrdinal("IDContacto")) ? dr.GetString(dr.GetOrdinal("IDContacto")) : string.Empty;
                    item.nombreContacto = !dr.IsDBNull(dr.GetOrdinal("NombreContacto")) ? dr.GetString(dr.GetOrdinal("NombreContacto")) : string.Empty;
                    item.correo = !dr.IsDBNull(dr.GetOrdinal("Correo")) ? dr.GetString(dr.GetOrdinal("Correo")) : string.Empty;
                    item.direccion = !dr.IsDBNull(dr.GetOrdinal("Direccion")) ? dr.GetString(dr.GetOrdinal("Direccion")) : string.Empty;
                    item.telefonoContacto = !dr.IsDBNull(dr.GetOrdinal("TelefonoCasa")) ? dr.GetString(dr.GetOrdinal("TelefonoCasa")) : string.Empty;
                    item.celularContacto = !dr.IsDBNull(dr.GetOrdinal("TelefonoCelular")) ? dr.GetString(dr.GetOrdinal("TelefonoCelular")) : string.Empty;
                    lista.Add(item);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ACDatosContactoProveedorAlmacen(CatContactosModels datos)
        {
            try
            {
                object[] parametros = { datos.Opcion,
                                        datos.IDContacto ?? string.Empty,
                                        datos.IDProveedor ?? string.Empty,
                                        datos.nombreContacto ?? string.Empty,
                                        datos.apMaterno ?? string.Empty,
                                        datos.apPaterno ?? string.Empty,
                                        datos.correo ?? string.Empty,
                                        datos.telefonoContacto ?? string.Empty,
                                        datos.celularContacto ?? string.Empty,
                                        datos.direccion ?? string.Empty,
                                        datos.observacion ?? string.Empty,
                                        datos.Usuario ?? string.Empty};
                object result = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_Catalogos_ac_CatContactoProveedorAlmacen", parametros);
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

        public CatContactosModels ObtenerDetalleCatcontactoProveedor(CatContactosModels datos)
        {
            try
            {
                object[] parametros = { datos.IDContacto, datos.IDProveedor };
                SqlDataReader Dr = null;
                Dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Catalogo_get_CatProveedorAlmacenCatContactoXID", parametros);
                while (Dr.Read())
                {
                    datos.IDContacto = !Dr.IsDBNull(Dr.GetOrdinal("IDContacto")) ? Dr.GetString(Dr.GetOrdinal("IDContacto")) : string.Empty;
                    datos.nombreContacto = !Dr.IsDBNull(Dr.GetOrdinal("NombreContacto")) ? Dr.GetString(Dr.GetOrdinal("NombreContacto")) : string.Empty;
                    datos.apMaterno = !Dr.IsDBNull(Dr.GetOrdinal("ApMaterno")) ? Dr.GetString(Dr.GetOrdinal("ApMaterno")) : string.Empty;
                    datos.apPaterno = !Dr.IsDBNull(Dr.GetOrdinal("ApPaterno")) ? Dr.GetString(Dr.GetOrdinal("ApPaterno")) : string.Empty;
                    datos.correo = !Dr.IsDBNull(Dr.GetOrdinal("Correo")) ? Dr.GetString(Dr.GetOrdinal("Correo")) : string.Empty;
                    datos.telefonoContacto = !Dr.IsDBNull(Dr.GetOrdinal("TelefonoContacto")) ? Dr.GetString(Dr.GetOrdinal("TelefonoContacto")) : string.Empty;
                    datos.celularContacto = !Dr.IsDBNull(Dr.GetOrdinal("TelefonoCelular")) ? Dr.GetString(Dr.GetOrdinal("TelefonoCelular")) : string.Empty;
                    datos.direccion = !Dr.IsDBNull(Dr.GetOrdinal("Direccion")) ? Dr.GetString(Dr.GetOrdinal("Direccion")) : string.Empty;
                    datos.observacion = !Dr.IsDBNull(Dr.GetOrdinal("Observaciones")) ? Dr.GetString(Dr.GetOrdinal("Observaciones")) : string.Empty;
                    datos.Completado = true;
                }
                return datos;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CatContactosModels EliminarDatoContactoProveedorAlmacen(CatContactosModels datos)
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

        #endregion
    }
}