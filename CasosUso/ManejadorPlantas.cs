using System;
using System.Collections.Generic;
using System.Text;
using Dominio.Entidades;
using Dominio.InterfacesRepositorio;

namespace CasosUso
{
    public class ManejadorPlantas : IManejadorPlantas
    {
        public IRepositorioPlantas RepoPlantas { get; set; }

        public ManejadorPlantas (IRepositorioPlantas repo)
        {
            RepoPlantas = repo;
        }

        public bool ActualizarPlanta(Planta p)
        {
            return RepoPlantas.update(p);
        }

        public bool AgregarNuevaPlanta(Planta p, int idTipo, int idAmbiente, int idIluminacion)
        {
            return RepoPlantas.Add(p);
        }

        public Planta BuscarPlantaPorId(int idPlanta)
        {
            return RepoPlantas.FindById(idPlanta);
        }

        public bool DarDeBajaPlanta(int idPlanta)
        {
            return RepoPlantas.Remove(idPlanta);
        }

        public IEnumerable<Planta> TraerTodasLasPlantas()
        {
            return RepoPlantas.FindAll();
        }
    }
}
