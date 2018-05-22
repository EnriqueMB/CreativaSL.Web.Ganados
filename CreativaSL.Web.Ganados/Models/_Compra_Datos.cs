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

        public List<CompraModels> GetListaComprasNofinalizadas(CompraModels Compra)
        {

            try
            {
                List<CompraModels> Lista = new List<CompraModels>();
                CompraModels Item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Inicio_get_ComprasNoFinalizadas");
                while (dr.Read())
                {
                    Item = new CompraModels();
                    Item.IDProveedor = !dr.IsDBNull(dr.GetOrdinal("nombreRazonSocial")) ? dr.GetString(dr.GetOrdinal("nombreRazonSocial")) : string.Empty;
                    Item.FechaHoraProgramada = !dr.IsDBNull(dr.GetOrdinal("fechaHoraProgramada")) ? dr.GetDateTime(dr.GetOrdinal("fechaHoraProgramada")) : DateTime.Now;
                    Item.Estatus= !dr.IsDBNull(dr.GetOrdinal("estatus")) ? dr.GetInt16(dr.GetOrdinal("estatus")) : 0;
                    Item.GanadosPactadoMachos= !dr.IsDBNull(dr.GetOrdinal("ganadoPactadoMachos")) ? dr.GetInt32(dr.GetOrdinal("ganadoPactadoMachos")) : 0;
                    Item.GanadosPactadoHembras = !dr.IsDBNull(dr.GetOrdinal("ganadoPactadoHembras")) ? dr.GetInt32(dr.GetOrdinal("ganadoPactadoHembras")) : 0;
                    Item.GuiaTransito= !dr.IsDBNull(dr.GetOrdinal("guiaTransito")) ? dr.GetString(dr.GetOrdinal("guiaTransito")) : string.Empty;
                    Item.estatusDesc = !dr.IsDBNull(dr.GetOrdinal("estatusDesc")) ? dr.GetString(dr.GetOrdinal("estatusDesc")) : string.Empty;
                    Lista.Add(Item);
                }
                Compra.listaCompra = Lista;
                return Compra.listaCompra;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Json Datatables
        public SqlDataReader GetDocumentosDataTable(CompraModels Compra)
        {
            object[] parametros =
            {
                Compra.IDFlete
            };

            try
            {
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Flete_get_DocumentosXIDFlete", parametros);
                return dr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region Combos
        public List<CatSucursalesModels> GetListadoSucursales(CompraModels Compra)
        {
            CatSucursalesModels Sucursal;
            SqlDataReader dr = null;
            object[] parametros =
            {
                1
            };

            dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Combo_get_SucursalesXTipoEmpresa", parametros);

            while (dr.Read())
            {
                Sucursal = new CatSucursalesModels
                {
                    IDSucursal = !dr.IsDBNull(dr.GetOrdinal("IDSucursal")) ? dr.GetString(dr.GetOrdinal("IDSucursal")) : string.Empty,
                    NombreSucursal = !dr.IsDBNull(dr.GetOrdinal("NombreSucursal")) ? dr.GetString(dr.GetOrdinal("NombreSucursal")) : string.Empty,
                };

                Compra.ListaSucursales.Add(Sucursal);
            }
            return Compra.ListaSucursales;
        }
        public List<CatProveedorModels> GetListaProveedores(CompraModels Compra)
        {
            try
            {
                CatProveedorModels Proveedor;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "[spCSLDB_Combo_get_CatProveedor]");
                while (dr.Read())
                {
                    Proveedor = new CatProveedorModels
                    {
                        IDProveedor = !dr.IsDBNull(dr.GetOrdinal("IDProveedor")) ? dr.GetString(dr.GetOrdinal("IDProveedor")) : string.Empty,
                        NombreRazonSocial = !dr.IsDBNull(dr.GetOrdinal("NombreProveedor")) ? dr.GetString(dr.GetOrdinal("NombreProveedor")) : string.Empty,

                    };
                    Compra.ListaProveedores.Add(Proveedor);
                }
                return Compra.ListaProveedores;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CatLugarModels> GetListadoLugares(CompraModels Compra)
        {
            try
            {
                List<CatLugarModels> ListaLugares = new List<CatLugarModels>();
                CatLugarModels Lugar = new CatLugarModels();

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Combo_get_AllCatLugar");
                while (dr.Read())
                {
                    Lugar = new CatLugarModels
                    {
                        id_lugar = !dr.IsDBNull(dr.GetOrdinal("IDLugar")) ? dr.GetString(dr.GetOrdinal("IDLugar")) : string.Empty,
                        descripcion = !dr.IsDBNull(dr.GetOrdinal("NombreLugar")) ? dr.GetString(dr.GetOrdinal("NombreLugar")) : string.Empty,
                        latitud = float.Parse(dr["GpsLatitud"].ToString()),
                        longitud = float.Parse(dr["GpsLongitud"].ToString()),
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
        public List<CatLugarModels> GetListadoLugaresProveedorXIDProveedor(CompraModels Compra)
        {
            try
            {
                CatLugarModels Lugar = new CatLugarModels();
                object[] parametros = 
                {
                    Compra.IDProveedor
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Combo_get_CatLugarProvedorXIDProveedor", parametros);
                while (dr.Read())
                {
                    Lugar = new CatLugarModels
                    {
                        id_lugar = !dr.IsDBNull(dr.GetOrdinal("IDLugar")) ? dr.GetString(dr.GetOrdinal("IDLugar")) : string.Empty,
                        descripcion = !dr.IsDBNull(dr.GetOrdinal("NombreLugar")) ? dr.GetString(dr.GetOrdinal("NombreLugar")) : string.Empty,
                        latitud = float.Parse(dr["GpsLatitud"].ToString()),
                        longitud = float.Parse(dr["GpsLongitud"].ToString()),
                        Direccion = !dr.IsDBNull(dr.GetOrdinal("Direccion")) ? dr.GetString(dr.GetOrdinal("Direccion")) : string.Empty
                    };

                    Compra.ListaLugaresProveedor.Add(Lugar);
                }
                return Compra.ListaLugaresProveedor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CatEmpresaModels> GetListadoEmpresas(CompraModels Compra)
        {
            try
            {
                CatEmpresaModels Empresa;

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Combo_get_CatEmpresa");
                while (dr.Read())
                {
                    Empresa = new CatEmpresaModels
                    {
                        IDEmpresa = !dr.IsDBNull(dr.GetOrdinal("IDEmpresa")) ? dr.GetString(dr.GetOrdinal("IDEmpresa")) : string.Empty,
                        RazonFiscal = !dr.IsDBNull(dr.GetOrdinal("NombreEmpresa")) ? dr.GetString(dr.GetOrdinal("NombreEmpresa")) : string.Empty,
                    };

                    Compra.ListaEmpresas.Add(Empresa);
                }
                return Compra.ListaEmpresas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CatChoferModels> GetChoferesXIDEmpresa(CompraModels Compra)
        {
            try
            {
                object[] parametros =
                {
                    Compra.IDEmpresa
                };
                CatChoferModels Chofer;

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Combo_get_CatChoferesXIDEmpresa", parametros);
                while (dr.Read())
                {
                    Chofer = new CatChoferModels
                    {
                        IDChofer = !dr.IsDBNull(dr.GetOrdinal("IDChofer")) ? dr.GetString(dr.GetOrdinal("IDChofer")) : string.Empty,
                        Nombre = !dr.IsDBNull(dr.GetOrdinal("NombreCompleto")) ? dr.GetString(dr.GetOrdinal("NombreCompleto")) : string.Empty,
                    };

                    Compra.ListaChoferes.Add(Chofer);
                }
                return Compra.ListaChoferes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CatVehiculoModels> GetVehiculosXIDEmpresa(CompraModels Compra)
        {
            try
            {
                object[] parametros =
                {
                    Compra.IDEmpresa
                };
                CatVehiculoModels Vehiculo;

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Combo_get_CatVehiculosXIDEmpresa", parametros);
                while (dr.Read())
                {
                    Vehiculo = new CatVehiculoModels
                    {
                        IDVehiculo = !dr.IsDBNull(dr.GetOrdinal("IDVehiculo")) ? dr.GetString(dr.GetOrdinal("IDVehiculo")) : string.Empty,
                        nombreMarca = !dr.IsDBNull(dr.GetOrdinal("NombreVehiculo")) ? dr.GetString(dr.GetOrdinal("NombreVehiculo")) : string.Empty,
                        Modelo = !dr.IsDBNull(dr.GetOrdinal("Tipo")) ? dr.GetString(dr.GetOrdinal("Tipo")) : string.Empty
                    };

                    Compra.ListaVehiculos.Add(Vehiculo);
                }
                return Compra.ListaVehiculos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CatJaulaModels> GetJaulasXIDEmpresa(CompraModels Compra)
        {
            try
            {
                object[] parametros =
                {
                    Compra.IDEmpresa
                };
                CatJaulaModels Jaula;

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Combo_get_CatJaulaXIDEmpresa", parametros);
                while (dr.Read())
                {
                    Jaula = new CatJaulaModels
                    {
                        IDJaula = !dr.IsDBNull(dr.GetOrdinal("IDJaula")) ? dr.GetString(dr.GetOrdinal("IDJaula")) : string.Empty,
                        Matricula = !dr.IsDBNull(dr.GetOrdinal("Placa")) ? dr.GetString(dr.GetOrdinal("Placa")) : string.Empty,
                    };

                    Compra.ListaJaulas.Add(Jaula);
                }
                return Compra.ListaJaulas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CatRemolqueModels> GetRemolquesXIDEmpresa(CompraModels Compra)
        {
            try
            {
                object[] parametros =
                {
                    Compra.IDEmpresa
                };
                CatRemolqueModels Remolque;

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Combo_get_CatRemolqueXIDEmpresa", parametros);
                while (dr.Read())
                {
                    Remolque = new CatRemolqueModels
                    {
                        IDRemolque = !dr.IsDBNull(dr.GetOrdinal("IDRemolque")) ? dr.GetString(dr.GetOrdinal("IDRemolque")) : string.Empty,
                        placa = !dr.IsDBNull(dr.GetOrdinal("Placa")) ? dr.GetString(dr.GetOrdinal("Placa")) : string.Empty,
                    };

                    Compra.ListaRemolques.Add(Remolque);
                }
                return Compra.ListaRemolques;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CatEstatusGanadoModels> GetListadoEstatusGanado(CompraModels Compra)
        {
            CatEstatusGanadoModels EstatusGanado;
            SqlDataReader dr = null;
            
            dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Combo_get_CatEstatusGanado");

            while (dr.Read())
            {
                EstatusGanado = new CatEstatusGanadoModels
                {
                    id_estatusGanado = !dr.IsDBNull(dr.GetOrdinal("IDEstatusGanado")) ? dr.GetInt16(dr.GetOrdinal("IDEstatusGanado")) : 0,
                    descripcion = !dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? dr.GetString(dr.GetOrdinal("Descripcion")) : string.Empty,
                };

                Compra.ListaEstatusGanado.Add(EstatusGanado);
            }
            return Compra.ListaEstatusGanado;
        }
        public List<CatCostoFlete> GetListadoCostoFlete(CompraModels Compra)
        {
            CatCostoFlete CostoFlete;
            SqlDataReader dr = null;

            dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Combo_get_CatCostoFlete");

            while (dr.Read())
            {
                CostoFlete = new CatCostoFlete
                {
                    IDCostoFlete = !dr.IsDBNull(dr.GetOrdinal("IDCostoFlete")) ? dr.GetInt16(dr.GetOrdinal("IDCostoFlete")) : 0,
                    Descripcion = !dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? dr.GetString(dr.GetOrdinal("Descripcion")) : string.Empty,
                };

                Compra.ListaCostoFlete.Add(CostoFlete);
            }
            return Compra.ListaCostoFlete;
        }
        #endregion

        #region Get
        public int GetEstatusCompra(CompraModels Compra)
        {
            try
            {
                object[] parametros =
                {
                    Compra.IDCompra,
                };

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Compras_get_EstatusCompra", parametros);
                while (dr.Read())
                {
                    Compra.Estatus = !dr.IsDBNull(dr.GetOrdinal("estatus")) ? dr.GetInt16(dr.GetOrdinal("estatus")) : -1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Compra.Estatus;
        }
        public CompraModels GetCompraProgramada(CompraModels Compra)
        {
            try
            {
                object[] parametros =
                {
                    Compra.IDCompra
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Compras_get_CompraProgramada", parametros);

                while (dr.Read())
                {
                    Compra.IDSucursal = !dr.IsDBNull(dr.GetOrdinal("id_sucursal")) ? dr.GetString(dr.GetOrdinal("id_sucursal")) : string.Empty;
                    Compra.FechaHoraProgramada = !dr.IsDBNull(dr.GetOrdinal("fechaHoraProgramada")) ? dr.GetDateTime(dr.GetOrdinal("fechaHoraProgramada")) : DateTime.Now;
                    Compra.IDProveedor = !dr.IsDBNull(dr.GetOrdinal("id_proveedor")) ? dr.GetString(dr.GetOrdinal("id_proveedor")) : string.Empty;
                    Compra.IDPLugarProveedor = !dr.IsDBNull(dr.GetOrdinal("id_lugar_proveedor")) ? dr.GetString(dr.GetOrdinal("id_lugar_proveedor")) : string.Empty;
                    Compra.GanadosPactadoMachos = !dr.IsDBNull(dr.GetOrdinal("ganadoPactadoMachos")) ? dr.GetInt32(dr.GetOrdinal("ganadoPactadoMachos")) : 0;
                    Compra.GanadosPactadoHembras = !dr.IsDBNull(dr.GetOrdinal("ganadoPactadoHembras")) ? dr.GetInt32(dr.GetOrdinal("ganadoPactadoHembras")) : 0;
                    Compra.Folio = !dr.IsDBNull(dr.GetOrdinal("folio")) ? dr.GetString(dr.GetOrdinal("folio")) : string.Empty;
                }
                return Compra;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public CompraModels GetCompraEmbarque(CompraModels Compra)
        {
            try
            {
                object[] parametros =
                {
                    Compra.IDCompra
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Compras_get_CompraEmbarque", parametros);

                while (dr.Read())
                {
                    Compra.IDFlete = !dr.IsDBNull(dr.GetOrdinal("id_flete")) ? dr.GetString(dr.GetOrdinal("id_flete")) : string.Empty;
                    Compra.IDProveedor = !dr.IsDBNull(dr.GetOrdinal("id_proveedor")) ? dr.GetString(dr.GetOrdinal("id_proveedor")) : string.Empty;
                    Compra.IDEmpresa = !dr.IsDBNull(dr.GetOrdinal("id_empresa")) ? dr.GetString(dr.GetOrdinal("id_empresa")) : string.Empty;
                    Compra.IDChofer = !dr.IsDBNull(dr.GetOrdinal("id_chofer")) ? dr.GetString(dr.GetOrdinal("id_chofer")) : string.Empty;
                    Compra.IDVehiculo = !dr.IsDBNull(dr.GetOrdinal("id_vehiculo")) ? dr.GetString(dr.GetOrdinal("id_vehiculo")) : string.Empty;
                    Compra.IDJaula = !dr.IsDBNull(dr.GetOrdinal("id_jaula")) ? dr.GetString(dr.GetOrdinal("id_jaula")) : string.Empty;
                    Compra.IDRemolque = !dr.IsDBNull(dr.GetOrdinal("id_remolque")) ? dr.GetString(dr.GetOrdinal("id_remolque")) : string.Empty;
                    Compra.Flete.kmInicialVehiculo = !dr.IsDBNull(dr.GetOrdinal("kmInicialVehiculo")) ? dr.GetInt32(dr.GetOrdinal("kmInicialVehiculo")) : 0;
                    Compra.Trayecto.id_lugarOrigen = !dr.IsDBNull(dr.GetOrdinal("id_lugarOrigen")) ? dr.GetString(dr.GetOrdinal("id_lugarOrigen")) : string.Empty;
                    Compra.Trayecto.id_lugarDestino = !dr.IsDBNull(dr.GetOrdinal("id_lugarDestino")) ? dr.GetString(dr.GetOrdinal("id_lugarDestino")) : string.Empty;
                    Compra.IDPLugarProveedor = !dr.IsDBNull(dr.GetOrdinal("id_lugar_proveedor")) ? dr.GetString(dr.GetOrdinal("id_lugar_proveedor")) : string.Empty;
                }
                return Compra;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }







        public CompraModels GetCompra(CompraModels Compra)
        {
            try
            {
                object[] parametros =
                {
                    Compra.IDCompra
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Compras_get_Compra", parametros);

                while (dr.Read())
                {
                    Compra.IDProveedor = !dr.IsDBNull(dr.GetOrdinal("id_proveedor")) ? dr.GetString(dr.GetOrdinal("id_proveedor")) : string.Empty;
                    Compra.IDSucursal = !dr.IsDBNull(dr.GetOrdinal("id_sucursal")) ? dr.GetString(dr.GetOrdinal("id_sucursal")) : string.Empty;
                    Compra.IDFlete = !dr.IsDBNull(dr.GetOrdinal("id_flete")) ? dr.GetString(dr.GetOrdinal("id_flete")) : string.Empty;
                    Compra.GanadosPactadoMachos = !dr.IsDBNull(dr.GetOrdinal("ganadoPactadoMachos")) ? dr.GetInt32(dr.GetOrdinal("ganadoPactadoMachos")) : 0;
                    Compra.GanadosPactadoHembras = !dr.IsDBNull(dr.GetOrdinal("ganadoPactadoHembras")) ? dr.GetInt32(dr.GetOrdinal("ganadoPactadoHembras")) : 0;
                    Compra.GanadosPactadoTotal = !dr.IsDBNull(dr.GetOrdinal("ganadoTotalPactado")) ? dr.GetInt32(dr.GetOrdinal("ganadoTotalPactado")) : 0;
                    Compra.FechaHoraProgramada = !dr.IsDBNull(dr.GetOrdinal("fechaHoraProgramada")) ? dr.GetDateTime(dr.GetOrdinal("fechaHoraProgramada")) : DateTime.Now;
                    Compra.GuiaTransito = !dr.IsDBNull(dr.GetOrdinal("guiaTransito")) ? dr.GetString(dr.GetOrdinal("guiaTransito")) : string.Empty;
                    Compra.CertZoosanitario = !dr.IsDBNull(dr.GetOrdinal("certZoosanitario")) ? dr.GetString(dr.GetOrdinal("certZoosanitario")) : string.Empty;
                    Compra.CertTuberculosis = !dr.IsDBNull(dr.GetOrdinal("certTuberculosis")) ? dr.GetString(dr.GetOrdinal("certTuberculosis")) : string.Empty;
                    Compra.CertBrucelosis = !dr.IsDBNull(dr.GetOrdinal("certBrucelosis")) ? dr.GetString(dr.GetOrdinal("certBrucelosis")) : string.Empty;
                    Compra.IDEmpresa = !dr.IsDBNull(dr.GetOrdinal("id_empresa")) ? dr.GetString(dr.GetOrdinal("id_empresa")) : string.Empty;
                    Compra.IDChofer = !dr.IsDBNull(dr.GetOrdinal("id_chofer")) ? dr.GetString(dr.GetOrdinal("id_chofer")) : string.Empty;
                    Compra.IDJaula = !dr.IsDBNull(dr.GetOrdinal("id_jaula")) ? dr.GetString(dr.GetOrdinal("id_jaula")) : string.Empty;
                    Compra.IDRemolque = !dr.IsDBNull(dr.GetOrdinal("id_remolque")) ? dr.GetString(dr.GetOrdinal("id_remolque")) : string.Empty;
                    Compra.IDVehiculo = !dr.IsDBNull(dr.GetOrdinal("id_vehiculo")) ? dr.GetString(dr.GetOrdinal("id_vehiculo")) : string.Empty;
                    Compra.Flete.kmInicialVehiculo = !dr.IsDBNull(dr.GetOrdinal("kmInicialVehiculo")) ? dr.GetInt32(dr.GetOrdinal("kmInicialVehiculo")) : 0;
                    Compra.Trayecto.id_lugarOrigen = !dr.IsDBNull(dr.GetOrdinal("id_lugarOrigen")) ? dr.GetString(dr.GetOrdinal("id_lugarOrigen")) : string.Empty;
                    Compra.Trayecto.id_lugarDestino = !dr.IsDBNull(dr.GetOrdinal("id_lugarDestino")) ? dr.GetString(dr.GetOrdinal("id_lugarDestino")) : string.Empty;
                    Compra.Flete.kmInicialVehiculo = !dr.IsDBNull(dr.GetOrdinal("kmInicialVehiculo")) ? dr.GetInt32(dr.GetOrdinal("kmInicialVehiculo")) : 0;
                    Compra.Sucursal.MermaPredeterminada = !dr.IsDBNull(dr.GetOrdinal("mermaPredeterminada")) ? dr.GetDecimal(dr.GetOrdinal("mermaPredeterminada")) : 0;
                    Compra.MontoTotal = !dr.IsDBNull(dr.GetOrdinal("montoTotal")) ? dr.GetDecimal(dr.GetOrdinal("montoTotal")) : 0;
                    Compra.MermaPromedio = !dr.IsDBNull(dr.GetOrdinal("mermaPromedio")) ? dr.GetDecimal(dr.GetOrdinal("mermaPromedio")) : 0;
                    Compra.GanadosCompradoMachos = !dr.IsDBNull(dr.GetOrdinal("ganadoCompradoMachos")) ? dr.GetInt32(dr.GetOrdinal("ganadoCompradoMachos")) : 0;
                    Compra.GanadosCompradoHembras = !dr.IsDBNull(dr.GetOrdinal("ganadoCompradoHembras")) ? dr.GetInt32(dr.GetOrdinal("ganadoCompradoHembras")) : 0;
                    Compra.GanadosCompradoTotal = !dr.IsDBNull(dr.GetOrdinal("ganadoCompradoTotal")) ? dr.GetInt32(dr.GetOrdinal("ganadoCompradoTotal")) : 0;
                    Compra.IDPLugarProveedor = !dr.IsDBNull(dr.GetOrdinal("id_lugar_proveedor")) ? dr.GetString(dr.GetOrdinal("id_lugar_proveedor")) : string.Empty;
                    Compra.Proveedor.NombreRazonSocial = !dr.IsDBNull(dr.GetOrdinal("nombreRazonSocial")) ? dr.GetString(dr.GetOrdinal("nombreRazonSocial")) : string.Empty;
                }
                return Compra;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public SqlDataReader GetSqlDataReaderListadoPrecioRangoPeso(CompraModels Compra)
        {
            SqlDataReader dr = null;
            object[] parametros =
               {
                    Compra.IDProveedor
                };
            dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Compras_get_ListadoPrecioRangoPesoXIDProveedor", parametros);

            return dr;
        }
        public List<CatRangoPesoCompraModels> GetListadoPrecioRangoPeso(CompraModels Compra)
        {
            try
            {
                object[] parametros =
                {
                    Compra.IDEmpresa
                };
                CatRangoPesoCompraModels RangoPeso;

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Compras_get_ListadoPrecioRangoPesoXIDProveedor", parametros);
                while (dr.Read())
                {
                    RangoPeso = new CatRangoPesoCompraModels
                    {
                        IDRango = !dr.IsDBNull(dr.GetOrdinal("IDRango")) ? dr.GetInt16(dr.GetOrdinal("IDRango")) : 0,
                        PesoMinimo = !dr.IsDBNull(dr.GetOrdinal("PesoMinimo")) ? dr.GetDecimal(dr.GetOrdinal("PesoMinimo")) : 0,
                        PesoMaximo = !dr.IsDBNull(dr.GetOrdinal("PesoMaximo")) ? dr.GetDecimal(dr.GetOrdinal("PesoMaximo")) : 0,
                        EsMacho = !dr.IsDBNull(dr.GetOrdinal("EsMacho")) ? dr.GetBoolean(dr.GetOrdinal("EsMacho")) : false,
                        Precio = !dr.IsDBNull(dr.GetOrdinal("Precio")) ? dr.GetDecimal(dr.GetOrdinal("Precio")) : 0
                    };

                    Compra.ListaRangoPrecio.Add(RangoPeso);
                }
                return Compra.ListaRangoPrecio;
            }
            catch (Exception ex)
            {
                throw ex;
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
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Compras_get_CompraGanadoXIDGanado", parametros);
                while (dr.Read())
                {
                    Compra.IDSucursal = !dr.IsDBNull(dr.GetOrdinal("id_sucursal")) ? dr.GetString(dr.GetOrdinal("id_sucursal")) : string.Empty;
                    Compra.CompraGanado.IDCompra = !dr.IsDBNull(dr.GetOrdinal("id_compra")) ? dr.GetString(dr.GetOrdinal("id_compra")) : string.Empty;
                    Compra.Ganado.numArete = !dr.IsDBNull(dr.GetOrdinal("numArete")) ? dr.GetString(dr.GetOrdinal("numArete")) : string.Empty;
                    Compra.Ganado.genero = !dr.IsDBNull(dr.GetOrdinal("genero")) ? dr.GetString(dr.GetOrdinal("genero")) : string.Empty;
                    Compra.Ganado.Repeso = !dr.IsDBNull(dr.GetOrdinal("repeso")) ? dr.GetBoolean(dr.GetOrdinal("repeso")) : true;
                    Compra.CompraGanado.Merma = !dr.IsDBNull(dr.GetOrdinal("merma")) ? dr.GetDecimal(dr.GetOrdinal("merma")) : 0;
                    Compra.CompraGanado.PesoFinal = !dr.IsDBNull(dr.GetOrdinal("pesoFinal")) ? dr.GetDecimal(dr.GetOrdinal("pesoFinal")) : 0;
                    Compra.CompraGanado.PesoInicial = !dr.IsDBNull(dr.GetOrdinal("pesoInicial")) ? dr.GetDecimal(dr.GetOrdinal("pesoInicial")) : 0;
                    Compra.CompraGanado.PesoPagado = !dr.IsDBNull(dr.GetOrdinal("PesoPagado")) ? dr.GetDecimal(dr.GetOrdinal("PesoPagado")) : 0;
                    Compra.CompraGanado.PrecioKilo = !dr.IsDBNull(dr.GetOrdinal("precioKilo")) ? dr.GetDecimal(dr.GetOrdinal("precioKilo")) : 0;
                    Compra.CompraGanado.TotalPagado = !dr.IsDBNull(dr.GetOrdinal("totalPagado")) ? dr.GetDecimal(dr.GetOrdinal("totalPagado")) : 0;
                    Compra.CompraGanado.DiferenciaPeso = !dr.IsDBNull(dr.GetOrdinal("DiferenciaPeso")) ? dr.GetDecimal(dr.GetOrdinal("DiferenciaPeso")) : 0;
                    Compra.EstatusGanado.id_estatusGanado = !dr.IsDBNull(dr.GetOrdinal("id_estatusGanado")) ? dr.GetInt16(dr.GetOrdinal("id_estatusGanado")) : 0;
                    Compra.EstatusGanado.descripcion = !dr.IsDBNull(dr.GetOrdinal("estatusGanado")) ? dr.GetString(dr.GetOrdinal("estatusGanado")) : string.Empty;
                    Compra.Ganado.observacion = !dr.IsDBNull(dr.GetOrdinal("observacion")) ? dr.GetString(dr.GetOrdinal("observacion")) : string.Empty;
                    Compra.Sucursal.MermaPredeterminada = !dr.IsDBNull(dr.GetOrdinal("mermaPredeterminada")) ? dr.GetDecimal(dr.GetOrdinal("mermaPredeterminada")) : 0;
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
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Compras_get_GanadoXGanadoDetalleXIDCompra", parametros);

                return Auxiliar.SqlReaderToJson(dr);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        #region ABC
        #region Proveedor
        public CompraModels Compras_ac_Proveedor(CompraModels Compra)
        {
            try
            {
                object[] parametros =
                {
                     Compra.IDCompra  = string.IsNullOrEmpty(Compra.IDCompra) ? null : Compra.IDCompra
                    ,Compra.IDSucursal = string.IsNullOrEmpty(Compra.IDSucursal) ? null : Compra.IDSucursal
                    ,Compra.IDProveedor = string.IsNullOrEmpty(Compra.IDProveedor) ? null : Compra.IDProveedor
                    ,Compra.GanadosPactadoMachos
                    ,Compra.GanadosPactadoHembras
                    ,Compra.FechaHoraProgramada
                    ,Compra.Usuario = string.IsNullOrEmpty(Compra.Usuario) ? null : Compra.Usuario
                    ,Compra.IDPLugarProveedor = string.IsNullOrEmpty(Compra.IDPLugarProveedor) ? null : Compra.IDPLugarProveedor
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Compras_ac_Proveedor", parametros);

                while (dr.Read())
                {
                    Compra.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                    Compra.Completado = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : true;
                }
                return Compra;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Flete
        public CompraModels Compras_ac_Flete(CompraModels Compra)
        {
            try
            {
                object[] parametros =
                {
                     Compra.IDCompra = string.IsNullOrEmpty(Compra.IDCompra) ? null : Compra.IDCompra
                    ,Compra.IDFlete= string.IsNullOrEmpty(Compra.IDFlete) ? null : Compra.IDFlete
                    ,Compra.IDEmpresa= string.IsNullOrEmpty(Compra.IDEmpresa) ? null : Compra.IDEmpresa
                    ,Compra.IDChofer= string.IsNullOrEmpty(Compra.IDChofer) ? null : Compra.IDChofer
                    ,Compra.IDVehiculo= string.IsNullOrEmpty(Compra.IDVehiculo) ? null : Compra.IDVehiculo
                    ,Compra.IDJaula= string.IsNullOrEmpty(Compra.IDJaula) ? null : Compra.IDJaula
                    ,Compra.IDRemolque= string.IsNullOrEmpty(Compra.IDRemolque) ? null : Compra.IDRemolque
                    ,Compra.Flete.kmInicialVehiculo
                    ,Compra.Usuario= string.IsNullOrEmpty(Compra.Usuario) ? null : Compra.IDCompra
                    ,Compra.Trayecto.id_lugarOrigen = string.IsNullOrEmpty(Compra.Trayecto.id_lugarOrigen) ? null : Compra.Trayecto.id_lugarOrigen
                    ,Compra.Trayecto.id_lugarDestino = string.IsNullOrEmpty(Compra.Trayecto.id_lugarDestino) ? null : Compra.Trayecto.id_lugarDestino
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Compras_ac_Flete", parametros);

                while (dr.Read())
                {
                    Compra.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : true;
                    Compra.RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                }
                return Compra;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

     
      
        public CompraModels Compras_a_Documentos(CompraModels Compra)
        {
            try
            {
                object[] parametros =
                {
                     Compra.IDCompra = string.IsNullOrEmpty(Compra.IDCompra) ? null : Compra.IDCompra
                    ,Compra.GuiaTransito = string.IsNullOrEmpty(Compra.GuiaTransito) ? null : Compra.GuiaTransito
                    ,Compra.CertTuberculosis= string.IsNullOrEmpty(Compra.CertTuberculosis) ? null : Compra.CertTuberculosis
                    ,Compra.CertBrucelosis= string.IsNullOrEmpty(Compra.CertBrucelosis) ? null : Compra.CertBrucelosis
                    ,Compra.CertZoosanitario= string.IsNullOrEmpty(Compra.CertZoosanitario) ? null : Compra.CertZoosanitario
                    ,Compra.Usuario= string.IsNullOrEmpty(Compra.Usuario) ? null : Compra.Usuario
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Compras_a_Documentos", parametros);

                while (dr.Read())
                {
                    Compra.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                    Compra.Completado = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : true;
                }
                return Compra;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public CompraModels Compras_ac_Ganado(CompraModels Compra)
        {
            try
            {
                object[] parametros =
                {
                //--TABLA GANADO

                     Compra.Ganado.id_Ganados //@id_ganado char(36)
                    ,Compra.IDCompra//,@id_compra CHAR(36)
                    ,Compra.IDSucursal//,@id_sucursal CHAR(36)
                    ,Compra.EstatusGanado.id_estatusGanado//,@id_estatusGanado SMALLINT
                    ,Compra.Ganado.observacion//,@observacion TEXT
                    ,Compra.Ganado.numArete//,@numArete NVARCHAR(15)
                    ,Compra.Ganado.Repeso//,@repeso BIT
                    ,Compra.Ganado.genero//,@genero BIT
                    //--TABLA COMPRA-GANADO
                    ,Compra.CompraGanado.PesoInicial//,@pesoInicial DECIMAL(7,3)
                    ,Compra.CompraGanado.PesoFinal//,@pesoFinal DECIMAL(7,3)
                    ,Compra.CompraGanado.Merma//,@merma DECIMAL(7,3)
                    ,Compra.CompraGanado.PesoPagado//,@pesoPagado DECIMAL(7,3)
                    ,Compra.CompraGanado.PrecioKilo//,@precioKilo MONEY
                    //--OTROS
                    ,Compra.Usuario//,@id_usuario CHAR(36)
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Compra_ac_Ganado", parametros);

                while (dr.Read())
                {
                    Compra.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                    Compra.Completado = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : true;
                }
                return Compra;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Imagenes
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
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Compras_DeleteImageFierro", parametros);
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
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_Compras_SaveImageFierro", parametros);
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
        public List<CatFierroModels> GetListadoFierrosXIDCompra(CompraModels Compra)
        {
            CatFierroModels Fierro;
            SqlDataReader dr = null;
            object[] parametros =
            {
                Compra.IDCompra
            };

            dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_get_CatFierroXIDCompra", parametros);

            while (dr.Read())
            {
                Fierro = new CatFierroModels
                {
                    IDFierro = !dr.IsDBNull(dr.GetOrdinal("id_fierro")) ? dr.GetString(dr.GetOrdinal("id_fierro")) : string.Empty,
                    NombreFierro = !dr.IsDBNull(dr.GetOrdinal("nombreFierro")) ? dr.GetString(dr.GetOrdinal("nombreFierro")) : string.Empty,
                    ImgFierro = !dr.IsDBNull(dr.GetOrdinal("imgFierro")) ? dr.GetString(dr.GetOrdinal("imgFierro")) : string.Empty
                };

                Compra.ListaFierros.Add(Fierro);
            }
            return Compra.ListaFierros;

        }
        #endregion

        #region Index
        public SqlDataReader ObtenerCompraIndexDataTable(CompraModels CompraModels)
        {
            try
            {
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(CompraModels.Conexion, "spCSLDB_Compras_IndexVentas");
                return dr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

      











        
       
        public List<CatTipoClasificacionModels> GetListadoTipoClasificacion(CompraModels Compra)
        {
            CatTipoClasificacionModels TipoClasificacion;
            SqlDataReader dr = null;
            object[] parametros =
               {
                    Compra.Sucursal.IDSucursal
                };
            dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_COMPRAS_GetListadoTipoClasificacion", parametros);

            while (dr.Read())
            {
                TipoClasificacion = new CatTipoClasificacionModels
                {
                    IDTipoClasificacionGasto = !dr.IsDBNull(dr.GetOrdinal("id_tipoClasificacionGasto")) ? dr.GetInt16(dr.GetOrdinal("id_tipoClasificacionGasto")) : 0,
                    Descripcion = !dr.IsDBNull(dr.GetOrdinal("descripcion")) ? dr.GetString(dr.GetOrdinal("descripcion")) : string.Empty,
                };

                Compra.ListaTipoClasificacion.Add(TipoClasificacion);
            }
            return Compra.ListaTipoClasificacion;
        }
        public List<CFDI_FormaPagoModels> GetListadoFormaPago(CompraModels Compra)
        {
            CFDI_FormaPagoModels FormaPago;
            SqlDataReader dr = null;
            object[] parametros =
               {
                    Compra.Sucursal.IDSucursal
                };
            dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_COMPRAS_GetListadoFormaPago", parametros);

            while (dr.Read())
            {
                FormaPago = new CFDI_FormaPagoModels
                {
                    Clave = !dr.IsDBNull(dr.GetOrdinal("clave")) ? dr.GetInt16(dr.GetOrdinal("clave")) : 0,
                    Descripcion = !dr.IsDBNull(dr.GetOrdinal("descripcion")) ? dr.GetString(dr.GetOrdinal("descripcion")) : string.Empty,
                    Bancarizado = !dr.IsDBNull(dr.GetOrdinal("bancarizado")) ? dr.GetInt32(dr.GetOrdinal("bancarizado")) : 0,
                };

                Compra.ListaFormasPagos.Add(FormaPago);
            }
            return Compra.ListaFormasPagos;
        }


      

















        #region Insert
        public CompraModels CreateCompra(CompraModels Compra)
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
                Compra.Flete.IDRemolque,
                Compra.Flete.kmInicialVehiculo,
                //Datos del trayecto
                Compra.Trayecto.id_lugarOrigen,
                Compra.Trayecto.id_lugarDestino
            };

            SqlDataReader dr = null;
            dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_COMPRAS_CreateCompra", parametros);
            while (dr.Read())
            {
                Compra.IDCompra = !dr.IsDBNull(dr.GetOrdinal("id_compra")) ? dr.GetString(dr.GetOrdinal("id_compra")) : string.Empty;
            }
            return Compra;
        }
        #endregion
       
              
     
       
     
      



       



        public string GetRangoPeso(CompraModels Compra)
        {
            object[] parametros =
                {
                    Compra.IDCompra
                };
            try
            {
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_COMPRAS_GetListadoPrecioRangoPeso", parametros);

                return Auxiliar.SqlReaderToJson(dr);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        
    }
}