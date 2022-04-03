using System;
using System.Collections.Generic;
using System.Text;
using Dominio.Entidades;
using Dominio.InterfacesRepositorio;
using Microsoft.Data.SqlClient;
using Datos;

namespace Datos
{
    public class RepositorioPlantasADO : IRepositorioPlantas
    {
        #region Add Planta BD
        public bool Add(Planta obj)
        {
            bool altaOK = false;

            if (obj != null && obj.Validar(obj))
            {
                SqlConnection con = Conexion.ObtenerConexion();

                string sql = "INSERT INTO Plantas VALUES (@tipoPlanta, @nomCientifico, @nomVulgar, @descripcion, @ambiente, @alturaMaxima, @foto, @frecuenciaRiego, @tipoIluminacion, @tempMantenimiento); SELECT CAST (SCOPE_IDENTITY() AS INT);";

                SqlCommand SQLcom = new SqlCommand(sql, con);

                SQLcom.Parameters.AddWithValue("@tipoPlanta", obj.TipoPlanta);
                SQLcom.Parameters.AddWithValue("@nomCientifico", obj.NombreCientifico);
                SQLcom.Parameters.AddWithValue("@nomVulgar", obj.NombreVulgar); // list
                SQLcom.Parameters.AddWithValue("@descripcion", obj.Descripcion);
                SQLcom.Parameters.AddWithValue("@ambiente", obj.Ambiente);
                SQLcom.Parameters.AddWithValue("@alturaMaxima", obj.AlturaMaxima);
                SQLcom.Parameters.AddWithValue("@nomVulgar", obj.NombreVulgar);
                SQLcom.Parameters.AddWithValue("@foto", obj.Foto); // list
                SQLcom.Parameters.AddWithValue("@frecuenciaRiego", obj.FrecuenciaRiego);
                SQLcom.Parameters.AddWithValue("@tipoIluminacion", obj.TipoIluminacion);
                SQLcom.Parameters.AddWithValue("@tempMantenimiento", obj.TemperaturaMantenimiento);                

                try
                {
                    Conexion.AbrirConexion(con);
                    int id = (int)SQLcom.ExecuteScalar();
                    obj.IdPlanta = id;
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
        #endregion

        public IEnumerable<Planta> FindAll()
        {
            List<Planta> plantas = new List<Planta>();
            SqlConnection con = Conexion.ObtenerConexion();

            string sql = "SELECT * FROM Plantas;";
            SqlCommand SQLCom = new SqlCommand(sql, con);

            try
            {
                Conexion.AbrirConexion(con);
                SqlDataReader reader = SQLCom.ExecuteReader();

                while (reader.Read())
                {
                    Planta planta = new Planta()
                    {
                        IdPlanta = reader.GetInt32(reader.GetOrdinal("idTipo")),
                        NombreCientifico = reader.GetString(1),
                        //NombreVulgar= reader.GetString(2), // list
                        Descripcion = reader.GetString(3),
                        //Ambiente = RepositorioAmbienteMemoria.FindById(reader.GetInt32(reader.GetOrdinal("TipoAmbiente"))),
                        AlturaMaxima = reader.GetInt32(5),
                        //Foto = // lista
                        FrecuenciaRiego = reader.GetString(7),
                        //TipoIluminacion = mismo que ambiente
                        TemperaturaMantenimiento = reader.GetInt32(9)
                    };
                    plantas.Add(planta);
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
            return plantas;
        }

        public Planta FindById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Remove(int id)
        {
            throw new NotImplementedException();
        }

        public bool update(Planta obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Planta> BuscarPlantasMasAltas(double alturaCm)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Planta> BuscarPlantasMasBajas(double alturaCm)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Planta> BuscarPorAmbiente(string ambiente)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Planta> BuscarPorTexto(string texto)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Planta> BuscarPorTipo(string tipoPlanta)
        {
            throw new NotImplementedException();
        }
    }
}
