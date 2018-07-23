using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class _CatEmpresa_Datos
    {
        public List<CatEmpresaModels> GetListadoEmpresas(CatEmpresaModels Empresa)
        {
            try
            {
                CatEmpresaModels ItemEmpresa;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Empresa.Conexion, "spCSLDB_EMPRESA_get_CatEmpresas");
                while (dr.Read())
                {
                    ItemEmpresa = new CatEmpresaModels
                    {
                        IDEmpresa = !dr.IsDBNull(dr.GetOrdinal("id_empresa")) ? dr.GetString(dr.GetOrdinal("id_empresa")) : string.Empty,
                        LogoEmpresa = !dr.IsDBNull(dr.GetOrdinal("logoEmpresa")) ? dr.GetString(dr.GetOrdinal("logoEmpresa")) : string.Empty,
                        RazonFiscal = !dr.IsDBNull(dr.GetOrdinal("razonFiscal")) ? dr.GetString(dr.GetOrdinal("razonFiscal")) : string.Empty,
                        DireccionFiscal = !dr.IsDBNull(dr.GetOrdinal("direccionFiscal")) ? dr.GetString(dr.GetOrdinal("direccionFiscal")) : string.Empty,
                        RFC = !dr.IsDBNull(dr.GetOrdinal("RFC")) ? dr.GetString(dr.GetOrdinal("RFC")) : string.Empty,
                        Representante = !dr.IsDBNull(dr.GetOrdinal("representante")) ? dr.GetString(dr.GetOrdinal("representante")) : string.Empty,
                        NumTelefonico1 = !dr.IsDBNull(dr.GetOrdinal("numTelefonico1")) ? dr.GetString(dr.GetOrdinal("numTelefonico1")) : string.Empty,
                        NumTelefonico2 = !dr.IsDBNull(dr.GetOrdinal("numTelefonico2")) ? dr.GetString(dr.GetOrdinal("numTelefonico2")) : string.Empty,
                        Email = !dr.IsDBNull(dr.GetOrdinal("email")) ? dr.GetString(dr.GetOrdinal("email")) : string.Empty,
                        HorarioAtencion = !dr.IsDBNull(dr.GetOrdinal("horarioAtencion")) ? dr.GetString(dr.GetOrdinal("horarioAtencion")) : string.Empty,
                        PermitirSucursales = !dr.IsDBNull(dr.GetOrdinal("permitirSucursales")) ? dr.GetBoolean(dr.GetOrdinal("permitirSucursales")) : false,
                    };
                    ItemEmpresa.LogoEmpresa = ItemEmpresa.ValidarStringImage(ItemEmpresa.LogoEmpresa);

                    Empresa.ListaEmpresas.Add(ItemEmpresa);
                }
                dr.Close();
                return Empresa.ListaEmpresas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CatEmpresaModels GetEmpresaXID(CatEmpresaModels Empresa)
        {
            try
            {
                object[] parametros =
                {
                    Empresa.IDEmpresa
                };

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Empresa.Conexion, "spCSLDB_EMPRESA_Get_CatEmpresasXID", parametros);
                while (dr.Read())
                {
                    Empresa.LogoEmpresa = !dr.IsDBNull(dr.GetOrdinal("logoEmpresa")) ? dr.GetString(dr.GetOrdinal("logoEmpresa")) : string.Empty;
                    Empresa.LogoRFC = !dr.IsDBNull(dr.GetOrdinal("logoRFC")) ? dr.GetString(dr.GetOrdinal("logoRFC")) : string.Empty;
                    Empresa.RazonFiscal = !dr.IsDBNull(dr.GetOrdinal("razonFiscal")) ? dr.GetString(dr.GetOrdinal("razonFiscal")) : string.Empty;
                    Empresa.DireccionFiscal = !dr.IsDBNull(dr.GetOrdinal("direccionFiscal")) ? dr.GetString(dr.GetOrdinal("direccionFiscal")) : string.Empty;
                    Empresa.RFC = !dr.IsDBNull(dr.GetOrdinal("RFC")) ? dr.GetString(dr.GetOrdinal("RFC")) : string.Empty;
                    Empresa.Representante = !dr.IsDBNull(dr.GetOrdinal("representante")) ? dr.GetString(dr.GetOrdinal("representante")) : string.Empty;
                    Empresa.NumTelefonico1 = !dr.IsDBNull(dr.GetOrdinal("numTelefonico1")) ? dr.GetString(dr.GetOrdinal("numTelefonico1")) : string.Empty;
                    Empresa.NumTelefonico2 = !dr.IsDBNull(dr.GetOrdinal("numTelefonico2")) ? dr.GetString(dr.GetOrdinal("numTelefonico2")) : string.Empty;
                    Empresa.Email = !dr.IsDBNull(dr.GetOrdinal("email")) ? dr.GetString(dr.GetOrdinal("email")) : string.Empty;
                    Empresa.HorarioAtencion = !dr.IsDBNull(dr.GetOrdinal("horarioAtencion")) ? dr.GetString(dr.GetOrdinal("horarioAtencion")) : string.Empty;
                    Empresa.LogoEmpresaHttp = null;

                    //No hay imgEmpresa en BD
                    Empresa.ImagBDEmpresa = (string.IsNullOrWhiteSpace(Empresa.LogoEmpresa)) ? false : true;

                    //No hay imgRFC en BD
                    Empresa.ImagBDRFC = (string.IsNullOrWhiteSpace(Empresa.LogoRFC)) ? false : true;

                    Empresa.LogoEmpresa = Empresa.ValidarStringImage(Empresa.LogoEmpresa);
                    Empresa.LogoRFC = Empresa.ValidarStringImage(Empresa.LogoRFC);
                }
                dr.Close();
                return Empresa;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CatEmpresaModels UpdateEmpresaXID(CatEmpresaModels Empresa)
        {
            try
            {
                object[] parametros =
                {
                    Empresa.IDEmpresa, // @id_empresa CHAR(36)
                    Empresa.RazonFiscal,//,@razonFiscal NVARCHAR(150)
                    Empresa.DireccionFiscal,//,@direccionFiscal NVARCHAR(150)
                    Empresa.RFC,//,@RFC NVARCHAR(20)
                    Empresa.NumTelefonico1,//,@numTelefonico1 NVARCHAR(15)
                    Empresa.NumTelefonico2,//,@numTelefonico2 NVARCHAR(15)
                    Empresa.Email,//,@email NVARCHAR(50)
                    Empresa.LogoEmpresa,//,@logoEmpresa NVARCHAR(MAX)
                    Empresa.HorarioAtencion,//,@horarioAtencion NVARCHAR(150)
                    Empresa.Representante,//,@representante NVARCHAR(100)
                    Empresa.LogoRFC,//,@logoRFC NVARCHAR(MAX)
                    Empresa.IDUsuario,//,@id_usuario CHAR(36)
                    Empresa.ImagBDEmpresa
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Empresa.Conexion, "spCSLDB_EMPRESA_a_CatEmpresaXID", parametros);

                while (dr.Read())
                {
                    Empresa.RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                    Empresa.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : true;
                }
                dr.Close();
                return Empresa;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetCuentasBancarias(CatEmpresaModels Empresa)
        {
            try
            {
                object[] parametros =
                {
                    Empresa.IDEmpresa
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Empresa.Conexion, "spCSLDB_EMPRESA_get_CuentasBancarias", parametros);
                string jsonDr = Auxiliar.SqlReaderToJson(dr);
                dr.Close();
                return jsonDr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CatEmpresaModels GetDatosBancariosXID(CatEmpresaModels Empresa)
        {
            try
            {
                object[] parametros =
                {
                    Empresa.CuentaBancaria.IDDatosBancarios
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Empresa.Conexion, "spCSLDB_EMPRESA_get_CuentaBancariaXID", parametros);

                while (dr.Read())
                {
                    Empresa.Banco.IDBanco = !dr.IsDBNull(dr.GetOrdinal("id_banco")) ? dr.GetInt32(dr.GetOrdinal("id_banco")) : 0;
                    Empresa.Banco.Imagen = !dr.IsDBNull(dr.GetOrdinal("ImgBanco")) ? dr.GetString(dr.GetOrdinal("ImgBanco")) : string.Empty;
                    Empresa.Banco.Imagen = Empresa.ValidarStringImage(Empresa.Banco.Imagen);
                    Empresa.Banco.Descripcion = !dr.IsDBNull(dr.GetOrdinal("NomBanco")) ? dr.GetString(dr.GetOrdinal("NomBanco")) : string.Empty;
                    Empresa.CuentaBancaria.Titular = !dr.IsDBNull(dr.GetOrdinal("Titular")) ? dr.GetString(dr.GetOrdinal("Titular")) : string.Empty;
                    Empresa.CuentaBancaria.NumTarjeta = !dr.IsDBNull(dr.GetOrdinal("NumTarjeta")) ? dr.GetString(dr.GetOrdinal("NumTarjeta")) : string.Empty;
                    Empresa.CuentaBancaria.NumCuenta = !dr.IsDBNull(dr.GetOrdinal("NumCuenta")) ? dr.GetString(dr.GetOrdinal("NumCuenta")) : string.Empty;
                    Empresa.CuentaBancaria.Clabe = !dr.IsDBNull(dr.GetOrdinal("ClabeInter")) ? dr.GetString(dr.GetOrdinal("ClabeInter")) : string.Empty;
                }
                Empresa.CuentaBancaria.Titular.Trim();
                Empresa.CuentaBancaria.NumCuenta.Trim();
                Empresa.CuentaBancaria.NumCuenta.Trim();
                Empresa.CuentaBancaria.Clabe.Trim();
                dr.Close();
                return Empresa;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CatBancoModels> GetListaBancos(CatEmpresaModels Empresa)
        {
            try
            {
                CatBancoModels Banco;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Empresa.Conexion, "spCSLDB_EMPRESA_get_CatBancos");

                while (dr.Read())
                {
                    Banco = new CatBancoModels
                    {
                        IDBanco = !dr.IsDBNull(dr.GetOrdinal("id_banco")) ? dr.GetInt32(dr.GetOrdinal("id_banco")) : 0,
                        Imagen = !dr.IsDBNull(dr.GetOrdinal("ImgBanco")) ? dr.GetString(dr.GetOrdinal("ImgBanco")) : string.Empty,
                        Descripcion = !dr.IsDBNull(dr.GetOrdinal("NomBanco")) ? dr.GetString(dr.GetOrdinal("NomBanco")) : string.Empty
                    };
                    Banco.Imagen = Empresa.ValidarStringImage(Empresa.Banco.Imagen);
                    Empresa.ListaBancos.Add(Banco);
                }
                dr.Close();
                return Empresa.ListaBancos;
            }                
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CatEmpresaModels InsertUpdateCuentaBancaria(CatEmpresaModels Empresa)
        {
            try
            {
                object[] parametros =
                {
                    Empresa.CuentaBancaria.IDDatosBancarios,
                    Empresa.IDEmpresa,
                    Empresa.Banco.IDBanco,
                    Empresa.CuentaBancaria.Titular ?? string.Empty,
                    Empresa.CuentaBancaria.NumTarjeta ?? string.Empty,
                    Empresa.CuentaBancaria.NumCuenta ?? string.Empty,
                    Empresa.CuentaBancaria.Clabe ?? string.Empty,
                    Empresa.IDUsuario
                };
                SqlDataReader dr = null;

                dr = SqlHelper.ExecuteReader(Empresa.Conexion, "spCSLDB_EMPRESA_ac_CuentaBancaria", parametros);

                while (dr.Read())
                {
                    Empresa.RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                    Empresa.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : true;
                }
                dr.Close();
                return Empresa;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CatEmpresaModels DeleteCuentaBancaria(CatEmpresaModels Empresa)
        {
            try
            {
                object[] parametros =
                {
                    Empresa.CuentaBancaria.IDDatosBancarios,
                    Empresa.IDUsuario
                };
                SqlDataReader dr = null;

                dr = SqlHelper.ExecuteReader(Empresa.Conexion, "spCSLDB_EMPRESA_del_CuentaBancaria", parametros);

                while (dr.Read())
                {
                    Empresa.RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                    Empresa.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : true;
                }
                dr.Close();
                return Empresa;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CatEmpresaModels GetDatosEmpresaPrincipal(CatEmpresaModels Empresa)
        {
            try
            {
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Empresa.Conexion, "spCSLDB_EMPRESA_get_CatEmpresasIDTIPO1");
                while (dr.Read())
                {
                    Empresa.LogoEmpresa = !dr.IsDBNull(dr.GetOrdinal("LogoEmpresa")) ? dr.GetString(dr.GetOrdinal("LogoEmpresa")) : string.Empty;
                    Empresa.RazonFiscal = !dr.IsDBNull(dr.GetOrdinal("RazonFiscal")) ? dr.GetString(dr.GetOrdinal("RazonFiscal")) : string.Empty;
                    Empresa.DireccionFiscal = !dr.IsDBNull(dr.GetOrdinal("DireccionFiscal")) ? dr.GetString(dr.GetOrdinal("DireccionFiscal")) : string.Empty;
                    Empresa.RFC = !dr.IsDBNull(dr.GetOrdinal("RFC")) ? dr.GetString(dr.GetOrdinal("RFC")) : string.Empty;
                    Empresa.Representante = !dr.IsDBNull(dr.GetOrdinal("representante")) ? dr.GetString(dr.GetOrdinal("representante")) : string.Empty;
                    Empresa.NumTelefonico1 = !dr.IsDBNull(dr.GetOrdinal("NumTelefono1")) ? dr.GetString(dr.GetOrdinal("NumTelefono1")) : string.Empty;
                    Empresa.NumTelefonico2 = !dr.IsDBNull(dr.GetOrdinal("NumTelefono2")) ? dr.GetString(dr.GetOrdinal("NumTelefono2")) : string.Empty;
                    Empresa.Email = !dr.IsDBNull(dr.GetOrdinal("Correo")) ? dr.GetString(dr.GetOrdinal("Correo")) : string.Empty;
                    Empresa.HorarioAtencion = !dr.IsDBNull(dr.GetOrdinal("HorarioAtencion")) ? dr.GetString(dr.GetOrdinal("HorarioAtencion")) : string.Empty;
                    Empresa.LogoEmpresa = Empresa.ValidarStringImage(Empresa.LogoEmpresa);
                }
                dr.Close();
                return Empresa;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}