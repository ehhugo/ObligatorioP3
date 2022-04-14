using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using Dominio.Entidades;
using Dominio.InterfacesRepositorio;

using Microsoft.Data.SqlClient;

namespace Datos
{
    public class RepositorioAmbientesADO : IRepositorioAmbientes
    {

        #region Add/Delete
        public bool Add(Ambiente obj)
        {
            bool altaOK = false;

            if (obj != null && obj.Validar() && FindById(obj.IdAmbiente) == null)
            {
                SqlConnection con = Conexion.ObtenerConexion();
                string sql = "INSERT INTO Ambientes VALUES (@TipoAmbiente); SELECT CAST (SCOPE_IDENTITY() AS INT);";
                SqlCommand SQLcom = new SqlCommand(sql, con);
                SQLcom.Parameters.AddWithValue("@TipoAmbiente", obj.TipoAmbiente);

                try
                {
                    Conexion.AbrirConexion(con);
                    int id = (int)SQLcom.ExecuteScalar();
                    obj.IdAmbiente = id;
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
            string sql = $"DELETE FROM Ambientes WHERE idAmbiente = {id};";
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
        public IEnumerable<Ambiente> FindAll()
        {
            List<Ambiente> ambientes = new List<Ambiente>();

            SqlConnection con = Conexion.ObtenerConexion();

            string sql = "SELECT * FROM Ambientes;";
            SqlCommand SQLCom = new SqlCommand(sql, con);

            try
            {
                Conexion.AbrirConexion(con);
                SqlDataReader reader = SQLCom.ExecuteReader();

                while (reader.Read())
                {
                    Ambiente a = new Ambiente()
                    {
                        IdAmbiente = reader.GetInt32(reader.GetOrdinal("idAmbiente")),
                        TipoAmbiente = reader.GetString(1),
                    };
                    ambientes.Add(a);
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
            return ambientes;
        }

        public Ambiente FindById(int id)
        {
            Ambiente buscado = null;

            SqlConnection con = Conexion.ObtenerConexion();

            string sql = $"SELECT * FROM Ambientes WHERE idAmbiente = {id};";
            SqlCommand SQLCom = new SqlCommand(sql, con);

            try
            {
                Conexion.AbrirConexion(con);
                SqlDataReader reader = SQLCom.ExecuteReader();

                if (reader.Read())
                {
                    buscado = new Ambiente()
                    {
                        IdAmbiente = reader.GetInt32(reader.GetOrdinal("idAmbiente")),
                        TipoAmbiente = reader.GetString(1),
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
        public bool update(Ambiente obj)
        {
            bool ok = false;
            SqlConnection con = Conexion.ObtenerConexion();

            if (obj.Validar())
            {
                string sql = "UPDATE Ambientes SET TipoAmbiente = @TipoAmbinete WHERE idAmbiente =@IdAmbiente";
                SqlCommand SQLCom = new SqlCommand(sql, con);

                SQLCom.Parameters.AddWithValue("@TipoAmbinete", obj.TipoAmbiente);
                SQLCom.Parameters.AddWithValue("@IdAmbiente", obj.IdAmbiente);

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
