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
                        id_lugar = dr["id_lugar"].ToString(),
                        descripcion = dr["descripcion"].ToString(),
                        lat = dr["gpsLatitud"].ToString(),
                        lng = dr["gpsLongitud"].ToString()
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
                        IDChofer = dr["id_chofer"].ToString(),
                        Nombre = dr["ApPaterno"].ToString() + " " + dr["ApMaterno"].ToString() + " " + dr["Nombre"].ToString() ,
                        ApMaterno = dr["ApMaterno"].ToString(),
                        ApPaterno = dr["ApPaterno"].ToString()
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
                        IDVehiculo = dr["id_vehiculo"].ToString(),
                        nombreMarca = dr["marca"].ToString() + " " + dr["placa"].ToString(),
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
                        IDJaula = dr["id_jaula"].ToString(),
                        Matricula = dr["matricula"].ToString()
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
            Compra.SumarGanado();
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

            SqlDataReader SqlDataR = null;
            SqlDataR = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_COMPRAS_SaveCompra", parametros);
            

            while (SqlDataR.Read())
            {
                Compra.IDCompra = SqlDataR["id_compra"].ToString();
                //Compra.Resultado = Int32.Parse(SqlDataR["result"].ToString());
            }
            return Compra;
        }
        #endregion

      
        #region Edit

        #endregion
        #region Delete

        #endregion



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
    }
}