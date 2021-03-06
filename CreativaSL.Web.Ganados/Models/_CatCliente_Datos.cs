﻿using Microsoft.ApplicationBlocks.Data;
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
                dr.Close();
                datos.ListaClientes = Lista;
                return datos;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ClienteLugarModels ObtenerLugares(ClienteLugarModels datos)
        {
            try
            {
                List<ClienteLugarModels> Lista = new List<ClienteLugarModels>();
                ClienteLugarModels Item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Catalogo_get_ClientesLugar",datos.IDCliente,datos.IDSucursal);
                while (dr.Read())
                {
                    Item = new ClienteLugarModels();
                    Item.IDClienteLugar = !dr.IsDBNull(dr.GetOrdinal("id_clienteLugar")) ? dr.GetString(dr.GetOrdinal("id_clienteLugar")) : string.Empty;
                    Item.nombreCliente = !dr.IsDBNull(dr.GetOrdinal("nombreContacto")) ? dr.GetString(dr.GetOrdinal("nombreContacto")) : string.Empty;
                    Item.descripcionLugar = !dr.IsDBNull(dr.GetOrdinal("descripcion")) ? dr.GetString(dr.GetOrdinal("descripcion")) : string.Empty;
                    Item.nombreLugar = !dr.IsDBNull(dr.GetOrdinal("nombrePropietario")) ? dr.GetString(dr.GetOrdinal("nombrePropietario")) : string.Empty;
                    
                    Lista.Add(Item);
                }
                dr.Close();
                datos.ListaClienteLugar = Lista;
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
                    datos.Usuario ?? string.Empty,
                    datos.PSGCliente ?? string.Empty ,
                    datos.Tolerancia ,
                    datos.TipoCliente,
                    datos.TodaSucursale
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

        public CatContactosModels AcContactoCliente(CatContactosModels datos)
        {
            try
            {
                object[] parametros = {
                    datos.Opcion,
                    datos.IDContacto ?? string.Empty,
                    datos.IDCliente ?? string.Empty,
                    datos.nombreContacto ?? string.Empty,
                    datos.apMaterno ?? string.Empty,
                    datos.apPaterno ?? string.Empty,
                    datos.correo ?? string.Empty,
                    datos.telefonoContacto ?? string.Empty,
                    datos.celularContacto ?? string.Empty,
                    datos.direccion ?? string.Empty,
                    datos.observacion ?? string.Empty,
                    datos.Usuario ?? string.Empty
                };
                object aux = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_Catalogos_ac_ContactoCliente", parametros);
                datos.IDContacto = aux.ToString();
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

        public CatContactosModels EliminarContactoCliente(CatContactosModels datos)
        {
            try
            {
                object[] parametros =
                {
                    datos.IDContacto, datos.Usuario
                };
                object aux = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_Catalogo_del_CatContactos", parametros);
                datos.IDContacto = aux.ToString();
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

        public ClienteLugarModels EliminarLugarCliente(ClienteLugarModels datos)
        {
            try
            {
                object[] parametros =
                {
                    datos.IDClienteLugar,  datos.Usuario
                };
                object aux = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_Catalogo_del_ClienteLugar", parametros);
                datos.IDClienteLugar = aux.ToString();
                if (!string.IsNullOrEmpty(datos.IDClienteLugar))
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
        public List<CatLugarModels> obtenerLugaresClientes(ClienteLugarModels Datos)
        {
            try
            {
                List<CatLugarModels> lista = new List<CatLugarModels>();
                CatLugarModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Combo_get_CatLugarCliente", Datos.IDSucursal,Datos.IDCliente);
                while (dr.Read())
                {
                    item = new CatLugarModels();
                    item.id_lugar = !dr.IsDBNull(dr.GetOrdinal("IDLugar")) ? dr.GetString(dr.GetOrdinal("IDLugar")) : string.Empty;
                    item.descripcion = !dr.IsDBNull(dr.GetOrdinal("NombreLugar")) ? dr.GetString(dr.GetOrdinal("NombreLugar")) : string.Empty;
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
        public List<CatSucursalesModels> ObteneComboCatSucursal(CatClienteModels Datos)
        {
            try
            {
                List<CatSucursalesModels> lista = new List<CatSucursalesModels>();
                CatSucursalesModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Combo_get_CatSucursal");
                while (dr.Read())
                {
                    item = new CatSucursalesModels();
                    item.IDSucursal = !dr.IsDBNull(dr.GetOrdinal("IDSucursal")) ? dr.GetString(dr.GetOrdinal("IDSucursal")) : string.Empty;
                    item.NombreSucursal = !dr.IsDBNull(dr.GetOrdinal("NombreSucursal")) ? dr.GetString(dr.GetOrdinal("NombreSucursal")) : string.Empty;
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
                dr.Close();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CatTipoClienteModels> ObtenerListaTipoClientes(CatClienteModels Datos)
        {
            try
            {
                List<CatTipoClienteModels> lista = new List<CatTipoClienteModels>();
                CatTipoClienteModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Combo_get_CatTipoCliente");
                while (dr.Read())
                {
                    item = new CatTipoClienteModels();
                    item.Id_tipoCliente = !dr.IsDBNull(dr.GetOrdinal("id_tipoCliente")) ? dr.GetInt32(dr.GetOrdinal("id_tipoCliente")) : 0;
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
                    datos.PSGCliente = !dr.IsDBNull(dr.GetOrdinal("psgCliente")) ? dr.GetString(dr.GetOrdinal("psgCliente")) : string.Empty;
                    datos.Tolerancia = !dr.IsDBNull(dr.GetOrdinal("tolerancia")) ? dr.GetDecimal(dr.GetOrdinal("tolerancia")) : 0;
                    datos.TipoCliente = !dr.IsDBNull(dr.GetOrdinal("id_tipoCliente")) ? dr.GetInt32(dr.GetOrdinal("id_tipoCliente")) : 0;
                }
                dr.Close();
                return datos;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        public CatContactosModels ObtenerDetalleContactoCliente(CatContactosModels datos)
        {
            try
            {
                object[] parametros = { datos.IDContacto };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Catalogo_get_CatContactosClienteDetalle", parametros);
                while (dr.Read())
                {
                    datos.IDCliente = !dr.IsDBNull(dr.GetOrdinal("IDCliente")) ? dr.GetString(dr.GetOrdinal("IDCliente")) : string.Empty;
                    datos.IDContacto = !dr.IsDBNull(dr.GetOrdinal("IDContacto")) ? dr.GetString(dr.GetOrdinal("IDContacto")) : string.Empty;
                    datos.nombreContacto = !dr.IsDBNull(dr.GetOrdinal("NombreContacto")) ? dr.GetString(dr.GetOrdinal("NombreContacto")) : string.Empty;
                    datos.apPaterno = !dr.IsDBNull(dr.GetOrdinal("ApellidoPaterno")) ? dr.GetString(dr.GetOrdinal("ApellidoPaterno")) : string.Empty;
                    datos.apMaterno = !dr.IsDBNull(dr.GetOrdinal("ApellidoMaterno")) ? dr.GetString(dr.GetOrdinal("ApellidoMaterno")) : string.Empty;
                    datos.correo = !dr.IsDBNull(dr.GetOrdinal("CorreoElectronico")) ? dr.GetString(dr.GetOrdinal("CorreoElectronico")) : string.Empty;
                    datos.telefonoContacto = !dr.IsDBNull(dr.GetOrdinal("TelefonoContacto")) ? dr.GetString(dr.GetOrdinal("TelefonoContacto")) : string.Empty;
                    datos.celularContacto = !dr.IsDBNull(dr.GetOrdinal("CelularContacto")) ? dr.GetString(dr.GetOrdinal("CelularContacto")) : string.Empty;
                    datos.direccion = !dr.IsDBNull(dr.GetOrdinal("Direccion")) ? dr.GetString(dr.GetOrdinal("Direccion")) : string.Empty;
                    datos.observacion = !dr.IsDBNull(dr.GetOrdinal("Observacion")) ? dr.GetString(dr.GetOrdinal("Observacion")) : string.Empty;
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
        public ClienteLugarModels ACLugaresCliente(ClienteLugarModels datos)
        {
            try
            {
                object[] parametros = { datos.Opcion,
                                        datos.IDClienteLugar ?? string.Empty,
                                        datos.IDCliente ?? string.Empty,
                                        datos.IDLugar ?? string.Empty,
                                       
                                        datos.Usuario ?? string.Empty};
                object result = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_Catalogo_ac_ClienteLugar", parametros);
                datos.IDClienteLugar = result.ToString();
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
        public CatContactosModels ObtenerContactosCliente(CatContactosModels datos)
        {
            try
            {
                List<CatContactosModels> Lista = new List<CatContactosModels>();
                CatContactosModels Item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Catalogo_get_CatContactosCliente", datos.IDCliente);
                while (dr.Read())
                {
                    Item = new CatContactosModels();
                    Item.IDContacto = !dr.IsDBNull(dr.GetOrdinal("IDContacto")) ? dr.GetString(dr.GetOrdinal("IDContacto")) : string.Empty;
                    Item.nombreContacto = !dr.IsDBNull(dr.GetOrdinal("NombreContacto")) ? dr.GetString(dr.GetOrdinal("NombreContacto")) : string.Empty;
                    Item.apMaterno = !dr.IsDBNull(dr.GetOrdinal("ApellidoMaterno")) ? dr.GetString(dr.GetOrdinal("ApellidoMaterno")) : string.Empty;
                    Item.apPaterno = !dr.IsDBNull(dr.GetOrdinal("ApellidoPaterno")) ? dr.GetString(dr.GetOrdinal("ApellidoPaterno")) : string.Empty;
                    Item.correo = !dr.IsDBNull(dr.GetOrdinal("CorreoElectronico")) ? dr.GetString(dr.GetOrdinal("CorreoElectronico")) : string.Empty;
                    Item.telefonoContacto = !dr.IsDBNull(dr.GetOrdinal("TelefonoContacto")) ? dr.GetString(dr.GetOrdinal("TelefonoContacto")) : string.Empty;
                    Item.celularContacto = !dr.IsDBNull(dr.GetOrdinal("CelularContacto")) ? dr.GetString(dr.GetOrdinal("CelularContacto")) : string.Empty;
                    Item.direccion = !dr.IsDBNull(dr.GetOrdinal("Direccion")) ? dr.GetString(dr.GetOrdinal("Direccion")) : string.Empty;
                    Item.observacion = !dr.IsDBNull(dr.GetOrdinal("Observacion")) ? dr.GetString(dr.GetOrdinal("Observacion")) : string.Empty;

                    Lista.Add(Item);
                }
                datos.listaContacto = Lista;
                return datos;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region UPPCliente
        public UPPClienteModels ObtenerUPPCliente(UPPClienteModels Datos)
        {
            try
            {
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_get_UPPCliente", Datos.id_cliente);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Datos.UPP = !dr.IsDBNull(dr.GetOrdinal("UPP")) ? dr.GetString(dr.GetOrdinal("UPP")) : string.Empty;
                        Datos.fechaAlta = !dr.IsDBNull(dr.GetOrdinal("fechaAlta")) ? dr.GetDateTime(dr.GetOrdinal("fechaAlta")) : DateTime.Today;
                        Datos.id_pais = !dr.IsDBNull(dr.GetOrdinal("IDPais")) ? dr.GetString(dr.GetOrdinal("IDPais")) : string.Empty;
                        Datos.id_estadoCodigo = !dr.IsDBNull(dr.GetOrdinal("C_Estado")) ? dr.GetString(dr.GetOrdinal("C_Estado")) : string.Empty;
                        Datos.id_estado = !dr.IsDBNull(dr.GetOrdinal("IDEstado")) ? dr.GetInt32(dr.GetOrdinal("IDEstado")) : 0;
                        Datos.id_municipio = !dr.IsDBNull(dr.GetOrdinal("IDMunicipio")) ? dr.GetInt32(dr.GetOrdinal("IDMunicipio")) : 0;
                        Datos.nombrePredio = !dr.IsDBNull(dr.GetOrdinal("nombrePredio")) ? dr.GetString(dr.GetOrdinal("nombrePredio")) : string.Empty;
                        Datos.propietario = !dr.IsDBNull(dr.GetOrdinal("Propietario")) ? dr.GetString(dr.GetOrdinal("Propietario")) : string.Empty;
                        Datos.Imagen = !dr.IsDBNull(dr.GetOrdinal("imagenUPP")) ? dr.GetString(dr.GetOrdinal("imagenUPP")) : string.Empty;
                        Datos.ImagenServer = !dr.IsDBNull(dr.GetOrdinal("imagenServer")) ? dr.GetInt32(dr.GetOrdinal("imagenServer")) : 0;
                    }
                }
                else
                {
                    Datos.fechaAlta = new DateTime();
                    Datos.fechaAlta = DateTime.Today;
                }
                dr.Close();
                return Datos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public UPPClienteModels CUPPCliente(UPPClienteModels Datos)
        {
            try
            {
                object[] parametros =
                {
                   Datos.Opcion,
                   Datos.id_cliente,
                   Datos.UPP,
                   Datos.fechaAlta,
                   Datos.id_pais,
                   Datos.id_estadoCodigo,
                   Datos.id_municipio,
                   Datos.nombrePredio,
                   Datos.propietario,
                   Datos.Imagen,
                   Datos.BandImg,
                   Datos.Usuario
                    };
                object aux = SqlHelper.ExecuteScalar(Datos.Conexion, "spCSLDB_ac_UPPCliente", parametros);
                Datos.id_cliente = aux.ToString();
                int completo = 0;
                int.TryParse(Datos.id_cliente.ToString(), out completo);
                if (completo == 1)
                {
                    Datos.Completado = true;
                }
                else
                {
                    Datos.Completado = false;
                }
                return Datos;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<CatPaisModels> obtenerListaPaises(UPPClienteModels Datos)
        {
            try
            {
                List<CatPaisModels> lista = new List<CatPaisModels>();
                CatPaisModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_get_ComboPaises");
                while (dr.Read())
                {
                    item = new CatPaisModels();
                    item.id_pais = dr["clave"].ToString();
                    item.descripcion = dr["descripcion"].ToString();

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
        public List<CatEstadoModels> obtenerListaEstados(UPPClienteModels Datos)
        {
            try
            {
                List<CatEstadoModels> lista = new List<CatEstadoModels>();
                CatEstadoModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_get_ComboEstado", Datos.id_pais);
                while (dr.Read())
                {
                    item = new CatEstadoModels();
                    item.id_estado = Convert.ToInt32(dr["id_estado"].ToString());
                    item.codigoEstado = dr["clave"].ToString();
                    item.descripcion = dr["descripcion"].ToString();

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
        public List<CatMunicipioModels> obtenerListaMunicipios(UPPClienteModels Datos)
        {
            try
            {
                List<CatMunicipioModels> lista = new List<CatMunicipioModels>();
                CatMunicipioModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_get_ComboMunicipio", Datos.id_pais, Datos.id_estadoCodigo);
                while (dr.Read())
                {
                    item = new CatMunicipioModels();
                    item.id_municipio = Convert.ToInt32(dr["id_municipio"].ToString());
                    item.codigoMunicipio = dr["clave"].ToString();
                    item.descripcion = dr["descripcion"].ToString();

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
    }
}