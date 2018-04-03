using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CatCliente_Datos
    {
        public CatClienteModels ObtenerClientes(CatClienteModels datos)
        {
            try
            {
                List<CatClienteModels> Lista = new List<CatClienteModels>();
                CatClienteModels Item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Catalogo_get_CatCliente");
                while (dr.Read())
                {
                    Item = new CatClienteModels();
                    Item.IDCliente = !dr.IsDBNull(dr.GetOrdinal("IDCliente")) ? dr.GetString(dr.GetOrdinal("IDCliente")) : string.Empty;
                    Item.NombreRazonSocial = !dr.IsDBNull(dr.GetOrdinal("RazonSocial")) ? dr.GetString(dr.GetOrdinal("RazonSocial")) : string.Empty;
                    Item.EsPersonaFisica = !dr.IsDBNull(dr.GetOrdinal("EsPersona")) ? dr.GetBoolean(dr.GetOrdinal("EsPersona")) : false;
                    Item.RFC = !dr.IsDBNull(dr.GetOrdinal("RFC")) ? dr.GetString(dr.GetOrdinal("RFC")) : string.Empty;
                    Item.NombreSucursal = !dr.IsDBNull(dr.GetOrdinal("NombreSucursal")) ? dr.GetString(dr.GetOrdinal("NombreSucursal")) : string.Empty;
                    Item.NombreRegimenFiscal = !dr.IsDBNull(dr.GetOrdinal("NombreRegimenFiscal")) ? dr.GetString(dr.GetOrdinal("NombreRegimenFiscal")) : string.Empty;
                    Item.IDSucursal = !dr.IsDBNull(dr.GetOrdinal("IDSucursal")) ? dr.GetString(dr.GetOrdinal("IDSucursal")) : string.Empty;
                    Lista.Add(Item);
                }
                datos.ListaClientes = Lista;
                return datos;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CatClienteModels AbcCatClientes(CatClienteModels datos)
        {
            try
            {
                object[] parametros = {
                    datos.Opcion,
                    datos.IDCliente ?? string.Empty,
                    datos.IDSucursal ?? string.Empty,
                    datos.NombreResponsable ?? string.Empty,
                    datos.CorreoElectronico ?? string.Empty,
                    datos.Telefono ?? string.Empty,
                    datos.Celular ?? string.Empty,
                    datos.FechaIngreso != null ? datos.FechaIngreso : DateTime.Today,
                    datos.NombreRazonSocial ?? string.Empty,
                    datos.RFC ?? string.Empty,
                    datos.EsPersonaFisica,
                    datos.Direccion ?? string.Empty,
                    datos.IDRegimenFiscal ?? string.Empty,
                    datos.Usuario ?? string.Empty
                };
                object aux = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_Catalogo_ac_CatCliente", parametros);
                datos.IDCliente = aux.ToString();
                if (!string.IsNullOrEmpty(datos.IDCliente))
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

        public CatClienteModels EliminarCliente(CatClienteModels datos)
        {
            try
            {
                object[] parametros =
                {
                    datos.IDCliente, datos.IDSucursal, datos.Usuario
                };
                object aux = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_Catalogo_del_CatCliente", parametros);
                datos.IDCliente = aux.ToString();
                if (!string.IsNullOrEmpty(datos.IDCliente))
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

        public List<CatSucursalesModels> ObteneComboCatSucursal(CatClienteModels Datos)
        {
            try
            {
                List<CatSucursalesModels> lista = new List<CatSucursalesModels>();
                CatSucursalesModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Combo_get_CatSucursal");
                lista.Add(new CatSucursalesModels { IDSucursal = string.Empty, NombreSucursal=" - Seleccione -" });
                while (dr.Read())
                {
                    item = new CatSucursalesModels();
                    item.IDSucursal = !dr.IsDBNull(dr.GetOrdinal("IDSucursal")) ? dr.GetString(dr.GetOrdinal("IDSucursal")) : string.Empty;
                    item.NombreSucursal = !dr.IsDBNull(dr.GetOrdinal("NombreSucursal")) ? dr.GetString(dr.GetOrdinal("NombreSucursal")) : string.Empty;
                    lista.Add(item);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CFDI_RegimenFiscalModels> ObtenerComboRegimenFiscal(CatClienteModels Datos)
        {
            try
            {
                List<CFDI_RegimenFiscalModels> lista = new List<CFDI_RegimenFiscalModels>();
                CFDI_RegimenFiscalModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Combo_get_cfdiRegimenFiscal", Datos.EsPersonaFisica);
                while (dr.Read())
                {
                    item = new CFDI_RegimenFiscalModels();
                    item.Clave = !dr.IsDBNull(dr.GetOrdinal("Clave")) ? dr.GetString(dr.GetOrdinal("Clave")) : string.Empty;
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

        public CatClienteModels ObtenerDetalleCatCliente(CatClienteModels datos)
        {
            try
            {
                object[] parametros = { datos.IDCliente };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Catalogo_get_CatClienteXID", parametros);
                while (dr.Read())
                {
                    datos.IDCliente = !dr.IsDBNull(dr.GetOrdinal("IDCliente")) ? dr.GetString(dr.GetOrdinal("IDCliente")) : string.Empty;
                    datos.IDSucursal = !dr.IsDBNull(dr.GetOrdinal("IDSucursal")) ? dr.GetString(dr.GetOrdinal("IDSucursal")) : string.Empty;
                    datos.NombreResponsable = !dr.IsDBNull(dr.GetOrdinal("NombreContacto")) ? dr.GetString(dr.GetOrdinal("NombreContacto")) : string.Empty;
                    datos.CorreoElectronico = !dr.IsDBNull(dr.GetOrdinal("CorreoElectronico")) ? dr.GetString(dr.GetOrdinal("CorreoElectronico")) : string.Empty;
                    datos.Telefono = !dr.IsDBNull(dr.GetOrdinal("TelefonoContacto")) ? dr.GetString(dr.GetOrdinal("TelefonoContacto")) : string.Empty;
                    datos.Celular = !dr.IsDBNull(dr.GetOrdinal("CelularContacto")) ? dr.GetString(dr.GetOrdinal("CelularContacto")) : string.Empty;
                    datos.FechaIngreso = !dr.IsDBNull(dr.GetOrdinal("FechaIngreso")) ? dr.GetDateTime(dr.GetOrdinal("FechaIngreso")) : DateTime.Today;
                    datos.NombreRazonSocial = !dr.IsDBNull(dr.GetOrdinal("RazonSocial")) ? dr.GetString(dr.GetOrdinal("RazonSocial")) : string.Empty;
                    datos.RFC = !dr.IsDBNull(dr.GetOrdinal("RFC")) ? dr.GetString(dr.GetOrdinal("RFC")) : string.Empty;
                    datos.EsPersonaFisica = !dr.IsDBNull(dr.GetOrdinal("EsPersona")) ? dr.GetBoolean(dr.GetOrdinal("EsPersona")) : false;
                    datos.Direccion = !dr.IsDBNull(dr.GetOrdinal("Direccion")) ? dr.GetString(dr.GetOrdinal("Direccion")) : string.Empty;
                    datos.IDRegimenFiscal = !dr.IsDBNull(dr.GetOrdinal("IDRegimenFiscal")) ? dr.GetString(dr.GetOrdinal("IDRegimenFiscal")) : string.Empty;
                }
                return datos;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CFDI_RegimenFiscalModels> ObtenerDatosRegimen(CatClienteModels datos)
        {
            try
            {
                List<CFDI_RegimenFiscalModels> lista = new List<CFDI_RegimenFiscalModels>();
                CFDI_RegimenFiscalModels item;
                SqlDataReader dr = null;

                dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Catalogo_get_DatosRegimenFiscal", datos.IDRegimenFiscal);
                while (dr.Read())
                {
                    item = new CFDI_RegimenFiscalModels();
                    item.Fisica = !dr.IsDBNull(dr.GetOrdinal("Fisica")) ? dr.GetBoolean(dr.GetOrdinal("Fisica")) : false;
                    item.Moral = !dr.IsDBNull(dr.GetOrdinal("Moral")) ? dr.GetBoolean(dr.GetOrdinal("Moral")) : false;
                    item.FechaInicio = !dr.IsDBNull(dr.GetOrdinal("FechaInicioVigencia")) ? dr.GetString(dr.GetOrdinal("FechaInicioVigencia")) : string.Empty;
                    item.FechaFin = !dr.IsDBNull(dr.GetOrdinal("FechaFinVigencia")) ? dr.GetString(dr.GetOrdinal("FechaFinVigencia")) : string.Empty;
                    lista.Add(item);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CuentaBancariaModels> ObtenerCuentasXIDCliente(CuentaBancariaModels datos)
        {
            try
            {
                List<CuentaBancariaModels> Lista = new List<CuentaBancariaModels>();
                SqlDataReader Dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_shared_get_CuentasBancXIDCliente", datos.Cliente.IDCliente);
                CuentaBancariaModels Item;
                while(Dr.Read())
                {
                    Item = new CuentaBancariaModels();
                    Item.IDDatosBancarios = !Dr.IsDBNull(Dr.GetOrdinal("IDDatosBancarios")) ? Dr.GetString(Dr.GetOrdinal("IDDatosBancarios")) : string.Empty;
                    Item.Banco.Descripcion = !Dr.IsDBNull(Dr.GetOrdinal("Banco")) ? Dr.GetString(Dr.GetOrdinal("Banco")) : string.Empty;
                    Item.Titular = !Dr.IsDBNull(Dr.GetOrdinal("Titular")) ? Dr.GetString(Dr.GetOrdinal("Titular")) : string.Empty;
                    Item.NumTarjeta = !Dr.IsDBNull(Dr.GetOrdinal("NumTarjeta")) ? Dr.GetString(Dr.GetOrdinal("NumTarjeta")) : string.Empty;
                    Item.NumCuenta = !Dr.IsDBNull(Dr.GetOrdinal("NumCuenta")) ? Dr.GetString(Dr.GetOrdinal("NumCuenta")) : string.Empty;
                    Item.Clabe = !Dr.IsDBNull(Dr.GetOrdinal("Clabe")) ? Dr.GetString(Dr.GetOrdinal("Clabe")) : string.Empty;
                    Lista.Add(Item);
                }
                return Lista;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<CatBancoModels> ObtenerComboCatBancos(string Conexion)
        {
            try
            {
                List<CatBancoModels> Lista = new List<CatBancoModels>();
                SqlDataReader Dr = SqlHelper.ExecuteReader(Conexion, "spCSLDB_combo_get_CatBancos");
                CatBancoModels Item;
                while(Dr.Read())
                {
                    Item = new CatBancoModels();
                    Item.IDBanco = !Dr.IsDBNull(Dr.GetOrdinal("IDBanco")) ? Dr.GetInt32(Dr.GetOrdinal("IDBanco")) : -1;
                    Item.Descripcion = !Dr.IsDBNull(Dr.GetOrdinal("Descripcion")) ? Dr.GetString(Dr.GetOrdinal("Descripcion")) : string.Empty;
                    Lista.Add(Item);
                }
                return Lista;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void ACDatosBancariosCliente( CuentaBancariaModels datos)
        {
            try
            {
                object[] parametros = { datos.NuevoRegistro,
                                        datos.IDDatosBancarios ?? string.Empty,
                                        datos.Cliente.IDCliente ?? string.Empty,
                                        datos.Banco.IDBanco,
                                        datos.Titular ?? string.Empty,
                                        datos.NumCuenta ?? string.Empty,
                                        datos.NumTarjeta ?? string.Empty,
                                        datos.Clabe ?? string.Empty,
                                        datos.Usuario ?? string.Empty};
                object result = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_catalogos_ac_DatosBancariosCliente", parametros);
                if(result != null)
                {
                    if(!string.IsNullOrEmpty(result.ToString()))
                    {
                        datos.Completado = true;
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void ObtenerDetalleDatosBancariosCliente(CuentaBancariaModels datos)
        {
            try
            {
                SqlDataReader Dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_shared_get_DetalleCuentasBancXID", datos.IDDatosBancarios, datos.Cliente.IDCliente);
                while (Dr.Read())
                {
                    datos.Banco.IDBanco = !Dr.IsDBNull(Dr.GetOrdinal("Banco")) ? Dr.GetInt32(Dr.GetOrdinal("Banco")) : -1;
                    datos.Titular = !Dr.IsDBNull(Dr.GetOrdinal("Titular")) ? Dr.GetString(Dr.GetOrdinal("Titular")) : string.Empty;
                    datos.NumTarjeta = !Dr.IsDBNull(Dr.GetOrdinal("NumTarjeta")) ? Dr.GetString(Dr.GetOrdinal("NumTarjeta")) : string.Empty;
                    datos.NumCuenta = !Dr.IsDBNull(Dr.GetOrdinal("NumCuenta")) ? Dr.GetString(Dr.GetOrdinal("NumCuenta")) : string.Empty;
                    datos.Clabe = !Dr.IsDBNull(Dr.GetOrdinal("Clabe")) ? Dr.GetString(Dr.GetOrdinal("Clabe")) : string.Empty;
                    datos.Completado = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
                
        public CuentaBancariaModels EliminarDatosBancarios(CuentaBancariaModels datos)
        {
            try
            {
                object[] parametros =
                {
                    datos.IDDatosBancarios, datos.Usuario
                };
                object aux = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_shared_del_CuentaBancariaXID", parametros);
                if(aux!= null)
                {
                    int Resultado = 0;
                    int.TryParse(aux.ToString(), out Resultado);
                    if(Resultado == 1)
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