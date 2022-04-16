using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Dominio.Entidades;
using Dominio.InterfacesRepositorio;

using Microsoft.Data.SqlClient;

namespace Datos
{
    public class RepositorioUsuariosADO : IRepositorioUsuarios
    {
        public bool Add(Usuario obj)
        {
            throw new NotImplementedException();
        }          

        public Usuario BuscarUsuarioLogin(Usuario u)
        {
            Usuario buscado = null;

            SqlConnection con = Conexion.ObtenerConexion();

            string sql = $"SELECT * FROM usuarios WHERE Mail = '{u.Mail}' and Pass = '{u.Contrasenia}';";
            SqlCommand SQLCom = new SqlCommand(sql, con);

            try
            {
                Conexion.AbrirConexion(con);
                SqlDataReader reader = SQLCom.ExecuteReader();

                if (reader.Read())
                {
                    buscado = new Usuario()
                    {
                        idUsuario = reader.GetInt32(reader.GetOrdinal("idUsuario")),
                        Mail = reader.GetString(1),
                        Contrasenia = reader.GetString(2)
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

        public IEnumerable<Usuario> FindAll()
        {
            throw new NotImplementedException();
        }

        public Usuario FindById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Remove(int id)
        {
            throw new NotImplementedException();
        }

        public bool update(Usuario obj)
        {
            throw new NotImplementedException();
        }
    }
}

