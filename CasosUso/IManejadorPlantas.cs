using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace CasosUso
{
    public interface IManejadorPlantas
    {
        bool AgregarNuevaPlanta(Planta p, int idTipo, int idAmbiente, int idIluminacion);

        IEnumerable<Tipo> TraerTodosLosTipos();

        IEnumerable<Ambiente> TraerTodosLosAmbientes();

        IEnumerable<Iluminacion> TraerTodasLasIluminaciones();

        bool DarDeBajaPlanta(int idPlanta);

        IEnumerable<Planta> TraerTodasLasPlantas();

        Planta BuscarPlantaPorId(int idPlanta);

        bool ActualizarPlanta(Planta p);

        public IEnumerable<Planta> BuscarPlantasPorTexto(string texto);

        public IEnumerable<Planta> BuscarPlantasPorTipo(int tipo);

        public IEnumerable<Planta> BuscarPlantasPorAmbiente(int ambiente);

        public IEnumerable<Planta> BuscarPlantasMasAltas(double centimetros);
    }
}
