using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using Dominio.Entidades;
using Dominio.InterfacesRepositorio;

using Microsoft.Data.SqlClient;


namespace Datos
{
    public class RepositorioTiposADO : IRepositorioTipos

    {
        #region Add/Delete
        public bool Add(Tipo obj)
        {
            bool ok = false;

            if (obj != null && obj.Validar() && FindById(obj.IdTipo) == null)
            {
                SqlConnection con = Conexion.ObtenerConexion();

                string sql = "INSERT INTO Tipos VALUES (@nom, @des); SELECT CAST (SCOPE_IDENTITY() AS INT);";

                SqlCommand SQLcom = new SqlCommand(sql, con);

                SQLcom.Parameters.AddWithValue("@nom", obj.Nombre);
                SQLcom.Parameters.AddWithValue("@des", obj.Descripcion);

                try
                {
                    Conexion.AbrirConexion(con);
                    int id = (int)SQLcom.ExecuteScalar();
                    obj.IdTipo = id;
                    ok = true;
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

        public bool Remove(int id)
        {
            bool ok = false;
            SqlConnection con = Conexion.ObtenerConexion();
            string sql = $"DELETE FROM Tipos WHERE idTipo ={id};";
            SqlCommand SQLCom = new SqlCommand(sql, con);

            try
            {
                Conexion.AbrirConexion(con);
                int filasAfectadas = SQLCom.ExecuteNonQuery();
                ok = filasAfectadas == 1;
            }
            catch
            {
                throw;
            }
            finally
            {
                Conexion.CerrarYTerminarConexion(con);
            }
            return ok;
        }
        public bool EliminarPorNombre(string nombre)
        {
            bool ok = false;
            SqlConnection con = Conexion.ObtenerConexion();
            string sql = $"DELETE FROM Tipos WHERE  Nombre= '{nombre}';";
            SqlCommand SQLCom = new SqlCommand(sql, con);

            try
            {
                Conexion.AbrirConexion(con);
                int filasAfectadas = SQLCom.ExecuteNonQuery();
                ok = filasAfectadas == 1;
            }
            catch
            {
                throw;
            }
            finally
            {
                Conexion.CerrarYTerminarConexion(con);
            }
            return ok;
        }
        #endregion

        #region Buscar
        public IEnumerable<Tipo> FindAll()
        {
            List<Tipo> tipos = new List<Tipo>();

            SqlConnection con = Conexion.ObtenerConexion();

            string sql = "SELECT * FROM Tipos;";
            SqlCommand SQLCom = new SqlCommand(sql, con);

            try
            {
                Conexion.AbrirConexion(con);
                SqlDataReader reader = SQLCom.ExecuteReader();

                while (reader.Read())
                {
                    Tipo t = new Tipo()
                    {
                        IdTipo = reader.GetInt32(reader.GetOrdinal("idTipo")),
                        Nombre = reader.GetString(1),
                        Descripcion = reader.GetString(2)
                    };
                    tipos.Add(t);
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

            return tipos;
        }

        public Tipo FindById(int id)
        {
            Tipo buscado = null;

            SqlConnection con = Conexion.ObtenerConexion();

            string sql = $"SELECT * FROM Tipos WHERE idTipo = {id};";
            SqlCommand SQLCom = new SqlCommand(sql, con);

            try
            {
                Conexion.AbrirConexion(con);
                SqlDataReader reader = SQLCom.ExecuteReader();

                if (reader.Read())
                {
                    buscado = new Tipo()
                    {
                        IdTipo = reader.GetInt32(reader.GetOrdinal("idTipo")),
                        Nombre = reader.GetString(1),
                        Descripcion = reader.GetString(2)
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

            return buscado;

        }


        public Tipo BuscarPorNombre(string nombre)
        {
            Tipo buscado = null;

            SqlConnection con = Conexion.ObtenerConexion();

            string sql = $"SELECT * FROM Tipos WHERE Nombre = '{nombre}';";
            SqlCommand SQLCom = new SqlCommand(sql, con);

            try
            {
                Conexion.AbrirConexion(con);
                SqlDataReader reader = SQLCom.ExecuteReader();

                if (reader.Read())
                {
                    buscado = new Tipo()
                    {
                        IdTipo = reader.GetInt32(reader.GetOrdinal("idTipo")),
                        Nombre = reader.GetString(1),
                        Descripcion = reader.GetString(2)
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

            return buscado;

        }

        #endregion

        #region Update

        public bool update(Tipo obj)
        {
            bool ok = false;
            SqlConnection con = Conexion.ObtenerConexion();

            if (obj.Validar())
            {
                string sql = "UPDATE Tipos SET Nombre=@nom, Descripcion=@des WHERE idTipo =@IdTipo";
                SqlCommand SQLCom = new SqlCommand(sql, con);

                SQLCom.Parameters.AddWithValue("@nom", obj.Nombre);
                SQLCom.Parameters.AddWithValue("@des", obj.Descripcion);
                SQLCom.Parameters.AddWithValue("@IdTipo", obj.IdTipo);

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

        public bool ActualizarDescripcion(string nombre)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
