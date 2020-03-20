using CreativaSL.Web.Ganados.Models.Datatable;
using CreativaSL.Web.Ganados.Models.Dto.Base;
using CreativaSL.Web.Ganados.Models.Dto.Cliente;
using CreativaSL.Web.Ganados.Models.System;
using Microsoft.ApplicationBlocks.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CatCliente_Datos:BaseSQL
    {
       // public string ConexionSql { get; private set; }

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
                    datos.FotoPerfil = !dr.IsDBNull(dr.GetOrdinal("UrlFotoPerfil")) ? dr.GetString(dr.GetOrdinal("UrlFotoPerfil")) : string.Empty;

                    datos.FotoPerfil =
                        Auxiliar.ValidImageFormServer(datos.FotoPerfil, ProjectSettings.BaseDirClienteFotoPerfil);
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
                    Item.FotoCuenta = !Dr.IsDBNull(Dr.GetOrdinal("FotoCuenta")) ? Dr.GetString(Dr.GetOrdinal("FotoCuenta")) : string.Empty;
                    Item.Clabe = !Dr.IsDBNull(Dr.GetOrdinal("Clabe")) ? Dr.GetString(Dr.GetOrdinal("Clabe")) : string.Empty;

                    Item.FotoCuenta = Auxiliar.ValidImageFormServer(Item.FotoCuenta,
                        ProjectSettings.BaseDirClienteCuentasBancarias);

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
                SqlDataReader dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_catalogos_ac_DatosBancariosCliente", parametros);
                while (dr.Read())
                {
                    if(datos.NuevoRegistro)
                   datos.IDDatosBancarios= !dr.IsDBNull(dr.GetOrdinal("IDDatosBancarios")) ? dr.GetString(dr.GetOrdinal("IDDatosBancarios")) : string.Empty;
                   datos.Completado = true;
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
                    datos.FotoCuenta= !Dr.IsDBNull(Dr.GetOrdinal("imagenUrl")) ? Dr.GetString(Dr.GetOrdinal("imagenUrl")) : string.Empty;
                    datos.Completado = true;

                    datos.FotoCuenta = Auxiliar.ValidImageFormServer(datos.FotoCuenta,
                        ProjectSettings.BaseDirClienteCuentasBancarias);
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
                        datos.FotoCuenta = aux.ToString();
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
        #region Foto de Perfil
        public RespuestaAjax ActualizarFotoPerfil(string idProveedor, string idUsuario, string urlFotoPerfil, string conexion)
        {
            var respuesta = new RespuestaAjax();
            try
            {
                object[] parametros =
                {
                    idProveedor, idUsuario, urlFotoPerfil
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(conexion, "spCIDDB_Catalogo_a_CatCliente_fotoPerfil", parametros);


                while (dr.Read())
                {
                    respuesta.Success = !dr.IsDBNull(dr.GetOrdinal("Success"))
                        ? dr.GetBoolean(dr.GetOrdinal("Success"))
                        : false;
                    respuesta.Mensaje = !dr.IsDBNull(dr.GetOrdinal("Mensaje"))
                        ? dr.GetString(dr.GetOrdinal("Mensaje"))
                        : string.Empty;
                    respuesta.MensajeErrorSQL = !dr.IsDBNull(dr.GetOrdinal("MensajeErrorSQL"))
                        ? dr.GetString(dr.GetOrdinal("MensajeErrorSQL"))
                        : string.Empty;
                }

                dr.Close();


            }
            catch (Exception ex)
            {
                respuesta.Mensaje = ex.Message;
                respuesta.Success = false;
            }

            return respuesta;
        }
        #endregion
        #region FotoCuentas
        public RespuestaAjax ActualizarFotoCuentas(string idProveedor, string idUsuario, string urlFotoCuenta, string conexion)
        {
            var respuesta = new RespuestaAjax();
            try
            {
                object[] parametros =
                {
                    idProveedor, idUsuario, urlFotoCuenta
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(conexion, "spCIDDB_Catalogo_a_CatCliente_fotoCuentas", parametros);


                while (dr.Read())
                {
                    respuesta.Success = !dr.IsDBNull(dr.GetOrdinal("Success"))
                        ? dr.GetBoolean(dr.GetOrdinal("Success"))
                        : false;
                    respuesta.Mensaje = !dr.IsDBNull(dr.GetOrdinal("Mensaje"))
                        ? dr.GetString(dr.GetOrdinal("Mensaje"))
                        : string.Empty;
                    respuesta.MensajeErrorSQL = !dr.IsDBNull(dr.GetOrdinal("MensajeErrorSQL"))
                        ? dr.GetString(dr.GetOrdinal("MensajeErrorSQL"))
                        : string.Empty;
                }

                dr.Close();


            }
            catch (Exception ex)
            {
                respuesta.Mensaje = ex.Message;
                respuesta.Success = false;
            }

            return respuesta;
        }
        #endregion
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

                        Datos.Imagen =
                            Auxiliar.ValidImageFormServer(Datos.Imagen, ProjectSettings.BaseDirClienteUppPsg);
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
        #region Notas
        public void GuardarNota(CatClienteModels model)
        {
            using (var sqlcon = new SqlConnection(model.Conexion))
            {
                using (var cmd = new SqlCommand("spCIDDB_Catalogo_CatCliente_GuardarNota", sqlcon))
                {
                    //parametros de entrada
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@IdCliente", SqlDbType.Char).Value = model.IDCliente;
                    cmd.Parameters.Add("@Notas", SqlDbType.NVarChar).Value = model.Notas;

                    // execute
                    sqlcon.Open();

                    var reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        model.RespuestaAjax = new RespuestaAjax();

                        while (reader.Read())
                        {
                            model.RespuestaAjax.Success = bool.Parse(reader["Success"].ToString());
                            model.RespuestaAjax.Mensaje = reader["Mensaje"].ToString();
                        }
                    }
                    reader.Close();
                }
            }
        }
        public void ObtenerNota(CatClienteModels model)
        {
            using (var sqlcon = new SqlConnection(model.Conexion))
            {
                using (var cmd = new SqlCommand("spCIDDB_Catalogo_CatCliente_ObtenerNota", sqlcon))
                {
                    //parametros de entrada
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@IdCliente", SqlDbType.Char).Value = model.IDCliente;

                    // execute
                    sqlcon.Open();

                    var reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        model.RespuestaAjax = new RespuestaAjax();

                        while (reader.Read())
                        {
                            model.RespuestaAjax.Success = bool.Parse(reader["Success"].ToString());
                            model.RespuestaAjax.Mensaje = reader["Mensaje"].ToString();

                            if (model.RespuestaAjax.Success)
                            {
                                model.Notas = reader["Notas"].ToString();
                            }
                            else
                            {
                                throw new Exception(model.RespuestaAjax.Mensaje);
                            }
                        }
                    }
                    reader.Close();
                }
            }
        }

        #endregion
        #region DocumentosExtra
        public string DocumentosExtras_ObtenerIndex(DataTableAjaxPostModel dataTableAjaxPostModel, string id)
        {
            try
            {
                using (var sqlcon = new SqlConnection(ConexionSql))
                {
                    using (var cmd = new SqlCommand("spCSLDB_Catalogo_DocumentacionExtra_CatCliente_ObtenerIndex", sqlcon))
                    {
                        //parametros de entrada
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@Start", SqlDbType.Int).Value = dataTableAjaxPostModel.start;
                        cmd.Parameters.Add("@Length", SqlDbType.Int).Value = dataTableAjaxPostModel.length;
                        cmd.Parameters.Add("@SearchValue", SqlDbType.NVarChar).Value = dataTableAjaxPostModel.search.value;
                        cmd.Parameters.Add("@Draw", SqlDbType.Int).Value = dataTableAjaxPostModel.draw;
                        cmd.Parameters.Add("@ColumnNumber", SqlDbType.Int).Value = dataTableAjaxPostModel.order[0].column;
                        cmd.Parameters.Add("@ColumnDir", SqlDbType.NVarChar).Value = dataTableAjaxPostModel.order[0].dir;
                        cmd.Parameters.Add("@IdCliente", SqlDbType.Char).Value = id;

                        // execute
                        sqlcon.Open();

                        var reader = cmd.ExecuteReader();

                        var indexDatatableDto = new IndexDatatableDto();

                        if (reader.HasRows)
                        {
                            indexDatatableDto.Data = new List<object>();
                            var firstData = true;

                            while (reader.Read())
                            {
                                if (firstData)
                                {
                                    indexDatatableDto.Draw = int.Parse(reader["Draw"].ToString()); ;
                                    indexDatatableDto.RecordsFiltered = int.Parse(reader["RecordsFiltered"].ToString());
                                    indexDatatableDto.RecordsTotal = int.Parse(reader["RecordsTotal"].ToString());
                                    firstData = false;
                                }

                                var indexDto = new IndexClienteGanadoDocumentacionExtraDto();

                                indexDto.IdDocumentacionExtra = reader["IdDocumentacionExtra"].ToString();
                                indexDto.IdTipoDocumentacionExtra = int.Parse(reader["IdTipoDocumentacionExtra"].ToString());
                                indexDto.NombreTipoDocumentacionExtra = reader["NombreTipoDocumentacionExtra"].ToString();
                                indexDto.IdCliente = reader["IdCliente"].ToString();
                                indexDto.UrlArchivo = reader["UrlArchivo"].ToString();

                                indexDto.UrlArchivo = Auxiliar.ValidImageFormServer(indexDto.UrlArchivo,
                                    ProjectSettings.BaseDirClienteDocumentacionExtra);

                                indexDatatableDto.Data.Add(indexDto);
                            }
                        }

                        var json = JsonConvert.SerializeObject(indexDatatableDto);

                        reader.Close();

                        return json;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public RespuestaAjax GuardarDocumentoExtra(DocumentacionExtra_CatClienteModel model)
        {
            var respuesta = new RespuestaAjax();
            try
            {
                using (var sqlcon = new SqlConnection(ConexionSql))
                {
                    using (var cmd = new SqlCommand("spCSLDB_DocumentacionExtra_CatCliente_GuardarArchivo",
                        sqlcon))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@UrlArchivo", SqlDbType.NVarChar).Value = model.Archivo;
                        cmd.Parameters.Add("@IdCliente", SqlDbType.Char).Value = model.IdCliente;
                        cmd.Parameters.Add("@IdTipoDocumentacionExtra", SqlDbType.Int).Value =
                            model.IdTipoDocumentacionExtra;
                        cmd.Parameters.Add("@IdUsuario", SqlDbType.Char).Value = UsuarioActual;
                        cmd.Parameters.Add("@IdDocumentacionExtra", SqlDbType.Char).Value = model.IdDocumentacionExtra;


                        sqlcon.Open();

                        var reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                respuesta.Success = (bool)reader["Success"];
                                respuesta.Mensaje = reader["Mensaje"].ToString();
                                if (!respuesta.Success)
                                    respuesta.MensajeErrorSQL = reader["MensejeErrorSQL"].ToString();
                            }
                        }

                        reader.Close();
                    }
                }
            }
            catch (Exception e)
            {
                respuesta.Success = false;
                respuesta.MensajeErrorSQL = e.Message;
                respuesta.Mensaje =
                    "Se ha producido un error al guardar el documento, intentelo más tarde o contacte con soporte técnico.";
            }

            return respuesta;
        }
        public DocumentacionExtra_CatClienteModel ObtenerDocumentacionExtra(string idCliente, string idDocumentacionExtra)
        {
            var model = new DocumentacionExtra_CatClienteModel();

            using (var sqlcon = new SqlConnection(ConexionSql))
            {
                using (var cmd = new SqlCommand("CSLDB_DocumentacionExtra_CatCliente_ObtenerArchivo",
                    sqlcon))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@IdCliente", SqlDbType.Char).Value = idCliente;
                    cmd.Parameters.Add("@IdDocumentacionExtra", SqlDbType.Char).Value = idDocumentacionExtra;

                    sqlcon.Open();

                    var reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            model.IdTipoDocumentacionExtra = int.Parse(reader["IdTipoDocumentacionExtra"].ToString());
                            model.Archivo = reader["UrlArchivo"].ToString();
                            model.IdCliente = reader["IdCliente"].ToString();
                            model.IdDocumentacionExtra = reader["IdDocumentacionExtra"].ToString();

                            model.Archivo = Auxiliar.ValidImageFormServer(model.Archivo,
                                ProjectSettings.BaseDirClienteDocumentacionExtra);
                        }
                    }

                    reader.Close();
                }
            }

            return model;
        }
        public RespuestaAjax EliminarDocumentoExtra(string idCliente, string idDocumentacionExtra, string urlArchivo)
        {
            var respuesta = new RespuestaAjax();
            try
            {
                using (var sqlcon = new SqlConnection(ConexionSql))
                {
                    using (var cmd = new SqlCommand("spCSLDB_DocumentacionExtra_CatClientes_EliminarDocumentacionExtra",
                        sqlcon))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@IdCliente", SqlDbType.Char).Value = idCliente;
                        cmd.Parameters.Add("@IdDocumentacionExtra", SqlDbType.Char).Value = idDocumentacionExtra;
                        cmd.Parameters.Add("@UrlArchivo", SqlDbType.NVarChar).Value = urlArchivo;


                        sqlcon.Open();

                        var reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                respuesta.Success = (bool)reader["Success"];
                                respuesta.Mensaje = reader["Mensaje"].ToString();
                                if (!respuesta.Success)
                                    respuesta.MensajeErrorSQL = reader["MensejeErrorSQL"].ToString();
                            }
                        }

                        reader.Close();
                    }
                }
            }
            catch (Exception e)
            {
                respuesta.Success = false;
                respuesta.MensajeErrorSQL = e.Message;
                respuesta.Mensaje =
                    "Se ha producido un error al guardar el documento, intentelo más tarde o contacte con soporte técnico.";
            }

            return respuesta;
        }
        #endregion
        #region Imagenes

        public ImagenesClienteGanadoDto ObtenerCliente(string idCliente)
        {
            var model = new ImagenesClienteGanadoDto();

            using (var sqlcon = new SqlConnection(ConexionSql))
            {
                using (var cmd = new SqlCommand("spCIDDB_CatCliente_Imagenes_ObtenerCliente",
                    sqlcon))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@IdCliente", SqlDbType.Char).Value = idCliente;

                    sqlcon.Open();

                    var reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            model.IdCliente = reader["id_cliente"].ToString();
                            model.RazonSocial_Nombre = reader["nombreRazonSocial"].ToString();
                            model.FotoPerfilUrl = reader["UrlFotoPerfil"].ToString();
                            model.Rfc = reader["rfc"].ToString();
                            model.Sucursal = reader["Sucursal"].ToString();
                            model.TipoCliente = reader["TipoCliente"].ToString();
                            model.Direccion = reader["direccion"].ToString();
                            model.Tolerancia = reader["tolerancia"].ToString() + " %";
                            model.Telefonos = reader["Telefono"].ToString();
                            model.Email = reader["correo"].ToString();
                            model.UppPsgBase64 = reader["imagenUPP"].ToString();
                            IFormatProvider culture = new CultureInfo("es-MX", true);
                            model.FechaIngreso = DateTime.ParseExact(reader["FechaIngreso"].ToString(), "dd/MM/yyyy hh:mm:ss tt",
                                culture).ToString("dd/MM/yyyy", culture);

                            model.UppPsgBase64 = Auxiliar.ValidImageFormServer(model.UppPsgBase64,
                                ProjectSettings.BaseDirClienteUppPsg);
                            model.FotoPerfilUrl = Auxiliar.ValidImageFormServer(model.FotoPerfilUrl,
                                ProjectSettings.BaseDirClienteFotoPerfil);
                        }
                    }

                    reader.Close();
                }
            }

            return model;
        }
        public List<CuentaBancariaClienteModels> ObtenerCuentasBancarias(CuentaBancariaClienteModels Datos)
        {
            try
            {
                List<CuentaBancariaClienteModels> lista = new List<CuentaBancariaClienteModels>();
                CuentaBancariaClienteModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Catalogo_get_CatClienteDatosBancarios", Datos.IDCliente);
                while (dr.Read())
                {
                    item = new CuentaBancariaClienteModels();
                    item.IDDatosBancarios = !dr.IsDBNull(dr.GetOrdinal("IDDatosBancarios")) ? dr.GetString(dr.GetOrdinal("IDDatosBancarios")) : string.Empty;
                    item.Banco.Descripcion = !dr.IsDBNull(dr.GetOrdinal("NombreBanco")) ? dr.GetString(dr.GetOrdinal("NombreBanco")) : string.Empty;
                    item.Titular = !dr.IsDBNull(dr.GetOrdinal("NombreTitular")) ? dr.GetString(dr.GetOrdinal("NombreTitular")) : string.Empty;
                    item.NumTarjeta = !dr.IsDBNull(dr.GetOrdinal("NumeroTarjeta")) ? dr.GetString(dr.GetOrdinal("NumeroTarjeta")) : string.Empty;
                    item.NumCuenta = !dr.IsDBNull(dr.GetOrdinal("NumeroCuenta")) ? dr.GetString(dr.GetOrdinal("NumeroCuenta")) : string.Empty;
                    item.Clabe = !dr.IsDBNull(dr.GetOrdinal("ClaveInterbancaria")) ? dr.GetString(dr.GetOrdinal("ClaveInterbancaria")) : string.Empty;
                    item.ImagenUrl = !dr.IsDBNull(dr.GetOrdinal("ImagenUrl")) ? dr.GetString(dr.GetOrdinal("ImagenUrl")) : string.Empty;

                    item.ImagenUrl = Auxiliar.ValidImageFormServer(item.ImagenUrl,
                        ProjectSettings.BaseDirClienteCuentasBancarias);

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
        #region Obtener proveevedores por

        public List<ConfiguracionReporteCliente> ObtenerClienteXSucursal(List<string> sucursales)
        {
            var lista = new List<ConfiguracionReporteCliente>();

            using (var sqlcon = new SqlConnection(ConexionSql))
            {
                using (var cmd = new SqlCommand("spCIDDB_Catalogo_CatCliente_ObtenerClientesXSucursal",
                    sqlcon))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    DataTable dataTable;
                    dataTable = new DataTable();

                    dataTable.Columns.Add("Id", typeof(string));
                    if (sucursales != null)
                    {
                        foreach (var item in sucursales)
                        {
                            dataTable.Rows.Add(item);
                        }
                    }

                    cmd.Parameters.Add("@UDTT_Sucursales", SqlDbType.Structured).Value = dataTable;
                    sqlcon.Open();

                    var reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var item = new ConfiguracionReporteCliente();
                            item.IdCliente = reader["id_cliente"].ToString();
                            item.Cliente = reader["nombreRazonSocial"].ToString();
                            item.Sucursal = reader["nombreSuc"].ToString();

                            lista.Add(item);
                        }
                    }
                    reader.Close();
                }
            }

            return lista;
        }

        #endregion
        #region Reporte Cliente
        public List<ReporteClienteDto> ObtenerReporteClienteDtos(List<string> idClientes, DateTime fechaInicio, DateTime fechaFin)
        {
            var lista = new List<ReporteClienteDto>();

            foreach (var idCliente in idClientes)
            {
                using (var sqlcon = new SqlConnection(ConexionSql))
                {
                    using (var cmd = new SqlCommand("spCIDDB_CatCliente_Reporte_ObtenerCliente",
                        sqlcon))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.Add("@IdCliente", SqlDbType.Char).Value = idCliente;
                        cmd.Parameters.Add("@FechaInicio", SqlDbType.Date).Value = fechaInicio;
                        cmd.Parameters.Add("@FechaFin", SqlDbType.Date).Value = fechaFin;
                        sqlcon.Open();
                        var reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var item = new ReporteClienteDto();
                                var auxFotoPerfilUrl = reader["UrlFotoPerfil"].ToString();
                                //var auxIneUrl = reader["imgINE"].ToString();
                                //var auxManifestacionFierroUrl = reader["imgManifestacionFierro"].ToString();
                                var auxUppBaseUrl = reader["imagenUPP"].ToString();
                                var auxCuentaBancariaImagenUrl = reader["CuentaBancariaImagenUrl"].ToString();
                                var auxDocumentacionExtraImagenUrl = reader["DocumentacionExtraImagenUrl"].ToString();

                                item.FotoPerfilUrl = Auxiliar.FileMapPath(auxFotoPerfilUrl,
                                    ProjectSettings.BaseDirClienteFotoPerfil);                                
                                item.UppPsgUrl = Auxiliar.FileMapPath(auxUppBaseUrl,
                                    ProjectSettings.BaseDirClienteUppPsg);
                                item.CuentaBancariaImagenUrl = Auxiliar.FileMapPath(auxCuentaBancariaImagenUrl,
                                    ProjectSettings.BaseDirClienteCuentasBancarias);
                                item.DocumentacionExtraImagenUrl = Auxiliar.FileMapPath(auxDocumentacionExtraImagenUrl,
                                    ProjectSettings.BaseDirClienteDocumentacionExtra);

                                item.IdCliente = reader["id_cliente"].ToString();
                                item.RazonSocial_Nombre = reader["nombreRazonSocial"].ToString();
                                item.Rfc = reader["rfc"].ToString();
                                item.Sucursal = reader["Sucursal"].ToString();
                                item.TipoCliente = reader["TipoCliente"].ToString();
                                item.Direccion = reader["direccion"].ToString();
                                item.Tolerancia = reader["tolerancia"].ToString() + " %";                                
                                item.Telefonos = reader["Telefono"].ToString();
                                item.Email = reader["correo"].ToString();

                                IFormatProvider culture = new CultureInfo("es-MX", true);
                                item.FechaIngreso = DateTime.ParseExact(reader["FechaIngreso"].ToString(),
                                    "dd/MM/yyyy hh:mm:ss tt",
                                    culture).ToString("dd/MM/yyyy", culture);

                                item.ContactoId = reader["ContactoId"].ToString();
                                item.ContactoNombre = reader["ContactoNombre"].ToString();
                                item.ContactoTelefono = reader["ContactoTelefono"].ToString();
                                item.ContactoEmail = reader["ContactoEmail"].ToString();
                                item.ContactoDireccion = reader["ContactoDireccion"].ToString();
                                item.ContactoObservacion = reader["ContactoObservacion"].ToString();

                                item.CuentaBancariaId = reader["CuentaBancariaId"].ToString();
                                item.BancoNombre = reader["BancoNombre"].ToString();
                                item.CuentaBancariaTitular = reader["CuentaBancariaTitular"].ToString();
                                item.CuentaBancariaNumTarjeta = reader["CuentaBancariaNumTarjeta"].ToString();
                                item.CuentaBancariaNumCuenta = reader["CuentaBancariaNumCuenta"].ToString();
                                item.CuentaBancariaClabeInterbancaria =
                                    reader["CuentaBancariaClabeInterbancaria"].ToString();

                                item.DocumentacionExtraId = reader["DocumentacionExtraId"].ToString();
                                item.DocumentacionExtraTipoDocumentacionExtra =
                                    reader["DocumentacionExtraTipoDocumentacionExtra"].ToString();
                                item.VentaId = reader["VentaId"].ToString();
                                item.VentaFecha =
                                    string.IsNullOrEmpty(reader["VentaFecha"].ToString())
                                        ? "Sin fecha"
                                        : DateTime.ParseExact(reader["VentaFecha"].ToString(),
                                            "dd/MM/yyyy hh:mm:ss tt",
                                            culture).ToString("dd/MM/yyyy", culture);

                                item.VentaMerma =
                                    string.IsNullOrEmpty(reader["VentaMerma"].ToString())
                                        ? 0
                                        : decimal.Parse(reader["VentaMerma"].ToString());

                                item.VentaCantidadGanadoMacho =
                                    string.IsNullOrEmpty(reader["VentaCantidadGanadoMacho"].ToString())
                                        ? 0
                                        : int.Parse(reader["VentaCantidadGanadoMacho"].ToString());

                                item.VentaCantidadGanadoHembra =
                                    string.IsNullOrEmpty(reader["VentaCantidadGanadoHembra"].ToString())
                                        ? 0
                                        : int.Parse(reader["VentaCantidadGanadoHembra"].ToString());

                                item.VentaCantidadGanadoTotal =
                                    string.IsNullOrEmpty(reader["VentaCantidadGanadoTotal"].ToString())
                                        ? 0
                                        : int.Parse(reader["VentaCantidadGanadoTotal"].ToString());

                                item.VentaKilosGanadoMacho =
                                    string.IsNullOrEmpty(reader["VentaKilosGanadoMacho"].ToString())
                                        ? 0
                                        : decimal.Parse(reader["VentaKilosGanadoMacho"].ToString());

                                item.VentaKilosGanadoHembra =
                                    string.IsNullOrEmpty(reader["VentaKilosGanadoHembra"].ToString())
                                        ? 0
                                        : decimal.Parse(reader["VentaKilosGanadoHembra"].ToString());

                                item.VentaKilosGanadoTotal =
                                    string.IsNullOrEmpty(reader["VentaKilosGanadoTotal"].ToString())
                                        ? 0
                                        : decimal.Parse(reader["VentaKilosGanadoTotal"].ToString());

                                item.VentaImporteGanadoMacho =
                                    string.IsNullOrEmpty(reader["VentaImporteGanadoMacho"].ToString())
                                        ? 0
                                        : decimal.Parse(reader["VentaImporteGanadoMacho"].ToString());

                                item.VentaImporteGanadoHembra =
                                    string.IsNullOrEmpty(reader["VentaImporteGanadoHembra"].ToString())
                                        ? 0
                                        : decimal.Parse(reader["VentaImporteGanadoHembra"].ToString());

                                item.VentaImporteGanadoTotal =
                                    string.IsNullOrEmpty(reader["VentaImporteGanadoTotal"].ToString())
                                        ? 0
                                        : decimal.Parse(reader["VentaImporteGanadoTotal"].ToString());

                                item.VentaImporteDeducciones =
                                    string.IsNullOrEmpty(reader["VentaImporteDeducciones"].ToString())
                                        ? 0
                                        : decimal.Parse(reader["VentaImporteDeducciones"].ToString());

                                item.VentaImporteTotal =
                                    string.IsNullOrEmpty(reader["VentaImporteTotal"].ToString())
                                        ? 0
                                        : decimal.Parse(reader["VentaImporteTotal"].ToString());
                                item.Flete =
                                    string.IsNullOrEmpty(reader["Flete"].ToString())
                                        ? 0
                                        : decimal.Parse(reader["Flete"].ToString());
                                item.MostrarTablaContactos = (bool)reader["MostrarTablaContactos"];
                                item.MostrarTablaCuentasBancarias = (bool)reader["MostrarTablaCuentasBancarias"];
                                item.MostrarTablaDocumentacionExtra = (bool)reader["MostrarTablaDocumentacionExtra"];
                                item.MostrarTablaVentas = (bool)reader["MostrarTablaVentas"];
                                lista.Add(item);
                            }
                        }

                        reader.Close();
                    }
                }
            }
            return lista;
        }
        #endregion
    }
}