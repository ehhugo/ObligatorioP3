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

            if (obj != null && obj.Validar() && BuscarPorNombre(obj.Nombre) == null)
            {
                SqlConnection con = Conexion.ObtenerConexion();

                string sql = "INSERT INTO TiposDePLanta VALUES (@nom, @des); SELECT CAST (SCOPE_IDENTITY() AS INT);";

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

        public bool EliminarPorNombre(string name)
        {
            bool ok = false;
            SqlConnection con = Conexion.ObtenerConexion();
            string sql = $"DELETE FROM TiposDePlanta WHERE  Nombre= '{name}';";
            SqlCommand SQLCom = new SqlCommand(sql, con);

            try
            {
                Conexion.AbrirConexion(con);
                int filasAfectadas = SQLCom.ExecuteNonQuery();
                ok = filasAfectadas == 1;
            }
            catch
            {
                ok = false;
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

            string sql = "SELECT * FROM TiposDePlanta;";
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

            string sql = $"SELECT * FROM TiposDePlanta WHERE idTipo = {id};";
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

        public bool TipoExiste(string nombreTipo)
        {
            bool existeTipo = false;

            if (nombreTipo != null)
            {
                SqlConnection con = Conexion.ObtenerConexion();
                string sql = $"SELECT * FROM TiposDePlanta WHERE Nombre = '{nombreTipo}';";

                SqlCommand SQLCom = new SqlCommand(sql, con);

                try
                {
                    Conexion.AbrirConexion(con);
                    SqlDataReader reader = SQLCom.ExecuteReader();

                    if (reader.HasRows)
                    {
                        existeTipo = true;
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
            }
            return existeTipo;
        }

        public Tipo BuscarPorNombre(string nombre)
        {
            Tipo buscado = null;

            SqlConnection con = Conexion.ObtenerConexion();

            string sql = $"SELECT * FROM TiposDePlanta WHERE Nombre = '{nombre}';";
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
                string sql = "UPDATE TiposDePlanta SET Nombre=@nom, Descripcion=@des WHERE idTipo =@IdTipo";
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

        public bool ActualizarDescripcion(Tipo obj)
        {
            bool ok = false;
            SqlConnection con = Conexion.ObtenerConexion();

            if (obj.Descripcion.Length >= 10 && obj.Descripcion.Length <= 200)
            {
                string sql = $"UPDATE TiposDePlanta SET Descripcion=@des WHERE idTipo =@IdTipo";
                SqlCommand SQLCom = new SqlCommand(sql, con);

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
        #endregion
    }
}
