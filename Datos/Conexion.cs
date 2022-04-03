using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System.Data;

using System;
using System.Collections.Generic;
using System.Text;

namespace Datos
{
    class Conexion
    {
        public static string ObtenerCadenaConexion()
        {
            string cadenaConexion = "";

            IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            cadenaConexion = config.GetConnectionString("miConexion");

            return cadenaConexion;
        }

        public static SqlConnection ObtenerConexion()
        {
            SqlConnection con = new SqlConnection(ObtenerCadenaConexion());
            return con;
        }

        public static void AbrirConexion (SqlConnection con)
        {
            if(con != null && con.State != ConnectionState.Open) {
                con.Open();
            }
        }

        public static void CerrarConexion(SqlConnection con)
        {
            if (con != null && con.State != ConnectionState.Closed)
            {
                con.Close();
            }
        }

        public static void CerrarYTerminarConexion (SqlConnection con)
        {
            Conexion.CerrarConexion(con);
            con.Dispose();
        }


    }
}
