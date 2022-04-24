using System;
using System.Collections.Generic;
using System.Text;
using Dominio.Entidades;
using Dominio.InterfacesRepositorio;
using Microsoft.Data.SqlClient;
using Datos;
using System.Linq;

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

                string sql = "INSERT INTO Plantas VALUES " +
                    "(@tipoPlanta, @nomCientifico, @descripcion, @ambiente, @alturaMaxima, @foto, @frecuenciaRiego, @tipoIluminacion, @tempMantenimiento); " +
                    "SELECT CAST (SCOPE_IDENTITY() AS INT);";

                SqlCommand SQLcom = new SqlCommand(sql, con);

                SQLcom.Parameters.AddWithValue("@tipoPlanta", obj.TipoPlanta.IdTipo);
                SQLcom.Parameters.AddWithValue("@nomCientifico", obj.NombreCientifico);
                SQLcom.Parameters.AddWithValue("@descripcion", obj.Descripcion);
                SQLcom.Parameters.AddWithValue("@ambiente", obj.Ambiente.IdAmbiente);
                SQLcom.Parameters.AddWithValue("@alturaMaxima", obj.AlturaMaxima);
                SQLcom.Parameters.AddWithValue("@foto", obj.Foto);
                SQLcom.Parameters.AddWithValue("@frecuenciaRiego", obj.FrecuenciaRiego);
                SQLcom.Parameters.AddWithValue("@tipoIluminacion", obj.TipoIluminacion.IdIluminacion);
                SQLcom.Parameters.AddWithValue("@tempMantenimiento", obj.TemperaturaMantenimiento);

                try
                {
                    Conexion.AbrirConexion(con);
                    int id = (int)SQLcom.ExecuteScalar();
                    obj.IdPlanta = id;
                    AddNombresVulgares(obj.NombreVulgar, id);
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

        private void AddNombresVulgares(List<string> nombreVulgar, int id)
        {
            string stringNombres = nombreVulgar[0];            
            List<string> list = new List<string>();
            list = stringNombres.Split(',').ToList();
            foreach (var nombre in list)
            {
                if (nombre != null)
                {
                    SqlConnection con = Conexion.ObtenerConexion();

                    string sql = "INSERT INTO NombresVulgares VALUES " +
                        "(@NombreVulgar, @idPlanta); " +
                        "SELECT CAST (SCOPE_IDENTITY() AS INT);";

                    SqlCommand SQLcom = new SqlCommand(sql, con);

                    SQLcom.Parameters.AddWithValue("@NombreVulgar", nombre);
                    SQLcom.Parameters.AddWithValue("@idPlanta", id);                    

                    try
                    {
                        Conexion.AbrirConexion(con);
                        SQLcom.ExecuteScalar();              
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
            }
        }


        #endregion

        #region FindAll y FindById
        public IEnumerable<Planta> FindAll()
        {
            List<Planta> plantas = new List<Planta>();
            SqlConnection con = Conexion.ObtenerConexion();

            string sql = "SELECT P.*, TP.Nombre AS TipoDePlanta, TP.Descripcion AS DescripcionDeTipo, A.TipoAmbiente, TI.TipoIluminacion FROM Plantas P " +
                         "LEFT JOIN TiposDePlanta TP ON P.TipoPlanta = TP.idTipo " + "LEFT JOIN Ambientes A ON P.Ambiente = A.idAmbiente " +
                         "LEFT JOIN TiposDeIluminacion TI on P.Iluminacion = TI.idIluminacion;";

            SqlCommand SQLCom = new SqlCommand(sql, con);

            try
            {
                Conexion.AbrirConexion(con);
                SqlDataReader reader = SQLCom.ExecuteReader();

                while (reader.Read())
                {
                    Planta planta = CrearPlanta(reader);
                    planta.TipoPlanta = CrearTipo(reader);
                    planta.NombreVulgar = TraerNombresVulgaresDePlantas(planta.IdPlanta);
                    planta.Ambiente = CrearAmbiente(reader);
                    planta.TipoIluminacion = CrearIluminacion(reader);
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
            Planta buscada = null;

            SqlConnection con = Conexion.ObtenerConexion();
            string sql = "SELECT P.*, TP.Nombre AS TipoDePlanta, TP.Descripcion AS DescripcionDeTipo, A.TipoAmbiente, TI.TipoIluminacion FROM Plantas P " +
                         "LEFT JOIN TiposDePlanta TP ON P.TipoPlanta = TP.idTipo " +
                         "LEFT JOIN Ambientes A ON P.Ambiente = A.idAmbiente " +
                        $"LEFT JOIN TiposDeIluminacion TI on P.Iluminacion = TI.idIluminacion WHERE idPlanta = {id};";
            SqlCommand SQLCom = new SqlCommand(sql, con);

            try
            {
                Conexion.AbrirConexion(con);
                SqlDataReader reader = SQLCom.ExecuteReader();

                if (reader.Read())
                {
                    buscada = CrearPlanta(reader);
                    buscada.TipoPlanta = CrearTipo(reader);
                    buscada.NombreVulgar = TraerNombresVulgaresDePlantas(buscada.IdPlanta);
                    buscada.Ambiente = CrearAmbiente(reader);
                    buscada.TipoIluminacion = CrearIluminacion(reader);
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
                //SQLCom.Parameters.AddWithValue("@nomVulgar", obj.NombreVulgar); // list
                SQLCom.Parameters.AddWithValue("@descripcion", obj.Descripcion);
                SQLCom.Parameters.AddWithValue("@ambiente", obj.Ambiente);
                SQLCom.Parameters.AddWithValue("@alturaMaxima", obj.AlturaMaxima);
                SQLCom.Parameters.AddWithValue("@nomVulgar", obj.NombreVulgar);
                SQLCom.Parameters.AddWithValue("@foto", obj.Foto);
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
                string sql = "SELECT P.*, TP.Nombre AS TipoDePlanta, TP.Descripcion AS DescripcionDeTipo, A.TipoAmbiente, TI.TipoIluminacion FROM Plantas P " +
                             "LEFT JOIN TiposDePlanta TP ON P.TipoPlanta = TP.idTipo " +
                             "LEFT JOIN Ambientes A ON P.Ambiente = A.idAmbiente " +
                            $"LEFT JOIN TiposDeIluminacion TI on P.Iluminacion = TI.idIluminacion WHERE Altura >= {alturaCm};";
                SqlCommand SQLCom = new SqlCommand(sql, con);

                try
                {
                    Conexion.AbrirConexion(con);
                    SqlDataReader reader = SQLCom.ExecuteReader();

                    while (reader.Read())
                    {
                        Planta p = CrearPlanta(reader);
                        p.TipoPlanta = CrearTipo(reader);
                        p.NombreVulgar = TraerNombresVulgaresDePlantas(p.IdPlanta);
                        p.Ambiente = CrearAmbiente(reader);
                        p.TipoIluminacion = CrearIluminacion(reader);

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
                string sql = "SELECT P.*, TP.Nombre AS TipoDePlanta, TP.Descripcion AS DescripcionDeTipo, A.TipoAmbiente, TI.TipoIluminacion FROM Plantas P " +
                             "LEFT JOIN TiposDePlanta TP ON P.TipoPlanta = TP.idTipo " +
                             "LEFT JOIN Ambientes A ON P.Ambiente = A.idAmbiente " +
                            $"LEFT JOIN TiposDeIluminacion TI on P.Iluminacion = TI.idIluminacion WHERE Altura < {alturaCm};";
                SqlCommand SQLCom = new SqlCommand(sql, con);

                try
                {
                    Conexion.AbrirConexion(con);
                    SqlDataReader reader = SQLCom.ExecuteReader();

                    while (reader.Read())
                    {
                        Planta p = CrearPlanta(reader);
                        p.TipoPlanta = CrearTipo(reader);
                        p.NombreVulgar = TraerNombresVulgaresDePlantas(p.IdPlanta);
                        p.Ambiente = CrearAmbiente(reader);
                        p.TipoIluminacion = CrearIluminacion(reader);

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
        public IEnumerable<Planta> BuscarPorAmbiente(int ambiente)
        {
            List<Planta> plantasPorMabiente = new List<Planta>();

            if (ambiente != 0)
            {
                SqlConnection con = Conexion.ObtenerConexion();
                string sql = "SELECT P.*, TP.Nombre AS TipoDePlanta, TP.Descripcion AS DescripcionDeTipo, A.TipoAmbiente, TI.TipoIluminacion FROM Plantas P " +
                             "LEFT JOIN TiposDePlanta TP ON P.TipoPlanta = TP.idTipo " +
                             "LEFT JOIN Ambientes A ON P.Ambiente = A.idAmbiente " +
                            $"LEFT JOIN TiposDeIluminacion TI on P.Iluminacion = TI.idIluminacion WHERE Ambiente = {ambiente};";
                SqlCommand SQLCom = new SqlCommand(sql, con);

                try
                {
                    Conexion.AbrirConexion(con);
                    SqlDataReader reader = SQLCom.ExecuteReader();

                    while (reader.Read())
                    {
                        Planta p = CrearPlanta(reader);
                        p.TipoPlanta = CrearTipo(reader);
                        p.NombreVulgar = TraerNombresVulgaresDePlantas(p.IdPlanta);
                        p.Ambiente = CrearAmbiente(reader);
                        p.TipoIluminacion = CrearIluminacion(reader);

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
                string sql = "SELECT P.*, TP.Nombre AS TipoDePlanta, TP.Descripcion AS DescripcionDeTipo, NV.NombreVulgar, A.TipoAmbiente, TI.TipoIluminacion FROM Plantas P " +
                             "LEFT JOIN TiposDePlanta TP ON P.TipoPlanta = TP.idTipo  " +
                             "LEFT JOIN Ambientes A ON P.Ambiente = A.idAmbiente " +
                             "LEFT JOIN NombresVulgares NV ON P.idPlanta = NV.idPlanta "+
                            $"LEFT JOIN TiposDeIluminacion TI on P.Iluminacion = TI.idIluminacion WHERE NombreCientifico LIKE '%{texto}%' OR NombreVulgar LIKE '%{texto}%'";
                SqlCommand SQLCom = new SqlCommand(sql, con);

                try
                {
                    Conexion.AbrirConexion(con);
                    SqlDataReader reader = SQLCom.ExecuteReader();

                    while (reader.Read())
                    {
                        Planta p = CrearPlanta(reader);
                        p.NombreVulgar = TraerNombresVulgaresDePlantas(p.IdPlanta);
                        p.TipoPlanta = CrearTipo(reader);
                        p.Ambiente = CrearAmbiente(reader);
                        p.TipoIluminacion = CrearIluminacion(reader);

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
       
        public bool BuscarPorNombreCientifico(string nomCientifico)
        {
            bool existeNomCientifico = false;

            if (nomCientifico != null)
            {
                SqlConnection con = Conexion.ObtenerConexion();
                string sql = "SELECT P.*, TP.Nombre AS TipoDePlanta, TP.Descripcion AS DescripcionDeTipo, A.TipoAmbiente, TI.TipoIluminacion FROM Plantas P " +
                             "LEFT JOIN TiposDePlanta TP ON P.TipoPlanta = TP.idTipo  " +
                             "LEFT JOIN Ambientes A ON P.Ambiente = A.idAmbiente " +
                            $"LEFT JOIN TiposDeIluminacion TI on P.Iluminacion = TI.idIluminacion WHERE NombreCientifico = '{nomCientifico}'";

                SqlCommand SQLCom = new SqlCommand(sql, con);

                try
                {
                    Conexion.AbrirConexion(con);
                    SqlDataReader reader = SQLCom.ExecuteReader();

                    if (reader.HasRows)
                    {
                        existeNomCientifico = true;
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
            return existeNomCientifico;
        }

        public IEnumerable<Planta> BuscarPorTipo(int tipoPlanta)
        {
            List<Planta> plantasPorTipo = new List<Planta>();

            if (tipoPlanta != 0)
            {
                SqlConnection con = Conexion.ObtenerConexion();
                string sql = "SELECT P.*, TP.Nombre AS TipoDePlanta, TP.Descripcion AS DescripcionDeTipo, A.TipoAmbiente, TI.TipoIluminacion FROM Plantas P " +
                             "LEFT JOIN TiposDePlanta TP ON P.TipoPlanta = TP.idTipo " +
                             "LEFT JOIN Ambientes A ON P.Ambiente = A.idAmbiente " +
                            $"LEFT JOIN TiposDeIluminacion TI on P.Iluminacion = TI.idIluminacion WHERE TipoPlanta = {tipoPlanta};";
                SqlCommand SQLCom = new SqlCommand(sql, con);

                try
                {
                    Conexion.AbrirConexion(con);
                    SqlDataReader reader = SQLCom.ExecuteReader();

                    while (reader.Read())
                    {
                        Planta p = CrearPlanta(reader);
                        p.TipoPlanta = CrearTipo(reader);
                        p.NombreVulgar = TraerNombresVulgaresDePlantas(p.IdPlanta);
                        p.Ambiente = CrearAmbiente(reader);
                        p.TipoIluminacion = CrearIluminacion(reader);

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



        #region Métodos para Crear Ambiente, NombreVulgar, iluminación, Tipo y Planta
        private List<string> TraerNombresVulgaresDePlantas(int id)
        {
            List<string> nombresVulgares = new List<String>();

            SqlConnection con = Conexion.ObtenerConexion();
            string SQL = $"SELECT * FROM NombresVulgares WHERE idPlanta ={id}";

            SqlCommand SQLcom = new SqlCommand(SQL, con);

            try
            {
                Conexion.AbrirConexion(con);
                SqlDataReader reader = SQLcom.ExecuteReader();

                while (reader.Read())
                {
                    string nombreVulgar = reader.GetString(reader.GetOrdinal("NombreVulgar"));
                    nombreVulgar += " ";
                    nombresVulgares.Add(nombreVulgar);
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

            return nombresVulgares;
        }

        private Tipo CrearTipo(SqlDataReader reader)
        {
            Tipo tipoBuscado = new Tipo()
            {
                IdTipo = reader.GetInt32(reader.GetOrdinal("TipoPlanta")),
                Nombre = reader.GetString(reader.GetOrdinal("TipoDePlanta")),
                Descripcion = reader.GetString(reader.GetOrdinal("DescripcionDeTipo")),
            };
            return tipoBuscado;
        }

        private Ambiente CrearAmbiente(SqlDataReader reader)
        {
            Ambiente ambienteBuscado = new Ambiente()
            {
                IdAmbiente = reader.GetInt32(reader.GetOrdinal("Ambiente")),
                TipoAmbiente = reader.GetString(reader.GetOrdinal("TipoAmbiente")),
            };
            return ambienteBuscado;
        }

        private Iluminacion CrearIluminacion(SqlDataReader reader)
        {
            Iluminacion iluminacionBuscada = new Iluminacion()
            {
                IdIluminacion = reader.GetInt32(reader.GetOrdinal("Iluminacion")),
                Tipo = reader.GetString(reader.GetOrdinal("TipoIluminacion")),
            };
            return iluminacionBuscada;
        }

        private Planta CrearPlanta(SqlDataReader reader)
        {
            Planta p = new Planta()
            {
                IdPlanta = reader.GetInt32(reader.GetOrdinal("idPlanta")),
                NombreCientifico = reader.GetString(reader.GetOrdinal("NombreCientifico")),
                Descripcion = reader.GetString(reader.GetOrdinal("Descripcion")),
                AlturaMaxima = reader.GetDecimal(reader.GetOrdinal("Altura")),
                FrecuenciaRiego = reader.GetString(reader.GetOrdinal("FrecuenciaDeRiego")),
                Foto = reader.GetString(reader.GetOrdinal("Foto")),
                TemperaturaMantenimiento = reader.GetInt32(reader.GetOrdinal("TempMantenimiento"))
            };

            return p;

        }
        #endregion
    }
}