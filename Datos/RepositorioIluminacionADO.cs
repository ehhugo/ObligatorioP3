using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using Dominio.Entidades;
using Dominio.InterfacesRepositorio;

using Microsoft.Data.SqlClient;

namespace Datos
{
    public class RepositorioIluminacionADO : IRepositorioIluminaciones
    {
        #region Add/Delete
        public bool Add(Iluminacion obj)
        {
            bool altaOK = false;

            if (obj != null && obj.Validar(obj) && FindById(obj.IdIluminacion) == null)
            {
                SqlConnection con = Conexion.ObtenerConexion();
                
                string sql = "INSERT INTO TiposDeIluminacion VALUES (@Tipo); SELECT CAST (SCOPE_IDENTITY() AS INT);";
                
                SqlCommand SQLcom = new SqlCommand(sql, con);
                SQLcom.Parameters.AddWithValue("@Tipo", obj.Tipo);

                try
                {
                    Conexion.AbrirConexion(con);
                    int id = (int)SQLcom.ExecuteScalar();
                    obj.IdIluminacion = id;
                    altaOK = true;
                }
                catch
                {
                    throw;
                }
                finally
                {
                    Conexion.CerrarYTerminarConexion(con);
                }
            }
            return altaOK;
        }

        public bool Remove(int id)
        {
            bool eliminarOK = false;
            SqlConnection con = Conexion.ObtenerConexion();
            string sql = $"DELETE FROM TiposDeIluminacion WHERE idIluminacion = {id};";
            SqlCommand SQLCom = new SqlCommand(sql, con);

            try
            {
                Conexion.AbrirConexion(con);
                int filasAfectadas = SQLCom.ExecuteNonQuery();
                eliminarOK = filasAfectadas == 1;
            }
            catch
            {
                throw;
            }
            finally
            {
                Conexion.CerrarYTerminarConexion(con);
            }
            return eliminarOK;
        }
        #endregion

        #region Find
        public IEnumerable<Iluminacion> FindAll()
        {
            List<Iluminacion> iluminaciones = new List<Iluminacion>();

            SqlConnection con = Conexion.ObtenerConexion();

            string sql = "SELECT * FROM TiposDeIluminacion;";
            SqlCommand SQLCom = new SqlCommand(sql, con);

            try
            {
                Conexion.AbrirConexion(con);
                SqlDataReader reader = SQLCom.ExecuteReader();

                while (reader.Read())
                {
                    Iluminacion i = new Iluminacion()
                    {
                        IdIluminacion = reader.GetInt32(reader.GetOrdinal("idIluminacion")),
                        Tipo = reader.GetString(1),
                    };
                    iluminaciones.Add(i);
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                Conexion.CerrarYTerminarConexion(con);
            }
            return iluminaciones;
        }

        public Iluminacion FindById(int id)
        {
            Iluminacion buscada = null;

            SqlConnection con = Conexion.ObtenerConexion();

            string sql = $"SELECT * FROM TiposDeIluminacion WHERE idIluminacion = {id};";
            SqlCommand SQLCom = new SqlCommand(sql, con);

            try
            {
                Conexion.AbrirConexion(con);
                SqlDataReader reader = SQLCom.ExecuteReader();

                if (reader.Read())
                {
                    buscada = new Iluminacion()
                    {
                        IdIluminacion = reader.GetInt32(reader.GetOrdinal("idIluminacion")),
                        Tipo = reader.GetString(1),
                    };
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                Conexion.CerrarYTerminarConexion(con);
            }

            return buscada;
        }
        #endregion

        #region Update
        public bool update(Iluminacion obj)
        {
            bool ok = false;
            SqlConnection con = Conexion.ObtenerConexion();

            if (obj.Validar(obj))
            {
                string sql = "UPDATE TiposDeIluminacion SET TipoIluminacion = @TipoIluminacion WHERE idIluminacion =@idIluminacion";
                SqlCommand SQLCom = new SqlCommand(sql, con);

                SQLCom.Parameters.AddWithValue("@TipoIluminacion", obj.Tipo);
                SQLCom.Parameters.AddWithValue("@idIluminacion", obj.IdIluminacion);

                try
                {
                    Conexion.AbrirConexion(con);
                    int afectadas = SQLCom.ExecuteNonQuery();
                    ok = afectadas == 1;
                }
                catch
                {
                    throw;
                }
                finally
                {
                    Conexion.CerrarYTerminarConexion(con);
                }
            }
            return ok;
        }
        #endregion
    }
}
