using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace CasosUso
{
    public interface IManejadorPlantas
    {
        bool AgregarNuevaPlanta(Planta p, int idTipo, int idAmbiente, int idIluminacion);

        bool DarDeBajaPlanta(int idPlanta);

        IEnumerable<Planta> TraerTodasLasPlantas();

        Planta BuscarPlantaPorId(int idPlanta);

        bool ActualizarPlanta(Planta p);
    }
}
