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

        public string GetFleteIndexDataTable(FleteModels Flete)
        {
            try
            {
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Flete.Conexion, "spCSLDB_Flete_index");
                string datatable = Auxiliar.SqlReaderToJson(dr);
                dr.Close();
                return datatable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string GetDocumentosDataTable(FleteModels Flete)
        {
            object[] parametros =
            {
                Flete.id_flete
            };

            try
            {
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Flete.Conexion, "spCSLDB_Flete_get_DocumentosXIDFlete", parametros);
                string datatable = Auxiliar.SqlReaderToJson(dr);
                dr.Close();
                return datatable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string GetGanadoDataTable(FleteModels Flete)
        {
            try
            {
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Flete.Conexion, "spCSLDB_get_GanadoActivo");
                string datatable = Auxiliar.SqlReaderToJson(dr);
                dr.Close();
                return datatable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string GetProductoGanadoXIDFlete(FleteModels Flete)
        {
            try
            {
                object[] parametros =
                    {
                        Flete.id_flete
                    };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Flete.Conexion, "spCSLDB_Flete_get_ProductoGanadoPropioXIDFlete", parametros);
                string datatable = Auxiliar.SqlReaderToJson(dr);
                dr.Close();
                return datatable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string GetProductoGanadoNOPropioXIDFlete(FleteModels Flete)
        {
            try
            {
                object[] parametros =
                    {
                        Flete.id_flete
                    };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Flete.Conexion, "spCSLDB_Flete_get_ProductoGanadoNOPropioXIDFlete", parametros);
                string datatable = Auxiliar.SqlReaderToJson(dr);
                dr.Close();
                return datatable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public SqlDataReader GetEventoXIDFlete(FleteModels Flete)
        {
            try
            {
                object[] parametros =
                    {
                        Flete.id_flete
                    };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Flete.Conexion, "spCSLDB_Flete_get_Eventos", parametros);
                dr.Close();
                return dr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public SqlDataReader GetProductoGanadoNoAccidentadoXIDEvento(EventoEnvioModels Evento)
        {
            try
            {
                object[] parametros =
                    {
                        Evento.IDEnvio,
                        Evento.IDEvento
                    };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Evento.Conexion, "spCSLDB_Flete_get_ProductoGanadoCargadoSINEventoXIDFleteXIDEvento", parametros);
                dr.Close();
                return dr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public SqlDataReader GetProductoGanadoAccidentadoXIDEvento(EventoEnvioModels Evento)
        {
            try
            {
                object[] parametros =
                    {
                        Evento.IDEnvio,
                        Evento.IDEvento
                    };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Evento.Conexion, "spCSLDB_Flete_get_ProductoGanadoCargadoCONEventoXIDFleteXIDEvento", parametros);
                dr.Close();
                return dr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

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
                    //Flete.Jaula.IDJaula = !dr.IsDBNull(dr.GetOrdinal("id_jaula")) ? dr.GetString(dr.GetOrdinal("id_jaula")) : string.Empty;//,f.id_jaula
                    //Flete.Remolque.IDRemolque = !dr.IsDBNull(dr.GetOrdinal("id_remolque")) ? dr.GetString(dr.GetOrdinal("id_remolque")) : string.Empty;//,f.id_remolque
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
                    Flete.Cliente.RFC = !dr.IsDBNull(dr.GetOrdinal("rfc")) ? dr.GetString(dr.GetOrdinal("rfc")) : string.Empty;//,c.rfc
                    Flete.precioFlete = !dr.IsDBNull(dr.GetOrdinal("precioFlete")) ? dr.GetDecimal(dr.GetOrdinal("precioFlete")) : 0; //,f.precioFlete
                    Flete.TotalImpuestoRetenido = !dr.IsDBNull(dr.GetOrdinal("totalImpuestoRetenido")) ? dr.GetDecimal(dr.GetOrdinal("totalImpuestoRetenido")) : 0;//,f.totalImpuestoRetenido
                    Flete.TotalImpuestoTrasladado = !dr.IsDBNull(dr.GetOrdinal("totalImpuestoTrasladado")) ? dr.GetDecimal(dr.GetOrdinal("totalImpuestoTrasladado")) : 0;//,f.totalImpuestoTrasladado
                    Flete.TotalFlete = !dr.IsDBNull(dr.GetOrdinal("totalFlete")) ? dr.GetDecimal(dr.GetOrdinal("totalFlete")) : 0;//,f.totalFlete
                    Flete.CondicionPago = !dr.IsDBNull(dr.GetOrdinal("condicionPago")) ? dr.GetString(dr.GetOrdinal("condicionPago")) : string.Empty;//,f.condicionPago
                    Flete.MetodoPago.Clave = !dr.IsDBNull(dr.GetOrdinal("id_metodoPago")) ? dr.GetString(dr.GetOrdinal("id_metodoPago")) : string.Empty;//,f.id_metodoPago
                    Flete.FormaPago.Clave = !dr.IsDBNull(dr.GetOrdinal("id_formaPago")) ? dr.GetInt16(dr.GetOrdinal("id_formaPago")) : 0;//,f.id_formaPago
                    Flete.Id_sucursal = !dr.IsDBNull(dr.GetOrdinal("id_sucursal")) ? dr.GetString(dr.GetOrdinal("id_sucursal")) : string.Empty;
                }
                dr.Close();
                return Flete;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Get get_Recepcion
        public FleteModels Flete_get_Recepcion(FleteModels Flete)
        {
            try
            {
                object[] parametros =
                {
                     Flete.id_flete
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Flete.Conexion, "spCSLDB_Flete_get_Recepcion", parametros);

                while (dr.Read())
                {
                    Flete.RecepcionOrigen.IDRecepcionOrigen = !dr.IsDBNull(dr.GetOrdinal("id_recepcionOrigen")) ? dr.GetString(dr.GetOrdinal("id_recepcionOrigen")) : string.Empty;
                    Flete.RecepcionOrigen.KilometrajeFinal = !dr.IsDBNull(dr.GetOrdinal("kilometrajeFinal")) ? dr.GetInt32(dr.GetOrdinal("kilometrajeFinal")) : 0;
                    Flete.RecepcionOrigen.HoraLlegada = !dr.IsDBNull(dr.GetOrdinal("horaLlegada")) ? dr.GetTimeSpan(dr.GetOrdinal("horaLlegada")) : DateTime.Now.TimeOfDay;
                    Flete.RecepcionOrigen.FechaLlegada = !dr.IsDBNull(dr.GetOrdinal("fechaLlegada")) ? dr.GetDateTime(dr.GetOrdinal("fechaLlegada")) : DateTime.Now;
                    Flete.RecepcionOrigen.Observacion = !dr.IsDBNull(dr.GetOrdinal("observacion")) ? dr.GetString(dr.GetOrdinal("observacion")) : string.Empty;

                    Flete.RecepcionDestino.id_recepcion = !dr.IsDBNull(dr.GetOrdinal("id_recepcionDestino")) ? dr.GetString(dr.GetOrdinal("id_recepcionDestino")) : string.Empty;
                    Flete.RecepcionDestino.kiloTotalRecibido = !dr.IsDBNull(dr.GetOrdinal("kiloTotalRecibido")) ? dr.GetDecimal(dr.GetOrdinal("kiloTotalRecibido")) : 0;
                    Flete.RecepcionDestino.GanadosTotal = !dr.IsDBNull(dr.GetOrdinal("ganadoTotal")) ? dr.GetInt32(dr.GetOrdinal("ganadoTotal")) : 0;
                    Flete.RecepcionDestino.fechaLlegada = !dr.IsDBNull(dr.GetOrdinal("fechaLlegadaDestino")) ? dr.GetDateTime(dr.GetOrdinal("fechaLlegadaDestino")) : DateTime.Now;
                    Flete.RecepcionDestino.HoraLlegada = !dr.IsDBNull(dr.GetOrdinal("horaLlegadaDestino")) ? dr.GetTimeSpan(dr.GetOrdinal("horaLlegadaDestino")) : DateTime.Now.TimeOfDay;
                    Flete.RecepcionDestino.HoraDescarga = !dr.IsDBNull(dr.GetOrdinal("horaDescargaDestino")) ? dr.GetTimeSpan(dr.GetOrdinal("horaDescargaDestino")) : DateTime.Now.TimeOfDay;
                    Flete.RecepcionDestino.recibidoPor = !dr.IsDBNull(dr.GetOrdinal("recibidoPor")) ? dr.GetString(dr.GetOrdinal("recibidoPor")) : string.Empty;
                    Flete.RecepcionDestino.ValijaSellada = !dr.IsDBNull(dr.GetOrdinal("valijaSellada")) ? dr.GetBoolean(dr.GetOrdinal("valijaSellada")) : false;
                    Flete.RecepcionDestino.RecepcionDocumentos = !dr.IsDBNull(dr.GetOrdinal("recepcionDocumentos")) ? dr.GetBoolean(dr.GetOrdinal("recepcionDocumentos")) : false;
                    Flete.RecepcionDestino.observacion = !dr.IsDBNull(dr.GetOrdinal("observacionDestino")) ? dr.GetString(dr.GetOrdinal("observacionDestino")) : string.Empty;

                    Flete.Folio = !dr.IsDBNull(dr.GetOrdinal("folio")) ? dr.GetString(dr.GetOrdinal("folio")) : string.Empty;
                }
                dr.Close();
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
                dr.Close();
                return Flete;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Get DocumentoXIDDocumeto
        public Flete_TipoDocumentoModels GetDocumentoXIDDocumento(Flete_TipoDocumentoModels Flete_Tipo)
        {
            try
            {
                object[] parametros =
                {
                     Flete_Tipo.IDDocumento
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Flete_Tipo.Conexion, "spCSLDB_Flete_get_DocumentoXIDDocumento", parametros);

                while (dr.Read())
                {
                    Flete_Tipo.IDTipoDocumento = !dr.IsDBNull(dr.GetOrdinal("id_tipoDocumento")) ? dr.GetInt16(dr.GetOrdinal("id_tipoDocumento")) : 0;
                    Flete_Tipo.Clave = !dr.IsDBNull(dr.GetOrdinal("clave")) ? dr.GetString(dr.GetOrdinal("clave")) : string.Empty;
                    //Solo para mostrar
                    Flete_Tipo.Imagen = !dr.IsDBNull(dr.GetOrdinal("imagen")) ? dr.GetString(dr.GetOrdinal("imagen")) : string.Empty;
                }
                if (string.IsNullOrEmpty(Flete_Tipo.Imagen))
                {
                    //No hay imagen en el server
                    Flete_Tipo.MostrarImagen = Auxiliar.SetDefaultImage();
                    Flete_Tipo.FlagImg = false;
                }
                else
                {
                    //Guardamos el string de la imagen
                    Flete_Tipo.MostrarImagen = Flete_Tipo.Imagen;
                    Flete_Tipo.FlagImg = true;
                }

                Flete_Tipo.ExtensionImagenBase64 = Auxiliar.ObtenerExtensionImagenBase64(Flete_Tipo.MostrarImagen);
                dr.Close();
                return Flete_Tipo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Get EventoXIDEvento
        public EventoEnvioModels GetEventoXIDEventoXIDFlete(EventoEnvioModels Evento)
        {
            try
            {
                object[] parametros =
                {
                     Evento.IDEvento,
                     Evento.IDEnvio
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Evento.Conexion, "spCSLDB_Flete_get_EventoXIDEventoXIDFlete", parametros);

                while (dr.Read())
                {
                    Evento.IDEvento = !dr.IsDBNull(dr.GetOrdinal("id_evento")) ? dr.GetInt32(dr.GetOrdinal("id_evento")) : 0;
                    Evento.IDEnvio = !dr.IsDBNull(dr.GetOrdinal("id_flete")) ? dr.GetString(dr.GetOrdinal("id_flete")) : string.Empty;
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
                dr.Close();
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
                dr.Close();
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
                dr.Close();
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
                dr.Close();
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
                dr.Close();
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
                dr.Close();
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
                dr.Close();
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
                dr.Close();
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
                dr.Close();
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
                dr.Close();
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
                dr.Close();
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
                dr.Close();
                return Flete.Remolque.listaRemolque;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CatTipoDocumentoModels> GetListaTiposDocumentos(Flete_TipoDocumentoModels Flete)
        {
            try
            {
                CatTipoDocumentoModels TipoDocumento;
                List<CatTipoDocumentoModels> ListaTipoDocumentos = new List<CatTipoDocumentoModels>();
                object[] parametro =
                {
                    Flete.IDFlete
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Flete.Conexion, "spCSLDB_Combo_get_CatTipoDocumentoXIDFlete", parametro);
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
        #region Sucursales
        public List<CatSucursalesModels> GetListadoSucursales(FleteModels Flete)
        {
            List<CatSucursalesModels> ListaSucursales = new List<CatSucursalesModels>();
            CatSucursalesModels Sucursal;
            SqlDataReader dr = null;
            object[] parametros =
            {
                1
            };

            dr = SqlHelper.ExecuteReader(Flete.Conexion, "spCSLDB_Combo_get_SucursalesXTipoEmpresa", parametros);

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
        #endregion

        #region Funciones AC
        #region AC_Cliente
        public RespuestaAjax Flete_ac_FleteCliente(FleteModels Flete)
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
                    ,Flete.kmInicialVehiculo
                    ,Flete.FechaTentativaEntrega
                    ,Flete.Usuario
                    ,Flete.Id_sucursal
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Flete.Conexion, "spCSLDB_Flete_ac_FleteCliente", parametros);
                RespuestaAjax RespuestaAjax = new RespuestaAjax();

                while (dr.Read())
                {
                    RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                    RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : true;
                }
                dr.Close();
                return RespuestaAjax;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Flete_ac_FleteTrayecto
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
                dr.Close();
                return Flete;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region A_Cobro
        public RespuestaAjax Flete_ac_FleteCobro(FleteModels Flete)
        {
            try
            {
                object[] parametros =
                {
                     Flete.id_flete
                    ,Flete.precioFlete
                    ,Flete.CondicionPago
                    ,Flete.MetodoPago.Clave
                    ,Flete.FormaPago.Clave
                    ,Flete.Usuario
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Flete.Conexion, "spCSLDB_Flete_ac_FleteCobro", parametros);
                RespuestaAjax RespuestaAjax = new RespuestaAjax();

                while (dr.Read())
                {
                    RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                    RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : true;
                }
                dr.Close();
                return RespuestaAjax;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region A_EstatusFleteXIDFlete
        public FleteModels Flete_a_CambiarEstatusFleteXIDFlete(FleteModels Flete)
        {
            try
            {
                object[] parametros =
                {
                     Flete.id_flete
                    ,Flete.Usuario
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Flete.Conexion, "spCSLDB_Flete_a_CambiarEstatusFleteXIDFlete", parametros);

                while (dr.Read())
                {
                    Flete.RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                    Flete.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : true;
                }
                dr.Close();
                return Flete;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Flete_ac_Documento
        public Flete_TipoDocumentoModels Flete_ac_Documento(Flete_TipoDocumentoModels FleteTipo)
        {
            try
            {
                object[] parametros =
                {
                     FleteTipo.IDDocumento
                    ,FleteTipo.IDFlete
                    ,FleteTipo.IDTipoDocumento
                    ,FleteTipo.Clave
                    ,FleteTipo.Imagen
                    ,FleteTipo.Usuario
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(FleteTipo.Conexion, "spCSLDB_Flete_ac_Documento", parametros);

                while (dr.Read())
                {
                    FleteTipo.RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                    FleteTipo.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : true;
                }
                dr.Close();
                return FleteTipo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Producto Ganado Externo
        public Flete_ProductoModels AC_ProductoGanadoExterno(Flete_ProductoModels ganado)
        {
            try
            {
                object[] parametros =
                {
                    ganado.ID_Producto,
                    ganado.ID_Flete,
                    ganado.NumArete,
                    ganado.Genero,
                    ganado.PesoAproximado,
                    ganado.Usuario
                };

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(ganado.Conexion, "spCSLDB_Flete_ac_ProductoGanadoExterno", parametros);

                while (dr.Read())
                {
                    ganado.RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                    ganado.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : true;
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                ganado.RespuestaAjax.Mensaje = ex.ToString();
                ganado.RespuestaAjax.Success = false;
            }

            return ganado;
        }
        public Flete_ProductoModels DEL_ProductoGanadoExterno(Flete_ProductoModels ganado)
        {
            try
            {
                object[] parametros =
                {
                    ganado.ID_Producto,
                    ganado.ID_Flete,
                    ganado.Usuario
                };

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(ganado.Conexion, "spCSLDB_Flete_del_ProductoGanadoExterno", parametros);

                while (dr.Read())
                {
                    ganado.RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                    ganado.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : true;
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                ganado.RespuestaAjax.Mensaje = ex.ToString();
                ganado.RespuestaAjax.Success = false;
            }

            return ganado;
        }
        #endregion
        #region Evento
        public EventoEnvioModels AC_Evento(EventoEnvioModels Evento)
        {
            try
            {
                object[] parametros =
                {
                   Evento.IDEvento      ,Evento.IDEnvio                 ,Evento.IDTipoEvento        ,Evento.Cantidad
                  ,Evento.Lugar         ,Evento.FechaDeteccion          ,Evento.HoraDetecccion      ,Evento.Observacion
                  ,Evento.ImagenBase64  ,Evento.ListaProductosEvento    ,Evento.Usuario
                };

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Evento.Conexion, "spCSLDB_Flete_ac_Evento", parametros);

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
                    Evento.IDEnvio,
                    Evento.Usuario
                };

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Evento.Conexion, "spCSLDB_Flete_del_Evento", parametros);

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
        #region Recepcion Destino
        public FleteModels AC_RecepcionDestino(FleteModels Flete)
        {
            try
            {
                object[] parametros =
                {
                   Flete.RecepcionDestino.id_recepcion,         Flete.RecepcionDestino.IDFlete,                 Flete.RecepcionDestino.kiloTotalRecibido,
                   Flete.RecepcionDestino.GanadosTotal,         Flete.RecepcionDestino.fechaLlegada,            Flete.RecepcionDestino.HoraLlegada,
                   Flete.RecepcionDestino.HoraDescarga,         Flete.RecepcionDestino.recibidoPor,             Flete.RecepcionDestino.ValijaSellada,        Flete.RecepcionDestino.RecepcionDocumentos,     Flete.RecepcionDestino.observacion,
                   Flete.RecepcionDestino.Usuario
                };

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Flete.RecepcionDestino.Conexion, "spCSLDB_Flete_ac_RecepcionDestino", parametros);
                Flete.RecepcionDestino.RespuestaAjax = new RespuestaAjax();
                while (dr.Read())
                {
                    Flete.RecepcionDestino.RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                    Flete.RecepcionDestino.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : true;
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                Flete.RecepcionDestino.RespuestaAjax.Mensaje = ex.ToString();
                Flete.RecepcionDestino.RespuestaAjax.Success = false;
            }

            return Flete;
        }
        #endregion
        #region Recepcion Origen
        public FleteModels AC_RecepcionOrigen(FleteModels Flete)
        {
            try
            {
                object[] parametros =
                {
                   Flete.RecepcionOrigen.IDRecepcionOrigen,        Flete.RecepcionOrigen.IDFlete,             Flete.RecepcionOrigen.KilometrajeFinal,
                   Flete.RecepcionOrigen.HoraLlegada,              Flete.RecepcionOrigen.FechaLlegada,        Flete.RecepcionOrigen.Observacion,
                   Flete.RecepcionOrigen.Usuario
                };

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Flete.RecepcionOrigen.Conexion, "spCSLDB_Flete_ac_RecepcionOrigen", parametros);
                Flete.RecepcionOrigen.RespuestaAjax = new RespuestaAjax();
                while (dr.Read())
                {
                    Flete.RecepcionOrigen.RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                    Flete.RecepcionOrigen.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : true;
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                Flete.RecepcionOrigen.RespuestaAjax.Mensaje = ex.ToString();
                Flete.RecepcionOrigen.RespuestaAjax.Success = false;
            }

            return Flete;
        }
        #endregion
        #endregion

        public Flete_TipoDocumentoModels Flete_del_DocumentoXIDDocumento(Flete_TipoDocumentoModels FleteTipo)
        {
            try
            {
                object[] parametros =
                {
                     FleteTipo.IDDocumento
                    ,FleteTipo.Usuario
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(FleteTipo.Conexion, "spCSLDB_Flete_del_DocumentoXIDDocumento", parametros);

                while (dr.Read())
                {
                    FleteTipo.RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                    FleteTipo.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : true;
                }
                dr.Close();
                return FleteTipo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Flete_ProductoModels Flete_c_ProductoGanado(Flete_ProductoModels ganado)
        {
            try
            {
                object[] parametros =
                {
                     ganado.ListaStringIDProductoSeleccionado
                    ,ganado.ID_Flete
                    ,ganado.Usuario
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(ganado.Conexion, "spCSLDB_Flete_c_ProductoGanadoPropio", parametros);
                ganado.RespuestaAjax = new RespuestaAjax();

                while (dr.Read())
                {
                    ganado.RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                    ganado.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : true;
                }
                dr.Close();
                return ganado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Flete_ProductoModels Flete_del_ProductoGanado(Flete_ProductoModels ganado)
        {
            try
            {
                object[] parametros =
                {
                     ganado.ListaStringIDProductoSeleccionado
                    ,ganado.ID_Flete
                    ,ganado.Usuario
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(ganado.Conexion, "spCSLDB_Flete_del_ProductoGanadoXIDGanadoIDFlete", parametros);
                ganado.RespuestaAjax = new RespuestaAjax();

                while (dr.Read())
                {
                    ganado.RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                    ganado.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : true;
                }
                dr.Close();
                return ganado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}