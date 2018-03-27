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

        #region Listados
        /// <summary>
        /// Obtiene un listado de todos los proveedores dados de alta en relacioón a la sucursal
        /// </summary>
        /// <param name="CompraModels"></param>
        /// <returns></returns>
        public List<CatProveedorModels> GetListadoProveedores(CompraModels Compra)
        {
            try
            {
                List<CatProveedorModels> ListaProveedores = new List<CatProveedorModels>();
                CatProveedorModels Proveedor = new CatProveedorModels
                {
                    IDProveedor = string.Empty,
                    NombreRazonSocial = "SELECCIONE UN PROVEEDOR"
                };
                ListaProveedores.Add(Proveedor);
                object[] parametros =
                {
                    Compra.Sucursal.IDSucursal
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_COMPRAS_GetListadoProveedores", parametros);
                while (dr.Read())
                {
                    Proveedor = new CatProveedorModels
                    {
                        IDProveedor = !dr.IsDBNull(dr.GetOrdinal("id_proveedor")) ? dr.GetString(dr.GetOrdinal("id_proveedor")) : string.Empty,
                        NombreRazonSocial = !dr.IsDBNull(dr.GetOrdinal("NombreProveedor")) ? dr.GetString(dr.GetOrdinal("NombreProveedor")) : string.Empty,
                    };
                    ListaProveedores.Add(Proveedor);
                }
                return ListaProveedores;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Obtiene un listado de todos los lugares en relación a la sucursal
        /// </summary>
        /// <param name="Compra"></param>
        /// <returns></returns>
        public List<CatLugarModels> GetListadoLugares(CompraModels Compra)
        {
            try
            {
                List<CatLugarModels> ListaLugares = new List<CatLugarModels>();
                CatLugarModels Lugar = new CatLugarModels();

                object[] parametros =
                {
                    Compra.Sucursal.IDSucursal
                };

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_COMPRAS_GetListadoLugares", parametros);
                while (dr.Read())
                {
                    Lugar = new CatLugarModels
                    {
                        id_lugar = !dr.IsDBNull(dr.GetOrdinal("id_lugar")) ? dr.GetString(dr.GetOrdinal("id_lugar")) : string.Empty,
                        descripcion = !dr.IsDBNull(dr.GetOrdinal("descripcion")) ? dr.GetString(dr.GetOrdinal("descripcion")) : string.Empty,
                        latitud = float.Parse(dr["gpsLatitud"].ToString()),
                        longitud = float.Parse(dr["gpsLongitud"].ToString()),
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
        /// <summary>
        /// Obtiene un listado de todos los choferes en relación a la sucursal
        /// </summary>
        /// <param name="Compra"></param>
        /// <returns></returns>
        public List<CatChoferModels> GetListadoChoferes(CompraModels Compra)
        {
            try
            {
                List<CatChoferModels> ListaChoferes = new List<CatChoferModels>();
                CatChoferModels Chofer = new CatChoferModels();

                Chofer = new CatChoferModels
                {
                    IDChofer = string.Empty,
                    Nombre = "SELECCIONE UN CHOFER"
                };
                ListaChoferes.Add(Chofer);

                object[] parametros =
                {
                    Compra.Sucursal.IDSucursal
                };

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_COMPRAS_GetListadoChoferes", parametros);
                while (dr.Read())
                {
                    Chofer = new CatChoferModels
                    {
                        IDChofer = !dr.IsDBNull(dr.GetOrdinal("id_chofer")) ? dr.GetString(dr.GetOrdinal("id_chofer")) : string.Empty,
                        Nombre = !dr.IsDBNull(dr.GetOrdinal("Nombre")) ? dr.GetString(dr.GetOrdinal("Nombre")) : string.Empty,
                        ApPaterno = !dr.IsDBNull(dr.GetOrdinal("ApPaterno")) ? dr.GetString(dr.GetOrdinal("ApPaterno")) : string.Empty,
                        ApMaterno = !dr.IsDBNull(dr.GetOrdinal("ApMaterno")) ? dr.GetString(dr.GetOrdinal("ApMaterno")) : string.Empty
                    };
                    Chofer.Nombre = Chofer.ApPaterno + " " + Chofer.ApMaterno + " " + Chofer.Nombre;
                    ListaChoferes.Add(Chofer);
                }
                return ListaChoferes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Obtiene un listado de todos los vehículos en relación a la sucursal
        /// </summary>
        /// <param name="Compra"></param>
        /// <returns></returns>
        public List<CatVehiculoModels> GetListadoVehiculos(CompraModels Compra)
        {
            try
            {
                object[] parametros =
                {
                    Compra.Sucursal.IDSucursal
                };
                List<CatVehiculoModels> ListaVehiculos = new List<CatVehiculoModels>();
                CatVehiculoModels Vehiculo = new CatVehiculoModels
                {
                    IDVehiculo = string.Empty,
                    nombreMarca = "SELECCIONE UN VEHICULOS",
                    Modelo = "GRUPO OCAMPO"
                };
                ListaVehiculos.Add(Vehiculo);
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_COMPRAS_GetListadoVehiculos", parametros);
                while (dr.Read())
                {
                    Vehiculo = new CatVehiculoModels
                    {
                        //!dr.IsDBNull(dr.GetOrdinal("NombreProveedor")) ? dr.GetString(dr.GetOrdinal("NombreProveedor")) : string.Empty,
                        IDVehiculo = !dr.IsDBNull(dr.GetOrdinal("id_vehiculo")) ? dr.GetString(dr.GetOrdinal("id_vehiculo")) : string.Empty,
                        nombreMarca = !dr.IsDBNull(dr.GetOrdinal("marca")) ? dr.GetString(dr.GetOrdinal("marca")) : string.Empty,
                        Placas = !dr.IsDBNull(dr.GetOrdinal("placa")) ? dr.GetString(dr.GetOrdinal("placa")) : string.Empty,
                        Modelo = !dr.IsDBNull(dr.GetOrdinal("tipo")) ? dr.GetString(dr.GetOrdinal("tipo")) : string.Empty
                    };
                    Vehiculo.nombreMarca = Vehiculo.nombreMarca + " |" + Vehiculo.Placas;
                    ListaVehiculos.Add(Vehiculo);
                }
                return ListaVehiculos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Obtiene un listado de todos las jaulas en relación a la sucursal
        /// </summary>
        /// <param name="Compra"></param>
        /// <returns></returns>
        public List<CatJaulaModels> GetListadoJaulas(CompraModels Compra)
        {
            try
            {
                object[] parametros =
                {
                    Compra.Sucursal.IDSucursal
                };
                List<CatJaulaModels> ListaJaulas = new List<CatJaulaModels>();
                CatJaulaModels Jaula = new CatJaulaModels
                {
                    IDJaula = string.Empty,
                    Matricula = "SELECCIONE UNA JAULA",
                };
                ListaJaulas.Add(Jaula);
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_COMPRAS_GetListadoJaulas", parametros);
                while (dr.Read())
                {
                    Jaula = new CatJaulaModels
                    {
                        IDJaula = !dr.IsDBNull(dr.GetOrdinal("id_jaula")) ? dr.GetString(dr.GetOrdinal("id_jaula")) : string.Empty,
                        Matricula = !dr.IsDBNull(dr.GetOrdinal("matricula")) ? dr.GetString(dr.GetOrdinal("matricula")) : string.Empty
                    };
                    ListaJaulas.Add(Jaula);
                }
                return ListaJaulas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Obtiene un listado de todos los fierros en relación a una compra
        /// </summary>
        /// <param name="Compra"></param>
        /// <returns></returns>
        public List<CatFierroModels> GetListadoFierros(CompraModels Compra)
        {
            object[] parametros =
            {
                Compra.IDCompra
            };

            SqlDataReader dr = null;
            dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_COMPRAS_GetListadoFierros", parametros);

            CatFierroModels Fierro;

            while (dr.Read())
            {
                Fierro = new CatFierroModels
                {
                    IDFierro = !dr.IsDBNull(dr.GetOrdinal("id_fierro")) ? dr.GetString(dr.GetOrdinal("id_fierro")) : string.Empty,
                    NombreFierro = !dr.IsDBNull(dr.GetOrdinal("nombreFierro")) ? dr.GetString(dr.GetOrdinal("nombreFierro")) : string.Empty,
                    ImgFierro = !dr.IsDBNull(dr.GetOrdinal("imgFierro")) ? dr.GetString(dr.GetOrdinal("imgFierro")) : string.Empty,
                };

                Compra.ListaFierros.Add(Fierro);
            }
            return Compra.ListaFierros;
        }
        /// <summary>
        /// Obtiene un listado de todos las sucursales
        /// </summary>
        /// <param name="Compra"></param>
        /// <returns></returns>
        public List<CatSucursalesModels> GetListadoSucusales(CompraModels Compra)
        {
            SqlDataReader dr = null;
            dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_COMPRAS_GetListadoSucursales");

            
            while (dr.Read())
            {
                Compra.Sucursal = new CatSucursalesModels
                {
                    IDSucursal = !dr.IsDBNull(dr.GetOrdinal("id_sucursal")) ? dr.GetString(dr.GetOrdinal("id_sucursal")) : string.Empty,
                    NombreSucursal = !dr.IsDBNull(dr.GetOrdinal("nombreSuc")) ? dr.GetString(dr.GetOrdinal("nombreSuc")) : string.Empty,
                };

                Compra.ListaSucursales.Add(Compra.Sucursal);
            }
            return Compra.ListaSucursales;
        }

        #endregion
        #region Index
        /// <summary>
        /// Obtiene los datos a mostar en el modulo COMPRAS->INDEX
        /// </summary>
        /// <param name="CompraModels"></param>
        /// <returns></returns>
        public CompraModels ObtenerCompraIndex(CompraModels CompraModels)
        {
            try
            {
                DataSet Ds = null;
                Ds = SqlHelper.ExecuteDataset(CompraModels.Conexion, "spCSLDB_COMPRAS_IndexVentas");
                if (Ds != null)
                {
                    if (Ds.Tables.Count > 0)
                    {
                        if (Ds.Tables[0] != null)
                        {
                            CompraModels.TablaCompra = Ds.Tables[0];
                        }
                    }
                }
                return CompraModels;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Insert
        public CompraModels SaveCompra(CompraModels Compra)
        {
            //Sumamos ganado macho y hembra para obtener el total;
            Compra.SumarGanadoPactado();
            object[] parametros =
            {
                //Sucursal seleccionada
                Compra.Sucursal.IDSucursal,
                //Compra
                Compra.IDUsuario,
                Compra.IDProveedor,
                Compra.GuiaTransito,
                //Ganado
                Compra.GanadosPactadoMachos,
                Compra.GanadosPactadoHembras,
                Compra.GanadosPactadoTotal,
                Compra.FechaHoraProgramada,
                //Certificados
                Compra.CertZoosanitario,
                Compra.CertTuberculosis,
                Compra.CertBrucelosis,
                //Datos del flete
                Compra.Flete.id_chofer,
                Compra.Flete.id_vehiculo,
                Compra.Flete.id_jaula,
                Compra.Flete.kmInicialVehiculo,
                //Datos del trayecto
                Compra.Trayecto.id_lugarOrigen,
                Compra.Trayecto.id_lugarDestino
            };

            SqlDataReader dr = null;
            dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_COMPRAS_SaveCompra", parametros);
            while (dr.Read())
            {
                Compra.IDCompra = !dr.IsDBNull(dr.GetOrdinal("id_compra")) ? dr.GetString(dr.GetOrdinal("id_compra")) : string.Empty;
            }
            return Compra;
        }
        #endregion
        
        #region Edit

        #endregion
        #region Delete

        #endregion
        
        /// <summary>
        /// Obteniene los datos de la tabla Compra, por medio del id de la compra
        /// </summary>
        /// <param name="Compra"></param>
        /// <returns></returns>
        public CompraModels GetCompra(CompraModels Compra)
        {
            try
            {
                object[] parametros =
                {
                    Compra.IDCompra
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_COMPRAS_GetCompra", parametros);
                CultureInfo CultureInfo = new CultureInfo("es-MX");

                while (dr.Read())
                {
                    Compra.IDDocumentoXPagar = !dr.IsDBNull(dr.GetOrdinal("id_documentoXPagar")) ? dr.GetString(dr.GetOrdinal("id_documentoXPagar")) : string.Empty;
                    Compra.IDFlete = !dr.IsDBNull(dr.GetOrdinal("id_flete")) ? dr.GetString(dr.GetOrdinal("id_flete")) : string.Empty;
                    Compra.IDProveedor = !dr.IsDBNull(dr.GetOrdinal("id_proveedor")) ? dr.GetString(dr.GetOrdinal("id_proveedor")) : string.Empty;
                    Compra.IDRecepcion = !dr.IsDBNull(dr.GetOrdinal("id_recepcion")) ? dr.GetString(dr.GetOrdinal("id_recepcion")) : string.Empty;
                    Compra.IDSucursal = !dr.IsDBNull(dr.GetOrdinal("id_sucursal")) ? dr.GetString(dr.GetOrdinal("id_sucursal")) : string.Empty;
                    Compra.CertBrucelosis = !dr.IsDBNull(dr.GetOrdinal("certBrucelosis")) ? dr.GetString(dr.GetOrdinal("certBrucelosis")) : string.Empty;
                    Compra.CertTuberculosis = !dr.IsDBNull(dr.GetOrdinal("certTuberculosis")) ? dr.GetString(dr.GetOrdinal("certTuberculosis")) : string.Empty;
                    Compra.CertZoosanitario = !dr.IsDBNull(dr.GetOrdinal("certZoosanitario")) ? dr.GetString(dr.GetOrdinal("certZoosanitario")) : string.Empty;
                    Compra.FechaHoraTerminada = !dr.IsDBNull(dr.GetOrdinal("fechaHoraTerminada")) ? dr.GetDateTime(dr.GetOrdinal("fechaHoraTerminada")) : DateTime.Now;
                    Compra.FechaHoraProgramada = !dr.IsDBNull(dr.GetOrdinal("fechaHoraProgramada")) ? dr.GetDateTime(dr.GetOrdinal("fechaHoraProgramada")) : DateTime.Now;
                    Compra.GanadosPactadoHembras = !dr.IsDBNull(dr.GetOrdinal("ganadoPactadoHembras")) ? dr.GetInt32(dr.GetOrdinal("ganadoPactadoHembras")) : 0;
                    Compra.GanadosPactadoMachos = !dr.IsDBNull(dr.GetOrdinal("ganadoPactadoMachos")) ? dr.GetInt32(dr.GetOrdinal("ganadoPactadoMachos")) : 0;
                    Compra.GanadosPactadoTotal = !dr.IsDBNull(dr.GetOrdinal("ganadoPactadoTotal")) ? dr.GetInt32(dr.GetOrdinal("ganadoPactadoTotal")) : 0;
                    Compra.GanadosCompradoMachos = !dr.IsDBNull(dr.GetOrdinal("ganadoCompradoMachos")) ? dr.GetInt32(dr.GetOrdinal("ganadoCompradoMachos")) : 0;
                    Compra.GanadosCompradoHembras = !dr.IsDBNull(dr.GetOrdinal("ganadoCompradoHembras")) ? dr.GetInt32(dr.GetOrdinal("ganadoCompradoHembras")) : 0;
                    Compra.GanadosCompradoTotal = !dr.IsDBNull(dr.GetOrdinal("ganadoCompradoTotal")) ? dr.GetInt32(dr.GetOrdinal("ganadoCompradoTotal")) : 0;
                    Compra.GuiaTransito = !dr.IsDBNull(dr.GetOrdinal("guiaTransito")) ? dr.GetString(dr.GetOrdinal("guiaTransito")) : string.Empty;
                    Compra.KilosTotal = !dr.IsDBNull(dr.GetOrdinal("kilosTotal")) ? dr.GetDecimal(dr.GetOrdinal("kilosTotal")) : 0;
                    Compra.MermaPromedio = !dr.IsDBNull(dr.GetOrdinal("mermaPromedio")) ? dr.GetDecimal(dr.GetOrdinal("mermaPromedio")) : 0;
                    Compra.MontoPagado = !dr.IsDBNull(dr.GetOrdinal("montoPagado")) ? dr.GetDecimal(dr.GetOrdinal("montoPagado")) : 0;
                    Compra.MontoPorPagar = !dr.IsDBNull(dr.GetOrdinal("montoPorPagar")) ? dr.GetDecimal(dr.GetOrdinal("montoPorPagar")) : 0;
                    Compra.MontoTotal = !dr.IsDBNull(dr.GetOrdinal("montoTotal")) ? dr.GetDecimal(dr.GetOrdinal("montoTotal")) : 0;
                    Compra.Sucursal.MermaPredeterminada = !dr.IsDBNull(dr.GetOrdinal("mermaPredeterminada")) ? dr.GetDecimal(dr.GetOrdinal("mermaPredeterminada")) : 0;
                    Compra.Flete.id_vehiculo = !dr.IsDBNull(dr.GetOrdinal("id_vehiculo")) ? dr.GetString(dr.GetOrdinal("id_vehiculo")) : string.Empty;
                    Compra.Flete.id_chofer = !dr.IsDBNull(dr.GetOrdinal("id_chofer")) ? dr.GetString(dr.GetOrdinal("id_chofer")) : string.Empty;
                    Compra.Flete.id_jaula = !dr.IsDBNull(dr.GetOrdinal("id_jaula")) ? dr.GetString(dr.GetOrdinal("id_jaula")) : string.Empty;
                    Compra.Flete.kmInicialVehiculo = !dr.IsDBNull(dr.GetOrdinal("kmInicialVehiculo")) ? dr.GetInt32(dr.GetOrdinal("kmInicialVehiculo")) : 0;
                    Compra.Flete.kmFinalVehiculo = !dr.IsDBNull(dr.GetOrdinal("kmFinalVehiculo")) ? dr.GetInt32(dr.GetOrdinal("kmFinalVehiculo")) : 0;
                    Compra.Trayecto.id_lugarOrigen = !dr.IsDBNull(dr.GetOrdinal("id_lugarOrigen")) ? dr.GetString(dr.GetOrdinal("id_lugarOrigen")) : string.Empty;
                    Compra.Trayecto.id_lugarDestino = !dr.IsDBNull(dr.GetOrdinal("id_lugarDestino")) ? dr.GetString(dr.GetOrdinal("id_lugarDestino")) : string.Empty;
                }

                return Compra;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public CompraModels GetCompraGanadoXIDGanado(CompraModels Compra)
        {
            try
            {
                object[] parametros =
                {
                    Compra.Ganado.id_Ganados
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_COMPRAS_GetCompraGanadoXIDGanado", parametros);
                while (dr.Read())
                {
                    Compra.CompraGanado.IDCompra = !dr.IsDBNull(dr.GetOrdinal("id_compra")) ? dr.GetString(dr.GetOrdinal("id_compra")) : string.Empty;
                    Compra.Ganado.numArete = !dr.IsDBNull(dr.GetOrdinal("numArete")) ? dr.GetString(dr.GetOrdinal("numArete")) : string.Empty;
                    Compra.CompraGanado.Merma = !dr.IsDBNull(dr.GetOrdinal("merma")) ? dr.GetDecimal(dr.GetOrdinal("merma")) : 0;
                    Compra.CompraGanado.PesoFinal = !dr.IsDBNull(dr.GetOrdinal("pesoFinal")) ? dr.GetDecimal(dr.GetOrdinal("pesoFinal")) : 0;
                    Compra.CompraGanado.PesoInicial = !dr.IsDBNull(dr.GetOrdinal("pesoInicial")) ? dr.GetDecimal(dr.GetOrdinal("pesoInicial")) : 0;
                    Compra.CompraGanado.PesoPagado = !dr.IsDBNull(dr.GetOrdinal("PesoPagado")) ? dr.GetDecimal(dr.GetOrdinal("PesoPagado")) : 0;
                    Compra.CompraGanado.PrecioKilo = !dr.IsDBNull(dr.GetOrdinal("precioKilo")) ? dr.GetDecimal(dr.GetOrdinal("precioKilo")) : 0;
                    Compra.CompraGanado.DiferenciaPeso = !dr.IsDBNull(dr.GetOrdinal("DiferenciaPeso")) ? dr.GetDecimal(dr.GetOrdinal("DiferenciaPeso")) : 0;
                }
                return Compra;
            }
            catch (Exception ex)
            {

                throw ex;
            }
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
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_COMPRAS_GetGanadoXGanadoDetalle", parametros);

                SqlDataReaderExtension ext = new SqlDataReaderExtension();
                return  ext.ToJson(ref dr);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

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
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_COMPRAS_DeleteImageFierro", parametros);
                while (dr.Read())
                {
                    Compra.Mensaje = !dr.IsDBNull(dr.GetOrdinal("Mensaje")) ? dr.GetString(dr.GetOrdinal("Mensaje")) : string.Empty;
                }
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
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_COMPRAS_SaveImageFierro", parametros);
                while (dr.Read())
                {
                    Compra.Fierro.IDFierro = !dr.IsDBNull(dr.GetOrdinal("id_fierro")) ? dr.GetString(dr.GetOrdinal("id_fierro")) : string.Empty;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Compra;
        }
    }
}