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
        public IRepositorioTipos RepoTipos { get; set; }
        public IRepositorioAmbientes RepoAmbientes { get; set; }
        public IRepositorioIluminaciones RepoIluminacion { get; set; }

        public ManejadorPlantas (IRepositorioPlantas repo, IRepositorioTipos repoTipos, IRepositorioAmbientes repoAmbientes, IRepositorioIluminaciones repoIluminacion)
        {
            RepoPlantas = repo;
            RepoTipos = repoTipos;
            RepoAmbientes = repoAmbientes;
            RepoIluminacion = repoIluminacion;
        }

        public bool ActualizarPlanta(Planta p)
        {
            return RepoPlantas.update(p);
        }

        public bool AgregarNuevaPlanta(Planta p, int idTipo, int idAmbiente, int idIluminacion)
        {
            bool ok = false;
            Tipo t = RepoTipos.FindById(idTipo);



            if (t != null)
            {
                Ambiente a = RepoAmbientes.FindById(idAmbiente);
                Iluminacion i = RepoIluminacion.FindById(idIluminacion);

                if (a != null && i != null)
                {                    
                    p.TipoPlanta = t;
                    p.Ambiente = a;
                    p.TipoIluminacion = i;
                    ok = RepoPlantas.Add(p);
                }
            }
            return ok;
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

        public IEnumerable<Tipo> TraerTodosLosTipos()
        {
            return RepoTipos.FindAll();
        }

        public IEnumerable<Ambiente> TraerTodosLosAmbientes()
        {
            return RepoAmbientes.FindAll();
        }

        public IEnumerable<Iluminacion> TraerTodasLasIluminaciones()
        {
            return RepoIluminacion.FindAll();
        }

        public IEnumerable<Planta> BuscarPlantasPorTexto(string texto)
        {
            return RepoPlantas.BuscarPorTexto(texto);
        }

        public IEnumerable<Planta> BuscarPlantasPorTipo(int tipo)
        {
            return RepoPlantas.BuscarPorTipo(tipo); 
        }

        public IEnumerable<Planta> BuscarPlantasPorAmbiente(int ambiente)
        {
            return RepoPlantas.BuscarPorAmbiente(ambiente);
        }

        public IEnumerable<Planta> BuscarPlantasMasBajas(double centimetros)
        {
            return RepoPlantas.BuscarPlantasMasBajas(centimetros);
        }

        public IEnumerable<Planta> BuscarPlantasMasAltas(double centimetros)
        {
            return RepoPlantas.BuscarPlantasMasAltas(centimetros);
        }

        public bool BuscarPlantaPorNombreCientifico(string nombreCientifico)
        {
            return RepoPlantas.BuscarPorNombreCientifico(nombreCientifico);
        }
    }
}
