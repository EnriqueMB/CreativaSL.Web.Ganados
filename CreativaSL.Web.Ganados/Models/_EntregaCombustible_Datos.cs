using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class _EntregaCombustible_Datos
    {

        public List<EntregaCombistibleModels> ObtenerEntregasCombustible(EntregaCombistibleModels Datos)
        {
            try
            {
                List<EntregaCombistibleModels> Lista = new List<EntregaCombistibleModels>();
                EntregaCombistibleModels Item;

                Item = new EntregaCombistibleModels { Fecha = DateTime.Today, Sucursal = new CatSucursalesModels {NombreSucursal ="Matriz" },
                Vehiculo = new CatVehiculoModels { Modelo="Chevrolet AVEO 2005" } , Total = 3456.43m, IDEntregaCombustible = "Entrega0001" };
                Lista.Add(Item);
                //object[] Parametros = { Datos.BandSucursal, Datos.BandVehiculo, Datos.BandFecha, Datos.Sucursal.IDSucursal, Datos.Vehiculo.IDVehiculo, Datos.Fecha };
                //SqlDataReader Dr = SqlHelper.ExecuteReader(Datos.Conexion, "", Parametros);
                //while(Dr.Read())
                //{
                //spCSLDB_Catalogo_get_EntregaCombustible SP QUE LISTA LOS LA ENTREGA DE COMBUSTIBLE FALTA APLICAR FILTRO 
                //}
                return Lista;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}