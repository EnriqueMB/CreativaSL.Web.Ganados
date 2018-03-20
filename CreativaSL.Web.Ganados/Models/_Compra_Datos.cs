using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace CreativaSL.Web.Ganados.Models
{
    public class _Compra_Datos
    {
        private CultureInfo CultureInfo = new CultureInfo("es-MX");
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

        /// <summary>
        /// Obtiene un listado de todos los proveedores dados de alta
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public List<CatProveedorModels> ObtenerListadoProveedores(CompraModels Compra)
        {
            try
            {
                List<CatProveedorModels> ListaProveedores = new List<CatProveedorModels>();

                CatProveedorModels Proveedor = new CatProveedorModels
                {
                    IDProveedor = "0",
                    NombreRazonSocial = "SELECCIONE UN PROVEEDOR"
                };
                ListaProveedores.Add(Proveedor);
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_COMPRAS_GetProveedores");
                while (dr.Read())
                {
                    Proveedor = new CatProveedorModels
                    {
                        IDProveedor = dr["id_proveedor"].ToString(),
                        NombreRazonSocial = dr["NombreProveedor"].ToString(),
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

        public CompraModels GetCompraPacta(CompraModels Compra)
        {
            object[] parametros =
            {
                Compra.IDProveedor
            };

            SqlDataReader SqlDataR = null;
            SqlDataR = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_COMPRAS_GetCompraPactada", parametros);
            CultureInfo CultureInfo = new CultureInfo("es-MX");

            while (SqlDataR.Read())
            {
                Compra.IDProveedor           = SqlDataR["id_proveedor"].ToString();
                Compra.GanadosPactadoMachos  = Int32.Parse(SqlDataR["ganadoPactadoMachos"].ToString());
                Compra.GanadosPactadoHembras = Int32.Parse(SqlDataR["ganadoPactadoHembras"].ToString());
                String fecha                 = SqlDataR["fechaHoraProgramada"].ToString();
                Compra.FechaHoraProgramada   = DateTime.Parse(fecha, CultureInfo);
            }
            return Compra;
        }
        public CompraModels GetFierros(CompraModels Compra)
        {
            object[] parametros =
            {
                Compra.IDCompra
            };

            SqlDataReader SqlDataR = null;
            SqlDataR = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_COMPRAS_GetFierros", parametros);

            CatFierroModels Fierro;

            while (SqlDataR.Read())
            {
                Fierro = new CatFierroModels
                {
                    IDFierro = SqlDataR["id_fierro"].ToString(),
                    NombreFierro = SqlDataR["nombreFierro"].ToString(),
                    ImgFierro = SqlDataR["imgFierro"].ToString()
                };

                Compra.ListaFierros.Add(Fierro);
            }
            return Compra;
        }
        public CompraModels GetFlete(CompraModels Compra)
        {
            object[] parametros =
            {
                Compra.IDCompra
            };

            SqlDataReader SqlDataR = null;
            SqlDataR = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_COMPRAS_GetFlete", parametros);

            while (SqlDataR.Read())
            {
                Compra.GuiaTransito = SqlDataR["guiaTransito"].ToString();
                Compra.CertZoosanitario = SqlDataR["certZoosanitario"].ToString();
                Compra.CertTuberculosis = SqlDataR["certTuberculosis"].ToString();
                Compra.CertBrucelosis = SqlDataR["certBrucelosis"].ToString();
                Compra.Flete.precioFlete = decimal.Parse(SqlDataR["precioFlete"].ToString(), CultureInfo);
                Compra.Chofer.Nombre = SqlDataR["nombreChofer"].ToString();
                Compra.Chofer.ApPaterno = SqlDataR["apPaternoChofer"].ToString();
                Compra.Chofer.ApMaterno = SqlDataR["apMaternoChofer"].ToString();
                Compra.Vehiculo.Modelo = SqlDataR["modeloVehiculo"].ToString();
                Compra.Vehiculo.Placas = SqlDataR["placaVehiculo"].ToString();
                Compra.TipoVehiculo.Descripcion = SqlDataR["descripcionTipoVehiculo"].ToString();
                Compra.Trayecto.id_lugarOrigen = SqlDataR["id_lugarOrigen"].ToString();
                Compra.Trayecto.id_lugarDestino = SqlDataR["id_lugarDestino"].ToString();
            }
            return Compra;
        }

        public List<CatChoferModels> GetChoferes(CompraModels Compra)
        {
            try
            {
                List<CatChoferModels> ListaChoferes = new List<CatChoferModels>();

                CatChoferModels Chofer = new CatChoferModels
                {
                    IDChofer = "0",
                    Nombre = "SELECCIONE UN PROVEEDOR"
                };
                ListaChoferes.Add(Chofer);
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_COMPRAS_GetChoferes");
                while (dr.Read())
                {
                    Chofer = new CatChoferModels
                    {
                        IDChofer = dr["id_chofer"].ToString(),
                        Nombre = dr["nombre"].ToString() + " " + dr["apPaterno"].ToString() + " " + dr["apMaterno"].ToString()
                    };
                    ListaChoferes.Add(Chofer);
                }
                return ListaChoferes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CatVehiculoModels> GetVehiculos(CompraModels Compra)
        {
            try
            {
                List<CatVehiculoModels> ListaVehiculos = new List<CatVehiculoModels>();

                CatVehiculoModels Vehiculo = new CatVehiculoModels
                {
                    IDVehiculo = "0",
                    nombreMarca = "SELECCIONE UN VEHICULOS"
                };
                ListaVehiculos.Add(Vehiculo);
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_COMPRAS_GetVehiculos");
                while (dr.Read())
                {
                    Vehiculo = new CatVehiculoModels
                    {
                        IDVehiculo = dr["id_vehiculo"].ToString(),
                        nombreMarca =  dr["marca"].ToString() + " " + dr["placa"].ToString(),
                        Modelo = dr["tipo"].ToString()
                    };
                    ListaVehiculos.Add(Vehiculo);
                }
                return ListaVehiculos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ResponseModel Guardar(CompraModels Compra)
        {
            var rm = new ResponseModel();
            try
            {
                if(string.IsNullOrEmpty(Compra.IDCompra))
                {
                    //Si esta vacío la compra la generamos un insert
                    object[] parametros =
                    {
                        Compra.IDProveedor,
                        Compra.GanadosPactadoMachos,
                        Compra.GanadosPactadoHembras,
                        Compra.GanadosPactadoTotal,
                        Compra.FechaHoraProgramada
                    };

                    SqlDataReader SqlDataR = null;
                    SqlDataR = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_COMPRAS_ObtenerInfoTab1", parametros);
                    

                    while (SqlDataR.Read())
                    {
                        Compra.IDProveedor = SqlDataR["id_proveedor"].ToString();
                        Compra.GanadosPactadoMachos = Int32.Parse(SqlDataR["ganadoPactadoMachos"].ToString());
                        Compra.GanadosPactadoHembras = Int32.Parse(SqlDataR["ganadoPactadoHembras"].ToString());
                        String fecha = SqlDataR["fechaHoraProgramada"].ToString();
                        Compra.FechaHoraProgramada = DateTime.Parse(fecha, CultureInfo);
                    }
                }
                else
                {
                    //En caso contrario es un update de la compra
                }
                rm.SetResponse(true);
            }
            catch (Exception)
            {
                throw;
            }

            return rm;
        }




        public CompraModels ObtenerLugares(CompraModels CompraModels, int opcion)
        {
            try
            {
                object[] parametros =
                {
                    opcion
                };

                DataSet Ds = null;
                Ds = SqlHelper.ExecuteDataset(CompraModels.Conexion, "spCSLDB_COMPRAS_getLugares", parametros);

                if (Ds != null)
                {
                    if (Ds.Tables.Count > 0)
                    {
                        if (Ds.Tables[0] != null)
                        {
                            CompraModels.TablaLugares = Ds.Tables[0];
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
    }
}