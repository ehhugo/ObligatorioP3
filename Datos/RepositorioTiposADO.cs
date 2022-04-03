using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using Dominio.Entidades;
using Dominio.InterfacesRepositorio;

using Microsoft.Data.SqlClient;


namespace Datos
{
    class RepositorioTiposADO : IRepositorioTipos

    {

        public bool Add(Tipo obj)
        {
            bool ok = false;

            if (obj != null && obj.Validar())
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
                    obj.idTipo = id;
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

        public Tipo BuscarPorNombre(string nombre)
        {
            throw new NotImplementedException();
        }

        public bool EliminarPorNombre(string nombre)
        {
            throw new NotImplementedException();
        }



        public Tipo FindById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Remove(int id)
        {
            throw new NotImplementedException();
        }

        public bool update(Tipo obj)
        {
            throw new NotImplementedException();
        }

        public bool ActualizarDescripcion(string nombre)
        {
            throw new NotImplementedException();
        }
    }
}
