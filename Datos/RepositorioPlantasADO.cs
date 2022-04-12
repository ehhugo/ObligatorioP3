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
        #region Add
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

        #region FindAll y FindById
        public IEnumerable<Planta> FindAll()
        {
            List<Planta> plantas = new List<Planta>();
            List<string> aux = new List<string>();

            SqlConnection con = Conexion.ObtenerConexion();

            string sql = "SELECT P.*, TP.Nombre, TP.Descripcion, A.TipoAmbiente, TI.TipoIluminacion FROM Plantas P " +
                "LEFT JOIN TiposDePlanta TP ON P.TipoPlanta = TP.idTipo " + "LEFT JOIN Ambientes A ON P.Ambiente = A.idAmbiente " +
                "LEFT JOIN TiposDeIluminacion TI on P.Iluminacion = TI.idIluminacion;";

            SqlCommand SQLCom = new SqlCommand(sql, con);

            try
            {
                Conexion.AbrirConexion(con);
                SqlDataReader reader = SQLCom.ExecuteReader();

                while (reader.Read())
                {
                    Planta planta = new Planta()
                    {     
                        IdPlanta = reader.GetInt32(reader.GetOrdinal("idPlanta")),
                        TipoPlanta = CrearTipo(reader),
                        NombreCientifico = reader.GetString(reader.GetOrdinal("NombreCientifico")),
                        NombreVulgar= aux, //reader.GetString(reader.GetOrdinal("NombreVulgar"))
                        Descripcion = reader.GetString(4),
                        Ambiente = CrearAmbiente(reader),
                        AlturaMaxima = reader.GetDecimal(6),
                        Foto = reader.GetString(7),
                        FrecuenciaRiego = reader.GetString(8),
                        TipoIluminacion = CrearIluminacion(reader),
                        TemperaturaMantenimiento = reader.GetInt32(10)
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

        private Tipo CrearTipo(SqlDataReader reader)
        {
            Tipo tipoBuscado = new Tipo()
            {
                IdTipo = reader.GetInt32(1),
                Nombre = reader.GetString(11),
                Descripcion = reader.GetString(12),                
            };
            return tipoBuscado;
        }

        private Ambiente CrearAmbiente(SqlDataReader reader)
        {
            Ambiente ambienteBuscado = new Ambiente()
            {
                IdAmbiente = reader.GetInt32(5),
                TipoAmbiente = reader.GetString(13),
            };
            return ambienteBuscado;
        }

        private Iluminacion CrearIluminacion(SqlDataReader reader)
        {
            Iluminacion iluminacionBuscada = new Iluminacion()
            {
                IdIluminacion = reader.GetInt32(9),
                Tipo = reader.GetString(14),
            };
            return iluminacionBuscada;
        }

        public Planta FindById(int id)
        {
            Planta  buscada = null;
            List<string> aux = new List<string>();

            SqlConnection con = Conexion.ObtenerConexion();
            string sql = $"SELECT * FROM Plantas WHERE idPlanta = {id};";
            SqlCommand SQLCom = new SqlCommand(sql, con);

            try
            {
                Conexion.AbrirConexion(con);
                SqlDataReader reader = SQLCom.ExecuteReader();

                if (reader.Read())
                {
                    buscada = new Planta()
                    {
                        IdPlanta = reader.GetInt32(reader.GetOrdinal("idPlanta")),
                        NombreCientifico = reader.GetString(reader.GetOrdinal("NombreCientifico")),
                        NombreVulgar = aux, //reader.GetString(reader.GetOrdinal("NombreVulgar"))
                        Descripcion = reader.GetString(3),
                        Ambiente = null,
                        AlturaMaxima = reader.GetInt32(5),
                        Foto = reader.GetString(6),
                        FrecuenciaRiego = reader.GetString(7),
                        TipoIluminacion = null,
                        TemperaturaMantenimiento = reader.GetInt32(9)
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

        #endregion // quitar auxiliares

        #region Remove
        public bool Remove(int id)
        {
            bool eliminadoOK = false;
            SqlConnection con = Conexion.ObtenerConexion();
            string sql = $"DELETE FROM Plantas WHERE  idPlanta ={id};";
            SqlCommand SQLCom = new SqlCommand(sql, con);

            try
            {
                Conexion.AbrirConexion(con);
                int filasAfectadas = SQLCom.ExecuteNonQuery();
                eliminadoOK = filasAfectadas == 1;
            }
            catch
            {
                throw;
            }
            finally
            {
                Conexion.CerrarYTerminarConexion(con);
            }
            return eliminadoOK;
            
        }
        #endregion

        #region Update
        public bool update(Planta obj)
        {
            bool modificadoOK = false;
            SqlConnection con = Conexion.ObtenerConexion();

            if (obj.Validar(obj))
            {
                string sql = "UPDATE Plantas SET TipoPlanta=@tipoPlanta, NombreCientifico=@nomCientifico, NombreVulgar=@nomVulgar,  Descripcion=@descripcion, Ambiente=@ambiente, AlturaMaxima=@alturaMaxima, Foto=@foto, FrecuenciaRiego=@frecuenciaRiego, TipoIluminacion=@tipoIluminacion, TemperaturaMantenimiento=@tempMantenimiento WHERE idPlanta =@IdPlanta";
                SqlCommand SQLCom = new SqlCommand(sql, con);

                SQLCom.Parameters.AddWithValue("@tipoPlanta", obj.TipoPlanta);
                SQLCom.Parameters.AddWithValue("@nomCientifico", obj.NombreCientifico);
                SQLCom.Parameters.AddWithValue("@nomVulgar", obj.NombreVulgar); // list
                SQLCom.Parameters.AddWithValue("@descripcion", obj.Descripcion);
                SQLCom.Parameters.AddWithValue("@ambiente", obj.Ambiente);
                SQLCom.Parameters.AddWithValue("@alturaMaxima", obj.AlturaMaxima);
                SQLCom.Parameters.AddWithValue("@nomVulgar", obj.NombreVulgar);
                SQLCom.Parameters.AddWithValue("@foto", obj.Foto); // list
                SQLCom.Parameters.AddWithValue("@frecuenciaRiego", obj.FrecuenciaRiego);
                SQLCom.Parameters.AddWithValue("@tipoIluminacion", obj.TipoIluminacion);
                SQLCom.Parameters.AddWithValue("@tempMantenimiento", obj.TemperaturaMantenimiento);

                try
                {
                    Conexion.AbrirConexion(con);
                    int afectadas = SQLCom.ExecuteNonQuery();
                    modificadoOK = afectadas == 1;
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
            return modificadoOK;
        }
        #endregion

        #region Plantas Mas altas y Mas Baja
        public IEnumerable<Planta> BuscarPlantasMasAltas(double alturaCm)
        {
            List<Planta> plantasMasAltas = new List<Planta>();

            if (alturaCm > 0)
            {           
                SqlConnection con = Conexion.ObtenerConexion();
                string sql = @"SELECT * FROM Plantas WHERE alturaMaxima >= " + alturaCm;
                SqlCommand SQLCom = new SqlCommand(sql, con);

                try
                {
                    Conexion.AbrirConexion(con);
                    SqlDataReader reader = SQLCom.ExecuteReader();

                    while (reader.Read())
                    {
                        Planta p = new Planta()
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
                        plantasMasAltas.Add(p);
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
            return plantasMasAltas;
        }

        public IEnumerable<Planta> BuscarPlantasMasBajas(double alturaCm)
        {
            List<Planta> plantasMasBajas = new List<Planta>();

            if (alturaCm > 0)
            {
                SqlConnection con = Conexion.ObtenerConexion();
                string sql = @"SELECT * FROM Plantas WHERE alturaMaxima < " + alturaCm;
                SqlCommand SQLCom = new SqlCommand(sql, con);

                try
                {
                    Conexion.AbrirConexion(con);
                    SqlDataReader reader = SQLCom.ExecuteReader();

                    while (reader.Read())
                    {
                        Planta p = new Planta()
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
                        plantasMasBajas.Add(p);
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
            return plantasMasBajas;
        }
        #endregion

        #region Buscar por Ambiente, Texto y Por Tipo
        public IEnumerable<Planta> BuscarPorAmbiente(string ambiente)
        {
            List<Planta> plantasPorMabiente = new List<Planta>();

            if (ambiente != null)
            {
                SqlConnection con = Conexion.ObtenerConexion();
                string sql = @"SELECT * FROM Plantas WHERE ambiente = " + ambiente;
                SqlCommand SQLCom = new SqlCommand(sql, con);

                try
                {
                    Conexion.AbrirConexion(con);
                    SqlDataReader reader = SQLCom.ExecuteReader();

                    while (reader.Read())
                    {
                        Planta p = new Planta()
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
                        plantasPorMabiente.Add(p);
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
            return plantasPorMabiente;
        }

        public IEnumerable<Planta> BuscarPorTexto(string texto)
        {
            List<Planta> plantasConTextoEnNombre = new List<Planta>();

            if (texto != null)
            {
                SqlConnection con = Conexion.ObtenerConexion();
                string sql = @"SELECT * FROM Plantas WHERE nombreVulgar LIKE " + texto + "OR nombreCientifico LIKE " + texto; // armar consulta bien
                SqlCommand SQLCom = new SqlCommand(sql, con);

                try
                {
                    Conexion.AbrirConexion(con);
                    SqlDataReader reader = SQLCom.ExecuteReader();

                    while (reader.Read())
                    {
                        Planta p = new Planta()
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
                        plantasConTextoEnNombre.Add(p);
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
            return plantasConTextoEnNombre;
        }

        public IEnumerable<Planta> BuscarPorTipo(string tipoPlanta)
        {
            List<Planta> plantasPorTipo = new List<Planta>();

            if (tipoPlanta != null)
            {
                SqlConnection con = Conexion.ObtenerConexion();
                string sql = @"SELECT * FROM Plantas WHERE tipoPlanta =" + tipoPlanta;
                SqlCommand SQLCom = new SqlCommand(sql, con);

                try
                {
                    Conexion.AbrirConexion(con);
                    SqlDataReader reader = SQLCom.ExecuteReader();

                    while (reader.Read())
                    {
                        Planta p = new Planta()
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
                        plantasPorTipo.Add(p);
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
            return plantasPorTipo;
        }
        #endregion
    }
}